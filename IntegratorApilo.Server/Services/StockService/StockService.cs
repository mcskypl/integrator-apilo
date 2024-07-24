using static IntegratorApilo.Shared.Apilo.ApiloProducts;

namespace IntegratorApilo.Server.Services.StockService;

public class StockService : IStockService
{
    private readonly SystemstDataContext _systemstContext;
    private readonly IApiloWarehouseService _apiloWarehouseService;
    private readonly ILogger<StockService> _logger;

    public StockService(SystemstDataContext systemstContext, IApiloWarehouseService apiloWarehouseService, ILogger<StockService> logger)
    {
        _systemstContext = systemstContext;
        _apiloWarehouseService = apiloWarehouseService;
        _logger = logger;
    }

    public async Task<ServiceResponse<bool>> Init()
    {
        while (true)
        {
            _logger.LogInformation($"Pobieranie konfiguracji z bazy...");
            var apiloConfigs = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).ToListAsync() ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");

            foreach (var apiloConfig in apiloConfigs)
            {
                _logger.LogInformation($"Pobrano konfigurację {apiloConfig.AppName}");

                try
                {
                    if (apiloConfig.ApiloDatabases == null || apiloConfig.SyncStocksMin == 0)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1));
                        continue;
                    }

                    var database = apiloConfig.ApiloDatabases.FirstOrDefault(x => x.SyncStocks != 0);
                    if (database == null) continue;

                    _logger.LogInformation($"Synchronizacja stanów z bazy {database.DatabaseName}");

                    _logger.LogInformation($"Pobieranie kartotek z Apilo...");
                    var produktyApilo = await _apiloWarehouseService.GetProductsList(apiloConfig.IdConfig);
                    _logger.LogInformation($"Pobrano kartotek z Apilo: {produktyApilo.Data.Count()}");

                    _logger.LogInformation($"Pobieranie kartotek z bazy Streamsoft...");
                    var kartoteki = await GetStocksFromDatabase(apiloConfig.IdConfig, database.IdDatabase);
                    _logger.LogInformation($"Pobrano kartotek z bazy Streamsoft: {kartoteki.Data.Count()}");

                    var productsToUpdate = new List<ApiloProduct>();

                    foreach (var kartoteka in kartoteki.Data)
                    {
                        try
                        {
                            var produktApilo = produktyApilo.Data.Where(x => x.Sku == kartoteka.Indeks).FirstOrDefault();

                            if (produktApilo == null)
                            {
                                _logger.LogInformation($"Nie znaleziono indeksu {kartoteka.Indeks} w bazie Apilo");
                                continue;
                            }

                            if (kartoteka.Stanmag == null) continue;

                            if (produktApilo.Quantity != kartoteka.Stanmag.Standysp)
                            {
                                var apiloProduct = new ApiloProduct()
                                {
                                    Id = produktApilo.Id,
                                    Quantity = (int)kartoteka.Stanmag.Standysp
                                };

                                productsToUpdate.Add(apiloProduct);

                                _logger.LogInformation($"Do aktualizacji ilość produkt {kartoteka.Indeks} [{produktApilo.Quantity}] -> [{kartoteka.Stanmag.Standysp}]");
                            }
                            else
                            {
                                _logger.LogInformation($"Bez zmian w ilości produktu {kartoteka.Indeks} [{produktApilo.Quantity}] -> [{kartoteka.Stanmag.Standysp}]");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"{kartoteka.Indeks} - {ex.Message}");
                        }
                    }

                    if (productsToUpdate.Count() > 0)
                    {
                        var updateProducts = await _apiloWarehouseService.UpdateProducts(apiloConfig.IdConfig, productsToUpdate);

                        if (updateProducts.Success) _logger.LogInformation($"Zaktualizowano produktów {updateProducts.Data}");
                        else _logger.LogError($"Błąd w trakcie aktualizowania produktów {updateProducts.Message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}");
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(10));
        }
    }

    public async Task<ServiceResponse<List<Kartoteka>>> GetStocksFromDatabase(int idConfig, int idDatabase)
    {
        ServiceResponse<List<Kartoteka>> result = new();

        try
        {
            var apiloConfig = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).FirstOrDefaultAsync(x => x.IdConfig == idConfig)
                ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");

            string? connectionString = apiloConfig.ApiloDatabases.FirstOrDefault(x => x.IdDatabase == idDatabase).ConnectionString;

            using (DataContext context = new DataContext(connectionString))
            {
                var kartoteki = await context.Kartoteka.Include(e => e.Stanmag).Where(k => k.Stanmag.IdMagazyn == apiloConfig.IdMagazynStocks).ToListAsync();
                result.Data = kartoteki;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }
}
