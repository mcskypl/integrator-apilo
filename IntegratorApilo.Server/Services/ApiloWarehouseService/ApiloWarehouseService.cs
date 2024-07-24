using IntegratorApilo.Shared.Apilo;
using System.Text.Json;
using static IntegratorApilo.Shared.Apilo.ApiloProducts;

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

    public async Task<ServiceResponse<List<ApiloProduct>>> GetProductsList(int idConfig)
    {
        var apiloConfig = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).FirstOrDefaultAsync(x => x.IdConfig == idConfig)
            ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");

        var result = new ServiceResponse<List<ApiloProduct>>()
        {
            Data = new List<ApiloProduct>()
        };

        try
        {
            int offset = 0;

            while (true)
            {
                var request = new RestRequest($"{apiloConfig.ApiAddress}/rest/api/warehouse/product/", Method.Get);
                request.AddHeader("Authorization", $"Bearer {apiloConfig.AccessToken}");
                request.AddParameter("limit", 2000);
                request.AddParameter("offset", offset);
                RestResponse response = await _restClient.ExecuteAsync(request);

                if (response.Content == null) throw new Exception("Content is null");

                if (!response.IsSuccessStatusCode)
                {
                    var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                    throw new Exception(apiloErrorResponse.Message);
                }

                var apiloProducts = JsonSerializer.Deserialize<ApiloProducts>(response.Content);
                result.Data.AddRange(apiloProducts.Products);

                if (apiloProducts.Products.Count == 0) break;

                offset += apiloProducts.Products.Count;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public class Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public async Task<ServiceResponse<int>> UpdateProducts(int idConfig, List<ApiloProduct> products)
    {
        var apiloConfig = await _systemstContext.ApiloConfig.Include(e => e.ApiloDatabases).FirstOrDefaultAsync(x => x.IdConfig == idConfig)
            ?? throw new Exception("SYSTEMST.XXX_APILO_CONFIG is null");

        var result = new ServiceResponse<int>();

        try
        {
            var productsToUpdate = new List<object>();

            foreach (var product in products)
            {
                var productUpdate = new
                {
                    product.Id,
                    product.Quantity
                };

                productsToUpdate.Add(productUpdate);

                if (productsToUpdate.Count() >= 512) break;
            }


            var request = new RestRequest($"{apiloConfig.ApiAddress}/rest/api/warehouse/product/", Method.Patch);
            request.AddHeader("Authorization", $"Bearer {apiloConfig.AccessToken}");
            request.AddJsonBody(productsToUpdate);

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotModified) result.Data = 0;
            else if (response.StatusCode == System.Net.HttpStatusCode.OK) result.Data = productsToUpdate.Count();
            else
            {
                var apiloErrorResponse = JsonSerializer.Deserialize<ApiloErrorResponse>(response.Content) ?? throw new Exception("ApiloErrorResponse is null");
                result.Success = false;
                result.Message = $"{apiloErrorResponse.Message} | {apiloErrorResponse.Description} | {productsToUpdate.ToString()}";
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }
}
