using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
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
            txt_factor.Text = "1";

            LoadCategorias();
            LoadMarcas();
            LoadProveedores();
            int? impuestoSeleccionado = null;
            if (VariablesGlobales.edicion | VariablesGlobales.borrado)
            {
                impuestoSeleccionado = ObtenerImpuestoAsociado(VariablesGlobales.edita_item_entity.IdItem);
            }
            LoadImpuestosIva(impuestoSeleccionado);

            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_item.Text = VariablesGlobales.edita_item_entity.Item;
                txt_desc.Text = VariablesGlobales.edita_item_entity.Descript;
                txt_cantidad.Text = Conversions.ToString(VariablesGlobales.edita_item_entity.Cantidad.Value);
                txt_costo.Text = VariablesGlobales.edita_item_entity.Costo.ToString();
                txt_prclista.Text = VariablesGlobales.edita_item_entity.PrecioLista.ToString();
                txt_factor.Text = Conversions.ToString(VariablesGlobales.edita_item_entity.Factor.Value);

                cmb_cat.SelectedValue = VariablesGlobales.edita_item_entity.IdTipo;
                cmb_marca.SelectedValue = VariablesGlobales.edita_item_entity.IdMarca;
                cmb_proveedor.SelectedValue = VariablesGlobales.edita_item_entity.IdProveedor;
                cmb_iva.Enabled = false;
                cmb_iva.Text = @"Archivos\Impuestos\Items-Impuestos para modificar";

                chk_activo.Checked = VariablesGlobales.edita_item_entity.Activo;
                chk_descuento.Checked = VariablesGlobales.edita_item_entity.EsDescuento;
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
            else if (!VariablesGlobales.edicion && !VariablesGlobales.borrado)
            {
                cmb_cat.SelectedIndex = -1;
                cmb_cat.Text = "Seleccione una categoría";
                if (VariablesGlobales.id_marca_default > 0)
                {
                    cmb_marca.SelectedValue = VariablesGlobales.id_marca_default;
                }
                else
                {
                    cmb_marca.SelectedIndex = -1;
                }

                if (VariablesGlobales.id_proveedor_default > 0)
                {
                    cmb_proveedor.SelectedValue = VariablesGlobales.id_proveedor_default;
                }
                else
                {
                    cmb_proveedor.SelectedIndex = -1;
                }

                cmb_iva.SelectedIndex = -1;
                cmb_iva.Text = "Seleccione un impuesto...";
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
            if (VariablesGlobales.id > 0)
            {
                cmb_marca.SelectedValue = VariablesGlobales.id;
            }
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
            if (VariablesGlobales.id > 0)
            {
                cmb_proveedor.SelectedValue = VariablesGlobales.id;
            }
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
            if (VariablesGlobales.id > 0)
            {
                cmb_cat.SelectedValue = VariablesGlobales.id;
            }
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
            else if (string.IsNullOrEmpty(txt_factor.Text))
            {
                Interaction.MsgBox("El campo 'Factor' es obligatorio y está vacio");
                return;
            }
            else if (!chk_descuento.Checked && cmb_cat.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar una categoría");
                return;
            }
            else if (!chk_descuento.Checked && cmb_marca.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar una marca");
                return;
            }
            else if (!chk_descuento.Checked && cmb_proveedor.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar un proveedor");
                return;
            }
            else if (!chk_descuento.Checked && cmb_iva.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar un impuesto.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var tmp = new ItemEntity();

            if (chk_descuento.Checked)
            {
                tmp.Costo = 0m;
                tmp.PrecioLista = 0m;
                tmp.IdTipo = 2; // 2 = Descuentos
                tmp.IdMarca = 2; // 2 = Centrex
                tmp.IdProveedor = 2; // 2 = Centrex            
            }
            else
            {
                tmp.Costo = Conversions.ToDecimal(txt_costo.Text);
                tmp.PrecioLista = Conversions.ToDecimal(txt_prclista.Text);
                tmp.IdTipo = Convert.ToInt32(cmb_cat.SelectedValue);
                tmp.IdMarca = Convert.ToInt32(cmb_marca.SelectedValue);
                tmp.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
            }
            tmp.Item = txt_item.Text;
            tmp.Descript = txt_desc.Text;
            tmp.Cantidad = Conversions.ToInteger(txt_cantidad.Text);
            tmp.Factor = Conversions.ToDecimal(txt_factor.Text);
            tmp.Activo = chk_activo.Checked;
            tmp.EsDescuento = chk_descuento.Checked;

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
                ii.IdImpuesto = Convert.ToInt32(cmb_iva.SelectedValue);
                ii.Activo = true;
                mitem.addItemIVA(VariablesGlobales.ConvertToItemImpuesto(ii));
            }

            if (chk_secuencia.Checked == true)
            {
                txt_item.Text = "";
                txt_desc.Text = "";
                txt_cantidad.Text = "1000000";
                txt_costo.Text = "";
                txt_prclista.Text = "";
                txt_factor.Text = "1";
                LoadCategorias();
                LoadMarcas();
                LoadProveedores();
                LoadImpuestosIva(null);
                cmb_cat.SelectedIndex = -1;
                cmb_cat.Text = "Seleccione una categoría";
                cmb_marca.SelectedValue = VariablesGlobales.id_marca_default;
                cmb_proveedor.SelectedValue = VariablesGlobales.id_proveedor_default;
                cmb_iva.SelectedIndex = -1;
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

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };

        private void LoadCategorias()
        {
            var orden = OrdenAsc("Tipo");
            generales.Cargar_Combo(
                ref cmb_cat,
                entidad: "TipoItemEntity",
                displaymember: "Tipo",
                valuemember: "IdTipo",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: orden);
        }

        private void LoadMarcas()
        {
            var orden = OrdenAsc("Marca");
            generales.Cargar_Combo(
                ref cmb_marca,
                entidad: "MarcaItemEntity",
                displaymember: "Marca",
                valuemember: "IdMarca",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: orden);
        }

        private void LoadProveedores()
        {
            var orden = OrdenAsc("RazonSocial");
            generales.Cargar_Combo(
                ref cmb_proveedor,
                entidad: "ProveedorEntity",
                displaymember: "RazonSocial",
                valuemember: "IdProveedor",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: orden);
        }

        private void LoadImpuestosIva(int? impuestoSeleccionado)
        {
            using var ctx = new CentrexDbContext();
            var datos = ctx.ImpuestoEntity
                .AsNoTracking()
                .Where(i => i.Activo && EF.Functions.Like(i.Nombre, "%IVA%"))
                .OrderBy(i => i.Nombre)
                .Select(i => new KeyValuePair<int, string>(i.IdImpuesto, i.Nombre))
                .ToList();

            var dt = combos_ef.ToDataTable(datos, "id_impuesto", "nombre");
            cmb_iva.DataSource = dt;
            cmb_iva.DisplayMember = "nombre";
            cmb_iva.ValueMember = "id_impuesto";

            if (impuestoSeleccionado.HasValue && datos.Exists(d => d.Key == impuestoSeleccionado.Value))
            {
                cmb_iva.SelectedValue = impuestoSeleccionado.Value;
            }
            else
            {
                cmb_iva.SelectedIndex = -1;
                cmb_iva.Text = "Seleccione un impuesto...";
            }
        }

        private int? ObtenerImpuestoAsociado(int idItem)
        {
            using var ctx = new CentrexDbContext();
            return ctx.ItemImpuestoEntity
                .AsNoTracking()
                .Where(ii => ii.IdItem == idItem && ii.Activo)
                .Select(ii => (int?)ii.IdImpuesto)
                .FirstOrDefault();
        }
    }
}
