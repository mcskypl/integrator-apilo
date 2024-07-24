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
        using (var scope = _serviceProvider.CreateScope())
        {
            while (true)
            {
                try
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                    await orderService.Init();
                }
                catch (Exception ex)
                {
                    await Task.Delay(TimeSpan.FromMinutes(10));
                }
            }
        }
    }
}
