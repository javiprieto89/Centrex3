using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_ordenCompra : Form
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
            cmd_add_item = new Button();
            cmd_add_item.Click += new EventHandler(cmd_add_item_Click);
            lbl_fechaCarga = new Label();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_proveedor = new Label();
            lbl_fecha1 = new Label();
            cms_general = new ContextMenuStrip(components);
            EditarToolStripMenuItem = new ToolStripMenuItem();
            EditarToolStripMenuItem.Click += new EventHandler(EditarToolStripMenuItem_Click);
            BorrarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem.Click += new EventHandler(BorrarToolStripMenuItem_Click);
            chk_secuencia = new CheckBox();
            lbl_total = new Label();
            txt_total = new TextBox();
            txt_impuestos = new TextBox();
            txt_impuestos.TextChanged += new EventHandler(txt_impuestos_TextChanged);
            lbl_impuestos = new Label();
            txt_subTotal = new TextBox();
            txt_subTotal.TextChanged += new EventHandler(txt_subTotal_TextChanged);
            lbl_subTotal = new Label();
            cmb_proveedor = new ComboBox();
            cmb_proveedor.KeyPress += new KeyPressEventHandler(cmb_cliente_KeyPress);
            dg_viewOC = new DataGridView();
            dg_viewOC.DoubleClick += new EventHandler(dg_view_proveedor_DoubleClick);
            dg_viewOC.DoubleClick += new EventHandler(dg_view_DoubleClick);
            dg_viewOC.MouseDown += new MouseEventHandler(dg_view_MouseDown);
            lbl_nOrdenCompra = new Label();
            lbl_ordenCompra = new Label();
            pic_searchProveedor = new PictureBox();
            pic_searchProveedor.Click += new EventHandler(pic_searchProveedor_Click);
            lbl_fechaRecepcion = new Label();
            lbl_fecha2 = new Label();
            chk_imprimir = new CheckBox();
            txt_nota = new TextBox();
            lbl_nota = new Label();
            txt_totalO = new TextBox();
            cms_enviado = new ContextMenuStrip(components);
            ModificarArtículoRecibidoToolStripMenuItem = new ToolStripMenuItem();
            ModificarArtículoRecibidoToolStripMenuItem.Click += new EventHandler(ModificarArtículoRecibidoToolStripMenuItem_Click);
            ModificarCantidadRecibidaToolStripMenuItem = new ToolStripMenuItem();
            ModificarCantidadRecibidaToolStripMenuItem.Click += new EventHandler(ModificarCantidadRecibidaToolStripMenuItem_Click);
            lbl_fecha3 = new Label();
            dtp_fechaComprobante = new DateTimePicker();
            cms_general.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewOC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).BeginInit();
            cms_enviado.SuspendLayout();
            SuspendLayout();
            // 
            // cmd_add_item
            // 
            cmd_add_item.Location = new Point(19, 406);
            cmd_add_item.Name = "cmd_add_item";
            cmd_add_item.Size = new Size(133, 22);
            cmd_add_item.TabIndex = 1;
            cmd_add_item.Text = "Añadir producto";
            cmd_add_item.UseVisualStyleBackColor = true;
            // 
            // lbl_fechaCarga
            // 
            lbl_fechaCarga.AutoSize = true;
            lbl_fechaCarga.Location = new Point(156, 25);
            lbl_fechaCarga.Name = "lbl_fechaCarga";
            lbl_fechaCarga.Size = new Size(50, 13);
            lbl_fechaCarga.TabIndex = 49;
            lbl_fechaCarga.Text = "%fecha%";
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(332, 683);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 5;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(232, 683);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 4;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(19, 148);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 41;
            lbl_proveedor.Text = "Proveedor";
            // 
            // lbl_fecha1
            // 
            lbl_fecha1.AutoSize = true;
            lbl_fecha1.Location = new Point(19, 25);
            lbl_fecha1.Name = "lbl_fecha1";
            lbl_fecha1.Size = new Size(85, 13);
            lbl_fecha1.TabIndex = 38;
            lbl_fecha1.Text = "Fecha de carga:";
            // 
            // cms_general
            // 
            cms_general.ImageScalingSize = new Size(28, 28);
            cms_general.Items.AddRange(new ToolStripItem[] { EditarToolStripMenuItem, BorrarToolStripMenuItem });
            cms_general.Name = "ContextMenuStrip1";
            cms_general.Size = new Size(107, 48);
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
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(19, 632);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 5;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_total.Location = new Point(447, 453);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(54, 20);
            lbl_total.TabIndex = 53;
            lbl_total.Text = "Total:";
            // 
            // txt_total
            // 
            txt_total.Location = new Point(528, 453);
            txt_total.Name = "txt_total";
            txt_total.ReadOnly = true;
            txt_total.Size = new Size(80, 20);
            txt_total.TabIndex = 4;
            // 
            // txt_impuestos
            // 
            txt_impuestos.Location = new Point(340, 453);
            txt_impuestos.Name = "txt_impuestos";
            txt_impuestos.ReadOnly = true;
            txt_impuestos.Size = new Size(80, 20);
            txt_impuestos.TabIndex = 60;
            // 
            // lbl_impuestos
            // 
            lbl_impuestos.AutoSize = true;
            lbl_impuestos.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_impuestos.Location = new Point(229, 453);
            lbl_impuestos.Name = "lbl_impuestos";
            lbl_impuestos.Size = new Size(84, 20);
            lbl_impuestos.TabIndex = 61;
            lbl_impuestos.Text = "Impuestos";
            // 
            // txt_subTotal
            // 
            txt_subTotal.Location = new Point(122, 453);
            txt_subTotal.Name = "txt_subTotal";
            txt_subTotal.ReadOnly = true;
            txt_subTotal.Size = new Size(80, 20);
            txt_subTotal.TabIndex = 633;
            // 
            // lbl_subTotal
            // 
            lbl_subTotal.AutoSize = true;
            lbl_subTotal.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_subTotal.Location = new Point(19, 453);
            lbl_subTotal.Name = "lbl_subTotal";
            lbl_subTotal.Size = new Size(73, 20);
            lbl_subTotal.TabIndex = 64;
            lbl_subTotal.Text = "Subtotal:";
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(81, 143);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(493, 21);
            cmb_proveedor.TabIndex = 635;
            // 
            // dg_viewOC
            // 
            dg_viewOC.AllowUserToAddRows = false;
            dg_viewOC.AllowUserToDeleteRows = false;
            dg_viewOC.AllowUserToOrderColumns = true;
            dg_viewOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dg_viewOC.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewOC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewOC.ContextMenuStrip = cms_general;
            dg_viewOC.Location = new Point(19, 174);
            dg_viewOC.MultiSelect = false;
            dg_viewOC.Name = "dg_viewOC";
            dg_viewOC.ReadOnly = true;
            dg_viewOC.RowHeadersVisible = false;
            dg_viewOC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewOC.Size = new Size(592, 216);
            dg_viewOC.TabIndex = 640;
            // 
            // lbl_nOrdenCompra
            // 
            lbl_nOrdenCompra.AutoSize = true;
            lbl_nOrdenCompra.Location = new Point(541, 25);
            lbl_nOrdenCompra.Name = "lbl_nOrdenCompra";
            lbl_nOrdenCompra.Size = new Size(65, 13);
            lbl_nOrdenCompra.TabIndex = 643;
            lbl_nOrdenCompra.Text = "%oCompra%";
            lbl_nOrdenCompra.Visible = false;
            // 
            // lbl_ordenCompra
            // 
            lbl_ordenCompra.AutoSize = true;
            lbl_ordenCompra.Location = new Point(448, 25);
            lbl_ordenCompra.Name = "lbl_ordenCompra";
            lbl_ordenCompra.Size = new Size(92, 13);
            lbl_ordenCompra.TabIndex = 642;
            lbl_ordenCompra.Text = "Orden de compra:";
            lbl_ordenCompra.Visible = false;
            // 
            // pic_searchProveedor
            // 
            pic_searchProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchProveedor.Location = new Point(591, 143);
            pic_searchProveedor.Name = "pic_searchProveedor";
            pic_searchProveedor.Size = new Size(22, 22);
            pic_searchProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchProveedor.TabIndex = 48;
            pic_searchProveedor.TabStop = false;
            // 
            // lbl_fechaRecepcion
            // 
            lbl_fechaRecepcion.AutoSize = true;
            lbl_fechaRecepcion.Location = new Point(156, 106);
            lbl_fechaRecepcion.Name = "lbl_fechaRecepcion";
            lbl_fechaRecepcion.Size = new Size(103, 13);
            lbl_fechaRecepcion.TabIndex = 662;
            lbl_fechaRecepcion.Text = "%fecha_recepcion%";
            lbl_fechaRecepcion.Visible = false;
            // 
            // lbl_fecha2
            // 
            lbl_fecha2.AutoSize = true;
            lbl_fecha2.Location = new Point(19, 106);
            lbl_fecha2.Name = "lbl_fecha2";
            lbl_fecha2.Size = new Size(105, 13);
            lbl_fecha2.TabIndex = 661;
            lbl_fecha2.Text = "Fecha de recepción:";
            // 
            // chk_imprimir
            // 
            chk_imprimir.AutoSize = true;
            chk_imprimir.Location = new Point(19, 590);
            chk_imprimir.Name = "chk_imprimir";
            chk_imprimir.Size = new Size(144, 17);
            chk_imprimir.TabIndex = 667;
            chk_imprimir.Text = "Imprimir orden de compra";
            chk_imprimir.UseVisualStyleBackColor = true;
            // 
            // txt_nota
            // 
            txt_nota.Location = new Point(62, 503);
            txt_nota.MaxLength = 91;
            txt_nota.Multiline = true;
            txt_nota.Name = "txt_nota";
            txt_nota.Size = new Size(546, 57);
            txt_nota.TabIndex = 665;
            // 
            // lbl_nota
            // 
            lbl_nota.AutoSize = true;
            lbl_nota.Location = new Point(19, 523);
            lbl_nota.Name = "lbl_nota";
            lbl_nota.Size = new Size(38, 13);
            lbl_nota.TabIndex = 666;
            lbl_nota.Text = "Notas:";
            // 
            // txt_totalO
            // 
            txt_totalO.Location = new Point(283, 408);
            txt_totalO.Name = "txt_totalO";
            txt_totalO.ReadOnly = true;
            txt_totalO.Size = new Size(166, 20);
            txt_totalO.TabIndex = 668;
            txt_totalO.Visible = false;
            // 
            // cms_enviado
            // 
            cms_enviado.Items.AddRange(new ToolStripItem[] { ModificarArtículoRecibidoToolStripMenuItem, ModificarCantidadRecibidaToolStripMenuItem });
            cms_enviado.Name = "cms2";
            cms_enviado.Size = new Size(220, 48);
            // 
            // ModificarArtículoRecibidoToolStripMenuItem
            // 
            ModificarArtículoRecibidoToolStripMenuItem.Name = "ModificarArtículoRecibidoToolStripMenuItem";
            ModificarArtículoRecibidoToolStripMenuItem.Size = new Size(219, 22);
            ModificarArtículoRecibidoToolStripMenuItem.Text = "Modificar artículo recibido";
            // 
            // ModificarCantidadRecibidaToolStripMenuItem
            // 
            ModificarCantidadRecibidaToolStripMenuItem.Name = "ModificarCantidadRecibidaToolStripMenuItem";
            ModificarCantidadRecibidaToolStripMenuItem.Size = new Size(219, 22);
            ModificarCantidadRecibidaToolStripMenuItem.Text = "Modificar cantidad recibida";
            // 
            // lbl_fecha3
            // 
            lbl_fecha3.AutoSize = true;
            lbl_fecha3.Location = new Point(16, 64);
            lbl_fecha3.Name = "lbl_fecha3";
            lbl_fecha3.Size = new Size(122, 13);
            lbl_fecha3.TabIndex = 669;
            lbl_fecha3.Text = "Fecha del comprobante:";
            // 
            // dtp_fechaComprobante
            // 
            dtp_fechaComprobante.Location = new Point(159, 58);
            dtp_fechaComprobante.Name = "dtp_fechaComprobante";
            dtp_fechaComprobante.Size = new Size(217, 20);
            dtp_fechaComprobante.TabIndex = 670;
            // 
            // add_ordenCompra
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmd_exit;
            ClientSize = new Size(628, 734);
            ControlBox = false;
            Controls.Add(dtp_fechaComprobante);
            Controls.Add(lbl_fecha3);
            Controls.Add(txt_totalO);
            Controls.Add(chk_imprimir);
            Controls.Add(txt_nota);
            Controls.Add(lbl_nota);
            Controls.Add(lbl_fechaRecepcion);
            Controls.Add(lbl_fecha2);
            Controls.Add(lbl_nOrdenCompra);
            Controls.Add(lbl_ordenCompra);
            Controls.Add(dg_viewOC);
            Controls.Add(cmb_proveedor);
            Controls.Add(txt_subTotal);
            Controls.Add(lbl_subTotal);
            Controls.Add(txt_impuestos);
            Controls.Add(lbl_impuestos);
            Controls.Add(txt_total);
            Controls.Add(lbl_total);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_add_item);
            Controls.Add(lbl_fechaCarga);
            Controls.Add(pic_searchProveedor);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_proveedor);
            Controls.Add(lbl_fecha1);
            Margin = new Padding(2);
            Name = "add_ordenCompra";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Carga de orden de compra";
            cms_general.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_viewOC).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).EndInit();
            cms_enviado.ResumeLayout(false);
            FormClosed += new FormClosedEventHandler(add_ordenCompra_FormClosed);
            KeyDown += new KeyEventHandler(add_ordenCompra_KeyDown);
            Load += new EventHandler(add_ordenCompra_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Button cmd_add_item;
        internal Label lbl_fechaCarga;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_proveedor;
        internal Label lbl_fecha1;
        internal CheckBox chk_secuencia;
        internal Label lbl_total;
        internal TextBox txt_total;
        internal ContextMenuStrip cms_general;
        internal ToolStripMenuItem EditarToolStripMenuItem;
        internal ToolStripMenuItem BorrarToolStripMenuItem;
        internal TextBox txt_impuestos;
        internal Label lbl_impuestos;
        internal TextBox txt_subTotal;
        internal Label lbl_subTotal;
        internal ComboBox cmb_proveedor;
        internal PictureBox pic_searchProveedor;
        internal DataGridView dg_viewOC;
        internal Label lbl_nOrdenCompra;
        internal Label lbl_ordenCompra;
        internal Label lbl_fechaRecepcion;
        internal Label lbl_fecha2;
        internal CheckBox chk_imprimir;
        internal TextBox txt_nota;
        internal Label lbl_nota;
        internal TextBox txt_totalO;
        internal ContextMenuStrip cms_enviado;
        internal ToolStripMenuItem ModificarArtículoRecibidoToolStripMenuItem;
        internal ToolStripMenuItem ModificarCantidadRecibidaToolStripMenuItem;
        internal Label lbl_fecha3;
        internal DateTimePicker dtp_fechaComprobante;
    }
}
