using IntegratorApilo.Shared.Streamsoft;
using Microsoft.Extensions.Logging;

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
        _logger.LogInformation($"Pobieranie konfiguracji z bazy...");
        var apiloConfig = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).FirstOrDefaultAsync()
            ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");
        _logger.LogInformation($"Pobrano konfigurację {apiloConfig.AppName}");

        while (true)
        {
            try
            {
                _systemstContext.Entry(apiloConfig).Reload();

                if (apiloConfig.ApiloDatabases == null || apiloConfig.SyncStocksMin == 0) 
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    continue;
                }

                _logger.LogInformation($"Pobieranie kartotek z Apilo...");
                var produktyApilo = await _apiloWarehouseService.GetProductsList(1);
                _logger.LogInformation($"Pobrano kartotek z Apilo: {produktyApilo.Data.Count()}");

                var kartoteki = await GetStocksFromDatabase(1, 1);
                _logger.LogInformation($"Pobrano kartotek z bazy Streamsoft: {kartoteki.Data.Count()}");
                

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
                            produktApilo.Quantity = (int)produktApilo.Quantity;
                            await _apiloWarehouseService.UpdateProducts(1, produktApilo);

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
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMinutes(1));
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
