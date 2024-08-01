using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratorApilo.Shared.Streamsoft;

public class BlankOutput
{
    [Column("SUCCESS")] public int? Success { get; set; }
}