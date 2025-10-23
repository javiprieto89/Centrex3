using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

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
            var proveedores = ctx.Proveedores.Where(p => p.activo == true).OrderBy(p => p.razon_social).ToList();

            cmb_proveedor.DataSource = proveedores;
            cmb_proveedor.DisplayMember = "RazonSocial";
            cmb_proveedor.ValueMember = "IdProveedor";

            if (VariablesGlobales.edicion)
            {
                txt_fecha.Text = DateTime.Parse(Conversions.ToString(VariablesGlobales.edita_registro_stock.fecha.Value)).ToString("dd/MM/yyyy");
                lbl_fecha_ingreso.Text = DateTime.Parse(VariablesGlobales.edita_registro_stock.FechaIngreso).ToString("dd/MM/yyyy");
                txt_factura.Text = VariablesGlobales.edita_registro_stock.factura;
                cmb_proveedor.SelectedValue = (byte)VariablesGlobales.edita_registro_stock.IdProveedor;

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
                var tmp = ctx.TmpRegistroStock.ToList();
                if (tmp.Any())
                {
                    ctx.TmpRegistroStock.RemoveRange(tmp);
                    ctx.SaveChanges();
                }
            }

            updateform();
        }

        private void add_stock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ingreso_guardado)
                return;
            if (!VariablesGlobales.edicion)
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

            if (VariablesGlobales.edicion && editaStock)
            {
                data = ctx.RegistrosStock.Include("Item").Where(rs => rs.IdIngreso == VariablesGlobales.edita_registro_stock.IdIngreso).Select(rs => new
                {
                    ID = rs.IdRegistro,
                    Código = rs.Item.item,
                    Producto = rs.Item.descript,
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
                data = ctx.TmpRegistroStock.Include("Item").Where(rst => rst.activo == true).Select(rst => new
                {
                    ID = rst.IdRegistroTmp,
                    Código = rst.Item.item,
                    Producto = rst.Item.descript,
                    Factura = rst.factura,
                    Fecha = rst.fecha,
                    Cantidad = rst.cantidad,
                    Costo = rst.costo,
                    rst.PrecioLista,
                    Factor = rst.factor,
                    Notas = rst.nota
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
            if (!VariablesGlobales.edicion)
            {
                if (Interaction.MsgBox("Se modificarán todos los items anteriormente cargados. ¿Está seguro de que desea continuar?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == Constants.vbYes)
                {
                    stock.AddStock();
                    // Limpiar tabla temporal
                    var tmp = ctx.TmpRegistroStock.ToList();
                    if (tmp.Any())
                    {
                        ctx.TmpRegistroStock.RemoveRange(tmp);
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

            if (VariablesGlobales.id == 0)
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
            if (!VariablesGlobales.edicion)
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
            string tmpTabla = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            Enabled = true;
            VariablesGlobales.tabla = tmpTabla;

            var proveedor = ctx.Proveedores.FirstOrDefault(p => p.IdProveedor == VariablesGlobales.id);
            if (proveedor is not null)
            {
                cmb_proveedor.SelectedIndex = cmb_proveedor.FindString(proveedor.RazonSocial);
            }
            VariablesGlobales.id = 0;
        }

        // ==========================================================
        // Editar registro (doble clic)
        // ==========================================================
        private void dg_view_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (VariablesGlobales.edicion && !editaStock)
                return;
            if (dg_view.Rows.Count == 0)
                return;

            int seleccionado = Conversions.ToInteger(dg_view.CurrentRow.Cells[0].Value);

            edicion_item_registro_stock = true;
            VariablesGlobales.edita_item_registro_stock = ctx.RegistrosStock.FirstOrDefault(r => r.IdRegistro == seleccionado);
            if (VariablesGlobales.edita_item_registro_stock is not null)
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
            var tmpItem = ctx.TmpRegistroStock.FirstOrDefault(t => t.IdRegistroTmp == seleccionado);

            if (tmpItem is not null)
            {
                var item = ctx.Items.FirstOrDefault(i => i.IdItem == tmpItem.IdItem);
                string nombreItem = item is not null ? item.item : "(Desconocido)";

                if (Interaction.MsgBox("¿Está seguro de borrar el item: " + nombreItem + "?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == Constants.vbYes)
                {
                    ctx.TmpRegistroStock.Remove(tmpItem);
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
                if (VariablesGlobales.edicion)
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

