using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_CONNECTION")]
public class ApiloConnection
{
    [Column("ID_CONNECTION")] public int IdConnection { get; set; }
    [Column("ID_SHOP")] public int IdShop { get; set; }
    [Column("CONNECTION_STRING")] public string ConnectionString { get; set; } = String.Empty;
    [Column("DATABASE_NAME")] public string DatabaseName { get; set; } = String.Empty;
    [Column("SYNC_INVOICES")] public int? SyncInvoices { get; set; }
    [Column("UPDATED_AT")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<ApiloAccount>? ApiloAccounts { get; set; }
    [JsonIgnore] public ApiloShop ApiloShop { get; set; } = new();
}
