using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("separador_decimal")]
public partial class SeparadorDecimalEntity
{
    [Key]
    [Column("id_separador")]
    public int IdSeparador { get; set; }

    [Column("separador")]
    [StringLength(1)]
    [Unicode(false)]
    public string Separador { get; set; } = null!;
}
