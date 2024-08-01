namespace IntegratorApilo.Server.Services.HostedService;

public class OrderSyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OrderSyncService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                try
                {
                    await orderService.Init();
                }
                catch (Exception ex)
                {
                    // Czekanie 10 minut przed ponowną próbą
                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
            }
        }
    }
}
