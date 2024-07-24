using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("NAGLZAMODB")]
public class Naglzamodb
{
    [Column("ID_NAGLZAMODB")] public int IdNaglzamodb { get; set; }
    [Column("ID_NAGL")] public int IdNagl { get; set; }
    [Column("ID_DEFDOKWYST")] public int IdDefdokwyst { get; set; }
}
