
using System.Text.Json;

namespace IntegratorApilo.Server.Services.ApiloFinanceDocumentService;

public class ApiloFinanceDocumentService : IApiloFinanceDocumentService
{
    private readonly RestClient _restClient;
    private readonly MainDataContext _mainDataContext;

    public ApiloFinanceDocumentService(RestClient restClient, MainDataContext mainDataContext)
    {
        _restClient = restClient;
        _mainDataContext = mainDataContext;
    }

    public async Task<ServiceResponse<ApiloDocuments>> GetListOfAccountingDocuments(int idShop, int offset)
    {
        var result = new ServiceResponse<ApiloDocuments>();

        try
        {
            var shopSettings = await _mainDataContext.ApiloShopSetting.Where(s => s.IdShop == idShop).ToListAsync() ?? throw new Exception("ShopSettings is null");

            string apiAddress = shopSettings.FirstOrDefault(s => s.IdSetting == 10001).StringValue;
            string accessToken = shopSettings.FirstOrDefault(s => s.IdSetting == 10005).StringValue;

            var request = new RestRequest($"{apiAddress}/rest/api/finance/documents/?type=1", Method.Get);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddParameter("limit", 512);
            request.AddParameter("offset", offset);

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                throw new Exception(apiloErrorResponse.Message);
            }

            ApiloDocuments apiloOrderDetails = JsonSerializer.Deserialize<ApiloDocuments>(response.Content);
            result.Data = apiloOrderDetails;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }
}
