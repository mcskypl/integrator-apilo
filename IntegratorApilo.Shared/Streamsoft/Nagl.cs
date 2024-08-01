using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

[Table("NAGL")]
public class Nagl
{
    [Column("ID_NAGL")] public int IdNagl { get; set; }
    [Column("NAPODSTAWIE")] public string? Napodstawie { get; set; }
    [Column("UWAGI")] public string? Uwagi { get; set; }
}
