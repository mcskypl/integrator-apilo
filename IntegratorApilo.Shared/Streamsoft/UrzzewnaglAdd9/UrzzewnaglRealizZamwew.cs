using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("URZZEWNAGL_REALIZ_ZAMWEW")]
public class UrzzewnaglRealizZamwew
{
    [Column("AID_NAGL")] public int? AidNagl { get; set; }
    [Column("BLAD")] public int? Blad { get; set; }
}