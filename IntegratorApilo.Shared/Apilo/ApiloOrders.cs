using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Apilo;

public class ApiloOrders
{
    public class AddressCustomer
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("phone")] public string? Phone { get; set; }
        [JsonPropertyName("email")] public string? Email { get; set; }
        [JsonPropertyName("id")] public int? Id { get; set; }
        [JsonPropertyName("streetName")] public string? StreetName { get; set; }
        [JsonPropertyName("streetNumber")] public string? StreetNumber { get; set; }
        [JsonPropertyName("city")] public string? City { get; set; }
        [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
        [JsonPropertyName("country")] public string? Country { get; set; }
        [JsonPropertyName("department")] public string? Department { get; set; }
        [JsonPropertyName("class")] public string? Class { get; set; }
    }

    public class Order
    {
        [JsonPropertyName("idExternal")] public string? IdExternal { get; set; }
        [JsonPropertyName("isInvoice")] public bool? IsInvoice { get; set; }
        [JsonPropertyName("originalCurrency")] public string? OriginalCurrency { get; set; }
        [JsonPropertyName("isEncrypted")] public bool IsEncrypted { get; set; }
        [JsonPropertyName("createdAt")] public string? CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")] public string? UpdatedAt { get; set; }
        [JsonPropertyName("orderItems")] public List<OrderItem>? OrderItems { get; set; }
        [JsonPropertyName("addressCustomer")] public AddressCustomer? AddressCustomer { get; set; }
        [JsonPropertyName("isCanceledByBuyer")] public bool? IsCanceledByBuyer { get; set; }
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
    }

    public class OrderItem
    {
        [JsonPropertyName("id")] public int? Id { get; set; }
        [JsonPropertyName("idExternal")] public string? IdExternal { get; set; }
        [JsonPropertyName("ean")] public string? Ean { get; set; }
        [JsonPropertyName("sku")] public string? Sku { get; set; }
        [JsonPropertyName("originalName")] public string? OriginalName { get; set; }
        [JsonPropertyName("originalCode")] public string? OriginalCode { get; set; }
        [JsonPropertyName("originalPriceWithTax")] public string? OriginalPriceWithTax { get; set; }
        [JsonPropertyName("originalPriceWithoutTax")] public string? OriginalPriceWithoutTax { get; set; }
        [JsonPropertyName("media")] public string? Media { get; set; }
        [JsonPropertyName("quantity")] public int? Quantity { get; set; }
        [JsonPropertyName("tax")] public string? Tax { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("unit")] public string? Unit { get; set; }
        [JsonPropertyName("type")]  public int? Type { get; set; }
    }

    [JsonPropertyName("orders")] public List<Order>? Orders { get; set; }
    [JsonPropertyName("totalCount")] public int TotalCount { get; set; }
    [JsonPropertyName("currentOffset")] public int CurrentOffset { get; set; }
    [JsonPropertyName("pageResultCount")] public int PageResultCount { get; set; }
}
