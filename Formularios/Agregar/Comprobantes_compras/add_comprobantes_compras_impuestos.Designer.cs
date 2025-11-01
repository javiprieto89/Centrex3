using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_comprobantes_compras_impuestos : Form
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
            lblImpuesto = new Label();
            lbl_importe = new Label();
            txt_importe = new TextBox();
            txt_importe.KeyPress += new KeyPressEventHandler(txt_importe_KeyPress);
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_impuesto = new Label();
            SuspendLayout();
            // 
            // lblImpuesto
            // 
            lblImpuesto.AutoSize = true;
            lblImpuesto.Location = new Point(47, 26);
            lblImpuesto.Name = "lblImpuesto";
            lblImpuesto.Size = new Size(50, 13);
            lblImpuesto.TabIndex = 0;
            lblImpuesto.Text = "Impuesto";
            // 
            // lbl_importe
            // 
            lbl_importe.AutoSize = true;
            lbl_importe.Location = new Point(47, 82);
            lbl_importe.Name = "lbl_importe";
            lbl_importe.Size = new Size(42, 13);
            lbl_importe.TabIndex = 2;
            lbl_importe.Text = "Importe";
            // 
            // txt_importe
            // 
            txt_importe.Location = new Point(113, 79);
            txt_importe.Name = "txt_importe";
            txt_importe.Size = new Size(100, 20);
            txt_importe.TabIndex = 3;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(152, 159);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 5;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(54, 159);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 4;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Location = new Point(113, 26);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.Size = new Size(65, 13);
            lbl_impuesto.TabIndex = 1;
            lbl_impuesto.Text = "%impuesto%";
            // 
            // add_comprobantes_compras_impuestos
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 207);
            Controls.Add(lbl_impuesto);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_importe);
            Controls.Add(lbl_importe);
            Controls.Add(lblImpuesto);
            Name = "add_comprobantes_compras_impuestos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar impuesto";
            Load += new EventHandler(add_comprobantes_compras_impuestos_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lblImpuesto;
        internal Label lbl_importe;
        internal TextBox txt_importe;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_impuesto;
    }
}


