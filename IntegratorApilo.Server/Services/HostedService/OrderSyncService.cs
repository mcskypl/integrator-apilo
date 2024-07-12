namespace IntegratorApilo.Server.Services.HostedService;

public class OrderSyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OrderSyncService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public int SyncOrdersSec { get; set; } = 10;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                await orderService.Init();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
