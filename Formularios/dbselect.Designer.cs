using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class dbselect : Form
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
            cmb_selectdb = new ComboBox();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            lbl_elija = new Label();
            SuspendLayout();
            // 
            // cmb_selectdb
            // 
            cmb_selectdb.FormattingEnabled = true;
            cmb_selectdb.Items.AddRange(new object[] { "Centrex", "CentrexTest" });
            cmb_selectdb.Location = new Point(42, 52);
            cmb_selectdb.Name = "cmb_selectdb";
            cmb_selectdb.Size = new Size(255, 24);
            cmb_selectdb.TabIndex = 0;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(42, 110);
            cmd_ok.Margin = new Padding(4);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(255, 52);
            cmd_ok.TabIndex = 1;
            cmd_ok.Text = "Iniciar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(42, 186);
            cmd_exit.Margin = new Padding(4);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(255, 52);
            cmd_exit.TabIndex = 2;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // lbl_elija
            // 
            lbl_elija.AutoSize = true;
            lbl_elija.Location = new Point(46, 15);
            lbl_elija.Name = "lbl_elija";
            lbl_elija.Size = new Size(156, 17);
            lbl_elija.TabIndex = 4;
            lbl_elija.Text = "Elija una base de datos";
            // 
            // dbselect
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(339, 253);
            Controls.Add(lbl_elija);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(cmb_selectdb);
            Name = "dbselect";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Centrex";
            Load += new EventHandler(dbselect_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ComboBox cmb_selectdb;
        internal Button cmd_ok;
        internal Button cmd_exit;
        internal Label lbl_elija;
    }
}


