using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("STANMAG")]
public class Stanmag
{
    [Column("ID_STANMAG")] public int IdStanmag { get; set; }
    [Column("ID_KARTOTEKA")] public int IdKartoteka { get; set; }
    [Column("ID_MAGAZYN")] public int IdMagazyn { get; set; }
    [Column("STANDYSP")] public float Standysp { get; set; }

    public Kartoteka? Kartoteka { get; set; }
}
