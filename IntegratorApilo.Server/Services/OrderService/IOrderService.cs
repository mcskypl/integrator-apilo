namespace IntegratorApilo.Server.Services.OrderService;

public interface IOrderService
{
    Task<ServiceResponse<bool>> Init();
    Task<ServiceResponse<int>> AddKontrah(ApiloOrders.Order order);
}
