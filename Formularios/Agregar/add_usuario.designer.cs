using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_usuario : Form
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
            txt_password = new TextBox();
            lbl_password = new Label();
            txt_nombre = new TextBox();
            lbl_nombre = new Label();
            chk_activo = new CheckBox();
            txt_usuario = new TextBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_usuario = new Label();
            lbl_perfil = new Label();
            cmb_perfil = new ComboBox();
            cmb_perfil.KeyPress += new KeyPressEventHandler(cmb_perfil_KeyPress);
            pic_showPassword = new PictureBox();
            pic_showPassword.Click += new EventHandler(pic_showPassword_Click);
            pic_showPassword.MouseLeave += new EventHandler(pic_showPassword_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)pic_showPassword).BeginInit();
            SuspendLayout();
            // 
            // txt_password
            // 
            txt_password.Location = new Point(133, 57);
            txt_password.Name = "txt_password";
            txt_password.Size = new Size(259, 20);
            txt_password.TabIndex = 24;
            txt_password.UseSystemPasswordChar = true;
            // 
            // lbl_password
            // 
            lbl_password.AutoSize = true;
            lbl_password.Location = new Point(18, 59);
            lbl_password.Name = "lbl_password";
            lbl_password.Size = new Size(53, 13);
            lbl_password.TabIndex = 32;
            lbl_password.Text = "Password";
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(133, 93);
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(259, 20);
            txt_nombre.TabIndex = 25;
            // 
            // lbl_nombre
            // 
            lbl_nombre.AutoSize = true;
            lbl_nombre.Location = new Point(18, 94);
            lbl_nombre.Name = "lbl_nombre";
            lbl_nombre.Size = new Size(44, 13);
            lbl_nombre.TabIndex = 31;
            lbl_nombre.Text = "Nombre";
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(21, 169);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(94, 17);
            chk_activo.TabIndex = 26;
            chk_activo.Text = "Usuario activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_usuario
            // 
            txt_usuario.Location = new Point(133, 21);
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(259, 20);
            txt_usuario.TabIndex = 23;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(21, 208);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 27;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(224, 242);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 29;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(126, 242);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 28;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_usuario
            // 
            lbl_usuario.AutoSize = true;
            lbl_usuario.Location = new Point(18, 24);
            lbl_usuario.Name = "lbl_usuario";
            lbl_usuario.Size = new Size(43, 13);
            lbl_usuario.TabIndex = 30;
            lbl_usuario.Text = "Usuario";
            // 
            // lbl_perfil
            // 
            lbl_perfil.AutoSize = true;
            lbl_perfil.Location = new Point(18, 129);
            lbl_perfil.Name = "lbl_perfil";
            lbl_perfil.Size = new Size(106, 13);
            lbl_perfil.TabIndex = 34;
            lbl_perfil.Text = "Perfil predeterminado";
            // 
            // cmb_perfil
            // 
            cmb_perfil.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_perfil.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_perfil.FormattingEnabled = true;
            cmb_perfil.Location = new Point(133, 129);
            cmb_perfil.Name = "cmb_perfil";
            cmb_perfil.Size = new Size(259, 21);
            cmb_perfil.TabIndex = 33;
            // 
            // pic_showPassword
            // 
            pic_showPassword.Image = My.Resources.Resources.iconoLupa;
            pic_showPassword.Location = new Point(398, 57);
            pic_showPassword.Name = "pic_showPassword";
            pic_showPassword.Size = new Size(22, 22);
            pic_showPassword.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_showPassword.TabIndex = 36;
            pic_showPassword.TabStop = false;
            // 
            // add_usuario
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 287);
            Controls.Add(pic_showPassword);
            Controls.Add(cmb_perfil);
            Controls.Add(lbl_perfil);
            Controls.Add(txt_password);
            Controls.Add(lbl_password);
            Controls.Add(txt_nombre);
            Controls.Add(lbl_nombre);
            Controls.Add(chk_activo);
            Controls.Add(txt_usuario);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_usuario);
            Name = "add_usuario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar usuario";
            ((System.ComponentModel.ISupportInitialize)pic_showPassword).EndInit();
            Load += new EventHandler(add_usuario_Load);
            FormClosed += new FormClosedEventHandler(add_usuario_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TextBox txt_password;
        internal Label lbl_password;
        internal TextBox txt_nombre;
        internal Label lbl_nombre;
        internal CheckBox chk_activo;
        internal TextBox txt_usuario;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_usuario;
        internal PictureBox pic_showPassword;
        internal Label lbl_perfil;
        internal ComboBox cmb_perfil;
    }
}
