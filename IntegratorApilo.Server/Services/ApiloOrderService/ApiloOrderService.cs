using IntegratorApilo.Shared.Apilo;
using System.Text.Json;

namespace IntegratorApilo.Server.Services.ApiloOrderService;

public class ApiloOrderService : IApiloOrderService
{
    private readonly RestClient _restClient;
    private readonly SystemstDataContext _systemstDataContext;
    private readonly MainDataContext _mainDataContext;

    public ApiloOrderService(RestClient restClient, SystemstDataContext systemstDataContext, MainDataContext mainDataContext)
    {
        _restClient = restClient;
        _systemstDataContext = systemstDataContext;
        _mainDataContext = mainDataContext;
    }

    public async Task<ServiceResponse<ApiloOrderDetails>> GetOrderDetails(int idShop, string idApiloOrder)
    {
        var result = new ServiceResponse<ApiloOrderDetails>();

        try
        {
            var shopSettings = await _mainDataContext.ApiloShopSetting.Where(s => s.IdShop == idShop).ToListAsync() ?? throw new Exception("ShopSettings is null");

            string apiAddress = shopSettings.FirstOrDefault(s => s.IdSetting == 10001).StringValue;
            string accessToken = shopSettings.FirstOrDefault(s => s.IdSetting == 10005).StringValue;

            //var apiloConfig = await _systemstDataContext.ApiloConfig.FirstOrDefaultAsync(x => x.IdConfig == idConfig) ?? throw new Exception("ApiloConfig is null");

            var request = new RestRequest($"{apiAddress}/rest/api/orders/{idApiloOrder}/", Method.Get);
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                throw new Exception(apiloErrorResponse.Message);
            }

            ApiloOrderDetails apiloOrderDetails = JsonSerializer.Deserialize<ApiloOrderDetails>(response.Content);
            result.Data = apiloOrderDetails;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<List<ApiloOrderStatus>>> GetOrderStatus(int idShop)
    {
        var result = new ServiceResponse<List<ApiloOrderStatus>>();

        try
        {
            var shopSettings = await _mainDataContext.ApiloShopSetting.Where(s => s.IdShop == idShop).ToListAsync() ?? throw new Exception("ShopSettings is null");

            string apiAddress = shopSettings.FirstOrDefault(s => s.IdSetting == 10001).StringValue;
            string accessToken = shopSettings.FirstOrDefault(s => s.IdSetting == 10005).StringValue;

            //var apiloConfig = await _systemstDataContext.ApiloConfig.FirstOrDefaultAsync(x => x.IdConfig == idConfig) ?? throw new Exception("ApiloConfig is null");

            var request = new RestRequest($"{apiAddress}/rest/api/orders/status/map/", Method.Get);
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                throw new Exception(apiloErrorResponse.Message);
            }

            List<ApiloOrderStatus> apiloOrderStatusList = JsonSerializer.Deserialize<List<ApiloOrderStatus>>(response.Content);

            foreach (var apiloOrderStatus in apiloOrderStatusList)
            {
                var apiloOrderStatusTmp = await _mainDataContext.ApiloOrderStatus.FirstOrDefaultAsync(x => x.OrderStatusId == apiloOrderStatus.OrderStatusId);

                // add new status
                if (apiloOrderStatusTmp == null)
                {
                    ApiloOrderStatus newApiloOrderStatus = new()
                    {
                        OrderStatusId = apiloOrderStatus.OrderStatusId,
                        ShopId = idShop,
                        Key = apiloOrderStatus.Key,
                        Name = apiloOrderStatus.Name,
                        Description = apiloOrderStatus.Description
                    };

                    await _mainDataContext.ApiloOrderStatus.AddAsync(newApiloOrderStatus);
                    await _mainDataContext.SaveChangesAsync();
                }
                //edit status
                else
                {

                }
            }

           

            result.Data = apiloOrderStatusList;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<ServiceResponse<ApiloOrders>> GetSimpleListOfOrders(int idShop)
    {
        var result = new ServiceResponse<ApiloOrders>();
        try
        {
            var shopSettings = await _mainDataContext.ApiloShopSetting.Where(s => s.IdShop == idShop).ToListAsync() ?? throw new Exception("ShopSettings is null");

            DateTime lastUpdate = (DateTime)shopSettings.FirstOrDefault(s => s.IdSetting == 10010).DatetimeValue;
            string apiAddress = shopSettings.FirstOrDefault(s => s.IdSetting == 10001).StringValue;
            string accessToken = shopSettings.FirstOrDefault(s => s.IdSetting == 10005).StringValue;

            string formattedDateTime = lastUpdate.AddSeconds(1).ToString("yyyy-MM-ddTHH:mm:ss");
            string offset = lastUpdate.ToString("zzz").Replace(":", "");
            formattedDateTime += offset;

            var request = new RestRequest($"{apiAddress}/rest/api/orders/", Method.Get);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
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
