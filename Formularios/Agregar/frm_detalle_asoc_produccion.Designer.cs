using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_detalle_asoc_produccion : Form
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
        private System.ComponentModel.IContainer components = null!;

        // NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        // Se puede modificar usando el Diseñador de Windows Forms.  
        // No lo modifique con el editor de código.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            dg_view = new DataGridView();
            cmd_exit = new Button();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            ((System.ComponentModel.ISupportInitialize)dg_view).BeginInit();
            SuspendLayout();
            // 
            // dg_view
            // 
            dg_view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view.Location = new Point(12, 14);
            dg_view.Name = "dg_view";
            dg_view.Size = new Size(776, 378);
            dg_view.TabIndex = 0;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(475, 411);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 7;
            cmd_exit.Text = "Cancelar";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(251, 411);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 6;
            cmd_ok.Text = "Confirmar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // frm_detalle_asoc_produccion
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 449);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(dg_view);
            Name = "frm_detalle_asoc_produccion";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalle de produccion";
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            Load += new EventHandler(frm_detalle_asoc_produccion_Load);
            ResumeLayout(false);

        }

        internal DataGridView dg_view;
        internal Button cmd_exit;
        internal Button cmd_ok;
    }
}


