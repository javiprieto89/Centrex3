using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_prnCmp : Form
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
            ReportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            DbCentrexDataSet = new dbCentrexDataSet();
            Factura_cabeceraBindingSource = new BindingSource(components);
            Factura_cabeceraTableAdapter = new dbCentrexDataSetTableAdapters.factura_cabeceraTableAdapter();
            TableAdapterManager = new dbCentrexDataSetTableAdapters.TableAdapterManager();
            Factura_detalleBindingSource = new BindingSource(components);
            Factura_detalleTableAdapter = new dbCentrexDataSetTableAdapters.factura_detalleTableAdapter();
            Datos_empresaBindingSource = new BindingSource(components);
            Datos_empresaTableAdapter = new dbCentrexDataSetTableAdapters.datos_empresaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)DbCentrexDataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Factura_cabeceraBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Factura_detalleBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Datos_empresaBindingSource).BeginInit();
            SuspendLayout();
            // 
            // ReportViewer1
            // 
            ReportViewer1.Dock = DockStyle.Fill;
            ReportViewer1.Location = new Point(0, 0);
            ReportViewer1.Name = "ReportViewer1";
            ReportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            ReportViewer1.Size = new Size(930, 733);
            ReportViewer1.TabIndex = 0;
            // 
            // DbCentrexDataSet
            // 
            DbCentrexDataSet.DataSetName = "dbCentrexDataSet";
            DbCentrexDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
            // 
            // Factura_cabeceraBindingSource
            // 
            Factura_cabeceraBindingSource.DataMember = "factura_cabecera";
            Factura_cabeceraBindingSource.DataSource = DbCentrexDataSet;
            // 
            // Factura_cabeceraTableAdapter
            // 
            Factura_cabeceraTableAdapter.ClearBeforeFill = true;
            // 
            // TableAdapterManager
            // 
            TableAdapterManager.BackupDataSetBeforeUpdate = false;
            TableAdapterManager.Connection = null;
            TableAdapterManager.UpdateOrder = dbCentrexDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // Factura_detalleBindingSource
            // 
            Factura_detalleBindingSource.DataMember = "factura_detalle";
            Factura_detalleBindingSource.DataSource = DbCentrexDataSet;
            // 
            // Factura_detalleTableAdapter
            // 
            Factura_detalleTableAdapter.ClearBeforeFill = true;
            // 
            // Datos_empresaBindingSource
            // 
            Datos_empresaBindingSource.DataMember = "datos_empresa";
            Datos_empresaBindingSource.DataSource = DbCentrexDataSet;
            // 
            // Datos_empresaTableAdapter
            // 
            Datos_empresaTableAdapter.ClearBeforeFill = true;
            // 
            // frm_prnCmp
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 733);
            Controls.Add(ReportViewer1);
            Name = "frm_prnCmp";
            Text = "Impresión";
            ((System.ComponentModel.ISupportInitialize)DbCentrexDataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)Factura_cabeceraBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)Factura_detalleBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)Datos_empresaBindingSource).EndInit();
            Load += new EventHandler(frm_reportes_Load);
            FormClosing += new FormClosingEventHandler(frm_reportes_FormClosing);
            ResumeLayout(false);

        }
        internal Microsoft.Reporting.WinForms.ReportViewer ReportViewer1;
        internal dbCentrexDataSet DbCentrexDataSet;
        internal BindingSource Factura_cabeceraBindingSource;
        internal dbCentrexDataSetTableAdapters.factura_cabeceraTableAdapter Factura_cabeceraTableAdapter;
        internal dbCentrexDataSetTableAdapters.TableAdapterManager TableAdapterManager;
        internal BindingSource Factura_detalleBindingSource;
        internal dbCentrexDataSetTableAdapters.factura_detalleTableAdapter Factura_detalleTableAdapter;
        internal BindingSource Datos_empresaBindingSource;
        internal dbCentrexDataSetTableAdapters.datos_empresaTableAdapter Datos_empresaTableAdapter;
    }
}


