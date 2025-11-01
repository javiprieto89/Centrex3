using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_produccion : Form
    {
        private bool recibido = false;
        private int idUsuario;
        private Guid idUnico;
        private readonly CentrexDbContext ctx = new CentrexDbContext();

        public add_produccion()
        {
            InitializeComponent();
        }

        private void add_produccion_Load(object sender, EventArgs e)
        {
            idUsuario = usuario_logueado?.IdUsuario ?? 0;
            idUnico = Generar_ID_Unico();

            // Cargar combo de proveedores
            generales.Cargar_Combo(
                ref cmb_proveedor,
                "ProveedorEntity",
                "RazonSocial",
                "IdProveedor",
                -1,
                true,
                null,
                null
            );
            cmb_proveedor.Text = "Seleccione un proveedor...";

            if (edicion || borrado)
            {
                lbl_nProduccion.Text = edita_produccion.IdProduccion.ToString();
                lbl_produccion.Visible = true;
                lbl_nProduccion.Visible = true;
                lbl_fechaCarga.Text = edita_produccion.FechaCarga.ToString("dd/MM/yyyy");

                if (edita_produccion.FechaEnvio.HasValue)
                {
                    lbl_fechaEnvio.Text = edita_produccion.FechaEnvio.Value.ToString("dd/MM/yyyy");
                    cmd_enviar.Text = "Cerrar pedido";
                    dg_viewProduccion.ContextMenuStrip = cms_enviado;
                    recibido = true;
                }
                else lbl_fechaEnvio.Text = "";

                if (edita_produccion.FechaRecepcion.HasValue)
                    lbl_fechaRecepcion.Text = edita_produccion.FechaRecepcion.Value.ToString("dd/MM/yyyy");
                else lbl_fechaRecepcion.Text = "";

                cmb_proveedor.SelectedValue = edita_produccion.IdProveedor;
                cmb_proveedor.Enabled = false;

                CargarGrillaProduccion();
            }
            else
            {
                borrarTmpProduccion(idUsuario);
                lbl_fechaCarga.Text = generales.Hoy();
                lbl_fechaEnvio.Text = "";
                lbl_fechaRecepcion.Text = "";
            }

            if (borrado ||
                (edita_produccion.IdProduccion != 0 && !edita_produccion.Activo))
            {
                cmb_proveedor.Enabled = false;
                cmd_add_item.Enabled = false;
                pic_searchProveedor.Enabled = false;
                chk_imprimir.Enabled = false;
                chk_secuencia.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_enviar.Enabled = false;
            }
        }

        private void add_produccion_FormClosed(object sender, FormClosedEventArgs e)
        {
            id = 0;
            edita_produccion = new ProduccionEntity();
            borrarTmpProduccion(idUsuario);
            generales.ActivaItems("produccion_items");
            edicion = false;
            generales.closeandupdate(this);
        }

        private void cmd_add_item_Click(object sender, EventArgs e)
        {
            var tmpTabla = tabla;
            var tmpActivo = activo;
            tabla = "items_sinDescuento";
            activo = true;
            agregaitem = true;

            Enabled = false;
            var srch = new search(true, false, true);
            srch.ShowDialog();
            tabla = tmpTabla;
            activo = tmpActivo;
            agregaitem = false;
            Enabled = true;

            if (id > 0)
            {
                edita_item = mitem.info_item(id);
                var agregaItemFrm = new infoagregaitem(true, false, true, idUsuario, idUnico);
                agregaItemFrm.ShowDialog();
                edita_item = null;
            }

            if (asocitems.Tiene_Items_Asociados(id))
            {
                var frm_detalle_prod = new frm_detalle_asoc_produccion(id);
                frm_detalle_prod.ShowDialog();
            }

            id = 0;
            CargarGrillaProduccion();
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            generales.closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedIndex == -1)
            {
                Interaction.MsgBox("Debe seleccionar un proveedor.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            if (dg_viewProduccion.Rows.Count == 0)
            {
                Interaction.MsgBox("No hay ítems cargados.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            var p = new ProduccionEntity();

            p.IdProveedor = (int)cmb_proveedor.SelectedValue;
            p.FechaCarga = ConversorFechas.GetFecha(lbl_fechaCarga, p.FechaCarga);
            p.Enviado = !string.IsNullOrWhiteSpace(lbl_fechaEnvio.Text);
            p.Recibido = !string.IsNullOrWhiteSpace(lbl_fechaRecepcion.Text);
            p.Activo = string.IsNullOrWhiteSpace(lbl_fechaRecepcion.Text);


            if (!string.IsNullOrWhiteSpace(lbl_fechaEnvio.Text))
                p.FechaEnvio = ConversorFechas.GetFecha(lbl_fechaEnvio, p.FechaEnvio);
            if (!string.IsNullOrWhiteSpace(lbl_fechaRecepcion.Text))
                p.FechaRecepcion = ConversorFechas.GetFecha(lbl_fechaRecepcion, p.FechaRecepcion);

            if (edicion)
            {
                p.IdProduccion = edita_produccion.IdProduccion;
                ctx.Entry(p).State = EntityState.Modified;
            }
            else
            {
                ctx.ProduccionEntity.Add(p);
            }

            ctx.SaveChanges();

            borrarTmpProduccion(idUsuario);

            if (chk_imprimir.Checked)
            {
                //var frm = new frm_prnOrdenProduccion(p.IdProduccion);
                //frm.ShowDialog();
            }

            if (chk_secuencia.Checked)
                CargarGrillaProduccion();
            else
                generales.closeandupdate(this);
        }

        private void cmd_enviar_Click(object sender, EventArgs e)
        {
            if (recibido)
            {
                if (Interaction.MsgBox("¿Finalizar recepción? No se podrá modificar luego.", MsgBoxStyle.YesNo | MsgBoxStyle.Question) == MsgBoxResult.Yes)
                    lbl_fechaRecepcion.Text = generales.Hoy();
                else
                    return;
            }
            else
            {
                lbl_fechaEnvio.Text = generales.Hoy();
            }

            cmd_ok_Click(null, null);
        }

        private void CargarGrillaProduccion()
        {
            try
            {
                var query = ctx.TmpProduccionItemEntity
                    .Include(t => t.IdItemNavigation)
                    .Include(t => t.IdItemRecibidoNavigation)
                    .Where(t => t.Activo == true && t.IdUsuario == idUsuario)
                    .OrderBy(t => t.IdTmpProduccionItem);

                var result = new DataGridQueryResult
                {
                    Query = query,
                    GridName = "produccion",
                    EsMaterializada = false
                };

                LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_viewProduccion, result).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar grilla: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void EditarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dg_viewProduccion.CurrentRow == null)
                    return;

                // Simula doble click (igual al VB original)
                //dg_viewProduccion_DoubleClick(null, null);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al editar el ítem: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void BorrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dg_viewProduccion.CurrentRow == null)
                    return;

                var idTmp = dg_viewProduccion.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrWhiteSpace(idTmp))
                    return;

                // Extrae el ID temporal antes del guion
                var idTmpProduccionItem = idTmp.Split('-')[0];

                if (Interaction.MsgBox("¿Seguro que desea eliminar el ítem seleccionado?",
                                       MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Centrex") != MsgBoxResult.Yes)
                    return;

                generales.ejecutarSQL($"DELETE FROM tmpproduccion_items WHERE id_tmpProduccionItem = '{idTmpProduccionItem}'");

                CargarGrillaProduccion();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al borrar ítem: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void dg_view_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hit = dg_viewProduccion.HitTest(e.X, e.Y);
                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        dg_viewProduccion.CurrentCell = dg_viewProduccion.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                        cms_general.Show(dg_viewProduccion, e.Location);
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al abrir menú contextual: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void ModificarArtículoRecibidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dg_viewProduccion.CurrentRow == null) return;

                var seleccionado = dg_viewProduccion.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrWhiteSpace(seleccionado)) return;

                int idTmpItem = int.Parse(seleccionado.Split('-')[0]);

                tabla = "items_sinDescuento";
                activo = true;
                Enabled = false;

                var srch = new search(true, false, false);
                srch.ShowDialog();
                Enabled = true;

                int idItemNuevo = id;
                if (idItemNuevo <= 0) return;

                generales.ejecutarSQL($"UPDATE tmpproduccion_items SET id_item_recibido = '{idItemNuevo}' WHERE id_tmpProduccionItem = '{idTmpItem}'");
                CargarGrillaProduccion();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al modificar artículo recibido: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void ModificarCantidadRecibidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dg_viewProduccion.CurrentRow == null) return;

                var seleccionado = dg_viewProduccion.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrWhiteSpace(seleccionado)) return;

                int idTmpItem = int.Parse(seleccionado.Split('-')[0]);

                var agregaFrm = new infoagregaitem(true, false, false, idUsuario, idUnico);
                agregaFrm.ShowDialog();

                int nuevaCantidad = agregaFrm.cant;
                if (nuevaCantidad < 0) return;

                generales.ejecutarSQL($"UPDATE tmpproduccion_items SET cantidad_recibida = '{nuevaCantidad}' WHERE id_tmpProduccionItem = '{idTmpItem}'");
                CargarGrillaProduccion();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al modificar cantidad recibida: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }
    }
}
