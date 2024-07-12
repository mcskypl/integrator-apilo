using FirebirdSql.Data.FirebirdClient;

namespace IntegratorApilo.Server.Data;

public class DataContext : DbContext
{
    public string ConnectionString { get; set; }

    public DataContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseFirebird(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kartoteka>(entity =>
        {
            entity.HasKey(e => e.IdKartoteka);
            entity.HasOne(e => e.Stanmag)
                  .WithOne(e => e.Kartoteka)
                  .HasForeignKey<Stanmag>(e => e.IdKartoteka);
        });

        modelBuilder.Entity<Stanmag>(entity =>
        {
            entity.HasKey(e => e.IdStanmag);
            entity.HasOne(e => e.Kartoteka)
                  .WithOne(e => e.Stanmag)
                  .HasForeignKey<Stanmag>(e => e.IdKartoteka);
        });

        modelBuilder.Entity<ApiloKontrah>().HasNoKey();
        modelBuilder.Entity<UrzzewnaglAdd9>().HasNoKey();
        modelBuilder.Entity<UrzzewpozAdd9>().HasNoKey();
        modelBuilder.Entity<UrzzewnaglRealizZamwew>().HasNoKey();
        modelBuilder.Entity<Urzzewnagl>().HasKey(e => e.IdUrzzewnagl);
    }

    // Tables
    public DbSet<Kartoteka> Kartoteka { get; set; }
    public DbSet<Stanmag> Stanmag { get; set; }

    // Procedures
    public DbSet<ApiloKontrah> ApiloKontrahs { get; set; }
    public DbSet<UrzzewnaglAdd9> UrzzewnaglAdd9s { get; set; }
    public DbSet<UrzzewpozAdd9> UrzzewpozAdd9s { get; set; }
    public DbSet<UrzzewnaglRealizZamwew> UrzzewnaglRealizZamwews { get; set; }

    // Procedures body
    public async Task<ApiloKontrah?> ApiloKontrah(string nazwaskr, string telefon, string email, string kodKraj, string companyname, string customername, string nip, string miejscowosc, string kodpocztowy, string ulica)
    {
        FbParameter[] @params =  {
                new FbParameter("@NAZWASKR", nazwaskr),
                new FbParameter("@TELEFON", telefon),
                new FbParameter("@EMAIL", email),
                new FbParameter("@KRAJ_KOD", kodKraj),
                new FbParameter("@COMPANYNAME", companyname),
                new FbParameter("@CUSTOMERNAME", customername),
                new FbParameter("@NIP", nip),
                new FbParameter("@MIEJSCOWOSC", miejscowosc),
                new FbParameter("@KODPOCZTOWY", kodpocztowy),
                new FbParameter("@ULICA", ulica),
            };

        ApiloKontrah? results = await ApiloKontrahs
            .FromSqlRaw("SELECT * FROM XXX_APILO_KONTRAH(@NAZWASKR, @TELEFON, @EMAIL, @KRAJ_KOD, @COMPANYNAME" +
                        ", @CUSTOMERNAME, @NIP,@MIEJSCOWOSC, @KODPOCZTOWY, @ULICA)", @params)
            .FirstOrDefaultAsync();

        return results;
    }

    public async Task<UrzzewnaglAdd9> UrzzewnaglAdd9(UrzzewnaglAdd9Request data)
    {
        FbParameter[] @params =  {
                new FbParameter("@AID_URZZEWSKAD", data.AidUrzzewskad),
                new FbParameter("@AKODURZ", data.Akodurz),
                new FbParameter("@ANRUZYT", data.Anruzyt),
                new FbParameter("@ANRAKW", data.Anrakw),
                new FbParameter("@AODB_IDKONTRAH", data.AodbIdkontrah),
                new FbParameter("@APONIPW", data.Aponipw),
                new FbParameter("@AJAKINUMERKONTRAH", data.Ajakinumerkontrah),
                new FbParameter("@AODB_DATA", data.AodbData),
                new FbParameter("@AODB_NAZWADOK", data.AodbNazwadok),
                new FbParameter("@AODB_NRDOK", data.AodbNrdok),
                new FbParameter("@AODB_TERMIN", data.AodbTermin),
                new FbParameter("@AODB_PLATNOSC", data.AodbPlatnosc),
                new FbParameter("@AODB_SUMA", data.AodbSuma),
                new FbParameter("@AODB_ILEPOZ", data.AodbIlepoz),
                new FbParameter("@AODB_GOTOWKA", data.AodbGotowka),
                new FbParameter("@AODB_DOT_DATA", data.AodbDotData),
                new FbParameter("@AODB_DOT_NAZWADOK", data.AodbDotNazwadok),
                new FbParameter("@AODB_DOT_NRDOK", data.AodbDotNrdok),
                new FbParameter("@AODB_UWAGI", data.AodbUwagi),
                new FbParameter("@AODB_DATAOBOW", data.AodbDataobow),
                new FbParameter("@AODB_SPOSDOSTAWY", data.AodbSposdostawy),
                new FbParameter("@AODB_CECHA1", data.AodbCecha1),
                new FbParameter("@AODB_CECHA2", data.AodbCecha2),
                new FbParameter("@AODB_CECHA3", data.AodbCecha3),
                new FbParameter("@AODB_CECHA4", data.AodbCecha4),
                new FbParameter("@AODB_CECHA5", data.AodbCecha5),
                new FbParameter("@AODB_IDNABYWCA", data.AodbIdnabuywca),
                new FbParameter("@AODB_IDKONTRAHDOST", data.AodbIdkontrahdost),
                new FbParameter("@AODB_ANULOWANY", data.AodbAnulowany),
                new FbParameter("@AODB_WYDRUKOWANY", data.AodbWydrukowany),
                new FbParameter("@AODB_FISKWYDRUKOWANY", data.AodbFiskwydrukowany),
                new FbParameter("@AODB_MAGAZYN", data.AodbMagazyn),
                new FbParameter("@ANAZWADOK_DOREALIZ", data.AnazwadokDorealiz),
                new FbParameter("@AID_WALUTA", data.AidWaluta),
                new FbParameter("@AKURS", data.Akurs),
                new FbParameter("@AID_ZLECENIEPARTIAZP", data.AidZleceniepartiazp),
                new FbParameter("@AOZNJPK", data.Aoznjpk),
                new FbParameter("@AODB_KODKRAJVAT", data.AodbKodkrajvat),
                new FbParameter("@AODB_KODKRAJNAD", data.AodbKodkrajnad),
                new FbParameter("@AODB_KODKRAJPRZEZ", data.AodbKodkrajprzez)
            };

        UrzzewnaglAdd9? results = await UrzzewnaglAdd9s
            .FromSqlRaw("select * from URZZEWNAGL_ADD_9(@AID_URZZEWSKAD, @AKODURZ, @ANRUZYT, @ANRAKW, @AODB_IDKONTRAH, @APONIPW" +
                        ", @AJAKINUMERKONTRAH, @AODB_DATA, @AODB_NAZWADOK, @AODB_NRDOK, @AODB_TERMIN, @AODB_PLATNOSC, @AODB_SUMA" +
                        ", @AODB_ILEPOZ, @AODB_GOTOWKA, @AODB_DOT_DATA, @AODB_DOT_NAZWADOK, @AODB_DOT_NRDOK, @AODB_UWAGI" +
                        ", @AODB_DATAOBOW, @AODB_SPOSDOSTAWY, @AODB_CECHA1, @AODB_CECHA2, @AODB_CECHA3, @AODB_CECHA4" +
                        ", @AODB_CECHA5, @AODB_IDNABYWCA, @AODB_IDKONTRAHDOST, @AODB_ANULOWANY, @AODB_WYDRUKOWANY" +
                        ", @AODB_FISKWYDRUKOWANY, @AODB_MAGAZYN, @ANAZWADOK_DOREALIZ, @AID_WALUTA, @AKURS, @AID_ZLECENIEPARTIAZP" +
                        ", @AOZNJPK, @AODB_KODKRAJVAT, @AODB_KODKRAJNAD, @AODB_KODKRAJPRZEZ)", @params)
            .FirstOrDefaultAsync();

        return results;
    }

    public async Task<UrzzewpozAdd9?> UrzzewpozAdd9(UrzzewpozAdd9Request data)
    {
        FbParameter[] @params =  {
                new FbParameter("@AID_URZZEWNAGL", data.AidUrzzewnagl),
                new FbParameter("@AKODTOW", data.Akodtow),
                new FbParameter("@AILOSC", data.Ailosc),
                new FbParameter("@ACENA", data.Acena),
                new FbParameter("@ACENAUZG", data.Acenauzg),
                new FbParameter("@APROCBONIF", data.Aprocbonif),
                new FbParameter("@AODB_UWAGI", data.AodbUwagi),
                new FbParameter("@AODB_CECHA1", data.AodbCecha1),
                new FbParameter("@AODB_CECHA2", data.AodbCecha2),
                new FbParameter("@AODB_CECHA3", data.AodbCecha3),
                new FbParameter("@AODB_CENA_BRUTTO", data.AodbCenaBrutto),
                new FbParameter("@AODB_DOT_DATA", data.AodbDotData),
                new FbParameter("@AODB_DOT_NAZWADOK", data.AodbDotNazwadok),
                new FbParameter("@AODB_DOT_NRDOK", data.AodbDotNrdok),
                new FbParameter("@AODB_DOT_LP", data.AodbDotLp),
                new FbParameter("@AODB_MAG_OZNNRWYDR", data.AodbMagOznnrwudr),
                new FbParameter("@AODB_DATA_WAZNOSCI", data.AodbDataWaznosci),
                new FbParameter("@AODB_RODZAJ_REZERWACJI", data.AodbRodzajRezerwacji),
                new FbParameter("@AODB_ILOSC_REZERWACJI", data.AodbIloscRezerwacji),
                new FbParameter("@AODB_ID_DOSTAWA_REZ", data.AodbIdDostawaRez),
                new FbParameter("@AODB_PROCCLA", data.AodbProccla),
                new FbParameter("@ACENA_PRZEPISZ", data.AcenaPrzepisz),
                new FbParameter("@AODB_DOT_LPDOD", data.AodbDotLpdod),
                new FbParameter("@AODB_NRDOSTAWY", data.AodbNrdostawy)
            };

        UrzzewpozAdd9? results = await UrzzewpozAdd9s
            .FromSqlRaw("SELECT * FROM URZZEWPOZ_ADD_9(@AID_URZZEWNAGL, @AKODTOW, @AILOSC, @ACENA, @ACENAUZG, @APROCBONIF, @AODB_UWAGI" +
                        ", @AODB_CECHA1, @AODB_CECHA2, @AODB_CECHA3, @AODB_CENA_BRUTTO, @AODB_DOT_DATA, @AODB_DOT_NAZWADOK" +
                        ", @AODB_DOT_NRDOK, @AODB_DOT_LP, @AODB_MAG_OZNNRWYDR, @AODB_DATA_WAZNOSCI, @AODB_RODZAJ_REZERWACJI, @AODB_ILOSC_REZERWACJI" +
                        ", @AODB_ID_DOSTAWA_REZ, @AODB_PROCCLA, @ACENA_PRZEPISZ, @AODB_DOT_LPDOD, @AODB_NRDOSTAWY)", @params)
            .FirstOrDefaultAsync();

        return results;
    }

    public async Task<UrzzewnaglRealizZamwew?> UrzzewnaglRealizZamwew(UrzzewnaglRealizZamwewRequest data)
    {
        FbParameter[] @params =  {
                new FbParameter("@AID_URZZEWNAGL", data.AidUrzzewnagl),
                new FbParameter("@KASUJURZZEWNAGL", data.Kasujurzzewnagl),
            };

        UrzzewnaglRealizZamwew? results = await UrzzewnaglRealizZamwews
            .FromSqlRaw("SELECT * FROM URZZEWNAGL_REALIZ_ZAMWEW(@AID_URZZEWNAGL, @KASUJURZZEWNAGL)", @params)
            .FirstOrDefaultAsync();

        return results;
    }
}
