using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("pedidos")]
public partial class PedidoEntity
{
    [Key]
    [Column("id_pedido")]
    public int IdPedido { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("fecha_edicion")]
    public DateOnly FechaEdicion { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("markup", TypeName = "decimal(18, 3)")]
    public decimal? Markup { get; set; }

    [Column("subtotal", TypeName = "decimal(18, 3)")]
    public decimal Subtotal { get; set; }

    [Column("iva", TypeName = "decimal(18, 3)")]
    public decimal? Iva { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal Total { get; set; }

    [Column("nota1")]
    [StringLength(200)]
    public string? Nota1 { get; set; }

    [Column("nota2")]
    [StringLength(200)]
    public string? Nota2 { get; set; }

    [Column("esPresupuesto")]
    public bool EsPresupuesto { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("cerrado")]
    public bool Cerrado { get; set; }

    [Column("idPresupuesto")]
    public int? IdPresupuesto { get; set; }

    [Column("id_comprobante")]
    public int IdComprobante { get; set; }

    [Column("cae")]
    [StringLength(50)]
    public string? Cae { get; set; }

    [Column("fechaVencimiento_cae")]
    public DateOnly? FechaVencimientoCae { get; set; }

    [Column("puntoVenta")]
    public int? PuntoVenta { get; set; }

    [Column("numeroComprobante")]
    public int? NumeroComprobante { get; set; }

    [Column("codigoDeBarras")]
    [StringLength(100)]
    public string? CodigoDeBarras { get; set; }

    [Column("esTest")]
    public bool EsTest { get; set; }

    [Column("id_cc")]
    public int? IdCc { get; set; }

    [Column("fc_qr")]
    public byte[]? FcQr { get; set; }

    [Column("numeroComprobante_anulado")]
    public int? NumeroComprobanteAnulado { get; set; }

    [Column("numeroPedido_anulado")]
    public int? NumeroPedidoAnulado { get; set; }

    [Column("esDuplicado")]
    public bool? EsDuplicado { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [ForeignKey("IdCc, IdCliente")]
    [InverseProperty("PedidoEntity")]
    public virtual CcClienteEntity? CcClienteEntity { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("PedidoEntity")]
    public virtual ClienteEntity IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdComprobante")]
    [InverseProperty("PedidoEntity")]
    public virtual ComprobanteEntity IdComprobanteNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("PedidoEntity")]
    public virtual UsuarioEntity? IdUsuarioNavigation { get; set; }

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<PedidoItemEntity> PedidoItemEntity { get; set; } = new List<PedidoItemEntity>();

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<TmpPedidoItemEntity> TmpPedidoItemEntity { get; set; } = new List<TmpPedidoItemEntity>();

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<TransaccionEntity> TransaccionEntity { get; set; } = new List<TransaccionEntity>();
}
