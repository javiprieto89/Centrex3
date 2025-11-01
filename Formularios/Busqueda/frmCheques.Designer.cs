using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmCheques : Form
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
            lbl_cheques = new Label();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_exit = new Button();
            dg_view = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dg_view).BeginInit();
            SuspendLayout();
            // 
            // lbl_cheques
            // 
            lbl_cheques.AutoSize = true;
            lbl_cheques.Location = new Point(22, 27);
            lbl_cheques.Name = "lbl_cheques";
            lbl_cheques.Size = new Size(104, 13);
            lbl_cheques.TabIndex = 0;
            lbl_cheques.Text = "Cheques disponibles";
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(155, 451);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 53;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(255, 451);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 52;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // dg_view
            // 
            dg_view.AllowUserToAddRows = false;
            dg_view.AllowUserToDeleteRows = false;
            dg_view.AllowUserToOrderColumns = true;
            dg_view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view.Location = new Point(25, 61);
            dg_view.MultiSelect = false;
            dg_view.Name = "dg_view";
            dg_view.ReadOnly = true;
            dg_view.RowHeadersVisible = false;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.Size = new Size(457, 359);
            dg_view.TabIndex = 54;
            // 
            // frmCheques
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 501);
            Controls.Add(dg_view);
            Controls.Add(cmd_ok);
            Controls.Add(cmd_exit);
            Controls.Add(lbl_cheques);
            Name = "frmCheques";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccion de cheques";
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            Load += new EventHandler(frmCheques_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_cheques;
        internal Button cmd_ok;
        internal Button cmd_exit;
        internal DataGridView dg_view;
    }
}


