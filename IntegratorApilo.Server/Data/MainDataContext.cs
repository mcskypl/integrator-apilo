using Microsoft.EntityFrameworkCore;

namespace IntegratorApilo.Server.Data;

public class MainDataContext : DbContext
{
    public MainDataContext(DbContextOptions<MainDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiloSetting>(entity =>
        {
            entity.HasKey(e => e.IdSetting);
            entity.Property(e => e.IdSetting).ValueGeneratedOnAdd();
            entity.Property(e => e.IdSettingType).IsRequired();
            entity.Property(e => e.SettingName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<ApiloSetting>().HasData(
                     new ApiloSetting
                     {
                         IdSetting = 10001,
                         IdSettingType = 0,
                         SettingName = "Adres API"
                     },
                    new ApiloSetting
                    {
                        IdSetting = 10002,
                        IdSettingType = 0,
                        SettingName = "Client ID"
                    }
                    ,
                    new ApiloSetting
                    {
                        IdSetting = 10003,
                        IdSettingType = 0,
                        SettingName = "Client Secret"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10004,
                        IdSettingType = 0,
                        SettingName = "Auth Token"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10005,
                        IdSettingType = 0,
                        SettingName = "Access Token"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10006,
                        IdSettingType = 0,
                        SettingName = "Access Token Exp"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10007,
                        IdSettingType = 0,
                        SettingName = "Refresh Token"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10008,
                        IdSettingType = 0,
                        SettingName = "Refresh Token Exp"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10009,
                        IdSettingType = 0,
                        SettingName = "Synchronizacja zamówień"
                    },
                    new ApiloSetting
                    {
                        IdSetting = 10010,
                        IdSettingType = 0,
                        SettingName = "Last update"
                    }
                 );

        modelBuilder.Entity<ApiloShop>(entity =>
        {
            entity.HasKey(e => e.IdShop);
            entity.Property(e => e.IdShop).ValueGeneratedOnAdd();
            entity.Property(e => e.ShopName).IsRequired().HasMaxLength(60);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(155);
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();


            entity.HasMany(e => e.ApiloConnections)
                 .WithOne(e => e.ApiloShop)
                 .HasForeignKey(e => e.IdShop);

            entity.HasMany(e => e.ApiloShopSettings)
                 .WithOne(e => e.ApiloShop)
                 .HasForeignKey(e => e.IdShop);

            entity.HasMany(e => e.ApiloOrderStatuses)
                 .WithOne(e => e.ApiloShop)
                 .HasForeignKey(e => e.ShopId);
        });

        modelBuilder.Entity<ApiloShopSetting>(entity =>
        {
            entity.HasKey(e => e.IdShopSetting);
            entity.Property(e => e.IdShopSetting).ValueGeneratedOnAdd();
            entity.Property(e => e.IdSetting).IsRequired();
            entity.Property(e => e.IdShop).IsRequired();
            entity.Property(e => e.StringValue).HasMaxLength(1000);
            entity.Property(e => e.NumericValue).HasColumnType("decimal(18,4)");
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasOne(e => e.ApiloSetting)
                 .WithMany(e => e.ApiloShopSettings)
                 .HasForeignKey(e => e.IdSetting);

            entity.HasOne(e => e.ApiloShop)
                 .WithMany(e => e.ApiloShopSettings)
                 .HasForeignKey(e => e.IdShop);
        });

        modelBuilder.Entity<ApiloConnection>(entity =>
        {
            entity.HasKey(e => e.IdConnection);
            entity.Property(e => e.IdConnection).ValueGeneratedOnAdd();
            entity.Property(e => e.IdShop).IsRequired();
            entity.Property(e => e.ConnectionString).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.DatabaseName).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasMany(e => e.ApiloAccounts)
                 .WithOne(e => e.ApiloConnection)
                 .HasForeignKey(e => e.IdConnection);
        });

        modelBuilder.Entity<ApiloAccount>(entity =>
        {
            entity.HasKey(e => e.IdAccount);
            entity.Property(e => e.IdAccount).ValueGeneratedOnAdd();
            entity.Property(e => e.IdConnection).IsRequired();
            entity.Property(e => e.PlatformAccountId).IsRequired();
            entity.Property(e => e.Active).IsRequired();
            entity.Property(e => e.KodUrzzew).IsRequired().HasMaxLength(15);
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<ApiloOrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId);
            entity.Property(e => e.OrderStatusId).ValueGeneratedOnAdd();
            entity.Property(e => e.ShopId).IsRequired();
            entity.Property(e => e.Key).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
        });
    }

    public DbSet<ApiloSetting> ApiloSetting { get; set; }
    public DbSet<ApiloShop> ApiloShop { get; set; }
    public DbSet<ApiloConnection> ApiloConnection { get; set; }
    public DbSet<ApiloAccount> ApiloAccount { get; set; }
    public DbSet<ApiloShopSetting> ApiloShopSetting { get; set; }
    public DbSet<ApiloOrderStatus> ApiloOrderStatus { get; set; }
    
}
