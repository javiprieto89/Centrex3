using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_marcai : Form
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
            lbl_marca = new Label();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            txt_marca = new TextBox();
            chk_activo = new CheckBox();
            SuspendLayout();
            // 
            // lbl_marca
            // 
            lbl_marca.AutoSize = true;
            lbl_marca.Location = new Point(22, 28);
            lbl_marca.Name = "lbl_marca";
            lbl_marca.Size = new Size(37, 13);
            lbl_marca.TabIndex = 0;
            lbl_marca.Text = "Marca";
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(24, 89);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 2;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(192, 125);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 4;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(94, 125);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 3;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // txt_marca
            // 
            txt_marca.Location = new Point(78, 21);
            txt_marca.Name = "txt_marca";
            txt_marca.Size = new Size(259, 20);
            txt_marca.TabIndex = 0;
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(24, 57);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(88, 17);
            chk_activo.TabIndex = 1;
            chk_activo.Text = "Marca activa";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // add_marcai
            // 
            AcceptButton = cmd_ok;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmd_exit;
            ClientSize = new Size(358, 160);
            Controls.Add(chk_activo);
            Controls.Add(txt_marca);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_marca);
            Name = "add_marcai";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Insertar marca del item";
            FormClosed += new FormClosedEventHandler(add_marcai_FormClosed);
            Load += new EventHandler(add_marcai_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_marca;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal TextBox txt_marca;
        internal CheckBox chk_activo;
    }
}


