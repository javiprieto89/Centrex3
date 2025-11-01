using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class muestra_texto : Form
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
            txt_msg = new TextBox();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            SuspendLayout();
            // 
            // txt_msg
            // 
            txt_msg.Location = new Point(42, 46);
            txt_msg.Multiline = true;
            txt_msg.Name = "txt_msg";
            txt_msg.ReadOnly = true;
            txt_msg.Size = new Size(582, 261);
            txt_msg.TabIndex = 0;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(272, 370);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(123, 34);
            cmd_ok.TabIndex = 1;
            cmd_ok.Text = "Cerrar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // muestra_texto
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(666, 450);
            Controls.Add(cmd_ok);
            Controls.Add(txt_msg);
            FormBorderStyle = FormBorderStyle.None;
            Name = "muestra_texto";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Popup";
            TopMost = true;
            Load += new EventHandler(muestra_texto_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal TextBox txt_msg;
        internal Button cmd_ok;
    }
}


