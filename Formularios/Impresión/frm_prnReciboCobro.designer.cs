using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_prnReciboCobro : Form
    {

        // Form reemplaza a Dispose para limpiar la lista de componentes.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Requerido por el Diseñador de Windows Forms
        private System.ComponentModel.IContainer components;

        // NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        // Se puede modificar usando el Diseñador de Windows Forms.  
        // No lo modifique con el editor de código.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            rpt_view = new Microsoft.Reporting.WinForms.ReportViewer();
            SuspendLayout();
            // 
            // rpt_view
            // 
            rpt_view.Dock = DockStyle.Fill;
            rpt_view.Location = new Point(0, 0);
            rpt_view.Name = "rpt_view";
            rpt_view.Size = new Size(930, 733);
            rpt_view.TabIndex = 1;
            // 
            // frm_prnReciboCobro
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 733);
            Controls.Add(rpt_view);
            Name = "frm_prnReciboCobro";
            Text = "Impresión";
            Load += new EventHandler(frm_prnReciboCobro_Load);
            ResumeLayout(false);

        }

        internal Microsoft.Reporting.WinForms.ReportViewer rpt_view;
    }
}
