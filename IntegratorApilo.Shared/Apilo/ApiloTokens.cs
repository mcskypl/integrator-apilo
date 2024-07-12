using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Apilo;

public class ApiloTokens
{
    [JsonPropertyName("accessToken")] public string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("accessTokenExpireAt")] public string AccessTokenExpireAt { get; set; } = string.Empty;
    [JsonPropertyName("refreshToken")] public string RefreshToken { get; set; } = string.Empty;
    [JsonPropertyName("refreshTokenExpireAt")] public string RefreshTokenExpireAt { get; set; } = string.Empty;
}