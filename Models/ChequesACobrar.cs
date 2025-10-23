using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Keyless]
public partial class ChequesACobrarEntity
{
    [Column("fecha_cobro")]
    public DateOnly? FechaCobro { get; set; }

    [Column("importe", TypeName = "decimal(18, 3)")]
    public decimal Importe { get; set; }

    [Column("banco")]
    [StringLength(100)]
    public string Banco { get; set; } = null!;

    [Column("cuenta_bancaria")]
    [StringLength(100)]
    public string CuentaBancaria { get; set; } = null!;

    [Column("saldo", TypeName = "decimal(18, 3)")]
    public decimal? Saldo { get; set; }

    [Column("fecha_cobro_real")]
    public DateOnly? FechaCobroReal { get; set; }

    [Column("id_banco")]
    public int IdBanco { get; set; }

    [Column("id_cuentaBancaria")]
    public int? IdCuentaBancaria { get; set; }

    [Column("id_estadoch")]
    public int IdEstadoch { get; set; }

    [Column("id_cheque")]
    public int IdCheque { get; set; }
}
