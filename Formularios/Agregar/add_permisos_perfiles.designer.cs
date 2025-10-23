using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_permisos_perfiles : Form
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
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            pic_searchPerfil = new PictureBox();
            cmb_perfiles = new ComboBox();
            cmb_perfiles.KeyPress += new KeyPressEventHandler(cmb_perfiles_KeyPress);
            lbl_perfiles = new Label();
            pic_searchPermiso = new PictureBox();
            cmb_permisos = new ComboBox();
            cmb_permisos.KeyPress += new KeyPressEventHandler(cmb_permisos_KeyPress);
            lbl_permisos = new Label();
            ((System.ComponentModel.ISupportInitialize)pic_searchPerfil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchPermiso).BeginInit();
            SuspendLayout();
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(21, 127);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 27;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(224, 181);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 29;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(126, 181);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 28;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // pic_searchPerfil
            // 
            pic_searchPerfil.Image = My.Resources.Resources.iconoLupa;
            pic_searchPerfil.Location = new Point(384, 69);
            pic_searchPerfil.Name = "pic_searchPerfil";
            pic_searchPerfil.Size = new Size(22, 22);
            pic_searchPerfil.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchPerfil.TabIndex = 35;
            pic_searchPerfil.TabStop = false;
            // 
            // cmb_perfiles
            // 
            cmb_perfiles.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_perfiles.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_perfiles.FormattingEnabled = true;
            cmb_perfiles.Location = new Point(119, 70);
            cmb_perfiles.Name = "cmb_perfiles";
            cmb_perfiles.Size = new Size(259, 21);
            cmb_perfiles.TabIndex = 33;
            // 
            // lbl_perfiles
            // 
            lbl_perfiles.AutoSize = true;
            lbl_perfiles.Location = new Point(18, 70);
            lbl_perfiles.Name = "lbl_perfiles";
            lbl_perfiles.Size = new Size(41, 13);
            lbl_perfiles.TabIndex = 34;
            lbl_perfiles.Text = "Perfiles";
            // 
            // pic_searchPermiso
            // 
            pic_searchPermiso.Image = My.Resources.Resources.iconoLupa;
            pic_searchPermiso.Location = new Point(384, 23);
            pic_searchPermiso.Name = "pic_searchPermiso";
            pic_searchPermiso.Size = new Size(22, 22);
            pic_searchPermiso.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchPermiso.TabIndex = 38;
            pic_searchPermiso.TabStop = false;
            // 
            // cmb_permisos
            // 
            cmb_permisos.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_permisos.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_permisos.FormattingEnabled = true;
            cmb_permisos.Location = new Point(119, 24);
            cmb_permisos.Name = "cmb_permisos";
            cmb_permisos.Size = new Size(259, 21);
            cmb_permisos.TabIndex = 36;
            // 
            // lbl_permisos
            // 
            lbl_permisos.AutoSize = true;
            lbl_permisos.Location = new Point(18, 24);
            lbl_permisos.Name = "lbl_permisos";
            lbl_permisos.Size = new Size(49, 13);
            lbl_permisos.TabIndex = 37;
            lbl_permisos.Text = "Permisos";
            // 
            // add_permisos_perfiles
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 226);
            Controls.Add(pic_searchPermiso);
            Controls.Add(cmb_permisos);
            Controls.Add(lbl_permisos);
            Controls.Add(pic_searchPerfil);
            Controls.Add(cmb_perfiles);
            Controls.Add(lbl_perfiles);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Name = "add_permisos_perfiles";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar relación perfil/permisos";
            ((System.ComponentModel.ISupportInitialize)pic_searchPerfil).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchPermiso).EndInit();
            Load += new EventHandler(add_permisos_perfiles_Load);
            FormClosed += new FormClosedEventHandler(add_permisos_perfiles_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal PictureBox pic_searchPerfil;
        internal ComboBox cmb_perfiles;
        internal Label lbl_perfiles;
        internal PictureBox pic_searchPermiso;
        internal ComboBox cmb_permisos;
        internal Label lbl_permisos;
    }
}
