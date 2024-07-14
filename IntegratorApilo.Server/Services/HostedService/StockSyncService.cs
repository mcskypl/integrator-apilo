using IntegratorApilo.Server.Services.StockService;

namespace IntegratorApilo.Server.Services.HostedService;

public class StockSyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public StockSyncService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();
            await stockService.Init();
        }
    }
}
