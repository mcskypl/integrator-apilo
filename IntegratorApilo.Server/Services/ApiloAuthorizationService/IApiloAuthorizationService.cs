namespace IntegratorApilo.Server.Services.ApiloAuthorizationService;

public interface IApiloAuthorizationService
{
    Task<ServiceResponse<ApiloTokens>> GetTokens();
}
