namespace IntegratorApilo.Server.Services.ApiloOrderService;

public interface IApiloOrderService
{
    Task<ServiceResponse<ApiloOrders>> GetSimpleListOfOrders(ApiloConfig apiloConfig);
}
