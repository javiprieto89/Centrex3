using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Centrex
{

    //static class MainModule
    //{
    //    [STAThread()]
    //    public static void Main()
    //    {
    //        // Configurar protocolos TLS (antes de crear formularios)
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

    //        // Inicializar entorno gráfico
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);

    //        // Iniciar el formulario principal
    //        Application.Run(new main());
    //    }
    //}

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
            pc = SystemInformation.ComputerName.ToUpper();

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
                        generales.ejecutarSQL(fileReader);
                        File.Move(archivo, Path.ChangeExtension(archivo, ".jav"));
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al ejecutar procesos de actualización de base de datos: " + Constants.vbCrLf + ex.Message, Constants.vbCritical, "Computron");
            }

            // Control de usuarios inicial
            int cantUsuarios = generales.cantReg("UsuarioEntity");
            if (cantUsuarios == 0)
            {
                while (cantUsuarios <= 0)
                {
                    if (Interaction.MsgBox("No hay usuarios creados. ¿Desea crear uno ahora?", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKCancel), "Centrex") == MsgBoxResult.Cancel)
                        Environment.Exit(0);
                    My.MyProject.Forms.add_usuario.ShowDialog();
                    cantUsuarios = generales.cantReg("UsuarioEntity");
                    if (cantUsuarios == 0)
                    {
                        Interaction.MsgBox("Debe crear un usuario para continuar.", Constants.vbExclamation);
                    }
                    else
                    {
                        Interaction.MsgBox("Reinicie el sistema e inicie sesión con el usuario creado.", Constants.vbInformation);
                        generales.closeandupdate(this);
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
                    VariablesGlobales.usuario_logueado = usuarios.info_usuario("javierp", true);
                }

                generales.borrar_tabla_pedidos_temporales(VariablesGlobales.usuario_logueado.IdUsuario);
                generales.borrarTmpProduccion(VariablesGlobales.usuario_logueado.IdUsuario);
                generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
                stock.ArchivarIngresoStock();
                Visible = true;
            }

            CheckForIllegalCrossThreadCalls = false;
            generales.borrartbl("tmpcobros_retenciones");

            if (generales.haycambios())
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
                tss_usuario_logueado.Text = "Usuario logueado: " + VariablesGlobales.usuario_logueado.Nombre;
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

        private async void Treev_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            VariablesGlobales.tabla_vieja = VariablesGlobales.tabla;
            VariablesGlobales.tabla = e.Node.Name;
            txt_search.Text = "";
            chk_historicos.Checked = false;

            desde = 0;
            pagina = 1;

            // Casos especiales que abren formularios
            if (tabla != "")
            {
                if (tabla == "root")
                {
                    cmd_add.Enabled = false;
                    dg_view.Visible = false;
                    txt_search.Enabled = false;
                    lbl_borrarbusqueda.Enabled = false;
                    chk_historicos.Enabled = false;
                    pic.Visible = true;
                    return;
                }

                // Usar DataGridQueryFactory + LoadDataGridWithEntityAsync directamente
                using var ctx = new CentrexDbContext();
                var result = DataGridQueryFactory.GetQueryForTable(
             ctx,
           VariablesGlobales.tabla,
                    VariablesGlobales.activo
                );

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(
            dg_view,
               result,
               depuracion: VariablesGlobales.depuracion
                    );

                cmd_add.Enabled = true;
                dg_view.Visible = true;
                txt_search.Enabled = true;
                lbl_borrarbusqueda.Enabled = true;
                chk_historicos.Enabled = true;
            }
            else
            {
                return;
            }
        }

        // ========================= PAGINACIÓN ========================= '

        private async void cmd_next_Click(object sender, EventArgs e)
                {
                    if (pagina >= Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage))
                        return;
                    desde += VariablesGlobales.itXPage;
                    pagina += 1;

                    using var ctx = new CentrexDbContext();
                    var result = DataGridQueryFactory.GetQueryForTable(ctx, VariablesGlobales.tabla, VariablesGlobales.activo);
                    await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: VariablesGlobales.depuracion);
                }

        private async void cmd_prev_Click(object sender, EventArgs e)
        {
            if (pagina <= 1)
                return;
            desde -= VariablesGlobales.itXPage;
            pagina -= 1;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, VariablesGlobales.tabla, VariablesGlobales.activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: VariablesGlobales.depuracion);
        }

        private async void cmd_first_Click(object sender, EventArgs e)
        {
            desde = 0;
            pagina = 1;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, VariablesGlobales.tabla, VariablesGlobales.activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: VariablesGlobales.depuracion);
        }

        private async void cmd_last_Click(object sender, EventArgs e)
        {
            pagina = tPaginas;
            desde = nRegs - VariablesGlobales.itXPage;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, VariablesGlobales.tabla, VariablesGlobales.activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: VariablesGlobales.depuracion);
        }

        // ========================= ANULACIONES ========================= '

        private async void AnularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int seleccionado = Conversions.ToInteger(dg_view.CurrentRow.Cells[0].Value);
            using (CentrexDbContext context = new CentrexDbContext())
            {
                switch (VariablesGlobales.tabla ?? "")
                {
                    case "pagos":
                        {
                            var p = context.PagoEntity.FirstOrDefault(x => x.IdPago == seleccionado);
                            if (p is null)
                            {
                                Interaction.MsgBox("El pago no existe.", Constants.vbExclamation);
                                return;
                            }
                            if (p.Total < 0m)
                            {
                                Interaction.MsgBox("Este pago ya está anulado.", Constants.vbExclamation);
                                return;
                            }

                            var anula = new PagoEntity()
                            {
                                FechaCarga = p.FechaCarga,
                                Efectivo = -p.Efectivo,
                                TotalTransferencia = -p.TotalTransferencia,
                                TotalCh = -p.TotalCh,
                                Total = -p.Total,
                                Notas = "ANULA ORDEN DE PAGO: " + p.IdPago + Constants.vbCrLf + p.Notas,
                                IdAnulaPago = p.IdPago,
                                Activo = true
                            };
                            context.PagoEntity.Add(anula);
                            context.SaveChanges();
                            Interaction.MsgBox("Pago anulado correctamente.", Constants.vbInformation);
                            break;
                        }

                    case "cobros":
                        {
                            var c = context.CobroEntity.FirstOrDefault(x => x.IdCobro == seleccionado);
                            if (c is null)
                            {
                                Interaction.MsgBox("El cobro no existe.", Constants.vbExclamation);
                                return;
                            }
                            if (c.Total < 0m)
                            {
                                Interaction.MsgBox("Este cobro ya está anulado.", Constants.vbExclamation);
                                return;
                            }

                            var anulaC = new CobroEntity()
                            {
                                FechaCarga = c.FechaCarga,
                                Efectivo = -c.Efectivo,
                                TotalTransferencia = -c.TotalTransferencia,
                                TotalCh = -c.TotalCh,
                                TotalRetencion = -c.TotalRetencion,
                                Total = -c.Total,
                                Notas = "ANULA COBRO: " + c.IdCobro + Constants.vbCrLf + c.Notas,
                                IdAnulaCobro = c.IdCobro,
                                Activo = true
                            };
                            context.CobroEntity.Add(anulaC);
                            context.SaveChanges();
                            Interaction.MsgBox("Cobro anulado correctamente.", Constants.vbInformation);
                            break;
                        }
                }
            }

            // Recargar grid inline
            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, VariablesGlobales.tabla, VariablesGlobales.activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: VariablesGlobales.depuracion);
        }
    }
}
