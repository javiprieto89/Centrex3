using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class login : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            lbl_usuario = new Label();
            txt_usuario = new TextBox();
            cmd_exit = new Button();
            cmd_start = new Button();
            txt_password = new TextBox();
            lbl_password = new Label();
            PictureBox1 = new PictureBox();
            pic_showPassword = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_showPassword).BeginInit();
            SuspendLayout();
            // 
            // lbl_usuario
            // 
            lbl_usuario.AutoSize = true;
            lbl_usuario.FlatStyle = FlatStyle.Flat;
            lbl_usuario.Location = new Point(19, 183);
            lbl_usuario.Margin = new Padding(4, 0, 4, 0);
            lbl_usuario.Name = "lbl_usuario";
            lbl_usuario.Size = new Size(59, 20);
            lbl_usuario.TabIndex = 0;
            lbl_usuario.Text = "Usuario";
            // 
            // txt_usuario
            // 
            txt_usuario.Location = new Point(124, 178);
            txt_usuario.Margin = new Padding(4, 5, 4, 5);
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(313, 27);
            txt_usuario.TabIndex = 0;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(147, 422);
            cmd_exit.Margin = new Padding(4, 5, 4, 5);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(232, 65);
            cmd_exit.TabIndex = 3;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            cmd_exit.Click += cmd_exit_Click;
            // 
            // cmd_start
            // 
            cmd_start.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmd_start.Location = new Point(147, 332);
            cmd_start.Margin = new Padding(4, 5, 4, 5);
            cmd_start.Name = "cmd_start";
            cmd_start.Size = new Size(232, 65);
            cmd_start.TabIndex = 2;
            cmd_start.Text = "Iniciar sesión";
            cmd_start.UseVisualStyleBackColor = true;
            cmd_start.Click += cmd_start_Click;
            // 
            // txt_password
            // 
            txt_password.Location = new Point(124, 266);
            txt_password.Margin = new Padding(4, 5, 4, 5);
            txt_password.Name = "txt_password";
            txt_password.Size = new Size(313, 27);
            txt_password.TabIndex = 1;
            txt_password.UseSystemPasswordChar = true;
            // 
            // lbl_password
            // 
            lbl_password.AutoSize = true;
            lbl_password.Location = new Point(19, 271);
            lbl_password.Margin = new Padding(4, 0, 4, 0);
            lbl_password.Name = "lbl_password";
            lbl_password.Size = new Size(70, 20);
            lbl_password.TabIndex = 28;
            lbl_password.Text = "Password";
            // 
            // PictureBox1
            // 
            PictureBox1.Location = new Point(43, 18);
            PictureBox1.Margin = new Padding(4, 5, 4, 5);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(441, 131);
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 38;
            PictureBox1.TabStop = false;
            // 
            // pic_showPassword
            // 
            pic_showPassword.Location = new Point(447, 266);
            pic_showPassword.Margin = new Padding(4, 5, 4, 5);
            pic_showPassword.Name = "pic_showPassword";
            pic_showPassword.Size = new Size(22, 22);
            pic_showPassword.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_showPassword.TabIndex = 37;
            pic_showPassword.TabStop = false;
            pic_showPassword.Click += pic_showPassword_Click;
            pic_showPassword.MouseLeave += pic_showPassword_MouseLeave;
            // 
            // login
            // 
            AcceptButton = cmd_start;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            CancelButton = cmd_exit;
            ClientSize = new Size(527, 509);
            Controls.Add(PictureBox1);
            Controls.Add(pic_showPassword);
            Controls.Add(txt_password);
            Controls.Add(lbl_password);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_start);
            Controls.Add(txt_usuario);
            Controls.Add(lbl_usuario);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar sesión";
            FormClosed += login_FormClosed;
            Load += login_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_showPassword).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_usuario;
        internal TextBox txt_usuario;
        internal Button cmd_exit;
        internal Button cmd_start;
        internal TextBox txt_password;
        internal Label lbl_password;
        internal PictureBox pic_showPassword;
        internal PictureBox PictureBox1;
    }
}
