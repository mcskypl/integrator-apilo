using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Apilo;

public class Document
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("documentNumber")] public string? DocumentNumber { get; set; }
    [JsonPropertyName("originalAmountTotalWithTax")] public string? OriginalAmountTotalWithTax { get; set; }
    [JsonPropertyName("originalAmountTotalWithoutTax")] public string? OriginalAmountTotalWithoutTax { get; set; }
    [JsonPropertyName("originalCurrencyExchangeValue")] public string? OriginalCurrencyExchangeValue { get; set; }
    [JsonPropertyName("originalCurrency")] public string? OriginalCurrency { get; set; }
    [JsonPropertyName("currency")] public string? Currency { get; set; }
    [JsonPropertyName("createdAt")] public string? CreatedAt { get; set; }
    [JsonPropertyName("invoicedAt")] public string? InvoicedAt { get; set; }
    [JsonPropertyName("soldAt")] public string? SoldAt { get; set; }
    [JsonPropertyName("type")] public int Type { get; set; }
    [JsonPropertyName("documentReceiver")] public DocumentReceiver? DocumentReceiver { get; set; }
    [JsonPropertyName("documentIssuer")] public DocumentIssuer? DocumentIssuer { get; set; }
    [JsonPropertyName("documentItems")] public List<DocumentItem>? DocumentItems { get; set; }
    [JsonPropertyName("paymentType")] public int PaymentType { get; set; }
}

public class DocumentIssuer
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }
    [JsonPropertyName("companyTaxNumber")] public string? CompanyTaxNumber { get; set; }
    [JsonPropertyName("streetName")] public string? StreetName { get; set; }
    [JsonPropertyName("streetNumber")] public string? StreetNumber { get; set; }
    [JsonPropertyName("city")] public string? City { get; set; }
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
    [JsonPropertyName("country")] public string? Country { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
}

public class DocumentItem
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("originalPriceWithTax")] public string? OriginalPriceWithTax { get; set; }
    [JsonPropertyName("originalPriceWithoutTax")] public string? OriginalPriceWithoutTax { get; set; }
    [JsonPropertyName("tax")] public string? Tax { get; set; }
    [JsonPropertyName("quantity")] public int? Quantity { get; set; }
    [JsonPropertyName("originalAmountTotalWithTax")] public string? OriginalAmountTotalWithTax { get; set; }
    [JsonPropertyName("originalAmountTotalWithoutTax")] public string? OriginalAmountTotalWithoutTax { get; set; }
    [JsonPropertyName("originalAmountTotalTax")] public string? OriginalAmountTotalTax { get; set; }
    [JsonPropertyName("gtu")] public string? Gtu { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }
    [JsonPropertyName("ean")] public string? Ean { get; set; }
    [JsonPropertyName("unit")] public string? Unit { get; set; }
    [JsonPropertyName("type")] public int? Type { get; set; }
}

public class DocumentReceiver
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("companyName")] public string? CompanyName { get; set; }
    [JsonPropertyName("companyTaxNumber")] public string? CompanyTaxNumber { get; set; }
    [JsonPropertyName("streetName")] public string? StreetName { get; set; }
    [JsonPropertyName("streetNumber")] public string? StreetNumber { get; set; }
    [JsonPropertyName("city")] public string? City { get; set; }
    [JsonPropertyName("zipCode")] public string? ZipCode { get; set; }
    [JsonPropertyName("country")] public string? Country { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
}

public class ApiloDocuments
{
    [JsonPropertyName("documents")] public List<Document>? Documents { get; set; }
    [JsonPropertyName("totalCount")] public int TotalCount { get; set; }
    [JsonPropertyName("currentOffset")] public int CurrentOffset { get; set; }
    [JsonPropertyName("pageResultCount")] public int PageResultCount { get; set; }
}
