namespace IntegratorApilo.Server.Services.ApiloWarehouseService;

public interface IApiloWarehouseService
{
    Task<ServiceResponse<ApiloProducts>> GetProductsList(int idConfig);
    Task<ServiceResponse<ApiloOrders>> UpdateProducts(ApiloConfig apiloConfig);
}
