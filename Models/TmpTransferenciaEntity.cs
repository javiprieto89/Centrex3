using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("tmptransferencias")]
public partial class TmpTransferenciaEntity
{
    [Key]
    [Column("id_tmpTransferencia")]
    public int IdTmpTransferencia { get; set; }

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

    [ForeignKey("IdCuentaBancaria")]
    [InverseProperty("TmpTransferenciaEntity")]
    public virtual CuentaBancariaEntity IdCuentaBancariaNavigation { get; set; } = null!;
}
