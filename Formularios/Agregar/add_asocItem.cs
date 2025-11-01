using System;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_asocItem
    {
        private AsocItemEntity iAsoc = new AsocItemEntity();
        private ItemEntity item = new ItemEntity();
        private ItemEntity asocItem = new ItemEntity();

        public add_asocItem()
        {
            InitializeComponent();
        }

        private void pic_searchItem_Click(object sender, EventArgs e)
        {
            string tmpTabla;
            bool tmpActivo;

            tmpTabla = tabla;
            tmpActivo = activo;
            tabla = "asocItem_Item";
            activo = true;

            agregaitem = false;
            Enabled = false;

            My.MyProject.Forms.search.ShowDialog();
            tabla = tmpTabla;
            activo = tmpActivo;
            agregaitem = false;

            iAsoc.IdItem = edita_item.IdItem;
            txt_item.Text = edita_item.IdItem + " - " + edita_item.Item;
            txt_descriptItem.Text = edita_item.Descript;
        }

        private void add_asocItem_Load(object sender, EventArgs e)
        {
            // form = Me ' Comentado para evitar error de compilación
            {
                ref var withBlock = ref iAsoc;
                withBlock.IdItem = -1;
                withBlock.IdItemAsoc = -1;
                withBlock.Cantidad = -99;
            }

            if (edicion == true || borrado == true)
            {
                // Deshabilito controles comunes 
                txt_item.Enabled = false;
                txt_descriptItem.Enabled = false;
                pic_searchItem.Enabled = false;
                chk_secuencia.Checked = false;
                chk_secuencia.Enabled = false;

                item = mitem.info_item(edita_asocItem.IdItem);
                asocItem = mitem.info_item(edita_asocItem.IdItemAsoc);

                txt_item.Text = item.IdItem + " - " + item.Item;
                txt_descriptItem.Text = item.Descript;

                txt_asocItem.Text = asocItem.IdItem + " - " + asocItem.Item;
                txt_descriptAsocItem.Text = asocItem.Descript;

                txt_cantidad.Text = edita_asocItem.Cantidad.ToString();

                iAsoc.IdItem = edita_asocItem.IdItem;
                iAsoc.IdItemAsoc = edita_asocItem.IdItemAsoc;
                iAsoc.Cantidad = edita_asocItem.Cantidad;

                if (borrado)
                {
                    txt_asocItem.Enabled = false;
                    txt_descriptAsocItem.Enabled = false;
                    txt_cantidad.Enabled = false;
                    cmd_ok.Enabled = false;
                    Show();
                    if (MessageBox.Show("¿Está seguro que desea borrar esta relación?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (asocitems.borrarAsocItem(edita_asocItem) == false)
                        {
                            MessageBox.Show("No se ha podido borrar la relación de los artículos, consulte con el programador", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    closeandupdate(this);
                }
            }
        }

        private void pic_searchAsocItem_Click(object sender, EventArgs e)
        {
            string tmpTabla;
            bool tmpActivo;

            tmpTabla = tabla;
            tmpActivo = activo;
            tabla = "asocItem_Item";
            activo = true;

            agregaitem = false;
            Enabled = false;

            My.MyProject.Forms.search.ShowDialog();
            tabla = tmpTabla;
            activo = tmpActivo;
            agregaitem = false;

            iAsoc.IdItemAsoc = edita_item.IdItem;
            txt_asocItem.Text = edita_item.IdItem + " - " + edita_item.Item;
            txt_descriptAsocItem.Text = edita_item.Descript;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_cantidad.Text))
                iAsoc.Cantidad = int.Parse(txt_cantidad.Text);

            if (iAsoc.IdItem == -1)
            {
                MessageBox.Show("Debe ingresar un item al que se le asignaran el resto de los productos", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (iAsoc.IdItemAsoc == -1)
            {
                MessageBox.Show("Debe ingresar un item asociado al producto maestro", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (iAsoc.Cantidad == -99)
            {
                MessageBox.Show("Debe asignar la cantidad de items que componen al producto maestro", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (edicion == true)
            {
                if (asocitems.updateAsocItem(iAsoc) == false)
                {
                    MessageBox.Show("Hubo un problema al actualizar el cliente, consulte con su programador", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                asocitems.addAsocItem(iAsoc);
            }

            if (chk_secuencia.Checked == true)
            {
                iAsoc.IdItemAsoc = -1;
                iAsoc.Cantidad = -99;
                txt_item.Enabled = false;
                txt_descriptItem.Enabled = false;
                pic_searchItem.Enabled = false;
                txt_asocItem.Text = "";
                txt_descriptAsocItem.Text = "";
                txt_cantidad.Text = 0.ToString();
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!generales.esNumero(e))
            {
                e.KeyChar = '\0';
            }
        }

        private void add_asocItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }
    }
}


