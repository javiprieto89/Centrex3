using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_stock : Form
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
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_cancel = new Button();
            cmd_cancel.Click += new EventHandler(cmd_cancel_Click);
            lbl_fecha = new Label();
            ContextMenuStrip1 = new ContextMenuStrip(components);
            EditarToolStripMenuItem = new ToolStripMenuItem();
            EditarToolStripMenuItem.Click += new EventHandler(EditarToolStripMenuItem_Click);
            BorrarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem.Click += new EventHandler(BorrarToolStripMenuItem_Click);
            txt_fecha = new TextBox();
            txt_factura = new TextBox();
            lbl_factura = new Label();
            lbl_proveedor = new Label();
            cmd_additem = new Button();
            cmd_additem.Click += new EventHandler(cmd_additem_Click);
            psearch_proveedor = new PictureBox();
            psearch_proveedor.Click += new EventHandler(psearch_proveedor_Click);
            cmb_proveedor = new ComboBox();
            cmb_proveedor.KeyPress += new KeyPressEventHandler(cmb_proveedor_KeyPress);
            lbl_notas = new Label();
            txt_notas = new TextBox();
            lbl_fechaingreso = new Label();
            lbl_fecha_ingreso = new Label();
            lbl_fecha_ingreso.MouseEnter += new EventHandler(lbl_fecha_ingreso_MouseEnter);
            lbl_fecha_ingreso.MouseLeave += new EventHandler(lbl_fecha_ingreso_MouseLeave);
            lbl_tooltip = new Label();
            dg_view = new DataGridView();
            dg_view.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dg_view_CellMouseDoubleClick);
            ContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dg_view).BeginInit();
            SuspendLayout();
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(288, 472);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(159, 42);
            cmd_ok.TabIndex = 6;
            cmd_ok.Text = "Aceptar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_cancel
            // 
            cmd_cancel.DialogResult = DialogResult.Cancel;
            cmd_cancel.Location = new Point(470, 472);
            cmd_cancel.Name = "cmd_cancel";
            cmd_cancel.Size = new Size(159, 42);
            cmd_cancel.TabIndex = 7;
            cmd_cancel.Text = "Cancelar";
            cmd_cancel.UseVisualStyleBackColor = true;
            // 
            // lbl_fecha
            // 
            lbl_fecha.AutoSize = true;
            lbl_fecha.Location = new Point(27, 16);
            lbl_fecha.Name = "lbl_fecha";
            lbl_fecha.Size = new Size(40, 13);
            lbl_fecha.TabIndex = 8;
            lbl_fecha.Text = "Fecha:";
            // 
            // ContextMenuStrip1
            // 
            ContextMenuStrip1.ImageScalingSize = new Size(28, 28);
            ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { EditarToolStripMenuItem, BorrarToolStripMenuItem });
            ContextMenuStrip1.Name = "ContextMenuStrip";
            ContextMenuStrip1.Size = new Size(107, 48);
            // 
            // EditarToolStripMenuItem
            // 
            EditarToolStripMenuItem.Name = "EditarToolStripMenuItem";
            EditarToolStripMenuItem.Size = new Size(106, 22);
            EditarToolStripMenuItem.Text = "Editar";
            // 
            // BorrarToolStripMenuItem
            // 
            BorrarToolStripMenuItem.Name = "BorrarToolStripMenuItem";
            BorrarToolStripMenuItem.Size = new Size(106, 22);
            BorrarToolStripMenuItem.Text = "Borrar";
            // 
            // txt_fecha
            // 
            txt_fecha.Location = new Point(95, 12);
            txt_fecha.Margin = new Padding(2);
            txt_fecha.Name = "txt_fecha";
            txt_fecha.Size = new Size(154, 20);
            txt_fecha.TabIndex = 0;
            // 
            // txt_factura
            // 
            txt_factura.Location = new Point(95, 40);
            txt_factura.Margin = new Padding(2);
            txt_factura.Name = "txt_factura";
            txt_factura.Size = new Size(154, 20);
            txt_factura.TabIndex = 1;
            // 
            // lbl_factura
            // 
            lbl_factura.AutoSize = true;
            lbl_factura.Location = new Point(26, 41);
            lbl_factura.Margin = new Padding(2, 0, 2, 0);
            lbl_factura.Name = "lbl_factura";
            lbl_factura.Size = new Size(43, 13);
            lbl_factura.TabIndex = 9;
            lbl_factura.Text = "Factura";
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(26, 68);
            lbl_proveedor.Margin = new Padding(2, 0, 2, 0);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 10;
            lbl_proveedor.Text = "Proveedor";
            // 
            // cmd_additem
            // 
            cmd_additem.Location = new Point(23, 112);
            cmd_additem.Name = "cmd_additem";
            cmd_additem.Size = new Size(75, 23);
            cmd_additem.TabIndex = 4;
            cmd_additem.Text = "Agregar item";
            cmd_additem.UseVisualStyleBackColor = true;
            // 
            // psearch_proveedor
            // 
            psearch_proveedor.Image = My.Resources.Resources.iconoLupa;
            psearch_proveedor.Location = new Point(254, 66);
            psearch_proveedor.Name = "psearch_proveedor";
            psearch_proveedor.Size = new Size(22, 22);
            psearch_proveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_proveedor.TabIndex = 70;
            psearch_proveedor.TabStop = false;
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(95, 66);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(154, 21);
            cmb_proveedor.TabIndex = 2;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(312, 55);
            lbl_notas.Margin = new Padding(2, 0, 2, 0);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 11;
            lbl_notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(412, 41);
            txt_notas.Margin = new Padding(2);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(482, 45);
            txt_notas.TabIndex = 3;
            txt_notas.WordWrap = false;
            // 
            // lbl_fechaingreso
            // 
            lbl_fechaingreso.AutoSize = true;
            lbl_fechaingreso.Location = new Point(312, 16);
            lbl_fechaingreso.Name = "lbl_fechaingreso";
            lbl_fechaingreso.Size = new Size(92, 13);
            lbl_fechaingreso.TabIndex = 71;
            lbl_fechaingreso.Text = "Fecha de ingreso:";
            // 
            // lbl_fecha_ingreso
            // 
            lbl_fecha_ingreso.AutoSize = true;
            lbl_fecha_ingreso.Location = new Point(410, 16);
            lbl_fecha_ingreso.Name = "lbl_fecha_ingreso";
            lbl_fecha_ingreso.Size = new Size(40, 13);
            lbl_fecha_ingreso.TabIndex = 72;
            lbl_fecha_ingreso.Text = "Fecha:";
            // 
            // lbl_tooltip
            // 
            lbl_tooltip.AutoSize = true;
            lbl_tooltip.ForeColor = Color.Red;
            lbl_tooltip.Location = new Point(428, 30);
            lbl_tooltip.Margin = new Padding(2, 0, 2, 0);
            lbl_tooltip.Name = "lbl_tooltip";
            lbl_tooltip.Size = new Size(224, 13);
            lbl_tooltip.TabIndex = 73;
            lbl_tooltip.Text = "La fecha de ingreso NO puede ser modificada";
            lbl_tooltip.Visible = false;
            // 
            // dg_view
            // 
            dg_view.AllowUserToAddRows = false;
            dg_view.AllowUserToDeleteRows = false;
            dg_view.AllowUserToOrderColumns = true;
            dg_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dg_view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view.ContextMenuStrip = ContextMenuStrip1;
            dg_view.Location = new Point(23, 141);
            dg_view.MultiSelect = false;
            dg_view.Name = "dg_view";
            dg_view.ReadOnly = true;
            dg_view.RowHeadersVisible = false;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.Size = new Size(871, 312);
            dg_view.TabIndex = 86;
            // 
            // add_stock
            // 
            AcceptButton = cmd_ok;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmd_cancel;
            ClientSize = new Size(914, 525);
            Controls.Add(dg_view);
            Controls.Add(lbl_tooltip);
            Controls.Add(lbl_fecha_ingreso);
            Controls.Add(lbl_fechaingreso);
            Controls.Add(txt_notas);
            Controls.Add(lbl_notas);
            Controls.Add(psearch_proveedor);
            Controls.Add(cmb_proveedor);
            Controls.Add(cmd_additem);
            Controls.Add(lbl_proveedor);
            Controls.Add(txt_factura);
            Controls.Add(lbl_factura);
            Controls.Add(txt_fecha);
            Controls.Add(lbl_fecha);
            Controls.Add(cmd_cancel);
            Controls.Add(cmd_ok);
            Margin = new Padding(2);
            Name = "add_stock";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingreso de mercaderia";
            ContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            Load += new EventHandler(add_stock_Load);
            FormClosing += new FormClosingEventHandler(add_stock_FormClosing);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Button cmd_ok;
        internal Button cmd_cancel;
        internal Label lbl_fecha;
        internal TextBox txt_fecha;
        internal TextBox txt_factura;
        internal Label lbl_factura;
        internal Label lbl_proveedor;
        internal Button cmd_additem;
        internal PictureBox psearch_proveedor;
        internal ComboBox cmb_proveedor;
        internal Label lbl_notas;
        internal TextBox txt_notas;
        internal ContextMenuStrip ContextMenuStrip1;
        internal ToolStripMenuItem EditarToolStripMenuItem;
        internal ToolStripMenuItem BorrarToolStripMenuItem;
        internal Label lbl_fechaingreso;
        internal Label lbl_fecha_ingreso;
        internal Label lbl_tooltip;
        internal DataGridView dg_view;
    }
}


