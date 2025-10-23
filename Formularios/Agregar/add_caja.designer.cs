using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_caja : Form
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
            components = new System.ComponentModel.Container();
            lbl_caja = new Label();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            txt_caja = new TextBox();
            chk_activo = new CheckBox();
            chk_cc = new CheckBox();
            ToolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // lbl_caja
            // 
            lbl_caja.AutoSize = true;
            lbl_caja.Location = new Point(22, 28);
            lbl_caja.Name = "lbl_caja";
            lbl_caja.Size = new Size(28, 13);
            lbl_caja.TabIndex = 0;
            lbl_caja.Text = "Caja";
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(27, 136);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 2;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(191, 190);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 4;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(93, 190);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 3;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // txt_caja
            // 
            txt_caja.Location = new Point(78, 21);
            txt_caja.Name = "txt_caja";
            txt_caja.Size = new Size(259, 20);
            txt_caja.TabIndex = 0;
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(25, 103);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(79, 17);
            chk_activo.TabIndex = 1;
            chk_activo.Text = "Caja activa";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // chk_cc
            // 
            chk_cc.AutoSize = true;
            chk_cc.Location = new Point(25, 70);
            chk_cc.Name = "chk_cc";
            chk_cc.Size = new Size(118, 17);
            chk_cc.TabIndex = 5;
            chk_cc.Text = "Es cuenta corriente";
            ToolTip1.SetToolTip(chk_cc, "Si tilda esta casilla, al hacer el cierre de caja no se cerrarán los pedidos que " + "tengan esta caja seleccionada");
            chk_cc.UseVisualStyleBackColor = true;
            // 
            // add_caja
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(358, 236);
            Controls.Add(chk_cc);
            Controls.Add(chk_activo);
            Controls.Add(txt_caja);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_caja);
            Name = "add_caja";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Insertar caja";
            FormClosed += new FormClosedEventHandler(Add_caja_FormClosed);
            Load += new EventHandler(Add_caja_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_caja;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal TextBox txt_caja;
        internal CheckBox chk_activo;
        internal CheckBox chk_cc;
        internal ToolTip ToolTip1;
    }
}
