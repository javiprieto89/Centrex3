using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{

    static class MainModule
    {
        [STAThread()]
        public static void Main()
        {
            // Configurar protocolos TLS (antes de crear formularios)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            // Inicializar entorno gráfico
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Iniciar el formulario principal
            Application.Run(new main());
        }
    }

    public partial class main : Form
    {

        private bool ultimoComprobanteFin;
        private int desde;
        private int pagina;
        private int nRegs;
        private int tPaginas;
        private ColumnClickEventArgs orderCol = null;

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            Visible = false;
            VariablesGlobales.pc = SystemInformation.ComputerName.ToUpper();

            // ******* Configuraciones inciales *******
            var c = new configInit();
            c.leerConfig();

            VariablesGlobales.comprobantePresupuesto_default = 3;
            VariablesGlobales.id_comprobante_default = 3;
            VariablesGlobales.id_tipoDocumento_default = 80;
            VariablesGlobales.id_tipoComprobante_default = 1;
            VariablesGlobales.id_cliente_pedido_default = 2;
            VariablesGlobales.id_pais_default = 1;
            VariablesGlobales.id_provincia_default = 1;
            VariablesGlobales.id_marca_default = 1;
            VariablesGlobales.id_proveedor_default = 1;
            VariablesGlobales.cuit_emisor_default = "20233695255";
            VariablesGlobales.id_condicion_compra_default = 1;
            VariablesGlobales.STR_COMPROBANTES_CONTABLES = "1, 6, 11, 51, 201, 206, 211, 1006, 2, 7, 12, 52, 202, 207, 212, 1007, 1002, 1003, 1004, 1005, 1010, 1015, 3, 8, 13, 53, 203, 208, 213, 1008, 4, 1009";
            VariablesGlobales.versiondb = "1.0.0";

            if (VariablesGlobales.pc != "JARVIS" && VariablesGlobales.pc != "SERVTEC-06" && VariablesGlobales.pc != "CORTANA" && VariablesGlobales.pc != "SKYNET")
            {
                VariablesGlobales.depuracion = false;
                Timer1.Enabled = true;
            }
            else
            {
                VariablesGlobales.depuracion = true;
                chk_test.Visible = true;
                Timer1.Enabled = false;
                VariablesGlobales.serversql = "127.0.0.1";
            }

            if (Debugger.IsAttached)
            {
                VariablesGlobales.depuracion = true;
                Timer1.Enabled = false;
            }

            if (VariablesGlobales.pc == "SILVIA")
            {
                VariablesGlobales.serversql = "192.168.1.100";
            }

            // Carga inicial de scripts DB si existen
            try
            {
                if (VariablesGlobales.pc != "JARVIS" || !VariablesGlobales.depuracion)
                {
                    var di = new DirectoryInfo(@"..\..\ScriptsDB");
                    foreach (var fi in di.GetFiles("*.txt"))
                    {
                        string archivo = fi.FullName;
                        string fileReader = File.ReadAllText(archivo);
                        ejecutarSQL(fileReader);
                        File.Move(archivo, Path.ChangeExtension(archivo, ".jav"));
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al ejecutar procesos de actualización de base de datos: " + Constants.vbCrLf + ex.Message, Constants.vbCritical, "Computron");
            }

            // Control de usuarios inicial
            int cantUsuarios = cantReg(VariablesGlobales.basedb, "SELECT * FROM usuarios");
            if (cantUsuarios == 0)
            {
                while (cantUsuarios <= 0)
                {
                    if (Interaction.MsgBox("No hay usuarios creados. ¿Desea crear uno ahora?", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKCancel), "Centrex") == MsgBoxResult.Cancel)
                        Environment.Exit(0);
                    My.MyProject.Forms.add_usuario.ShowDialog();
                    cantUsuarios = cantReg(VariablesGlobales.basedb, "SELECT * FROM usuarios");
                    if (cantUsuarios == 0)
                    {
                        Interaction.MsgBox("Debe crear un usuario para continuar.", Constants.vbExclamation);
                    }
                    else
                    {
                        Interaction.MsgBox("Reinicie el sistema e inicie sesión con el usuario creado.", Constants.vbInformation);
                        closeandupdate(this);
                    }
                }
            }
            else
            {
                // Logueo del sistema
                if (VariablesGlobales.pc != "JARVIS" && VariablesGlobales.pc != "SERVTEC-06" && !VariablesGlobales.depuracion)
                {
                    My.MyProject.Forms.login.ShowDialog();
                }
                else
                {
                    VariablesGlobales.usuario_logueado = info_usuario("javierp", true);
                }

                borrar_tabla_pedidos_temporales(VariablesGlobales.usuario_logueado.IdUsuario);
                producciones.borrarTmpProduccion(VariablesGlobales.usuario_logueado.IdUsuario);
                generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
                stock.ArchivarIngresoStock();
                Visible = true;
            }

            CheckForIllegalCrossThreadCalls = false;
            generales.borrartbl("tmpcobros_retenciones");

            if (haycambios())
                VariablesGlobales.frmCambios.ShowDialog();

            cmd_add.Enabled = false;
            dg_view.Visible = false;
            txt_search.Enabled = false;
            lbl_borrarbusqueda.Enabled = false;
            chk_historicos.Enabled = false;

            {
                var withBlock = My.MyProject.Application.Info.Version;
                tss_version.Text = "Versión: " + My.MyProject.Application.Info.Version.Major.ToString() + "." + My.MyProject.Application.Info.Version.Minor.ToString() + "." + My.MyProject.Application.Info.Version.Revision.ToString();
                tss_dbInfo.Text = "ServerSQL: " + VariablesGlobales.serversql + " - DB: " + VariablesGlobales.basedb + " - Ver.DB: " + VariablesGlobales.versiondb;
                tss_usuario_logueado.Text = "Usuario logueado: " + VariablesGlobales.usuario_logueado.nombre;
            }
            tss_hora.Text = "Hora: " + Conversions.ToString(DateAndTime.TimeOfDay);

            // Expandir ramas principales del TreeView
            int[] ramas = new int[] { 0, 1, 3, 4 };
            {
                var withBlock1 = Treev;
                withBlock1.SelectedNode = Treev.Nodes[0];
                withBlock1.SelectedNode.Expand();
                foreach (int rama in ramas)
                {
                    withBlock1.SelectedNode = Treev.Nodes[0].Nodes[rama];
                    withBlock1.SelectedNode.Expand();
                }
            }
        }

        // ========================= EVENTOS PRINCIPALES ========================= '

        private void Treev_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            VariablesGlobales.tabla_vieja = VariablesGlobales.tabla;
            VariablesGlobales.tabla = e.Node.Name;
            txt_search.Text = "";
            chk_historicos.Checked = false;

            desde = 0;
            pagina = 1;
            ActualizarDataGrid();

            cmd_add.Enabled = true;
            dg_view.Visible = true;
            txt_search.Enabled = true;
            lbl_borrarbusqueda.Enabled = true;
            chk_historicos.Enabled = true;
        }

        private void ActualizarDataGrid()
        {
            try
            {
                // ========== Inicialización ==========
                dg_view.Visible = true;
                txt_search.Enabled = true;
                lbl_borrarbusqueda.Enabled = true;
                chk_historicos.Enabled = true;
                cmd_add.Enabled = true;
                pic.Visible = false;

                string filtro = txt_search.Text.Trim();
                object data = null;

                using (var ctx = new CentrexDbContext())
                {
                    // ========== CASOS ESPECIALES ==========
                    switch (VariablesGlobales.tabla ?? "")
                    {
                        case "root":
                            {
                                cmd_add.Enabled = false;
                                dg_view.Visible = false;
                                txt_search.Enabled = false;
                                lbl_borrarbusqueda.Enabled = false;
                                chk_historicos.Enabled = false;
                                pic.Visible = true;
                                return;
                            }

                        case "depositarCH":
                            {
                                var frmDep = new frm_depositarCH();
                                frmDep.ShowDialog();
                                return;
                            }

                        case "rechazarCH":
                            {
                                var frmRech = new frm_rechazarCH();
                                frmRech.ShowDialog();
                                return;
                            }

                        case "ccProveedores":
                            {
                                var frmCCP = new infoccProveedores();
                                frmCCP.ShowDialog();
                                return;
                            }

                        case "ccClientes":
                            {
                                var frmCCC = new infoccClientes();
                                frmCCC.ShowDialog();
                                return;
                            }

                        case "ultimoComprobante":
                            {
                                var frmUltimo = new frm_ultimo_comprobante();
                                frmUltimo.ShowDialog();
                                return;
                            }

                        case "info_fc":
                            {
                                var frmInfo = new info_fc();
                                frmInfo.ShowDialog();
                                return;
                            }
                    }

                    // ========== TABLAS PRINCIPALES ==========
                    switch (VariablesGlobales.tabla ?? "")
                    {
                        case "clientes":
                            {
                                data = ctx.Clientes.Where(c => c.activo == true && string.IsNullOrEmpty(filtro) | c.RazonSocial.Contains(filtro)).Select(c => new
                                {
                                    ID = c.IdCliente,
                                    c.RazonSocial,
                                    Telefono = c.telefono,
                                    Email = c.email,
                                    Activo = c.activo
                                }).ToList();
                                break;
                            }

                        case "proveedores":
                            {
                                data = ctx.Proveedores.Where(p => p.activo == true && filtro == "" | p.razon_social.Contains(filtro)).Select(p => new
                                {
                                    ID = p.IdProveedor,
                                    RazonSocial = p.razon_social,
                                    Telefono = p.telefono,
                                    Email = p.email,
                                    Activo = p.activo
                                }).ToList();
                                break;
                            }

                        case "items":
                            {
                                data = ctx.Items.Include(i => i.Marca).Where(i => i.activo && filtro == "" | i.descript.Contains(filtro)).Select(i => new
                                {
                                    ID = i.IdItem,
                                    Item = i.item,
                                    Descript = i.descript,
                                    Cantidad = i.cantidad,
                                    Costo = i.costo,
                                    PrecioLista = i.precio_lista,
                                    i.Tipo.Tipo,
                                    Marca = i.Marca.marca,
                                    Proveedor = i.Proveedor.nombre,
                                    Factor = i.factor,
                                    i.esDescuento,
                                    i.esMarkup,
                                    Activo = i.activo
                                }).ToList();
                                break;
                            }

                        case "cobros":
                            {
                                data = ctx.Cobros.Include(c => c.Cliente).Select(c => new
                                {
                                    ID = c.IdCobro,
                                    Fecha = c.FechaCobro,
                                    Cliente = c.Cliente.RazonSocial,
                                    Total = c.total,
                                    Notas = c.notas
                                }).ToList();
                                break;
                            }

                        case "pedidos":
                            {
                                data = ctx.Pedidos.Include(p => p.Cliente).Select(p => new
                                {
                                    ID = p.IdPedido,
                                    Fecha = p.FechaEdicion,
                                    Cliente = p.Cliente.RazonSocial,
                                    Total = p.total,
                                    Activo = p.activo
                                }).ToList();
                                break;
                            }

                        case "bancos":
                            {
                                data = ctx.Bancos.Select(b => new
                                {
                                    ID = b.IdBanco,
                                    Nombre = b.nombre,
                                    Activo = b.activo
                                }).ToList();
                                break;
                            }

                        default:
                            {
                                data = new List<object>(); // Devuelve una lista vacía
                                break;
                            }
                    }

                    // ========== ASIGNAR FUENTE ==========
                    dg_view.DataSource = data;

                    // ========== FORMATO ESPECÍFICO ==========
                    if (VariablesGlobales.tabla == "archivoConsultas")
                    {
                        dg_view.Columns[0].Width = 50;
                    }
                    else if (VariablesGlobales.tabla == "cobros")
                    {
                        foreach (DataGridViewRow row in dg_view.Rows)
                        {
                            var totalCell = row.Cells["Total"].Value;
                            if (totalCell is not null && totalCell.ToString().Contains("-"))
                            {
                                row.Cells["Total"].Style.BackColor = Color.Red;
                            }
                        }
                    }

                    // ========== PAGINACIÓN (opcional futuro) ==========
                    // lbl_totalRegistros.Text = $"{dg_view.Rows.Count} registros encontrados."
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al actualizar la grilla: " + ex.Message, Constants.vbCritical, "Centrex");
            }
        }

        // ========================= PAGINACIÓN ========================= '

        private void cmd_next_Click(object sender, EventArgs e)
        {
            if (pagina >= Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage))
                return;
            desde += VariablesGlobales.itXPage;
            pagina += 1;
            ActualizarDataGrid();
        }

        private void cmd_prev_Click(object sender, EventArgs e)
        {
            if (pagina <= 1)
                return;
            desde -= VariablesGlobales.itXPage;
            pagina -= 1;
            ActualizarDataGrid();
        }

        private void cmd_first_Click(object sender, EventArgs e)
        {
            desde = 0;
            pagina = 1;
            ActualizarDataGrid();
        }

        private void cmd_last_Click(object sender, EventArgs e)
        {
            pagina = tPaginas;
            desde = nRegs - VariablesGlobales.itXPage;
            ActualizarDataGrid();
        }

        // ========================= ANULACIONES ========================= '

        private void AnularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int seleccionado = Conversions.ToInteger(dg_view.CurrentRow.Cells[0].Value);
            using (CentrexDbContext context = GetDbContext())
            {
                switch (VariablesGlobales.tabla ?? "")
                {
                    case "pagos":
                        {
                            var p = context.Pagos.FirstOrDefault(x => x.IdPago == seleccionado);
                            if (p is null)
                            {
                                Interaction.MsgBox("El pago no existe.", Constants.vbExclamation);
                                return;
                            }
                            if (p.total < 0m)
                            {
                                Interaction.MsgBox("Este pago ya está anulado.", Constants.vbExclamation);
                                return;
                            }

                            var anula = new PagoEntity()
                            {
                                FechaCarga = DateTime.Now,
                                efectivo = -p.efectivo,
                                totalTransferencia = -p.totalTransferencia,
                                totalCh = -p.totalCh,
                                total = -p.total,
                                notas = "ANULA ORDEN DE PAGO: " + p.IdPago + Constants.vbCrLf + p.notas,
                                IdAnulaPago = p.IdPago,
                                activo = true
                            };
                            context.Pagos.Add(anula);
                            context.SaveChanges();
                            Interaction.MsgBox("Pago anulado correctamente.", Constants.vbInformation);
                            break;
                        }

                    case "cobros":
                        {
                            var c = context.Cobros.FirstOrDefault(x => x.IdCobro == seleccionado);
                            if (c is null)
                            {
                                Interaction.MsgBox("El cobro no existe.", Constants.vbExclamation);
                                return;
                            }
                            if (c.total < 0m)
                            {
                                Interaction.MsgBox("Este cobro ya está anulado.", Constants.vbExclamation);
                                return;
                            }

                            var anulaC = new CobroEntity()
                            {
                                FechaCarga = DateTime.Now,
                                efectivo = -c.efectivo,
                                totalTransferencia = -c.totalTransferencia,
                                totalCh = -c.totalCh,
                                totalRetencion = -c.totalRetencion,
                                total = -c.total,
                                notas = "ANULA COBRO: " + c.IdCobro + Constants.vbCrLf + c.notas,
                                IdAnulaCobro = c.IdCobro,
                                activo = true
                            };
                            context.Cobros.Add(anulaC);
                            context.SaveChanges();
                            Interaction.MsgBox("Cobro anulado correctamente.", Constants.vbInformation);
                            break;
                        }
                }
            }
            ActualizarDataGrid();
        }

        // ========================= CONVERSIONES COMPATIBILIDAD ========================= '

        private item EntityToItem(ItemEntity iEntity)
        {
            var i = new item();
            i.id_item = iEntity.IdItem;
            i.descript = iEntity.descript;
            i.activo = iEntity.activo;
            return i;
        }

        private pedido EntityToPedido(PedidoEntity pEntity)
        {
            var p = new pedido();
            p.id_pedido = pEntity.IdPedido;
            p.fecha = Conversions.ToString(pEntity.fecha.Value);
            p.id_cliente = pEntity.IdCliente;
            p.total = (double)pEntity.total;
            return p;
        }

        private cobro EntityToCobro(CobroEntity cEntity)
        {
            var c = new cobro();
            c.id_cobro = cEntity.IdCobro;
            c.total = (double)cEntity.total;
            return c;
        }

        private pago EntityToPago(PagoEntity pEntity)
        {
            var p = new pago();
            p.id_pago = pEntity.IdPago;
            p.total = (double)pEntity.total;
            return p;
        }



    }
}
