using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_ESHOP_BL_FV")]
public class ApiloInvoice
{
    [Column("INVOICE_ID")] public int? InvoiceId { get; set; }
    [Column("ORDER_ID")] public int? OrderId { get; set; }
    [Column("NUMBER")] public string? Number { get; set; }
    [Column("CURRENCY")] public string? Currency { get; set; }
    [Column("TOTAL_PRICE_BRUTTO")] public float? TotalPriceBrutto { get; set; }
    [Column("INVOICE_FULLNAME")] public string? InvoiceFullname { get; set; }
    [Column("INVOICE_COMPANY")] public string? InvoiceCompany { get; set; }
    [Column("INVOICE_NIP")] public string? InvoiceNip { get; set; }
    [Column("INVOICE_ADDRESS")] public string? InvoiceAddress { get; set; }
    [Column("INVOICE_CITY")] public string? InvoiceCity { get; set; }
    [Column("INVOICE_POSTCODE")] public string? InvoicePostcode { get; set; }
    [Column("INVOICE_COUNTRY")] public string? InvoiceCountry { get; set; }
    [Column("INVOICE_COUNTRY_CODE")] public string? InvoiceCountryCode { get; set; }
    [Column("PAYMENT")] public string? Payment { get; set; }
    [Column("CORRECTING_TO_INVOICE_ID")] public int? CorrectingToInvoiceId { get; set; }
    [Column("CZY_REALIZ")] public int? CzyRealiz { get; set; }
    [Column("DATA_DODANIA")] public DateTime? DataDodania { get; set; }
}
