using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_transferencia : Form
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
            lbl_fecha = new Label();
            lbl_importe = new Label();
            lbl_cuentaBancaria = new Label();
            lbl_banco = new Label();
            cmb_cuentaBancaria = new ComboBox();
            cmb_cuentaBancaria.KeyPress += new KeyPressEventHandler(cmb_cuentaBancaria_KeyPress);
            psearch_banco = new PictureBox();
            psearch_banco.Click += new EventHandler(psearch_banco_Click);
            cmb_banco = new ComboBox();
            cmb_banco.SelectionChangeCommitted += new EventHandler(cmb_banco_SelectionChangeCommitted);
            cmb_banco.KeyPress += new KeyPressEventHandler(cmb_banco_KeyPress);
            psearch_cuentaBancaria = new PictureBox();
            psearch_cuentaBancaria.Click += new EventHandler(psearch_cuentaBancaria_Click);
            dtp_fecha = new DateTimePicker();
            txt_importe = new TextBox();
            txt_importe.KeyPress += new KeyPressEventHandler(txt_importe_KeyPress);
            lbl_notas = new Label();
            txt_notas = new TextBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            txt_nComprobante = new TextBox();
            lbl_nComprobante = new Label();
            ((System.ComponentModel.ISupportInitialize)psearch_banco).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_cuentaBancaria).BeginInit();
            SuspendLayout();
            // 
            // lbl_fecha
            // 
            lbl_fecha.AutoSize = true;
            lbl_fecha.Location = new Point(42, 29);
            lbl_fecha.Name = "lbl_fecha";
            lbl_fecha.Size = new Size(37, 13);
            lbl_fecha.TabIndex = 7;
            lbl_fecha.Text = "Fecha";
            // 
            // lbl_importe
            // 
            lbl_importe.AutoSize = true;
            lbl_importe.Location = new Point(42, 186);
            lbl_importe.Name = "lbl_importe";
            lbl_importe.Size = new Size(42, 13);
            lbl_importe.TabIndex = 1;
            lbl_importe.Text = "Importe";
            // 
            // lbl_cuentaBancaria
            // 
            lbl_cuentaBancaria.Location = new Point(42, 132);
            lbl_cuentaBancaria.Name = "lbl_cuentaBancaria";
            lbl_cuentaBancaria.Size = new Size(51, 33);
            lbl_cuentaBancaria.TabIndex = 2;
            lbl_cuentaBancaria.Text = "Cuenta bancaria";
            // 
            // lbl_banco
            // 
            lbl_banco.AutoSize = true;
            lbl_banco.Location = new Point(42, 89);
            lbl_banco.Name = "lbl_banco";
            lbl_banco.Size = new Size(38, 13);
            lbl_banco.TabIndex = 3;
            lbl_banco.Text = "Banco";
            // 
            // cmb_cuentaBancaria
            // 
            cmb_cuentaBancaria.FormattingEnabled = true;
            cmb_cuentaBancaria.Location = new Point(126, 132);
            cmb_cuentaBancaria.Name = "cmb_cuentaBancaria";
            cmb_cuentaBancaria.Size = new Size(352, 21);
            cmb_cuentaBancaria.TabIndex = 2;
            // 
            // psearch_banco
            // 
            psearch_banco.Image = My.Resources.Resources.iconoLupa;
            psearch_banco.Location = new Point(493, 80);
            psearch_banco.Name = "psearch_banco";
            psearch_banco.Size = new Size(22, 22);
            psearch_banco.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_banco.TabIndex = 29;
            psearch_banco.TabStop = false;
            // 
            // cmb_banco
            // 
            cmb_banco.FormattingEnabled = true;
            cmb_banco.Location = new Point(126, 81);
            cmb_banco.Name = "cmb_banco";
            cmb_banco.Size = new Size(352, 21);
            cmb_banco.TabIndex = 1;
            // 
            // psearch_cuentaBancaria
            // 
            psearch_cuentaBancaria.Image = My.Resources.Resources.iconoLupa;
            psearch_cuentaBancaria.Location = new Point(493, 132);
            psearch_cuentaBancaria.Name = "psearch_cuentaBancaria";
            psearch_cuentaBancaria.Size = new Size(22, 22);
            psearch_cuentaBancaria.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_cuentaBancaria.TabIndex = 30;
            psearch_cuentaBancaria.TabStop = false;
            // 
            // dtp_fecha
            // 
            dtp_fecha.Format = DateTimePickerFormat.Short;
            dtp_fecha.Location = new Point(126, 29);
            dtp_fecha.Name = "dtp_fecha";
            dtp_fecha.Size = new Size(104, 20);
            dtp_fecha.TabIndex = 0;
            // 
            // txt_importe
            // 
            txt_importe.Location = new Point(126, 183);
            txt_importe.Name = "txt_importe";
            txt_importe.Size = new Size(104, 20);
            txt_importe.TabIndex = 3;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(42, 281);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 8;
            lbl_notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(126, 283);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(352, 97);
            txt_notas.TabIndex = 1;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(282, 427);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 3;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(184, 427);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 2;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // txt_nComprobante
            // 
            txt_nComprobante.Location = new Point(126, 233);
            txt_nComprobante.Name = "txt_nComprobante";
            txt_nComprobante.Size = new Size(104, 20);
            txt_nComprobante.TabIndex = 0;
            // 
            // lbl_nComprobante
            // 
            lbl_nComprobante.Location = new Point(42, 233);
            lbl_nComprobante.Name = "lbl_nComprobante";
            lbl_nComprobante.Size = new Size(78, 30);
            lbl_nComprobante.TabIndex = 31;
            lbl_nComprobante.Text = "Número de comprobante";
            // 
            // add_transferencia
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(552, 482);
            Controls.Add(txt_nComprobante);
            Controls.Add(lbl_nComprobante);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_notas);
            Controls.Add(lbl_notas);
            Controls.Add(txt_importe);
            Controls.Add(dtp_fecha);
            Controls.Add(psearch_cuentaBancaria);
            Controls.Add(psearch_banco);
            Controls.Add(cmb_banco);
            Controls.Add(cmb_cuentaBancaria);
            Controls.Add(lbl_banco);
            Controls.Add(lbl_cuentaBancaria);
            Controls.Add(lbl_importe);
            Controls.Add(lbl_fecha);
            Name = "add_transferencia";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingresar transferencia";
            ((System.ComponentModel.ISupportInitialize)psearch_banco).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_cuentaBancaria).EndInit();
            Load += new EventHandler(add_transferencia_Load);
            FormClosed += new FormClosedEventHandler(add_transferencia_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_fecha;
        internal Label lbl_importe;
        internal Label lbl_cuentaBancaria;
        internal Label lbl_banco;
        internal ComboBox cmb_cuentaBancaria;
        internal PictureBox psearch_banco;
        internal ComboBox cmb_banco;
        internal PictureBox psearch_cuentaBancaria;
        internal DateTimePicker dtp_fecha;
        internal TextBox txt_importe;
        internal Label lbl_notas;
        internal TextBox txt_notas;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal TextBox txt_nComprobante;
        internal Label lbl_nComprobante;
    }
}


