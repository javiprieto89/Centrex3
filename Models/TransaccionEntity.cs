using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centrex.Models;

[Table("transacciones")]
public partial class TransaccionEntity
{
    [Key]
    [Column("id_transaccion")]
    public int IdTransaccion { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("id_pedido")]
    public int? IdPedido { get; set; }

    [Column("id_comprobanteCompra")]
    public int? IdComprobanteCompra { get; set; }

    [Column("id_cobro")]
    public int? IdCobro { get; set; }

    [Column("id_pago")]
    public int? IdPago { get; set; }

    [Column("id_tipoComprobante")]
    public int? IdTipoComprobante { get; set; }

    [Column("numeroComprobante")]
    public int? NumeroComprobante { get; set; }

    [Column("puntoVenta")]
    public int? PuntoVenta { get; set; }

    [Column("total", TypeName = "decimal(18, 3)")]
    public decimal? Total { get; set; }

    [Column("id_cc")]
    public int? IdCc { get; set; }

    [Column("id_cliente")]
    public int? IdCliente { get; set; }

    [Column("id_proveedor")]
    public int? IdProveedor { get; set; }

    [ForeignKey("IdCobro")]
    [InverseProperty("TransaccionEntity")]
    public virtual CobroEntity? IdCobroNavigation { get; set; }

    [ForeignKey("IdComprobanteCompra")]
    [InverseProperty("TransaccionEntity")]
    public virtual ComprobanteCompraEntity? IdComprobanteCompraNavigation { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("TransaccionEntity")]
    public virtual ClienteEntity? IdClienteNavigation { get; set; }

    [ForeignKey("IdPago")]
    [InverseProperty("TransaccionEntity")]
    public virtual PagoEntity? IdPagoNavigation { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("TransaccionEntity")]
    public virtual PedidoEntity? IdPedidoNavigation { get; set; }

    [ForeignKey("IdTipoComprobante")]
    [InverseProperty("TransaccionEntity")]
    public virtual TipoComprobanteEntity? IdTipoComprobanteNavigation { get; set; }
}
