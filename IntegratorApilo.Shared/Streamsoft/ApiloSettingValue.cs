using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_SETTING_VALUE")]
public class ApiloSettingValue
{
    [Column("SETTING_VALUE_ID")] public int SettingValueId { get; set; }
    [Column("SETTING_ID")] public int SettingId { get; set; }
    [Column("SETTING_TYPE")] public int SettingType { get; set; } = 0;
    [Column("OBJECT_ID")] public int ObjectId { get; set; }
    [Column("INT_VALUE")] public int? IntValue { get; set; }
    [Column("NUMERIC_VALUE")] public float? NumericValue { get; set; }
    [Column("STRING_VALUE")] public string? StringValue { get; set; }
    [Column("DATETIME_VALUE")] public DateTime? DatetimeValue { get; set; }
    [Column("UPDATED_AT")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ApiloSetting ApiloSetting { get; set; } = new();
}
