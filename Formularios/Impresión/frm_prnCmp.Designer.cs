using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    public partial class frm_prnCmp : Form
    {
        private bool _exPRE;
        private bool _imprimePRN;

        // 🔹 Constructor por defecto (requerido por el diseñador)
        public frm_prnCmp()
        {
            InitializeComponent();
        }

        // 🔹 Constructor con parámetros (para llamadas como new frm_prnCmp(true, false))
        public frm_prnCmp(bool esPresupuesto, bool imprimeRemito)
        {
            InitializeComponent();
            _exPRE = esPresupuesto;
            _imprimePRN = imprimeRemito;
        }

        // 🔹 Limpieza de recursos
        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                    components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // 🔹 Diseñador (auto-generado)
        private System.ComponentModel.IContainer components;

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ReportViewer1 = new Panel();
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

            // Panel principal
            ReportViewer1.Dock = DockStyle.Fill;
            ReportViewer1.Location = new Point(0, 0);
            ReportViewer1.Name = "ReportViewer1";
            ReportViewer1.Size = new Size(930, 733);
            ReportViewer1.TabIndex = 0;

            // Dataset
            DbCentrexDataSet.DataSetName = "dbCentrexDataSet";
            DbCentrexDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;

            // BindingSources
            Factura_cabeceraBindingSource.DataMember = "factura_cabecera";
            Factura_cabeceraBindingSource.DataSource = DbCentrexDataSet;

            Factura_detalleBindingSource.DataMember = "factura_detalle";
            Factura_detalleBindingSource.DataSource = DbCentrexDataSet;

            Datos_empresaBindingSource.DataMember = "datos_empresa";
            Datos_empresaBindingSource.DataSource = DbCentrexDataSet;

            // TableAdapters
            Factura_cabeceraTableAdapter.ClearBeforeFill = true;
            Factura_detalleTableAdapter.ClearBeforeFill = true;
            Datos_empresaTableAdapter.ClearBeforeFill = true;

            TableAdapterManager.BackupDataSetBeforeUpdate = false;
            TableAdapterManager.UpdateOrder =
                dbCentrexDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;

            // Propiedades del formulario
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
            ResumeLayout(false);
        }

        // 🔹 Controles internos
        internal Panel ReportViewer1;
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
