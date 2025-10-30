using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{
    public partial class add_impuesto
    {
   private bool esRetencion;
    private bool esPercepcion;
 public int id_impuesto;
        
        public add_impuesto()
        {
            // Esta llamada es exigida por el diseñador.
            InitializeComponent();
 }

        public add_impuesto(bool _esRetencion, bool _esPercepcion)
        {
            // Esta llamada es exigida por el diseñador.
      InitializeComponent();

      // Agregue cualquier inicialización después de la llamada a InitializeComponent().
    esRetencion = _esRetencion;
         esPercepcion = _esPercepcion;
     }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
      if (string.IsNullOrEmpty(txt_nombre.Text))
      {
                Interaction.MsgBox("El campo 'Impuesto (%)' es obligatorio y está vacio");
                return;
            }
         else if (string.IsNullOrEmpty(txt_porcentaje.Text))
         {
      Interaction.MsgBox("El campo 'Porcentaje' es obligatorio y está vacio");
   return;
    }

    var tmp = new ImpuestoEntity();

            tmp.Nombre = txt_nombre.Text;
            tmp.Porcentaje = Conversions.ToDecimal(txt_porcentaje.Text);
    tmp.EsRetencion = chk_esRetencion.Checked;
    tmp.EsPercepcion = chk_esPercepcion.Checked;
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
   tmp.IdImpuesto = VariablesGlobales.edita_impuesto.IdImpuesto;
         if (impuestos.updateImpuesto(tmp) == false)
{
     Interaction.MsgBox("Hubo un problema al actualizar el impuesto, consulte con su programador", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
       closeandupdate(this);
     }
     }
            else
 {
  id_impuesto = impuestos.addImpuesto(tmp);
   if (id_impuesto == -1)
                {
         Interaction.MsgBox("Hubo un problema al dar de alta el impuesto, consulte con su programador", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
      closeandupdate(this);
 }
  }

            if (chk_secuencia.Checked == true)
 {
      txt_nombre.Text = "";
   txt_porcentaje.Text = "";
       chk_activo.Checked = true;
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

        private void add_marcai_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_descuento_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
     if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
           chk_secuencia.Enabled = false;
           txt_nombre.Text = VariablesGlobales.edita_impuesto.Nombre;
 txt_porcentaje.Text = VariablesGlobales.edita_impuesto.Porcentaje.ToString();
          chk_esPercepcion.Checked = VariablesGlobales.edita_impuesto.EsPercepcion ?? false;
          chk_esRetencion.Checked = VariablesGlobales.edita_impuesto.EsRetencion ?? false;
     chk_activo.Checked = VariablesGlobales.edita_impuesto.Activo;
            }

     if (VariablesGlobales.borrado == true)
            {
                txt_nombre.Enabled = false;
                txt_porcentaje.Enabled = false;
           chk_esRetencion.Enabled = false;
          chk_esPercepcion.Enabled = false;
 chk_activo.Enabled = false;
    cmd_ok.Visible = false;
         cmd_exit.Visible = false;
           Show();
      if (Interaction.MsgBox("¿Está seguro que desea borrar este impuesto?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
        {
        if (impuestos.borrarImpuesto(VariablesGlobales.edita_impuesto) == false)
    {
      if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del impuesto, ¿desea intentar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
   {
               // Realizo un borrado lógico
      if (impuestos.updateImpuesto(VariablesGlobales.edita_impuesto, true) == true)
  {
               Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el impuesto no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el impuesto tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
   }
         else
           {
      Interaction.MsgBox("No se ha podido borrar el impuesto, consulte con el programador");
            }
      }
     }
          }
closeandupdate(this);
}

    if (esRetencion)
  {
         chk_esRetencion.Checked = true;
            chk_esRetencion.Enabled = false;
  chk_esPercepcion.Enabled = false;
     chk_secuencia.Enabled = false;
       txt_porcentaje.Text = "0";
  }
          else if (esPercepcion)
            {
                chk_esPercepcion.Checked = true;
  chk_esPercepcion.Enabled = false;
  chk_esRetencion.Enabled = false;
        chk_secuencia.Enabled = false;
          txt_porcentaje.Text = "0";
            }
        }
    }
}

