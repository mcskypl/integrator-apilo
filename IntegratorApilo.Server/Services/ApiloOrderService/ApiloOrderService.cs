using IntegratorApilo.Shared.Streamsoft;
using System;
using System.Text.Json;

namespace IntegratorApilo.Server.Services.ApiloOrderService;

public class ApiloOrderService : IApiloOrderService
{
    private readonly RestClient _restClient;

    public ApiloOrderService(RestClient restClient)
    {
        _restClient = restClient;
        //_restClient.AddDefaultHeader("Accept", "application/json");
    }

    public async Task<ServiceResponse<ApiloOrders>> GetSimpleListOfOrders(ApiloConfig apiloConfig)
    {
        var result = new ServiceResponse<ApiloOrders>();
        try
        {
            string formattedDateTime = apiloConfig.LastUpdatedAt.AddSeconds(1).ToString("yyyy-MM-ddTHH:mm:ss");
            string offset = apiloConfig.LastUpdatedAt.ToString("zzz").Replace(":", "");
            formattedDateTime += offset;

            var request = new RestRequest($"{apiloConfig.ApiAddress}/rest/api/orders/", Method.Get);
            request.AddHeader("Authorization", $"Bearer {apiloConfig.AccessToken}");
            request.AddParameter("updatedAfter", formattedDateTime);
            request.AddParameter("sort", "updatedAtAsc");
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.Content == null) throw new Exception("Content is null");

            if (!response.IsSuccessStatusCode)
            {
                var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                throw new Exception(apiloErrorResponse.Message);
            }

            var apiloOrders = JsonSerializer.Deserialize<ApiloOrders>(response.Content);
            result.Data = apiloOrders;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }
}
