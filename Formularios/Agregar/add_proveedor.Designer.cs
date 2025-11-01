using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_proveedor : Form
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
            TabControl1 = new TabControl();
            tab_general = new TabPage();
            cmb_claseFiscal = new ComboBox();
            lbl_claseFiscal = new Label();
            txt_vendedor = new TextBox();
            lbl_vendedor = new Label();
            cmb_tipoDocumento = new ComboBox();
            lbl_tipoDocumento = new Label();
            txt_email = new TextBox();
            lbl_email = new Label();
            txt_contacto = new TextBox();
            lbl_contacto = new Label();
            txt_telefono = new TextBox();
            lbl_tel = new Label();
            txt_taxNumber = new TextBox();
            txt_taxNumber.KeyPress += new KeyPressEventHandler(txt_taxNumber_KeyPress);
            lbl_taxNumber = new Label();
            txt_celular = new TextBox();
            lbl_celular = new Label();
            txt_razonSocial = new TextBox();
            lbl_razonSocial = new Label();
            tab_fiscal = new TabPage();
            lbl_cpFiscal = new Label();
            txt_cpFiscal = new TextBox();
            cmb_paisFiscal = new ComboBox();
            cmb_paisFiscal.SelectionChangeCommitted += new EventHandler(cmb_paisFiscal_SelectionChangeCommitted);
            lbl_paisFiscal = new Label();
            cmb_provinciaFiscal = new ComboBox();
            txt_direccionFiscal = new TextBox();
            lbl_direccionFiscal = new Label();
            lbl_provinciaFiscal = new Label();
            lbl_localidadFiscal = new Label();
            txt_localidadFiscal = new TextBox();
            tab_entrega = new TabPage();
            lbl_cpEntrega = new Label();
            txt_cpEntrega = new TextBox();
            cmb_paisEntrega = new ComboBox();
            cmb_paisEntrega.SelectionChangeCommitted += new EventHandler(cmb_paisEntrega_SelectionChangeCommitted);
            lbl_paisEntrega = new Label();
            cmb_provinciaEntrega = new ComboBox();
            txt_direccionEntrega = new TextBox();
            lbl_direccionEntrega = new Label();
            lbl_provinciaEntrega = new Label();
            lbl_localidadEntrega = new Label();
            txt_localidadEntrega = new TextBox();
            txt_notas = new TextBox();
            lbl_notas = new Label();
            chk_esInscripto = new CheckBox();
            chk_activo = new CheckBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            TabControl1.SuspendLayout();
            tab_general.SuspendLayout();
            tab_fiscal.SuspendLayout();
            tab_entrega.SuspendLayout();
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(tab_general);
            TabControl1.Controls.Add(tab_fiscal);
            TabControl1.Controls.Add(tab_entrega);
            TabControl1.Location = new Point(17, 17);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(349, 376);
            TabControl1.TabIndex = 0;
            // 
            // tab_general
            // 
            tab_general.BackColor = SystemColors.Control;
            tab_general.Controls.Add(cmb_claseFiscal);
            tab_general.Controls.Add(lbl_claseFiscal);
            tab_general.Controls.Add(txt_vendedor);
            tab_general.Controls.Add(lbl_vendedor);
            tab_general.Controls.Add(cmb_tipoDocumento);
            tab_general.Controls.Add(lbl_tipoDocumento);
            tab_general.Controls.Add(txt_email);
            tab_general.Controls.Add(lbl_email);
            tab_general.Controls.Add(txt_contacto);
            tab_general.Controls.Add(lbl_contacto);
            tab_general.Controls.Add(txt_telefono);
            tab_general.Controls.Add(lbl_tel);
            tab_general.Controls.Add(txt_taxNumber);
            tab_general.Controls.Add(lbl_taxNumber);
            tab_general.Controls.Add(txt_celular);
            tab_general.Controls.Add(lbl_celular);
            tab_general.Controls.Add(txt_razonSocial);
            tab_general.Controls.Add(lbl_razonSocial);
            tab_general.Location = new Point(4, 22);
            tab_general.Name = "tab_general";
            tab_general.Padding = new Padding(3);
            tab_general.Size = new Size(341, 350);
            tab_general.TabIndex = 0;
            tab_general.Text = "General";
            // 
            // cmb_claseFiscal
            // 
            cmb_claseFiscal.FormattingEnabled = true;
            cmb_claseFiscal.Location = new Point(149, 66);
            cmb_claseFiscal.Name = "cmb_claseFiscal";
            cmb_claseFiscal.Size = new Size(163, 21);
            cmb_claseFiscal.TabIndex = 1;
            // 
            // lbl_claseFiscal
            // 
            lbl_claseFiscal.AutoSize = true;
            lbl_claseFiscal.Location = new Point(28, 69);
            lbl_claseFiscal.Name = "lbl_claseFiscal";
            lbl_claseFiscal.Size = new Size(60, 13);
            lbl_claseFiscal.TabIndex = 63;
            lbl_claseFiscal.Text = "Clase fiscal";
            // 
            // txt_vendedor
            // 
            txt_vendedor.Location = new Point(149, 204);
            txt_vendedor.MaxLength = 45;
            txt_vendedor.Name = "txt_vendedor";
            txt_vendedor.Size = new Size(163, 20);
            txt_vendedor.TabIndex = 5;
            // 
            // lbl_vendedor
            // 
            lbl_vendedor.AutoSize = true;
            lbl_vendedor.Location = new Point(28, 205);
            lbl_vendedor.Name = "lbl_vendedor";
            lbl_vendedor.Size = new Size(53, 13);
            lbl_vendedor.TabIndex = 61;
            lbl_vendedor.Text = "Vendedor";
            // 
            // cmb_tipoDocumento
            // 
            cmb_tipoDocumento.FormattingEnabled = true;
            cmb_tipoDocumento.Location = new Point(149, 101);
            cmb_tipoDocumento.Name = "cmb_tipoDocumento";
            cmb_tipoDocumento.Size = new Size(163, 21);
            cmb_tipoDocumento.TabIndex = 2;
            // 
            // lbl_tipoDocumento
            // 
            lbl_tipoDocumento.AutoSize = true;
            lbl_tipoDocumento.Location = new Point(28, 103);
            lbl_tipoDocumento.Name = "lbl_tipoDocumento";
            lbl_tipoDocumento.Size = new Size(99, 13);
            lbl_tipoDocumento.TabIndex = 59;
            lbl_tipoDocumento.Text = "Tipo de documento";
            // 
            // txt_email
            // 
            txt_email.Location = new Point(149, 306);
            txt_email.MaxLength = 45;
            txt_email.Name = "txt_email";
            txt_email.Size = new Size(163, 20);
            txt_email.TabIndex = 8;
            // 
            // lbl_email
            // 
            lbl_email.AutoSize = true;
            lbl_email.Location = new Point(28, 307);
            lbl_email.Name = "lbl_email";
            lbl_email.Size = new Size(32, 13);
            lbl_email.TabIndex = 58;
            lbl_email.Text = "Email";
            // 
            // txt_contacto
            // 
            txt_contacto.Location = new Point(149, 170);
            txt_contacto.MaxLength = 45;
            txt_contacto.Name = "txt_contacto";
            txt_contacto.Size = new Size(163, 20);
            txt_contacto.TabIndex = 4;
            // 
            // lbl_contacto
            // 
            lbl_contacto.AutoSize = true;
            lbl_contacto.Location = new Point(28, 171);
            lbl_contacto.Name = "lbl_contacto";
            lbl_contacto.Size = new Size(50, 13);
            lbl_contacto.TabIndex = 56;
            lbl_contacto.Text = "Contacto";
            // 
            // txt_telefono
            // 
            txt_telefono.Location = new Point(149, 238);
            txt_telefono.MaxLength = 45;
            txt_telefono.Name = "txt_telefono";
            txt_telefono.Size = new Size(163, 20);
            txt_telefono.TabIndex = 6;
            // 
            // lbl_tel
            // 
            lbl_tel.AutoSize = true;
            lbl_tel.Location = new Point(28, 239);
            lbl_tel.Name = "lbl_tel";
            lbl_tel.Size = new Size(49, 13);
            lbl_tel.TabIndex = 55;
            lbl_tel.Text = "Teléfono";
            // 
            // txt_taxNumber
            // 
            txt_taxNumber.Location = new Point(149, 136);
            txt_taxNumber.MaxLength = 13;
            txt_taxNumber.Name = "txt_taxNumber";
            txt_taxNumber.Size = new Size(163, 20);
            txt_taxNumber.TabIndex = 3;
            // 
            // lbl_taxNumber
            // 
            lbl_taxNumber.AutoSize = true;
            lbl_taxNumber.Location = new Point(28, 137);
            lbl_taxNumber.Name = "lbl_taxNumber";
            lbl_taxNumber.Size = new Size(85, 13);
            lbl_taxNumber.TabIndex = 53;
            lbl_taxNumber.Text = "CUIT/CUIL/DNI";
            // 
            // txt_celular
            // 
            txt_celular.Location = new Point(149, 272);
            txt_celular.MaxLength = 45;
            txt_celular.Name = "txt_celular";
            txt_celular.Size = new Size(163, 20);
            txt_celular.TabIndex = 7;
            // 
            // lbl_celular
            // 
            lbl_celular.AutoSize = true;
            lbl_celular.Location = new Point(28, 273);
            lbl_celular.Name = "lbl_celular";
            lbl_celular.Size = new Size(39, 13);
            lbl_celular.TabIndex = 50;
            lbl_celular.Text = "Celular";
            // 
            // txt_razonSocial
            // 
            txt_razonSocial.Location = new Point(149, 32);
            txt_razonSocial.MaxLength = 45;
            txt_razonSocial.Name = "txt_razonSocial";
            txt_razonSocial.Size = new Size(163, 20);
            txt_razonSocial.TabIndex = 0;
            // 
            // lbl_razonSocial
            // 
            lbl_razonSocial.AutoSize = true;
            lbl_razonSocial.Location = new Point(28, 35);
            lbl_razonSocial.Name = "lbl_razonSocial";
            lbl_razonSocial.Size = new Size(68, 13);
            lbl_razonSocial.TabIndex = 47;
            lbl_razonSocial.Text = "Razón social";
            // 
            // tab_fiscal
            // 
            tab_fiscal.BackColor = SystemColors.Control;
            tab_fiscal.Controls.Add(lbl_cpFiscal);
            tab_fiscal.Controls.Add(txt_cpFiscal);
            tab_fiscal.Controls.Add(cmb_paisFiscal);
            tab_fiscal.Controls.Add(lbl_paisFiscal);
            tab_fiscal.Controls.Add(cmb_provinciaFiscal);
            tab_fiscal.Controls.Add(txt_direccionFiscal);
            tab_fiscal.Controls.Add(lbl_direccionFiscal);
            tab_fiscal.Controls.Add(lbl_provinciaFiscal);
            tab_fiscal.Controls.Add(lbl_localidadFiscal);
            tab_fiscal.Controls.Add(txt_localidadFiscal);
            tab_fiscal.Location = new Point(4, 22);
            tab_fiscal.Name = "tab_fiscal";
            tab_fiscal.Padding = new Padding(3);
            tab_fiscal.Size = new Size(341, 350);
            tab_fiscal.TabIndex = 1;
            tab_fiscal.Text = "Información fiscal";
            // 
            // lbl_cpFiscal
            // 
            lbl_cpFiscal.AutoSize = true;
            lbl_cpFiscal.Location = new Point(28, 307);
            lbl_cpFiscal.Name = "lbl_cpFiscal";
            lbl_cpFiscal.Size = new Size(21, 13);
            lbl_cpFiscal.TabIndex = 43;
            lbl_cpFiscal.Text = "CP";
            // 
            // txt_cpFiscal
            // 
            txt_cpFiscal.Location = new Point(149, 300);
            txt_cpFiscal.MaxLength = 200;
            txt_cpFiscal.Name = "txt_cpFiscal";
            txt_cpFiscal.Size = new Size(163, 20);
            txt_cpFiscal.TabIndex = 13;
            // 
            // cmb_paisFiscal
            // 
            cmb_paisFiscal.FormattingEnabled = true;
            cmb_paisFiscal.Location = new Point(149, 27);
            cmb_paisFiscal.Name = "cmb_paisFiscal";
            cmb_paisFiscal.Size = new Size(163, 21);
            cmb_paisFiscal.TabIndex = 9;
            // 
            // lbl_paisFiscal
            // 
            lbl_paisFiscal.AutoSize = true;
            lbl_paisFiscal.Location = new Point(28, 35);
            lbl_paisFiscal.Name = "lbl_paisFiscal";
            lbl_paisFiscal.Size = new Size(29, 13);
            lbl_paisFiscal.TabIndex = 40;
            lbl_paisFiscal.Text = "País";
            // 
            // cmb_provinciaFiscal
            // 
            cmb_provinciaFiscal.FormattingEnabled = true;
            cmb_provinciaFiscal.Location = new Point(149, 95);
            cmb_provinciaFiscal.Name = "cmb_provinciaFiscal";
            cmb_provinciaFiscal.Size = new Size(163, 21);
            cmb_provinciaFiscal.TabIndex = 10;
            // 
            // txt_direccionFiscal
            // 
            txt_direccionFiscal.Location = new Point(149, 164);
            txt_direccionFiscal.MaxLength = 200;
            txt_direccionFiscal.Name = "txt_direccionFiscal";
            txt_direccionFiscal.Size = new Size(163, 20);
            txt_direccionFiscal.TabIndex = 11;
            // 
            // lbl_direccionFiscal
            // 
            lbl_direccionFiscal.AutoSize = true;
            lbl_direccionFiscal.Location = new Point(28, 171);
            lbl_direccionFiscal.Name = "lbl_direccionFiscal";
            lbl_direccionFiscal.Size = new Size(79, 13);
            lbl_direccionFiscal.TabIndex = 6;
            lbl_direccionFiscal.Text = "Dirección fiscal";
            // 
            // lbl_provinciaFiscal
            // 
            lbl_provinciaFiscal.AutoSize = true;
            lbl_provinciaFiscal.Location = new Point(28, 103);
            lbl_provinciaFiscal.Name = "lbl_provinciaFiscal";
            lbl_provinciaFiscal.Size = new Size(51, 13);
            lbl_provinciaFiscal.TabIndex = 35;
            lbl_provinciaFiscal.Text = "Provincia";
            // 
            // lbl_localidadFiscal
            // 
            lbl_localidadFiscal.AutoSize = true;
            lbl_localidadFiscal.Location = new Point(28, 239);
            lbl_localidadFiscal.Name = "lbl_localidadFiscal";
            lbl_localidadFiscal.Size = new Size(53, 13);
            lbl_localidadFiscal.TabIndex = 38;
            lbl_localidadFiscal.Text = "Localidad";
            // 
            // txt_localidadFiscal
            // 
            txt_localidadFiscal.Location = new Point(149, 232);
            txt_localidadFiscal.MaxLength = 200;
            txt_localidadFiscal.Name = "txt_localidadFiscal";
            txt_localidadFiscal.Size = new Size(163, 20);
            txt_localidadFiscal.TabIndex = 12;
            // 
            // tab_entrega
            // 
            tab_entrega.BackColor = SystemColors.Control;
            tab_entrega.Controls.Add(lbl_cpEntrega);
            tab_entrega.Controls.Add(txt_cpEntrega);
            tab_entrega.Controls.Add(cmb_paisEntrega);
            tab_entrega.Controls.Add(lbl_paisEntrega);
            tab_entrega.Controls.Add(cmb_provinciaEntrega);
            tab_entrega.Controls.Add(txt_direccionEntrega);
            tab_entrega.Controls.Add(lbl_direccionEntrega);
            tab_entrega.Controls.Add(lbl_provinciaEntrega);
            tab_entrega.Controls.Add(lbl_localidadEntrega);
            tab_entrega.Controls.Add(txt_localidadEntrega);
            tab_entrega.Location = new Point(4, 22);
            tab_entrega.Name = "tab_entrega";
            tab_entrega.Size = new Size(341, 350);
            tab_entrega.TabIndex = 2;
            tab_entrega.Text = "Información de entrega";
            // 
            // lbl_cpEntrega
            // 
            lbl_cpEntrega.AutoSize = true;
            lbl_cpEntrega.Location = new Point(28, 307);
            lbl_cpEntrega.Name = "lbl_cpEntrega";
            lbl_cpEntrega.Size = new Size(21, 13);
            lbl_cpEntrega.TabIndex = 6;
            lbl_cpEntrega.Text = "CP";
            // 
            // txt_cpEntrega
            // 
            txt_cpEntrega.Location = new Point(149, 300);
            txt_cpEntrega.MaxLength = 200;
            txt_cpEntrega.Name = "txt_cpEntrega";
            txt_cpEntrega.Size = new Size(163, 20);
            txt_cpEntrega.TabIndex = 18;
            // 
            // cmb_paisEntrega
            // 
            cmb_paisEntrega.FormattingEnabled = true;
            cmb_paisEntrega.Location = new Point(149, 27);
            cmb_paisEntrega.Name = "cmb_paisEntrega";
            cmb_paisEntrega.Size = new Size(163, 21);
            cmb_paisEntrega.TabIndex = 14;
            // 
            // lbl_paisEntrega
            // 
            lbl_paisEntrega.AutoSize = true;
            lbl_paisEntrega.Location = new Point(28, 35);
            lbl_paisEntrega.Name = "lbl_paisEntrega";
            lbl_paisEntrega.Size = new Size(29, 13);
            lbl_paisEntrega.TabIndex = 1;
            lbl_paisEntrega.Text = "País";
            // 
            // cmb_provinciaEntrega
            // 
            cmb_provinciaEntrega.FormattingEnabled = true;
            cmb_provinciaEntrega.Location = new Point(149, 95);
            cmb_provinciaEntrega.Name = "cmb_provinciaEntrega";
            cmb_provinciaEntrega.Size = new Size(163, 21);
            cmb_provinciaEntrega.TabIndex = 15;
            // 
            // txt_direccionEntrega
            // 
            txt_direccionEntrega.Location = new Point(149, 164);
            txt_direccionEntrega.MaxLength = 200;
            txt_direccionEntrega.Name = "txt_direccionEntrega";
            txt_direccionEntrega.Size = new Size(163, 20);
            txt_direccionEntrega.TabIndex = 16;
            // 
            // lbl_direccionEntrega
            // 
            lbl_direccionEntrega.AutoSize = true;
            lbl_direccionEntrega.Location = new Point(28, 171);
            lbl_direccionEntrega.Name = "lbl_direccionEntrega";
            lbl_direccionEntrega.Size = new Size(79, 13);
            lbl_direccionEntrega.TabIndex = 7;
            lbl_direccionEntrega.Text = "Dirección fiscal";
            // 
            // lbl_provinciaEntrega
            // 
            lbl_provinciaEntrega.AutoSize = true;
            lbl_provinciaEntrega.Location = new Point(28, 103);
            lbl_provinciaEntrega.Name = "lbl_provinciaEntrega";
            lbl_provinciaEntrega.Size = new Size(51, 13);
            lbl_provinciaEntrega.TabIndex = 7;
            lbl_provinciaEntrega.Text = "Provincia";
            // 
            // lbl_localidadEntrega
            // 
            lbl_localidadEntrega.AutoSize = true;
            lbl_localidadEntrega.Location = new Point(28, 239);
            lbl_localidadEntrega.Name = "lbl_localidadEntrega";
            lbl_localidadEntrega.Size = new Size(53, 13);
            lbl_localidadEntrega.TabIndex = 8;
            lbl_localidadEntrega.Text = "Localidad";
            // 
            // txt_localidadEntrega
            // 
            txt_localidadEntrega.Location = new Point(149, 232);
            txt_localidadEntrega.MaxLength = 200;
            txt_localidadEntrega.Name = "txt_localidadEntrega";
            txt_localidadEntrega.Size = new Size(163, 20);
            txt_localidadEntrega.TabIndex = 17;
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(396, 39);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(349, 354);
            txt_notas.TabIndex = 19;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(393, 17);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 52;
            lbl_notas.Text = "Notas";
            // 
            // chk_esInscripto
            // 
            chk_esInscripto.AutoSize = true;
            chk_esInscripto.Location = new Point(17, 422);
            chk_esInscripto.Name = "chk_esInscripto";
            chk_esInscripto.Size = new Size(130, 17);
            chk_esInscripto.TabIndex = 20;
            chk_esInscripto.Text = "Responsable inscripto";
            chk_esInscripto.UseVisualStyleBackColor = true;
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(208, 422);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(107, 17);
            chk_activo.TabIndex = 21;
            chk_activo.Text = "Proveedor activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(21, 486);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 22;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(435, 530);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 31;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(337, 530);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 30;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // add_proveedor
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(763, 596);
            Controls.Add(TabControl1);
            Controls.Add(lbl_notas);
            Controls.Add(chk_esInscripto);
            Controls.Add(chk_activo);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_notas);
            Name = "add_proveedor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Insertar Proveedor";
            TabControl1.ResumeLayout(false);
            tab_general.ResumeLayout(false);
            tab_general.PerformLayout();
            tab_fiscal.ResumeLayout(false);
            tab_fiscal.PerformLayout();
            tab_entrega.ResumeLayout(false);
            tab_entrega.PerformLayout();
            FormClosed += new FormClosedEventHandler(add_proveedor_FormClosed);
            Load += new EventHandler(add_proveedor_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TabControl TabControl1;
        internal TabPage tab_general;
        internal ComboBox cmb_tipoDocumento;
        internal Label lbl_tipoDocumento;
        internal TextBox txt_email;
        internal Label lbl_email;
        internal TextBox txt_contacto;
        internal Label lbl_contacto;
        internal TextBox txt_telefono;
        internal Label lbl_tel;
        internal TextBox txt_taxNumber;
        internal Label lbl_taxNumber;
        internal TextBox txt_celular;
        internal Label lbl_celular;
        internal TextBox txt_razonSocial;
        internal Label lbl_razonSocial;
        internal TabPage tab_fiscal;
        internal Label lbl_cpFiscal;
        internal TextBox txt_cpFiscal;
        internal ComboBox cmb_paisFiscal;
        internal Label lbl_paisFiscal;
        internal ComboBox cmb_provinciaFiscal;
        internal TextBox txt_direccionFiscal;
        internal Label lbl_direccionFiscal;
        internal Label lbl_provinciaFiscal;
        internal Label lbl_localidadFiscal;
        internal TextBox txt_localidadFiscal;
        internal TabPage tab_entrega;
        internal Label lbl_cpEntrega;
        internal TextBox txt_cpEntrega;
        internal ComboBox cmb_paisEntrega;
        internal Label lbl_paisEntrega;
        internal ComboBox cmb_provinciaEntrega;
        internal TextBox txt_direccionEntrega;
        internal Label lbl_direccionEntrega;
        internal Label lbl_provinciaEntrega;
        internal Label lbl_localidadEntrega;
        internal TextBox txt_localidadEntrega;
        internal TextBox txt_notas;
        internal Label lbl_notas;
        internal CheckBox chk_esInscripto;
        internal CheckBox chk_activo;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal TextBox txt_vendedor;
        internal Label lbl_vendedor;
        internal ComboBox cmb_claseFiscal;
        internal Label lbl_claseFiscal;
    }
}


