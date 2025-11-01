using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_mercadopago_qr : Form
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
            lbl_titulo = new Label();
            pic_qr = new PictureBox();
            lbl_instrucciones = new Label();
            lbl_monto = new Label();
            lbl_referencia = new Label();
            btn_generar_qr = new Button();
            btn_generar_qr.Click += new EventHandler(btn_generar_qr_Click);
            btn_copiar_link = new Button();
            btn_copiar_link.Click += new EventHandler(btn_copiar_link_Click);
            btn_cerrar = new Button();
            btn_cerrar.Click += new EventHandler(btn_cerrar_Click);
            ((System.ComponentModel.ISupportInitialize)pic_qr).BeginInit();
            SuspendLayout();
            // 
            // lbl_titulo
            // 
            lbl_titulo.AutoSize = true;
            lbl_titulo.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_titulo.Location = new Point(12, 9);
            lbl_titulo.Name = "lbl_titulo";
            lbl_titulo.Size = new Size(200, 24);
            lbl_titulo.TabIndex = 0;
            lbl_titulo.Text = "Escanear QR para Pagar";
            // 
            // pic_qr
            // 
            pic_qr.BorderStyle = BorderStyle.FixedSingle;
            pic_qr.Location = new Point(100, 50);
            pic_qr.Name = "pic_qr";
            pic_qr.Size = new Size(200, 200);
            pic_qr.TabIndex = 1;
            pic_qr.TabStop = false;
            // 
            // lbl_instrucciones
            // 
            lbl_instrucciones.AutoSize = true;
            lbl_instrucciones.Location = new Point(12, 270);
            lbl_instrucciones.Name = "lbl_instrucciones";
            lbl_instrucciones.Size = new Size(200, 65);
            lbl_instrucciones.TabIndex = 2;
            lbl_instrucciones.Text = "1. Abre la app de MercadoPago" + '\r' + '\n' + "2. Toca 'Escanear código'" + '\r' + '\n' + "3. Apunta la cámara al QR" + '\r' + '\n' + "4. Confirma el pago";
            // 
            // lbl_monto
            // 
            lbl_monto.AutoSize = true;
            lbl_monto.Font = new Font("Microsoft Sans Serif", 10.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_monto.Location = new Point(12, 350);
            lbl_monto.Name = "lbl_monto";
            lbl_monto.Size = new Size(100, 17);
            lbl_monto.TabIndex = 3;
            lbl_monto.Text = "Monto: $0.00";
            // 
            // lbl_referencia
            // 
            lbl_referencia.AutoSize = true;
            lbl_referencia.Location = new Point(12, 375);
            lbl_referencia.Name = "lbl_referencia";
            lbl_referencia.Size = new Size(100, 13);
            lbl_referencia.TabIndex = 4;
            lbl_referencia.Text = "Referencia: ";
            // 
            // btn_generar_qr
            // 
            btn_generar_qr.Location = new Point(12, 400);
            btn_generar_qr.Name = "btn_generar_qr";
            btn_generar_qr.Size = new Size(100, 30);
            btn_generar_qr.TabIndex = 5;
            btn_generar_qr.Text = "Generar Nuevo QR";
            btn_generar_qr.UseVisualStyleBackColor = true;
            // 
            // btn_copiar_link
            // 
            btn_copiar_link.Location = new Point(150, 400);
            btn_copiar_link.Name = "btn_copiar_link";
            btn_copiar_link.Size = new Size(100, 30);
            btn_copiar_link.TabIndex = 6;
            btn_copiar_link.Text = "Copiar Link";
            btn_copiar_link.UseVisualStyleBackColor = true;
            // 
            // btn_cerrar
            // 
            btn_cerrar.Location = new Point(300, 400);
            btn_cerrar.Name = "btn_cerrar";
            btn_cerrar.Size = new Size(75, 30);
            btn_cerrar.TabIndex = 7;
            btn_cerrar.Text = "Cerrar";
            btn_cerrar.UseVisualStyleBackColor = true;
            // 
            // frm_mercadopago_qr
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 450);
            Controls.Add(btn_cerrar);
            Controls.Add(btn_copiar_link);
            Controls.Add(btn_generar_qr);
            Controls.Add(lbl_referencia);
            Controls.Add(lbl_monto);
            Controls.Add(lbl_instrucciones);
            Controls.Add(pic_qr);
            Controls.Add(lbl_titulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frm_mercadopago_qr";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pago con MercadoPago";
            ((System.ComponentModel.ISupportInitialize)pic_qr).EndInit();
            Load += new EventHandler(frm_mercadopago_qr_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_titulo;
        internal PictureBox pic_qr;
        internal Label lbl_instrucciones;
        internal Label lbl_monto;
        internal Label lbl_referencia;
        internal Button btn_generar_qr;
        internal Button btn_copiar_link;
        internal Button btn_cerrar;
    }
}


