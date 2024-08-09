using System.Globalization;
using IntegratorApilo.Server.Services.ApiloFinanceDocumentService;

namespace IntegratorApilo.Server.Services.InvoiceService;

public class InvoiceService : IInvoiceService
{
    public string ConnectionString { get; set; }

    private readonly MainDataContext _mainDataContext;
    private readonly ILogger<InvoiceService> _logger;
    private readonly IApiloFinanceDocumentService _apiloFinanceDocumentService;

    public InvoiceService(MainDataContext mainDataContext, ILogger<InvoiceService> logger, IApiloFinanceDocumentService apiloFinanceDocumentService)
    {
        _mainDataContext = mainDataContext;
        _logger = logger;
        _apiloFinanceDocumentService = apiloFinanceDocumentService;
    }

    public async Task<ServiceResponse<bool>> Init()
    {
        _logger.LogInformation($"InvoiceService");

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

                foreach (var database in shop.ApiloConnections)
                {
                    if (database.DatabaseName != "ALBO") continue;
                    
                    // if (database.SyncInvoices == 0) continue;

                    _logger.LogInformation($"Synchronizacja faktur z bazą: {database.DatabaseName}");
                    ConnectionString = database.ConnectionString;

                    _logger.LogInformation($"Pobieranie dokumentów z Apilo");
                    var listOfAccountingDocuments = await _apiloFinanceDocumentService.GetListOfAccountingDocuments(shop.IdShop, (int)database.SyncInvoices);
                    _logger.LogInformation($"Pobrano dokumentów: {listOfAccountingDocuments.Data.Documents.Count()}");

                    foreach (var document in listOfAccountingDocuments.Data.Documents)
                    {
                        _logger.LogInformation($"Pobieranie dokumentu sprzedaży: {document.DocumentNumber}");
                        
                        try
                        {
                            using (DataContext context = new DataContext(database.ConnectionString))
                            {
                                string kodurzzew = "FV_BL";
                                if (shop.IdShop == 1) kodurzzew = "APILO_GDY";
                                
                                var urzzewnagl = await context.EshopImpFv(document, kodurzzew);
                                
                                foreach (var item in document.DocumentItems)
                                {
                                    _logger.LogInformation($"Dodawanie pozycji do dokumentu sprzedaży: {document.DocumentNumber} -> {item.Sku} # {item.Name}");
                                    
                                    var tax = float.Parse(item.Tax, CultureInfo.InvariantCulture);
                                    string kodTowaru = "";

                                    if (tax == 23) kodTowaru = "KART_VAT_23";
                                    if (tax == 0) kodTowaru = "KART_VAT_0";
                                    if (tax == 5) kodTowaru = "KART_VAT_5";
                                    if (tax == 8) kodTowaru = "KART_VAT_8";
                                    
                                    UrzzewpozAdd9Request urzzewpozAdd9Request = new()
                                    {
                                        AidUrzzewnagl = urzzewnagl.Success.Value,
                                        Akodtow = kodTowaru,
                                        Ailosc = item.Quantity.Value,
                                        Acena = float.Parse(item.OriginalPriceWithTax, CultureInfo.InvariantCulture),
                                        Acenauzg = 1,
                                        Aprocbonif = 0,
                                        AodbUwagi = item.Name,
                                        AodbCenaBrutto = 1,
                                        AodbCecha1 = item.Sku,
                                        AodbCecha2 = "KART_BL"
                                    };

                                    var urzzewpozAdd9 = await context.UrzzewpozAdd9(urzzewpozAdd9Request);

                                    var urzzewnaglMain = await context.Urzzewnagl.FirstOrDefaultAsync(u => u.IdUrzzewnagl == urzzewnagl.Success.Value);

                                    urzzewnaglMain.Status = 3;
                                    await context.SaveChangesAsync();

                                    // var asdasd = await context.EshopImpFvItem(item, document.Id);
                                    // await context.SaveChangesAsync();
                                }
                            }

                            
                        }
                        catch (Exception ex)
                        {
                            string innerException = "";

                            if (ex.InnerException != null) innerException = ex.InnerException.Message;
                            
                            _logger.LogError($"Błąd przy dodawaniu dokumentu sprzedaży: {ex.Message} {innerException}");
                        }
                        
                        await _mainDataContext.Entry(database).ReloadAsync();
                        database.SyncInvoices = database.SyncInvoices + 1;
                        await _mainDataContext.SaveChangesAsync();

                        // return null;
                    }

                }
            }

            await Task.Delay(TimeSpan.FromMinutes(10));
        }
    }
}
