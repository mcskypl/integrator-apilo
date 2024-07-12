using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_DATABASE")]
public class ApiloDatabase
{
    [Column("ID_DATABASE")] public int IdDatabase { get; set; }
    [Column("ID_CONFIG")] public int IdConfig { get; set; }
    [Column("CONNECTION_STRING")] public string? ConnectionString { get; set; }

    public ApiloConfig ApiloConfig { get; set; } = new();
}
