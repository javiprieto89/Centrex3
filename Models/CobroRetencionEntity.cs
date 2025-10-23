using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("cobros_retenciones")]
public partial class CobroRetencionEntity
{
    [Key]
    [Column("id_retencion")]
    public int IdRetencion { get; set; }

    [Column("id_cobro")]
    public int IdCobro { get; set; }

    [Column("id_impuesto")]
    public int IdImpuesto { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [ForeignKey("IdCobro")]
    [InverseProperty("CobroRetencionEntity")]
    public virtual CobroEntity IdCobroNavigation { get; set; } = null!;

    [ForeignKey("IdImpuesto")]
    [InverseProperty("CobroRetencionEntity")]
    public virtual ImpuestoEntity IdImpuestoNavigation { get; set; } = null!;
}
