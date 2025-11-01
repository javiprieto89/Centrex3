using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("pagos")]
public partial class PagoEntity
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("fecha_carga")]
    public DateOnly? FechaCarga { get; set; }

    [Column("fecha_pago")]
    public DateOnly FechaPago { get; set; }

    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("id_cc")]
    public int IdCc { get; set; }

    [Column("dineroEnCc", TypeName = "decimal(18, 3)")]
    public decimal DineroEnCc { get; set; }

    [Column("efectivo", TypeName = "decimal(18, 3)")]
    public decimal Efectivo { get; set; }

    [Column("totalTransferencia", TypeName = "decimal(18, 3)")]
    public decimal TotalTransferencia { get; set; }

    [Column("totalCh", TypeName = "decimal(18, 0)")]
    public decimal TotalCh { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("hayCheque")]
    public bool HayCheque { get; set; }

    [Column("hayTransferencia")]
    public bool HayTransferencia { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("id_anulaPago")]
    public int? IdAnulaPago { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [ForeignKey("IdCc")]
    [InverseProperty("PagoEntity")]
    public virtual CcProveedorEntity IdCcProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdProveedor")]
    [InverseProperty("PagoEntity")]
    public virtual ProveedorEntity IdProveedorNavigation { get; set; } = null!;

    [InverseProperty("IdPagoNavigation")]
    public virtual ICollection<PagoChequeEntity> PagoChequeEntity { get; set; } = new List<PagoChequeEntity>();

    [InverseProperty("IdPagoNavigation")]
    public virtual ICollection<PagoNfacturaImporteEntity> PagoNfacturaImporteEntity { get; set; } = new List<PagoNfacturaImporteEntity>();

    [InverseProperty("IdPagoNavigation")]
    public virtual ICollection<TmpSelChEntity> TmpSelChEntity { get; set; } = new List<TmpSelChEntity>();

    [InverseProperty("IdPagoNavigation")]
    public virtual ICollection<TransaccionEntity> TransaccionEntity { get; set; } = new List<TransaccionEntity>();

    [InverseProperty("IdPagoNavigation")]
    public virtual ICollection<TransferenciaEntity> TransferenciaEntity { get; set; } = new List<TransferenciaEntity>();
}
