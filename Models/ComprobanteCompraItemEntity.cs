using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Models;

[Table("comprobantes_compras_items")]
public partial class ComprobanteCompraItemEntity
{
    [Key]
    [Column("id_comprobanteCompraItem")]
    public int IdComprobanteCompraItem { get; set; }

    [Column("id_comprobanteCompra")]
    public int IdComprobanteCompra { get; set; }

    [Column("id_item")]
    public int IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio", TypeName = "decimal(18, 2)")]
    public decimal Precio { get; set; }

    [ForeignKey("IdComprobanteCompra")]
    [InverseProperty("ComprobanteCompraItemEntity")]
    public virtual ComprobanteCompraEntity IdComprobanteCompraNavigation { get; set; } = null!;

    [ForeignKey("IdItem")]
    [InverseProperty("ComprobanteCompraItemEntity")]
    public virtual ItemEntity IdItemNavigation { get; set; } = null!;
}
