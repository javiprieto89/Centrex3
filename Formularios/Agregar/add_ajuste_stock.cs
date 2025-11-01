using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Centrex
{
    public partial class add_ajuste_stock
    {
        public add_ajuste_stock()
        {
            InitializeComponent();
        }

        private void add_ajuste_stock_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void add_ajuste_stock_Load(object sender, EventArgs e)
        {
            // form = Me ' Comentado para evitar error de compilación

            // Cargo todas los items
            Cargar_Combo(
                 ref cmb_items,
                "Items",
                "Descript",
                "IdItem",
                predet: 0,
                soloActivos: true,
                filtros: new Dictionary<string, object> { { "IdProveedor", 1 } },
                orden: new List<Tuple<string, bool>> { Tuple.Create("Descript", true) }
            );
            cmb_items.Text = "Seleccione un item...";
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            var _as = new AjusteStockEntity();

            if (cmb_items.Text == "Seleccione un item...")
            {
                Interaction.MsgBox("Debe seleccionar un item.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_cantidad.Text))
            {
                Interaction.MsgBox("Debe escribir una cantidad a ajustar.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            _as.Fecha = DateOnly.FromDateTime(DateTime.Now);
            _as.IdItem = Conversions.ToInteger(cmb_items.SelectedValue);
            _as.Cantidad = Conversions.ToInteger(txt_cantidad.Text);
            _as.Notas = txt_notas.Text;

            if (!add_ajusteStock(_as))
            {
                Interaction.MsgBox("Hubo un error al ingresar el ajuste de stock.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
            }
            closeandupdate(this);
        }

        private void pic_search_item_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = tabla;
            // tabla = "items_sinDescuento"
            tabla = "items_registros_stock";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            // Establezco la opción del combo
            // cmb_items.SelectedIndex = cmb_items.FindString(info_item(id).descript)
            cmb_items.SelectedValue = id;
            id = 0;
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Conversions.ToInteger(esNumero(e, true)) == 0)
            {
                e.KeyChar = Conversions.ToChar("");
            }
        }
    }
}
