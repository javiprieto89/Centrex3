using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_cuentaBancaria : Form
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
            components = new System.ComponentModel.Container();
            lbl_banco = new Label();
            lbl_nombreCuenta = new Label();
            lbl_moneda = new Label();
            chk_secuencia = new CheckBox();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            chk_activo = new CheckBox();
            tt_nCuenta = new ToolTip(components);
            cmb_banco = new ComboBox();
            cmb_banco.KeyPress += new KeyPressEventHandler(cmb_banco_KeyPress);
            cmb_moneda = new ComboBox();
            cmb_moneda.KeyPress += new KeyPressEventHandler(cmb_moneda_KeyPress);
            txt_cuentaBancaria = new TextBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            psearch_banco = new PictureBox();
            psearch_banco.Click += new EventHandler(psearch_banco_Click);
            ((System.ComponentModel.ISupportInitialize)psearch_banco).BeginInit();
            SuspendLayout();
            // 
            // lbl_banco
            // 
            lbl_banco.AutoSize = true;
            lbl_banco.Location = new Point(22, 40);
            lbl_banco.Name = "lbl_banco";
            lbl_banco.Size = new Size(38, 13);
            lbl_banco.TabIndex = 0;
            lbl_banco.Text = "Banco";
            // 
            // lbl_nombreCuenta
            // 
            lbl_nombreCuenta.AutoSize = true;
            lbl_nombreCuenta.Location = new Point(22, 83);
            lbl_nombreCuenta.Name = "lbl_nombreCuenta";
            lbl_nombreCuenta.Size = new Size(63, 13);
            lbl_nombreCuenta.TabIndex = 1;
            lbl_nombreCuenta.Text = "Descripción";
            tt_nCuenta.SetToolTip(lbl_nombreCuenta, "Número de cuenta");
            // 
            // lbl_moneda
            // 
            lbl_moneda.AutoSize = true;
            lbl_moneda.Location = new Point(22, 134);
            lbl_moneda.Name = "lbl_moneda";
            lbl_moneda.Size = new Size(46, 13);
            lbl_moneda.TabIndex = 2;
            lbl_moneda.Text = "Moneda";
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(25, 274);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 4;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(143, 337);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Checked = true;
            chk_activo.CheckState = CheckState.Checked;
            chk_activo.Location = new Point(25, 201);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(136, 17);
            chk_activo.TabIndex = 3;
            chk_activo.Text = "Cuenta bancaria activa";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // cmb_banco
            // 
            cmb_banco.FormattingEnabled = true;
            cmb_banco.Location = new Point(104, 32);
            cmb_banco.Name = "cmb_banco";
            cmb_banco.Size = new Size(352, 21);
            cmb_banco.TabIndex = 0;
            // 
            // cmb_moneda
            // 
            cmb_moneda.FormattingEnabled = true;
            cmb_moneda.Location = new Point(104, 126);
            cmb_moneda.Name = "cmb_moneda";
            cmb_moneda.Size = new Size(352, 21);
            cmb_moneda.TabIndex = 2;
            // 
            // txt_cuentaBancaria
            // 
            txt_cuentaBancaria.Location = new Point(104, 80);
            txt_cuentaBancaria.Name = "txt_cuentaBancaria";
            txt_cuentaBancaria.Size = new Size(352, 20);
            txt_cuentaBancaria.TabIndex = 1;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(261, 337);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 24;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // psearch_banco
            // 
            psearch_banco.Image = My.Resources.Resources.iconoLupa;
            psearch_banco.Location = new Point(471, 31);
            psearch_banco.Name = "psearch_banco";
            psearch_banco.Size = new Size(22, 22);
            psearch_banco.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_banco.TabIndex = 27;
            psearch_banco.TabStop = false;
            // 
            // add_cuentaBancaria
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(502, 393);
            Controls.Add(psearch_banco);
            Controls.Add(cmd_exit);
            Controls.Add(txt_cuentaBancaria);
            Controls.Add(cmb_moneda);
            Controls.Add(cmb_banco);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_ok);
            Controls.Add(chk_activo);
            Controls.Add(lbl_moneda);
            Controls.Add(lbl_nombreCuenta);
            Controls.Add(lbl_banco);
            Name = "add_cuentaBancaria";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar cuenta bancaria";
            ((System.ComponentModel.ISupportInitialize)psearch_banco).EndInit();
            Load += new EventHandler(add_cuentaBancaria_Load);
            FormClosed += new FormClosedEventHandler(add_cuentaBancaria_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_banco;
        internal Label lbl_nombreCuenta;
        internal Label lbl_moneda;
        internal CheckBox chk_secuencia;
        internal Button cmd_ok;
        internal CheckBox chk_activo;
        internal ToolTip tt_nCuenta;
        internal ComboBox cmb_banco;
        internal ComboBox cmb_moneda;
        internal TextBox txt_cuentaBancaria;
        internal Button cmd_exit;
        internal PictureBox psearch_banco;
    }
}


