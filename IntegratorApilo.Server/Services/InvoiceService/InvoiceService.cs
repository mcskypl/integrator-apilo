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
                    if (database.SyncInvoices == 0) continue;

                    _logger.LogInformation($"Synchronizacja faktur z bazą: {database.DatabaseName}");
                    ConnectionString = database.ConnectionString;

                    _logger.LogInformation($"Pobieranie dokumentów z Apilo");
                    var listOfAccountingDocuments = await _apiloFinanceDocumentService.GetListOfAccountingDocuments(shop.IdShop);
                    _logger.LogInformation($"Pobrano dokumentów: {listOfAccountingDocuments.Data.Documents.Count()}");


                }
            }
        }
    }
}
