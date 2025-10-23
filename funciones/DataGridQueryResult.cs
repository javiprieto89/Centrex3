using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Centrex
{
    /// <summary>
    /// Resultado de una consulta para cargar en DataGridView
    /// </summary>
    public class DataGridQueryResult
    {
        public IQueryable Query { get; set; }
        public bool MostrarSoloActivos { get; set; } = true;
        public List<string> ColumnasOcultar { get; set; } = new List<string>();
        public Dictionary<string, Color> ColoresColumnas { get; set; } = new Dictionary<string, Color>();
        public bool TieneCheckbox { get; set; } = false;
        public string NombreColumnCheckbox { get; set; } = "Seleccionar";
        public int PosicionColumnCheckbox { get; set; } = 0;
        public List<string> OrdenarPor { get; set; } = new List<string>();
        public bool OrdenAscendente { get; set; } = true;
        public bool EsMaterializada { get; set; } = false; // Para queries que necesitan ToList() antes
        public object DataMaterializada { get; set; } // Para queries ya materializadas
        public string GridName { get; set; } // Para configuraciones específicas por nombre de grid
    }

    /// <summary>
    /// Factory para crear queries dinámicas según la tabla solicitada
    /// MIGRADO DESDE SQL A EF CORE - Mantiene todas las relaciones del código original
    /// </summary>
    public static class DataGridQueryFactory
    {
        // Helpers para formateos que SQL hacía con CASE/CONCAT/REPLICATE
        private static string YesNo(bool value) => value ? "Si" : "No";

        private static string ComprobanteAfipFmt(string tipoAfip, int pv, int numero)
            => $"{tipoAfip} Nº {pv.ToString().PadLeft(4, '0')}-{numero.ToString().PadLeft(8, '0')}";

        private static string DepositadoEn(string? cuentaNombre, string? bancoNombre)
            => string.IsNullOrEmpty(cuentaNombre) ? "No" : $"Si, en: {cuentaNombre} - {bancoNombre}";

        public static DataGridQueryResult GetQueryForTable(
            CentrexDbContext ctx,
            string tabla,
            bool historicoActivo = true,
            int idBanco = -1,
            bool activo = true)
        {
            var result = new DataGridQueryResult();
            tabla = tabla?.Trim().ToLower();

            switch (tabla)
            {
                // =======================
                // ARCHIVOS (vacío en original)
                // =======================
                case "archivos":
                    result.Query = null;
                    break;

                // =======================
                // CONDICIONES COMPRA
                // =======================
                case "condiciones_compra":
                    result.Query = ctx.CondicionCompraEntity
                        .Where(c => c.Activo == historicoActivo)
                        .OrderBy(c => c.Condicion)
                        .Select(c => new
                        {
                            ID = c.IdCondicionCompra,
                            Condición = c.Condicion,
                            Vencimiento = c.Vencimiento,
                            Recargo = c.Recargo,
                            Activo = c.Activo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // CONCEPTOS COMPRA
                // =======================
                case "conceptos_compra":
                    result.Query = ctx.ConceptoCompraEntity
                        .Where(c => c.Activo == historicoActivo)
                        .OrderBy(c => c.Concepto)
                        .Select(c => new
                        {
                            ID = c.IdConceptoCompra,
                            Concepto = c.Concepto,
                            Activo = c.Activo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // CLIENTES
                // SQL: INNER JOIN provincias AS prof ON c.id_provincia_fiscal = prof.id_provincia
                //      INNER JOIN paises AS pf ON prof.id_pais = pf.id_pais
                //      INNER JOIN provincias AS proe ON c.id_provincia_entrega = proe.id_provincia
                //      INNER JOIN paises AS pe ON proe.id_pais = pe.id_pais
                //      INNER JOIN tipos_documentos AS td ON c.id_tipoDocumento = td.id_tipoDocumento
                // =======================
                case "clientes":
                    result.Query = ctx.ClienteEntity
                        .Include(c => c.IdTipoDocumentoNavigation)
                        .Include(c => c.IdProvinciaEntregaNavigation).ThenInclude(pe => pe.IdPaisNavigation)
                        .Include(c => c.IdProvinciaFiscalNavigation).ThenInclude(pf => pf.IdPaisNavigation)
                        .Where(c => c.Activo == historicoActivo)
                        .OrderBy(c => c.RazonSocial)
                        .Select(c => new
                        {
                            ID = c.IdCliente,
                            RazonSocial = c.RazonSocial,
                            NombreFantasia = c.NombreFantasia,
                            DireccionEntrega = c.DireccionEntrega,
                            Localidad = c.LocalidadEntrega,
                            Provincia = c.IdProvinciaEntregaNavigation.Provincia,
                            Telefono = c.Telefono,
                            Email = c.Email,
                            Contacto = c.Contacto,
                            Celular = c.Celular,
                            CUIT = c.TaxNumber,
                            TipoDoc = c.IdTipoDocumentoNavigation.Documento,
                            ProvinciaFiscal = c.IdProvinciaFiscalNavigation.Provincia,
                            DireccionFiscal = c.DireccionFiscal,
                            LocalidadFiscal = c.LocalidadFiscal,
                            CPFiscal = c.CpFiscal,
                            CPEntrega = c.CpEntrega,
                            Inscripto = c.EsInscripto,
                            Activo = c.Activo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ARCHIVO CC CLIENTES
                // SQL: INNER JOIN clientes AS c ON ccc.id_cliente = c.id_cliente
                //      INNER JOIN sysMoneda AS sm ON ccc.id_moneda = sm.id_moneda
                // =======================
                case "archivoccclientes":
                    result.Query = ctx.CcClienteEntity
                        .Include(ccc => ccc.IdClienteNavigation)
                        .Include(ccc => ccc.IdMonedaNavigation)
                        .OrderBy(ccc => ccc.IdClienteNavigation.RazonSocial).ThenBy(ccc => ccc.Nombre)
                        .Select(ccc => new
                        {
                            ID = ccc.IdCc,
                            Cliente = ccc.IdClienteNavigation.RazonSocial,
                            Nombre = ccc.Nombre,
                            Moneda = ccc.IdMonedaNavigation.Moneda,
                            Saldo = ccc.Saldo,
                            CCActiva = ccc.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // PROVEEDORES
                // SQL: INNER JOIN provincias, paises, tipos_documentos (igual que clientes)
                // =======================
                case "proveedores":
                    result.Query = ctx.ProveedorEntity
                        .Include(p => p.IdTipoDocumentoNavigation)
                        .Include(p => p.IdProvinciaEntregaNavigation).ThenInclude(pe => pe.IdPaisNavigation)
                        .Include(p => p.IdProvinciaFiscalNavigation).ThenInclude(pf => pf.IdPaisNavigation)
                        .Where(p => p.Activo == historicoActivo)
                        .OrderBy(p => p.RazonSocial)
                        .Select(p => new
                        {
                            ID = p.IdProveedor,
                            RazonSocial = p.RazonSocial,
                            DireccionEntrega = p.DireccionEntrega,
                            Localidad = p.LocalidadEntrega,
                            Provincia = p.IdProvinciaEntregaNavigation.Provincia,
                            Telefono = p.Telefono,
                            Email = p.Email,
                            Contacto = p.Contacto,
                            Celular = p.Celular,
                            CUIT = p.TaxNumber,
                            TipoDoc = p.IdTipoDocumentoNavigation.Documento,
                            ProvinciaFiscal = p.IdProvinciaFiscalNavigation.Provincia,
                            DireccionFiscal = p.DireccionFiscal,
                            LocalidadFiscal = p.LocalidadFiscal,
                            CPFiscal = p.CpFiscal,
                            CPEntrega = p.CpEntrega,
                            Inscripto = p.EsInscripto,
                            Activo = p.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ARCHIVO CC PROVEEDORES
                // SQL: INNER JOIN proveedores, INNER JOIN sysMoneda
                // =======================
                case "archivoccproveedores":
                    result.Query = ctx.CcProveedorEntity
                        .Include(ccp => ccp.IdProveedorNavigation)
                        .Include(ccp => ccp.IdMonedaNavigation)
                        .OrderBy(ccp => ccp.IdProveedorNavigation.RazonSocial).ThenBy(ccp => ccp.Nombre)
                        .Select(ccp => new
                        {
                            ID = ccp.IdCc,
                            Proveedor = ccp.IdProveedorNavigation.RazonSocial,
                            Nombre = ccp.Nombre,
                            Moneda = ccp.IdMonedaNavigation.Moneda,
                            Saldo = ccp.Saldo,
                            CCActiva = ccp.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // MARCAS ITEMS
                // =======================
                case "marcas_items":
                    result.Query = ctx.MarcaItemEntity
                        .Where(m => m.Activo == historicoActivo)
                        .OrderBy(m => m.Marca)
                        .Select(m => new
                        {
                            ID = m.IdMarca,
                            Marca = m.Marca,
                            Activo = m.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // TIPOS ITEMS
                // =======================
                case "tipos_items":
                    result.Query = ctx.TipoItemEntity
                        .Where(t => t.Activo == historicoActivo)
                        .OrderBy(t => t.Tipo)
                        .Select(t => new
                        {
                            ID = t.IdTipo,
                            Categoría = t.Tipo,
                            Activo = t.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ITEMS
                // SQL: INNER JOIN tipos_items, INNER JOIN marcas_items, INNER JOIN proveedores
                // =======================
                case "items":
                case "itemsimpuestositems":
                    result.Query = ctx.ItemEntity
                        .Include(i => i.IdTipoNavigation)
                        .Include(i => i.IdMarcaNavigation)
                        .Include(i => i.IdProveedorNavigation)
                        .Where(i => i.Activo == historicoActivo)
                        .OrderBy(i => i.Item)
                        .ThenBy(i => i.Descript)
                        .Select(i => new
                        {
                            ID = i.IdItem,
                            Código = i.Item,
                            Producto = i.Descript,
                            PrecioDeLista = i.PrecioLista,
                            Factor = i.Factor,
                            Costo = i.Costo,
                            Categoría = i.IdTipoNavigation.Tipo,
                            Marca = i.IdMarcaNavigation.Marca,
                            Proveedor = i.IdProveedorNavigation.RazonSocial,
                            Descuento = i.EsDescuento,
                            Markup = i.EsMarkup,
                            Activo = i.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ASOC ITEMS
                // SQL: INNER JOIN items AS i (item principal), INNER JOIN items AS ii (item asociado)
                // =======================
                case "asocitems":
                    result.Query = ctx.AsocItemEntity
                        .Include(ai => ai.IdItemNavigation)
                        .Include(ai => ai.IdItemAsocNavigation)
                        .Where(ai => ai.IdItemNavigation.Activo == historicoActivo)
                        .OrderBy(ai => ai.IdItemNavigation.Item)
                        .Select(ai => new
                        {
                            ID = ai.IdItem.ToString() + "-" + ai.IdItemAsoc.ToString(),
                            Item = ai.IdItemNavigation.Item + "-" + ai.IdItemAsocNavigation.Item,
                            Producto = ai.IdItemNavigation.Descript,
                            ProductoAsociado = ai.IdItemAsocNavigation.Descript,
                            Cantidad = ai.Cantidad
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ITEMS SIN DESCUENTO
                // SQL: Mismo que items pero con filtro esDescuento = '0' AND esMarkup = '0'
                // =======================
                case "items_sindescuento":                
                    result.Query = ctx.ItemEntity
                        .Include(i => i.IdTipoNavigation)
                        .Include(i => i.IdMarcaNavigation)
                        .Include(i => i.IdProveedorNavigation)
                        .Where(i => i.Activo == historicoActivo && !i.EsDescuento && !i.EsMarkup)
                        .OrderBy(i => i.Item)
                        .ThenBy(i => i.Descript)
                        .Select(i => new
                        {
                            ID = i.IdItem,
                            Código = i.Item,
                            Producto = i.Descript,
                            PrecioDeLista = i.PrecioLista,
                            Factor = i.Factor,
                            Costo = i.Costo,
                            Categoría = i.IdTipoNavigation.Tipo,
                            Marca = i.IdMarcaNavigation.Marca,
                            Proveedor = i.IdProveedorNavigation.RazonSocial,
                            Descuento = i.EsDescuento,
                            Markup = i.EsMarkup,
                            Activo = i.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ITEMS DESCUENTOS
                // SQL: Filtro esDescuento = '1' AND esMarkup = '0', ORDER BY factor
                // =======================
                case "itemsdescuentos":
                    result.Query = ctx.ItemEntity
                        .Include(i => i.IdTipoNavigation)
                        .Include(i => i.IdMarcaNavigation)
                        .Include(i => i.IdProveedorNavigation)
                        .Where(i => i.Activo == historicoActivo && i.EsDescuento && !i.EsMarkup)
                        .OrderBy(i => i.Factor)
                        .Select(i => new
                        {
                            ID = i.IdItem,
                            Código = i.Item,
                            Producto = i.Descript,
                            PrecioDeLista = i.PrecioLista,
                            Factor = i.Factor,
                            Costo = i.Costo,
                            Categoría = i.IdTipoNavigation.Tipo,
                            Marca = i.IdMarcaNavigation.Marca,
                            Proveedor = i.IdProveedorNavigation.RazonSocial,
                            Descuento = i.EsDescuento,
                            Markup = i.EsMarkup,
                            Activo = i.Activo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ITEMS REGISTROS STOCK / ITEMS RECIBIDOS
                // SQL: Similar a items pero sin filtro de esDescuento/esMarkup, incluye cantidad
                // =======================
                case "items_registros_stock":
                case "items_recibidos":
                    result.Query = ctx.ItemEntity
                        .Include(i => i.IdTipoNavigation)
                        .Include(i => i.IdMarcaNavigation)
                        .Include(i => i.IdProveedorNavigation)
                        .OrderBy(i => i.Item)
                        .Select(i => new
                        {
                            ID = i.IdItem,
                            Código = i.Item,
                            Producto = i.Descript,
                            Cantidad = i.Cantidad,
                            PrecioDeLista = i.PrecioLista,
                            Costo = i.Costo,
                            Categoría = i.IdTipoNavigation.Tipo,
                            Marca = i.IdMarcaNavigation.Marca,
                            Proveedor = i.IdProveedorNavigation.RazonSocial,
                            Activo = i.Activo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // COMPROBANTES
                // SQL: INNER JOIN tipos_comprobantes, CASE para formato
                // =======================
                case "comprobantes":
                    result.Query = ctx.ComprobanteEntity
                        .Include(c => c.IdTipoComprobanteNavigation)
                        .Where(c => c.Activo == historicoActivo)
                        .OrderBy(c => c.Comprobante)
                        .Select(c => new
                        {
                            ID = c.IdComprobante,
                            Comprobante = c.Comprobante,
                            TipoComprobante = c.IdTipoComprobanteNavigation.ComprobanteAfip,
                            NumeroComprobante = c.NumeroComprobante,
                            PuntoVenta = c.PuntoVenta,
                            FormatoComprobante = (bool)c.EsFiscal ? "Fiscal" : (bool)c.EsElectronica ? "Electrónico" : "Manual",
                            ComprobanteTesteo = c.Testing ? "Si" : "No",
                            Activo = c.Activo ? "Si" : "No",
                            MaximoItems = c.MaxItems,
                            Contabilizar = c.Contabilizar ? "Si" : "No",
                            MueveStock = c.MueveStock ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ARCHIVO CONSULTAS
                // =======================
                case "archivoconsultas":
                    result.Query = ctx.ConsultaPersonalizadaEntity
                        .Where(c => c.Activo == historicoActivo)
                        .OrderBy(c => c.Nombre)
                        .Select(c => new
                        {
                            ID = c.IdConsulta,
                            Nombre = c.Nombre,
                            ConsultaSQL = c.Consulta,
                            Activo = c.Activo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // CAJA
                // =======================
                case "caja":
                    result.Query = ctx.CajaEntity
                        .Where(c => c.Activo == activo)
                        .OrderBy(c => c.Nombre)
                        .Select(c => new
                        {
                            ID = c.IdCaja,
                            Caja = c.Nombre,
                            EsCuentaCorriente = c.EsCc ? "Si" : "No",
                            Activo = c.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // BANCOS
                // SQL: INNER JOIN paises
                // =======================
                case "bancos":
                    result.Query = ctx.BancoEntity
                        .Include(b => b.IdPaisNavigation)
                        .Where(b => b.Activo == historicoActivo)
                        .OrderBy(b => b.Nombre)
                        .Select(b => new
                        {
                            ID = b.IdBanco,
                            Banco = b.Nombre,
                            País = b.IdPaisNavigation.Pais,
                            BancoNumero = b.NBanco,
                            Activo = b.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // CUENTAS BANCARIAS
                // SQL: INNER JOIN bancos, INNER JOIN sysMoneda
                // =======================
                case "cuentas_bancarias":
                    result.Query = ctx.CuentaBancariaEntity
                        .Include(cb => cb.IdBancoNavigation)
                        .Include(cb => cb.IdMonedaNavigation)
                        .Where(cb => cb.Activo == historicoActivo && (idBanco == -1 || cb.IdBanco == idBanco))
                        .OrderBy(cb => cb.IdBancoNavigation.Nombre)
                        .ThenBy(cb => cb.Nombre)
                        .Select(cb => new
                        {
                            ID = cb.IdCuentaBancaria,
                            Banco = cb.IdBancoNavigation.Nombre,
                            CuentaBancaria = cb.Nombre,
                            Moneda = cb.IdMonedaNavigation.Moneda,
                            Saldo = cb.Saldo,
                            Activo = cb.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // CHEQUES RECIBIDOS
                // SQL: INNER JOIN clientes, INNER JOIN bancos, LEFT JOIN cuentas_bancarias,
                //      LEFT JOIN bancos (para cuenta), INNER JOIN sysestados_cheques
                // =======================
                case "chrecibidos":
                    {
                        var query = ctx.ChequeEntity
                            .Include(ch => ch.IdClienteNavigation)
                            .Include(ch => ch.IdBancoNavigation)
                            .Include(ch => ch.IdCuentaBancariaNavigation).ThenInclude(cb => cb.IdBancoNavigation)
                            .Include(ch => ch.IdEstadochNavigation)
                            .Where(ch => (bool)ch.Activo && (bool)ch.Recibido)
                            .OrderBy(ch => ch.IdCheque)
                            .Select(ch => new
                            {
                                ID = ch.IdCheque,
                                Cliente = ch.IdClienteNavigation.RazonSocial,
                                Banco = ch.IdBancoNavigation.Nombre,
                                NumeroCheque = ch.NCheque,
                                Importe = ch.Importe,
                                Estado = ch.IdEstadochNavigation.Estado,
                                Depositado = ch.IdCuentaBancariaNavigation == null ? "No" : "Si, en: " + ch.IdCuentaBancariaNavigation.IdBancoNavigation.Nombre + " - " + ch.IdCuentaBancariaNavigation.Nombre,                                
                                Activo = ch.Activo ? "Si" : "No"
                            });

                        result.Query = query;
                        result.EsMaterializada = true;
                        result.ColumnasOcultar.Add("ID");
                        result.ColumnasOcultar.Add("CuentaBancariaNombre");
                        result.ColumnasOcultar.Add("BancoCuentaNombre");
                        break;
                    }

                // =======================
                // CHEQUES EMITIDOS
                // SQL: INNER JOIN proveedores (en lugar de clientes), resto igual
                // =======================
                case "chemitidos":
                    {
                        var query = ctx.ChequeEntity
                            .Include(ch => ch.IdProveedorNavigation)
                            .Include(ch => ch.IdBancoNavigation)
                            .Include(ch => ch.IdCuentaBancariaNavigation).ThenInclude(cb => cb.IdBancoNavigation)
                            .Include(ch => ch.IdEstadochNavigation)
                            .Where(ch => (bool)ch.Activo && (bool)ch.Emitido)
                            .OrderBy(ch => ch.IdCheque)
                            .Select(ch => new
                            {
                                ID = ch.IdCheque,
                                Proveedor = ch.IdProveedorNavigation.RazonSocial,
                                Banco = ch.IdBancoNavigation.Nombre,
                                NumeroCheque = ch.NCheque,
                                Importe = ch.Importe,
                                Estado = ch.IdEstadochNavigation.Estado,
                                Depositado = ch.IdCuentaBancariaNavigation  == null ? "No" : "Si, en: " + ch.IdCuentaBancariaNavigation.IdBancoNavigation.Nombre + " - " + ch.IdCuentaBancariaNavigation.Nombre,                                
                                Activo = ch.Activo ? "Si" : "No"
                            });

                        result.Query = query;
                        result.EsMaterializada = true;
                        result.ColumnasOcultar.Add("ID");
                        result.ColumnasOcultar.Add("CuentaBancariaNombre");
                        result.ColumnasOcultar.Add("BancoCuentaNombre");
                        break;
                    }

                // =======================
                // CHEQUES CARTERA
                // SQL: LEFT JOIN clientes, LEFT JOIN proveedores, LEFT JOIN bancos, 
                //      LEFT JOIN cuentas_bancarias, LEFT JOIN sysestados_cheques
                // =======================
                case "chcartera":
                  result.Query = ctx.ChequeEntity
                 .Include(ch => ch.IdClienteNavigation)
                 .Include(ch => ch.IdProveedorNavigation)
                 .Include(ch => ch.IdBancoNavigation)
                 .Include(ch => ch.IdCuentaBancariaNavigation)
                 .Include(ch => ch.IdEstadochNavigation)
                 .Where(ch => ch.Activo == historicoActivo)
                 .OrderBy(ch => ch.IdCheque)
                 .Select(ch => new
                 {
                     ID = ch.IdCheque,
                     FechaIngreso = ch.FechaIngreso,
                     FechaEmisión = ch.FechaEmision,
                     RecibidoDe = ch.IdClienteNavigation.RazonSocial,    // EF maneja el null
                     EntregadoA = ch.IdProveedorNavigation.RazonSocial,  // EF maneja el null
                     BancoEmisor = ch.IdBancoNavigation.Nombre,          // EF maneja el null
                     DepositadoEn = ch.IdCuentaBancariaNavigation.Nombre, // EF maneja el null
                     NCheque = ch.NCheque,
                     SegundoNDeCheque = ch.NCheque2,
                     Monto = ch.Importe,
                     Estado = ch.IdEstadochNavigation.Estado,                 // EF maneja el null
                     FechaDeCobro = ch.FechaCobro,
                     FechaDeSalida = ch.FechaSalida,
                     FechaDeDeposito = ch.FechaDeposito
                 });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // IMPUESTOS
                // =======================
                case "impuestos":
                case "itemsimpuestosimpuestos":
                    result.Query = ctx.ImpuestoEntity
                        .Where(i => i.Activo == historicoActivo)
                        .OrderBy(i => i.Nombre)
                        .Select(i => new
                        {
                            ID = i.IdImpuesto,
                            Impuesto = i.Nombre,
                            Porcentaje = i.Porcentaje,
                            Retención = (bool)i.EsRetencion ? "Si" : "No",
                            Percepción = (bool)i.EsPercepcion ? "Si" : "No",
                            Activo = i.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // RETENCIONES
                // SQL: WHERE esRetencion = '1'
                // =======================
                case "retenciones":
                    result.Query = ctx.ImpuestoEntity
                        .Where(i => (bool)i.EsRetencion && (bool)i.Activo == historicoActivo)
                        .OrderBy(i => i.Nombre)
                        .Select(i => new
                        {
                            ID = i.IdImpuesto,
                            Impuesto = i.Nombre,
                            Porcentaje = i.Porcentaje,
                            Retención = (bool)i.EsRetencion ? "Si" : "No",
                            Percepción = (bool)i.EsPercepcion ? "Si" : "No",
                            Activo = i.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ITEM IVA
                // SQL: WHERE nombre LIKE '%iva%'
                // =======================
                case "itemiva":
                    result.Query = ctx.ImpuestoEntity
                        .Where(i => i.Activo == historicoActivo && i.Nombre.Contains("iva", StringComparison.CurrentCultureIgnoreCase))
                        .OrderBy(i => i.Nombre)
                        .Select(i => new
                        {
                            ID = i.IdImpuesto,
                            Impuesto = i.Nombre,
                            Porcentaje = i.Porcentaje,
                            Activo = i.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ITEMS IMPUESTOS
                // SQL: INNER JOIN items, INNER JOIN impuestos
                // =======================
                case "itemsimpuestos":
                    result.Query = ctx.ItemImpuestoEntity
                        .Include(ii => ii.IdItemNavigation)
                        .Include(ii => ii.IdImpuestoNavigation)
                        .Where(ii => ii.Activo == activo)
                        .OrderBy(ii => ii.IdImpuestoNavigation.Nombre).ThenBy(ii => ii.IdItemNavigation.Descript)
                        .Select(ii => new
                        {
                            ID = ii.IdItem.ToString() + "-" + ii.IdImpuesto.ToString(),
                            Item = ii.IdItemNavigation.Descript,
                            Impuesto = ii.IdImpuestoNavigation.Nombre,
                            Activo = ii.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // ORDENES COMPRAS
                // SQL: INNER JOIN proveedores
                // =======================
                case "ordenescompras":
                    result.Query = ctx.OrdenCompraEntity
                        .Include(oc => oc.IdProveedorNavigation)
                        .Where(oc => oc.Activo == historicoActivo)
                        .OrderBy(oc => oc.IdOrdenCompra)
                        .Select(oc => new
                        {
                            ID = oc.IdOrdenCompra,
                            Proveedor = oc.IdProveedorNavigation.RazonSocial,
                            FechaCarga = oc.FechaCarga,
                            FechaComprobante = oc.FechaComprobante,
                            FechaRecepcion = oc.FechaRecepcion,
                            Importe = oc.Total,
                            Activo = oc.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // COMPROBANTES COMPRAS
                // SQL: INNER JOIN tipos_comprobantes, proveedores, cc_proveedores, sysMoneda, condiciones_compra
                // =======================
                case "comprobantes_compras":
                    result.Query = ctx.ComprobanteCompraEntity
                        .Include(cc => cc.IdTipoComprobanteNavigation)
                        .Include(cc => cc.IdProveedorNavigation)
                        .Include(cc => cc.IdCcProveedorNavigation)
                        .Include(cc => cc.IdMonedaNavigation)
                        .Include(cc => cc.IdCondicionCompraNavigation)
                        .Where(cc => !cc.Activo)
                        .OrderBy(cc => cc.FechaComprobante)
                        .Select(cc => new
                        {
                            ID = cc.IdComprobanteCompra,
                            FechaCarga = cc.FechaCarga,
                            FechaComprobante = cc.FechaComprobante,
                            Proveedor = cc.IdProveedorNavigation.RazonSocial,
                            Comprobante = cc.IdTipoComprobanteNavigation.ComprobanteAfip + " Nº " +
                                         cc.PuntoVenta.ToString().PadLeft(4, '0') + "-" +
                                         cc.NumeroComprobante.ToString().PadLeft(8, '0'),
                            Moneda = cc.IdMonedaNavigation.Moneda,
                            TasaCambio = cc.TasaCambio,
                            CondicionCompra = cc.IdCondicionCompraNavigation.Condicion,
                            Subtotal = cc.Subtotal,
                            Impuestos = cc.Impuestos,
                            Conceptos = cc.Conceptos,
                            Total = cc.Total,
                            CAE = cc.Cae
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // PAGOS
                // SQL: INNER JOIN proveedores, cc_proveedores, LEFT JOIN pagos_cheques
                // =======================
                case "pagos":
                    result.Query = ctx.PagoEntity
                        .Include(pg => pg.IdProveedorNavigation)
                        .Include(pg => pg.IdCcProveedorNavigation)
                        .OrderBy(pg => pg.FechaPago)
                        .Select(pg => new
                        {
                            ID = pg.IdPago,
                            Recibo = pg.Total > 0 ? $"OP.{pg.IdPago.ToString().PadLeft(6, '0')}" : $"OP. AN.{pg.IdPago.ToString().PadLeft(6, '0')}",
                            FechaCarga = pg.FechaCarga,
                            FechaPago = pg.FechaPago,
                            Proveedor = pg.IdProveedorNavigation.RazonSocial,
                            CC = pg.IdCcProveedorNavigation.Nombre,
                            Efectivo = pg.Efectivo,
                            TransferenciasBancarias = pg.TotalTransferencia,
                            TotalCheques = pg.TotalCh,
                            TotalPago = pg.Total
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // REGISTROS STOCK
                // SQL: INNER JOIN items, INNER JOIN proveedores
                //      Filtro especial: Si activo, WHERE fecha_ingreso = hoy
                // =======================
                case "registros_stock":
                    if (activo)
                    {
                        var today = DateOnly.FromDateTime(DateTime.Today);
                        result.Query = ctx.RegistroStockEntity
                            .Include(rs => rs.IdItemNavigation)
                            .Include(rs => rs.IdProveedorNavigation)
                            .Where(rs => rs.Activo && rs.FechaIngreso == today)
                            .OrderBy(rs => rs.FechaIngreso).ThenBy(rs => rs.IdRegistro)
                            .Select(rs => new
                            {
                                ID = rs.IdRegistro,
                                FechaFC = rs.Fecha,
                                FechaIngreso = rs.FechaIngreso,
                                Código = rs.IdItemNavigation.Item,
                                Producto = rs.IdItemNavigation.Descript,
                                Cantidad = rs.Cantidad,
                                CantidadAnterior = rs.CantidadAnterior,
                                Precio = rs.PrecioLista,
                                PrecioAnterior = rs.PrecioListaAnterior,
                                Factor = rs.Factor,
                                FactorAnterior = rs.FactorAnterior,
                                Costo = rs.Costo,
                                CostoAnterior = rs.CostoAnterior,
                                Proveedor = rs.IdProveedorNavigation.RazonSocial,
                                Factura = rs.Factura,
                                Notas = rs.Nota,
                                Activo = rs.Activo
                            });
                    }
                    else
                    {
                        result.Query = ctx.RegistroStockEntity
                            .Include(rs => rs.IdItemNavigation)
                            .Include(rs => rs.IdProveedorNavigation)
                            .Where(rs => !rs.Activo)
                            .OrderBy(rs => rs.FechaIngreso).ThenBy(rs => rs.IdRegistro)
                            .Select(rs => new
                            {
                                ID = rs.IdRegistro,
                                FechaFC = rs.Fecha,
                                FechaIngreso = rs.FechaIngreso,
                                Código = rs.IdItemNavigation.Item,
                                Producto = rs.IdItemNavigation.Descript,
                                Cantidad = rs.Cantidad,
                                CantidadAnterior = rs.CantidadAnterior,
                                Precio = rs.PrecioLista,
                                PrecioAnterior = rs.PrecioListaAnterior,
                                Factor = rs.Factor,
                                FactorAnterior = rs.FactorAnterior,
                                Costo = rs.Costo,
                                CostoAnterior = rs.CostoAnterior,
                                Proveedor = rs.IdProveedorNavigation.RazonSocial,
                                Factura = rs.Factura,
                                Notas = rs.Nota,
                                Activo = rs.Activo
                            });
                    }
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // AJUSTES STOCK
                // SQL: INNER JOIN items
                //      Filtro especial: Si activo, WHERE fecha = hoy
                // =======================
                case "ajustes_stock":
                    if (activo)
                    {
                        var today = DateOnly.FromDateTime(DateTime.Today);
                        result.Query = ctx.AjusteStockEntity
                            .Include(a => a.IdItemNavigation)
                            .Where(a => a.Fecha == today)
                            .OrderBy(a => a.IdAjusteStock)
                            .Select(a => new
                            {
                                ID = a.IdAjusteStock,
                                Fecha = a.Fecha,
                                Código = a.IdItemNavigation.Item,
                                Producto = a.IdItemNavigation.Descript,
                                Cantidad = a.Cantidad,
                                Notas = a.Notas
                            });
                    }
                    else
                    {
                        result.Query = ctx.AjusteStockEntity
                            .Include(a => a.IdItemNavigation)
                            .OrderBy(a => a.IdAjusteStock)
                            .Select(a => new
                            {
                                ID = a.IdAjusteStock,
                                Fecha = a.Fecha,
                                Código = a.IdItemNavigation.Item,
                                Producto = a.IdItemNavigation.Descript,
                                Cantidad = a.Cantidad,
                                Notas = a.Notas
                            });
                    }
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // PRODUCCIÓN
                // SQL: INNER JOIN proveedores
                // =======================
                case "produccion":
                    result.Query = ctx.ProduccionEntity
                        .Include(p => p.IdProveedorNavigation)
                        .Where(p => p.Activo == historicoActivo)
                        .OrderBy(p => p.IdProduccion)
                        .Select(p => new
                        {
                            ID = p.IdProduccion,
                            Proveedor = p.IdProveedorNavigation.RazonSocial,
                            FechaCarga = p.FechaCarga,
                            FechaEnvio = p.FechaEnvio,
                            FechaRecepcion = p.FechaRecepcion,
                            MercaderiaEnviada = (bool) p.Enviado ? "SI" : "NO",
                            MercaderiaRecibida = (bool) p.Recibido ? "SI" : "NO"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // PEDIDOS
                // SQL: INNER JOIN clientes, INNER JOIN comprobantes
                //      Si activo: WHERE cerrado = '0' AND activo = '1'
                //      Si no: WHERE cerrado = '1' AND activo = '0'
                // =======================
                case "pedidos":
                    if (activo)
                    {
                        result.Query = ctx.PedidoEntity
                            .Include(p => p.IdClienteNavigation)
                            .Include(p => p.IdComprobanteNavigation)
                            .Where(p => !p.Cerrado && p.Activo)
                            .OrderByDescending(p => p.IdPedido)
                            .Select(p => new
                            {
                                ID = p.IdPedido,
                                Fecha = p.Fecha,
                                RazónSocial = p.IdClienteNavigation.RazonSocial,
                                Comprobante = p.IdComprobanteNavigation.Comprobante,
                                Total = p.Total,
                                Activo = p.Activo
                            });
                    }
                    else
                    {
                        result.Query = ctx.PedidoEntity
                            .Include(p => p.IdClienteNavigation)
                            .Include(p => p.IdComprobanteNavigation)
                            .Where(p => p.Cerrado && !p.Activo)
                            .OrderByDescending(p => p.FechaEdicion).ThenByDescending(p => p.IdPedido)
                            .Select(p => new
                            {
                                ID = p.IdPedido,
                                Fecha = p.FechaEdicion,
                                RazónSocial = p.IdClienteNavigation.RazonSocial,
                                Comprobante = p.IdComprobanteNavigation.Comprobante,
                                NumeroComprobante = p.IdComprobanteNavigation.IdTipoComprobante == 99 ? p.IdPresupuesto : p.NumeroComprobante,
                                Total = p.Total,
                                Activo = p.Activo
                            });
                    }
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // COBROS
                // SQL: INNER JOIN clientes, INNER JOIN cc_clientes
                //      Usa función dbo.CalculoComprobante (simulado aquí)
                // =======================
                case "cobros":
                    result.Query = ctx.CobroEntity
                        .Include(c => c.IdClienteNavigation)
                        .Where(c => c.Activo == historicoActivo)
                        .OrderBy(c => c.FechaCobro)
                        .Select(c => new
                        {
                            ID = c.IdCobro,
                            Cobro = c.Total > 0
                                ? (c.IdCobroNoOficial == -1
                                    ? CentrexDbContext.CalculoComprobante("RC", 1, c.IdCobroOficial)
                                    : CentrexDbContext.CalculoComprobante("RC", 1, c.IdCobroNoOficial))
                                : (c.IdCobroNoOficial == -1
                                    ? CentrexDbContext.CalculoComprobante("AN. RC.", 1, c.IdCobroOficial)
                                    : CentrexDbContext.CalculoComprobante("AN. RC.", 1, c.IdCobroNoOficial)),
                            FechaCarga = c.FechaCarga,
                            FechaCobro = c.FechaCobro,
                            Cliente = c.IdClienteNavigation.RazonSocial,
                            Efectivo = c.Efectivo,
                            Transferencia = c.TotalTransferencia,
                            TotalCheques = c.TotalCh,
                            Retenciones = c.TotalRetencion,
                            Total = c.Total
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // STOCK
                // SQL: WHERE esMarkup = 'FALSE' AND esDescuento = 'FALSE' AND activo = 'TRUE'
                // =======================
                case "stock":
                    result.Query = ctx.ItemEntity
                        .Where(i => !i.EsMarkup && !i.EsDescuento && i.Activo)
                        .OrderBy(i => i.Item).ThenBy(i => i.Descript)
                        .Select(i => new
                        {
                            ID = i.IdItem,
                            Producto = i.Item,
                            Descripción = i.Descript,
                            Cantidad = i.Cantidad,
                            Precio = i.PrecioLista,
                            Costo = i.Costo
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // TIPOS COMPROBANTES
                // =======================
                case "tipos_comprobantes":
                    result.Query = ctx.TipoComprobanteEntity
                        .OrderBy(tc => tc.ComprobanteAfip)
                        .Select(tc => new
                        {
                            ID = tc.IdTipoComprobante,
                            TipoComprobante = tc.ComprobanteAfip
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // PERMISOS
                // =======================
                case "permisos":
                    result.Query = ctx.PermisoEntity
                        .OrderBy(p => p.Nombre)
                        .Select(p => new
                        {
                            ID = p.IdPermiso,
                            Permiso = p.Nombre
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // PERFILES
                // =======================
                case "perfiles":
                    result.Query = ctx.PerfilEntity
                        .OrderBy(p => p.Nombre)
                        .Select(p => new
                        {
                            ID = p.IdPerfil,
                            Perfil = p.Nombre,
                            Activo = p.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // USUARIOS
                // =======================
                case "usuarios":
                    result.Query = ctx.UsuarioEntity
                        .OrderBy(u => u.Nombre)
                        .Select(u => new
                        {
                            ID = u.IdUsuario,
                            Usuario = u.Usuario,
                            Nombre = u.Nombre,
                            Activo = u.Activo ? "Si" : "No"
                        });
                    result.ColumnasOcultar.Add("ID");
                    break;

                // =======================
                // FORMULARIOS ESPECIALES (Abren ventanas)
                // =======================
                case "nuevopedido":
                    {
                        var form = new add_pedido();
                        form.ShowDialog();
                        result.Query = null;
                        break;
                    }

                case "ccclientes":
                    {
                        var form = new infoccClientes();
                        form.ShowDialog();
                        result.Query = null;
                        break;
                    }

                case "cpersonalizadas":
                    {
                        var form = new grilla_resultados();
                        form.ShowDialog();
                        result.Query = null;
                        break;
                    }

                case "exportsiap":
                    {
                        var form = new frm_exportaSiap();
                        form.ShowDialog();
                        result.Query = null;
                        break;
                    }

                case "configuracion":
                    {
                        var form = new config();
                        form.ShowDialog();
                        result.Query = null;
                        break;
                    }

                case "acercade":
                    {
                        var form = new frmacercade();
                        form.ShowDialog();
                        result.Query = null;
                        break;
                    }

                // =======================
                // DEFAULT (error)
                // =======================
                default:
                    result.Query = null;
                    MessageBox.Show($"La tabla '{tabla}' no está implementada.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

            return result;
        }
    }
}