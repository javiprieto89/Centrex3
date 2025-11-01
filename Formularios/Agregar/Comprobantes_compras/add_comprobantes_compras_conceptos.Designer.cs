using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_comprobantes_compras_conceptos : Form
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
            lblConcepto = new Label();
            lbl_concepto = new Label();
            lbl_subtotal = new Label();
            lbl_iva = new Label();
            lbl_total = new Label();
            txt_subtotal = new TextBox();
            txt_subtotal.KeyPress += new KeyPressEventHandler(txt_subtotal_KeyPress);
            txt_iva = new TextBox();
            txt_iva.KeyPress += new KeyPressEventHandler(txt_iva_KeyPress);
            txt_total = new TextBox();
            txt_total.KeyPress += new KeyPressEventHandler(txt_total_KeyPress);
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            SuspendLayout();
            // 
            // lblConcepto
            // 
            lblConcepto.AutoSize = true;
            lblConcepto.Location = new Point(33, 19);
            lblConcepto.Name = "lblConcepto";
            lblConcepto.Size = new Size(53, 13);
            lblConcepto.TabIndex = 0;
            lblConcepto.Text = "Concepto";
            // 
            // lbl_concepto
            // 
            lbl_concepto.AutoSize = true;
            lbl_concepto.Location = new Point(106, 19);
            lbl_concepto.Name = "lbl_concepto";
            lbl_concepto.Size = new Size(68, 13);
            lbl_concepto.TabIndex = 1;
            lbl_concepto.Text = "%concepto%";
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Location = new Point(33, 66);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.Size = new Size(46, 13);
            lbl_subtotal.TabIndex = 2;
            lbl_subtotal.Text = "Subtotal";
            // 
            // lbl_iva
            // 
            lbl_iva.AutoSize = true;
            lbl_iva.Location = new Point(33, 102);
            lbl_iva.Name = "lbl_iva";
            lbl_iva.Size = new Size(33, 13);
            lbl_iva.TabIndex = 4;
            lbl_iva.Text = "I.V.A.";
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Location = new Point(33, 142);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(31, 13);
            lbl_total.TabIndex = 6;
            lbl_total.Text = "Total";
            // 
            // txt_subtotal
            // 
            txt_subtotal.Location = new Point(106, 59);
            txt_subtotal.Name = "txt_subtotal";
            txt_subtotal.Size = new Size(183, 20);
            txt_subtotal.TabIndex = 3;
            // 
            // txt_iva
            // 
            txt_iva.Location = new Point(106, 95);
            txt_iva.Name = "txt_iva";
            txt_iva.Size = new Size(183, 20);
            txt_iva.TabIndex = 5;
            // 
            // txt_total
            // 
            txt_total.Location = new Point(106, 135);
            txt_total.Name = "txt_total";
            txt_total.Size = new Size(183, 20);
            txt_total.TabIndex = 7;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(169, 209);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 9;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(71, 209);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 8;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // add_comprobantes_compras_conceptos
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(322, 247);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_total);
            Controls.Add(txt_iva);
            Controls.Add(txt_subtotal);
            Controls.Add(lbl_total);
            Controls.Add(lbl_iva);
            Controls.Add(lbl_subtotal);
            Controls.Add(lbl_concepto);
            Controls.Add(lblConcepto);
            Name = "add_comprobantes_compras_conceptos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar concepto";
            Load += new EventHandler(add_comprobantes_compras_conceptos_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lblConcepto;
        internal Label lbl_concepto;
        internal Label lbl_subtotal;
        internal Label lbl_iva;
        internal Label lbl_total;
        internal TextBox txt_subtotal;
        internal TextBox txt_iva;
        internal TextBox txt_total;
        internal Button cmd_exit;
        internal Button cmd_ok;
    }
}


