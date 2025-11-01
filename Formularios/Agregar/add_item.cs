using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            chk_activo.Checked = true;
            txt_factor.Text = "1";

            LoadCategorias();
            LoadMarcas();
            LoadProveedores();
            int? impuestoSeleccionado = null;
            if (edicion || borrado)
            {
                impuestoSeleccionado = ObtenerImpuestoAsociado(edita_item_entity.IdItem);
            }
            LoadImpuestosIva(impuestoSeleccionado);

            if (edicion == true || borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_item.Text = edita_item_entity.Item;
                txt_desc.Text = edita_item_entity.Descript;
                txt_cantidad.Text = edita_item_entity.Cantidad.Value.ToString();
                txt_costo.Text = edita_item_entity.Costo.ToString();
                txt_prclista.Text = edita_item_entity.PrecioLista.ToString();
                txt_factor.Text = edita_item_entity.Factor.Value.ToString();

                cmb_cat.SelectedValue = edita_item_entity.IdTipo;
                cmb_marca.SelectedValue = edita_item_entity.IdMarca;
                cmb_proveedor.SelectedValue = edita_item_entity.IdProveedor;
                cmb_iva.Enabled = false;
                cmb_iva.Text = @"Archivos\Impuestos\Items-Impuestos para modificar";

                chk_activo.Checked = edita_item_entity.Activo;
                chk_descuento.Checked = edita_item_entity.EsDescuento;
            }

            if (borrado == true)
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
                if (MessageBox.Show("¿Está seguro que desea borrar este item?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (mitem.borraritem(edita_item_entity) == false)
                    {
                        if (MessageBox.Show("Ocurrió un error al realizar el borrado del item, ¿desea intentar desactivarlo para que no aparezca en la búsqueda?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Realizo un borrado lógico
                            if (mitem.updateitem(edita_item_entity) == true)
                            {
                                MessageBox.Show("Se ha podido realizar un borrado lógico, pero el item no se borró definitivamente.\r\nEsto posiblemente se deba a que el item tiene operaciones realizadas y por lo tanto no podrá borrarse", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido borrar el item, consulte con el programador", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
            else if (!edicion && !borrado)
            {
                cmb_cat.SelectedIndex = -1;
                cmb_cat.Text = "Seleccione una categoría";
                if (id_marca_default > 0)
                {
                    cmb_marca.SelectedValue = id_marca_default;
                }
                else
                {
                    cmb_marca.SelectedIndex = -1;
                }

                if (id_proveedor_default > 0)
                {
                    cmb_proveedor.SelectedValue = id_proveedor_default;
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
            tmp = tabla;
            tabla = "marcas_items";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            if (id > 0)
            {
                cmb_marca.SelectedValue = id;
            }
            id = 0;
        }

        private void psearch_proveedor_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = tabla;
            tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            if (id > 0)
            {
                cmb_proveedor.SelectedValue = id;
            }
            id = 0;
        }

        private void psearch_tipo_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = tabla;
            tabla = "tipos_items";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            if (id > 0)
            {
                cmb_cat.SelectedValue = id;
            }
            id = 0;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_item.Text))
            {
                MessageBox.Show("El campo 'Código' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("El campo 'Producto' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_costo.Text) && chk_descuento.Checked == false)
            {
                MessageBox.Show("El campo 'Costo' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_prclista.Text) && chk_descuento.Checked == false)
            {
                MessageBox.Show("El campo 'Precio de lista' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_factor.Text))
            {
                MessageBox.Show("El campo 'Factor' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!chk_descuento.Checked && cmb_cat.SelectedValue is null)
            {
                MessageBox.Show("Debe seleccionar una categoría", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!chk_descuento.Checked && cmb_marca.SelectedValue is null)
            {
                MessageBox.Show("Debe seleccionar una marca", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!chk_descuento.Checked && cmb_proveedor.SelectedValue is null)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!chk_descuento.Checked && cmb_iva.SelectedValue is null)
            {
                MessageBox.Show("Debe seleccionar un impuesto.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                tmp.Costo = decimal.Parse(txt_costo.Text);
                tmp.PrecioLista = decimal.Parse(txt_prclista.Text);
                tmp.IdTipo = Convert.ToInt32(cmb_cat.SelectedValue);
                tmp.IdMarca = Convert.ToInt32(cmb_marca.SelectedValue);
                tmp.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
            }
            tmp.Item = txt_item.Text;
            tmp.Descript = txt_desc.Text;
            tmp.Cantidad = int.Parse(txt_cantidad.Text);
            tmp.Factor = decimal.Parse(txt_factor.Text);
            tmp.Activo = chk_activo.Checked;
            tmp.EsDescuento = chk_descuento.Checked;

            if (edicion == true)
            {
                tmp.IdItem = edita_item_entity.IdItem;
                if (mitem.updateitem(tmp) == false)
                {
                    MessageBox.Show("Hubo un problema al actualizar el item, consulte con su programador", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                mitem.addItemIVA(ConvertToItemImpuesto(ii));
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
                cmb_marca.SelectedValue = id_marca_default;
                cmb_proveedor.SelectedValue = id_proveedor_default;
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
        }

        private void cmb_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txt_costo_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_costo.Text) && chk_descuento.Checked == false)
            {
                txt_prclista.Text = (double.Parse(txt_costo.Text) * double.Parse(txt_factor.Text)).ToString();
            }
        }

        private void txt_factor_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_costo.Text) && chk_descuento.Checked == false)
            {
                txt_prclista.Text = (double.Parse(txt_costo.Text) * double.Parse(txt_factor.Text)).ToString();
            }
        }

        private void chk_descuento_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_descuento.Checked == true)
            {
                cmb_cat.SelectedValue = 2; // 2 = Descuentos
                cmb_marca.SelectedValue = 2; // 2 = Centrex
                cmb_proveedor.SelectedValue = 2; // 2 = Centrex

                txt_costo.Enabled = false;
                txt_prclista.Enabled = false;
                cmb_cat.Enabled = false;
                cmb_marca.Enabled = false;
                cmb_proveedor.Enabled = false;
            }
            else
            {
                txt_costo.Enabled = true;
                txt_prclista.Enabled = true;
                cmb_cat.Enabled = true;
                cmb_marca.Enabled = true;
                cmb_proveedor.Enabled = true;
            }
        }

        private void pic_search_iva_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = tabla;
            tabla = "itemIVA";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            cmb_iva.SelectedValue = id;
            id = 0;
        }

        private void cmb_cat_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmb_marca_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmb_iva_KeyPress(object sender, KeyPressEventArgs e)
        {
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
