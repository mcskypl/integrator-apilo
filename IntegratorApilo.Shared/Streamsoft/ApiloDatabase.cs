using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_DATABASE")]
public class ApiloDatabase
{
    [Column("ID_DATABASE")] public int IdDatabase { get; set; }
    [Column("ID_CONFIG")] public int IdConfig { get; set; }
    [Column("DATABASE_NAME")] public string? DatabaseName { get; set; }
    [Column("CONNECTION_STRING")] public string? ConnectionString { get; set; }
    [Column("SYNC_STOCKS")] public int? SyncStocks { get; set; } = 0;
    [Column("ID_KONTRAH")] public int? IdKontrah { get; set; } = null;
    [Column("KOD_URZZEW")] public string? KodUrzzew { get; set; }

    [JsonIgnore] public ApiloConfig ApiloConfig { get; set; } = new();
}
