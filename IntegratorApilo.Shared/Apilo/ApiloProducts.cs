using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Apilo;

public class ApiloProducts
{
    public class Product
    {
        [JsonPropertyName("id")] public int? Id { get; set; }
        [JsonPropertyName("sku")] public string? Sku { get; set; }
        [JsonPropertyName("ean")] public string? Ean { get; set; }
        [JsonPropertyName("name")]public string? Name { get; set; }
        [JsonPropertyName("unit")] public string? Unit { get; set; }
        [JsonPropertyName("weight")] public string? Weight { get; set; }
        [JsonPropertyName("quantity")] public int? Quantity { get; set; }
        [JsonPropertyName("priceWithTax")] public string? PriceWithTax { get; set; }
        [JsonPropertyName("priceWithoutTax")] public string? PriceWithoutTax { get; set; }
        [JsonPropertyName("tax")] public string? Tax { get; set; }
        [JsonPropertyName("originalCode")] public string? OriginalCode { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
    }

    [JsonPropertyName("products")] public List<Product>? Products { get; set; }
    [JsonPropertyName("totalCount")] public int TotalCount { get; set; }
}
