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
        private System.ComponentModel.IContainer components = null!;

        // NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        // Se puede modificar usando el Diseñador de Windows Forms.  
        // No lo modifique con el editor de código.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            cmb_consultas = new ComboBox();
            dg_view_resultados = new DataGridView();
            cmd_ejecutar = new Button();
            lbl_consultas = new Label();
            lbl_registros = new Label();
            ((System.ComponentModel.ISupportInitialize)dg_view_resultados).BeginInit();
            SuspendLayout();
            // 
            // cmb_consultas
            // 
            cmb_consultas.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_consultas.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_consultas.FormattingEnabled = true;
            cmb_consultas.Location = new Point(131, 32);
            cmb_consultas.Margin = new Padding(4, 5, 4, 5);
            cmb_consultas.Name = "cmb_consultas";
            cmb_consultas.Size = new Size(980, 28);
            cmb_consultas.TabIndex = 59;
            // 
            // dg_view_resultados
            // 
            dg_view_resultados.AllowUserToAddRows = false;
            dg_view_resultados.AllowUserToDeleteRows = false;
            dg_view_resultados.AllowUserToOrderColumns = true;
            dg_view_resultados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view_resultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_resultados.Location = new Point(32, 83);
            dg_view_resultados.Margin = new Padding(4, 5, 4, 5);
            dg_view_resultados.MultiSelect = false;
            dg_view_resultados.Name = "dg_view_resultados";
            dg_view_resultados.ReadOnly = true;
            dg_view_resultados.RowHeadersVisible = false;
            dg_view_resultados.RowHeadersWidth = 51;
            dg_view_resultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view_resultados.Size = new Size(1277, 920);
            dg_view_resultados.TabIndex = 58;
            // 
            // cmd_ejecutar
            // 
            cmd_ejecutar.Location = new Point(1120, 32);
            cmd_ejecutar.Margin = new Padding(4, 5, 4, 5);
            cmd_ejecutar.Name = "cmd_ejecutar";
            cmd_ejecutar.Size = new Size(189, 32);
            cmd_ejecutar.TabIndex = 57;
            cmd_ejecutar.Text = "Ejecutar consulta";
            cmd_ejecutar.UseVisualStyleBackColor = true;
            cmd_ejecutar.Click += cmd_ejecutar_Click;
            // 
            // lbl_consultas
            // 
            lbl_consultas.AutoSize = true;
            lbl_consultas.Location = new Point(28, 37);
            lbl_consultas.Margin = new Padding(4, 0, 4, 0);
            lbl_consultas.Name = "lbl_consultas";
            lbl_consultas.Size = new Size(72, 20);
            lbl_consultas.TabIndex = 56;
            lbl_consultas.Text = "Consultas";
            // 
            // lbl_registros
            // 
            lbl_registros.AutoSize = true;
            lbl_registros.Location = new Point(32, 1029);
            lbl_registros.Name = "lbl_registros";
            lbl_registros.Size = new Size(50, 20);
            lbl_registros.TabIndex = 60;
            lbl_registros.Text = "label1";
            // 
            // grilla_resultados
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1337, 1058);
            Controls.Add(lbl_registros);
            Controls.Add(cmb_consultas);
            Controls.Add(dg_view_resultados);
            Controls.Add(cmd_ejecutar);
            Controls.Add(lbl_consultas);
            Margin = new Padding(4, 5, 4, 5);
            Name = "grilla_resultados";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Consultas personalizadas";
            Load += grilla_resultados_Load;
            ((System.ComponentModel.ISupportInitialize)dg_view_resultados).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        internal ComboBox cmb_consultas;
        internal DataGridView dg_view_resultados;
        internal Button cmd_ejecutar;
        internal Label lbl_consultas;
        private Label lbl_registros;
    }
}


