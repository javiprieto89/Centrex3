using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_consulta : Form
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
            txt_consulta = new TextBox();
            lbl_consulta = new Label();
            chk_activo = new CheckBox();
            txt_nombre = new TextBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_nombre = new Label();
            SuspendLayout();
            // 
            // txt_consulta
            // 
            txt_consulta.Location = new Point(22, 86);
            txt_consulta.Multiline = true;
            txt_consulta.Name = "txt_consulta";
            txt_consulta.Size = new Size(983, 493);
            txt_consulta.TabIndex = 22;
            // 
            // lbl_consulta
            // 
            lbl_consulta.AutoSize = true;
            lbl_consulta.Location = new Point(19, 57);
            lbl_consulta.Name = "lbl_consulta";
            lbl_consulta.Size = new Size(72, 13);
            lbl_consulta.TabIndex = 28;
            lbl_consulta.Text = "Consulta SQL";
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(22, 596);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(99, 17);
            chk_activo.TabIndex = 23;
            chk_activo.Text = "Consulta activa";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(149, 21);
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(856, 20);
            txt_nombre.TabIndex = 21;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(22, 628);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 24;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(531, 655);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 26;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(433, 655);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 25;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_nombre
            // 
            lbl_nombre.AutoSize = true;
            lbl_nombre.Location = new Point(19, 24);
            lbl_nombre.Name = "lbl_nombre";
            lbl_nombre.Size = new Size(113, 13);
            lbl_nombre.TabIndex = 27;
            lbl_nombre.Text = "Nombre de la consulta";
            // 
            // add_consulta
            // 
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 698);
            Controls.Add(txt_consulta);
            Controls.Add(lbl_consulta);
            Controls.Add(chk_activo);
            Controls.Add(txt_nombre);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_nombre);
            Name = "add_consulta";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar consulta personalizada";
            FormClosed += new FormClosedEventHandler(add_Consulta_FormClosed);
            Load += new EventHandler(add_Consulta_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TextBox txt_consulta;
        internal Label lbl_consulta;
        internal CheckBox chk_activo;
        internal TextBox txt_nombre;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_nombre;
    }
}


