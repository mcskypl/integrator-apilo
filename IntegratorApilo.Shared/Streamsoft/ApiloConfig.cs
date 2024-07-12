using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("XXX_APILO_CONFIG")]
public class ApiloConfig
{
    [Column("ID_CONFIG")] public int IdConfig { get; set; }
    [Column("APP_NAME")] public string AppName { get; set; } = String.Empty;
    [Column("APP_DESCRIPTION")] public string? AppDescription { get; set; }
    [Column("API_ADDRESS")] public string ApiAddress { get; set; } = String.Empty;
    [Column("CLIENT_ID")] public int ClientId { get; set; }
    [Column("CLIENT_SECRET")] public string ClientSecret { get; set; } = String.Empty;
    [Column("AUTH_TOKEN")] public string AuthToken { get; set; } = String.Empty;
    [Column("ACCESS_TOKEN")] public string? AccessToken { get; set; }
    [Column("ACCESS_TOKEN_EXP")] public DateTime? AccessTokenExp { get; set; }
    [Column("REFRESH_TOKEN")] public string? RefreshToken { get; set; }
    [Column("REFRESH_TOKEN_EXP")] public DateTime? RefreshTokenExp { get; set; }
    [Column("SYNC_ORDERS_MIN")] public int SyncOrdersMin { get; set; } = 0;
    [Column("SYNC_STOCKS_MIN")] public int SyncStocksMin { get; set; } = 0;
    [Column("LAST_UPDATED_AT")] public DateTime LastUpdatedAt { get; set; } = DateTime.MinValue;
    [Column("ID_MAGAZYN_STOCKS")] public int? IdMagazynStocks { get; set; }

    public ICollection<ApiloDatabase>? ApiloDatabases { get; set; }
}