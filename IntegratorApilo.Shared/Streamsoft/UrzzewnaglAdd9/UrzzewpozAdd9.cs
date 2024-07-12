using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("URZZEWPOZ_ADD_9")]
public partial class UrzzewpozAdd9
{
    [Column("AID_URZZEWPOZ")] public int? AidUrzzewpoz { get; set; }
    [Column("BLAD")] public int? Blad { get; set; }
}