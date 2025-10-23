using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("cheques")]
public partial class ChequeEntity
{
    [Key]
    [Column("id_cheque")]
    public int IdCheque { get; set; }

    [Column("fecha_ingreso")]
    public DateOnly FechaIngreso { get; set; }

    [Column("fecha_emision")]
    public DateOnly FechaEmision { get; set; }

    [Column("id_cliente")]
    public int? IdCliente { get; set; }

    [Column("id_proveedor")]
    public int? IdProveedor { get; set; }

    [Column("id_banco")]
    public int IdBanco { get; set; }

    [Column("nCheque")]
    public int NCheque { get; set; }

    [Column("nCheque2")]
    public int NCheque2 { get; set; }

    [Column("importe", TypeName = "decimal(18, 3)")]
    public decimal Importe { get; set; }

    [Column("id_estadoch")]
    public int IdEstadoch { get; set; }

    [Column("fecha_cobro")]
    public DateOnly? FechaCobro { get; set; }

    [Column("fecha_salida")]
    public DateOnly? FechaSalida { get; set; }

    [Column("fecha_deposito")]
    public DateOnly? FechaDeposito { get; set; }

    [Column("recibido")]
    public bool? Recibido { get; set; }

    [Column("emitido")]
    public bool? Emitido { get; set; }

    [Column("id_cuentaBancaria")]
    public int? IdCuentaBancaria { get; set; }

    [Column("eCheck")]
    public bool ECheck { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [InverseProperty("IdChequeNavigation")]
    public virtual ICollection<CobroChequeEntity> CobroChequeEntity { get; set; } = new List<CobroChequeEntity>();

    [ForeignKey("IdBanco")]
    [InverseProperty("ChequeEntity")]
    public virtual BancoEntity IdBancoNavigation { get; set; } = null!;

    [ForeignKey("IdCliente")]
    [InverseProperty("ChequeEntity")]
    public virtual ClienteEntity? IdClienteNavigation { get; set; }

    [ForeignKey("IdCuentaBancaria")]
    [InverseProperty("ChequeEntity")]
    public virtual CuentaBancariaEntity? IdCuentaBancariaNavigation { get; set; }

    [ForeignKey("IdEstadoch")]
    [InverseProperty("ChequeEntity")]
    public virtual SysEstadoChequeEntity IdEstadochNavigation { get; set; } = null!;

    [ForeignKey("IdProveedor")]
    [InverseProperty("ChequeEntity")]
    public virtual ProveedorEntity? IdProveedorNavigation { get; set; }

    [InverseProperty("IdChequeNavigation")]
    public virtual ICollection<PagoChequeEntity> PagoChequeEntity { get; set; } = new List<PagoChequeEntity>();

    [InverseProperty("IdChequeNavigation")]
    public virtual ICollection<TmpSelChEntity> TmpSelChEntity { get; set; } = new List<TmpSelChEntity>();
}
