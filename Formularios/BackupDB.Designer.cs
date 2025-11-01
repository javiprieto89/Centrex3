using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class BackupDB : Form
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
            lbl_info = new Label();
            ProgressBar1 = new ProgressBar();
            Timer1 = new Timer(components);
            Timer1.Tick += new EventHandler(Timer1_Tick);
            SuspendLayout();
            // 
            // lbl_info
            // 
            lbl_info.AutoSize = true;
            lbl_info.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_info.Location = new Point(50, 69);
            lbl_info.Name = "lbl_info";
            lbl_info.Size = new Size(583, 20);
            lbl_info.TabIndex = 0;
            lbl_info.Text = "Aguarde por favor mientras se efecuta un backup de la base de datos...";
            // 
            // ProgressBar1
            // 
            ProgressBar1.Location = new Point(128, 150);
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(426, 23);
            ProgressBar1.TabIndex = 21;
            // 
            // Timer1
            // 
            // 
            // BackupDB
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 245);
            ControlBox = false;
            Controls.Add(ProgressBar1);
            Controls.Add(lbl_info);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BackupDB";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Realizando backup...";
            Load += new EventHandler(BackupDB_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_info;
        internal ProgressBar ProgressBar1;
        internal Timer Timer1;
    }
}


