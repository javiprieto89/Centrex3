using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_item
    {
        public add_item()
        {
            InitializeComponent();
        }
        private void add_item_Load(object sender, EventArgs e)
        {
            // form = Me ' Comentado para evitar error de compilación

            chk_activo.Checked = true;
            txt_factor.Text = 1.ToString();

            cmb_cat.DataSource = combos_alias.ComboCategoriasItems();
            cmb_cat.DisplayMember = "tipo";
            cmb_cat.ValueMember = "IdTipo";
            cmb_marca.DataSource = combos_ef.ComboMarcas();
            cmb_marca.DisplayMember = "marca";
            cmb_marca.ValueMember = "IdMarca";
            cmb_proveedor.DataSource = combos_ef.ComboProveedores();
            cmb_proveedor.DisplayMember = "razon_social";
            cmb_proveedor.ValueMember = "IdProveedor";
            if (!VariablesGlobales.edicion & !VariablesGlobales.borrado)
            {
                cmb_iva.DataSource = combos_ef.ComboImpuestosIVAActivos();
                cmb_iva.DisplayMember = "nombre";
                cmb_iva.ValueMember = "id_impuesto";
            }
            else
            {
                cmb_iva.Text = @"Archivos\Impuestos\Items-Impuestos para modificar";
            }


            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_item.Text = VariablesGlobales.edita_item_entity.item;
                txt_desc.Text = VariablesGlobales.edita_item_entity.descript;
                txt_cantidad.Text = Conversions.ToString(VariablesGlobales.edita_item_entity.cantidad.Value);
                txt_costo.Text = VariablesGlobales.edita_item_entity.costo.ToString();
                txt_prclista.Text = VariablesGlobales.edita_item_entity.PrecioLista;
                txt_factor.Text = Conversions.ToString(VariablesGlobales.edita_item_entity.factor.Value);

                cmb_cat.SelectedValue = (byte)VariablesGlobales.edita_item_entity.IdTipo;
                cmb_marca.SelectedValue = (byte)VariablesGlobales.edita_item_entity.IdMarca;
                cmb_proveedor.SelectedValue = (byte)VariablesGlobales.edita_item_entity.IdProveedor;
                cmb_iva.Enabled = false;

                chk_activo.Checked = VariablesGlobales.edita_item_entity.activo;
                chk_descuento.Checked = VariablesGlobales.edita_item_entity.esDescuento;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_item.Enabled = false;
                txt_desc.Enabled = false;
                txt_costo.Enabled = false;
                txt_prclista.Enabled = false;
                txt_factor.Enabled = false;
                txt_cantidad.Enabled = false;
                cmb_cat.Enabled = false;
                cmb_marca.Enabled = false;
                cmb_proveedor.Enabled = false;
                chk_activo.Enabled = false;
                chk_descuento.Enabled = false;
                psearch_marca.Enabled = false;
                psearch_proveedor.Enabled = false;
                psearch_tipo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este item?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (mitem.borraritem(VariablesGlobales.edita_item_entity) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del item, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (mitem.updateitem(VariablesGlobales.edita_item_entity) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el item no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el item, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar el item, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
            else if (VariablesGlobales.edicion == false)
            {
                cmb_cat.Text = "Seleccione una categoría";
                // cmb_marca.Text = "Seleccione una marca"
                // cmb_proveedor.Text = "Seleccione un proveedor"
                cmb_marca.SelectedValue = 1;
                cmb_proveedor.SelectedValue = 1;
            }
        }

        private void psearch_marca_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "marcas_items";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo
            cmb_marca.SelectedIndex = cmb_marca.FindString(marcasitems.info_marcai(VariablesGlobales.id).marca);
            // cmb_marca.SelectedIndex = id
            VariablesGlobales.id = 0;
        }

        private void psearch_proveedor_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo
            cmb_proveedor.SelectedIndex = cmb_proveedor.FindString(proveedores.info_proveedor(VariablesGlobales.id.ToString()).razon_social);
            // cmb_proveedor.SelectedIndex = id
            VariablesGlobales.id = 0;
        }

        private void psearch_tipo_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "tipos_items";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo
            cmb_cat.SelectedIndex = cmb_cat.FindString(tipositems.InfoTipoItem(VariablesGlobales.id).tipo);
            // cmb_cat.SelectedIndex = id
            VariablesGlobales.id = 0;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_item.Text))
            {
                Interaction.MsgBox("El campo 'Código' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_desc.Text))
            {
                Interaction.MsgBox("El campo 'Producto' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_costo.Text) & chk_descuento.Checked == false)
            {
                Interaction.MsgBox("El campo 'Costo' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_prclista.Text) & chk_descuento.Checked == false)
            {
                Interaction.MsgBox("El campo 'Precio de lista' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(lbl_factor.Text))
            {
                Interaction.MsgBox("El campo 'Factor' es obligatorio y está vacio");
                return;
            }
            else if (cmb_cat.Text == "Seleccione una categoría" & chk_descuento.Checked == false)
            {
                Interaction.MsgBox("Debe seleccionar una categoría");
                return;
            }
            else if (cmb_marca.Text == "Seleccione una marca" & chk_descuento.Checked == false)
            {
                Interaction.MsgBox("Debe seleccionar una marca");
                return;
            }
            else if (cmb_proveedor.Text == "Seleccione un proveedor" & chk_descuento.Enabled == false)
            {
                Interaction.MsgBox("Debe seleccionar un proveedor");
                return;
            }

            var tmp = new ItemEntity();

            if (chk_descuento.Checked)
            {
                tmp.costo = 0m;
                tmp.PrecioLista = 0;
                tmp.IdTipo = 2; // 2 = Descuentos
                tmp.IdMarca = 2; // 2 = Centrex
                tmp.IdProveedor = 2; // 2 = Centrex            
            }
            else
            {
                tmp.costo = Conversions.ToDecimal(txt_costo.Text);
                tmp.PrecioLista = txt_prclista.Text;
                tmp.IdTipo = Conversions.ToInteger(cmb_cat.SelectedItem(0).ToString());
                tmp.IdMarca = Conversions.ToInteger(cmb_marca.SelectedItem(0).ToString());
                tmp.IdProveedor = Conversions.ToInteger(cmb_proveedor.SelectedItem(0).ToString());
            }
            tmp.item = txt_item.Text;
            tmp.descript = txt_desc.Text;
            tmp.cantidad = Conversions.ToInteger(txt_cantidad.Text);
            tmp.factor = Conversions.ToDecimal(txt_factor.Text);
            tmp.activo = chk_activo.Checked;
            tmp.esDescuento = chk_descuento.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdItem = VariablesGlobales.edita_item_entity.IdItem;
                if (mitem.updateitem(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el item, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                mitem.additem(tmp);
                var ii = new ItemImpuestoEntity();
                var entUlt = mitem.infoItem_lastItem();
                ii.IdItem = entUlt.IdItem;
                ii.IdImpuesto = cmb_iva.SelectedValue;
                ii.activo = true;
                mitem.addItemIVA(VariablesGlobales.ConvertToItemImpuesto(ii));
            }

            if (chk_secuencia.Checked == true)
            {
                txt_item.Text = "";
                txt_desc.Text = "";
                txt_cantidad.Text = "1000000";
                txt_costo.Text = "";
                txt_prclista.Text = "";
                txt_factor.Text = 1.ToString();
                var argcombo = cmb_cat;
                generales.Cargar_Combo(ref argcombo, "SELECT IdTipo, tipo FROM tipos_items ORDER BY tipo ASC", VariablesGlobales.basedb, "tipo", Conversions.ToInteger("IdTipo"));
                cmb_cat = argcombo;
                cmb_cat.Text = "Seleccione una categoría";
                var argcombo1 = cmb_marca;
                generales.Cargar_Combo(ref argcombo1, "SELECT IdMarca, marca FROM marcas_items ORDER BY marca ASC", VariablesGlobales.basedb, "marca", Conversions.ToInteger("IdMarca"));
                cmb_marca = argcombo1;
                cmb_marca.SelectedValue = VariablesGlobales.id_marca_default;
                var argcombo2 = cmb_proveedor;
                generales.Cargar_Combo(ref argcombo2, "SELECT IdProveedor, razon_social FROM proveedores ORDER BY razon_social ASC", VariablesGlobales.basedb, "razon_social", Conversions.ToInteger("IdProveedor"));
                cmb_proveedor = argcombo2;
                cmb_proveedor.SelectedValue = VariablesGlobales.id_proveedor_default;
                chk_activo.Checked = true;
                chk_descuento.Checked = false;
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
            // add_item_FormClosed(Nothing, Nothing)
        }

        private void cmb_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If LCase(cmb_cat.SelectedItem(1).ToString) = "rosca" Then
            // cmb_rosca.Enabled = True
            // psearch_rosca.Enabled = True
            // cargar_combo(cmb_rosca, "SELECT i.id_item AS 'ID', i.item AS 'Código' FROM items AS i " & _
            // "LEFT JOIN tipos_items AS ti ON i.IdTipo = ti.IdTipo " & _
            // "WHERE ti.tipo = 'rosca' ", basedb, "Item", "ID")
            // Else
            // cmb_rosca.Enabled = False
            // psearch_rosca.Enabled = False
            // cmb_rosca.Items.Clear()
            // End If
        }

        private void txt_costo_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_costo.Text) & chk_descuento.Checked == false)
            {
                txt_prclista.Text = (Conversions.ToDouble(txt_costo.Text) * Conversions.ToDouble(txt_factor.Text)).ToString();
            }
        }

        private void txt_factor_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_costo.Text) & chk_descuento.Checked == false)
            {
                txt_prclista.Text = (Conversions.ToDouble(txt_costo.Text) * Conversions.ToDouble(txt_factor.Text)).ToString();
            }
        }

        private void chk_descuento_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_descuento.Checked == true)
            {
                cmb_cat.SelectedValue = 2; // 2 = Descuentos
                cmb_marca.SelectedValue = 2; // 2 = Centrex
                cmb_proveedor.SelectedValue = 2; // 2 = Centrex
                                                 // txt_cantidad.Text = 1000000

                txt_costo.Enabled = false;
                txt_prclista.Enabled = false;
                cmb_cat.Enabled = false;
                cmb_marca.Enabled = false;
                cmb_proveedor.Enabled = false;
            }
            // txt_cantidad.Enabled = True
            else
            {
                txt_costo.Enabled = true;
                txt_prclista.Enabled = true;
                cmb_cat.Enabled = true;
                cmb_marca.Enabled = true;
                cmb_proveedor.Enabled = true;
                // txt_cantidad.Enabled = False
            }
        }

        private void pic_search_iva_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "itemIVA";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo
            // cmb_impuestos.SelectedIndex = cmb_impuestos.FindString(info_impuesto(id).nombre)
            cmb_iva.SelectedValue = VariablesGlobales.id;
            VariablesGlobales.id = 0;
        }

        private void cmb_cat_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_marca_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_iva_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }
    }
}
