using static IntegratorApilo.Shared.Apilo.ApiloProducts;

namespace IntegratorApilo.Server.Services.ApiloWarehouseService;

public interface IApiloWarehouseService
{
    Task<ServiceResponse<List<ApiloProduct>>> GetProductsList(int idConfig);
    Task<ServiceResponse<bool>> UpdateProducts(int idConfig, ApiloProduct products);
}
