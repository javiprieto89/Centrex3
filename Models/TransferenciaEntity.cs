using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("transferencias")]
public partial class TransferenciaEntity
{
    [Key]
    [Column("id_transferencia")]
    public int IdTransferencia { get; set; }

    [Column("id_cobro")]
    public int? IdCobro { get; set; }

    [Column("id_pago")]
    public int? IdPago { get; set; }

    [Column("id_cuentaBancaria")]
    public int IdCuentaBancaria { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("nComprobante")]
    [StringLength(50)]
    public string? NComprobante { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [ForeignKey("IdCobro")]
    [InverseProperty("TransferenciaEntity")]
    public virtual CobroEntity? IdCobroNavigation { get; set; }

    [ForeignKey("IdCuentaBancaria")]
    [InverseProperty("TransferenciaEntity")]
    public virtual CuentaBancariaEntity IdCuentaBancariaNavigation { get; set; } = null!;

    [ForeignKey("IdPago")]
    [InverseProperty("TransferenciaEntity")]
    public virtual PagoEntity? IdPagoNavigation { get; set; }
}
