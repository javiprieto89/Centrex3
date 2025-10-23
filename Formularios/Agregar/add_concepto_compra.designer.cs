using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_concepto_compra : Form
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
            chk_activo = new CheckBox();
            txt_concepto = new TextBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_concepto = new Label();
            SuspendLayout();
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(23, 76);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(104, 17);
            chk_activo.TabIndex = 3;
            chk_activo.Text = "Concepto activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_concepto
            // 
            txt_concepto.Location = new Point(174, 36);
            txt_concepto.Name = "txt_concepto";
            txt_concepto.Size = new Size(259, 20);
            txt_concepto.TabIndex = 0;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(23, 116);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 4;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(247, 154);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 6;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(149, 154);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_concepto
            // 
            lbl_concepto.AutoSize = true;
            lbl_concepto.Location = new Point(20, 36);
            lbl_concepto.Name = "lbl_concepto";
            lbl_concepto.Size = new Size(53, 13);
            lbl_concepto.TabIndex = 14;
            lbl_concepto.Text = "Concepto";
            // 
            // add_concepto_compra
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 197);
            Controls.Add(chk_activo);
            Controls.Add(txt_concepto);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_concepto);
            Name = "add_concepto_compra";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar conceptos de compra";
            FormClosed += new FormClosedEventHandler(Add_marcai_FormClosed);
            Load += new EventHandler(Add_concepto_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal CheckBox chk_activo;
        internal TextBox txt_concepto;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_concepto;
    }
}
