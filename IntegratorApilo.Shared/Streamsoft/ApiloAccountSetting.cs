using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_ACCOUNT_SETTING")]
public class ApiloAccountSetting
{
    [Column("ID_ACCOUNT_SETTING")] public int IdAccountSetting { get; set; }
    [Column("ID_ACCOUNT")] public int IdAccount { get; set; }
    [Column("VALUE")] public string Value { get; set; } = String.Empty;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;
}
