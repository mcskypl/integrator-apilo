using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_ORDER_STATUS")]
public class ApiloOrderStatus
{
    [Column("ORDER_STATUS_ID")][JsonPropertyName("id")] public int OrderStatusId { get; set; }
    [Column("SHOP_ID")][JsonIgnore] public int ShopId { get; set; }
    [Column("KEY")][JsonPropertyName("key")] public string? Key { get; set; }
    [Column("NAME")][JsonPropertyName("name")] public string? Name { get; set; }
    [Column("DESCRIPTION")][JsonPropertyName("description")] public string? Description { get; set; }

    [JsonIgnore] public ApiloShop ApiloShop { get; set; } = new();
}
