using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_error_detalle : Form
    {

        // Form overrides dispose to clean up the component list.
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

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null!;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            txt_mensaje = new TextBox();
            Label1 = new Label();
            txt_detalles = new TextBox();
            txt_detalles.KeyDown += new KeyEventHandler(txt_detalles_KeyDown);
            Label2 = new Label();
            btn_copiar = new Button();
            btn_copiar.Click += new EventHandler(btn_copiar_Click);
            btn_cerrar = new Button();
            btn_cerrar.Click += new EventHandler(btn_cerrar_Click);
            SuspendLayout();
            // 
            // txt_mensaje
            // 
            txt_mensaje.Location = new Point(12, 30);
            txt_mensaje.Multiline = true;
            txt_mensaje.Name = "txt_mensaje";
            txt_mensaje.ReadOnly = true;
            txt_mensaje.ScrollBars = ScrollBars.Vertical;
            txt_mensaje.Size = new Size(760, 60);
            txt_mensaje.TabIndex = 0;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label1.Location = new Point(12, 14);
            Label1.Name = "Label1";
            Label1.Size = new Size(58, 13);
            Label1.TabIndex = 1;
            Label1.Text = "Mensaje:";
            // 
            // txt_detalles
            // 
            txt_detalles.Font = new Font("Consolas", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_detalles.Location = new Point(12, 120);
            txt_detalles.Multiline = true;
            txt_detalles.Name = "txt_detalles";
            txt_detalles.ReadOnly = true;
            txt_detalles.ScrollBars = ScrollBars.Both;
            txt_detalles.Size = new Size(760, 400);
            txt_detalles.TabIndex = 2;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label2.Location = new Point(12, 104);
            Label2.Name = "Label2";
            Label2.Size = new Size(58, 13);
            Label2.TabIndex = 3;
            Label2.Text = "Detalles:";
            // 
            // btn_copiar
            // 
            btn_copiar.Location = new Point(616, 540);
            btn_copiar.Name = "btn_copiar";
            btn_copiar.Size = new Size(75, 30);
            btn_copiar.TabIndex = 4;
            btn_copiar.Text = "Copiar";
            btn_copiar.UseVisualStyleBackColor = true;
            // 
            // btn_cerrar
            // 
            btn_cerrar.Location = new Point(697, 540);
            btn_cerrar.Name = "btn_cerrar";
            btn_cerrar.Size = new Size(75, 30);
            btn_cerrar.TabIndex = 5;
            btn_cerrar.Text = "Cerrar";
            btn_cerrar.UseVisualStyleBackColor = true;
            // 
            // frm_error_detalle
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 581);
            Controls.Add(btn_cerrar);
            Controls.Add(btn_copiar);
            Controls.Add(Label2);
            Controls.Add(txt_detalles);
            Controls.Add(Label1);
            Controls.Add(txt_mensaje);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frm_error_detalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalle del Error";
            Load += new EventHandler(frm_error_detalle_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal TextBox txt_mensaje;
        internal Label Label1;
        internal TextBox txt_detalles;
        internal Label Label2;
        internal Button btn_copiar;
        internal Button btn_cerrar;
    }
}


