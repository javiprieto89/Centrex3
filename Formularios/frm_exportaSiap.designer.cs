using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_exportaSiap : Form
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
            lbl_exportar = new Label();
            Label1 = new Label();
            chk_hasta = new CheckBox();
            chk_hasta.CheckedChanged += new EventHandler(chk_hasta_CheckedChanged);
            chk_desde = new CheckBox();
            chk_desde.CheckedChanged += new EventHandler(chk_desde_CheckedChanged);
            dtp_hasta = new DateTimePicker();
            dtp_desde = new DateTimePicker();
            Label2 = new Label();
            cmd_cerrar = new Button();
            cmd_cerrar.Click += new EventHandler(cmd_cerrar_Click);
            cmb_consultas = new ComboBox();
            cmb_consultas.SelectionChangeCommitted += new EventHandler(cmb_consultas_SelectionChangeCommitted);
            SaveFileDialog1 = new SaveFileDialog();
            pExportTXT = new PictureBox();
            pExportTXT.Click += new EventHandler(pExportTXT_Click);
            pExportXLS = new PictureBox();
            pExportXLS.Click += new EventHandler(pExportXLS_Click);
            ((System.ComponentModel.ISupportInitialize)pExportTXT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pExportXLS).BeginInit();
            SuspendLayout();
            // 
            // lbl_exportar
            // 
            lbl_exportar.AutoSize = true;
            lbl_exportar.Location = new Point(11, 13);
            lbl_exportar.Name = "lbl_exportar";
            lbl_exportar.Size = new Size(85, 13);
            lbl_exportar.TabIndex = 0;
            lbl_exportar.Text = "Datos a exportar";
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(38, 214);
            Label1.Name = "Label1";
            Label1.Size = new Size(135, 13);
            Label1.TabIndex = 130;
            Label1.Text = "Exportar resultados a Excel";
            Label1.Visible = false;
            // 
            // chk_hasta
            // 
            chk_hasta.AutoSize = true;
            chk_hasta.Location = new Point(14, 118);
            chk_hasta.Name = "chk_hasta";
            chk_hasta.Size = new Size(54, 17);
            chk_hasta.TabIndex = 139;
            chk_hasta.Text = "Hasta";
            chk_hasta.UseVisualStyleBackColor = true;
            // 
            // chk_desde
            // 
            chk_desde.AutoSize = true;
            chk_desde.Location = new Point(14, 80);
            chk_desde.Name = "chk_desde";
            chk_desde.Size = new Size(57, 17);
            chk_desde.TabIndex = 138;
            chk_desde.Text = "Desde";
            chk_desde.UseVisualStyleBackColor = true;
            // 
            // dtp_hasta
            // 
            dtp_hasta.Enabled = false;
            dtp_hasta.Location = new Point(77, 118);
            dtp_hasta.Name = "dtp_hasta";
            dtp_hasta.Size = new Size(291, 20);
            dtp_hasta.TabIndex = 137;
            // 
            // dtp_desde
            // 
            dtp_desde.Enabled = false;
            dtp_desde.Location = new Point(77, 80);
            dtp_desde.Name = "dtp_desde";
            dtp_desde.Size = new Size(293, 20);
            dtp_desde.TabIndex = 136;
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(208, 214);
            Label2.Name = "Label2";
            Label2.Size = new Size(130, 13);
            Label2.TabIndex = 141;
            Label2.Text = "Exportar resultados a TXT";
            Label2.Visible = false;
            // 
            // cmd_cerrar
            // 
            cmd_cerrar.Location = new Point(119, 264);
            cmd_cerrar.Name = "cmd_cerrar";
            cmd_cerrar.Size = new Size(142, 21);
            cmd_cerrar.TabIndex = 143;
            cmd_cerrar.Text = "Cerrar";
            cmd_cerrar.UseVisualStyleBackColor = true;
            // 
            // cmb_consultas
            // 
            cmb_consultas.FormattingEnabled = true;
            cmb_consultas.Location = new Point(14, 38);
            cmb_consultas.Name = "cmb_consultas";
            cmb_consultas.Size = new Size(356, 21);
            cmb_consultas.TabIndex = 0;
            // 
            // pExportTXT
            // 
            pExportTXT.Image = My.Resources.Resources.iconotxtByN;
            pExportTXT.Location = new Point(240, 164);
            pExportTXT.Name = "pExportTXT";
            pExportTXT.Size = new Size(55, 42);
            pExportTXT.SizeMode = PictureBoxSizeMode.StretchImage;
            pExportTXT.TabIndex = 142;
            pExportTXT.TabStop = false;
            // 
            // pExportXLS
            // 
            pExportXLS.Image = My.Resources.Resources.iconoExcelByN;
            pExportXLS.Location = new Point(67, 164);
            pExportXLS.Name = "pExportXLS";
            pExportXLS.Size = new Size(55, 42);
            pExportXLS.SizeMode = PictureBoxSizeMode.AutoSize;
            pExportXLS.TabIndex = 131;
            pExportXLS.TabStop = false;
            // 
            // frm_exportaSiap
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 297);
            Controls.Add(cmb_consultas);
            Controls.Add(cmd_cerrar);
            Controls.Add(pExportTXT);
            Controls.Add(Label2);
            Controls.Add(chk_hasta);
            Controls.Add(chk_desde);
            Controls.Add(dtp_hasta);
            Controls.Add(dtp_desde);
            Controls.Add(pExportXLS);
            Controls.Add(Label1);
            Controls.Add(lbl_exportar);
            Name = "frm_exportaSiap";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Exportación de datos SIAp";
            ((System.ComponentModel.ISupportInitialize)pExportTXT).EndInit();
            ((System.ComponentModel.ISupportInitialize)pExportXLS).EndInit();
            Load += new EventHandler(frm_exportaSiap_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_exportar;
        internal PictureBox pExportXLS;
        internal Label Label1;
        internal CheckBox chk_hasta;
        internal CheckBox chk_desde;
        internal DateTimePicker dtp_hasta;
        internal DateTimePicker dtp_desde;
        internal PictureBox pExportTXT;
        internal Label Label2;
        internal Button cmd_cerrar;
        internal ComboBox cmb_consultas;
        internal SaveFileDialog SaveFileDialog1;
    }
}
