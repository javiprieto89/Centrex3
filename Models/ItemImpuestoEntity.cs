using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[PrimaryKey("IdItem", "IdImpuesto")]
[Table("items_impuestos")]
public partial class ItemImpuestoEntity
{
    [Key]
    [Column("id_item")]
    public int IdItem { get; set; }

    [Key]
    [Column("id_impuesto")]
    public int IdImpuesto { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [ForeignKey("IdImpuesto")]
    [InverseProperty("ItemImpuestoEntity")]
    public virtual ImpuestoEntity IdImpuestoNavigation { get; set; } = null!;

    [ForeignKey("IdItem")]
    [InverseProperty("ItemImpuestoEntity")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;
}
