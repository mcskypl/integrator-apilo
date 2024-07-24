using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Apilo;

public class ApiloOrderAddress
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("phone")] public string? Phone { get; set; }
    [JsonPropertyName("email")] public string? Email { get; set; }
    [JsonPropertyName("streetName")] public string? StreetName { get; set; }
    [JsonPropertyName("streetNumber")] public string? StreetNumber { get; set; }
    [JsonPropertyName("city")] public string? City { get; set; }
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
    [JsonPropertyName("country")] public string? Country { get; set; }
    [JsonPropertyName("parcelIdExternal")] public string? ParcelIdExternal { get; set; }
    [JsonPropertyName("parcelName")] public string? ParcelName { get; set; }
    [JsonPropertyName("companyTaxNumber")] public string? CompanyTaxNumber { get; set; }
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }
}

public class ApiloOrderItem
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("idExternal")] public string? IdExternal { get; set; }
    [JsonPropertyName("ean")] public string? Ean { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }
    [JsonPropertyName("originalName")] public string OriginalName { get; set; } = string.Empty;
    [JsonPropertyName("originalCode")] public string? OriginalCode { get; set; }
    [JsonPropertyName("originalPriceWithTax")] public string OriginalPriceWithTax { get; set; } = string.Empty;
    [JsonPropertyName("originalPriceWithoutTax")] public string? OriginalPriceWithoutTax { get; set; }
    [JsonPropertyName("media")] public string? Media { get; set; }
    [JsonPropertyName("quantity")] public int Quantity { get; set; }
    [JsonPropertyName("tax")] public string Tax { get; set; } = string.Empty;
    [JsonPropertyName("status")] public int? Status { get; set; }
    [JsonPropertyName("unit")] public string? Unit { get; set; }
    [JsonPropertyName("type")] public int Type { get; set; }
    [JsonPropertyName("productId")] public int? ProductId { get; set; }
    [JsonPropertyName("productSet")] public int? ProductSet { get; set; }
}

public class ApiloOrderPayment
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("idExternal")] public string? IdExternal { get; set; }
    [JsonPropertyName("type")] public int Type { get; set; }
    [JsonPropertyName("paymentDate")] public string PaymentDate { get; set; } = string.Empty;
    [JsonPropertyName("amount")] public string Amount { get; set; } = string.Empty;
    [JsonPropertyName("currency")] public string? Currency { get; set; }
    [JsonPropertyName("comment")] public string? Comment { get; set; }
    [JsonPropertyName("orderId")] public string? OrderId { get; set; }
}

public class ApiloOrderPreferences
{
    [JsonPropertyName("idUser")] public string? IdUser { get; set; }
}

public class ApiloOrderDetails
{
    [JsonPropertyName("platformAccountId")] public int? PlatformAccountId { get; set; }
    [JsonPropertyName("idExternal")] public string? IdExternal { get; set; }
    [JsonPropertyName("isInvoice")] public bool? IsInvoice { get; set; }
    [JsonPropertyName("customerLogin")] public string? CustomerLogin { get; set; }
    [JsonPropertyName("paymentStatus")] public int PaymentStatus { get; set; }
    [JsonPropertyName("paymentType")] public int PaymentType { get; set; }
    [JsonPropertyName("originalCurrency")] public string? OriginalCurrency { get; set; }
    [JsonPropertyName("originalAmountTotalWithoutTax")] public string OriginalAmountTotalWithoutTax { get; set; } = string.Empty;
    [JsonPropertyName("originalAmountTotalWithTax")] public string OriginalAmountTotalWithTax { get; set; } = string.Empty;
    [JsonPropertyName("originalAmountTotalPaid")] public string OriginalAmountTotalPaid { get; set; } = string.Empty;
    [JsonPropertyName("sendDateMin")] public string? SendDateMin { get; set; }
    [JsonPropertyName("sendDateMax")] public string? SendDateMax { get; set; }
    [JsonPropertyName("isEncrypted")] public bool? IsEncrypted { get; set; }
    [JsonPropertyName("preferences")] public ApiloOrderPreferences? Preferences { get; set; }
    [JsonPropertyName("createdAt")] public string? CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")] public string? UpdatedAt { get; set; }
    [JsonPropertyName("orderItems")] public List<ApiloOrderItem> OrderItems { get; set; } = new();
    [JsonPropertyName("orderPayments")] public List<ApiloOrderPayment> OrderPayments { get; set; } = new();
    [JsonPropertyName("addressCustomer")] public ApiloOrderAddress AddressCustomer { get; set; } = new();
    [JsonPropertyName("addressDelivery")] public ApiloOrderAddress AddressDelivery { get; set; } = new();
    [JsonPropertyName("addressInvoice")] public ApiloOrderAddress? AddressInvoice { get; set; }
    [JsonPropertyName("carrierAccount")] public int CarrierAccount { get; set; }
    [JsonPropertyName("orderNotes")] public List<object>? OrderNotes { get; set; }
    [JsonPropertyName("orderedAt")] public string OrderedAt { get; set; } = string.Empty;
    [JsonPropertyName("platformId")] public int PlatformId { get; set; }
    [JsonPropertyName("isCanceledByBuyer")] public bool? IsCanceledByBuyer { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("status")] public int Status { get; set; }
}

