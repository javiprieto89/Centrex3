using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class search : Form
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
            txt_search = new TextBox();
            txt_search.KeyPress += new KeyPressEventHandler(txt_search_KeyPress);
            lblbusqueda = new Label();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            chk_secuencia = new CheckBox();
            lbl_borrarbusqueda = new Label();
            lbl_borrarbusqueda.DoubleClick += new EventHandler(lbl_borrarbusqueda_DoubleClick);
            cmd_addItem = new Button();
            dg_view = new DataGridView();
            dg_view.CellDoubleClick += new DataGridViewCellEventHandler(dg_view_CellDoubleClick);
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
            lbl_totalRegistros = new Label();
            ((System.ComponentModel.ISupportInitialize)dg_view).BeginInit();
            SuspendLayout();
            // 
            // txt_search
            // 
            txt_search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txt_search.Location = new Point(137, 46);
            txt_search.Margin = new Padding(4, 4, 4, 4);
            txt_search.Name = "txt_search";
            txt_search.Size = new Size(687, 22);
            txt_search.TabIndex = 0;
            // 
            // lblbusqueda
            // 
            lblbusqueda.AutoSize = true;
            lblbusqueda.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblbusqueda.Location = new Point(33, 49);
            lblbusqueda.Margin = new Padding(4, 0, 4, 0);
            lblbusqueda.Name = "lblbusqueda";
            lblbusqueda.Size = new Size(80, 17);
            lblbusqueda.TabIndex = 11;
            lblbusqueda.Text = "Búsqueda";
            // 
            // cmd_ok
            // 
            cmd_ok.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_ok.Location = new Point(875, 31);
            cmd_ok.Margin = new Padding(4, 4, 4, 4);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(224, 52);
            cmd_ok.TabIndex = 1;
            cmd_ok.Text = "Seleccionar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(36, 630);
            chk_secuencia.Margin = new Padding(4, 4, 4, 4);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(139, 21);
            chk_secuencia.TabIndex = 12;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            chk_secuencia.Visible = false;
            // 
            // lbl_borrarbusqueda
            // 
            lbl_borrarbusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_borrarbusqueda.AutoSize = true;
            lbl_borrarbusqueda.Location = new Point(832, 54);
            lbl_borrarbusqueda.Name = "lbl_borrarbusqueda";
            lbl_borrarbusqueda.Size = new Size(14, 17);
            lbl_borrarbusqueda.TabIndex = 22;
            lbl_borrarbusqueda.Text = "x";
            // 
            // cmd_addItem
            // 
            cmd_addItem.Location = new Point(36, 94);
            cmd_addItem.Margin = new Padding(4, 4, 4, 4);
            cmd_addItem.Name = "cmd_addItem";
            cmd_addItem.Size = new Size(143, 28);
            cmd_addItem.TabIndex = 23;
            cmd_addItem.Text = "Agregar producto";
            cmd_addItem.UseVisualStyleBackColor = true;
            cmd_addItem.Visible = false;
            // 
            // dg_view
            // 
            dg_view.AllowUserToAddRows = false;
            dg_view.AllowUserToDeleteRows = false;
            dg_view.AllowUserToOrderColumns = true;
            dg_view.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dg_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dg_view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view.Location = new Point(36, 138);
            dg_view.Margin = new Padding(4, 4, 4, 4);
            dg_view.MultiSelect = false;
            dg_view.Name = "dg_view";
            dg_view.ReadOnly = true;
            dg_view.RowHeadersVisible = false;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.Size = new Size(1061, 475);
            dg_view.TabIndex = 54;
            dg_view.KeyDown += new KeyEventHandler(dg_view_KeyDown);
            // 
            // cmd_go
            // 
            cmd_go.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_go.Location = new Point(1059, 97);
            cmd_go.Margin = new Padding(4, 4, 4, 4);
            cmd_go.Name = "cmd_go";
            cmd_go.Size = new Size(39, 25);
            cmd_go.TabIndex = 72;
            cmd_go.Text = "Ir";
            cmd_go.UseVisualStyleBackColor = true;
            // 
            // txt_nPage
            // 
            txt_nPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nPage.Location = new Point(967, 97);
            txt_nPage.Margin = new Padding(4, 4, 4, 4);
            txt_nPage.Name = "txt_nPage";
            txt_nPage.Size = new Size(83, 22);
            txt_nPage.TabIndex = 71;
            // 
            // cmd_last
            // 
            cmd_last.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_last.Location = new Point(920, 97);
            cmd_last.Margin = new Padding(4, 4, 4, 4);
            cmd_last.Name = "cmd_last";
            cmd_last.Size = new Size(39, 25);
            cmd_last.TabIndex = 70;
            cmd_last.Text = ">>|";
            cmd_last.UseVisualStyleBackColor = true;
            // 
            // cmd_next
            // 
            cmd_next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_next.Location = new Point(859, 97);
            cmd_next.Margin = new Padding(4, 4, 4, 4);
            cmd_next.Name = "cmd_next";
            cmd_next.Size = new Size(53, 25);
            cmd_next.TabIndex = 69;
            cmd_next.Text = ">>";
            cmd_next.UseVisualStyleBackColor = true;
            // 
            // cmd_prev
            // 
            cmd_prev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_prev.Location = new Point(797, 97);
            cmd_prev.Margin = new Padding(4, 4, 4, 4);
            cmd_prev.Name = "cmd_prev";
            cmd_prev.Size = new Size(53, 25);
            cmd_prev.TabIndex = 68;
            cmd_prev.Text = "<<";
            cmd_prev.UseVisualStyleBackColor = true;
            // 
            // cmd_first
            // 
            cmd_first.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_first.Location = new Point(751, 97);
            cmd_first.Margin = new Padding(4, 4, 4, 4);
            cmd_first.Name = "cmd_first";
            cmd_first.Size = new Size(39, 25);
            cmd_first.TabIndex = 67;
            cmd_first.Text = "|<<";
            cmd_first.UseVisualStyleBackColor = true;
            // 
            // lbl_totalRegistros
            // 
            lbl_totalRegistros.AutoSize = true;
            lbl_totalRegistros.Location = new Point(258, 631);
            lbl_totalRegistros.Name = "lbl_totalRegistros";
            lbl_totalRegistros.Size = new Size(117, 17);
            lbl_totalRegistros.TabIndex = 73;
            lbl_totalRegistros.Text = "lbl_totalRegistros";
            // 
            // search
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 672);
            Controls.Add(lbl_totalRegistros);
            Controls.Add(cmd_go);
            Controls.Add(txt_nPage);
            Controls.Add(cmd_last);
            Controls.Add(cmd_next);
            Controls.Add(cmd_prev);
            Controls.Add(cmd_first);
            Controls.Add(dg_view);
            Controls.Add(cmd_addItem);
            Controls.Add(lbl_borrarbusqueda);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_ok);
            Controls.Add(txt_search);
            Controls.Add(lblbusqueda);
            Margin = new Padding(4, 4, 4, 4);
            Name = "search";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Buscar";
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            Load += new EventHandler(search_Load);
            FormClosed += new FormClosedEventHandler(search_FormClosed);
            KeyUp += new KeyEventHandler(search_KeyUp);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TextBox txt_search;
        internal Label lblbusqueda;
        internal Button cmd_ok;
        internal CheckBox chk_secuencia;
        internal Label lbl_borrarbusqueda;
        internal Button cmd_addItem;
        internal DataGridView dg_view;
        internal Button cmd_go;
        internal TextBox txt_nPage;
        internal Button cmd_last;
        internal Button cmd_next;
        internal Button cmd_prev;
        internal Button cmd_first;
        internal Label lbl_totalRegistros;
    }
}


