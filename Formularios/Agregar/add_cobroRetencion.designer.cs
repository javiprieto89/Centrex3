using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_cobroRetencion : Form
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
            lbl_importe = new Label();
            lbl_retencion = new Label();
            psearch_retencion = new PictureBox();
            psearch_retencion.Click += new EventHandler(psearch_retencion_Click);
            cmb_retencion = new ComboBox();
            txt_importe = new TextBox();
            txt_importe.KeyPress += new KeyPressEventHandler(txt_importe_KeyPress);
            lbl_notas = new Label();
            txt_notas = new TextBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_agregarImpuesto = new Button();
            cmd_agregarImpuesto.Click += new EventHandler(cmd_agregarImpuesto_Click);
            ((System.ComponentModel.ISupportInitialize)psearch_retencion).BeginInit();
            SuspendLayout();
            // 
            // lbl_importe
            // 
            lbl_importe.AutoSize = true;
            lbl_importe.Location = new Point(23, 106);
            lbl_importe.Name = "lbl_importe";
            lbl_importe.Size = new Size(42, 13);
            lbl_importe.TabIndex = 1;
            lbl_importe.Text = "Importe";
            // 
            // lbl_retencion
            // 
            lbl_retencion.AutoSize = true;
            lbl_retencion.Location = new Point(23, 33);
            lbl_retencion.Name = "lbl_retencion";
            lbl_retencion.Size = new Size(56, 13);
            lbl_retencion.TabIndex = 3;
            lbl_retencion.Text = "Retención";
            // 
            // psearch_retencion
            // 
            psearch_retencion.Image = My.Resources.Resources.iconoLupa;
            psearch_retencion.Location = new Point(448, 25);
            psearch_retencion.Name = "psearch_retencion";
            psearch_retencion.Size = new Size(22, 22);
            psearch_retencion.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_retencion.TabIndex = 29;
            psearch_retencion.TabStop = false;
            // 
            // cmb_retencion
            // 
            cmb_retencion.FormattingEnabled = true;
            cmb_retencion.Location = new Point(90, 25);
            cmb_retencion.Name = "cmb_retencion";
            cmb_retencion.Size = new Size(352, 21);
            cmb_retencion.TabIndex = 0;
            // 
            // txt_importe
            // 
            txt_importe.Location = new Point(90, 103);
            txt_importe.Name = "txt_importe";
            txt_importe.Size = new Size(104, 20);
            txt_importe.TabIndex = 1;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(23, 179);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 8;
            lbl_notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(90, 180);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(352, 97);
            txt_notas.TabIndex = 2;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(256, 336);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 4;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(158, 336);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 3;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_agregarImpuesto
            // 
            cmd_agregarImpuesto.Location = new Point(90, 52);
            cmd_agregarImpuesto.Name = "cmd_agregarImpuesto";
            cmd_agregarImpuesto.Size = new Size(181, 23);
            cmd_agregarImpuesto.TabIndex = 30;
            cmd_agregarImpuesto.Text = "Agregar retención en impuestos";
            cmd_agregarImpuesto.UseVisualStyleBackColor = true;
            // 
            // add_cobroRetencion
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 384);
            Controls.Add(cmd_agregarImpuesto);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_notas);
            Controls.Add(lbl_notas);
            Controls.Add(txt_importe);
            Controls.Add(psearch_retencion);
            Controls.Add(cmb_retencion);
            Controls.Add(lbl_retencion);
            Controls.Add(lbl_importe);
            Name = "add_cobroRetencion";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingresar transferencia";
            ((System.ComponentModel.ISupportInitialize)psearch_retencion).EndInit();
            Load += new EventHandler(add_cobroRetencion_Load);
            FormClosed += new FormClosedEventHandler(add_cobroRetencion_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_importe;
        internal Label lbl_retencion;
        internal PictureBox psearch_retencion;
        internal ComboBox cmb_retencion;
        internal TextBox txt_importe;
        internal Label lbl_notas;
        internal TextBox txt_notas;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Button cmd_agregarImpuesto;
    }
}
