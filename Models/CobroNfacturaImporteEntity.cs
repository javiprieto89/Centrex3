using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("cobros_Nfacturas_importes")]
public partial class CobroNfacturaImporteEntity
{
    [Column("id_cobro")]
    public int IdCobro { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("nfactura")]
    [StringLength(50)]
    public string Nfactura { get; set; } = null!;

    [Column("importe", TypeName = "decimal(18, 6)")]
    public decimal Importe { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("IdCobro")]
    [InverseProperty("CobroNfacturaImporteEntity")]
    public virtual CobroEntity IdCobroNavigation { get; set; } = null!;
}
