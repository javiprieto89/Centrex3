using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex
{

    public partial class add_stock
    {
        public bool ingreso_guardado;
        private CentrexDbContext ctx = new CentrexDbContext();

        public add_stock()
        {
            InitializeComponent();
        }

        private void add_stock_Load(object sender, EventArgs e)
        {
            // Cargar proveedores con EF
            var proveedores = ctx.ProveedorEntity.Where(p => p.Activo == true).OrderBy(p => p.RazonSocial).ToList();

            cmb_proveedor.DataSource = proveedores;
            cmb_proveedor.DisplayMember = "RazonSocial";
            cmb_proveedor.ValueMember = "IdProveedor";

            if (edicion)
            {
                txt_fecha.Text = DateTime.Parse(Conversions.ToString(edita_registro_stock.Fecha.Value)).ToString("dd/MM/yyyy");
                lbl_fecha_ingreso.Text = edita_registro_stock.FechaIngreso.ToString("dd/MM/yyyy");
                txt_factura.Text = edita_registro_stock.Factura;
                cmb_proveedor.SelectedValue = (byte)edita_registro_stock.IdProveedor;

                if (!editaStock)
                {
                    txt_fecha.Enabled = false;
                    txt_factura.Enabled = false;
                    txt_notas.Enabled = false;
                    cmb_proveedor.Enabled = false;
                    psearch_proveedor.Enabled = false;
                    cmd_additem.Enabled = false;
                }
            }
            else
            {
                txt_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lbl_fecha_ingreso.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // Valor por defecto si existe un proveedor base
                try
                {
                    cmb_proveedor.SelectedValue = 20;
                }
                catch
                {
                }

                // Limpiar tabla temporal
                var tmp = ctx.TmpRegistroStockEntity.ToList();
                if (tmp.Any())
                {
                    ctx.TmpRegistroStockEntity.RemoveRange(tmp);
                    ctx.SaveChanges();
                }
            }

            updateform();
        }

        private void add_stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ingreso_guardado)
                return;
            if (!edicion)
            {
                if (Interaction.MsgBox("Eliminará el ingreso, por lo cual no se contabilizará. ¿Deseá continuar?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == Constants.vbNo)
                {
                    e.Cancel = true;
                }
            }
        }

        // ==========================================================
        // Refresca el contenido del DataGridView
        // ==========================================================
        private void updateform()
        {
            object data;

            if (edicion && editaStock)
            {
                data = ctx.RegistroStockEntity.Include("Item").Where(rs => rs.IdIngreso == edita_registro_stock.IdIngreso).Select(rs => new
                {
                    ID = rs.IdRegistro,
                    Código = rs.IdItemNavigation.Item,
                    Producto = rs.IdItemNavigation.Descript,
                    rs.Factura,
                    rs.Fecha,
                    rs.Cantidad,
                    rs.Costo,
                    rs.PrecioLista,
                    rs.Factor,
                    Notas = rs.Nota
                }).ToList();
            }
            else
            {
                data = ctx.TmpRegistroStockEntity.Include("Item").Where(rst => rst.Activo == true).Select(rst => new
                {
                    ID = rst.IdRegistrotmp,
                    Código = rst.IdItemNavigation.Item,
                    Producto = rst.IdItemNavigation.Descript,
                    Factura = rst.Factura,
                    Fecha = rst.Fecha,
                    Cantidad = rst.Cantidad,
                    Costo = rst.Costo,
                    rst.PrecioLista,
                    Factor = rst.Factor,
                    Notas = rst.Nota
                }).ToList();
            }

            dg_view.AutoGenerateColumns = true;
            dg_view.DataSource = data;
        }

        // ==========================================================
        // Guarda el ingreso (trasladando tmp → registros_stock)
        // ==========================================================
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (!edicion)
            {
                if (Interaction.MsgBox("Se modificarán todos los items anteriormente cargados. ¿Está seguro de que desea continuar?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == Constants.vbYes)
                {
                    stock.AddStock();
                    // Limpiar tabla temporal
                    var tmp = ctx.TmpRegistroStockEntity.ToList();
                    if (tmp.Any())
                    {
                        ctx.TmpRegistroStockEntity.RemoveRange(tmp);
                        ctx.SaveChanges();
                    }
                    Interaction.MsgBox("Se actualizaron los items correctamente", (MsgBoxStyle)Constants.vbOK, "Centrex");
                    ingreso_guardado = true;
                }
                else
                {
                    return;
                }
            }

            closeandupdate(this);
        }

        // ==========================================================
        // Agregar ítem (abre buscador + formulario de carga)
        // ==========================================================
        private void cmd_additem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            Enabled = true;

            if (id == 0)
                return;

            var frm = new infoagregarstock(Conversions.ToInteger(cmb_proveedor.SelectedValue));
            frm.ShowDialog();
            updateform();
        }

        // ==========================================================
        // Cancelar ingreso (cierra sin guardar)
        // ==========================================================
        private void cmd_cancel_Click(object sender, EventArgs e)
        {
            if (!edicion)
            {
                if (Interaction.MsgBox("Eliminará el ingreso, por lo cual no se contabilizará. ¿Deseá continuar?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == Constants.vbYes)
                {
                    ingreso_guardado = true;
                    closeandupdate(this);
                }
            }
            else
            {
                closeandupdate(this);
            }
        }

        // ==========================================================
        // Buscar proveedor
        // ==========================================================
        private void psearch_proveedor_Click(object sender, EventArgs e)
        {
            string tmpTabla = tabla;
            tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            Enabled = true;
            tabla = tmpTabla;

            var proveedor = ctx.ProveedorEntity.FirstOrDefault(p => p.IdProveedor == id);
            if (proveedor is not null)
            {
                cmb_proveedor.SelectedIndex = cmb_proveedor.FindString(proveedor.RazonSocial);
            }
            id = 0;
        }

        // ==========================================================
        // Editar registro (doble clic)
        // ==========================================================
        private void dg_view_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (edicion && !editaStock)
                return;
            if (dg_view.Rows.Count == 0)
                return;

            int seleccionado = Conversions.ToInteger(dg_view.CurrentRow.Cells[0].Value);

            edicion_item_registro_stock = true;
            edita_item_registro_stock = ctx.RegistroStockEntity.FirstOrDefault(r => r.IdRegistro == seleccionado);
            if (edita_item_registro_stock is not null)
            {
                My.MyProject.Forms.infoagregarstock.ShowDialog();
                updateform();
            }
            edicion_item_registro_stock = false;
        }

        // ==========================================================
        // Borrar item del grid temporal
        // ==========================================================
        private void BorrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_view.Rows.Count == 0)
                return;

            int seleccionado = Conversions.ToInteger(dg_view.CurrentRow.Cells[0].Value);
            var tmpItem = ctx.TmpRegistroStockEntity.FirstOrDefault(t => t.IdRegistrotmp == seleccionado);

            if (tmpItem is not null)
            {
                var item = ctx.ItemEntity.FirstOrDefault(i => i.IdItem == tmpItem.IdItem);
                string nombreItem = item is not null ? item.Item : "(Desconocido)";

                if (Interaction.MsgBox("¿Está seguro de borrar el item: " + nombreItem + "?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == Constants.vbYes)
                {
                    ctx.TmpRegistroStockEntity.Remove(tmpItem);
                    ctx.SaveChanges();
                    updateform();
                }
            }
        }

        // ==========================================================
        // Eventos UI menores
        // ==========================================================
        private void EditarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dg_view_CellMouseDoubleClick(null, null);
        }

        private void lsv_items_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (edicion)
                    ContextMenuStrip1.Enabled = false;
            }
        }

        private void lbl_fecha_ingreso_MouseEnter(object sender, EventArgs e)
        {
            lbl_tooltip.Visible = true;
        }

        private void lbl_fecha_ingreso_MouseLeave(object sender, EventArgs e)
        {
            lbl_tooltip.Visible = false;
        }

        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

