using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tmpOC_items")]
public partial class TmpOcItemEntity
{
    [Key]
    [Column("id_tmpOCItem")]
    public int IdTmpOcitem { get; set; }

    [Column("id_ocItem")]
    public int? IdOcItem { get; set; }

    [Column("id_ordenCompra")]
    public int? IdOrdenCompra { get; set; }

    [Column("id_item")]
    public int? IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio", TypeName = "decimal(18, 3)")]
    public decimal Precio { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string? Descript { get; set; }

    [Column("cantidad_recibida")]
    public int? CantidadRecibida { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("IdOcItem")]
    [InverseProperty("TmpOcItemEntity")]
    public virtual OrdenCompraItemEntity? IdOcItemNavigation { get; set; }

    [ForeignKey("IdOrdenCompra")]
    [InverseProperty("TmpOcItemEntity")]
    public virtual OrdenCompraEntity? IdOrdenCompraNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("TmpOcItemEntity")]
    public virtual UsuarioEntity IdUsuarioNavigation { get; set; } = null!;
}
