namespace IntegratorApilo.Server.Services.ApiloConnectionService;

public class ApiloConnectionService : IApiloConnectionService
{
    private readonly HttpClient _http;

    public ApiloConnectionService(HttpClient http)
	{
        _http = http;
    }

    public async Task Get()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://mdksoft.apilo.com/rest/auth/token/");
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("Authorization", "Basic MzpkNDA4ZWI1Ny03ZjA2LTU0MTYtYjFiMC03NmU3NThhNTZiMzc=");
        var content = new StringContent("{\n  \"grantType\": \"authorization_code\",\n  \"token\": \"8b701447-cdc1-5277-9b12-15416cd16afc\"\n}", null, "application/json");
        request.Content = content;
        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
