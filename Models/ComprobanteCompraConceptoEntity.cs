using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("comprobantes_compras_conceptos")]
public partial class ComprobanteCompraConceptoEntity
{
    [Key]
    [Column("id_comprobanteCompraConcepto")]
    public int IdComprobanteCompraConcepto { get; set; }

    [Column("id_comprobanteCompra")]
    public int IdComprobanteCompra { get; set; }

    [Column("id_concepto_compra")]
    public int IdConceptoCompra { get; set; }

    [Column("subtotal", TypeName = "decimal(18, 3)")]
    public decimal Subtotal { get; set; }

    [Column("iva", TypeName = "decimal(18, 3)")]
    public decimal Iva { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [ForeignKey("IdComprobanteCompra")]
    [InverseProperty("ComprobanteCompraConceptoEntity")]
    public virtual ComprobanteCompraEntity IdComprobanteCompraNavigation { get; set; } = null!;

    [ForeignKey("IdConceptoCompra")]
    [InverseProperty("ComprobanteCompraConceptoEntity")]
    public virtual ConceptoCompraEntity IdConceptoCompraNavigation { get; set; } = null!;
}
