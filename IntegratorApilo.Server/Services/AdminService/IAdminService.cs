namespace IntegratorApilo.Server.Services.AdminService;

public interface IAdminService
{
    Task<ServiceResponse<List<ApiloConfig>>> GetConfigs();
    Task<ServiceResponse<List<ApiloShop>>> GetShops();
    Task<ServiceResponse<List<ApiloDatabase>>> UpdateConfig(ApiloConfig apiloConfig);
    Task<ServiceResponse<List<ApiloShop>>> UpdateShop(ApiloShop apiloShop);
    Task<ServiceResponse<List<ApiloShop>>> AddShop(ApiloShop apiloShop);
}
