using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Apilo;

public class ApiloErrorResponse
{
    [JsonPropertyName("message")] public string? Message { get; set; }
    [JsonPropertyName("code")] public int? Code { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}
