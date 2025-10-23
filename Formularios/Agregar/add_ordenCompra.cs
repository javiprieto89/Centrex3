using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_ordenCompra
    {

        private double totalOriginal;
        private double subTotalOriginal;
        public comprobante comprobanteSeleccionado;
        private int nOC = -1;
        private int idUsuario;
        private string idUnico;

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
            VariablesGlobales.agregaitem = false;
            actualizarDataGrid();
        }

        private void add_ordenCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            VariablesGlobales.id = 0;
            var oc = new ordenCompra();
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
            string sqlstr;
            var cli = new cliente();
            // form = Me ' Comentado para evitar error de compilación

            // Cargo el combo con todos los proveedores
            sqlstr = "SELECT p.id_proveedor AS 'id_proveedor', p.razon_social AS 'razon_social' FROM proveedores AS p WHERE p.activo = '1' ORDER BY p.razon_social ASC";
            var argcombo = cmb_proveedor;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "razon_social", Conversions.ToInteger("id_proveedor"));
            cmb_proveedor = argcombo;

            txt_subTotal.TextChanged -= new EventHandler(txt_subTotal_TextChanged);
            txt_impuestos.TextChanged -= new EventHandler(txt_impuestos_TextChanged);

            if (VariablesGlobales.edicion | VariablesGlobales.borrado)
            {
                // cargo fecha de la orden de compra
                // cargo cliente de la orden de compra
                // cargo items
                // cargo total
                // inhabilito carga secuencial

                lbl_fechaCarga.Text = Conversions.ToString(DateTime.Parse(Conversions.ToString(VariablesGlobales.edita_ordenCompra.fecha_carga.Value)));
                dtp_fechaComprobante.Value = DateTime.Parse(Conversions.ToString(VariablesGlobales.edita_ordenCompra.fecha_comprobante.Value));

                var p = new proveedor();
                p = proveedores.info_proveedor(VariablesGlobales.edita_ordenCompra.IdProveedor.ToString());

                cmb_proveedor.SelectedValue = VariablesGlobales.edita_ordenCompra.IdProveedor;

                actualizarDataGrid();

                txt_subTotal.Text = VariablesGlobales.edita_ordenCompra.subtotal.ToString();
                txt_impuestos.Text = VariablesGlobales.edita_ordenCompra.iva.ToString();
                txt_total.Text = VariablesGlobales.edita_ordenCompra.total.ToString();
                txt_totalO.Text = txt_total.Text;
                txt_nota.Text = VariablesGlobales.edita_ordenCompra.notas;
                chk_secuencia.Enabled = false;
                subTotalOriginal = Conversions.ToDouble(txt_subTotal.Text);

                lbl_nOrdenCompra.Text = VariablesGlobales.edita_ordenCompra.id_ordenCompra.ToString();
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
            }

            txt_subTotal.TextChanged += new EventHandler(txt_subTotal_TextChanged);
            txt_impuestos.TextChanged += new EventHandler(txt_impuestos_TextChanged);

            if (dg_viewOC.Rows.Count > 0)
            {
                cmd_ok.Enabled = true;
            }
            else
            {
                cmd_ok.Enabled = false;
            }

            if (VariablesGlobales.edita_ordenCompra.id_ordenCompra != 0 | VariablesGlobales.borrado == true)
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
            ordenCompra ultimaOC = null;

            if (cmb_proveedor.Text == "Seleccione un cliente" | string.IsNullOrEmpty(cmb_proveedor.Text))
            {
                Interaction.MsgBox("El campo 'Cliente' es obligatorio y está vacio");
                return;
            }
            else if (dg_viewOC.Rows.Count == 0)
            {
                Interaction.MsgBox("No hay items cargados");
                return;
            }

            var oc = new ordenCompra();

            oc.IdProveedor = Conversions.ToInteger(cmb_proveedor.SelectedValue);
            oc.fecha_carga = lbl_fechaCarga.Text;
            oc.fecha_comprobante = Conversions.ToString(dtp_fechaComprobante.Value.Date);
            if (lbl_fechaRecepcion.Text != "%fecha_recepcion%")
                oc.fecha_recepcion = lbl_fechaRecepcion.Text;
            oc.subtotal = Conversions.ToDouble(txt_subTotal.Text);
            oc.iva = Conversions.ToDouble(txt_impuestos.Text);
            oc.total = Conversions.ToDouble(txt_total.Text);
            oc.notas = txt_nota.Text;
            oc.activo = true;

            if (VariablesGlobales.edicion == true)
            {
                // actualizar proveedor
                oc.id_ordenCompra = VariablesGlobales.edita_ordenCompra.id_ordenCompra;
                ultimaOC = oc;
                if (ordenesCompras.updateOrdenCompra(oc) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la orden de compra.", Constants.vbExclamation);
                    closeandupdate(this);
                }
                // actualizar orde de compra (items)            
                // actualizo, agrego y borro los items de la orden de compra
                ordenesCompras.guardarOrdenCompra(VariablesGlobales.edita_ordenCompra.id_ordenCompra);
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

            // Establezco la opción del combo, si es 0 elijo el cliente default
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

            VariablesGlobales.edita_item = mitem.info_item(seleccionado);

            // Obtengo el ID interno de la tabla tmpOC_items
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            VariablesGlobales.edita_item.id_item_temporal = Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1);

            var frm_infoAgregaItem = new infoagregaitem(false, true, true, idUsuario, idUnico);
            frm_infoAgregaItem.ShowDialog();

            var i = new ItemEntity();
            VariablesGlobales.edita_item = i;

            ordenesCompras.updatePreciosOC(dg_viewOC, txt_subTotal, txt_impuestos, txt_total, txt_totalO);
            subTotalOriginal = Conversions.ToDouble(txt_subTotal.Text);
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

            ordenesCompras.borraritemCargadoOC(Conversions.ToInteger(id_tmpOCItem_seleccionado));

            // Si se borró algún descuento recalcula los descuentos 
            // updateDescuentos()

            ordenesCompras.updatePreciosOC(dg_viewOC, txt_subTotal, txt_impuestos, txt_total, txt_totalO);
            subTotalOriginal = Conversions.ToDouble(txt_subTotal.Text);
            // resaltarcolumna(dg_view, 4, Color.Red)
        }

        private void txt_subTotal_TextChanged(object sender, EventArgs e)
        {
            txt_impuestos.Text = Math.Round(Conversions.ToDouble(txt_subTotal.Text) * 0.21d, 2).ToString();
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
            subTotalOriginal = Conversions.ToDouble(txt_subTotal.Text);
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

        private void cmb_cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void actualizarDataGrid()
        {
            string sqlstr;

            sqlstr = "SELECT CONCAT(ti.id_tmpOCItem, '-', ti.id_item) AS 'ID', ti.id_OCItem AS 'id_OCItem', " + "CASE WHEN i.id_item IS NULL THEN ti.descript ELSE i.descript END AS 'Producto', " + "ti.cantidad AS 'Cant.', ti.precio AS 'Precio', " + "ti.cantidad_recibida AS 'Cantidad Recibida' " + "FROM tmpOC_items AS ti " + "LEFT JOIN items AS i ON ti.id_item = i.id_item " + "WHERE ti.activo = '1' AND (i.esMarkup = '0' OR ti.id_item IS NULL) " + "ORDER BY id ASC";
            var argdataGrid = dg_viewOC;
            int argnRegs = 0;
            int argtPaginas = 0;
            TextBox argtxtnPage = null;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, 0, ref argnRegs, ref argtPaginas, 1, ref argtxtnPage, "", "");
            dg_viewOC = argdataGrid;
            ordenesCompras.updatePreciosOC(dg_viewOC, txt_subTotal, txt_impuestos, txt_total, txt_totalO);
            updateAndCheck();
        }

        private void ModificarArtículoRecibidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string seleccionado;
            string sqlstr;
            string tmpTabla;
            bool tmpActivo;
            int id_item;
            int id_item_tmp;

            // Obtengo el ID interno de la tabla tmpOC_items
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            id_item_tmp = Conversions.ToInteger(Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1));

            tmpTabla = VariablesGlobales.tabla;
            tmpActivo = VariablesGlobales.activo;
            VariablesGlobales.tabla = "items_sinDescuento";
            VariablesGlobales.activo = true;

            Enabled = false;
            var srch = new search(false, true, false);
            srch.ShowDialog();
            id_item = VariablesGlobales.id;

            sqlstr = "UPDATE tmpOC_items SET id_item_recibido = '" + id_item.ToString() + "' WHERE id_tmpOCItem = '" + id_item_tmp.ToString() + "'";
            ejecutarSQL(sqlstr);

            actualizarDataGrid();
        }

        private void ModificarCantidadRecibidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string seleccionado;
            string sqlstr;
            int cantidad_recibida;

            // Obtengo el ID del item
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            seleccionado = Strings.Right(seleccionado, seleccionado.Length - Strings.InStr(seleccionado, "-"));

            VariablesGlobales.edita_item = mitem.info_item(seleccionado);

            // Obtengo el ID interno de la tabla tmpOC_items
            seleccionado = dg_viewOC.CurrentRow.Cells[0].Value.ToString();
            VariablesGlobales.edita_item.id_item_temporal = Strings.Left(seleccionado, Strings.InStr(seleccionado, "-") - 1);

            var agregaItem = new infoagregaitem(false, true, false, idUsuario, idUnico);
            agregaItem.ShowDialog();
            cantidad_recibida = agregaItem.cant;

            sqlstr = "UPDATE tmpproduccion_items SET cantidad_recibida = '" + cantidad_recibida.ToString() + "' WHERE id_tmpProduccionItem = '" + VariablesGlobales.edita_item.id_item_temporal.ToString + "'";
            ejecutarSQL(sqlstr);

            actualizarDataGrid();
        }
    }
}

