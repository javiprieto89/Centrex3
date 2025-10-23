using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class infoccProveedores : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(infoccProveedores));
            chk_desdeSiempre = new CheckBox();
            chk_desdeSiempre.CheckedChanged += new EventHandler(chk_desdeSiempre_CheckedChanged);
            chk_hastaSiempre = new CheckBox();
            chk_hastaSiempre.CheckedChanged += new EventHandler(chk_hastaSiempre_CheckedChanged);
            dtp_hasta = new DateTimePicker();
            lbl_hasta = new Label();
            dtp_desde = new DateTimePicker();
            lbl_desde = new Label();
            cmb_proveedor = new ComboBox();
            cmb_proveedor.SelectionChangeCommitted += new EventHandler(cmb_proveedor_SelectionChangeCommitted);
            dg_view = new DataGridView();
            dg_view.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dg_view_CellMouseDoubleClick);
            cmd_consultar = new Button();
            cmd_consultar.Click += new EventHandler(cmd_consultar_Click);
            lbl_proveedor = new Label();
            lbl_cc = new Label();
            cmb_cc = new ComboBox();
            pExportXLS = new PictureBox();
            pExportXLS.Click += new EventHandler(pExportXLS_Click);
            psearch_cc = new PictureBox();
            psearch_cc.Click += new EventHandler(psearch_cc_Click);
            psearch_proveedor = new PictureBox();
            psearch_proveedor.Click += new EventHandler(psearch_proveedor_Click);
            SaveFileDialog1 = new SaveFileDialog();
            cmd_go = new Button();
            cmd_go.Click += new EventHandler(cmd_go_Click);
            txt_nPage = new TextBox();
            cmd_last = new Button();
            cmd_last.Click += new EventHandler(cmd_last_Click);
            cmd_next = new Button();
            cmd_next.Click += new EventHandler(cmd_next_Click);
            cmd_prev = new Button();
            cmd_prev.Click += new EventHandler(cmd_prev_Click);
            cmd_first = new Button();
            cmd_first.Click += new EventHandler(cmd_first_Click);
            lbl_total = new Label();
            lblTotal = new Label();
            lbl_saldo = new Label();
            lbl_saldo2 = new Label();
            dgView_paraExportar = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dg_view).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pExportXLS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_cc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgView_paraExportar).BeginInit();
            SuspendLayout();
            // 
            // chk_desdeSiempre
            // 
            chk_desdeSiempre.AutoSize = true;
            chk_desdeSiempre.Checked = true;
            chk_desdeSiempre.CheckState = CheckState.Checked;
            chk_desdeSiempre.Location = new Point(431, 64);
            chk_desdeSiempre.Name = "chk_desdeSiempre";
            chk_desdeSiempre.Size = new Size(116, 17);
            chk_desdeSiempre.TabIndex = 122;
            chk_desdeSiempre.Text = "Desde el comienzo";
            chk_desdeSiempre.UseVisualStyleBackColor = true;
            // 
            // chk_hastaSiempre
            // 
            chk_hastaSiempre.AutoSize = true;
            chk_hastaSiempre.Checked = true;
            chk_hastaSiempre.CheckState = CheckState.Checked;
            chk_hastaSiempre.Location = new Point(431, 103);
            chk_hastaSiempre.Name = "chk_hastaSiempre";
            chk_hastaSiempre.Size = new Size(87, 17);
            chk_hastaSiempre.TabIndex = 121;
            chk_hastaSiempre.Text = "Hasta el final";
            chk_hastaSiempre.UseVisualStyleBackColor = true;
            // 
            // dtp_hasta
            // 
            dtp_hasta.Enabled = false;
            dtp_hasta.Location = new Point(177, 100);
            dtp_hasta.Name = "dtp_hasta";
            dtp_hasta.Size = new Size(248, 20);
            dtp_hasta.TabIndex = 3;
            // 
            // lbl_hasta
            // 
            lbl_hasta.AutoSize = true;
            lbl_hasta.Location = new Point(102, 102);
            lbl_hasta.Name = "lbl_hasta";
            lbl_hasta.Size = new Size(35, 13);
            lbl_hasta.TabIndex = 115;
            lbl_hasta.Text = "Hasta";
            // 
            // dtp_desde
            // 
            dtp_desde.Enabled = false;
            dtp_desde.Location = new Point(177, 61);
            dtp_desde.Name = "dtp_desde";
            dtp_desde.Size = new Size(248, 20);
            dtp_desde.TabIndex = 2;
            // 
            // lbl_desde
            // 
            lbl_desde.AutoSize = true;
            lbl_desde.Location = new Point(102, 63);
            lbl_desde.Name = "lbl_desde";
            lbl_desde.Size = new Size(38, 13);
            lbl_desde.TabIndex = 113;
            lbl_desde.Text = "Desde";
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(177, 20);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(248, 21);
            cmb_proveedor.TabIndex = 0;
            cmb_proveedor.Text = "Seleccione un proveedor...";
            // 
            // dg_view
            // 
            dg_view.AllowUserToAddRows = false;
            dg_view.AllowUserToDeleteRows = false;
            dg_view.AllowUserToOrderColumns = true;
            dg_view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view.Location = new Point(22, 278);
            dg_view.MultiSelect = false;
            dg_view.Name = "dg_view";
            dg_view.ReadOnly = true;
            dg_view.RowHeadersVisible = false;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.Size = new Size(958, 367);
            dg_view.TabIndex = 6;
            // 
            // cmd_consultar
            // 
            cmd_consultar.Location = new Point(431, 163);
            cmd_consultar.Name = "cmd_consultar";
            cmd_consultar.Size = new Size(142, 44);
            cmd_consultar.TabIndex = 5;
            cmd_consultar.Text = "Ejecutar consulta";
            cmd_consultar.UseVisualStyleBackColor = true;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(101, 24);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 108;
            lbl_proveedor.Text = "Proveedor";
            // 
            // lbl_cc
            // 
            lbl_cc.AutoSize = true;
            lbl_cc.Location = new Point(481, 24);
            lbl_cc.Name = "lbl_cc";
            lbl_cc.Size = new Size(85, 13);
            lbl_cc.TabIndex = 130;
            lbl_cc.Text = "Cuenta corriente";
            // 
            // cmb_cc
            // 
            cmb_cc.Enabled = false;
            cmb_cc.FormattingEnabled = true;
            cmb_cc.Location = new Point(584, 20);
            cmb_cc.Name = "cmb_cc";
            cmb_cc.Size = new Size(248, 21);
            cmb_cc.TabIndex = 136;
            cmb_cc.Text = "Seleccione una cuenta corriente...";
            // 
            // pExportXLS
            // 
            pExportXLS.Image = (Image)resources.GetObject("pExportXLS.Image");
            pExportXLS.Location = new Point(22, 221);
            pExportXLS.Name = "pExportXLS";
            pExportXLS.Size = new Size(55, 42);
            pExportXLS.SizeMode = PictureBoxSizeMode.AutoSize;
            pExportXLS.TabIndex = 137;
            pExportXLS.TabStop = false;
            // 
            // psearch_cc
            // 
            psearch_cc.Image = (Image)resources.GetObject("psearch_cc.Image");
            psearch_cc.Location = new Point(841, 19);
            psearch_cc.Name = "psearch_cc";
            psearch_cc.Size = new Size(22, 22);
            psearch_cc.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_cc.TabIndex = 132;
            psearch_cc.TabStop = false;
            // 
            // psearch_proveedor
            // 
            psearch_proveedor.Image = (Image)resources.GetObject("psearch_proveedor.Image");
            psearch_proveedor.Location = new Point(431, 20);
            psearch_proveedor.Name = "psearch_proveedor";
            psearch_proveedor.Size = new Size(22, 22);
            psearch_proveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_proveedor.TabIndex = 125;
            psearch_proveedor.TabStop = false;
            // 
            // cmd_go
            // 
            cmd_go.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_go.Enabled = false;
            cmd_go.Location = new Point(950, 250);
            cmd_go.Name = "cmd_go";
            cmd_go.Size = new Size(29, 20);
            cmd_go.TabIndex = 153;
            cmd_go.Text = "Ir";
            cmd_go.UseVisualStyleBackColor = true;
            // 
            // txt_nPage
            // 
            txt_nPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nPage.Enabled = false;
            txt_nPage.Location = new Point(881, 250);
            txt_nPage.Name = "txt_nPage";
            txt_nPage.Size = new Size(63, 20);
            txt_nPage.TabIndex = 152;
            // 
            // cmd_last
            // 
            cmd_last.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_last.Enabled = false;
            cmd_last.Location = new Point(846, 250);
            cmd_last.Name = "cmd_last";
            cmd_last.Size = new Size(29, 20);
            cmd_last.TabIndex = 151;
            cmd_last.Text = ">>|";
            cmd_last.UseVisualStyleBackColor = true;
            // 
            // cmd_next
            // 
            cmd_next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_next.Enabled = false;
            cmd_next.Location = new Point(800, 250);
            cmd_next.Name = "cmd_next";
            cmd_next.Size = new Size(40, 20);
            cmd_next.TabIndex = 150;
            cmd_next.Text = ">>";
            cmd_next.UseVisualStyleBackColor = true;
            // 
            // cmd_prev
            // 
            cmd_prev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_prev.Enabled = false;
            cmd_prev.Location = new Point(754, 250);
            cmd_prev.Name = "cmd_prev";
            cmd_prev.Size = new Size(40, 20);
            cmd_prev.TabIndex = 149;
            cmd_prev.Text = "<<";
            cmd_prev.UseVisualStyleBackColor = true;
            // 
            // cmd_first
            // 
            cmd_first.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_first.Enabled = false;
            cmd_first.Location = new Point(719, 250);
            cmd_first.Name = "cmd_first";
            cmd_first.Size = new Size(29, 20);
            cmd_first.TabIndex = 148;
            cmd_first.Text = "|<<";
            cmd_first.UseVisualStyleBackColor = true;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_total.ForeColor = Color.Blue;
            lbl_total.Location = new Point(495, 250);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(71, 13);
            lbl_total.TabIndex = 147;
            lbl_total.Text = "%$$_total%";
            lbl_total.Visible = false;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = SystemColors.ControlText;
            lblTotal.Location = new Point(395, 250);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(98, 13);
            lblTotal.TabIndex = 146;
            lblTotal.Text = "Total facturado:";
            // 
            // lbl_saldo
            // 
            lbl_saldo.AutoSize = true;
            lbl_saldo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_saldo.ForeColor = Color.Green;
            lbl_saldo.Location = new Point(153, 250);
            lbl_saldo.Name = "lbl_saldo";
            lbl_saldo.Size = new Size(85, 13);
            lbl_saldo.TabIndex = 145;
            lbl_saldo.Text = "%$$_saldo%%";
            lbl_saldo.Visible = false;
            // 
            // lbl_saldo2
            // 
            lbl_saldo2.AutoSize = true;
            lbl_saldo2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_saldo2.ForeColor = SystemColors.ControlText;
            lbl_saldo2.Location = new Point(102, 250);
            lbl_saldo2.Name = "lbl_saldo2";
            lbl_saldo2.Size = new Size(43, 13);
            lbl_saldo2.TabIndex = 144;
            lbl_saldo2.Text = "Saldo:";
            // 
            // dgView_paraExportar
            // 
            dgView_paraExportar.AllowUserToAddRows = false;
            dgView_paraExportar.AllowUserToDeleteRows = false;
            dgView_paraExportar.AllowUserToOrderColumns = true;
            dgView_paraExportar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgView_paraExportar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgView_paraExportar.Location = new Point(902, 12);
            dgView_paraExportar.MultiSelect = false;
            dgView_paraExportar.Name = "dgView_paraExportar";
            dgView_paraExportar.ReadOnly = true;
            dgView_paraExportar.RowHeadersVisible = false;
            dgView_paraExportar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgView_paraExportar.Size = new Size(89, 64);
            dgView_paraExportar.TabIndex = 154;
            dgView_paraExportar.Visible = false;
            // 
            // infoccProveedores
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1003, 672);
            Controls.Add(dgView_paraExportar);
            Controls.Add(cmd_go);
            Controls.Add(txt_nPage);
            Controls.Add(cmd_last);
            Controls.Add(cmd_next);
            Controls.Add(cmd_prev);
            Controls.Add(cmd_first);
            Controls.Add(lbl_total);
            Controls.Add(lblTotal);
            Controls.Add(lbl_saldo);
            Controls.Add(lbl_saldo2);
            Controls.Add(pExportXLS);
            Controls.Add(cmb_cc);
            Controls.Add(psearch_cc);
            Controls.Add(lbl_cc);
            Controls.Add(psearch_proveedor);
            Controls.Add(chk_desdeSiempre);
            Controls.Add(chk_hastaSiempre);
            Controls.Add(dtp_hasta);
            Controls.Add(lbl_hasta);
            Controls.Add(dtp_desde);
            Controls.Add(lbl_desde);
            Controls.Add(cmb_proveedor);
            Controls.Add(dg_view);
            Controls.Add(cmd_consultar);
            Controls.Add(lbl_proveedor);
            Name = "infoccProveedores";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cuenta corriente de proveedores";
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            ((System.ComponentModel.ISupportInitialize)pExportXLS).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_cc).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgView_paraExportar).EndInit();
            Load += new EventHandler(ccProveedores_Load);
            FormClosed += new FormClosedEventHandler(ccProveedores_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }
        internal CheckBox chk_desdeSiempre;
        internal CheckBox chk_hastaSiempre;
        internal DateTimePicker dtp_hasta;
        internal Label lbl_hasta;
        internal DateTimePicker dtp_desde;
        internal Label lbl_desde;
        internal ComboBox cmb_proveedor;
        internal DataGridView dg_view;
        internal Button cmd_consultar;
        internal Label lbl_proveedor;
        internal PictureBox psearch_proveedor;
        internal Label lbl_cc;
        internal PictureBox psearch_cc;
        internal ComboBox cmb_cc;
        internal PictureBox pExportXLS;
        internal SaveFileDialog SaveFileDialog1;
        internal Button cmd_go;
        internal TextBox txt_nPage;
        internal Button cmd_last;
        internal Button cmd_next;
        internal Button cmd_prev;
        internal Button cmd_first;
        internal Label lbl_total;
        internal Label lblTotal;
        internal Label lbl_saldo;
        internal Label lbl_saldo2;
        internal DataGridView dgView_paraExportar;
    }
}
