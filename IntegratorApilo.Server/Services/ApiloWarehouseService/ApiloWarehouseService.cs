using System.Text.Json;

namespace IntegratorApilo.Server.Services.ApiloWarehouseService;

public class ApiloWarehouseService : IApiloWarehouseService
{
    private readonly RestClient _restClient;
    private readonly SystemstDataContext _systemstContext;

    public ApiloWarehouseService(RestClient restClient, SystemstDataContext systemstContext)
    {
        _restClient = restClient;
        _systemstContext = systemstContext;
    }

    public async Task<ServiceResponse<ApiloProducts>> GetProductsList(int idConfig)
    {
        var apiloConfig = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).FirstOrDefaultAsync(x => x.IdConfig == idConfig)
            ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");

        var result = new ServiceResponse<ApiloProducts>();

        try
        {
            var request = new RestRequest($"{apiloConfig.ApiAddress}/rest/api/warehouse/product/", Method.Get);
            request.AddHeader("Authorization", $"Bearer {apiloConfig.AccessToken}");
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.Content == null) throw new Exception("Content is null");

            if (!response.IsSuccessStatusCode)
            {
                var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                throw new Exception(apiloErrorResponse.Message);
            }

            var apiloProducts = JsonSerializer.Deserialize<ApiloProducts>(response.Content);
            result.Data = apiloProducts;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<ApiloOrders>> UpdateProducts(ApiloConfig apiloConfig)
    {
        throw new NotImplementedException();
    }
}
