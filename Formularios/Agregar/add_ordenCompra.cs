using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Centrex;

public partial class add_ordenCompra
{

        private double totalOriginal;
        private double subTotalOriginal;
        public ComprobanteEntity comprobanteSeleccionado;
        private int nOC = -1;
        private int idUsuario;
        private Guid idUnico;

        public add_ordenCompra()
        {
            InitializeComponent();
        }

        private void cmd_add_item_Click(object sender, EventArgs e)
        {
            string tmpTabla;
            bool tmpActivo;

            tmpTabla = VariablesGlobales.tabla;
            tmpActivo = VariablesGlobales.activo;
            VariablesGlobales.tabla = "items_sinDescuento";
            VariablesGlobales.activo = true;

            VariablesGlobales.agregaitem = true;
            Enabled = false;

            var srch = new search(false, true, true);
            srch.ShowDialog();
            VariablesGlobales.tabla = tmpTabla;
            VariablesGlobales.activo = tmpActivo;
            Enabled = true;

            if (srch.SelectedIndex > 0)
            {
                VariablesGlobales.edita_item = mitem.info_item(srch.SelectedIndex);
                var agregaItemFrm = new infoagregaitem(false, true, true, idUsuario, idUnico);
                agregaItemFrm.ShowDialog();
                VariablesGlobales.edita_item = new ItemEntity();
            }

            VariablesGlobales.agregaitem = false;
            VariablesGlobales.id = 0;
            actualizarDataGrid();
        }

        private void add_ordenCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            VariablesGlobales.id = 0;
            var oc = new OrdenCompraEntity();
            VariablesGlobales.edita_ordenCompra = oc;
            // borro la tabla temporal
            generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
            // restauro los que se borraron porque no se guardaron los cambios
            generales.ActivaItems("ordenesCompras_items");
            VariablesGlobales.edicion = false;
            closeandupdate(this);
        }

        private void add_ordenCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 45)
                cmd_add_item_Click(null, null);
        }
        private void add_ordenCompra_Load(object sender, EventArgs e)
        {
            idUsuario = VariablesGlobales.usuario_logueado?.IdUsuario ?? 0;
            idUnico = generales.Generar_ID_Unico();

            generales.Cargar_Combo(
                ref cmb_proveedor,
                entidad: "ProveedorEntity",
                displaymember: "RazonSocial",
                valuemember: "IdProveedor",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: OrdenAsc("RazonSocial"));

            if (cmb_proveedor.Items.Count == 0)
            {
                Interaction.MsgBox("No hay proveedores activos cargados; no es posible generar la orden de compra.", MsgBoxStyle.Exclamation, "Centrex");
                closeandupdate(this);
                return;
            }

            cmb_proveedor.SelectedIndex = -1;
            cmb_proveedor.Text = "Seleccione un proveedor...";

            txt_subTotal.TextChanged -= new EventHandler(txt_subTotal_TextChanged);
            txt_impuestos.TextChanged -= new EventHandler(txt_impuestos_TextChanged);

            if (VariablesGlobales.edicion | VariablesGlobales.borrado)
            {
            // cargo fecha de la orden de compra
            // cargo Proveedor de la orden de compra
            // cargo items
            // cargo total
            // inhabilito carga secuencial

            lbl_fechaCarga.Text = ConversorFechas.GetFecha(VariablesGlobales.edita_ordenCompra.FechaCarga);
            dtp_fechaComprobante.Value = ConversorFechas.GetFecha(VariablesGlobales.edita_ordenCompra.FechaComprobante, dtp_fechaComprobante);



            cmb_proveedor.SelectedValue = VariablesGlobales.edita_ordenCompra.IdProveedor;

                actualizarDataGrid();

                txt_subTotal.Text = VariablesGlobales.edita_ordenCompra.Subtotal.ToString();
                txt_impuestos.Text = VariablesGlobales.edita_ordenCompra.Iva.ToString();
                txt_total.Text = VariablesGlobales.edita_ordenCompra.Total.ToString();
                txt_totalO.Text = txt_total.Text;
                txt_nota.Text = VariablesGlobales.edita_ordenCompra.Notas;
                chk_secuencia.Enabled = false;
                subTotalOriginal = Convert.ToDouble(txt_subTotal.Text);

                lbl_nOrdenCompra.Text = VariablesGlobales.edita_ordenCompra.IdOrdenCompra.ToString();
                lbl_ordenCompra.Visible = true;
                lbl_nOrdenCompra.Visible = true;
                dg_viewOC.ContextMenuStrip = cms_enviado;
            }
            else
            {
                lbl_fechaCarga.Text = generales.Hoy();
                dtp_fechaComprobante.Value = DateTime.Now;
                txt_total.Text = "0,00";
                txt_subTotal.Text = "0,00";
                txt_impuestos.Text = "0,00";
                txt_totalO.Text = txt_total.Text;
                actualizarDataGrid();
            }

            txt_subTotal.TextChanged += new EventHandler(txt_subTotal_TextChanged);
            txt_impuestos.TextChanged += new EventHandler(txt_impuestos_TextChanged);

            cmd_ok.Enabled = dg_viewOC.Rows.Count > 0;

            if (VariablesGlobales.edita_ordenCompra.IdOrdenCompra != 0 | VariablesGlobales.borrado == true)
            {
                pic_searchProveedor.Enabled = false;
                dg_viewOC.Enabled = false;
                txt_subTotal.Enabled = false;
                txt_impuestos.Enabled = false;
                txt_total.Enabled = false;
                txt_nota.Enabled = false;
                cmd_add_item.Enabled = false;
                cmd_ok.Enabled = false;
            }

            if (VariablesGlobales.borrado == true)
            {
                cmd_exit.Enabled = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta orden de compra?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (ordenesCompras.borrarOrdenCompra(VariablesGlobales.edita_ordenCompra) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la orden de compra, ¿desea intetar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (ordenesCompras.updateOrdenCompra(VariablesGlobales.edita_ordenCompra, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la orden de compra no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la orden de compra, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la orden de compra.");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
            // AddHandler Me.txt_markup.TextChanged, New EventHandler(AddressOf Me.txt_markup_TextChanged)
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            OrdenCompraEntity ultimaOC = null;

            if (cmb_proveedor.SelectedValue is null)
            {
                Interaction.MsgBox("El campo 'Proveedor' es obligatorio y está vacio");
                return;
            }
            else if (dg_viewOC.Rows.Count == 0)
            {
                Interaction.MsgBox("No hay items cargados");
                return;
            }

            var oc = new OrdenCompraEntity();

            oc.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
            oc.FechaCarga = ConversorFechas.GetFecha(lbl_fechaCarga.Text, oc.FechaCarga);
            oc.FechaComprobante = ConversorFechas.GetFecha(dtp_fechaComprobante.Value.Date, oc.FechaComprobante);
            if (lbl_fechaRecepcion.Text != "%fecha_recepcion%")
                oc.FechaCarga = ConversorFechas.GetFecha(lbl_fechaRecepcion.Text, oc.FechaCarga);
            oc.Subtotal = Convert.ToDecimal(txt_subTotal.Text);
            oc.Iva = Convert.ToDecimal(txt_impuestos.Text);
            oc.Total = Convert.ToDecimal(txt_total.Text);
            oc.Notas = txt_nota.Text;
            oc.Activo = true;

            if (VariablesGlobales.edicion == true)
            {
                // actualizar proveedor
                oc.IdOrdenCompra = edita_ordenCompra.IdOrdenCompra;
                ultimaOC = oc;
                if (ordenesCompras.updateOrdenCompra(oc) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la orden de compra.", Constants.vbExclamation);
                    closeandupdate(this);
                }
                // actualizar orde de compra (items)            
                // actualizo, agrego y borro los items de la orden de compra
                ordenesCompras.guardarOrdenCompra(VariablesGlobales.edita_ordenCompra.IdOrdenCompra);
                generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
            }
            else
            {
                // Agrego la orden de compra
                if (ordenesCompras.addOrdenCompra(oc))
                {
                    ultimaOC = ordenesCompras.info_ordenCompra();
                    // Agrego los items a la orden de compra
                    if (!ordenesCompras.guardarOrdenCompra())
                    {
                        Interaction.MsgBox("Hubo un problema al agregar la orden de compra.", Constants.vbExclamation);
                        generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
                        closeandupdate(this);
                    }
                    else
                    {
                        generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
                    }
                }
                else
                {
                    Interaction.MsgBox("Hubo un problema al agregar la orden de compra.", Constants.vbExclamation);
                    generales.Borrar_tabla_segun_usuario("tmpOC_items", VariablesGlobales.usuario_logueado.IdUsuario);
                    closeandupdate(this);
                }
                VariablesGlobales.edicion = true;
                VariablesGlobales.edita_ordenCompra = ultimaOC;
            }

            // oc_a_ocTmp(VariablesGlobales.edita_ordenCompra.id_ordenCompra)

            if (chk_secuencia.Checked == true)
            {
                VariablesGlobales.edicion = false;
                actualizarDataGrid();
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void dg_view_proveedor_DoubleClick(object sender, EventArgs e)
        {
            ComboBox argcmb = null;
            generales.updateform(cmb: ref argcmb);
        }

        private void pic_searchProveedor_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el Proveedor default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_proveedor_default;
            var argcmb = cmb_proveedor;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_proveedor = argcmb;
        }

        private void dg_view_DoubleClick(object sender, EventArgs e)
        {
            string seleccionado;
            // Obtengo el ID del item
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            seleccionado = Strings.Right(seleccionado, seleccionado.Length - Strings.InStr(seleccionado, "-"));

            VariablesGlobales.edita_item = mitem.info_item((int)Conversion.Int(seleccionado));

            // Obtengo el ID interno de la tabla tmpOC_items
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            VariablesGlobales.edita_item.IdItemTemporal = (int)Conversion.Int(Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1));

            var frm_infoAgregaItem = new infoagregaitem(false, true, true, idUsuario, idUnico);
            frm_infoAgregaItem.ShowDialog();

            var i = new ItemEntity();
            VariablesGlobales.edita_item = i;

            ordenesCompras.updatePreciosOC(dg_viewOC, txt_subTotal, txt_impuestos, txt_total, txt_totalO);
            subTotalOriginal = Convert.ToDouble(txt_subTotal.Text);
        }

        private void EditarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dg_view_DoubleClick(null, null);
        }

        private void BorrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_viewOC.Rows.Count == 0)
                return;

            string seleccionado;
            string id_tmpOCItem_seleccionado;
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();

            // Obtengo el ID interno de la tabla tmpOC_items
            id_tmpOCItem_seleccionado = Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1);

            ordenesCompras.borraritemCargadoOC(Convert.ToInt32(id_tmpOCItem_seleccionado));

            // Si se borró algún descuento recalcula los descuentos 
            // updateDescuentos()

            ordenesCompras.updatePreciosOC(dg_viewOC, txt_subTotal, txt_impuestos, txt_total, txt_totalO);
            subTotalOriginal = Convert.ToDouble(txt_subTotal.Text);
            // resaltarcolumna(dg_view, 4, Color.Red)
        }

        private void txt_subTotal_TextChanged(object sender, EventArgs e)
        {
            txt_impuestos.Text = Math.Round(Convert.ToDouble(txt_subTotal.Text) * 0.21d, 2).ToString();
        }

        private void txt_impuestos_TextChanged(object sender, EventArgs e)
        {
            double subtotal;
            double iva;

            double.TryParse(txt_subTotal.Text, out subtotal);
            double.TryParse(txt_impuestos.Text, out iva);
            txt_total.Text = (subtotal + iva).ToString();
        }

        private void dg_view_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                {
                    var withBlock = dg_viewOC;
                    var Hitest = withBlock.HitTest(e.X, e.Y);
                    if (Hitest.Type == DataGridViewHitTestType.Cell)
                    {
                        withBlock.CurrentCell = withBlock.Rows[Hitest.RowIndex].Cells[Hitest.ColumnIndex];
                    }
                    cms_general.Visible = true;
                }
            }
        }
        private void updateAndCheck()
        {
            subTotalOriginal = Convert.ToDouble(txt_subTotal.Text);
            if (dg_viewOC.Rows.Count > 0)
            {
                cmd_ok.Enabled = true;
            }
            else
            {
                cmd_ok.Enabled = false;
            }
        }

        private void cmb_comprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void actualizarDataGrid()
        {
            using var context = new CentrexDbContext();

            var query = context.TmpOcItemEntity
                .AsNoTracking()
                .Where(t => t.Activo != false);

            if (VariablesGlobales.edicion)
            {
                query = query.Where(t => t.IdOrdenCompra == VariablesGlobales.edita_ordenCompra.IdOrdenCompra);
            }
            else
            {
                query = query.Where(t => !t.IdOrdenCompra.HasValue);
            }


            var items = (from tmp in query
                         join item in context.ItemEntity.AsNoTracking() on tmp.IdItem equals (int?)item.IdItem into itemJoin
                         from item in itemJoin.DefaultIfEmpty()
                         where !tmp.IdItem.HasValue || item == null || !item.EsMarkup
                         orderby tmp.IdTmpOcitem
                         select new
                         {
                             ID = $"{tmp.IdTmpOcitem}-{tmp.IdItem.GetValueOrDefault()}",
                             id_OCItem = tmp.IdOcItem,
                             Producto = item != null ? item.Descript : tmp.Descript,
                             Cantidad = tmp.Cantidad,
                             Precio = tmp.Precio,
                             CantidadRecibida = tmp.CantidadRecibida ?? 0
                         }).ToList();

            dg_viewOC.DataSource = items;

            if (dg_viewOC.Columns.Contains("id_OCItem"))
                dg_viewOC.Columns["id_OCItem"].HeaderText = "id_OCItem";
            if (dg_viewOC.Columns.Contains("Cantidad"))
                dg_viewOC.Columns["Cantidad"].HeaderText = "Cant.";
            if (dg_viewOC.Columns.Contains("Precio"))
            {
                dg_viewOC.Columns["Precio"].HeaderText = "Precio";
                dg_viewOC.Columns["Precio"].DefaultCellStyle.Format = "N2";
            }
            if (dg_viewOC.Columns.Contains("CantidadRecibida"))
                dg_viewOC.Columns["CantidadRecibida"].HeaderText = "Cantidad Recibida";

            ordenesCompras.updatePreciosOC(dg_viewOC, txt_subTotal, txt_impuestos, txt_total, txt_totalO);
            updateAndCheck();
        }

        private void ModificarArtículoRecibidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string seleccionado;
            string tmpTabla;
            bool tmpActivo;
            int id_item;
            int id_item_tmp;

            // Obtengo el ID interno de la tabla tmpOC_items
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            id_item_tmp = Convert.ToInt32(Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1));

            tmpTabla = VariablesGlobales.tabla;
            tmpActivo = VariablesGlobales.activo;
            VariablesGlobales.tabla = "items_sinDescuento";
            VariablesGlobales.activo = true;

            Enabled = false;
            var srch = new search(false, true, false);
            srch.ShowDialog();
            id_item = VariablesGlobales.id;

            using (var context = new CentrexDbContext())
            {
                var tmpItem = context.TmpOcItemEntity.FirstOrDefault(t => t.IdTmpOcitem == id_item_tmp);
                if (tmpItem is not null)
                {
                    tmpItem.IdItemRecibido = id_item;
                    context.SaveChanges();
                }
            }

            actualizarDataGrid();
        }

        private void ModificarCantidadRecibidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string seleccionado;
            int cantidad_recibida;

            // Obtengo el ID del item
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            seleccionado = Strings.Right(seleccionado, seleccionado.Length - Strings.InStr(seleccionado, "-"));

            VariablesGlobales.edita_item = mitem.info_item((int)Conversion.Int(seleccionado));

            // Obtengo el ID interno de la tabla tmpOC_items
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            VariablesGlobales.edita_item.IdItemTemporal = (int)Conversion.Int(Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1));

            var agregaItem = new infoagregaitem(false, true, false, idUsuario, idUnico);
            agregaItem.ShowDialog();
            cantidad_recibida = agregaItem.cant;

            using (var context = new CentrexDbContext())
            {
                int idTmp = Convert.ToInt32(VariablesGlobales.edita_item.IdItemTemporal);
                var tmpItem = context.TmpOcItemEntity.FirstOrDefault(t => t.IdTmpOcitem == idTmp);
                if (tmpItem is not null)
                {
                    tmpItem.CantidadRecibida = cantidad_recibida;
                    context.SaveChanges();
                }
            }

            actualizarDataGrid();
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };
}

