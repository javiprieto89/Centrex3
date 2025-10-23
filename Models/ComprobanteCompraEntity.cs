using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("comprobantes_compras")]
public partial class ComprobanteCompraEntity
{
    [Key]
    [Column("id_comprobanteCompra")]
    public int IdComprobanteCompra { get; set; }

    [Column("fecha_carga")]
    public DateOnly FechaCarga { get; set; }

    [Column("fecha_comprobante")]
    public DateOnly FechaComprobante { get; set; }

    [Column("id_tipoComprobante")]
    public int IdTipoComprobante { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("id_cc")]
    public int IdCc { get; set; }

    [Column("id_moneda")]
    public int IdMoneda { get; set; }

    [Column("puntoVenta")]
    [StringLength(10)]
    public string? PuntoVenta { get; set; }

    [Column("numeroComprobante")]
    [StringLength(50)]
    public string? NumeroComprobante { get; set; }

    [Column("id_condicion_compra")]
    public int IdCondicionCompra { get; set; }

    [Column("subtotal", TypeName = "decimal(18, 3)")]
    public decimal? Subtotal { get; set; }

    [Column("impuestos", TypeName = "decimal(18, 3)")]
    public decimal? Impuestos { get; set; }

    [Column("conceptos", TypeName = "decimal(18, 3)")]
    public decimal? Conceptos { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal? Total { get; set; }

    [Column("tasaCambio", TypeName = "decimal(18, 3)")]
    public decimal? TasaCambio { get; set; }

    [Column("nota")]
    public string? Nota { get; set; }

    [Column("cae")]
    [StringLength(50)]
    public string? Cae { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdComprobanteCompraNavigation")]
    public virtual ICollection<ComprobanteCompraConceptoEntity> ComprobanteCompraConceptoEntity { get; set; } = new List<ComprobanteCompraConceptoEntity>();

    [InverseProperty("IdComprobanteCompraNavigation")]
    public virtual ICollection<ComprobanteCompraImpuestoEntity> ComprobanteCompraImpuestoEntity { get; set; } = new List<ComprobanteCompraImpuestoEntity>();

    [InverseProperty("IdComprobanteCompraNavigation")]
    public virtual ICollection<ComprobanteCompraItemEntity> ComprobanteCompraItemEntity { get; set; } = new List<ComprobanteCompraItemEntity>();

    [ForeignKey("IdCc")]
    [InverseProperty("ComprobanteCompraEntity")]
    public virtual CcProveedorEntity IdCcProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdCondicionCompra")]
    [InverseProperty("ComprobanteCompraEntity")]
    public virtual CondicionCompraEntity IdCondicionCompraNavigation { get; set; } = null!;

    [ForeignKey("IdMoneda")]
    [InverseProperty("ComprobanteCompraEntity")]
    public virtual SysMonedaEntity IdMonedaNavigation { get; set; } = null!;

    [ForeignKey("IdProveedor")]
    [InverseProperty("ComprobanteCompraEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdTipoComprobante")]
    [InverseProperty("ComprobanteCompraEntity")]
    public virtual TipoComprobanteEntity IdTipoComprobanteNavigation { get; set; } = null!;

    [InverseProperty("IdComprobanteCompraNavigation")]
    public virtual ICollection<TransaccionEntity> TransaccionEntity { get; set; } = new List<TransaccionEntity>();
}
