using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("URZZEWNAGL_ADD_9")]
public partial class UrzzewnaglAdd9
{
    [Column("AID_URZZEWNAGL")] public int AidUrzzewnagl { get; set; }
    [Column("BLAD")] public int Blad { get; set; }
}