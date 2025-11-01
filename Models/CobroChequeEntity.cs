using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[PrimaryKey("IdCobro", "IdCheque")]
[Table("cobros_cheques")]
public partial class CobroChequeEntity
{
    [Key]
    [Column("id_cobro")]
    public int IdCobro { get; set; }

    [Key]
    [Column("id_cheque")]
    public int IdCheque { get; set; }

    [ForeignKey("IdCheque")]
    [InverseProperty("CobroChequeEntity")]
    public virtual ChequeEntity IdChequeNavigation { get; set; } = null!;

    [ForeignKey("IdCobro")]
    [InverseProperty("CobroChequeEntity")]
    public virtual CobroEntity IdCobroNavigation { get; set; } = null!;
}
