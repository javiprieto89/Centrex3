using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows6.1")]
    public partial class main : Form
    {
        private int desde;
        private int pagina;
        private int nRegs;
        private int tPaginas;

        private void ActualizarPaginacion()
        {
            int pageSize = Math.Max(1, itXPage);
            nRegs = dg_view?.Rows?.Count ?? 0;
            tPaginas = nRegs == 0 ? 1 : (int)Math.Ceiling(nRegs / (double)pageSize);
            if (pagina < 1)
            {
                pagina = 1;
            }
            else if (pagina > tPaginas)
            {
                pagina = tPaginas;
            }

            int maxDesde = Math.Max(0, (tPaginas - 1) * pageSize);
            if (desde > maxDesde)
            {
                desde = maxDesde;
            }
            else if (desde < 0)
            {
                desde = 0;
            }
        }

        public main()
        {
            InitializeComponent();
            ConfigurarDataGridInicial();
        }

        private void ConfigurarDataGridInicial()
        {
            dg_view.AutoGenerateColumns = true;
            dg_view.AllowUserToAddRows = false;
            dg_view.AllowUserToDeleteRows = false;
            dg_view.AllowUserToResizeRows = false;
            dg_view.MultiSelect = false;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.RowHeadersVisible = false;
            dg_view.DataBindingComplete -= Dg_view_DataBindingComplete;
            dg_view.DataBindingComplete += Dg_view_DataBindingComplete;
        }

        private void Dg_view_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //  if (tss_totalRegistros != null)
            //    tss_totalRegistros.Text = $"Registros: {dg_view.Rows.Count}";
        }

        private void main_Load(object sender, EventArgs e)
        {
            Visible = false;
            pc = SystemInformation.ComputerName.ToUpper();

            var c = new configInit();
            c.leerConfig();

            comprobantePresupuesto_default = 3;
            id_comprobante_default = 3;
            id_tipoDocumento_default = 80;
            id_tipoComprobante_default = 1;
            id_cliente_pedido_default = 2;
            id_pais_default = 1;
            id_provincia_default = 1;
            id_marca_default = 1;
            id_proveedor_default = 1;
            cuit_emisor_default = "20233695255";
            id_condicion_compra_default = 1;
            STR_COMPROBANTES_CONTABLES = "1, 6, 11, 51, 201, 206, 211, 1006, 2, 7, 12, 52, 202, 207, 212, 1007, 1002, 1003, 1004, 1005, 1010, 1015, 3, 8, 13, 53, 203, 208, 213, 1008, 4, 1009";
            versiondb = "1.0.0";

            if (pc != "JARVIS" && pc != "SERVTEC-06" && pc != "CORTANA" && pc != "SKYNET")
            {
                depuracion = false;
                Timer1.Enabled = true;
            }
            else
            {
                depuracion = true;
                chk_test.Visible = true;
                Timer1.Enabled = false;
                serversql = "127.0.0.1";
            }

            if (Debugger.IsAttached)
            {
                depuracion = true;
                Timer1.Enabled = false;
            }

            if (pc == "SILVIA")
            {
                serversql = "192.168.1.100";
            }

            try
            {
                if (pc != "JARVIS" || !depuracion)
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
                MessageBox.Show("Error al ejecutar procesos de actualización de base de datos:\n" + ex.Message,
                   "Computron", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int cantUsuarios = generales.cantReg("UsuarioEntity");
            if (cantUsuarios == 0)
            {
                while (cantUsuarios <= 0)
                {
                    if (MessageBox.Show("No hay usuarios creados. ¿Desea crear uno ahora?",
                    "Centrex", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                        Environment.Exit(0);

                    My.MyProject.Forms.add_usuario.ShowDialog();
                    cantUsuarios = generales.cantReg("UsuarioEntity");

                    if (cantUsuarios == 0)
                    {
                        MessageBox.Show("Debe crear un usuario para continuar.",
                             "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Reinicie el sistema e inicie sesión con el usuario creado.",
                             "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        generales.closeandupdate(this);
                    }
                }
            }
            else
            {
                if (pc != "JARVIS" && pc != "SERVTEC-06" && !depuracion)
                {
                    My.MyProject.Forms.login.ShowDialog();
                }
                else
                {
                    usuario_logueado = usuarios.info_usuario("javierp", true);
                }

                borrar_tabla_pedidos_temporales(usuario_logueado.IdUsuario);
                borrarTmpProduccion(usuario_logueado.IdUsuario);
                Borrar_tabla_segun_usuario("tmpOC_items", usuario_logueado.IdUsuario);
                stock.ArchivarIngresoStock();
                Visible = true;
            }

            CheckForIllegalCrossThreadCalls = false;
            generales.borrartbl("tmpcobros_retenciones");

            if (generales.haycambios())
                frmCambios.ShowDialog();

            cmd_add.Enabled = false;
            dg_view.Visible = false;
            txt_search.Enabled = false;
            lbl_borrarbusqueda.Enabled = false;
            chk_historicos.Enabled = false;

            var version = My.MyProject.Application.Info.Version;
            tss_version.Text = $"Versión: {version.Major}.{version.Minor}.{version.Revision}";
            tss_dbInfo.Text = "ServerSQL: " + serversql + " - DB: " + basedb + " - Ver.DB: " + versiondb;
            tss_usuario_logueado.Text = "Usuario logueado: " + usuario_logueado.Nombre;
            tss_hora.Text = "Hora: " + DateTime.Now.ToString("HH:mm:ss");

            int[] ramas = [0, 1, 3, 4];
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

        private async Task actualizarDataGrid(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var node = e.Node;
            await actualizarDataGrid(node.Name);
        }

        private async Task actualizarDataGrid(string tabla, bool modHistorico = false)
        {
            if (!cmd_add.Enabled)
            {
                cmd_add.Enabled = true;
                dg_view.Visible = true;
                txt_search.Enabled = true;
                lbl_borrarbusqueda.Enabled = true;
                chk_historicos.Enabled = true;
                pic.Visible = false;
            }

            tabla_vieja = tabla;
            if (string.IsNullOrWhiteSpace(tabla))
                return;

            txt_search.Text = string.Empty;
            if (modHistorico) chk_historicos.Checked = false;

            desde = 0;
            pagina = 1;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, tabla, activo);
            if (result.Query is null)
                return;

            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(
                dg_view,
                result,
                depuracion: depuracion
            );
        }

        private async void Treev_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabla = e.Node.Name;
            await actualizarDataGrid(sender, e);
        }

        private async void cmd_next_Click(object sender, EventArgs e)
        {
            ActualizarPaginacion();
            int pageSize = Math.Max(1, itXPage);
            if (pagina >= tPaginas)
                return;
            desde += pageSize;
            pagina += 1;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, tabla, activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: depuracion);
            ActualizarPaginacion();
        }

        private async void cmd_prev_Click(object sender, EventArgs e)
        {
            if (pagina <= 1)
                return;
            int pageSize = Math.Max(1, itXPage);
            desde -= pageSize;
            pagina -= 1;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, tabla, activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: depuracion);
            ActualizarPaginacion();
        }

        private async void cmd_first_Click(object sender, EventArgs e)
        {
            desde = 0;
            pagina = 1;

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, tabla, activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: depuracion);
            ActualizarPaginacion();
        }

        private async void cmd_last_Click(object sender, EventArgs e)
        {
            ActualizarPaginacion();
            int pageSize = Math.Max(1, itXPage);
            pagina = tPaginas;
            desde = Math.Max(0, nRegs - pageSize);

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, tabla, activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: depuracion);
            ActualizarPaginacion();
        }

        private async void AnularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int seleccionado = Convert.ToInt32(dg_view.CurrentRow.Cells[0].Value);
            using var context = new CentrexDbContext();
            switch (tabla ?? "")
            {
                case "pagos":
                    {
                        var p = context.PagoEntity.FirstOrDefault(x => x.IdPago == seleccionado);
                        if (p is null)
                        {
                            MessageBox.Show("El pago no existe.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (p.Total < 0m)
                        {
                            MessageBox.Show("Este pago ya está anulado.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        var anula = new PagoEntity()
                        {
                            FechaCarga = p.FechaCarga,
                            Efectivo = -p.Efectivo,
                            TotalTransferencia = -p.TotalTransferencia,
                            TotalCh = -p.TotalCh,
                            Total = -p.Total,
                            Notas = "ANULA ORDEN DE PAGO: " + p.IdPago + "\r\n" + p.Notas,
                            IdAnulaPago = p.IdPago,
                            Activo = true
                        };
                        context.PagoEntity.Add(anula);
                        context.SaveChanges();
                        MessageBox.Show("Pago anulado correctamente.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                case "cobros":
                    {
                        var c = context.CobroEntity.FirstOrDefault(x => x.IdCobro == seleccionado);
                        if (c is null)
                        {
                            MessageBox.Show("El cobro no existe.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (c.Total < 0m)
                        {
                            MessageBox.Show("Este cobro ya está anulado.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            Notas = "ANULA COBRO: " + c.IdCobro + "\r\n" + c.Notas,
                            IdAnulaCobro = c.IdCobro,
                            Activo = true
                        };
                        context.CobroEntity.Add(anulaC);
                        context.SaveChanges();
                        MessageBox.Show("Cobro anulado correctamente.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }

            using var ctx = new CentrexDbContext();
            var result = DataGridQueryFactory.GetQueryForTable(ctx, tabla, activo);
            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: depuracion);
            ActualizarPaginacion();
        }

        private void Treev_MouseClick(object sender, MouseEventArgs e)
        {
            cmsPreciosMasivo.Visible = false;
            if (Treev.SelectedNode.Name == "items" && e.Button == MouseButtons.Right)
            {
                Treev.ContextMenuStrip = this.cmsPreciosMasivo;
            }
            else
            {
                Treev.ContextMenuStrip = null;
            }
        }

        private async void dg_view_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!borrado)
                edicion = true;

            if (dg_view.Rows.Count == 0)
                return;

            string seleccionado = dg_view.CurrentRow.Cells[0].Value.ToString();
            int intSel = Convert.ToInt32(seleccionado);

            switch (tabla)
            {
                case "clientes":
                    edita_cliente = info_cliente(intSel);
                    new add_cliente().ShowDialog();
                    break;

                case "archivoCCClientes":
                    edita_ccCliente = info_ccCliente(intSel);
                    new add_ccCliente().ShowDialog();
                    break;

                case "archivoCCProveedores":
                    edita_ccProveedor = info_ccProveedor(intSel);
                    new add_ccProveedor().ShowDialog();
                    break;

                case "proveedores":
                    edita_proveedor = info_proveedor(intSel);
                    new add_proveedor().ShowDialog();
                    break;

                case "tipos_items":
                    edita_tipoitem = info_tipoitem(intSel);
                    new add_tipoitem().ShowDialog();
                    break;

                case "marcas_items":
                    edita_marcai = info_marcai(intSel);
                    new add_marcai().ShowDialog();
                    break;

                case "items":
                    edita_item = info_item(intSel);
                    new add_item().ShowDialog();
                    break;

                case "asocItems":
                    {
                        int separador = Conversion.Int(seleccionado.ToString().IndexOf('-'));
                        int id_item = Convert.ToInt32(seleccionado[..separador].Trim());
                        int id_asocItem = Convert.ToInt32(seleccionado[(separador + 1)..].Trim());
                        edita_asocItem = info_asocItem(id_item, id_asocItem);
                        new add_asocItem().ShowDialog();
                        break;
                    }

                case "comprobantes":
                    edita_comprobante = info_comprobante(intSel);
                    new add_comprobante().ShowDialog();
                    break;

                case "archivoconsultas":
                    edita_consulta = info_consulta(intSel);
                    new add_consulta().ShowDialog();
                    break;

                case "caja":
                    edita_caja = info_caja(intSel);
                    new add_caja().ShowDialog();
                    break;

                case "bancos":
                    edita_banco = info_banco(intSel);
                    new add_banco().ShowDialog();
                    break;

                case "cuentas_bancarias":
                    edita_cuentaBancaria = info_cuentaBancaria(intSel);
                    new add_cuentaBancaria().ShowDialog();
                    break;

                case "chRecibidos":
                case "chEmitidos":
                case "chCartera":
                    edita_cheque = info_cheque(intSel);
                    new add_cheque().ShowDialog();
                    break;

                case "impuestos":
                    edita_impuesto = info_impuesto(intSel);
                    new add_impuesto().ShowDialog();
                    break;

                case "condiciones_venta":
                    edita_condicion_venta = info_condicion_venta(intSel);
                    new add_condicion_venta().ShowDialog();
                    break;

                case "condiciones_compra":
                    edita_condicion_compra = info_condicion_compra(intSel);
                    new add_condicion_compra().ShowDialog();
                    break;

                case "conceptos_compra":
                    edita_concepto_compra = info_concepto_compra(intSel);
                    new add_concepto_compra().ShowDialog();
                    break;

                case "comprobantes_compras":
                    break;

                case "itemsImpuestos":
                    {
                        int separador = Conversion.Int(seleccionado.ToString().IndexOf('-'));
                        int id_item = Convert.ToInt32(seleccionado[..separador].Trim());
                        int id_impuesto = Convert.ToInt32(seleccionado[(separador + 1)..].Trim());
                        edita_itemImpuesto = info_itemImpuesto(id_item, id_impuesto);
                        new add_itemImpuesto().ShowDialog();
                        break;
                    }

                case "pagos":
                    return;

                case "registros_stock":
                    edita_registro_stock = info_registro_stock(intSel);
                    new add_stock().ShowDialog();
                    break;

                case "produccion":
                    borrarTmpProduccion(usuario_logueado.IdUsuario);
                    edita_produccion = info_produccion(intSel);
                    produccion_a_produccionTmp(intSel);
                    new add_produccion().ShowDialog();
                    break;

                case "pedidos":
                    if (chk_historicos.Checked)
                    {
                        id = intSel;
                    }
                    else
                    {
                        edita_pedido = info_pedido(intSel);
                        edita_pedido.IdUsuario = usuario_logueado.IdUsuario;
                        Guid idUnico = Generar_ID_Unico();
                        pedido_a_pedidoTmp(intSel, usuario_logueado.IdUsuario, idUnico);
                        new add_pedido(idUnico).ShowDialog();
                    }
                    break;

                case "cobros":
                    break;

                case "ordenesCompras":
                    edita_ordenCompra = info_ordenCompra(seleccionado);
                    oc_a_ocTmp(edita_ordenCompra.IdOrdenCompra);
                    new add_ordenCompra().ShowDialog();
                    break;

                case "permisos":
                    edita_permiso = info_permiso(intSel);
                    new add_permiso().ShowDialog();
                    break;

                case "usuarios":
                    edita_usuario = info_usuario(intSel);
                    new add_usuario().ShowDialog();
                    break;

                case "permisos_a_perfiles":
                    MessageBox.Show("La relación entre un permiso y un perfil no puede editarse",
                        "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case "perfiles_a_usuarios":
                    MessageBox.Show("La relación entre un usuario y un perfil no puede editarse",
                        "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

            if (!borrado)
                edicion = false;

            if (tabla != "pedidos")
                await actualizarDataGrid(tabla);
            else if (tabla == "pedidos" && activo)
                await actualizarDataGrid(tabla);
        }

        private async void chk_historicos_CheckedChanged(object sender, EventArgs e)
        {
            if (activo)
                activo = false;
            else
                activo = true;

            cmd_add.Enabled = false;
            await actualizarDataGrid(tabla);
        }

        private void chk_rpt_CheckedChanged(object sender, EventArgs e)
        {
            showrpt = chk_rpt.Checked;
        }

        private async void chk_test_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_test.Checked)
            {
                depuracion = true;
                Timer1.Enabled = false;
            }
            else
            {
                depuracion = false;
                Timer1.Enabled = true;
            }
            await actualizarDataGrid(tabla);
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pc != "JARVIS" && pc != "SERVTEC-06" && pc != "CORTANA" && !depuracion)
            {
                using (var backupForm = new BackupDB())
                {
                    // Mostrar el formulario de backup al cerrar la app
                    backupForm.ShowDialog();
                }

                // Si quisieras mostrar el aviso opcional:
                // MessageBox.Show("Recuerde realizar un backup de la base de datos periódicamente.",
                //     "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (modificacionesDB && !string.Equals(pc, "JARVIS", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Se deben hacer modificaciones mayores a la base de datos para que se ajuste a la última versión del programa." + Environment.NewLine +
                    "Avísele al programador, ya que puede ser que el programa no corra correctamente sin estas modificaciones.",
                    "Centrex",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Cierra completamente la aplicación
                Application.Exit();
            }
        }

        private async void cmd_pedido_Click(object sender, EventArgs e)
        {
            using (var form = new add_pedido())
            {
                form.ShowDialog();
            }
            //Treev.SelectedNode = Treev.Nodes["root"].Nodes["ventas"].Nodes["pedidos"];
            tabla = "pedidos";
            await actualizarDataGrid(tabla);
        }

        private async void cmd_addcliente_Click(object sender, EventArgs e)
        {
            using (var form = new add_cliente())
            {
                form.ShowDialog();
            }
            tabla = "clientes";
            await actualizarDataGrid(tabla);
        }

        private async void cmd_add_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabla?.ToLower())
                {
                    case "clientes":
                        new add_cliente().ShowDialog();
                        break;

                    case "archivoccclientes":
                        new add_ccCliente().ShowDialog();
                        break;

                    case "proveedores":
                        new add_proveedor().ShowDialog();
                        break;

                    case "archivoccproveedores":
                        new add_ccProveedor().ShowDialog();
                        break;

                    case "tipos_items":
                        new add_tipoitem().ShowDialog();
                        break;

                    case "marcas_items":
                        new add_marcai().ShowDialog();
                        break;

                    case "items":
                        new add_item().ShowDialog();
                        break;

                    case "asocitems":
                        new add_asocItem().ShowDialog();
                        break;

                    case "comprobantes":
                        new add_comprobante().ShowDialog();
                        break;

                    case "archivoconsultas":
                        new add_consulta().ShowDialog();
                        break;

                    case "caja":
                        new add_caja().ShowDialog();
                        break;

                    case "bancos":
                        new add_banco().ShowDialog();
                        break;

                    case "cuentas_bancarias":
                        new add_cuentaBancaria().ShowDialog();
                        break;

                    case "chrecibidos":
                        new add_cheque(true, false).ShowDialog();
                        break;

                    case "chemitidos":
                        new add_cheque(false, true).ShowDialog();
                        break;

                    case "chcartera":
                        new add_cheque().ShowDialog();
                        break;

                    case "impuestos":
                        new add_impuesto().ShowDialog();
                        break;

                    case "condiciones_venta":
                        new add_condicion_venta().ShowDialog();
                        break;

                    case "condiciones_compra":
                        new add_condicion_compra().ShowDialog();
                        break;

                    case "conceptos_compra":
                        new add_concepto_compra().ShowDialog();
                        break;

                    case "itemsimpuestos":
                        new add_itemImpuesto().ShowDialog();
                        break;

                    case "ordenescompras":
                        new add_ordenCompra().ShowDialog();
                        break;

                    case "comprobantes_compras":
                        new add_comprobantes_compras().ShowDialog();
                        break;

                    case "pagos":
                        new add_pago().ShowDialog();
                        break;

                    case "ajustes_stock":
                        new add_ajuste_stock().ShowDialog();
                        break;

                    case "registros_stock":
                        tabla = "items_registros_stock";
                        new add_stock().ShowDialog();
                        tabla = "registros_stock";
                        break;

                    case "produccion":
                        new add_produccion().ShowDialog();
                        break;

                    case "pedidos":
                        new add_pedido().ShowDialog();
                        break;

                    case "cobros":
                        new add_cobro().ShowDialog();
                        break;

                    case "cpersonalizadas":
                        new grilla_resultados().ShowDialog();
                        break;

                    case "perfiles":
                        new add_perfil().ShowDialog();
                        break;

                    case "permisos":
                        new add_permiso().ShowDialog();
                        break;

                    case "usuarios":
                        new add_usuario().ShowDialog();
                        break;

                    case "permisos_a_perfiles":
                    case "perfiles_a_usuarios":
                        // Formularios aún no implementados
                        break;

                    default:
                        MessageBox.Show($"La tabla '{tabla}' no tiene formulario asignado.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

                // 🔄 Refresca la grilla usando tu función existente
                await actualizarDataGrid(tabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en cmd_add_Click: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
