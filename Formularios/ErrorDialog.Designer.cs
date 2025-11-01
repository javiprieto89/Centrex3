using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    partial class ErrorDialog
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtError;
        private Button btnCerrar;
        private Button btnCopiar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtError = new TextBox();
            btnCerrar = new Button();
            btnCopiar = new Button();
            SuspendLayout();

            // txtError
            txtError.Multiline = true;
            txtError.ReadOnly = true;
            txtError.WordWrap = false;
            txtError.ScrollBars = ScrollBars.Both;
            txtError.Dock = DockStyle.Fill;
            txtError.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtError.BackColor = Color.White;

            // btnCerrar
            btnCerrar.Text = "Cerrar";
            btnCerrar.Dock = DockStyle.Bottom;
            btnCerrar.Height = 35;
            btnCerrar.Click += btnCerrar_Click;

            // btnCopiar
            btnCopiar.Text = "Copiar";
            btnCopiar.Dock = DockStyle.Bottom;
            btnCopiar.Height = 35;
            btnCopiar.Click += btnCopiar_Click;

            // ErrorDialog
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(txtError);
            Controls.Add(btnCopiar);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ErrorDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Centrex - Error";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
