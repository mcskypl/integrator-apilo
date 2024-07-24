namespace IntegratorApilo.Server.Services.HostedService;

public class InvoiceHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public InvoiceHostedService(IServiceProvider serviceProvider)
	{
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                //var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                //await orderService.Init();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
