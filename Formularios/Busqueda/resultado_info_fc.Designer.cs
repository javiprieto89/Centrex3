using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class resultado_info_fc : Form
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
            lbl_cae = new Label();
            txt_cae = new TextBox();
            txt_total = new TextBox();
            lbl_total = new Label();
            txt_subtotal = new TextBox();
            lbl_subtotal = new Label();
            txt_impuestos = new TextBox();
            lbl_impuestos = new Label();
            txt_CUIT = new TextBox();
            lbl_CUIT = new Label();
            txt_cliente = new TextBox();
            lbl_cliente = new Label();
            SuspendLayout();
            // 
            // lbl_cae
            // 
            lbl_cae.AutoSize = true;
            lbl_cae.Location = new Point(41, 133);
            lbl_cae.Name = "lbl_cae";
            lbl_cae.Size = new Size(28, 13);
            lbl_cae.TabIndex = 0;
            lbl_cae.Text = "CAE";
            // 
            // txt_cae
            // 
            txt_cae.Location = new Point(134, 128);
            txt_cae.Name = "txt_cae";
            txt_cae.ReadOnly = true;
            txt_cae.Size = new Size(176, 20);
            txt_cae.TabIndex = 1;
            // 
            // txt_total
            // 
            txt_total.Location = new Point(134, 254);
            txt_total.Name = "txt_total";
            txt_total.ReadOnly = true;
            txt_total.Size = new Size(176, 20);
            txt_total.TabIndex = 3;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Location = new Point(41, 262);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(31, 13);
            lbl_total.TabIndex = 2;
            lbl_total.Text = "Total";
            // 
            // txt_subtotal
            // 
            txt_subtotal.Location = new Point(134, 170);
            txt_subtotal.Name = "txt_subtotal";
            txt_subtotal.ReadOnly = true;
            txt_subtotal.Size = new Size(176, 20);
            txt_subtotal.TabIndex = 5;
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Location = new Point(41, 176);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.Size = new Size(46, 13);
            lbl_subtotal.TabIndex = 4;
            lbl_subtotal.Text = "Subtotal";
            // 
            // txt_impuestos
            // 
            txt_impuestos.Location = new Point(134, 212);
            txt_impuestos.Name = "txt_impuestos";
            txt_impuestos.ReadOnly = true;
            txt_impuestos.Size = new Size(176, 20);
            txt_impuestos.TabIndex = 7;
            // 
            // lbl_impuestos
            // 
            lbl_impuestos.AutoSize = true;
            lbl_impuestos.Location = new Point(41, 219);
            lbl_impuestos.Name = "lbl_impuestos";
            lbl_impuestos.Size = new Size(55, 13);
            lbl_impuestos.TabIndex = 6;
            lbl_impuestos.Text = "Impuestos";
            // 
            // txt_CUIT
            // 
            txt_CUIT.Location = new Point(134, 86);
            txt_CUIT.Name = "txt_CUIT";
            txt_CUIT.ReadOnly = true;
            txt_CUIT.Size = new Size(176, 20);
            txt_CUIT.TabIndex = 9;
            // 
            // lbl_CUIT
            // 
            lbl_CUIT.AutoSize = true;
            lbl_CUIT.Location = new Point(41, 90);
            lbl_CUIT.Name = "lbl_CUIT";
            lbl_CUIT.Size = new Size(32, 13);
            lbl_CUIT.TabIndex = 8;
            lbl_CUIT.Text = "CUIT";
            // 
            // txt_cliente
            // 
            txt_cliente.Location = new Point(134, 44);
            txt_cliente.Name = "txt_cliente";
            txt_cliente.ReadOnly = true;
            txt_cliente.Size = new Size(176, 20);
            txt_cliente.TabIndex = 11;
            // 
            // lbl_cliente
            // 
            lbl_cliente.AutoSize = true;
            lbl_cliente.Location = new Point(41, 47);
            lbl_cliente.Name = "lbl_cliente";
            lbl_cliente.Size = new Size(39, 13);
            lbl_cliente.TabIndex = 10;
            lbl_cliente.Text = "Cliente";
            // 
            // resultado_info_fc
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(339, 334);
            Controls.Add(txt_cliente);
            Controls.Add(lbl_cliente);
            Controls.Add(txt_CUIT);
            Controls.Add(lbl_CUIT);
            Controls.Add(txt_impuestos);
            Controls.Add(lbl_impuestos);
            Controls.Add(txt_subtotal);
            Controls.Add(lbl_subtotal);
            Controls.Add(txt_total);
            Controls.Add(lbl_total);
            Controls.Add(txt_cae);
            Controls.Add(lbl_cae);
            Name = "resultado_info_fc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Información del comprobante";
            Load += new EventHandler(resultado_info_fc_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_cae;
        internal TextBox txt_cae;
        internal TextBox txt_total;
        internal Label lbl_total;
        internal TextBox txt_subtotal;
        internal Label lbl_subtotal;
        internal TextBox txt_impuestos;
        internal Label lbl_impuestos;
        internal TextBox txt_CUIT;
        internal Label lbl_CUIT;
        internal TextBox txt_cliente;
        internal Label lbl_cliente;
    }
}


