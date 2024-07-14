using IntegratorApilo.Shared;
using IntegratorApilo.Shared.Streamsoft;
using Microsoft.Extensions.Options;

namespace IntegratorApilo.Server.Services.OrderService;

public class OrderService : IOrderService
{
    public int SyncOrdersSec { get; set; } = 0;
    public string ConnectionString { get; set; }

    private readonly IApiloOrderService _apiloOrderService;
    private readonly SystemstDataContext _systemstContext;

    public OrderService(IApiloOrderService apiloOrderService, SystemstDataContext systemstContext)
    {
        _apiloOrderService = apiloOrderService;
        _systemstContext = systemstContext;
    }

    public async Task<ServiceResponse<bool>> Init()
    {
        var apiloConfig = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).FirstOrDefaultAsync() 
            ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");

        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));

            _systemstContext.Entry(apiloConfig).Reload();
            if (apiloConfig.ApiloDatabases == null || apiloConfig.SyncOrdersMin == 0) continue;

            var ordersListObject = await _apiloOrderService.GetSimpleListOfOrders(apiloConfig);
            if (!ordersListObject.Success) throw new Exception(ordersListObject.Message);
            var ordersList = ordersListObject.Data.Orders ?? throw new Exception("ordersListObject.Data is null");

            foreach (var order in ordersList)
            {
                foreach (var database in apiloConfig.ApiloDatabases)
                {
                    ConnectionString = database.ConnectionString;

                    try
                    {
                        await AddKontrah(order);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        return null;
        //while (true)
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(SyncOrdersSec));

        //    try
        //    {
        //        _context.Entry(apiloConfig).Reload();

        //        if (apiloConfig.SyncOrdersMin == 0)
        //        {
        //            SyncOrdersSec = 10;
        //            continue;
        //        }

        //        SyncOrdersSec = apiloConfig.SyncOrdersMin * 60;

        //        var ordersListObject = await _apiloOrderService.GetSimpleListOfOrders();

        //        if (!ordersListObject.Success) throw new Exception(ordersListObject.Message);
        //        var ordersList = ordersListObject.Data.Orders ?? throw new Exception("ordersListObject.Data is null");

        //        foreach (var order in ordersList)
        //        {
        //            int idKontrah = 0;
        //            if (order.IsInvoice == true)
        //            {

        //            }
        //            else
        //            {
        //                var apiloKontrah = await _context.ApiloKontrah(order.AddressCustomer.Name, order.AddressCustomer.Phone, order.AddressCustomer.Email, order.AddressCustomer.Country, "", order.AddressCustomer.Name, null, order.AddressCustomer.City, order.AddressCustomer.ZipCode, order.AddressCustomer.StreetName + " " + order.AddressCustomer.StreetNumber);
        //                idKontrah = apiloKontrah.IdKontrah;
        //            }

        //            UrzzewnaglAdd9Request urzzewnaglAdd9Request = new()
        //            {
        //                AidUrzzewskad = 1,
        //                Akodurz = "BAS",
        //                Anruzyt = "(INT)",
        //                AodbIdkontrah = idKontrah.ToString(),
        //                Ajakinumerkontrah = 2,
        //                AodbData = DateTime.Parse(order.CreatedAt).Date,
        //                AodbNazwadok = "ZA",
        //                AodbNrdok = order.Id,
        //                AodbSuma = 1,
        //                AodbIlepoz = order.OrderItems.Count(),
        //                AodbCecha1 = order.IdExternal
        //            };

        //            var urzzewnaglAdd9 = await _context.UrzzewnaglAdd9(urzzewnaglAdd9Request);

        //            foreach (var poz in order.OrderItems)
        //            {
        //                if (poz.Sku == null) poz.Sku = "TRANSPORT";

        //                var kartoteka = await _context.Kartoteka.FirstOrDefaultAsync(k => k.Indeks == poz.Sku);
        //                if (kartoteka == null) poz.Sku = "BRAK_INDEKSU";

        //                UrzzewpozAdd9Request urzzewpozAdd9Request = new()
        //                {
        //                    AidUrzzewnagl = urzzewnaglAdd9.AidUrzzewnagl,
        //                    Akodtow = poz.Sku,
        //                    Ailosc = (int)poz.Quantity,
        //                    Acena = float.Parse(poz.OriginalPriceWithTax, System.Globalization.CultureInfo.InvariantCulture),
        //                    Acenauzg = 1,
        //                    Aprocbonif = 0,
        //                    AodbUwagi = poz.OriginalName
        //                };

        //                var urzzewpozAdd9 = await _context.UrzzewpozAdd9(urzzewpozAdd9Request);
        //            }

        //            UrzzewnaglRealizZamwewRequest urzzewnaglRealizZamwewRequest = new()
        //            {
        //                AidUrzzewnagl = urzzewnaglAdd9.AidUrzzewnagl,
        //                Kasujurzzewnagl = 0
        //            };

        //            var urzzewnaglRealizZamwew = await _context.UrzzewnaglRealizZamwew(urzzewnaglRealizZamwewRequest);

        //            await _context.Entry(apiloConfig).ReloadAsync();
        //            apiloConfig.LastUpdatedAt = DateTime.Parse(order.UpdatedAt);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Obsłuż błąd (logowanie itp.)
        //    }
        //}
    }

    public async Task<ServiceResponse<int>> AddKontrah(ApiloOrders.Order order)
    {
        ServiceResponse<int> serviceResponse = new();
        int idKontrah = 0;

        using (DataContext context = new DataContext(ConnectionString))
        {
            if (order.IsInvoice == true)
            {

            }
            else
            {
                var apiloKontrah = await context.ApiloKontrah(order.AddressCustomer.Name, order.AddressCustomer.Phone, order.AddressCustomer.Email, order.AddressCustomer.Country, "", order.AddressCustomer.Name, null, order.AddressCustomer.City, order.AddressCustomer.ZipCode, order.AddressCustomer.StreetName + " " + order.AddressCustomer.StreetNumber);
                idKontrah = apiloKontrah.IdKontrah;
            }
        }

        return serviceResponse;
    }
}
