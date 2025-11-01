using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("tmppedidos_items")]
public partial class TmpPedidoItemEntity
{
    [Key]
    [Column("id_tmpPedidoItem")]
    public int IdTmpPedidoItem { get; set; }

    [Column("id_pedidoItem")]
    public int? IdPedidoItem { get; set; }

    [Column("id_pedido")]
    public int? IdPedido { get; set; }

    [Column("id_item")]
    public int? IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio", TypeName = "decimal(18, 2)")]
    public decimal Precio { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string? Descript { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_unico")]
    public Guid IdUnico { get; set; }

    [ForeignKey("IdPedidoItem")]
    [InverseProperty("TmpPedidoItemEntity")]
    public virtual PedidoItemEntity? IdPedidoItemNavigation { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("TmpPedidoItemEntity")]
    public virtual PedidoEntity? IdPedidoNavigation { get; set; }
}
