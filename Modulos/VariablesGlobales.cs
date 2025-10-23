using System;
using System.Windows.Forms;


namespace Centrex
{

    public static class VariablesGlobales
    {

        // ======================================================
        // VARIABLES GLOBALES DE CONFIGURACIÓN
        // ======================================================
        public static string pc = "";
        public static string serversql = "127.0.0.1";
        public static string basedb = "dbCentrex";
        public static string versiondb = "";
        public static bool depuracion = false;
        public static bool modificacionesDB = false;
        public static string usuariodb = "sa";
        public static string passdb = "Ladeda78";
        public static object CN = null;
        public static string configuracion_regional = "";

        // ======================================================
        // VARIABLES DE USUARIO
        // ======================================================
        public static UsuarioEntity usuario_logueado = null;

        // ======================================================
        // VARIABLES DE INTERFAZ
        // ======================================================
        public static string tabla = "";
        public static string tabla_vieja = "";
        public static int itXPage = 50;
        public static int nRegs = 0;
        public static int tPaginas = 0;
        public static int pagina = 1;
        public static int desde = 0;
        public static int hasta = 0;
        public static Form frmCambios = null;
        public static bool showrpt = false;

        // ======================================================
        // VARIABLES DE ESTADO / CONTROL
        // ======================================================
        public static bool edicion = false;
        public static bool borrado = false;
        public static bool activo = true;
        public static bool agregaitem = false;
        public static bool busquedaavanzada = false;
        public static bool valida = false;
        public static int id = 0;
        public static string secuencia = "";
        public static bool terminarpedido = false;
        public static bool facturar = false;
        public static bool imprimirFactura = false;
        public static bool recargaPrecios = false;

        // ======================================================
        // VARIABLES DE FECHA Y FORMATO
        // ======================================================
        public static string Hoy_DateFormat = DateTime.Now.ToString("dd/MM/yyyy");
        public static DateTime Fecha_Sistema = DateTime.Now;
        public static string sepDecimal = ".";

        // ======================================================
        // VARIABLES DE BACKUP
        // ======================================================
        public static string archivoBackup = "";
        public static string dbBackup = "";
        public static string rutaBackup = "";

        // ======================================================
        // VARIABLES POR DEFECTO
        // ======================================================
        public static int comprobantePresupuesto_default = 0;
        public static int id_comprobante_default = 0;
        public static int id_tipoDocumento_default = 0;
        public static int id_tipoComprobante_default = 0;
        public static int id_cliente_pedido_default = 0;
        public static int id_pais_default = 0;
        public static int id_provincia_default = 0;
        public static int id_marca_default = 0;
        public static int id_proveedor_default = 0;
        public static int id_condicion_compra_default = 0;
        public static int id_caja = 0;
        public static string cuit_emisor_default = "";
        public static string STR_COMPROBANTES_CONTABLES = "";

        // ======================================================
        // VARIABLES GLOBALES DE ENTIDADES (EDICIÓN)
        // ======================================================
        public static ItemEntity edita_item;
        public static ItemEntity edita_item_entity;
        public static MarcaItemEntity edita_marcai;
        public static ProveedorEntity edita_proveedor;
        public static ProduccionEntity edita_produccion;
        public static RegistroStockEntity edita_registro_stock;
        public static TipoItemEntity edita_tipoitem;
        public static PedidoEntity edita_pedido;
        public static ConceptoCompraEntity edita_concepto_compra;
        public static CuentaBancariaEntity edita_cuentaBancaria;
        public static OrdenCompraEntity edita_ordenCompra;
        public static TransferenciaEntity edita_transferencia;
        public static banco edita_banco;
        public static caja edita_caja;
        public static ccCliente edita_ccCliente;
        public static ccProveedor edita_ccProveedor;
        public static cheque edita_cheque;
        public static cliente edita_cliente;
        public static comprobante edita_comprobante;
        public static condicion_compra edita_condicion_compra;
        public static condicion_venta edita_condicion_venta;
        public static ConsultaPersonalizadaEntity edita_Consulta;
        public static impuesto edita_impuesto;
        public static registro_stock edita_item_registro_stock;
        public static itemImpuesto edita_itemImpuesto;
        public static perfil edita_perfil;
        public static permiso edita_permiso;
        public static perfil_permiso edita_permiso_perfil;
        public static asocItem edita_asocItem;
        public static usuario edita_usuario;

        // ======================================================
        // VARIABLES DE PROCESOS
        // ======================================================
        public static string logLine = "";
        public static string InfoTmpTransferencia = "";
        public const int ID_CH_CARTERA = 1;
        public const int ID_CH_ENTREGADO = 2;
        public const int ID_CH_COBRADO = 3;
        public const int ID_CH_RECHAZADO = 4;
        public const int ID_CH_DEPOSITADO = 5;
        public const int ID_PESO = 1;
        public const int ID_DOLAR = 2;
        public static double Ultima_CC_Pedido_Cliente = 0d;
        public static string consultaUltimoComprobante = "";
        public static string Consultar_Comprobante = "";
        public static double precio = 0d;

        // ======================================================
        // VARIABLES AFIP Y OTROS OBJETOS EXTERNOS
        // ======================================================
        public static object wsfe = null;
        public static object selCheques = null;


        // ======================================================
        // FUNCIONES PLACEHOLDER (COMPATIBILIDAD LEGACY)
        // ======================================================
        public static string obtieneValorConfigGlobal(string str)
        {
            return Strings.Trim(Strings.Right(str, str.Length - (Strings.InStr(str, "=") + 1)));
        }




        // ======================================================
        // FUNCIONES DE CONVERSIÓN ENTRE ENTIDADES Y MODELOS LEGACY
        // ======================================================
        public static item ConvertToItem(ItemEntity itemEntity)
        {
            return new item()
            {
                id_item = itemEntity.IdItem,
                itemField = itemEntity.item,
                descript = itemEntity.descript,
                cantidad = (decimal)itemEntity.cantidad,
                precio = itemEntity.Precio,
                esDescuento = itemEntity.esDescuento,
                IdItemTemporal = itemEntity.IdItemTemporal
            };
        }

        public static TmpTransferenciaEntity ConvertToTmpTransferencia(TransferenciaEntity transferenciaEntity)
        {
            return new TmpTransferenciaEntity()
            {
                IdTransferencia = transferenciaEntity.IdTransferencia,
                IdCuentaBancaria = transferenciaEntity.IdCuentaBancaria,
                fecha = transferenciaEntity.fecha,
                total = transferenciaEntity.total,
                nComprobante = transferenciaEntity.nComprobante,
                notas = transferenciaEntity.notas
            };
        }

        public static pedido ConvertToPedido(PedidoEntity pedidoEntity)
        {
            return new pedido()
            {
                id_pedido = pedidoEntity.IdPedido,
                fecha = Conversions.ToString(pedidoEntity.fecha.Value),
                id_cliente = pedidoEntity.IdCliente,
                subTotal = (double)pedidoEntity.subtotal,
                iva = (double)pedidoEntity.iva,
                total = (double)pedidoEntity.total,
                activo = pedidoEntity.activo,
                cerrado = pedidoEntity.cerrado,
                esPresupuesto = pedidoEntity.esPresupuesto
            };
        }

        public static PedidoEntity ConvertToPedidoEntity(pedido pedido)
        {
            return new PedidoEntity()
            {
                IdPedido = pedido.id_pedido,
                fecha = Conversions.ToDate(pedido.fecha),
                IdCliente = pedido.id_cliente,
                subtotal = (decimal)pedido.subTotal,
                iva = (decimal?)pedido.iva,
                total = (decimal)pedido.total,
                activo = pedido.activo,
                cerrado = pedido.cerrado,
                esPresupuesto = pedido.esPresupuesto
            };
        }

        public static comprobante ConvertToComprobante(ComprobanteEntity comprobanteEntity)
        {
            return new comprobante()
            {
                id_comprobante = comprobanteEntity.IdComprobante,
                fecha = comprobanteEntity.Fecha,
                id_cliente = comprobanteEntity.IdCliente,
                subtotal = comprobanteEntity.Subtotal,
                iva = comprobanteEntity.Iva,
                total = comprobanteEntity.Total,
                activo = comprobanteEntity.activo
            };
        }

        public static ComprobanteEntity ConvertToComprobanteEntity(comprobante comprobante)
        {
            return new ComprobanteEntity()
            {
                IdComprobante = comprobante.id_comprobante,
                Fecha = comprobante.fecha,
                IdCliente = comprobante.id_cliente,
                Subtotal = comprobante.subtotal,
                Iva = comprobante.iva,
                Total = comprobante.total,
                activo = comprobante.activo
            };
        }

        public static itemImpuesto ConvertToItemImpuesto(ItemImpuestoEntity itemImpuestoEntity)
        {
            return new itemImpuesto()
            {
                id_itemImpuesto = itemImpuestoEntity.IdItemImpuesto,
                id_item = itemImpuestoEntity.IdItem,
                id_impuesto = itemImpuestoEntity.IdImpuesto,
                activo = itemImpuestoEntity.activo
            };
        }

    }
}
