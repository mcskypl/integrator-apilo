using IntegratorApilo.Shared;
using IntegratorApilo.Shared.Streamsoft;
using System.Net.Http.Json;

namespace IntegratorApilo.Client.Services.ApiloConfigService;

public class ApiloConfigService : IApiloConfigService
{
    private readonly HttpClient _http;

    public ApiloConfigService(HttpClient http)
    {
        _http = http;
    }

   

    public async Task<List<ApiloShop>> GetApiloConfig()
    {
        var response = await _http.GetAsync("api/admin-dashboard/shops");
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<ApiloShop>>>();

        return cartProducts.Data;
    }

    public async Task<ApiloShop> AddApiloShop(ApiloShop apiloShop)
    {
        var response = await _http.PostAsJsonAsync("api/admin-dashboard/shop", apiloShop);
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<ApiloShop>>();

        return cartProducts.Data;
    }

    public async Task<ApiloShop> UpdateApiloShop(ApiloShop apiloShop)
    {
        var response = await _http.PatchAsJsonAsync("api/admin-dashboard/shop", apiloShop);
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<ApiloShop>>();

        return cartProducts.Data;
    }
}
