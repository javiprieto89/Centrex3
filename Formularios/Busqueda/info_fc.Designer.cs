using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class info_fc : Form
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
            lbl_punto_venta = new Label();
            lbl_comprobante = new Label();
            lbl_numero_comprobante = new Label();
            txt_puntoVenta = new TextBox();
            txt_numeroComprobante = new TextBox();
            cmb_Comprobante = new ComboBox();
            cmb_Comprobante.SelectionChangeCommitted += new EventHandler(cmb_Comprobante_SelectionChangeCommitted);
            cmd_consultar = new Button();
            cmd_consultar.Click += new EventHandler(cmd_consultar_Click);
            SuspendLayout();
            // 
            // lbl_punto_venta
            // 
            lbl_punto_venta.AutoSize = true;
            lbl_punto_venta.Location = new Point(34, 97);
            lbl_punto_venta.Name = "lbl_punto_venta";
            lbl_punto_venta.Size = new Size(80, 13);
            lbl_punto_venta.TabIndex = 0;
            lbl_punto_venta.Text = "Punto de venta";
            // 
            // lbl_comprobante
            // 
            lbl_comprobante.AutoSize = true;
            lbl_comprobante.Location = new Point(34, 42);
            lbl_comprobante.Name = "lbl_comprobante";
            lbl_comprobante.Size = new Size(70, 13);
            lbl_comprobante.TabIndex = 1;
            lbl_comprobante.Text = "Comprobante";
            // 
            // lbl_numero_comprobante
            // 
            lbl_numero_comprobante.AutoSize = true;
            lbl_numero_comprobante.Location = new Point(34, 152);
            lbl_numero_comprobante.Name = "lbl_numero_comprobante";
            lbl_numero_comprobante.Size = new Size(124, 13);
            lbl_numero_comprobante.TabIndex = 2;
            lbl_numero_comprobante.Text = "Número de comprobante";
            // 
            // txt_puntoVenta
            // 
            txt_puntoVenta.Location = new Point(177, 92);
            txt_puntoVenta.Name = "txt_puntoVenta";
            txt_puntoVenta.ReadOnly = true;
            txt_puntoVenta.Size = new Size(177, 20);
            txt_puntoVenta.TabIndex = 1;
            // 
            // txt_numeroComprobante
            // 
            txt_numeroComprobante.Location = new Point(177, 149);
            txt_numeroComprobante.Name = "txt_numeroComprobante";
            txt_numeroComprobante.Size = new Size(177, 20);
            txt_numeroComprobante.TabIndex = 2;
            // 
            // cmb_Comprobante
            // 
            cmb_Comprobante.FormattingEnabled = true;
            cmb_Comprobante.Location = new Point(177, 34);
            cmb_Comprobante.Name = "cmb_Comprobante";
            cmb_Comprobante.Size = new Size(177, 21);
            cmb_Comprobante.TabIndex = 0;
            // 
            // cmd_consultar
            // 
            cmd_consultar.Location = new Point(105, 240);
            cmd_consultar.Name = "cmd_consultar";
            cmd_consultar.Size = new Size(179, 56);
            cmd_consultar.TabIndex = 3;
            cmd_consultar.Text = "Consultar";
            cmd_consultar.UseVisualStyleBackColor = true;
            // 
            // info_fc
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(388, 333);
            Controls.Add(cmd_consultar);
            Controls.Add(cmb_Comprobante);
            Controls.Add(txt_numeroComprobante);
            Controls.Add(txt_puntoVenta);
            Controls.Add(lbl_numero_comprobante);
            Controls.Add(lbl_comprobante);
            Controls.Add(lbl_punto_venta);
            Name = "info_fc";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Información de factura ya emitida";
            Load += new EventHandler(info_fc_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_punto_venta;
        internal Label lbl_comprobante;
        internal Label lbl_numero_comprobante;
        internal TextBox txt_puntoVenta;
        internal TextBox txt_numeroComprobante;
        internal ComboBox cmb_Comprobante;
        internal Button cmd_consultar;
    }
}


