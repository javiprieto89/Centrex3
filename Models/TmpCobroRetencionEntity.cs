using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tmpcobros_retenciones")]
public partial class TmpCobroRetencionEntity
{
    [Key]
    [Column("id_tmpRetencion")]
    public int IdTmpRetencion { get; set; }

    [Column("id_impuesto")]
    public int IdImpuesto { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [ForeignKey("IdImpuesto")]
    [InverseProperty("TmpCobroRetencionEntity")]
    public virtual ImpuestoEntity IdImpuestoNavigation { get; set; } = null!;
}
