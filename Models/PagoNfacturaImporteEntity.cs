using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("pagos_nFacturas_importes")]
public partial class PagoNfacturaImporteEntity
{
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("nfactura")]
    [StringLength(50)]
    public string Nfactura { get; set; } = null!;

    [Column("importe", TypeName = "decimal(18, 3)")]
    public decimal Importe { get; set; }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("IdPago")]
    [InverseProperty("PagoNfacturaImporteEntity")]
    public virtual PagoEntity IdPagoNavigation { get; set; } = null!;
}
