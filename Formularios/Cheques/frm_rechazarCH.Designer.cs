using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_rechazarCH : Form
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
            cmd_filtrarCH = new Button();
            cmd_filtrarCH.Click += new EventHandler(cmd_filtrarCH_Click);
            lbl_desde = new Label();
            dtp_desde = new DateTimePicker();
            lbl_hasta = new Label();
            dtp_hasta = new DateTimePicker();
            chk_hastaSiempre = new CheckBox();
            txt_importeCH = new TextBox();
            chk_desdeSiempre = new CheckBox();
            lbl_importeCH = new Label();
            lbl_nCH = new Label();
            txt_nCH = new TextBox();
            dg_view_chCartera = new DataGridView();
            cmb_rechazarCH = new Button();
            cmb_anularCH = new Button();
            cmb_cancelarRechazo = new Button();
            cmb_cancelarAnul = new Button();
            gp_depositados = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dg_view_chCartera).BeginInit();
            gp_depositados.SuspendLayout();
            SuspendLayout();
            // 
            // cmd_filtrarCH
            // 
            cmd_filtrarCH.Location = new Point(105, 231);
            cmd_filtrarCH.Name = "cmd_filtrarCH";
            cmd_filtrarCH.Size = new Size(147, 23);
            cmd_filtrarCH.TabIndex = 149;
            cmd_filtrarCH.Text = "Filtrar cheques";
            cmd_filtrarCH.UseVisualStyleBackColor = true;
            // 
            // lbl_desde
            // 
            lbl_desde.AutoSize = true;
            lbl_desde.Location = new Point(49, 31);
            lbl_desde.Name = "lbl_desde";
            lbl_desde.Size = new Size(38, 13);
            lbl_desde.TabIndex = 140;
            lbl_desde.Text = "Desde";
            // 
            // dtp_desde
            // 
            dtp_desde.Enabled = false;
            dtp_desde.Location = new Point(158, 24);
            dtp_desde.Name = "dtp_desde";
            dtp_desde.Size = new Size(263, 20);
            dtp_desde.TabIndex = 139;
            // 
            // lbl_hasta
            // 
            lbl_hasta.AutoSize = true;
            lbl_hasta.Location = new Point(49, 69);
            lbl_hasta.Name = "lbl_hasta";
            lbl_hasta.Size = new Size(35, 13);
            lbl_hasta.TabIndex = 142;
            lbl_hasta.Text = "Hasta";
            // 
            // dtp_hasta
            // 
            dtp_hasta.Enabled = false;
            dtp_hasta.Location = new Point(158, 69);
            dtp_hasta.Name = "dtp_hasta";
            dtp_hasta.Size = new Size(263, 20);
            dtp_hasta.TabIndex = 141;
            // 
            // chk_hastaSiempre
            // 
            chk_hastaSiempre.AutoSize = true;
            chk_hastaSiempre.Checked = true;
            chk_hastaSiempre.CheckState = CheckState.Checked;
            chk_hastaSiempre.Location = new Point(19, 68);
            chk_hastaSiempre.Name = "chk_hastaSiempre";
            chk_hastaSiempre.Size = new Size(15, 14);
            chk_hastaSiempre.TabIndex = 143;
            chk_hastaSiempre.UseVisualStyleBackColor = true;
            // 
            // txt_importeCH
            // 
            txt_importeCH.Location = new Point(265, 190);
            txt_importeCH.Name = "txt_importeCH";
            txt_importeCH.Size = new Size(263, 20);
            txt_importeCH.TabIndex = 148;
            // 
            // chk_desdeSiempre
            // 
            chk_desdeSiempre.AutoSize = true;
            chk_desdeSiempre.Checked = true;
            chk_desdeSiempre.CheckState = CheckState.Checked;
            chk_desdeSiempre.Location = new Point(19, 30);
            chk_desdeSiempre.Name = "chk_desdeSiempre";
            chk_desdeSiempre.Size = new Size(15, 14);
            chk_desdeSiempre.TabIndex = 144;
            chk_desdeSiempre.UseVisualStyleBackColor = true;
            // 
            // lbl_importeCH
            // 
            lbl_importeCH.AutoSize = true;
            lbl_importeCH.Location = new Point(105, 190);
            lbl_importeCH.Name = "lbl_importeCH";
            lbl_importeCH.Size = new Size(98, 13);
            lbl_importeCH.TabIndex = 147;
            lbl_importeCH.Text = "Importe del cheque";
            // 
            // lbl_nCH
            // 
            lbl_nCH.AutoSize = true;
            lbl_nCH.Location = new Point(105, 149);
            lbl_nCH.Name = "lbl_nCH";
            lbl_nCH.Size = new Size(98, 13);
            lbl_nCH.TabIndex = 145;
            lbl_nCH.Text = "Número de cheque";
            // 
            // txt_nCH
            // 
            txt_nCH.Location = new Point(265, 149);
            txt_nCH.Name = "txt_nCH";
            txt_nCH.Size = new Size(263, 20);
            txt_nCH.TabIndex = 146;
            // 
            // dg_view_chCartera
            // 
            dg_view_chCartera.AllowUserToAddRows = false;
            dg_view_chCartera.AllowUserToDeleteRows = false;
            dg_view_chCartera.AllowUserToOrderColumns = true;
            dg_view_chCartera.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view_chCartera.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_chCartera.Enabled = false;
            dg_view_chCartera.Location = new Point(20, 281);
            dg_view_chCartera.MultiSelect = false;
            dg_view_chCartera.Name = "dg_view_chCartera";
            dg_view_chCartera.ReadOnly = true;
            dg_view_chCartera.RowHeadersVisible = false;
            dg_view_chCartera.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view_chCartera.Size = new Size(607, 279);
            dg_view_chCartera.TabIndex = 150;
            // 
            // cmb_rechazarCH
            // 
            cmb_rechazarCH.Location = new Point(20, 586);
            cmb_rechazarCH.Name = "cmb_rechazarCH";
            cmb_rechazarCH.Size = new Size(124, 23);
            cmb_rechazarCH.TabIndex = 151;
            cmb_rechazarCH.Text = "Rechazar cheque";
            cmb_rechazarCH.UseVisualStyleBackColor = true;
            // 
            // cmb_anularCH
            // 
            cmb_anularCH.Location = new Point(184, 586);
            cmb_anularCH.Name = "cmb_anularCH";
            cmb_anularCH.Size = new Size(121, 23);
            cmb_anularCH.TabIndex = 152;
            cmb_anularCH.Text = "Anular cheque";
            cmb_anularCH.UseVisualStyleBackColor = true;
            // 
            // cmb_cancelarRechazo
            // 
            cmb_cancelarRechazo.Location = new Point(345, 586);
            cmb_cancelarRechazo.Name = "cmb_cancelarRechazo";
            cmb_cancelarRechazo.Size = new Size(121, 23);
            cmb_cancelarRechazo.TabIndex = 153;
            cmb_cancelarRechazo.Text = "Cancelar rechazo";
            cmb_cancelarRechazo.UseVisualStyleBackColor = true;
            // 
            // cmb_cancelarAnul
            // 
            cmb_cancelarAnul.Location = new Point(506, 586);
            cmb_cancelarAnul.Name = "cmb_cancelarAnul";
            cmb_cancelarAnul.Size = new Size(121, 23);
            cmb_cancelarAnul.TabIndex = 154;
            cmb_cancelarAnul.Text = "Cancelar anulación";
            cmb_cancelarAnul.UseVisualStyleBackColor = true;
            // 
            // gp_depositados
            // 
            gp_depositados.Controls.Add(chk_desdeSiempre);
            gp_depositados.Controls.Add(chk_hastaSiempre);
            gp_depositados.Controls.Add(dtp_hasta);
            gp_depositados.Controls.Add(lbl_hasta);
            gp_depositados.Controls.Add(dtp_desde);
            gp_depositados.Controls.Add(lbl_desde);
            gp_depositados.Location = new Point(105, 12);
            gp_depositados.Name = "gp_depositados";
            gp_depositados.Size = new Size(433, 120);
            gp_depositados.TabIndex = 155;
            gp_depositados.TabStop = false;
            gp_depositados.Text = "Fecha de cobro";
            // 
            // frm_rechazarCH
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 642);
            Controls.Add(gp_depositados);
            Controls.Add(cmb_cancelarAnul);
            Controls.Add(cmb_cancelarRechazo);
            Controls.Add(cmb_anularCH);
            Controls.Add(cmb_rechazarCH);
            Controls.Add(dg_view_chCartera);
            Controls.Add(cmd_filtrarCH);
            Controls.Add(txt_importeCH);
            Controls.Add(lbl_importeCH);
            Controls.Add(lbl_nCH);
            Controls.Add(txt_nCH);
            Name = "frm_rechazarCH";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rechazar cheques";
            ((System.ComponentModel.ISupportInitialize)dg_view_chCartera).EndInit();
            gp_depositados.ResumeLayout(false);
            gp_depositados.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        internal Button cmd_filtrarCH;
        internal Label lbl_desde;
        internal DateTimePicker dtp_desde;
        internal Label lbl_hasta;
        internal DateTimePicker dtp_hasta;
        internal CheckBox chk_hastaSiempre;
        internal TextBox txt_importeCH;
        internal CheckBox chk_desdeSiempre;
        internal Label lbl_importeCH;
        internal Label lbl_nCH;
        internal TextBox txt_nCH;
        internal DataGridView dg_view_chCartera;
        internal Button cmb_rechazarCH;
        internal Button cmb_anularCH;
        internal Button cmb_cancelarRechazo;
        internal Button cmb_cancelarAnul;
        internal GroupBox gp_depositados;
    }
}


