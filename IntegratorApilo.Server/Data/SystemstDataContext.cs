namespace IntegratorApilo.Server.Data;

public class SystemstDataContext : DbContext
{
    public SystemstDataContext(DbContextOptions<SystemstDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiloConfig>(entity =>
        {
            entity.HasKey(e => e.IdConfig);
            entity.Property(e => e.IdConfig).ValueGeneratedOnAdd();
            entity.Property(e => e.AppName).IsRequired().HasMaxLength(60);
            entity.Property(e => e.AppDescription).HasMaxLength(250);
            entity.Property(e => e.ApiAddress).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ClientSecret).IsRequired().HasMaxLength(200);
            entity.Property(e => e.AuthToken).IsRequired().HasMaxLength(200);
            entity.Property(e => e.AccessToken).HasMaxLength(1000);
            entity.Property(e => e.RefreshToken).HasMaxLength(1000);
            entity.Property(e => e.SyncOrdersMin).IsRequired();
            entity.Property(e => e.SyncStocksMin).IsRequired();
            entity.Property(e => e.LastUpdatedAt).IsRequired();

            entity.HasMany(e => e.ApiloDatabases)
                 .WithOne(e => e.ApiloConfig)
                 .HasForeignKey(e => e.IdConfig);
        });

        modelBuilder.Entity<ApiloDatabase>(entity =>
        {
            entity.HasKey(e => e.IdDatabase);
            entity.Property(e => e.IdDatabase).ValueGeneratedOnAdd();
            entity.Property(e => e.IdConfig).IsRequired();
            entity.Property(e => e.DatabaseName).HasMaxLength(50);
            entity.Property(e => e.ConnectionString).IsRequired().HasMaxLength(1000);
        });
    }

    public DbSet<ApiloConfig> ApiloConfig { get; set; }
    public DbSet<ApiloDatabase> ApiloDatabase { get; set; }
}