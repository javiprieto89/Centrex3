using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("cobros")]
public partial class CobroEntity
{
    [Key]
    [Column("id_cobro")]
    public int IdCobro { get; set; }

    [Column("id_cobro_oficial")]
    public int IdCobroOficial { get; set; }

    [Column("id_cobro_no_oficial")]
    public int IdCobroNoOficial { get; set; }

    [Column("fecha_carga")]
    public DateOnly FechaCarga { get; set; }

    [Column("fecha_cobro")]
    public DateOnly FechaCobro { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_cc")]
    public int IdCc { get; set; }

    [Column("dineroEnCc", TypeName = "decimal(18, 3)")]
    public decimal DineroEnCc { get; set; }

    [Column("efectivo", TypeName = "decimal(18, 3)")]
    public decimal Efectivo { get; set; }

    [Column("totalTransferencia", TypeName = "decimal(18, 3)")]
    public decimal TotalTransferencia { get; set; }

    [Column("totalCh", TypeName = "decimal(18, 3)")]
    public decimal TotalCh { get; set; }

    [Column("totalRetencion", TypeName = "decimal(18, 3)")]
    public decimal TotalRetencion { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("hayCheque")]
    public bool HayCheque { get; set; }

    [Column("hayTransferencia")]
    public bool HayTransferencia { get; set; }

    [Column("hayRetencion")]
    public bool HayRetencion { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("id_anulaCobro")]
    public int? IdAnulaCobro { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [Column("firmante")]
    [StringLength(50)]
    public string Firmante { get; set; } = null!;

    [InverseProperty("IdCobroNavigation")]
    public virtual ICollection<CobroChequeEntity> CobroChequeEntity { get; set; } = new List<CobroChequeEntity>();

    [InverseProperty("IdCobroNavigation")]
    public virtual ICollection<CobroNfacturaImporteEntity> CobroNfacturaImporteEntity { get; set; } = new List<CobroNfacturaImporteEntity>();

    [InverseProperty("IdCobroNavigation")]
    public virtual ICollection<CobroRetencionEntity> CobroRetencionEntity { get; set; } = new List<CobroRetencionEntity>();

    [ForeignKey("IdCliente")]
    [InverseProperty("CobroEntity")]
    public virtual ClienteEntity IdClienteNavigation { get; set; } = null!;

    [InverseProperty("IdCobroNavigation")]
    public virtual ICollection<TmpSelChEntity> TmpSelChEntity { get; set; } = new List<TmpSelChEntity>();

    [InverseProperty("IdCobroNavigation")]
    public virtual ICollection<TransaccionEntity> TransaccionEntity { get; set; } = new List<TransaccionEntity>();

    [InverseProperty("IdCobroNavigation")]
    public virtual ICollection<TransferenciaEntity> TransferenciaEntity { get; set; } = new List<TransferenciaEntity>();
}
