using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("KARTOTEKA")]
public class Kartoteka
{
    [Column("ID_KARTOTEKA")] public int IdKartoteka { get; set; }
    [Column("INDEKS")] public string Indeks { get; set; } = string.Empty;
    [Column("NAZWASKR")] public string Nazwaskr { get; set; } = string.Empty;
    [Column("NAZWADL")] public string? Nazwadl { get; set; }

    public Stanmag? Stanmag { get; set; }
}
