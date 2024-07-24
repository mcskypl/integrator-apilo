using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_ACCOUNT")]
public class ApiloAccount
{
    [Column("ID_ACCOUNT")] public int IdAccount { get; set; }
    [Column("ID_CONNECTION")] public int IdConnection { get; set; }
    [Column("PLATFORM_ACCOUNT_ID")] public int PlatformAccountId { get; set; }
    [Column("ACTIVE")] public int Active { get; set; } = 0;
    [Column("ID_KONTRAH")] public int? IdKontrah { get; set; }
    [Column("KOD_URZZEW")] public string KodUrzzew { get; set; } = String.Empty;
    [Column("UPDATED_AT")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonIgnore] public ApiloConnection ApiloConnection { get; set; } = new();
}
