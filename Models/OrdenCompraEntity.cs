using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("ordenes_compras")]
public partial class OrdenCompraEntity
{
    [Key]
    [Column("id_ordenCompra")]
    public int IdOrdenCompra { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("fecha_carga")]
    public DateOnly FechaCarga { get; set; }

    [Column("fecha_comprobante")]
    public DateOnly FechaComprobante { get; set; }

    [Column("fecha_recepcion")]
    public DateOnly? FechaRecepcion { get; set; }

    [Column("subtotal", TypeName = "decimal(18, 3)")]
    public decimal Subtotal { get; set; }

    [Column("iva", TypeName = "decimal(18, 3)")]
    public decimal Iva { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("recibido")]
    public bool? Recibido { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("OrdenCompraEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;

    [InverseProperty("IdOrdenCompraNavigation")]
    public virtual ICollection<OrdenCompraItemEntity> OrdenCompraItemEntity { get; set; } = new List<OrdenCompraItemEntity>();

    [InverseProperty("IdOrdenCompraNavigation")]
    public virtual ICollection<TmpOcItemEntity> TmpOcItemEntity { get; set; } = new List<TmpOcItemEntity>();
}
