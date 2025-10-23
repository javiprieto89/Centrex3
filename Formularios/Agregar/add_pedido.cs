using System;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace Centrex
{

    // ===============================================
    // Formulario de alta/edición de pedidos - EF
    // Autor: Javier Prieto - 2025-10-20
    // ===============================================
    public partial class add_pedido
    {
        private int usuarioId = 1; // <-- reemplazar por usuario activo
        private PedidoEntity pedidoActual;
        private decimal totalActual;

        public add_pedido()
        {
            InitializeComponent();
        }

        private void add_pedido_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarItemsTemporales();
                ActualizarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar formulario: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===============================================
        // CARGA DE DATOS
        // ===============================================
        private void CargarCombos()
        {
            using (var ctx = new CentrexDbContext())
            {
                var clientes = ctx.ClienteEntity.OrderBy(c => c.RazonSocial).ToList();
                cmb_clientes.DataSource = clientes;
                cmb_clientes.DisplayMember = "RazonSocial";
                cmb_clientes.ValueMember = "IdCliente";
                cmb_clientes.SelectedIndex = -1;

                var comprobantes = ctx.ComprobanteEntity.Where(c => c.Activo == true).OrderBy(c => c.Comprobante).ToList();
                cmb_comprobantes.DataSource = comprobantes;
                cmb_comprobantes.DisplayMember = "Comprobante";
                cmb_comprobantes.ValueMember = "IdComprobante";
                cmb_comprobantes.SelectedIndex = -1;
            }
        }

        private void CargarItemsTemporales()
        {
            using (var ctx = new CentrexDbContext())
            {
                var lista = ctx.TmpPedidoItemEntity.Include(t => t.ItemEntity).Where(t => t.IdUsuario == usuarioId && (t.Activo ?? false)).ToList();


                dg_items.DataSource = null;
                dg_items.DataSource = lista;

                if (dg_items.Columns.Contains("ItemEntity"))
                {
                    dg_items.Columns["ItemEntity"].Visible = false;
                }
            }
        }

        // ===============================================
        // OPERACIONES
        // ===============================================
        private void cmd_add_item_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Crear formulario add_itemPedido
                MessageBox.Show("Funcionalidad de agregar item pendiente de implementar", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Dim frm As New add_itemPedido(usuarioId)
                // frm.ShowDialog()
                CargarItemsTemporales();
                ActualizarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar ítem: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_items.SelectedRows.Count == 0)
                return;
            int idTmp = Conversions.ToInteger(dg_items.SelectedRows[0].Cells["IdTmpPedidoItem"].Value);

            using (var ctx = new CentrexDbContext())
            {
                var tmp = ctx.TmpPedidoItemEntity.Find(idTmp);
                if (tmp is not null)
                {
                    ctx.TmpPedidoItemEntity.Remove(tmp);
                    ctx.SaveChanges();
                }
            }

            CargarItemsTemporales();
            ActualizarTotales();
        }

        private void ActualizarTotales()
        {
            using (var ctx = new CentrexDbContext())
            {
                var total = ctx.TmpPedidoItemEntity
                    .Where(t => t.IdUsuario == usuarioId && (t.Activo == true))
                    .Sum(t => t.Precio * t.Cantidad);
                totalActual = total;
                lbl_total.Text = $"Total: {total:C2}";
            }
        }

        // ===============================================
        // GUARDADO DEL PEDIDO
        // ===============================================
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_clientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cmb_comprobantes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un comprobante.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (var ctx = new CentrexDbContext())
            {
                pedidoActual = new PedidoEntity()
                {
                    IdCliente = Conversions.ToInteger(cmb_clientes.SelectedValue),
                    IdComprobante = Conversions.ToInteger(cmb_comprobantes.SelectedValue),
                    Subtotal = totalActual,
                    Iva = 0m,
                    Total = totalActual,
                    Activo = true,
                    Cerrado = false,
                    EsPresupuesto = false,
                    IdUsuario = usuarioId,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    FechaEdicion = DateOnly.FromDateTime(DateTime.Now)
                };

                bool guardado = Pedidos.GuardarPedido(pedidoActual, usuarioId);
                if (guardado)
                {
                    MessageBox.Show($"Pedido {pedidoActual.IdPedido} guardado correctamente.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    borrar_tabla_pedidos_temporales(usuarioId);
                    CargarItemsTemporales();
                    ActualizarTotales();
                }
                else
                {
                    MessageBox.Show("Error al guardar el pedido.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Cancelar pedido actual?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EliminarTmpItemsPorUsuario(usuarioId);
                CargarItemsTemporales();
                ActualizarTotales();
                Close();
            }
        }

    }
}
