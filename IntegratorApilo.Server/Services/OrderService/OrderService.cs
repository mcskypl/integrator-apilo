namespace IntegratorApilo.Server.Services.OrderService;

public class OrderService : IOrderService
{
    public int SyncOrdersSec { get; set; } = 0;
    public string ConnectionString { get; set; }

    private readonly IApiloOrderService _apiloOrderService;
    private readonly MainDataContext _mainDataContext;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IApiloOrderService apiloOrderService, MainDataContext mainDataContext, ILogger<OrderService> logger)
    {
        _apiloOrderService = apiloOrderService;
        _mainDataContext = mainDataContext;
        _logger = logger;
    }

    public async Task<ServiceResponse<bool>> Init()
    {
        _logger.LogInformation($"OrderService");

        var apiloShops = await _mainDataContext.ApiloShop.Include(e => e.ApiloShopSettings)
                                                         .Include(e => e.ApiloConnections)
                                                            .ThenInclude(c => c.ApiloAccounts)
                                                         .ToListAsync() ?? throw new Exception("SYSTEMST.XXX_APILO_SHOP is null");

        while (true)
        {
            foreach (var shop in apiloShops)
            {
                _logger.LogInformation($"Synchronizacja sklepu: {shop.ShopName}");
                _mainDataContext.Entry(shop).Reload();

                if (shop.ApiloConnections == null || shop.ApiloShopSettings.FirstOrDefault(x => x.IdSetting == 10009).StringValue == "0") continue;

                var ordersListObject = await _apiloOrderService.GetSimpleListOfOrders(shop.IdShop);
                if (!ordersListObject.Success) throw new Exception(ordersListObject.Message);
                var ordersList = ordersListObject.Data.Orders ?? throw new Exception("ordersListObject.Data is null");

                var apiloShopSetting__lastUpdate = await _mainDataContext.ApiloShopSetting.FirstOrDefaultAsync(x => x.IdShop == shop.IdShop && x.IdSetting == 10010) 
                    ?? throw new Exception("SYSTEMST.XXX_APILO_SHOP_SETTINGS is null");


                foreach (var order in ordersList)
                {
                    if (shop.IdShop == 2)
                    {
                        if (order.Status == 8 || order.Status == 32 || order.Status == 53 || order.Status == 8 || order.Status == 74 || order.Status == 26 || order.Status == 47 || order.Status == 68)
                        {

                        }
                        else continue;
                    }

                    if (shop.IdShop == 1)
                    {
                        if (order.Status == 8 || order.Status == 38 || order.Status == 51 || order.Status == 78 || order.Status == 99 ||
                            order.Status == 120 || order.Status == 141 || order.Status == 162 || order.Status == 174 || order.Status == 200 ||
                            order.Status == 221 || order.Status == 242 || order.Status == 48 || order.Status == 72 || order.Status == 93 ||
                            order.Status == 114 || order.Status == 135 || order.Status == 156 || order.Status == 168 || order.Status == 194 ||
                            order.Status == 215 || order.Status == 236)
                        {

                        }
                        else continue;
                    }
                    _logger.LogInformation($"Pobieranie zamówienia: {order.Id}");

                    foreach (var database in shop.ApiloConnections)
                    {
                        ConnectionString = database.ConnectionString;

                        try
                        {
                            string idConnection = database.IdConnection.ToString();
                            
                            using (DataContext context = new DataContext(ConnectionString))
                            {
                                var urzzewnagl = await context.Urzzewnagl.FirstOrDefaultAsync(u => u.OdbNrdok.Contains("APILO" + idConnection + "_" + order.Id));
                                if (urzzewnagl != null) 
                                {
                                    _logger.LogInformation("Zamówienie istnieje w urządzeniu zewnętrznym");

                                    await _mainDataContext.Entry(apiloShopSetting__lastUpdate).ReloadAsync();
                                    apiloShopSetting__lastUpdate.DatetimeValue = DateTime.Parse(order.UpdatedAt);
                                    await _mainDataContext.SaveChangesAsync();

                                    continue;
                                } 
                            }//darek to fujara
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"{ex.Message}");
                        }
                        

                        try
                        {
                            _logger.LogInformation($"Pobieranie detalów zamówienia: {order.Id}");
                            var orderDetails = await _apiloOrderService.GetOrderDetails(shop.IdShop, order.Id);
                            _logger.LogInformation($"Pobrano detale zamówienia: {orderDetails.Data.Id}");

                            var apiloAccount = database.ApiloAccounts.FirstOrDefault(x => x.PlatformAccountId == orderDetails.Data.PlatformAccountId);

                            if (apiloAccount == null) 
                            {
                                await _mainDataContext.Entry(apiloShopSetting__lastUpdate).ReloadAsync();
                                apiloShopSetting__lastUpdate.DatetimeValue = DateTime.Parse(order.UpdatedAt);
                                await _mainDataContext.SaveChangesAsync();

                                continue;
                            }


                            if (apiloAccount.Active == 0)
                            {
                                await _mainDataContext.Entry(apiloShopSetting__lastUpdate).ReloadAsync();
                                apiloShopSetting__lastUpdate.DatetimeValue = DateTime.Parse(order.UpdatedAt);
                                await _mainDataContext.SaveChangesAsync();

                                continue;
                            }

                            _logger.LogInformation($"Gotowe do stworzenia: {order.Id}");


                            int idKontrah = 0;
                            if (apiloAccount.IdKontrah == null)
                            {
                                var addKontrah = await AddKontrah(orderDetails.Data);
                                idKontrah = addKontrah.Data;
                            }
                            else idKontrah = (int)apiloAccount.IdKontrah;

                            _logger.LogInformation($"Kontrahent dla {order.Id} - {idKontrah}");

                            var addOrder = await AddOrder(idKontrah, orderDetails.Data, apiloAccount);

                            if (!addOrder.Success)
                            {
                                _logger.LogError(addOrder.Message);
                            }
                            else
                            {
                                _logger.LogInformation($"Dodano zamówienie {addOrder.Data}");
                            }



                            await _mainDataContext.Entry(apiloShopSetting__lastUpdate).ReloadAsync();
                            apiloShopSetting__lastUpdate.DatetimeValue = DateTime.Parse(order.UpdatedAt);
                            await _mainDataContext.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }

                    }
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(2));
        }
    }

    public async Task<ServiceResponse<int>> AddKontrah(ApiloOrderDetails order)
    {
        ServiceResponse<int> serviceResponse = new();
        int idKontrah = 0;

        using (DataContext context = new DataContext(ConnectionString))
        {
            if (order.IsInvoice == true)
            {
                var apiloKontrah = await context.ApiloKontrah(order.AddressInvoice.Name, order.AddressInvoice.Phone, order.AddressInvoice.Email, order.AddressInvoice.Country, "", order.AddressInvoice.Name, order.AddressInvoice.CompanyTaxNumber, order.AddressInvoice.City, order.AddressInvoice.ZipCode, order.AddressInvoice.StreetName + " " + order.AddressInvoice.StreetNumber);
                serviceResponse.Data = apiloKontrah.IdKontrah;
            }
            else
            {
                var apiloKontrah = await context.ApiloKontrah(order.AddressCustomer.Name, order.AddressCustomer.Phone, order.AddressCustomer.Email, order.AddressCustomer.Country, "", order.AddressCustomer.Name, null, order.AddressCustomer.City, order.AddressCustomer.ZipCode, order.AddressCustomer.StreetName + " " + order.AddressCustomer.StreetNumber);
                serviceResponse.Data = apiloKontrah.IdKontrah;
            }
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> AddOrder(int idKontrah, ApiloOrderDetails order, ApiloAccount apiloAccount)
    {
        _logger.LogInformation($"Kontrahent dla {order.Id} - {idKontrah}");
        ServiceResponse<int> serviceResponse = new();

        using (DataContext context = new DataContext(ConnectionString))
        {
            string idConnection = apiloAccount.IdConnection.ToString();

            UrzzewnaglAdd9Request urzzewnaglAdd9Request = new()
            {
                AidUrzzewskad = 1,
                Akodurz = apiloAccount.KodUrzzew,
                Anruzyt = "(INT)",
                AodbIdkontrah = idKontrah.ToString(),
                Ajakinumerkontrah = 2,
                AodbData = DateTime.Parse(order.CreatedAt).Date,
                AodbNazwadok = "ZA",
                AodbNrdok = "APILO" + idConnection + "_" + order.Id,
                AodbSuma = float.Parse(order.OriginalAmountTotalWithTax, System.Globalization.CultureInfo.InvariantCulture),
                AodbIlepoz = order.OrderItems.Count(),
                AodbCecha1 = order.IdExternal,
                AodbCecha2 = order.CustomerLogin
            };

            var urzzewnaglAdd9 = await context.UrzzewnaglAdd9(urzzewnaglAdd9Request);

            string deliveryMethod = string.Empty;
            string deliveryPrice = string.Empty;

            foreach (var poz in order.OrderItems)
            {
                if (poz.Type == 2) 
                {
                    poz.Sku = "00-OPŁATA-PRZESYŁKA";
                    deliveryMethod = poz.OriginalName;
                    deliveryPrice = poz.OriginalPriceWithTax;
                }

                var kartoteka = await context.Kartoteka.FirstOrDefaultAsync(k => k.Indeks == poz.Sku);
                if (kartoteka == null) poz.Sku = "BRAK_INDEKSU";

                var priceWithTax = float.Parse(poz.OriginalPriceWithTax, System.Globalization.CultureInfo.InvariantCulture);

                if (priceWithTax == 0) continue;

                UrzzewpozAdd9Request urzzewpozAdd9Request = new()
                {
                    AidUrzzewnagl = urzzewnaglAdd9.AidUrzzewnagl,
                    Akodtow = poz.Sku,
                    Ailosc = (int)poz.Quantity,
                    Acena = priceWithTax,
                    Acenauzg = 1,
                    Aprocbonif = 0,
                    AodbUwagi = poz.OriginalName,
                    AodbCenaBrutto = 1,
                };

                var urzzewpozAdd9 = await context.UrzzewpozAdd9(urzzewpozAdd9Request);
            }

            UrzzewnaglRealizZamRequest urzzewnaglRealizZamwewRequest = new()
            {
                AidUrzzewnagl = urzzewnaglAdd9.AidUrzzewnagl,
                Kasujurzzewnagl = 0
            };

            var urzzewnaglRealizZamwew = await context.UrzzewnaglRealizZam(urzzewnaglRealizZamwewRequest);

            if (order.AddressDelivery.Name == null) order.AddressDelivery.Name = string.Empty;
            if (order.AddressDelivery.ParcelName == null) order.AddressDelivery.ParcelName = string.Empty;
            if (order.AddressDelivery.StreetName == null) order.AddressDelivery.StreetName = string.Empty;
            if (order.AddressDelivery.StreetNumber == null) order.AddressDelivery.StreetNumber = string.Empty;
            if (order.AddressDelivery.ZipCode == null) order.AddressDelivery.ZipCode = string.Empty;
            if (order.AddressDelivery.City == null) order.AddressDelivery.City = string.Empty;
            if (order.AddressDelivery.Country == null) order.AddressDelivery.Country = string.Empty;
            if (order.AddressDelivery.Phone == null) order.AddressDelivery.Phone = string.Empty;
            if (order.AddressDelivery.Email == null) order.AddressDelivery.Email = string.Empty;

            string uwagi = $"### Dane wysyłki ###\n" +
                           $"{order.AddressDelivery.Name}\n" +
                           $"{order.AddressDelivery.StreetName} {order.AddressDelivery.StreetNumber}\n" +
                           $"{order.AddressDelivery.City} {order.AddressDelivery.Country}\n" +
                           $"{order.AddressDelivery.Phone}\n" +
                           $"{order.AddressDelivery.Email}\n" +
                           $"{order.AddressDelivery.ParcelName}";



            //+ deliveryMethod + "[ " + deliveryPrice + " " + order.OriginalCurrency + " ] " +
            //                  //"\nSposób płatności: " + order.PaymentMethod + " [ zapłacono: " + order.PaymentDone + " " + order.Currency + " ]" + pobranieString +
            //                  "\n\n" + order.AddressDelivery.Name +/* " " + order.DeliveryCompany +*/
            //                  "\n" + order.DeliveryAddress + ", " + order.DeliveryPostcode + " " + order.DeliveryCity + ", " + order.DeliveryCountryCode +
            //                  "\n" + order.DeliveryPointName + ", " + order.DeliveryPointAddress + ", " + order.DeliveryPointPostcode + " " + order.DeliveryPointCity +
            //                  "\n\nLogin użytkownika: " + order.UserLogin +
            //                  "\nNumer transakcji: " + order.ExternalOrderId +
            //                  "\nStrona z zamówieniem: " + order.OrderPage;
            //"\nWiadomość od klienta: " + order.UserComments;

            try
            {
                var nagl = await context.Nagl.FirstOrDefaultAsync(n => n.IdNagl == urzzewnaglRealizZamwew.AidNagl);
                nagl.Napodstawie = $"{order.Id} - {order.CustomerLogin}";
                nagl.Uwagi = uwagi;

                context.Nagl.Update(nagl);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (order.PlatformAccountId == 15) // Tylko dla Radiopaka
            {
                try
                {
                    var naglzamodb =  await context.Naglzamodb.FirstOrDefaultAsync(n => n.IdNagl == urzzewnaglRealizZamwew.AidNagl);
                    naglzamodb.IdDefdokwyst = 50;
                    context.Naglzamodb.Update(naglzamodb);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            
            if (order.PlatformAccountId == 11) // Tylko dla AAmo
            {
                try
                {
                    var naglzamodb =  await context.Naglzamodb.FirstOrDefaultAsync(n => n.IdNagl == urzzewnaglRealizZamwew.AidNagl);
                    naglzamodb.IdDefdokwyst = 10246;
                    context.Naglzamodb.Update(naglzamodb);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
           

            serviceResponse.Data = (int)urzzewnaglRealizZamwew.AidNagl;
        }

        return serviceResponse;
    }
}
