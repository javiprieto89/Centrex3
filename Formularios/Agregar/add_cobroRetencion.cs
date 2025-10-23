using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{

    public partial class add_cobroRetencion
    {
        private Form formViejo;
        private CentrexDbContext _context;

        // Parámetros de operación
        public bool Edicion { get; set; } = false;
        public bool Borrado { get; set; } = false;
        public TmpCobroRetencionEntity? EditaCobroRetencion { get; set; }

        public add_cobroRetencion()
        {
            InitializeComponent();
        }

        private void add_cobroRetencion_Load(object sender, EventArgs e)
        {
            _context = new CentrexDbContext();

            // Cargar combo de retenciones (Impuestos marcados como retención)
            cargar_comboRetenciones();

            if (!Edicion && !Borrado)
            {
                cmb_retencion.SelectedItem = null;
                cmb_retencion.Text = "Seleccione una retención...";
            }
            else if (EditaCobroRetencion is not null)
            {
                cmb_retencion.SelectedValue = EditaCobroRetencion.IdImpuesto;
                txt_importe.Text = EditaCobroRetencion.Total.ToString("0.00");
                txt_notas.Text = EditaCobroRetencion.Notas;
            }

            if (Borrado)
            {
                cmb_retencion.Enabled = false;
                txt_importe.Enabled = false;
                txt_notas.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_exit.Enabled = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta retención?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == MsgBoxResult.Yes)
                {
                    if (!borrarTmpCobroRetencion(EditaCobroRetencion.IdTmpRetencion))
                    {
                        Interaction.MsgBox("No se ha podido borrar la retención.");
                    }
                }
                closeandupdate(this);
            }

            // formViejo = Form ' Comentado para evitar error de compilación
            // Form = Me ' Comentado para evitar error de compilación
        }

        private void cargar_comboRetenciones()
        {
            try
            {
                var lista = _context.Impuestos
                    .Where(i => i.EsRetencion == true)
                    .OrderBy(i => i.Nombre)
                    .Select(i => new { Id = i.IdImpuesto, Nombre = i.Nombre })
                    .ToList();

                cmb_retencion.DataSource = lista;
                cmb_retencion.DisplayMember = "Nombre";
                cmb_retencion.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al cargar las retenciones: " + ex.Message, Constants.vbExclamation);
            }
        }

        private void txt_importe_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y punto decimal
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_retencion.SelectedItem is null)
            {
                Interaction.MsgBox("Debe seleccionar una retención", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrWhiteSpace(txt_importe.Text))
            {
                Interaction.MsgBox("Debe ingresar un importe", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var cb = new TmpCobroRetencionEntity()
            {
                IdImpuesto = Conversions.ToInteger(cmb_retencion.SelectedValue),
                Total = decimal.Parse(txt_importe.Text),
                Notas = txt_notas.Text
            };

            try
            {
                if (Edicion)
                {
                    cb.IdTmpRetencion = EditaCobroRetencion.IdTmpRetencion;
                    updateTmpCobroRetencion(cb);
                }
                else
                {
                    addTmpCobroRetencion(cb);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Ocurrió un error al guardar la retención: " + ex.Message, (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
            }

            closeandupdate(this);
        }

        private void psearch_retencion_Click(object sender, EventArgs e)
        {
            // Reutiliza tu formulario de búsqueda general
            var frmSearch = new search(Conversions.ToInteger("retenciones"));
            frmSearch.ShowDialog();

            if (frmSearch.SelectedIndex > 0)
            {
                cmb_retencion.SelectedValue = frmSearch.SelectedIndex;
            }
        }

        private void add_cobroRetencion_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Form = formViejo
            closeandupdate(this);
        }

        private void cmd_agregarImpuesto_Click(object sender, EventArgs e)
        {
            var frmImpuesto = new add_impuesto(true, false);
            frmImpuesto.ShowDialog();

            if (frmImpuesto.id_impuesto > 0)
            {
                cargar_comboRetenciones();
                cmb_retencion.SelectedValue = frmImpuesto.id_impuesto;
            }
        }

        // ----------------------------------------------
        // FUNCIONES ADAPTADAS A ENTITY FRAMEWORK
        // ----------------------------------------------
        private bool addTmpCobroRetencion(TmpCobroRetencionEntity cb)
        {
            try
            {
                _context.TmpCobrosRetenciones.Add(cb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al agregar retención: " + ex.Message, Constants.vbExclamation);
                return false;
            }
        }

        private bool updateTmpCobroRetencion(TmpCobroRetencionEntity cb)
        {
            try
            {
                var entity = _context.TmpCobrosRetenciones.FirstOrDefault(r => r.IdTmpRetencion == cb.IdTmpRetencion);
                if (entity is null)
                    return false;

                entity.IdImpuesto = cb.IdImpuesto;
                entity.Total = cb.Total;
                entity.Notas = cb.Notas;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al actualizar la retención: " + ex.Message, Constants.vbExclamation);
                return false;
            }
        }

        private bool borrarTmpCobroRetencion(int idRetencion)
        {
            try
            {
                var entity = _context.TmpCobrosRetenciones.FirstOrDefault(r => r.IdTmpRetencion == idRetencion);
                if (entity is null)
                    return false;
                _context.TmpCobrosRetenciones.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al borrar la retención: " + ex.Message, Constants.vbExclamation);
                return false;
            }
        }
    }
}
