using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_ultimo_comprobante : Form
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
            cmb_comprobante = new ComboBox();
            cmb_comprobante.KeyPress += new KeyPressEventHandler(cmb_comprobante_KeyPress);
            lbl_comprobante = new Label();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            SuspendLayout();
            // 
            // cmb_comprobante
            // 
            cmb_comprobante.FormattingEnabled = true;
            cmb_comprobante.Location = new Point(31, 62);
            cmb_comprobante.Name = "cmb_comprobante";
            cmb_comprobante.Size = new Size(328, 21);
            cmb_comprobante.TabIndex = 0;
            // 
            // lbl_comprobante
            // 
            lbl_comprobante.AutoSize = true;
            lbl_comprobante.Location = new Point(28, 33);
            lbl_comprobante.Name = "lbl_comprobante";
            lbl_comprobante.Size = new Size(75, 13);
            lbl_comprobante.TabIndex = 1;
            lbl_comprobante.Text = "Comprobantes";
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(109, 109);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(168, 23);
            cmd_ok.TabIndex = 53;
            cmd_ok.Text = "Consultar último comprobante";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // frm_ultimo_comprobante
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(386, 165);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_comprobante);
            Controls.Add(cmb_comprobante);
            Name = "frm_ultimo_comprobante";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Consulta comprobantes";
            Load += new EventHandler(frm_ultimo_comprobante_Load);
            FormClosed += new FormClosedEventHandler(frm_ultimo_comprobante_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal ComboBox cmb_comprobante;
        internal Label lbl_comprobante;
        internal Button cmd_ok;
    }
}


