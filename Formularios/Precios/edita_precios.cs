using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class edita_precios : Form
    {
        public edita_precios()
        {
            InitializeComponent();
        }

        private async void edita_precios_Load(object sender, EventArgs e)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                // 🔹 Consulta EF (reemplaza SQL directo)
                var query = ctx.ItemEntity
                    .AsNoTracking()
                    .Where(i => !i.EsDescuento && !i.EsMarkup && i.Activo)
                    .OrderBy(i => i.Item)
                    .Select(i => new
                    {
                        ID = i.IdItem,
                        Código = i.Item,
                        Producto = i.Descript,
                        Precio = i.PrecioLista
                    });

                var result = new DataGridQueryResult
                {
                    Query = query,
                    EsMaterializada = false
                };

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result);

                // 🔹 Configuración visual
                dg_view.Columns["ID"].ReadOnly = true;
                dg_view.Columns["Código"].ReadOnly = true;
                dg_view.Columns["Producto"].ReadOnly = true;
                dg_view.Columns["Precio"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar los ítems: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private async void cmd_ok_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirmación del usuario
                var res = Interaction.MsgBox(
                    "¿Está seguro de que desea aplicar la actualización de precios?",
                    (MsgBoxStyle)((int)MsgBoxStyle.YesNo | (int)MsgBoxStyle.Question),
                    "Centrex");

                if (res != MsgBoxResult.Yes)
                {
                    Interaction.MsgBox(
                        "Todos los cambios realizados se descartarán",
                        MsgBoxStyle.Information,
                        "Centrex");
                    generales.closeandupdate(this);
                    return;
                }

                using var ctx = new CentrexDbContext();
                int actualizados = 0;

                // 🔹 Recorremos cada fila y aplicamos cambios
                foreach (DataGridViewRow row in dg_view.Rows)
                {
                    if (row.IsNewRow) continue;

                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    var precioTexto = row.Cells["Precio"].Value?.ToString();

                    if (!decimal.TryParse(precioTexto, NumberStyles.Number, CultureInfo.CurrentCulture, out var nuevoPrecio))
                        continue;

                    var item = await ctx.ItemEntity.FirstOrDefaultAsync(i => i.IdItem == id);
                    if (item == null) continue;

                    if (item.PrecioLista != nuevoPrecio)
                    {
                        item.PrecioLista = nuevoPrecio;
                        ctx.ItemEntity.Update(item);
                        actualizados++;
                    }
                }

                await ctx.SaveChangesAsync();

                // Mensaje final
                if (actualizados > 0)
                {
                    Interaction.MsgBox(
                        $"Se actualizaron {actualizados} precios correctamente.",
                        MsgBoxStyle.Information,
                        "Centrex");
                }
                else
                {
                    Interaction.MsgBox(
                        "No hubo cambios de precios para aplicar.",
                        MsgBoxStyle.Information,
                        "Centrex");
                }

                // 🔹 Refrescar grilla (si querés que quede abierta)
                await RefrescarGrillaAsync();

                // 🔹 O cerrar el formulario directamente:
                // generales.closeandupdate(this);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al actualizar los precios: {ex.Message}",
                    MsgBoxStyle.Critical, "Centrex");
            }
        }

        private async Task RefrescarGrillaAsync()
        {
            using var ctx = new CentrexDbContext();

            var query = ctx.ItemEntity
                .AsNoTracking()
                .Where(i => !i.EsDescuento && !i.EsMarkup && i.Activo)
                .OrderBy(i => i.Item)
                .Select(i => new
                {
                    ID = i.IdItem,
                    Código = i.Item,
                    Producto = i.Descript,
                    Precio = i.PrecioLista
                });

            var result = new DataGridQueryResult
            {
                Query = query,
                EsMaterializada = false
            };

            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result);
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            generales.closeandupdate(this);
        }
    }
}
