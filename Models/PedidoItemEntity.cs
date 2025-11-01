using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("pedidos_items")]
public partial class PedidoItemEntity
{
    [Key]
    [Column("id_pedidoItem")]
    public int IdPedidoItem { get; set; }

    [Column("id_item")]
    public int? IdItem { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio", TypeName = "decimal(18, 2)")]
    public decimal Precio { get; set; }

    [Column("id_pedido")]
    public int? IdPedido { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("descript")]
    [StringLength(100)]
    public string? Descript { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("PedidoItemEntity")]
    public virtual ItemEntity? IdItemNavigation { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("PedidoItemEntity")]
    public virtual PedidoEntity? IdPedidoNavigation { get; set; }

    [InverseProperty("IdPedidoItemNavigation")]
    public virtual ICollection<TmpPedidoItemEntity> TmpPedidoItemEntity { get; set; } = new List<TmpPedidoItemEntity>();
}
