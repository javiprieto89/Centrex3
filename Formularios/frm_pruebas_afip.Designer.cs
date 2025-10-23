using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_pruebas_afip : Form
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
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            GroupBox1 = new GroupBox();
            txt_password = new TextBox();
            Label4 = new Label();
            cmb_certificados = new ComboBox();
            Label3 = new Label();
            cmb_mode = new ComboBox();
            Label2 = new Label();
            txt_cuit = new TextBox();
            Label1 = new Label();
            GroupBox2 = new GroupBox();
            txt_tipo_comprobante = new TextBox();
            Label6 = new Label();
            txt_punto_venta = new TextBox();
            Label5 = new Label();
            GroupBox3 = new GroupBox();
            btn_probar_parametros = new Button();
            btn_probar_parametros.Click += new EventHandler(btn_probar_parametros_Click);
            btn_probar_ultimo_comprobante = new Button();
            btn_probar_ultimo_comprobante.Click += new EventHandler(btn_probar_ultimo_comprobante_Click);
            btn_probar_conexion = new Button();
            btn_probar_conexion.Click += new EventHandler(btn_probar_conexion_Click);
            GroupBox4 = new GroupBox();
            txt_resultado = new TextBox();
            btn_limpiar = new Button();
            btn_limpiar.Click += new EventHandler(btn_limpiar_Click);
            btn_cerrar = new Button();
            btn_cerrar.Click += new EventHandler(btn_cerrar_Click);
            btn_obtener_puntos_de_venta = new Button();
            btn_obtener_puntos_de_venta.Click += new EventHandler(Btn_obtener_puntos_de_venta_Click);
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            GroupBox3.SuspendLayout();
            GroupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(txt_password);
            GroupBox1.Controls.Add(Label4);
            GroupBox1.Controls.Add(cmb_certificados);
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(cmb_mode);
            GroupBox1.Controls.Add(Label2);
            GroupBox1.Controls.Add(txt_cuit);
            GroupBox1.Controls.Add(Label1);
            GroupBox1.Location = new Point(16, 15);
            GroupBox1.Margin = new Padding(4, 4, 4, 4);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(4, 4, 4, 4);
            GroupBox1.Size = new Size(533, 185);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Configuración AFIP";
            // 
            // txt_password
            // 
            txt_password.Location = new Point(160, 135);
            txt_password.Margin = new Padding(4, 4, 4, 4);
            txt_password.Name = "txt_password";
            txt_password.PasswordChar = '*';
            txt_password.Size = new Size(265, 22);
            txt_password.TabIndex = 7;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(20, 139);
            Label4.Margin = new Padding(4, 0, 4, 0);
            Label4.Name = "Label4";
            Label4.Size = new Size(119, 17);
            Label4.TabIndex = 6;
            Label4.Text = "Contraseña Cert.:";
            // 
            // cmb_certificados
            // 
            cmb_certificados.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_certificados.FormattingEnabled = true;
            cmb_certificados.Location = new Point(160, 98);
            cmb_certificados.Margin = new Padding(4, 4, 4, 4);
            cmb_certificados.Name = "cmb_certificados";
            cmb_certificados.Size = new Size(265, 24);
            cmb_certificados.TabIndex = 5;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(20, 102);
            Label3.Margin = new Padding(4, 0, 4, 0);
            Label3.Name = "Label3";
            Label3.Size = new Size(79, 17);
            Label3.TabIndex = 4;
            Label3.Text = "Certificado:";
            // 
            // cmb_mode
            // 
            cmb_mode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_mode.FormattingEnabled = true;
            cmb_mode.Location = new Point(160, 62);
            cmb_mode.Margin = new Padding(4, 4, 4, 4);
            cmb_mode.Name = "cmb_mode";
            cmb_mode.Size = new Size(132, 24);
            cmb_mode.TabIndex = 3;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(20, 65);
            Label2.Margin = new Padding(4, 0, 4, 0);
            Label2.Name = "Label2";
            Label2.Size = new Size(47, 17);
            Label2.TabIndex = 2;
            Label2.Text = "Modo:";
            // 
            // txt_cuit
            // 
            txt_cuit.Location = new Point(160, 25);
            txt_cuit.Margin = new Padding(4, 4, 4, 4);
            txt_cuit.Name = "txt_cuit";
            txt_cuit.Size = new Size(199, 22);
            txt_cuit.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(20, 28);
            Label1.Margin = new Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(90, 17);
            Label1.TabIndex = 0;
            Label1.Text = "CUIT Emisor:";
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(txt_tipo_comprobante);
            GroupBox2.Controls.Add(Label6);
            GroupBox2.Controls.Add(txt_punto_venta);
            GroupBox2.Controls.Add(Label5);
            GroupBox2.Location = new Point(16, 222);
            GroupBox2.Margin = new Padding(4, 4, 4, 4);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(4, 4, 4, 4);
            GroupBox2.Size = new Size(533, 98);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Parámetros de Consulta";
            // 
            // txt_tipo_comprobante
            // 
            txt_tipo_comprobante.Location = new Point(160, 55);
            txt_tipo_comprobante.Margin = new Padding(4, 4, 4, 4);
            txt_tipo_comprobante.Name = "txt_tipo_comprobante";
            txt_tipo_comprobante.Size = new Size(132, 22);
            txt_tipo_comprobante.TabIndex = 3;
            txt_tipo_comprobante.Text = "1";
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Location = new Point(20, 59);
            Label6.Margin = new Padding(4, 0, 4, 0);
            Label6.Name = "Label6";
            Label6.Size = new Size(129, 17);
            Label6.TabIndex = 2;
            Label6.Text = "Tipo Comprobante:";
            // 
            // txt_punto_venta
            // 
            txt_punto_venta.Location = new Point(160, 18);
            txt_punto_venta.Margin = new Padding(4, 4, 4, 4);
            txt_punto_venta.Name = "txt_punto_venta";
            txt_punto_venta.Size = new Size(132, 22);
            txt_punto_venta.TabIndex = 1;
            txt_punto_venta.Text = "1";
            // 
            // Label5
            // 
            Label5.AutoSize = true;
            Label5.Location = new Point(20, 22);
            Label5.Margin = new Padding(4, 0, 4, 0);
            Label5.Name = "Label5";
            Label5.Size = new Size(110, 17);
            Label5.TabIndex = 0;
            Label5.Text = "Punto de Venta:";
            // 
            // GroupBox3
            // 
            GroupBox3.Controls.Add(btn_probar_parametros);
            GroupBox3.Controls.Add(btn_probar_ultimo_comprobante);
            GroupBox3.Controls.Add(btn_probar_conexion);
            GroupBox3.Location = new Point(16, 345);
            GroupBox3.Margin = new Padding(4, 4, 4, 4);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Padding = new Padding(4, 4, 4, 4);
            GroupBox3.Size = new Size(765, 98);
            GroupBox3.TabIndex = 2;
            GroupBox3.TabStop = false;
            GroupBox3.Text = "Pruebas";
            // 
            // btn_probar_parametros
            // 
            btn_probar_parametros.Location = new Point(360, 31);
            btn_probar_parametros.Margin = new Padding(4, 4, 4, 4);
            btn_probar_parametros.Name = "btn_probar_parametros";
            btn_probar_parametros.Size = new Size(160, 43);
            btn_probar_parametros.TabIndex = 2;
            btn_probar_parametros.Text = "Probar Parámetros";
            btn_probar_parametros.UseVisualStyleBackColor = true;
            // 
            // btn_probar_ultimo_comprobante
            // 
            btn_probar_ultimo_comprobante.Location = new Point(187, 31);
            btn_probar_ultimo_comprobante.Margin = new Padding(4, 4, 4, 4);
            btn_probar_ultimo_comprobante.Name = "btn_probar_ultimo_comprobante";
            btn_probar_ultimo_comprobante.Size = new Size(160, 43);
            btn_probar_ultimo_comprobante.TabIndex = 1;
            btn_probar_ultimo_comprobante.Text = "Último Comprobante";
            btn_probar_ultimo_comprobante.UseVisualStyleBackColor = true;
            // 
            // btn_probar_conexion
            // 
            btn_probar_conexion.Location = new Point(13, 31);
            btn_probar_conexion.Margin = new Padding(4, 4, 4, 4);
            btn_probar_conexion.Name = "btn_probar_conexion";
            btn_probar_conexion.Size = new Size(160, 43);
            btn_probar_conexion.TabIndex = 0;
            btn_probar_conexion.Text = "Probar Conexión";
            btn_probar_conexion.UseVisualStyleBackColor = true;
            // 
            // GroupBox4
            // 
            GroupBox4.Controls.Add(txt_resultado);
            GroupBox4.Location = new Point(16, 468);
            GroupBox4.Margin = new Padding(4, 4, 4, 4);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Padding = new Padding(4, 4, 4, 4);
            GroupBox4.Size = new Size(800, 246);
            GroupBox4.TabIndex = 3;
            GroupBox4.TabStop = false;
            GroupBox4.Text = "Resultados";
            // 
            // txt_resultado
            // 
            txt_resultado.Location = new Point(13, 25);
            txt_resultado.Margin = new Padding(4, 4, 4, 4);
            txt_resultado.Multiline = true;
            txt_resultado.Name = "txt_resultado";
            txt_resultado.ReadOnly = true;
            txt_resultado.ScrollBars = ScrollBars.Vertical;
            txt_resultado.Size = new Size(772, 208);
            txt_resultado.TabIndex = 0;
            // 
            // btn_limpiar
            // 
            btn_limpiar.Location = new Point(600, 726);
            btn_limpiar.Margin = new Padding(4, 4, 4, 4);
            btn_limpiar.Name = "btn_limpiar";
            btn_limpiar.Size = new Size(100, 37);
            btn_limpiar.TabIndex = 4;
            btn_limpiar.Text = "Limpiar";
            btn_limpiar.UseVisualStyleBackColor = true;
            // 
            // btn_cerrar
            // 
            btn_cerrar.Location = new Point(713, 726);
            btn_cerrar.Margin = new Padding(4, 4, 4, 4);
            btn_cerrar.Name = "btn_cerrar";
            btn_cerrar.Size = new Size(100, 37);
            btn_cerrar.TabIndex = 5;
            btn_cerrar.Text = "Cerrar";
            btn_cerrar.UseVisualStyleBackColor = true;
            // 
            // btn_obtener_puntos_de_venta
            // 
            btn_obtener_puntos_de_venta.Location = new Point(557, 376);
            btn_obtener_puntos_de_venta.Margin = new Padding(4);
            btn_obtener_puntos_de_venta.Name = "btn_obtener_puntos_de_venta";
            btn_obtener_puntos_de_venta.Size = new Size(160, 43);
            btn_obtener_puntos_de_venta.TabIndex = 3;
            btn_obtener_puntos_de_venta.Text = "Obtener puntos de venta";
            btn_obtener_puntos_de_venta.UseVisualStyleBackColor = true;
            // 
            // frm_pruebas_afip
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(832, 777);
            Controls.Add(btn_obtener_puntos_de_venta);
            Controls.Add(btn_cerrar);
            Controls.Add(btn_limpiar);
            Controls.Add(GroupBox4);
            Controls.Add(GroupBox3);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frm_pruebas_afip";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pruebas AFIP";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            GroupBox3.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            GroupBox4.PerformLayout();
            Load += new EventHandler(frm_pruebas_afip_Load);
            ResumeLayout(false);

        }

        internal GroupBox GroupBox1;
        internal TextBox txt_password;
        internal Label Label4;
        internal ComboBox cmb_certificados;
        internal Label Label3;
        internal ComboBox cmb_mode;
        internal Label Label2;
        internal TextBox txt_cuit;
        internal Label Label1;
        internal GroupBox GroupBox2;
        internal TextBox txt_tipo_comprobante;
        internal Label Label6;
        internal TextBox txt_punto_venta;
        internal Label Label5;
        internal GroupBox GroupBox3;
        internal Button btn_probar_parametros;
        internal Button btn_probar_ultimo_comprobante;
        internal Button btn_probar_conexion;
        internal GroupBox GroupBox4;
        internal TextBox txt_resultado;
        internal Button btn_limpiar;
        internal Button btn_cerrar;
        internal Button btn_obtener_puntos_de_venta;
    }
}


