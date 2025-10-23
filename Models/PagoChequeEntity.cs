using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[PrimaryKey("IdPago", "IdCheque")]
[Table("pagos_cheques")]
public partial class PagoChequeEntity
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Key]
    [Column("id_cheque")]
    public int IdCheque { get; set; }

    [ForeignKey("IdCheque")]
    [InverseProperty("PagoChequeEntity")]
    public virtual ChequeEntity IdChequeNavigation { get; set; } = null!;

    [ForeignKey("IdPago")]
    [InverseProperty("PagoChequeEntity")]
    public virtual PagoEntity IdPagoNavigation { get; set; } = null!;
}
