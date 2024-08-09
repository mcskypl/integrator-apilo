using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("URZZEWNAGL")]
public class Urzzewnagl
{
    [Column("ID_URZZEWNAGL")] public int IdUrzzewnagl { get; set; }
    [Column("ID_NAGL")] public int? IdNagl { get; set; }
    [Column("ODB_DATA")] public DateTime OdbData { get; set; }
    [Column("ODB_NRDOK")] public string OdbNrdok { get; set; } = "";
    [Column("STATUS")] public int Status { get; set; }
}
