using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_comprobantes_compras : Form
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
            components = new System.ComponentModel.Container();
            lbl_tipoComprobante = new Label();
            cmb_tipoComprobante = new ComboBox();
            cmb_cc = new ComboBox();
            cmb_cc.SelectionChangeCommitted += new EventHandler(cmb_cc_SelectionChangeCommitted);
            lbl_ccp = new Label();
            cmb_proveedor = new ComboBox();
            cmb_proveedor.SelectionChangeCommitted += new EventHandler(cmb_proveedor_SelectionChangeCommitted);
            Label1 = new Label();
            dtp_fechaComprobanteCompra = new DateTimePicker();
            lbl_fechaCarga = new Label();
            lbl_fechaComprobante = new Label();
            lblFechaCarga = new Label();
            tbl_comprobantesCompras = new TabControl();
            productos = new TabPage();
            dg_viewItems = new DataGridView();
            dg_viewItems.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dg_viewItems_CellMouseDoubleClick);
            cms_general = new ContextMenuStrip(components);
            editar = new ToolStripMenuItem();
            eliminar = new ToolStripMenuItem();
            impuestos = new TabPage();
            dg_viewImpuestos = new DataGridView();
            conceptos = new TabPage();
            dg_viewConceptos = new DataGridView();
            notas = new TabPage();
            txt_notas = new TextBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_agregar = new Button();
            cmd_agregar.Click += new EventHandler(cmd_agregar_Click);
            lbl_totalItems = new Label();
            txt_totalItems = new TextBox();
            txt_totalConceptos = new TextBox();
            lbl_totalConceptos = new Label();
            txt_totalImpuestos = new TextBox();
            lbl_totalImpuestos = new Label();
            txt_totalFactura = new TextBox();
            lbl_totalFactura = new Label();
            lbl_moneda = new Label();
            cmb_moneda = new ComboBox();
            lbl_tasaCambio = new Label();
            txt_tasaCambio = new TextBox();
            cmb_condicionCompra = new ComboBox();
            lbl_condicionCompra = new Label();
            lbl_puntoVenta = new Label();
            txt_puntoVenta = new TextBox();
            lbl_numeroComprobante = new Label();
            txt_numeroComprobante = new TextBox();
            cmd_confirmar = new Button();
            cmd_confirmar.Click += new EventHandler(cmd_confirmar_Click);
            txt_CAE = new TextBox();
            lbl_CAE = new Label();
            cmd_editar = new Button();
            cmd_editar.Click += new EventHandler(cmd_editar_Click);
            pic_searchCondicionCompra = new PictureBox();
            pic_searchCondicionCompra.Click += new EventHandler(pic_searchCondicionCompra_Click);
            pic_searchCCProveedor = new PictureBox();
            pic_searchCCProveedor.Click += new EventHandler(pic_searchCCProveedor_Click);
            pic_searchProveedor = new PictureBox();
            pic_searchProveedor.Click += new EventHandler(pic_proveedorProveedor_Click);
            txt_totalOriginal = new TextBox();
            lbl_total = new Label();
            pic_searchTipoComprobante = new PictureBox();
            pic_searchTipoComprobante.Click += new EventHandler(pic_searchTipoComprobante_Click);
            tbl_comprobantesCompras.SuspendLayout();
            productos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewItems).BeginInit();
            cms_general.SuspendLayout();
            impuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewImpuestos).BeginInit();
            conceptos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewConceptos).BeginInit();
            notas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_searchCondicionCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchCCProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchTipoComprobante).BeginInit();
            SuspendLayout();
            // 
            // lbl_tipoComprobante
            // 
            lbl_tipoComprobante.AutoSize = true;
            lbl_tipoComprobante.Location = new Point(12, 125);
            lbl_tipoComprobante.Name = "lbl_tipoComprobante";
            lbl_tipoComprobante.Size = new Size(108, 13);
            lbl_tipoComprobante.TabIndex = 0;
            lbl_tipoComprobante.Text = "Tipo de comprobante";
            // 
            // cmb_tipoComprobante
            // 
            cmb_tipoComprobante.FormattingEnabled = true;
            cmb_tipoComprobante.Location = new Point(165, 123);
            cmb_tipoComprobante.Name = "cmb_tipoComprobante";
            cmb_tipoComprobante.Size = new Size(215, 21);
            cmb_tipoComprobante.TabIndex = 1;
            // 
            // cmb_cc
            // 
            cmb_cc.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_cc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_cc.FormattingEnabled = true;
            cmb_cc.Location = new Point(530, 45);
            cmb_cc.Name = "cmb_cc";
            cmb_cc.Size = new Size(281, 21);
            cmb_cc.TabIndex = 659;
            // 
            // lbl_ccp
            // 
            lbl_ccp.Location = new Point(428, 45);
            lbl_ccp.Name = "lbl_ccp";
            lbl_ccp.Size = new Size(98, 29);
            lbl_ccp.TabIndex = 663;
            lbl_ccp.Text = "Cuenta corriente del proveedor";
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(165, 45);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(215, 21);
            cmb_proveedor.TabIndex = 657;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(12, 51);
            Label1.Name = "Label1";
            Label1.Size = new Size(56, 13);
            Label1.TabIndex = 661;
            Label1.Text = "Proveedor";
            // 
            // dtp_fechaComprobanteCompra
            // 
            dtp_fechaComprobanteCompra.Format = DateTimePickerFormat.Short;
            dtp_fechaComprobanteCompra.Location = new Point(576, 7);
            dtp_fechaComprobanteCompra.Name = "dtp_fechaComprobanteCompra";
            dtp_fechaComprobanteCompra.Size = new Size(104, 20);
            dtp_fechaComprobanteCompra.TabIndex = 655;
            // 
            // lbl_fechaCarga
            // 
            lbl_fechaCarga.AutoSize = true;
            lbl_fechaCarga.Location = new Point(165, 13);
            lbl_fechaCarga.Name = "lbl_fechaCarga";
            lbl_fechaCarga.Size = new Size(50, 13);
            lbl_fechaCarga.TabIndex = 660;
            lbl_fechaCarga.Text = "%carga%";
            // 
            // lbl_fechaComprobante
            // 
            lbl_fechaComprobante.AutoSize = true;
            lbl_fechaComprobante.Location = new Point(430, 12);
            lbl_fechaComprobante.Name = "lbl_fechaComprobante";
            lbl_fechaComprobante.Size = new Size(120, 13);
            lbl_fechaComprobante.TabIndex = 658;
            lbl_fechaComprobante.Text = "Fecha de comprobante:";
            // 
            // lblFechaCarga
            // 
            lblFechaCarga.AutoSize = true;
            lblFechaCarga.Location = new Point(12, 13);
            lblFechaCarga.Name = "lblFechaCarga";
            lblFechaCarga.Size = new Size(85, 13);
            lblFechaCarga.TabIndex = 656;
            lblFechaCarga.Text = "Fecha de carga:";
            // 
            // tbl_comprobantesCompras
            // 
            tbl_comprobantesCompras.Controls.Add(productos);
            tbl_comprobantesCompras.Controls.Add(impuestos);
            tbl_comprobantesCompras.Controls.Add(conceptos);
            tbl_comprobantesCompras.Controls.Add(notas);
            tbl_comprobantesCompras.Enabled = false;
            tbl_comprobantesCompras.Location = new Point(10, 233);
            tbl_comprobantesCompras.Name = "tbl_comprobantesCompras";
            tbl_comprobantesCompras.SelectedIndex = 0;
            tbl_comprobantesCompras.Size = new Size(812, 291);
            tbl_comprobantesCompras.TabIndex = 665;
            // 
            // productos
            // 
            productos.BackColor = SystemColors.Control;
            productos.Controls.Add(dg_viewItems);
            productos.Location = new Point(4, 22);
            productos.Name = "productos";
            productos.Padding = new Padding(3);
            productos.Size = new Size(804, 265);
            productos.TabIndex = 0;
            productos.Text = "Productos";
            // 
            // dg_viewItems
            // 
            dg_viewItems.AllowUserToAddRows = false;
            dg_viewItems.AllowUserToDeleteRows = false;
            dg_viewItems.AllowUserToOrderColumns = true;
            dg_viewItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dg_viewItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewItems.ContextMenuStrip = cms_general;
            dg_viewItems.Location = new Point(6, 6);
            dg_viewItems.MultiSelect = false;
            dg_viewItems.Name = "dg_viewItems";
            dg_viewItems.ReadOnly = true;
            dg_viewItems.RowHeadersVisible = false;
            dg_viewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewItems.Size = new Size(791, 253);
            dg_viewItems.TabIndex = 694;
            // 
            // cms_general
            // 
            cms_general.Items.AddRange(new ToolStripItem[] { editar, eliminar });
            cms_general.Name = "cms_general";
            cms_general.Size = new Size(118, 48);
            // 
            // editar
            // 
            editar.Name = "editar";
            editar.Size = new Size(117, 22);
            editar.Text = "Editar";
            // 
            // eliminar
            // 
            eliminar.Name = "eliminar";
            eliminar.Size = new Size(117, 22);
            eliminar.Text = "Eliminar";
            // 
            // impuestos
            // 
            impuestos.BackColor = SystemColors.Control;
            impuestos.Controls.Add(dg_viewImpuestos);
            impuestos.Location = new Point(4, 22);
            impuestos.Name = "impuestos";
            impuestos.Padding = new Padding(3);
            impuestos.Size = new Size(804, 265);
            impuestos.TabIndex = 1;
            impuestos.Text = "Impuestos";
            // 
            // dg_viewImpuestos
            // 
            dg_viewImpuestos.AllowUserToAddRows = false;
            dg_viewImpuestos.AllowUserToDeleteRows = false;
            dg_viewImpuestos.AllowUserToOrderColumns = true;
            dg_viewImpuestos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dg_viewImpuestos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewImpuestos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewImpuestos.Location = new Point(6, 6);
            dg_viewImpuestos.MultiSelect = false;
            dg_viewImpuestos.Name = "dg_viewImpuestos";
            dg_viewImpuestos.ReadOnly = true;
            dg_viewImpuestos.RowHeadersVisible = false;
            dg_viewImpuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewImpuestos.Size = new Size(791, 253);
            dg_viewImpuestos.TabIndex = 694;
            // 
            // conceptos
            // 
            conceptos.BackColor = SystemColors.Control;
            conceptos.Controls.Add(dg_viewConceptos);
            conceptos.Location = new Point(4, 22);
            conceptos.Name = "conceptos";
            conceptos.Size = new Size(804, 265);
            conceptos.TabIndex = 2;
            conceptos.Text = "Conceptos";
            // 
            // dg_viewConceptos
            // 
            dg_viewConceptos.AllowUserToAddRows = false;
            dg_viewConceptos.AllowUserToDeleteRows = false;
            dg_viewConceptos.AllowUserToOrderColumns = true;
            dg_viewConceptos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dg_viewConceptos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewConceptos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewConceptos.Location = new Point(6, 6);
            dg_viewConceptos.MultiSelect = false;
            dg_viewConceptos.Name = "dg_viewConceptos";
            dg_viewConceptos.ReadOnly = true;
            dg_viewConceptos.RowHeadersVisible = false;
            dg_viewConceptos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewConceptos.Size = new Size(791, 253);
            dg_viewConceptos.TabIndex = 694;
            // 
            // notas
            // 
            notas.BackColor = SystemColors.Control;
            notas.Controls.Add(txt_notas);
            notas.Location = new Point(4, 22);
            notas.Name = "notas";
            notas.Size = new Size(804, 265);
            notas.TabIndex = 3;
            notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(6, 6);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(812, 254);
            txt_notas.TabIndex = 0;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(437, 686);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 667;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Enabled = false;
            cmd_ok.Location = new Point(339, 686);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 666;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_agregar
            // 
            cmd_agregar.Enabled = false;
            cmd_agregar.Location = new Point(10, 540);
            cmd_agregar.Name = "cmd_agregar";
            cmd_agregar.Size = new Size(75, 23);
            cmd_agregar.TabIndex = 668;
            cmd_agregar.Text = "Agregar";
            cmd_agregar.UseVisualStyleBackColor = true;
            // 
            // lbl_totalItems
            // 
            lbl_totalItems.AutoSize = true;
            lbl_totalItems.Location = new Point(10, 584);
            lbl_totalItems.Name = "lbl_totalItems";
            lbl_totalItems.Size = new Size(81, 13);
            lbl_totalItems.TabIndex = 669;
            lbl_totalItems.Text = "Total productos";
            // 
            // txt_totalItems
            // 
            txt_totalItems.Location = new Point(111, 581);
            txt_totalItems.Name = "txt_totalItems";
            txt_totalItems.ReadOnly = true;
            txt_totalItems.Size = new Size(128, 20);
            txt_totalItems.TabIndex = 670;
            // 
            // txt_totalConceptos
            // 
            txt_totalConceptos.Location = new Point(588, 577);
            txt_totalConceptos.Name = "txt_totalConceptos";
            txt_totalConceptos.ReadOnly = true;
            txt_totalConceptos.Size = new Size(128, 20);
            txt_totalConceptos.TabIndex = 672;
            // 
            // lbl_totalConceptos
            // 
            lbl_totalConceptos.AutoSize = true;
            lbl_totalConceptos.Location = new Point(490, 580);
            lbl_totalConceptos.Name = "lbl_totalConceptos";
            lbl_totalConceptos.Size = new Size(84, 13);
            lbl_totalConceptos.TabIndex = 671;
            lbl_totalConceptos.Text = "Total conceptos";
            // 
            // txt_totalImpuestos
            // 
            txt_totalImpuestos.Location = new Point(348, 581);
            txt_totalImpuestos.Name = "txt_totalImpuestos";
            txt_totalImpuestos.ReadOnly = true;
            txt_totalImpuestos.Size = new Size(128, 20);
            txt_totalImpuestos.TabIndex = 674;
            // 
            // lbl_totalImpuestos
            // 
            lbl_totalImpuestos.AutoSize = true;
            lbl_totalImpuestos.Location = new Point(253, 584);
            lbl_totalImpuestos.Name = "lbl_totalImpuestos";
            lbl_totalImpuestos.Size = new Size(81, 13);
            lbl_totalImpuestos.TabIndex = 673;
            lbl_totalImpuestos.Text = "Total impuestos";
            // 
            // txt_totalFactura
            // 
            txt_totalFactura.Location = new Point(111, 625);
            txt_totalFactura.Name = "txt_totalFactura";
            txt_totalFactura.ReadOnly = true;
            txt_totalFactura.Size = new Size(128, 20);
            txt_totalFactura.TabIndex = 676;
            // 
            // lbl_totalFactura
            // 
            lbl_totalFactura.AutoSize = true;
            lbl_totalFactura.Location = new Point(10, 625);
            lbl_totalFactura.Name = "lbl_totalFactura";
            lbl_totalFactura.Size = new Size(67, 13);
            lbl_totalFactura.TabIndex = 675;
            lbl_totalFactura.Text = "Total factura";
            // 
            // lbl_moneda
            // 
            lbl_moneda.AutoSize = true;
            lbl_moneda.Location = new Point(428, 92);
            lbl_moneda.Name = "lbl_moneda";
            lbl_moneda.Size = new Size(46, 13);
            lbl_moneda.TabIndex = 678;
            lbl_moneda.Text = "Moneda";
            // 
            // cmb_moneda
            // 
            cmb_moneda.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_moneda.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_moneda.Enabled = false;
            cmb_moneda.FormattingEnabled = true;
            cmb_moneda.Location = new Point(530, 90);
            cmb_moneda.Name = "cmb_moneda";
            cmb_moneda.Size = new Size(54, 21);
            cmb_moneda.TabIndex = 679;
            // 
            // lbl_tasaCambio
            // 
            lbl_tasaCambio.Location = new Point(611, 90);
            lbl_tasaCambio.Name = "lbl_tasaCambio";
            lbl_tasaCambio.Size = new Size(57, 31);
            lbl_tasaCambio.TabIndex = 680;
            lbl_tasaCambio.Text = "Tasa de cambio";
            // 
            // txt_tasaCambio
            // 
            txt_tasaCambio.Location = new Point(683, 87);
            txt_tasaCambio.Name = "txt_tasaCambio";
            txt_tasaCambio.Size = new Size(128, 20);
            txt_tasaCambio.TabIndex = 681;
            // 
            // cmb_condicionCompra
            // 
            cmb_condicionCompra.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_condicionCompra.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_condicionCompra.FormattingEnabled = true;
            cmb_condicionCompra.Location = new Point(165, 84);
            cmb_condicionCompra.Name = "cmb_condicionCompra";
            cmb_condicionCompra.Size = new Size(215, 21);
            cmb_condicionCompra.TabIndex = 682;
            // 
            // lbl_condicionCompra
            // 
            lbl_condicionCompra.AutoSize = true;
            lbl_condicionCompra.Location = new Point(12, 88);
            lbl_condicionCompra.Name = "lbl_condicionCompra";
            lbl_condicionCompra.Size = new Size(107, 13);
            lbl_condicionCompra.TabIndex = 683;
            lbl_condicionCompra.Text = "Condición de compra";
            // 
            // lbl_puntoVenta
            // 
            lbl_puntoVenta.AutoSize = true;
            lbl_puntoVenta.Location = new Point(428, 139);
            lbl_puntoVenta.Name = "lbl_puntoVenta";
            lbl_puntoVenta.Size = new Size(80, 13);
            lbl_puntoVenta.TabIndex = 685;
            lbl_puntoVenta.Text = "Punto de venta";
            // 
            // txt_puntoVenta
            // 
            txt_puntoVenta.Location = new Point(530, 136);
            txt_puntoVenta.Name = "txt_puntoVenta";
            txt_puntoVenta.Size = new Size(54, 20);
            txt_puntoVenta.TabIndex = 686;
            // 
            // lbl_numeroComprobante
            // 
            lbl_numeroComprobante.Location = new Point(611, 131);
            lbl_numeroComprobante.Name = "lbl_numeroComprobante";
            lbl_numeroComprobante.Size = new Size(69, 29);
            lbl_numeroComprobante.TabIndex = 687;
            lbl_numeroComprobante.Text = "Número de comprobante";
            // 
            // txt_numeroComprobante
            // 
            txt_numeroComprobante.Location = new Point(683, 135);
            txt_numeroComprobante.Name = "txt_numeroComprobante";
            txt_numeroComprobante.Size = new Size(128, 20);
            txt_numeroComprobante.TabIndex = 688;
            // 
            // cmd_confirmar
            // 
            cmd_confirmar.Location = new Point(10, 195);
            cmd_confirmar.Name = "cmd_confirmar";
            cmd_confirmar.Size = new Size(75, 23);
            cmd_confirmar.TabIndex = 689;
            cmd_confirmar.Text = "Confirmar";
            cmd_confirmar.UseVisualStyleBackColor = true;
            // 
            // txt_CAE
            // 
            txt_CAE.Location = new Point(165, 162);
            txt_CAE.Name = "txt_CAE";
            txt_CAE.Size = new Size(215, 20);
            txt_CAE.TabIndex = 691;
            // 
            // lbl_CAE
            // 
            lbl_CAE.AutoSize = true;
            lbl_CAE.Location = new Point(12, 162);
            lbl_CAE.Name = "lbl_CAE";
            lbl_CAE.Size = new Size(50, 13);
            lbl_CAE.TabIndex = 690;
            lbl_CAE.Text = "CAE/CAI";
            // 
            // cmd_editar
            // 
            cmd_editar.Enabled = false;
            cmd_editar.Location = new Point(111, 195);
            cmd_editar.Name = "cmd_editar";
            cmd_editar.Size = new Size(75, 23);
            cmd_editar.TabIndex = 692;
            cmd_editar.Text = "Editar";
            cmd_editar.UseVisualStyleBackColor = true;
            // 
            // pic_searchCondicionCompra
            // 
            pic_searchCondicionCompra.Image = My.Resources.Resources.iconoLupa;
            pic_searchCondicionCompra.Location = new Point(386, 86);
            pic_searchCondicionCompra.Name = "pic_searchCondicionCompra";
            pic_searchCondicionCompra.Size = new Size(22, 22);
            pic_searchCondicionCompra.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCondicionCompra.TabIndex = 684;
            pic_searchCondicionCompra.TabStop = false;
            // 
            // pic_searchCCProveedor
            // 
            pic_searchCCProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchCCProveedor.Location = new Point(817, 45);
            pic_searchCCProveedor.Name = "pic_searchCCProveedor";
            pic_searchCCProveedor.Size = new Size(22, 22);
            pic_searchCCProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCCProveedor.TabIndex = 664;
            pic_searchCCProveedor.TabStop = false;
            // 
            // pic_searchProveedor
            // 
            pic_searchProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchProveedor.Location = new Point(386, 44);
            pic_searchProveedor.Name = "pic_searchProveedor";
            pic_searchProveedor.Size = new Size(22, 22);
            pic_searchProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchProveedor.TabIndex = 662;
            pic_searchProveedor.TabStop = false;
            // 
            // txt_totalOriginal
            // 
            txt_totalOriginal.Location = new Point(530, 175);
            txt_totalOriginal.Name = "txt_totalOriginal";
            txt_totalOriginal.Size = new Size(215, 20);
            txt_totalOriginal.TabIndex = 694;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Location = new Point(430, 175);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(31, 13);
            lbl_total.TabIndex = 693;
            lbl_total.Text = "Total";
            // 
            // pic_searchTipoComprobante
            // 
            pic_searchTipoComprobante.Image = My.Resources.Resources.iconoLupa;
            pic_searchTipoComprobante.Location = new Point(386, 122);
            pic_searchTipoComprobante.Name = "pic_searchTipoComprobante";
            pic_searchTipoComprobante.Size = new Size(22, 22);
            pic_searchTipoComprobante.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchTipoComprobante.TabIndex = 695;
            pic_searchTipoComprobante.TabStop = false;
            // 
            // add_comprobantes_compras
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 741);
            Controls.Add(pic_searchTipoComprobante);
            Controls.Add(txt_totalOriginal);
            Controls.Add(lbl_total);
            Controls.Add(cmd_editar);
            Controls.Add(txt_CAE);
            Controls.Add(lbl_CAE);
            Controls.Add(cmd_confirmar);
            Controls.Add(txt_numeroComprobante);
            Controls.Add(lbl_numeroComprobante);
            Controls.Add(txt_puntoVenta);
            Controls.Add(lbl_puntoVenta);
            Controls.Add(cmb_condicionCompra);
            Controls.Add(pic_searchCondicionCompra);
            Controls.Add(lbl_condicionCompra);
            Controls.Add(txt_tasaCambio);
            Controls.Add(lbl_tasaCambio);
            Controls.Add(cmb_moneda);
            Controls.Add(lbl_moneda);
            Controls.Add(txt_totalFactura);
            Controls.Add(lbl_totalFactura);
            Controls.Add(txt_totalImpuestos);
            Controls.Add(lbl_totalImpuestos);
            Controls.Add(txt_totalConceptos);
            Controls.Add(lbl_totalConceptos);
            Controls.Add(txt_totalItems);
            Controls.Add(lbl_totalItems);
            Controls.Add(cmd_agregar);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(tbl_comprobantesCompras);
            Controls.Add(cmb_cc);
            Controls.Add(pic_searchCCProveedor);
            Controls.Add(lbl_ccp);
            Controls.Add(cmb_proveedor);
            Controls.Add(pic_searchProveedor);
            Controls.Add(Label1);
            Controls.Add(dtp_fechaComprobanteCompra);
            Controls.Add(lbl_fechaCarga);
            Controls.Add(lbl_fechaComprobante);
            Controls.Add(lblFechaCarga);
            Controls.Add(cmb_tipoComprobante);
            Controls.Add(lbl_tipoComprobante);
            Name = "add_comprobantes_compras";
            StartPosition = FormStartPosition.CenterParent;
            Text = " ";
            tbl_comprobantesCompras.ResumeLayout(false);
            productos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_viewItems).EndInit();
            cms_general.ResumeLayout(false);
            impuestos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_viewImpuestos).EndInit();
            conceptos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_viewConceptos).EndInit();
            notas.ResumeLayout(false);
            notas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_searchCondicionCompra).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchCCProveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchTipoComprobante).EndInit();
            Load += new EventHandler(add_comprobantes_compras_Load);
            FormClosed += new FormClosedEventHandler(add_comprobantes_compras_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_tipoComprobante;
        internal ComboBox cmb_tipoComprobante;
        internal ComboBox cmb_cc;
        internal PictureBox pic_searchCCProveedor;
        internal Label lbl_ccp;
        internal ComboBox cmb_proveedor;
        internal PictureBox pic_searchProveedor;
        internal Label Label1;
        internal DateTimePicker dtp_fechaComprobanteCompra;
        internal Label lbl_fechaCarga;
        internal Label lbl_fechaComprobante;
        internal Label lblFechaCarga;
        internal TabControl tbl_comprobantesCompras;
        internal TabPage productos;
        internal TabPage impuestos;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal TabPage conceptos;
        internal Button cmd_agregar;
        internal Label lbl_totalItems;
        internal TextBox txt_totalItems;
        internal TextBox txt_totalConceptos;
        internal Label lbl_totalConceptos;
        internal TextBox txt_totalImpuestos;
        internal Label lbl_totalImpuestos;
        internal TextBox txt_totalFactura;
        internal Label lbl_totalFactura;
        internal Label lbl_moneda;
        internal ComboBox cmb_moneda;
        internal Label lbl_tasaCambio;
        internal TextBox txt_tasaCambio;
        internal ComboBox cmb_condicionCompra;
        internal PictureBox pic_searchCondicionCompra;
        internal Label lbl_condicionCompra;
        internal Label lbl_puntoVenta;
        internal TextBox txt_puntoVenta;
        internal Label lbl_numeroComprobante;
        internal TextBox txt_numeroComprobante;
        internal Button cmd_confirmar;
        internal TabPage notas;
        internal TextBox txt_notas;
        internal TextBox txt_CAE;
        internal Label lbl_CAE;
        internal Button cmd_editar;
        internal DataGridView dg_viewItems;
        internal DataGridView dg_viewImpuestos;
        internal DataGridView dg_viewConceptos;
        internal ContextMenuStrip cms_general;
        internal ToolStripMenuItem editar;
        internal ToolStripMenuItem eliminar;
        internal TextBox txt_totalOriginal;
        internal Label lbl_total;
        internal PictureBox pic_searchTipoComprobante;
    }
}


