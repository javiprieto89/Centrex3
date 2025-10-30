using Centrex.Models;
using Microsoft.VisualBasic;
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
        public static TransferenciaEntity edita_transferencia;
        public static TmpTransferenciaEntity edita_tmpTransferencia;
        public static TipoItemEntity edita_tipoitem;
        public static PedidoEntity edita_pedido;
        public static ConceptoCompraEntity edita_concepto_compra;
        public static CuentaBancariaEntity edita_cuentaBancaria;
        public static OrdenCompraEntity edita_ordenCompra;
        //public static TransferenciaEntity edita_transferencia;
        public static BancoEntity edita_banco;
        public static CajaEntity edita_caja;
        public static CcClienteEntity edita_ccCliente;
        public static CcProveedorEntity edita_ccProveedor;
        public static ChequeEntity edita_cheque;
        public static ClienteEntity edita_cliente;
        public static ComprobanteEntity edita_comprobante;
        public static CondicionCompraEntity edita_condicion_compra;   
        public static CondicionVentaEntity edita_condicion_venta;
public static ConsultaPersonalizadaEntity edita_Consulta;
        public static ImpuestoEntity edita_impuesto;
        public static RegistroStockEntity edita_item_registro_stock;
        public static ItemImpuestoEntity edita_itemImpuesto;
        public static PerfilEntity edita_perfil;
        public static PermisoEntity edita_permiso;
        public static PermisoPerfilEntity edita_permiso_perfil;
        public static AsocItemEntity edita_asocItem;
        public static UsuarioEntity edita_usuario;

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
        public static double precio = 0d;
        public static bool editaStock = false;
        public static bool edicion_item_registro_stock = false;        

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
        public static ItemEntity ConvertToItem(ItemEntity itemEntity)
        {
    var result = new ItemEntity();
       result.IdItem = itemEntity.IdItem;
  result.Item = itemEntity.Item;
    result.Descript = itemEntity.Descript;
      result.Cantidad = itemEntity.Cantidad;
            result.PrecioLista = itemEntity.PrecioLista;
            result.EsDescuento = itemEntity.EsDescuento;
    result.Activo = itemEntity.Activo;
          return result;
        }

        public static TmpTransferenciaEntity ConvertToTmpTransferencia(TransferenciaEntity transferenciaEntity)
        {
  return new TmpTransferenciaEntity()
            {
IdCuentaBancaria = transferenciaEntity.IdCuentaBancaria,
            Fecha = transferenciaEntity.Fecha,
 Total = transferenciaEntity.Total,
    NComprobante = transferenciaEntity.NComprobante,
       Notas = transferenciaEntity.Notas
            };
        }

        public static PedidoEntity ConvertToPedido(PedidoEntity pedidoEntity)
{
  var result = new PedidoEntity();
         result.IdPedido = pedidoEntity.IdPedido;
            result.Fecha = pedidoEntity.Fecha;
result.IdCliente = pedidoEntity.IdCliente;
result.Subtotal = pedidoEntity.Subtotal;
            result.Iva = pedidoEntity.Iva;
 result.Total = pedidoEntity.Total;
    result.Activo = pedidoEntity.Activo;
        result.Cerrado = pedidoEntity.Cerrado;
         result.EsPresupuesto = pedidoEntity.EsPresupuesto;
            return result;
      }

   public static PedidoEntity ConvertToPedidoEntity(PedidoEntity pedido)
    {
  return new PedidoEntity()
   {
   IdPedido = pedido.IdPedido,
 Fecha = pedido.Fecha,
      IdCliente = pedido.IdCliente,
Subtotal = pedido.Subtotal,
    Iva = pedido.Iva,
     Total = pedido.Total,
                Activo = pedido.Activo,
    Cerrado = pedido.Cerrado,
 EsPresupuesto = pedido.EsPresupuesto
            };
        }

        public static ComprobanteEntity ConvertToComprobante(ComprobanteEntity comprobanteEntity)
        {
            var result = new ComprobanteEntity();
            result.IdComprobante = comprobanteEntity.IdComprobante;
            result.Comprobante = comprobanteEntity.Comprobante;
            result.Activo = comprobanteEntity.Activo;
   return result;
        }

        public static ItemImpuestoEntity ConvertToItemImpuesto(ItemImpuestoEntity itemImpuestoEntity)
        {
  var result = new ItemImpuestoEntity();
            result.IdItem = itemImpuestoEntity.IdItem;
    result.IdImpuesto = itemImpuestoEntity.IdImpuesto;
       result.Activo = itemImpuestoEntity.Activo;
        return result;
     }

    }
}
