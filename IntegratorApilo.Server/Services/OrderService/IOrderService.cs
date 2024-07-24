namespace IntegratorApilo.Server.Services.OrderService;

public interface IOrderService
{
    Task<ServiceResponse<bool>> Init();
    Task<ServiceResponse<int>> AddKontrah(ApiloOrderDetails order);
    Task<ServiceResponse<int>> AddOrder(int idKontrah, ApiloOrderDetails order, ApiloAccount apiloAccount);
}
