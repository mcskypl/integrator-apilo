using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_SETTING")]
public class ApiloSetting
{
    [Column("ID_SETTING")] public int IdSetting { get; set; }
    [Column("ID_SETTING_TYPE")] public int IdSettingType { get; set; } = 1; // 0 - shop, 1 - connection, 2 -Account
    [Column("SETTING_NAME")] public string SettingName { get; set; } = String.Empty;
    [Column("UPDATED_AT")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonIgnore] public ICollection<ApiloShopSetting>? ApiloShopSettings { get; set; }
}
