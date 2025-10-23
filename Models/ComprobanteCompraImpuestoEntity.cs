using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("comprobantes_compras_impuestos")]
public partial class ComprobanteCompraImpuestoEntity
{
    [Key]
    [Column("id_comprobanteCompraImpuesto")]
    public int IdComprobanteCompraImpuesto { get; set; }

    [Column("id_comprobanteCompra")]
    public int IdComprobanteCompra { get; set; }

    [Column("id_impuesto")]
    public int IdImpuesto { get; set; }

    [Column("importe", TypeName = "decimal(18, 3)")]
    public decimal Importe { get; set; }

    [ForeignKey("IdComprobanteCompra")]
    [InverseProperty("ComprobanteCompraImpuestoEntity")]
    public virtual ComprobanteCompraEntity IdComprobanteCompraNavigation { get; set; } = null!;

    [ForeignKey("IdImpuesto")]
    [InverseProperty("ComprobanteCompraImpuestoEntity")]
    public virtual ImpuestoEntity IdImpuestoNavigation { get; set; } = null!;
}
