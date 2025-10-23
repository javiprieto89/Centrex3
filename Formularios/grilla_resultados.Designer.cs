using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class grilla_resultados : Form
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
            cmb_consultas = new ComboBox();
            dg_view_resultados = new DataGridView();
            cmd_ejecutar = new Button();
            cmd_ejecutar.Click += new EventHandler(cmd_ejecutar_Click);
            lbl_consultas = new Label();
            ((System.ComponentModel.ISupportInitialize)dg_view_resultados).BeginInit();
            SuspendLayout();
            // 
            // cmb_consultas
            // 
            cmb_consultas.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_consultas.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_consultas.FormattingEnabled = true;
            cmb_consultas.Location = new Point(98, 21);
            cmb_consultas.Name = "cmb_consultas";
            cmb_consultas.Size = new Size(736, 21);
            cmb_consultas.TabIndex = 59;
            // 
            // dg_view_resultados
            // 
            dg_view_resultados.AllowUserToAddRows = false;
            dg_view_resultados.AllowUserToDeleteRows = false;
            dg_view_resultados.AllowUserToOrderColumns = true;
            dg_view_resultados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view_resultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_resultados.Location = new Point(24, 54);
            dg_view_resultados.MultiSelect = false;
            dg_view_resultados.Name = "dg_view_resultados";
            dg_view_resultados.ReadOnly = true;
            dg_view_resultados.RowHeadersVisible = false;
            dg_view_resultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view_resultados.Size = new Size(958, 598);
            dg_view_resultados.TabIndex = 58;
            // 
            // cmd_ejecutar
            // 
            cmd_ejecutar.Location = new Point(840, 21);
            cmd_ejecutar.Name = "cmd_ejecutar";
            cmd_ejecutar.Size = new Size(142, 21);
            cmd_ejecutar.TabIndex = 57;
            cmd_ejecutar.Text = "Ejecutar consulta";
            cmd_ejecutar.UseVisualStyleBackColor = true;
            // 
            // lbl_consultas
            // 
            lbl_consultas.AutoSize = true;
            lbl_consultas.Location = new Point(21, 24);
            lbl_consultas.Name = "lbl_consultas";
            lbl_consultas.Size = new Size(53, 13);
            lbl_consultas.TabIndex = 56;
            lbl_consultas.Text = "Consultas";
            // 
            // grilla_resultados
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1003, 672);
            Controls.Add(cmb_consultas);
            Controls.Add(dg_view_resultados);
            Controls.Add(cmd_ejecutar);
            Controls.Add(lbl_consultas);
            Name = "grilla_resultados";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Consultas personalizadas";
            ((System.ComponentModel.ISupportInitialize)dg_view_resultados).EndInit();
            Load += new EventHandler(grilla_resultados_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ComboBox cmb_consultas;
        internal DataGridView dg_view_resultados;
        internal Button cmd_ejecutar;
        internal Label lbl_consultas;
    }
}


