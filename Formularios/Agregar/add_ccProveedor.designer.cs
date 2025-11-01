using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_ccProveedor : Form
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
            chk_activo = new CheckBox();
            txt_nombre = new TextBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_cc = new Label();
            txt_saldo = new TextBox();
            lbl_saldo = new Label();
            cmb_proveedor = new ComboBox();
            pic_searchProveedor = new PictureBox();
            pic_searchProveedor.Click += new EventHandler(pic_searchProveedor_Click);
            lbl_proveedor = new Label();
            cmb_moneda = new ComboBox();
            lbl_moneda = new Label();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).BeginInit();
            SuspendLayout();
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(23, 206);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(72, 17);
            chk_activo.TabIndex = 3;
            chk_activo.Text = "CC activa";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(76, 112);
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(259, 20);
            txt_nombre.TabIndex = 1;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(23, 238);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 4;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(191, 274);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 6;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(93, 274);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_cc
            // 
            lbl_cc.AutoSize = true;
            lbl_cc.Location = new Point(20, 117);
            lbl_cc.Name = "lbl_cc";
            lbl_cc.Size = new Size(44, 13);
            lbl_cc.TabIndex = 6;
            lbl_cc.Text = "Nombre";
            // 
            // txt_saldo
            // 
            txt_saldo.Location = new Point(76, 150);
            txt_saldo.Name = "txt_saldo";
            txt_saldo.Size = new Size(259, 20);
            txt_saldo.TabIndex = 2;
            // 
            // lbl_saldo
            // 
            lbl_saldo.AutoSize = true;
            lbl_saldo.Location = new Point(20, 158);
            lbl_saldo.Name = "lbl_saldo";
            lbl_saldo.Size = new Size(34, 13);
            lbl_saldo.TabIndex = 12;
            lbl_saldo.Text = "Saldo";
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(76, 22);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(259, 21);
            cmb_proveedor.TabIndex = 0;
            // 
            // pic_searchProveedor
            // 
            pic_searchProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchProveedor.Location = new Point(353, 22);
            pic_searchProveedor.Name = "pic_searchProveedor";
            pic_searchProveedor.Size = new Size(22, 22);
            pic_searchProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchProveedor.TabIndex = 637;
            pic_searchProveedor.TabStop = false;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(20, 25);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 636;
            lbl_proveedor.Text = "Proveedor";
            // 
            // cmb_moneda
            // 
            cmb_moneda.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_moneda.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_moneda.FormattingEnabled = true;
            cmb_moneda.Location = new Point(76, 66);
            cmb_moneda.Name = "cmb_moneda";
            cmb_moneda.Size = new Size(259, 21);
            cmb_moneda.TabIndex = 638;
            // 
            // lbl_moneda
            // 
            lbl_moneda.AutoSize = true;
            lbl_moneda.Location = new Point(20, 69);
            lbl_moneda.Name = "lbl_moneda";
            lbl_moneda.Size = new Size(46, 13);
            lbl_moneda.TabIndex = 639;
            lbl_moneda.Text = "Moneda";
            // 
            // add_ccProveedor
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 320);
            Controls.Add(cmb_moneda);
            Controls.Add(lbl_moneda);
            Controls.Add(cmb_proveedor);
            Controls.Add(pic_searchProveedor);
            Controls.Add(lbl_proveedor);
            Controls.Add(txt_saldo);
            Controls.Add(lbl_saldo);
            Controls.Add(chk_activo);
            Controls.Add(txt_nombre);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_cc);
            Name = "add_ccProveedor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar cuenta corriente a cliente";
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).EndInit();
            Load += new EventHandler(add_ccProveedor_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal CheckBox chk_activo;
        internal TextBox txt_nombre;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_cc;
        internal TextBox txt_saldo;
        internal Label lbl_saldo;
        internal ComboBox cmb_proveedor;
        internal PictureBox pic_searchProveedor;
        internal Label lbl_proveedor;
        internal ComboBox cmb_moneda;
        internal Label lbl_moneda;
    }
}
