using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tmpSelCH")]
public partial class TmpSelChEntity
{
    [Column("id_cobro")]
    public int? IdCobro { get; set; }

    [Column("id_pago")]
    public int? IdPago { get; set; }

    [Column("id_cheque")]
    public int IdCheque { get; set; }

    [Column("seleccionado")]
    public bool Seleccionado { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("IdCheque")]
    [InverseProperty("TmpSelChEntity")]
    public virtual ChequeEntity IdChequeNavigation { get; set; } = null!;

    [ForeignKey("IdCobro")]
    [InverseProperty("TmpSelChEntity")]
    public virtual CobroEntity? IdCobroNavigation { get; set; }

    [ForeignKey("IdPago")]
    [InverseProperty("TmpSelChEntity")]
    public virtual PagoEntity? IdPagoNavigation { get; set; }
}
