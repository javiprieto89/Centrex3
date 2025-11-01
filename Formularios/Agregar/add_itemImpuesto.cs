using System;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_itemImpuesto
    {
        public add_itemImpuesto()
        {
            InitializeComponent();
        }

        private void add_modeloa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void add_modeloa_Load(object sender, EventArgs e)
        {
            // Cargo todos los items (EF)
            cmb_items.DataSource = combos_ef_extra.ComboItems();
            cmb_items.DisplayMember = "descript";
            cmb_items.ValueMember = "id_item";
            cmb_items.Text = "Seleccione un item";

            // Cargo todos los impuestos (EF)
            cmb_impuestos.DataSource = combos_ef.ComboImpuestosIVAActivos();
            cmb_impuestos.DisplayMember = "nombre";
            cmb_impuestos.ValueMember = "id_impuesto";
            cmb_impuestos.Text = "Seleccione un impuesto";

            chk_activo.Checked = true;
            if (edicion == true | borrado == true)
            {
                chk_secuencia.Enabled = false;

                {
                    var withBlock = edita_itemImpuesto;
                    cmb_items.SelectedValue = withBlock.IdItem;
                    cmb_impuestos.SelectedValue = withBlock.IdImpuesto;
                    chk_activo.Checked = withBlock.Activo;
                }
            }

            if (borrado == true)
            {
                cmb_items.Enabled = false;
                cmb_impuestos.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta relación?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (itemsImpuestos.borraritemImpuesto(edita_itemImpuesto) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la relación, ¿desea intentar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (itemsImpuestos.updateitemImpuesto(edita_itemImpuesto, borra: true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la relación no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que esta relación tiene operaciones realizadas y/o dependencias por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la relación, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_items.Text == "Seleccione un item")
            {
                Interaction.MsgBox("El campo 'Item' es obligatorio y está vacio");
                return;
            }
            else if (cmb_impuestos.Text == "Seleccione un impuesto")
            {
                Interaction.MsgBox("El campo 'Impuestos' es obligatorio y está vacio");
                return;
            }

            var tmp = new ItemImpuestoEntity();
            tmp.IdItem = Conversions.ToInteger(cmb_items.SelectedValue);
            tmp.IdImpuesto = Conversions.ToInteger(cmb_impuestos.SelectedValue);
            tmp.Activo = chk_activo.Checked;

            if (edicion == true)
            {
                // tmp.IdItem = edita_itemImpuesto.IdItem
                // tmp.IdImpuesto = edita_impuesto.IdImpuesto
                if (itemsImpuestos.updateitemImpuesto(edita_itemImpuesto, tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la relación, consulte con su programador", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                itemsImpuestos.additemImpuesto(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                cmb_items.Text = "Seleccione un item";
                cmb_impuestos.Text = "Seleccione un impuesto";
                chk_activo.Checked = true;
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void pic_search_item_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = tabla;
            tabla = "itemsImpuestosItems";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            // Establezco la opción del combo
            // cmb_items.SelectedIndex = cmb_items.FindString(info_item(id).descript)
            cmb_items.SelectedValue = id;
            id = 0;
        }

        private void pic_search_impuestos_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = tabla;
            tabla = "itemsImpuestosImpuestos";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            // Establezco la opción del combo
            // cmb_impuestos.SelectedIndex = cmb_impuestos.FindString(info_impuesto(id).nombre)
            cmb_impuestos.SelectedValue = id;
            id = 0;
        }

        private void cmb_items_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_impuestos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }
    }
}

