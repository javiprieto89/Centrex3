using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("ordenesCompras_items")]
public partial class OrdenCompraItemEntity
{
    [Key]
    [Column("id_ocItem")]
    public int IdOcItem { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("descript")]
    [StringLength(54)]
    public string? Descript { get; set; }

    [Column("cantidad_recibida")]
    public int? CantidadRecibida { get; set; }

    [Column("precio", TypeName = "decimal(18, 3)")]
    public decimal Precio { get; set; }

    [Column("id_ordenCompra")]
    public int? IdOrdenCompra { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("OrdenCompraItemEntity")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;

    [ForeignKey("IdOrdenCompra")]
    [InverseProperty("OrdenCompraItemEntity")]
    public virtual OrdenCompraEntity? IdOrdenCompraNavigation { get; set; }

    [InverseProperty("IdOcItemNavigation")]
    public virtual ICollection<TmpOcItemEntity> TmpOcItemEntity { get; set; } = new List<TmpOcItemEntity>();
}
