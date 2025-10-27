using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{

    public partial class add_produccion
    {

        private bool recibido = false;
        private int idUsuario;
        private string idUnico;

        private CentrexDbContext ctx = new CentrexDbContext();

        public add_produccion()
        {
            InitializeComponent();
        }

        private void add_produccion_Load(object sender, EventArgs e)
        {
            idUsuario = VariablesGlobales.usuario_logueado?.IdUsuario ?? 0;
            idUnico = generales.Generar_ID_Unico();

            // Cargo proveedores usando EF
            var proveedores = ctx.Proveedores.Where(p => p.activo == true).OrderBy(p => p.razon_social).ToList();

            cmb_proveedor.DataSource = proveedores;
            cmb_proveedor.DisplayMember = "RazonSocial";
            cmb_proveedor.ValueMember = "IdProveedor";
            cmb_proveedor.SelectedIndex = -1;
            cmb_proveedor.Text = "Seleccione un proveedor...";

            if (VariablesGlobales.edicion | VariablesGlobales.borrado == true)
            {
                lbl_nProduccion.Text = VariablesGlobales.edita_produccion.IdProduccion;
                lbl_produccion.Visible = true;
                lbl_nProduccion.Visible = true;
                lbl_fechaCarga.Text = VariablesGlobales.edita_produccion.FechaCarga.ToString("dd/MM/yyyy");

                if (VariablesGlobales.edita_produccion.FechaEnvio.HasValue)
                {
                    lbl_fechaEnvio.Text = VariablesGlobales.edita_produccion.FechaEnvio.Value.ToString("dd/MM/yyyy");
                    cmd_enviar.Text = "Cerrar pedido";
                    dg_viewProduccion.ContextMenuStrip = cms_enviado;
                    recibido = true;
                }
                else
                {
                    lbl_fechaEnvio.Text = "";
                }

                if (VariablesGlobales.edita_produccion.FechaRecepcion.HasValue)
                {
                    lbl_fechaRecepcion.Text = VariablesGlobales.edita_produccion.FechaRecepcion.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    lbl_fechaRecepcion.Text = "";
                }

                cmb_proveedor.SelectedValue = VariablesGlobales.edita_produccion.IdProveedor;
                cmb_proveedor.Enabled = false;

                actualizarDataGrid();
            }
            else
            {
                borrarTmpProduccion(VariablesGlobales.usuario_logueado.IdUsuario);
                lbl_fechaCarga.Text = generales.Hoy();
                lbl_fechaEnvio.Text = "";
                lbl_fechaRecepcion.Text = "";
            }

            // Si está borrado o inactivo
            if (VariablesGlobales.borrado | (VariablesGlobales.edita_produccion.IdProduccion != 0 && !VariablesGlobales.edita_produccion.activo))
            {
                cmb_proveedor.Enabled = false;
                cmd_add_item.Enabled = false;
                pic_searchProveedor.Enabled = false;
                chk_imprimir.Enabled = false;
                chk_secuencia.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_enviar.Enabled = false;
            }

            // Si está en modo borrado
            if (VariablesGlobales.borrado)
            {
                if (!VariablesGlobales.edita_produccion.activo)
                {
                    Interaction.MsgBox("No se puede borrar un pedido de producción ya recibido", Constants.vbExclamation, "Centrex");
                    closeandupdate(this);
                    return;
                }

                cmd_exit.Enabled = false;
                if (Interaction.MsgBox("¿Está seguro que desea borrar este pedido de producción?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == Constants.vbYes)
                {
                    borrarTmpProduccion(VariablesGlobales.usuario_logueado.IdUsuario);

                    // Borrar items y produccion con EF
                    var prod = ctx.Produccion.Include("ProduccionItems").FirstOrDefault(p => p.IdProduccion == VariablesGlobales.edita_produccion.IdProduccion);

                    if (prod is not null)
                    {
                        ctx.ProduccionItems.RemoveRange((IEnumerable<ProduccionItemEntity>)prod.ProduccionItems);
                        ctx.Produccion.Remove(prod);
                        ctx.SaveChanges();
                    }
                }
                closeandupdate(this);
            }
        }

        private void add_produccion_FormClosed(object sender, FormClosedEventArgs e)
        {
            VariablesGlobales.id = 0;
            VariablesGlobales.edita_produccion = new ProduccionEntity();
            borrarTmpProduccion(VariablesGlobales.usuario_logueado.IdUsuario);
            generales.ActivaItems("produccion_items");
            VariablesGlobales.edicion = false;
            closeandupdate(this);
        }

        private void cmd_add_item_Click(object sender, EventArgs e)
        {
            string tmpTabla = VariablesGlobales.tabla;
            bool tmpActivo = VariablesGlobales.activo;
            VariablesGlobales.tabla = "items_sinDescuento";
            VariablesGlobales.activo = true;

            VariablesGlobales.agregaitem = true;
            Enabled = false;

            var srch = new search(true, false, true);
            srch.ShowDialog();
            VariablesGlobales.tabla = tmpTabla;
            VariablesGlobales.activo = tmpActivo;
            Enabled = true;

            int seleccionado = VariablesGlobales.id;
            if (seleccionado > 0)
            {
                VariablesGlobales.edita_item = mitem.info_item(seleccionado.ToString());
                var agregaItemFrm = new infoagregaitem(true, false, true, idUsuario, idUnico);
                agregaItemFrm.ShowDialog();
                VariablesGlobales.edita_item = new item();
            }

            if (asocitems.Tiene_Items_Asociados(seleccionado.ToString()))
            {
                var frm_detalle_prod = new frm_detalle_asoc_produccion(seleccionado);
                frm_detalle_prod.ShowDialog();
            }

            VariablesGlobales.agregaitem = false;
            VariablesGlobales.id = 0;
            actualizarDataGrid();

        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedIndex == -1)
            {
                Interaction.MsgBox("El campo 'Proveedor' es obligatorio.", Constants.vbExclamation);
                return;
            }
            else if (dg_viewProduccion.Rows.Count == 0)
            {
                Interaction.MsgBox("No hay items cargados.");
                return;
            }

            var p = new ProduccionEntity()
            {
                IdProveedor = cmb_proveedor.SelectedValue,
                FechaCarga = DateTime.Now,
                enviado = !string.IsNullOrEmpty(lbl_fechaEnvio.Text),
                recibido = !string.IsNullOrEmpty(lbl_fechaRecepcion.Text),
                activo = string.IsNullOrEmpty(lbl_fechaRecepcion.Text)
            };

            if (VariablesGlobales.edicion)
            {
                p.IdProduccion = VariablesGlobales.edita_produccion.IdProduccion;
                ctx.Entry(p).State = EntityState.Modified;
            }
            else
            {
                ctx.Produccion.Add(p);
            }

            ctx.SaveChanges();

            // Guardar items temporales vinculados
            var tmpItems = ctx.TmpProduccionItems.Where(t => t.IdUsuario == VariablesGlobales.usuario_logueado.IdUsuario).ToList();

            foreach (var t in tmpItems)
            {
                var nuevoItem = new ProduccionItemEntity()
                {
                    id_produccion = p.IdProduccion,
                    id_item = Conversions.ToInteger(((dynamic)t).IdItem),
                    cantidad = Conversions.ToInteger(((dynamic)t).Cantidad),
                    id_item_recibido = (int?)((dynamic)t).IdItemRecibido,
                    cantidad_recibida = (int?)((dynamic)t).CantidadRecibida,
                    activo = (bool?)((dynamic)t).Activo
                };
                ctx.ProduccionItems.Add(nuevoItem);
            }
            ctx.SaveChanges();

            borrarTmpProduccion(VariablesGlobales.usuario_logueado.IdUsuario);
            Interaction.MsgBox("Producción guardada correctamente.", Constants.vbInformation);

            if (chk_secuencia.Checked)
            {
                actualizarDataGrid();
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void cmd_enviar_Click(object sender, EventArgs e)
        {
            if (recibido)
            {
                if (Interaction.MsgBox("ATENCIÓN: Si finaliza la recepción, no podrá realizar más modificaciones.", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == Constants.vbYes)
                {
                    lbl_fechaRecepcion.Text = generales.Hoy();
                }
                else
                {
                    return;
                }
            }
            else
            {
                lbl_fechaEnvio.Text = generales.Hoy();
            }
            cmd_ok_Click(null, null);
        }

        // ======================
        // BINDING AUTOMÁTICO DE DATAGRID
        // ======================
        private void actualizarDataGrid()
        {
            var tmpItems = ctx.TmpProduccionItems.Include(t => t.IdItem).Include(t => t.IdItemRecibido).Where(t => (t.activo is { } arg1 ? arg1 == true : (bool?)null) && t.IdUsuario == VariablesGlobales.usuario_logueado.IdUsuario).ToList();

            dg_viewProduccion.AutoGenerateColumns = false;
            dg_viewProduccion.DataSource = tmpItems;

            if (dg_viewProduccion.Columns.Count == 0)
            {
                dg_viewProduccion.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ID", DataPropertyName = "IdTmpProduccionItem" });
                dg_viewProduccion.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Producto", DataPropertyName = "Item.Descript" });
                dg_viewProduccion.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Cantidad", DataPropertyName = "Cantidad" });
                dg_viewProduccion.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Producto Recibido", DataPropertyName = "ItemRecibido.Descript" });
                dg_viewProduccion.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Cantidad Recibida", DataPropertyName = "CantidadRecibida" });
            }
        }


        // ==========================================================
        // MODIFICAR ARTÍCULO RECIBIDO  — versión EF
        // ==========================================================
        private void ModificarArtículoRecibidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_viewProduccion.CurrentRow is null)
                return;

            string seleccionado = dg_viewProduccion.CurrentRow.Cells[0].Value.ToString();
            int id_item_tmp = Conversions.ToInteger(Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1));

            string tmpTabla = VariablesGlobales.tabla;
            bool tmpActivo = VariablesGlobales.activo;
            VariablesGlobales.tabla = "items_sinDescuento";
            VariablesGlobales.activo = true;

            Enabled = false;
            var srch = new search(true, false, false);
            srch.ShowDialog();
            int id_item = VariablesGlobales.id;

            VariablesGlobales.tabla = tmpTabla;
            VariablesGlobales.activo = tmpActivo;
            Enabled = true;

            // === EF: actualizar id_item_recibido ===
            var tmpItem = ctx.TmpProduccionItems.FirstOrDefault(t => t.IdTmpProduccionItem == id_item_tmp);
            if (tmpItem is not null)
            {
                tmpItem.IdItemRecibido = id_item;
                ctx.Entry(tmpItem).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            VariablesGlobales.id = 0;
            actualizarDataGrid();
        }


        // ==========================================================
        // MODIFICAR CANTIDAD RECIBIDA — versión EF
        // ==========================================================
        private void ModificarCantidadRecibidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_viewProduccion.CurrentRow is null)
                return;

            string seleccionado = dg_viewProduccion.CurrentRow.Cells[0].Value.ToString();
            int id_item_tmp = Conversions.ToInteger(Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1));

            // Abre el formulario de edición de cantidad
            var agregaItem = new infoagregaitem(true, false, false, idUsuario, idUnico);
            agregaItem.ShowDialog();

            decimal cantidad_recibida = agregaItem.cant;

            // === EF: actualizar cantidad_recibida ===
            var tmpItem = ctx.TmpProduccionItems.FirstOrDefault(t => t.IdTmpProduccionItem == id_item_tmp);
            if (tmpItem is not null)
            {
                tmpItem.CantidadRecibida = cantidad_recibida;
                ctx.Entry(tmpItem).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            actualizarDataGrid();
        }

    }
}
