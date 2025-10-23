using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_banco : Form
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
            lbl_banco = new Label();
            txt_nombre = new TextBox();
            cmb_pais = new ComboBox();
            cmb_pais.KeyPress += new KeyPressEventHandler(cmb_pais_KeyPress);
            chk_activo = new CheckBox();
            chk_secuencia = new CheckBox();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_pais = new Label();
            txt_bancoN = new TextBox();
            lbl_bancoN = new Label();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            SuspendLayout();
            // 
            // lbl_banco
            // 
            lbl_banco.AutoSize = true;
            lbl_banco.Location = new Point(23, 30);
            lbl_banco.Name = "lbl_banco";
            lbl_banco.Size = new Size(44, 13);
            lbl_banco.TabIndex = 0;
            lbl_banco.Text = "Nombre";
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(101, 23);
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(261, 20);
            txt_nombre.TabIndex = 0;
            // 
            // cmb_pais
            // 
            cmb_pais.FormattingEnabled = true;
            cmb_pais.Location = new Point(101, 73);
            cmb_pais.Name = "cmb_pais";
            cmb_pais.Size = new Size(261, 21);
            cmb_pais.TabIndex = 1;
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Checked = true;
            chk_activo.CheckState = CheckState.Checked;
            chk_activo.Location = new Point(26, 186);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(89, 17);
            chk_activo.TabIndex = 3;
            chk_activo.Text = "Banco activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(26, 244);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 4;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(104, 284);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_pais
            // 
            lbl_pais.AutoSize = true;
            lbl_pais.Location = new Point(23, 81);
            lbl_pais.Name = "lbl_pais";
            lbl_pais.Size = new Size(29, 13);
            lbl_pais.TabIndex = 25;
            lbl_pais.Text = "País";
            // 
            // txt_bancoN
            // 
            txt_bancoN.Location = new Point(101, 126);
            txt_bancoN.Name = "txt_bancoN";
            txt_bancoN.Size = new Size(261, 20);
            txt_bancoN.TabIndex = 2;
            // 
            // lbl_bancoN
            // 
            lbl_bancoN.AutoSize = true;
            lbl_bancoN.Location = new Point(23, 133);
            lbl_bancoN.Name = "lbl_bancoN";
            lbl_bancoN.Size = new Size(53, 13);
            lbl_bancoN.TabIndex = 26;
            lbl_bancoN.Text = "Banco Nº";
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(206, 284);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 27;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // add_banco
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 331);
            Controls.Add(cmd_exit);
            Controls.Add(txt_bancoN);
            Controls.Add(lbl_bancoN);
            Controls.Add(lbl_pais);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_ok);
            Controls.Add(chk_activo);
            Controls.Add(cmb_pais);
            Controls.Add(txt_nombre);
            Controls.Add(lbl_banco);
            Name = "add_banco";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar banco";
            Load += new EventHandler(add_banco_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_banco;
        internal TextBox txt_nombre;
        internal ComboBox cmb_pais;
        internal CheckBox chk_activo;
        internal CheckBox chk_secuencia;
        internal Button cmd_ok;
        internal Label lbl_pais;
        internal TextBox txt_bancoN;
        internal Label lbl_bancoN;
        internal Button cmd_exit;
    }
}


