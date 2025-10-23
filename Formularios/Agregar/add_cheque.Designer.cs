using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_cheque : Form
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
            lbl_fechaEmision = new Label();
            lbl_fechaCobro = new Label();
            lbl_estado = new Label();
            lbl_importe = new Label();
            lbl_ncheque2 = new Label();
            lbl_ncheque = new Label();
            lbl_banco = new Label();
            lbl_cliente = new Label();
            lbl_fechaSalida = new Label();
            lbl_fechaDeposito = new Label();
            rb_recibido = new RadioButton();
            rb_recibido.CheckedChanged += new EventHandler(rb_recibido_CheckedChanged);
            rb_emitido = new RadioButton();
            rb_emitido.CheckedChanged += new EventHandler(rb_emitido_CheckedChanged);
            chk_depositado = new CheckBox();
            chk_depositado.CheckedChanged += new EventHandler(chk_depositado_CheckedChanged);
            lbl_cuentaBancaria = new Label();
            cmb_cuentaBancaria = new ComboBox();
            cmb_cuentaBancaria.KeyPress += new KeyPressEventHandler(cmb_cuentaBancaria_KeyPress);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            txt_nCheque = new TextBox();
            txt_nCheque.KeyPress += new KeyPressEventHandler(txt_nCheque_KeyPress);
            txt_nCheque2 = new TextBox();
            txt_importe = new TextBox();
            txt_importe.KeyPress += new KeyPressEventHandler(txt_importe_KeyPress);
            cmb_cliente = new ComboBox();
            cmb_cliente.KeyPress += new KeyPressEventHandler(cmb_cliente_KeyPress);
            cmb_banco = new ComboBox();
            cmb_banco.KeyPress += new KeyPressEventHandler(cmb_banco_KeyPress);
            cmb_estadoch = new ComboBox();
            cmb_estadoch.KeyPress += new KeyPressEventHandler(cmb_estadoch_KeyPress);
            dtp_fEmision = new DateTimePicker();
            dtp_fCobro = new DateTimePicker();
            dtp_fSalida = new DateTimePicker();
            dtp_fDeposito = new DateTimePicker();
            chk_fCobro = new CheckBox();
            chk_fSalida = new CheckBox();
            chk_fSalida.CheckedChanged += new EventHandler(Ch_fSalida_CheckedChanged);
            chk_fDeposito = new CheckBox();
            chk_fDeposito.CheckedChanged += new EventHandler(Ch_fDeposito_CheckedChanged);
            cmb_proveedor = new ComboBox();
            cmb_proveedor.KeyPress += new KeyPressEventHandler(cmb_proveedor_KeyPress);
            lbl_proveedor = new Label();
            pic_searchCliente = new PictureBox();
            pic_searchCliente.Click += new EventHandler(pic_searchCliente_Click);
            pic_searchProveedor = new PictureBox();
            pic_searchProveedor.Click += new EventHandler(pic_searchProveedor_Click);
            pic_searchBanco = new PictureBox();
            pic_searchBanco.Click += new EventHandler(pic_searchBanco_Click);
            dtp_fIngreso = new DateTimePicker();
            lbl_fechaIngreso = new Label();
            chk_eCheck = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pic_searchCliente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchBanco).BeginInit();
            SuspendLayout();
            // 
            // lbl_fechaEmision
            // 
            lbl_fechaEmision.AutoSize = true;
            lbl_fechaEmision.Location = new Point(23, 74);
            lbl_fechaEmision.Name = "lbl_fechaEmision";
            lbl_fechaEmision.Size = new Size(90, 13);
            lbl_fechaEmision.TabIndex = 0;
            lbl_fechaEmision.Text = "Fecha de emisión";
            // 
            // lbl_fechaCobro
            // 
            lbl_fechaCobro.AutoSize = true;
            lbl_fechaCobro.Location = new Point(23, 114);
            lbl_fechaCobro.Name = "lbl_fechaCobro";
            lbl_fechaCobro.Size = new Size(82, 13);
            lbl_fechaCobro.TabIndex = 1;
            lbl_fechaCobro.Text = "Fecha de cobro";
            // 
            // lbl_estado
            // 
            lbl_estado.AutoSize = true;
            lbl_estado.Location = new Point(451, 25);
            lbl_estado.Name = "lbl_estado";
            lbl_estado.Size = new Size(96, 13);
            lbl_estado.TabIndex = 2;
            lbl_estado.Text = "Estado del cheque";
            // 
            // lbl_importe
            // 
            lbl_importe.AutoSize = true;
            lbl_importe.Location = new Point(23, 438);
            lbl_importe.Name = "lbl_importe";
            lbl_importe.Size = new Size(42, 13);
            lbl_importe.TabIndex = 3;
            lbl_importe.Text = "Importe";
            // 
            // lbl_ncheque2
            // 
            lbl_ncheque2.AutoSize = true;
            lbl_ncheque2.Location = new Point(23, 393);
            lbl_ncheque2.Name = "lbl_ncheque2";
            lbl_ncheque2.Size = new Size(94, 13);
            lbl_ncheque2.TabIndex = 4;
            lbl_ncheque2.Text = "2do Nº de cheque";
            // 
            // lbl_ncheque
            // 
            lbl_ncheque.AutoSize = true;
            lbl_ncheque.Location = new Point(23, 348);
            lbl_ncheque.Name = "lbl_ncheque";
            lbl_ncheque.Size = new Size(73, 13);
            lbl_ncheque.TabIndex = 5;
            lbl_ncheque.Text = "Nº de cheque";
            // 
            // lbl_banco
            // 
            lbl_banco.AutoSize = true;
            lbl_banco.Location = new Point(23, 303);
            lbl_banco.Name = "lbl_banco";
            lbl_banco.Size = new Size(71, 13);
            lbl_banco.TabIndex = 6;
            lbl_banco.Text = "Banco emisor";
            // 
            // lbl_cliente
            // 
            lbl_cliente.AutoSize = true;
            lbl_cliente.Location = new Point(23, 190);
            lbl_cliente.Name = "lbl_cliente";
            lbl_cliente.Size = new Size(39, 13);
            lbl_cliente.TabIndex = 7;
            lbl_cliente.Text = "Cliente";
            // 
            // lbl_fechaSalida
            // 
            lbl_fechaSalida.AutoSize = true;
            lbl_fechaSalida.Location = new Point(485, 111);
            lbl_fechaSalida.Name = "lbl_fechaSalida";
            lbl_fechaSalida.Size = new Size(82, 13);
            lbl_fechaSalida.TabIndex = 8;
            lbl_fechaSalida.Text = "Fecha de salida";
            // 
            // lbl_fechaDeposito
            // 
            lbl_fechaDeposito.AutoSize = true;
            lbl_fechaDeposito.Location = new Point(485, 158);
            lbl_fechaDeposito.Name = "lbl_fechaDeposito";
            lbl_fechaDeposito.Size = new Size(95, 13);
            lbl_fechaDeposito.TabIndex = 9;
            lbl_fechaDeposito.Text = "Fecha de depósito";
            // 
            // rb_recibido
            // 
            rb_recibido.AutoSize = true;
            rb_recibido.Location = new Point(23, 154);
            rb_recibido.Name = "rb_recibido";
            rb_recibido.Size = new Size(102, 17);
            rb_recibido.TabIndex = 10;
            rb_recibido.TabStop = true;
            rb_recibido.Text = "Cheque recibido";
            rb_recibido.UseVisualStyleBackColor = true;
            // 
            // rb_emitido
            // 
            rb_emitido.AutoSize = true;
            rb_emitido.Location = new Point(23, 221);
            rb_emitido.Name = "rb_emitido";
            rb_emitido.Size = new Size(98, 17);
            rb_emitido.TabIndex = 11;
            rb_emitido.TabStop = true;
            rb_emitido.Text = "Cheque emitido";
            rb_emitido.UseVisualStyleBackColor = true;
            // 
            // chk_depositado
            // 
            chk_depositado.AutoSize = true;
            chk_depositado.Enabled = false;
            chk_depositado.Location = new Point(457, 205);
            chk_depositado.Name = "chk_depositado";
            chk_depositado.Size = new Size(180, 17);
            chk_depositado.TabIndex = 12;
            chk_depositado.Text = "El cheque ya ha sido depositado";
            chk_depositado.UseVisualStyleBackColor = true;
            // 
            // lbl_cuentaBancaria
            // 
            lbl_cuentaBancaria.AutoSize = true;
            lbl_cuentaBancaria.Location = new Point(454, 251);
            lbl_cuentaBancaria.Name = "lbl_cuentaBancaria";
            lbl_cuentaBancaria.Size = new Size(189, 13);
            lbl_cuentaBancaria.TabIndex = 13;
            lbl_cuentaBancaria.Text = "Cuenta bancaria en la que se deposito";
            // 
            // cmb_cuentaBancaria
            // 
            cmb_cuentaBancaria.Enabled = false;
            cmb_cuentaBancaria.FormattingEnabled = true;
            cmb_cuentaBancaria.Location = new Point(454, 295);
            cmb_cuentaBancaria.Name = "cmb_cuentaBancaria";
            cmb_cuentaBancaria.Size = new Size(355, 21);
            cmb_cuentaBancaria.TabIndex = 14;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(134, 585);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 54;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(26, 543);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 52;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(234, 585);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 53;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // txt_nCheque
            // 
            txt_nCheque.Location = new Point(163, 341);
            txt_nCheque.MaxLength = 8;
            txt_nCheque.Name = "txt_nCheque";
            txt_nCheque.Size = new Size(224, 20);
            txt_nCheque.TabIndex = 55;
            // 
            // txt_nCheque2
            // 
            txt_nCheque2.Location = new Point(163, 386);
            txt_nCheque2.Name = "txt_nCheque2";
            txt_nCheque2.Size = new Size(224, 20);
            txt_nCheque2.TabIndex = 56;
            // 
            // txt_importe
            // 
            txt_importe.Location = new Point(163, 431);
            txt_importe.Name = "txt_importe";
            txt_importe.Size = new Size(224, 20);
            txt_importe.TabIndex = 57;
            // 
            // cmb_cliente
            // 
            cmb_cliente.FormattingEnabled = true;
            cmb_cliente.Location = new Point(163, 182);
            cmb_cliente.Name = "cmb_cliente";
            cmb_cliente.Size = new Size(224, 21);
            cmb_cliente.TabIndex = 58;
            // 
            // cmb_banco
            // 
            cmb_banco.FormattingEnabled = true;
            cmb_banco.Location = new Point(163, 295);
            cmb_banco.Name = "cmb_banco";
            cmb_banco.Size = new Size(224, 21);
            cmb_banco.TabIndex = 59;
            // 
            // cmb_estadoch
            // 
            cmb_estadoch.Enabled = false;
            cmb_estadoch.FormattingEnabled = true;
            cmb_estadoch.Location = new Point(585, 21);
            cmb_estadoch.Name = "cmb_estadoch";
            cmb_estadoch.Size = new Size(224, 21);
            cmb_estadoch.TabIndex = 60;
            // 
            // dtp_fEmision
            // 
            dtp_fEmision.Location = new Point(163, 68);
            dtp_fEmision.Name = "dtp_fEmision";
            dtp_fEmision.Size = new Size(224, 20);
            dtp_fEmision.TabIndex = 113;
            // 
            // dtp_fCobro
            // 
            dtp_fCobro.Location = new Point(163, 116);
            dtp_fCobro.Name = "dtp_fCobro";
            dtp_fCobro.Size = new Size(224, 20);
            dtp_fCobro.TabIndex = 114;
            // 
            // dtp_fSalida
            // 
            dtp_fSalida.Enabled = false;
            dtp_fSalida.Location = new Point(585, 105);
            dtp_fSalida.Name = "dtp_fSalida";
            dtp_fSalida.Size = new Size(224, 20);
            dtp_fSalida.TabIndex = 115;
            // 
            // dtp_fDeposito
            // 
            dtp_fDeposito.Enabled = false;
            dtp_fDeposito.Location = new Point(585, 152);
            dtp_fDeposito.Name = "dtp_fDeposito";
            dtp_fDeposito.Size = new Size(224, 20);
            dtp_fDeposito.TabIndex = 116;
            // 
            // chk_fCobro
            // 
            chk_fCobro.AutoSize = true;
            chk_fCobro.Enabled = false;
            chk_fCobro.Location = new Point(-6, 111);
            chk_fCobro.Name = "chk_fCobro";
            chk_fCobro.Size = new Size(15, 14);
            chk_fCobro.TabIndex = 117;
            chk_fCobro.UseVisualStyleBackColor = true;
            chk_fCobro.Visible = false;
            // 
            // chk_fSalida
            // 
            chk_fSalida.AutoSize = true;
            chk_fSalida.Enabled = false;
            chk_fSalida.Location = new Point(454, 113);
            chk_fSalida.Name = "chk_fSalida";
            chk_fSalida.Size = new Size(15, 14);
            chk_fSalida.TabIndex = 118;
            chk_fSalida.UseVisualStyleBackColor = true;
            // 
            // chk_fDeposito
            // 
            chk_fDeposito.AutoSize = true;
            chk_fDeposito.Enabled = false;
            chk_fDeposito.Location = new Point(454, 158);
            chk_fDeposito.Name = "chk_fDeposito";
            chk_fDeposito.Size = new Size(15, 14);
            chk_fDeposito.TabIndex = 119;
            chk_fDeposito.UseVisualStyleBackColor = true;
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(163, 248);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(224, 21);
            cmb_proveedor.TabIndex = 121;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(23, 256);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 120;
            lbl_proveedor.Text = "Proveedor";
            // 
            // pic_searchCliente
            // 
            pic_searchCliente.Image = My.Resources.Resources.iconoLupa;
            pic_searchCliente.Location = new Point(403, 181);
            pic_searchCliente.Name = "pic_searchCliente";
            pic_searchCliente.Size = new Size(22, 22);
            pic_searchCliente.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCliente.TabIndex = 122;
            pic_searchCliente.TabStop = false;
            // 
            // pic_searchProveedor
            // 
            pic_searchProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchProveedor.Location = new Point(403, 247);
            pic_searchProveedor.Name = "pic_searchProveedor";
            pic_searchProveedor.Size = new Size(22, 22);
            pic_searchProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchProveedor.TabIndex = 123;
            pic_searchProveedor.TabStop = false;
            // 
            // pic_searchBanco
            // 
            pic_searchBanco.Image = My.Resources.Resources.iconoLupa;
            pic_searchBanco.Location = new Point(403, 295);
            pic_searchBanco.Name = "pic_searchBanco";
            pic_searchBanco.Size = new Size(22, 22);
            pic_searchBanco.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchBanco.TabIndex = 124;
            pic_searchBanco.TabStop = false;
            // 
            // dtp_fIngreso
            // 
            dtp_fIngreso.Enabled = false;
            dtp_fIngreso.Location = new Point(163, 25);
            dtp_fIngreso.Name = "dtp_fIngreso";
            dtp_fIngreso.Size = new Size(224, 20);
            dtp_fIngreso.TabIndex = 126;
            // 
            // lbl_fechaIngreso
            // 
            lbl_fechaIngreso.AutoSize = true;
            lbl_fechaIngreso.Location = new Point(23, 31);
            lbl_fechaIngreso.Name = "lbl_fechaIngreso";
            lbl_fechaIngreso.Size = new Size(89, 13);
            lbl_fechaIngreso.TabIndex = 125;
            lbl_fechaIngreso.Text = "Fecha de ingreso";
            // 
            // chk_eCheck
            // 
            chk_eCheck.AutoSize = true;
            chk_eCheck.Location = new Point(26, 481);
            chk_eCheck.Name = "chk_eCheck";
            chk_eCheck.Size = new Size(118, 17);
            chk_eCheck.TabIndex = 127;
            chk_eCheck.Text = "Cheque electrónico";
            chk_eCheck.UseVisualStyleBackColor = true;
            // 
            // add_cheque
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 626);
            Controls.Add(chk_eCheck);
            Controls.Add(dtp_fIngreso);
            Controls.Add(lbl_fechaIngreso);
            Controls.Add(pic_searchBanco);
            Controls.Add(pic_searchProveedor);
            Controls.Add(pic_searchCliente);
            Controls.Add(cmb_proveedor);
            Controls.Add(lbl_proveedor);
            Controls.Add(chk_fDeposito);
            Controls.Add(chk_fSalida);
            Controls.Add(chk_fCobro);
            Controls.Add(dtp_fDeposito);
            Controls.Add(dtp_fSalida);
            Controls.Add(dtp_fCobro);
            Controls.Add(dtp_fEmision);
            Controls.Add(cmb_estadoch);
            Controls.Add(cmb_banco);
            Controls.Add(cmb_cliente);
            Controls.Add(txt_importe);
            Controls.Add(txt_nCheque2);
            Controls.Add(txt_nCheque);
            Controls.Add(cmd_ok);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmb_cuentaBancaria);
            Controls.Add(lbl_cuentaBancaria);
            Controls.Add(chk_depositado);
            Controls.Add(rb_emitido);
            Controls.Add(rb_recibido);
            Controls.Add(lbl_fechaDeposito);
            Controls.Add(lbl_fechaSalida);
            Controls.Add(lbl_cliente);
            Controls.Add(lbl_banco);
            Controls.Add(lbl_ncheque);
            Controls.Add(lbl_ncheque2);
            Controls.Add(lbl_importe);
            Controls.Add(lbl_estado);
            Controls.Add(lbl_fechaCobro);
            Controls.Add(lbl_fechaEmision);
            Name = "add_cheque";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar cheques";
            ((System.ComponentModel.ISupportInitialize)pic_searchCliente).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchBanco).EndInit();
            Load += new EventHandler(Add_cheque_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_fechaEmision;
        internal Label lbl_fechaCobro;
        internal Label lbl_estado;
        internal Label lbl_importe;
        internal Label lbl_ncheque2;
        internal Label lbl_ncheque;
        internal Label lbl_banco;
        internal Label lbl_cliente;
        internal Label lbl_fechaSalida;
        internal Label lbl_fechaDeposito;
        internal RadioButton rb_recibido;
        internal RadioButton rb_emitido;
        internal CheckBox chk_depositado;
        internal Label lbl_cuentaBancaria;
        internal ComboBox cmb_cuentaBancaria;
        internal Button cmd_ok;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal TextBox txt_nCheque;
        internal TextBox txt_nCheque2;
        internal TextBox txt_importe;
        internal ComboBox cmb_cliente;
        internal ComboBox cmb_banco;
        internal ComboBox cmb_estadoch;
        internal DateTimePicker dtp_fEmision;
        internal DateTimePicker dtp_fCobro;
        internal DateTimePicker dtp_fSalida;
        internal DateTimePicker dtp_fDeposito;
        internal CheckBox chk_fCobro;
        internal CheckBox chk_fSalida;
        internal CheckBox chk_fDeposito;
        internal ComboBox cmb_proveedor;
        internal Label lbl_proveedor;
        internal PictureBox pic_searchCliente;
        internal PictureBox pic_searchProveedor;
        internal PictureBox pic_searchBanco;
        internal DateTimePicker dtp_fIngreso;
        internal Label lbl_fechaIngreso;
        internal CheckBox chk_eCheck;
    }
}


