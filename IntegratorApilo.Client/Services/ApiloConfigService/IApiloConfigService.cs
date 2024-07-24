using IntegratorApilo.Shared.Streamsoft;

namespace IntegratorApilo.Client.Services.ApiloConfigService;

public interface IApiloConfigService
{
    Task<List<ApiloShop>> GetApiloConfig();
    Task<ApiloShop> UpdateApiloShop(ApiloShop apiloShop);
    Task<ApiloShop> AddApiloShop(ApiloShop apiloShop);
}
