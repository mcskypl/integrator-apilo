using IntegratorApilo.Shared.Apilo;
using System.Text;
using System.Text.Json;

namespace IntegratorApilo.Server.Services.ApiloAuthorizationService;

public class ApiloAuthorizationService : IApiloAuthorizationService
{
    private readonly HttpClient _http;
    //private readonly DataContext _context;

    public ApiloAuthorizationService(HttpClient http /*DataContext context*/)
    {
        _http = http;
        //_context = context;
    }

    public async Task<ServiceResponse<ApiloTokens>> GetTokens()
    {
        var result = new ServiceResponse<ApiloTokens>();

        //try
        //{
        //    var apiloConfig = await _context.ApiloConfig.FirstOrDefaultAsync();

        //    if (apiloConfig == null) throw new Exception("Tabela XXX_APILO_CONFIG zwróciła null");

        //    var grantType = apiloConfig.AccessToken == null ? "authorization_code" : "refresh_token";
        //    var token = apiloConfig.AccessToken == null ? apiloConfig.AuthToken : apiloConfig.RefreshToken;

        //    string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(apiloConfig.ClientId + ":" + apiloConfig.ClientSecret));

        //    var data = new
        //    {
        //        grantType,
        //        token
        //    };

        //    var json = JsonSerializer.Serialize(data);

        //    var request = new HttpRequestMessage(HttpMethod.Post, $"{apiloConfig.ApiAddress}/rest/auth/token/");
        //    request.Headers.Add("Accept", "application/json");
        //    request.Headers.Add("Authorization", $"Basic {encoded}");
        //    var content = new StringContent(json, Encoding.UTF8);
        //    request.Content = content;
        //    var response = await _http.SendAsync(request);
        //    response.EnsureSuccessStatusCode();
        //    var responseContent = await response.Content.ReadAsStringAsync();

        //    var apiloTokens = JsonSerializer.Deserialize<ApiloTokens>(responseContent);
        //    result.Data = apiloTokens;

        //    apiloConfig.AccessToken = apiloTokens.AccessToken;
        //    apiloConfig.AccessTokenExp = DateTime.Parse(apiloTokens.AccessTokenExpireAt);
        //    apiloConfig.RefreshToken = apiloTokens.RefreshToken;
        //    apiloConfig.RefreshTokenExp = DateTime.Parse(apiloTokens.AccessTokenExpireAt);

        //    _context.ApiloConfig.Update(apiloConfig);
        //    await _context.SaveChangesAsync();
        //}
        //catch (Exception ex) 
        //{
        //    result.Success = false;
        //    result.Message = ex.Message;
        //    //result.Errors = new ServiceResponseError()
        //    //{
        //    //    ErrorException = ex.Message.ToString()
        //    //};
        //}
        
        return result;
    }
}
