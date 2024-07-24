using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_SHOP_SETTING")]
public class ApiloShopSetting
{
    [Column("ID_SHOP_SETTING")] public int IdShopSetting { get; set; }
    [Column("ID_SETTING")] public int IdSetting { get; set; }
    [Column("ID_SHOP")] public int IdShop { get; set; }
    [Column("INT_VALUE")] public int? IntValue { get; set; }
    [Column("STRING_VALUE")] public string? StringValue { get; set; }
    [Column("NUMERIC_VALUE")] public float? NumericValue { get; set; }
    [Column("DATETIME_VALUE")] public DateTime? DatetimeValue { get; set; }
    [Column("UPDATED_AT")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ApiloSetting ApiloSetting { get; set; } = new();
    [JsonIgnore] public ApiloShop ApiloShop { get; set; } = new();
}
