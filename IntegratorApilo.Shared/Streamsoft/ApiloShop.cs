using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_SHOP")]
public class ApiloShop
{
    [Column("ID_SHOP")] public int IdShop { get; set; }
    [Column("SHOP_NAME")] public string ShopName { get; set; } = String.Empty;
    [Column("DESCRIPTION")] public string Description { get; set; } = String.Empty;
    [Column("UPDATED_AT")] public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [Column("CREATED_AT")] public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<ApiloConnection>? ApiloConnections { get; set; }
    public ICollection<ApiloShopSetting>? ApiloShopSettings { get; set; }
    public ICollection<ApiloOrderStatus>? ApiloOrderStatuses { get; set; }
}
