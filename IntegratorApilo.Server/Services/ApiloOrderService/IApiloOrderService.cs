namespace IntegratorApilo.Server.Services.ApiloOrderService;

public interface IApiloOrderService
{
    Task<ServiceResponse<ApiloOrders>> GetSimpleListOfOrders(int idShop);
    Task<ServiceResponse<ApiloOrderDetails>> GetOrderDetails(int idShop, string idApiloOrder);
    Task<ServiceResponse<List<ApiloOrderStatus>>> GetOrderStatus(int idShop);
}
