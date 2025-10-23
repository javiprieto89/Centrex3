using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_pedido : Form
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
            lbl_date = new Label();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_cliente = new Label();
            lbl_fecha = new Label();
            ContextMenuStrip1 = new ContextMenuStrip(components);
            EditarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem.Click += new EventHandler(BorrarToolStripMenuItem_Click);
            RecargarPrecioToolStripMenuItem = new ToolStripMenuItem();
            chk_secuencia = new CheckBox();
            lbl_total = new Label();
            txt_total = new TextBox();
            cmd_recargaprecios = new Button();
            lbl_nota1 = new Label();
            txt_nota1 = new TextBox();
            txt_nota2 = new TextBox();
            lbl_nota2 = new Label();
            txt_impuestos = new TextBox();
            lbl_impuestos = new Label();
            cmd_emitir = new Button();
            txt_subTotal = new TextBox();
            lbl_subTotal = new Label();
            txt_totalO = new TextBox();
            lbl_markup = new Label();
            txt_markup = new TextBox();
            cmb_clientes = new ComboBox();
            chk_remitos = new CheckBox();
            cmb_comprobantes = new ComboBox();
            lbl_comprobante = new Label();
            chk_presupuesto = new CheckBox();
            lbl_order = new Label();
            lbl_pedido = new Label();
            txt_totalDescuentos = new TextBox();
            lbl_totalDescuentos = new Label();
            lbl_noTaxNumber = new Label();
            chk_esTest = new CheckBox();
            tt_descuento = new ToolTip(components);
            lbl_noMarkupNoPresupuesto = new Label();
            cmb_cc = new ComboBox();
            lbl_cc = new Label();
            dg_items = new DataGridView();
            lbl_items = new Label();
            cmd_add_item = new Button();
            cmd_add_item.Click += new EventHandler(cmd_add_item_Click);
            cmd_add_descuento = new Button();
            cmd_addItemManual = new Button();
            cmd_first = new Button();
            cmd_prev = new Button();
            cmd_next = new Button();
            cmd_last = new Button();
            txt_nPage = new TextBox();
            cmd_go = new Button();
            psearch_cc = new PictureBox();
            pic_searchComprobante = new PictureBox();
            pic_searchCliente = new PictureBox();
            lbl_avisoAFIP_NC_ND = new Label();
            cmb_seleccionarComprobanteAnulación = new Button();
            lbl_comprobanteAsociado = new Label();
            txt_comprobanteAsociado = new TextBox();
            ContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_items).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_cc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchComprobante).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchCliente).BeginInit();
            SuspendLayout();
            // 
            // lbl_date
            // 
            lbl_date.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lbl_date.AutoSize = true;
            lbl_date.Location = new Point(77, 14);
            lbl_date.Margin = new Padding(4, 0, 4, 0);
            lbl_date.Name = "lbl_date";
            lbl_date.Size = new Size(67, 17);
            lbl_date.TabIndex = 49;
            lbl_date.Text = "%fecha%";
            // 
            // cmd_exit
            // 
            cmd_exit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(35, 823);
            cmd_exit.Margin = new Padding(4, 4, 4, 4);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(100, 28);
            cmd_exit.TabIndex = 11;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_ok.Location = new Point(35, 714);
            cmd_ok.Margin = new Padding(4, 4, 4, 4);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(100, 28);
            cmd_ok.TabIndex = 10;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_cliente
            // 
            lbl_cliente.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lbl_cliente.AutoSize = true;
            lbl_cliente.Location = new Point(23, 58);
            lbl_cliente.Margin = new Padding(4, 0, 4, 0);
            lbl_cliente.Name = "lbl_cliente";
            lbl_cliente.Size = new Size(51, 17);
            lbl_cliente.TabIndex = 41;
            lbl_cliente.Text = "Cliente";
            // 
            // lbl_fecha
            // 
            lbl_fecha.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lbl_fecha.AutoSize = true;
            lbl_fecha.Location = new Point(16, 14);
            lbl_fecha.Margin = new Padding(4, 0, 4, 0);
            lbl_fecha.Name = "lbl_fecha";
            lbl_fecha.Size = new Size(51, 17);
            lbl_fecha.TabIndex = 38;
            lbl_fecha.Text = "Fecha:";
            // 
            // ContextMenuStrip1
            // 
            ContextMenuStrip1.ImageScalingSize = new Size(28, 28);
            ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { EditarToolStripMenuItem, BorrarToolStripMenuItem, RecargarPrecioToolStripMenuItem });
            ContextMenuStrip1.Name = "ContextMenuStrip1";
            ContextMenuStrip1.Size = new Size(184, 76);
            // 
            // EditarToolStripMenuItem
            // 
            EditarToolStripMenuItem.Name = "EditarToolStripMenuItem";
            EditarToolStripMenuItem.Size = new Size(183, 24);
            EditarToolStripMenuItem.Text = "Editar";
            // 
            // BorrarToolStripMenuItem
            // 
            BorrarToolStripMenuItem.Name = "BorrarToolStripMenuItem";
            BorrarToolStripMenuItem.Size = new Size(183, 24);
            BorrarToolStripMenuItem.Text = "Borrar";
            // 
            // RecargarPrecioToolStripMenuItem
            // 
            RecargarPrecioToolStripMenuItem.Name = "RecargarPrecioToolStripMenuItem";
            RecargarPrecioToolStripMenuItem.Size = new Size(183, 24);
            RecargarPrecioToolStripMenuItem.Text = "Recargar precio";
            // 
            // chk_secuencia
            // 
            chk_secuencia.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(35, 884);
            chk_secuencia.Margin = new Padding(4, 4, 4, 4);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(139, 21);
            chk_secuencia.TabIndex = 5;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // lbl_total
            // 
            lbl_total.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_total.Location = new Point(451, 656);
            lbl_total.Margin = new Padding(4, 0, 4, 0);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(68, 25);
            lbl_total.TabIndex = 53;
            lbl_total.Text = "Total:";
            // 
            // txt_total
            // 
            txt_total.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_total.Location = new Point(559, 656);
            txt_total.Margin = new Padding(4, 4, 4, 4);
            txt_total.Name = "txt_total";
            txt_total.ReadOnly = true;
            txt_total.Size = new Size(231, 22);
            txt_total.TabIndex = 4;
            // 
            // cmd_recargaprecios
            // 
            cmd_recargaprecios.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmd_recargaprecios.DialogResult = DialogResult.Cancel;
            cmd_recargaprecios.Location = new Point(988, 654);
            cmd_recargaprecios.Margin = new Padding(4, 4, 4, 4);
            cmd_recargaprecios.Name = "cmd_recargaprecios";
            cmd_recargaprecios.Size = new Size(221, 27);
            cmd_recargaprecios.TabIndex = 12;
            cmd_recargaprecios.Text = "Recargar precios";
            cmd_recargaprecios.UseVisualStyleBackColor = true;
            // 
            // lbl_nota1
            // 
            lbl_nota1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_nota1.AutoSize = true;
            lbl_nota1.Location = new Point(831, 196);
            lbl_nota1.Margin = new Padding(4, 0, 4, 0);
            lbl_nota1.Name = "lbl_nota1";
            lbl_nota1.Size = new Size(133, 17);
            lbl_nota1.TabIndex = 55;
            lbl_nota1.Text = "Condición de venta:";
            // 
            // txt_nota1
            // 
            txt_nota1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nota1.Location = new Point(835, 231);
            txt_nota1.Margin = new Padding(4, 4, 4, 4);
            txt_nota1.MaxLength = 85;
            txt_nota1.Multiline = true;
            txt_nota1.Name = "txt_nota1";
            txt_nota1.Size = new Size(353, 70);
            txt_nota1.TabIndex = 6;
            // 
            // txt_nota2
            // 
            txt_nota2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nota2.Location = new Point(835, 389);
            txt_nota2.Margin = new Padding(4, 4, 4, 4);
            txt_nota2.MaxLength = 91;
            txt_nota2.Multiline = true;
            txt_nota2.Name = "txt_nota2";
            txt_nota2.Size = new Size(353, 69);
            txt_nota2.TabIndex = 7;
            // 
            // lbl_nota2
            // 
            lbl_nota2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_nota2.AutoSize = true;
            lbl_nota2.Location = new Point(831, 356);
            lbl_nota2.Margin = new Padding(4, 0, 4, 0);
            lbl_nota2.Name = "lbl_nota2";
            lbl_nota2.Size = new Size(107, 17);
            lbl_nota2.TabIndex = 57;
            lbl_nota2.Text = "Observaciones:";
            // 
            // txt_impuestos
            // 
            txt_impuestos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_impuestos.Location = new Point(685, 587);
            txt_impuestos.Margin = new Padding(4, 4, 4, 4);
            txt_impuestos.Name = "txt_impuestos";
            txt_impuestos.ReadOnly = true;
            txt_impuestos.Size = new Size(105, 22);
            txt_impuestos.TabIndex = 60;
            // 
            // lbl_impuestos
            // 
            lbl_impuestos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_impuestos.AutoSize = true;
            lbl_impuestos.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_impuestos.Location = new Point(545, 587);
            lbl_impuestos.Margin = new Padding(4, 0, 4, 0);
            lbl_impuestos.Name = "lbl_impuestos";
            lbl_impuestos.Size = new Size(102, 25);
            lbl_impuestos.TabIndex = 61;
            lbl_impuestos.Text = "Impuestos";
            // 
            // cmd_emitir
            // 
            cmd_emitir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_emitir.Location = new Point(35, 768);
            cmd_emitir.Margin = new Padding(4, 4, 4, 4);
            cmd_emitir.Name = "cmd_emitir";
            cmd_emitir.Size = new Size(100, 28);
            cmd_emitir.TabIndex = 9;
            cmd_emitir.Text = "Emitir";
            cmd_emitir.UseVisualStyleBackColor = true;
            // 
            // txt_subTotal
            // 
            txt_subTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_subTotal.Location = new Point(411, 587);
            txt_subTotal.Margin = new Padding(4, 4, 4, 4);
            txt_subTotal.Name = "txt_subTotal";
            txt_subTotal.ReadOnly = true;
            txt_subTotal.Size = new Size(105, 22);
            txt_subTotal.TabIndex = 633;
            // 
            // lbl_subTotal
            // 
            lbl_subTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_subTotal.AutoSize = true;
            lbl_subTotal.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_subTotal.Location = new Point(285, 587);
            lbl_subTotal.Margin = new Padding(4, 0, 4, 0);
            lbl_subTotal.Name = "lbl_subTotal";
            lbl_subTotal.Size = new Size(90, 25);
            lbl_subTotal.TabIndex = 64;
            lbl_subTotal.Text = "Subtotal:";
            // 
            // txt_totalO
            // 
            txt_totalO.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txt_totalO.Location = new Point(988, 688);
            txt_totalO.Margin = new Padding(4, 4, 4, 4);
            txt_totalO.Name = "txt_totalO";
            txt_totalO.ReadOnly = true;
            txt_totalO.Size = new Size(220, 22);
            txt_totalO.TabIndex = 68;
            txt_totalO.Visible = false;
            // 
            // lbl_markup
            // 
            lbl_markup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_markup.AutoSize = true;
            lbl_markup.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_markup.Location = new Point(29, 587);
            lbl_markup.Margin = new Padding(4, 0, 4, 0);
            lbl_markup.Name = "lbl_markup";
            lbl_markup.Size = new Size(89, 25);
            lbl_markup.TabIndex = 69;
            lbl_markup.Text = "Markup: ";
            // 
            // txt_markup
            // 
            txt_markup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_markup.Location = new Point(151, 587);
            txt_markup.Margin = new Padding(4, 4, 4, 4);
            txt_markup.Name = "txt_markup";
            txt_markup.Size = new Size(105, 22);
            txt_markup.TabIndex = 8;
            txt_markup.Text = "0";
            // 
            // cmb_clientes
            // 
            cmb_clientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            cmb_clientes.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_clientes.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_clientes.FormattingEnabled = true;
            cmb_clientes.Location = new Point(124, 52);
            cmb_clientes.Margin = new Padding(4, 4, 4, 4);
            cmb_clientes.Name = "cmb_clientes";
            cmb_clientes.Size = new Size(456, 24);
            cmb_clientes.TabIndex = 0;
            // 
            // chk_remitos
            // 
            chk_remitos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            chk_remitos.AutoSize = true;
            chk_remitos.Checked = true;
            chk_remitos.CheckState = CheckState.Checked;
            chk_remitos.Location = new Point(817, 106);
            chk_remitos.Margin = new Padding(4, 4, 4, 4);
            chk_remitos.Name = "chk_remitos";
            chk_remitos.Size = new Size(115, 21);
            chk_remitos.TabIndex = 636;
            chk_remitos.Text = "Emitir remitos";
            chk_remitos.UseVisualStyleBackColor = true;
            // 
            // cmb_comprobantes
            // 
            cmb_comprobantes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            cmb_comprobantes.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_comprobantes.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_comprobantes.FormattingEnabled = true;
            cmb_comprobantes.Location = new Point(124, 101);
            cmb_comprobantes.Margin = new Padding(4, 4, 4, 4);
            cmb_comprobantes.Name = "cmb_comprobantes";
            cmb_comprobantes.Size = new Size(456, 24);
            cmb_comprobantes.TabIndex = 2;
            // 
            // lbl_comprobante
            // 
            lbl_comprobante.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lbl_comprobante.AutoSize = true;
            lbl_comprobante.Location = new Point(23, 107);
            lbl_comprobante.Margin = new Padding(4, 0, 4, 0);
            lbl_comprobante.Name = "lbl_comprobante";
            lbl_comprobante.Size = new Size(93, 17);
            lbl_comprobante.TabIndex = 637;
            lbl_comprobante.Text = "Comprobante";
            // 
            // chk_presupuesto
            // 
            chk_presupuesto.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            chk_presupuesto.AutoSize = true;
            chk_presupuesto.Location = new Point(659, 106);
            chk_presupuesto.Margin = new Padding(4, 4, 4, 4);
            chk_presupuesto.Name = "chk_presupuesto";
            chk_presupuesto.Size = new Size(149, 21);
            chk_presupuesto.TabIndex = 8;
            chk_presupuesto.Text = "Es un presupuesto";
            chk_presupuesto.UseVisualStyleBackColor = true;
            chk_presupuesto.Visible = false;
            // 
            // lbl_order
            // 
            lbl_order.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lbl_order.AutoSize = true;
            lbl_order.Location = new Point(259, 14);
            lbl_order.Margin = new Padding(4, 0, 4, 0);
            lbl_order.Name = "lbl_order";
            lbl_order.Size = new Size(75, 17);
            lbl_order.TabIndex = 643;
            lbl_order.Text = "%pedido%";
            lbl_order.Visible = false;
            // 
            // lbl_pedido
            // 
            lbl_pedido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            lbl_pedido.AutoSize = true;
            lbl_pedido.Location = new Point(197, 14);
            lbl_pedido.Margin = new Padding(4, 0, 4, 0);
            lbl_pedido.Name = "lbl_pedido";
            lbl_pedido.Size = new Size(56, 17);
            lbl_pedido.TabIndex = 642;
            lbl_pedido.Text = "Pedido:";
            lbl_pedido.Visible = false;
            // 
            // txt_totalDescuentos
            // 
            txt_totalDescuentos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_totalDescuentos.Location = new Point(240, 654);
            txt_totalDescuentos.Margin = new Padding(4, 4, 4, 4);
            txt_totalDescuentos.Name = "txt_totalDescuentos";
            txt_totalDescuentos.ReadOnly = true;
            txt_totalDescuentos.Size = new Size(173, 22);
            txt_totalDescuentos.TabIndex = 644;
            // 
            // lbl_totalDescuentos
            // 
            lbl_totalDescuentos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_totalDescuentos.AutoSize = true;
            lbl_totalDescuentos.Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_totalDescuentos.Location = new Point(29, 656);
            lbl_totalDescuentos.Margin = new Padding(4, 0, 4, 0);
            lbl_totalDescuentos.Name = "lbl_totalDescuentos";
            lbl_totalDescuentos.Size = new Size(162, 25);
            lbl_totalDescuentos.TabIndex = 645;
            lbl_totalDescuentos.Text = "Total descuentos";
            // 
            // lbl_noTaxNumber
            // 
            lbl_noTaxNumber.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_noTaxNumber.AutoSize = true;
            lbl_noTaxNumber.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_noTaxNumber.ForeColor = Color.Red;
            lbl_noTaxNumber.Location = new Point(307, 863);
            lbl_noTaxNumber.Margin = new Padding(4, 0, 4, 0);
            lbl_noTaxNumber.Name = "lbl_noTaxNumber";
            lbl_noTaxNumber.Size = new Size(649, 17);
            lbl_noTaxNumber.TabIndex = 646;
            lbl_noTaxNumber.Text = "El cliente no tiene cargado el CUIT por lo cual no se pueden emitir documentos of" + "iciales";
            lbl_noTaxNumber.Visible = false;
            // 
            // chk_esTest
            // 
            chk_esTest.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            chk_esTest.AutoSize = true;
            chk_esTest.Location = new Point(1096, 106);
            chk_esTest.Margin = new Padding(4, 4, 4, 4);
            chk_esTest.Name = "chk_esTest";
            chk_esTest.Size = new Size(93, 21);
            chk_esTest.TabIndex = 647;
            chk_esTest.Text = "Es un test";
            chk_esTest.UseVisualStyleBackColor = true;
            // 
            // tt_descuento
            // 
            tt_descuento.Active = false;
            // 
            // lbl_noMarkupNoPresupuesto
            // 
            lbl_noMarkupNoPresupuesto.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_noMarkupNoPresupuesto.AutoSize = true;
            lbl_noMarkupNoPresupuesto.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_noMarkupNoPresupuesto.ForeColor = Color.Red;
            lbl_noMarkupNoPresupuesto.Location = new Point(359, 836);
            lbl_noMarkupNoPresupuesto.Margin = new Padding(4, 0, 4, 0);
            lbl_noMarkupNoPresupuesto.Name = "lbl_noMarkupNoPresupuesto";
            lbl_noMarkupNoPresupuesto.Size = new Size(547, 17);
            lbl_noMarkupNoPresupuesto.TabIndex = 648;
            lbl_noMarkupNoPresupuesto.Text = "Si no tilda 'Es un presupuesto' y no hay markup NO SE CALCULARÁ I.V.A.";
            lbl_noMarkupNoPresupuesto.Visible = false;
            // 
            // cmb_cc
            // 
            cmb_cc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            cmb_cc.Enabled = false;
            cmb_cc.FormattingEnabled = true;
            cmb_cc.Location = new Point(796, 52);
            cmb_cc.Margin = new Padding(4, 4, 4, 4);
            cmb_cc.Name = "cmb_cc";
            cmb_cc.Size = new Size(355, 24);
            cmb_cc.TabIndex = 1;
            cmb_cc.Text = "Seleccione una cuenta corriente...";
            // 
            // lbl_cc
            // 
            lbl_cc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lbl_cc.AutoSize = true;
            lbl_cc.Location = new Point(659, 58);
            lbl_cc.Margin = new Padding(4, 0, 4, 0);
            lbl_cc.Name = "lbl_cc";
            lbl_cc.Size = new Size(113, 17);
            lbl_cc.TabIndex = 650;
            lbl_cc.Text = "Cuenta corriente";
            // 
            // dg_items
            // 
            dg_items.AllowUserToAddRows = false;
            dg_items.AllowUserToDeleteRows = false;
            dg_items.AllowUserToOrderColumns = true;
            dg_items.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dg_items.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dg_items.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_items.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_items.ContextMenuStrip = ContextMenuStrip1;
            dg_items.Location = new Point(20, 193);
            dg_items.Margin = new Padding(4, 4, 4, 4);
            dg_items.MultiSelect = false;
            dg_items.Name = "dg_items";
            dg_items.ReadOnly = true;
            dg_items.RowHeadersVisible = false;
            dg_items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_items.Size = new Size(789, 282);
            dg_items.TabIndex = 640;
            // 
            // lbl_items
            // 
            lbl_items.AutoSize = true;
            lbl_items.Font = new Font("Microsoft Sans Serif", 13.8f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_items.Location = new Point(336, 154);
            lbl_items.Name = "lbl_items";
            lbl_items.Size = new Size(76, 29);
            lbl_items.TabIndex = 51;
            lbl_items.Text = "Items";
            // 
            // cmd_add_item
            // 
            cmd_add_item.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_add_item.Location = new Point(139, 518);
            cmd_add_item.Margin = new Padding(4, 4, 4, 4);
            cmd_add_item.Name = "cmd_add_item";
            cmd_add_item.Size = new Size(177, 27);
            cmd_add_item.TabIndex = 3;
            cmd_add_item.Text = "Añadir producto";
            cmd_add_item.UseVisualStyleBackColor = true;
            // 
            // cmd_add_descuento
            // 
            cmd_add_descuento.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_add_descuento.Location = new Point(509, 518);
            cmd_add_descuento.Margin = new Padding(4, 4, 4, 4);
            cmd_add_descuento.Name = "cmd_add_descuento";
            cmd_add_descuento.Size = new Size(177, 27);
            cmd_add_descuento.TabIndex = 5;
            cmd_add_descuento.Text = "Agregar descuento";
            cmd_add_descuento.UseVisualStyleBackColor = true;
            // 
            // cmd_addItemManual
            // 
            cmd_addItemManual.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_addItemManual.Location = new Point(324, 518);
            cmd_addItemManual.Margin = new Padding(4, 4, 4, 4);
            cmd_addItemManual.Name = "cmd_addItemManual";
            cmd_addItemManual.Size = new Size(177, 27);
            cmd_addItemManual.TabIndex = 4;
            cmd_addItemManual.Text = "Añadir producto manual";
            cmd_addItemManual.UseVisualStyleBackColor = true;
            // 
            // cmd_first
            // 
            cmd_first.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_first.Location = new Point(16, 482);
            cmd_first.Margin = new Padding(4, 4, 4, 4);
            cmd_first.Name = "cmd_first";
            cmd_first.Size = new Size(39, 25);
            cmd_first.TabIndex = 652;
            cmd_first.Text = "|<<";
            cmd_first.UseVisualStyleBackColor = true;
            cmd_first.Visible = false;
            // 
            // cmd_prev
            // 
            cmd_prev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_prev.Location = new Point(63, 482);
            cmd_prev.Margin = new Padding(4, 4, 4, 4);
            cmd_prev.Name = "cmd_prev";
            cmd_prev.Size = new Size(53, 25);
            cmd_prev.TabIndex = 653;
            cmd_prev.Text = "<<";
            cmd_prev.UseVisualStyleBackColor = true;
            cmd_prev.Visible = false;
            // 
            // cmd_next
            // 
            cmd_next.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_next.Location = new Point(124, 482);
            cmd_next.Margin = new Padding(4, 4, 4, 4);
            cmd_next.Name = "cmd_next";
            cmd_next.Size = new Size(53, 25);
            cmd_next.TabIndex = 654;
            cmd_next.Text = ">>";
            cmd_next.UseVisualStyleBackColor = true;
            cmd_next.Visible = false;
            // 
            // cmd_last
            // 
            cmd_last.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_last.Location = new Point(185, 482);
            cmd_last.Margin = new Padding(4, 4, 4, 4);
            cmd_last.Name = "cmd_last";
            cmd_last.Size = new Size(39, 25);
            cmd_last.TabIndex = 655;
            cmd_last.Text = ">>|";
            cmd_last.UseVisualStyleBackColor = true;
            cmd_last.Visible = false;
            // 
            // txt_nPage
            // 
            txt_nPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_nPage.Location = new Point(232, 482);
            txt_nPage.Margin = new Padding(4, 4, 4, 4);
            txt_nPage.Name = "txt_nPage";
            txt_nPage.Size = new Size(83, 22);
            txt_nPage.TabIndex = 656;
            txt_nPage.Visible = false;
            // 
            // cmd_go
            // 
            cmd_go.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_go.Location = new Point(324, 482);
            cmd_go.Margin = new Padding(4, 4, 4, 4);
            cmd_go.Name = "cmd_go";
            cmd_go.Size = new Size(39, 25);
            cmd_go.TabIndex = 657;
            cmd_go.Text = "Ir";
            cmd_go.UseVisualStyleBackColor = true;
            cmd_go.Visible = false;
            // 
            // psearch_cc
            // 
            psearch_cc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            psearch_cc.Image = My.Resources.Resources.iconoLupa;
            psearch_cc.Location = new Point(1160, 50);
            psearch_cc.Margin = new Padding(4, 4, 4, 4);
            psearch_cc.Name = "psearch_cc";
            psearch_cc.Size = new Size(22, 22);
            psearch_cc.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_cc.TabIndex = 651;
            psearch_cc.TabStop = false;
            // 
            // pic_searchComprobante
            // 
            pic_searchComprobante.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pic_searchComprobante.Image = My.Resources.Resources.iconoLupa;
            pic_searchComprobante.Location = new Point(589, 100);
            pic_searchComprobante.Margin = new Padding(4, 4, 4, 4);
            pic_searchComprobante.Name = "pic_searchComprobante";
            pic_searchComprobante.Size = new Size(22, 22);
            pic_searchComprobante.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchComprobante.TabIndex = 638;
            pic_searchComprobante.TabStop = false;
            // 
            // pic_searchCliente
            // 
            pic_searchCliente.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pic_searchCliente.Image = My.Resources.Resources.iconoLupa;
            pic_searchCliente.Location = new Point(589, 50);
            pic_searchCliente.Margin = new Padding(4, 4, 4, 4);
            pic_searchCliente.Name = "pic_searchCliente";
            pic_searchCliente.Size = new Size(22, 22);
            pic_searchCliente.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCliente.TabIndex = 48;
            pic_searchCliente.TabStop = false;
            // 
            // lbl_avisoAFIP_NC_ND
            // 
            lbl_avisoAFIP_NC_ND.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_avisoAFIP_NC_ND.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_avisoAFIP_NC_ND.ForeColor = Color.Red;
            lbl_avisoAFIP_NC_ND.Location = new Point(287, 716);
            lbl_avisoAFIP_NC_ND.Margin = new Padding(4, 0, 4, 0);
            lbl_avisoAFIP_NC_ND.Name = "lbl_avisoAFIP_NC_ND";
            lbl_avisoAFIP_NC_ND.Size = new Size(733, 44);
            lbl_avisoAFIP_NC_ND.TabIndex = 658;
            lbl_avisoAFIP_NC_ND.Text = "Por una nueva disposición de AFIP para emitir notas de crédito y débito deberá in" + "dicar a qué factura corresponde la contrapartida que está queriendo emitir. ";
            lbl_avisoAFIP_NC_ND.Visible = false;
            // 
            // cmb_seleccionarComprobanteAnulación
            // 
            cmb_seleccionarComprobanteAnulación.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmb_seleccionarComprobanteAnulación.Location = new Point(357, 774);
            cmb_seleccionarComprobanteAnulación.Margin = new Padding(4, 4, 4, 4);
            cmb_seleccionarComprobanteAnulación.Name = "cmb_seleccionarComprobanteAnulación";
            cmb_seleccionarComprobanteAnulación.Size = new Size(221, 27);
            cmb_seleccionarComprobanteAnulación.TabIndex = 659;
            cmb_seleccionarComprobanteAnulación.Text = "Seleccionar comprobante";
            cmb_seleccionarComprobanteAnulación.UseVisualStyleBackColor = true;
            cmb_seleccionarComprobanteAnulación.Visible = false;
            // 
            // lbl_comprobanteAsociado
            // 
            lbl_comprobanteAsociado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lbl_comprobanteAsociado.AutoSize = true;
            lbl_comprobanteAsociado.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_comprobanteAsociado.ForeColor = Color.Black;
            lbl_comprobanteAsociado.Location = new Point(587, 780);
            lbl_comprobanteAsociado.Margin = new Padding(4, 0, 4, 0);
            lbl_comprobanteAsociado.Name = "lbl_comprobanteAsociado";
            lbl_comprobanteAsociado.Size = new Size(174, 17);
            lbl_comprobanteAsociado.TabIndex = 660;
            lbl_comprobanteAsociado.Text = "Comprobante asociado";
            lbl_comprobanteAsociado.Visible = false;
            // 
            // txt_comprobanteAsociado
            // 
            txt_comprobanteAsociado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_comprobanteAsociado.Location = new Point(783, 772);
            txt_comprobanteAsociado.Margin = new Padding(4, 4, 4, 4);
            txt_comprobanteAsociado.Name = "txt_comprobanteAsociado";
            txt_comprobanteAsociado.ReadOnly = true;
            txt_comprobanteAsociado.Size = new Size(105, 22);
            txt_comprobanteAsociado.TabIndex = 661;
            txt_comprobanteAsociado.Visible = false;
            // 
            // add_pedido
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmd_exit;
            ClientSize = new Size(1245, 907);
            ControlBox = false;
            Controls.Add(txt_comprobanteAsociado);
            Controls.Add(lbl_comprobanteAsociado);
            Controls.Add(cmb_seleccionarComprobanteAnulación);
            Controls.Add(lbl_avisoAFIP_NC_ND);
            Controls.Add(cmd_go);
            Controls.Add(txt_nPage);
            Controls.Add(cmd_last);
            Controls.Add(cmd_next);
            Controls.Add(cmd_prev);
            Controls.Add(cmd_first);
            Controls.Add(cmb_cc);
            Controls.Add(psearch_cc);
            Controls.Add(lbl_cc);
            Controls.Add(cmd_addItemManual);
            Controls.Add(lbl_noMarkupNoPresupuesto);
            Controls.Add(chk_esTest);
            Controls.Add(lbl_noTaxNumber);
            Controls.Add(txt_totalDescuentos);
            Controls.Add(lbl_totalDescuentos);
            Controls.Add(lbl_order);
            Controls.Add(lbl_pedido);
            Controls.Add(cmd_add_descuento);
            Controls.Add(cmb_comprobantes);
            Controls.Add(pic_searchComprobante);
            Controls.Add(lbl_comprobante);
            Controls.Add(chk_remitos);
            Controls.Add(cmb_clientes);
            Controls.Add(txt_markup);
            Controls.Add(lbl_markup);
            Controls.Add(txt_totalO);
            Controls.Add(txt_subTotal);
            Controls.Add(lbl_subTotal);
            Controls.Add(cmd_emitir);
            Controls.Add(txt_impuestos);
            Controls.Add(lbl_impuestos);
            Controls.Add(chk_presupuesto);
            Controls.Add(txt_nota2);
            Controls.Add(lbl_nota2);
            Controls.Add(txt_nota1);
            Controls.Add(lbl_nota1);
            Controls.Add(cmd_recargaprecios);
            Controls.Add(txt_total);
            Controls.Add(lbl_total);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_add_item);
            Controls.Add(lbl_date);
            Controls.Add(pic_searchCliente);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_cliente);
            Controls.Add(lbl_fecha);
            Controls.Add(lbl_items);
            Controls.Add(dg_items);
            Margin = new Padding(3, 2, 3, 2);
            MinimizeBox = false;
            MinimumSize = new Size(1261, 735);
            Name = "add_pedido";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Carga de pedido";
            ContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_items).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_cc).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchComprobante).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchCliente).EndInit();
            Load += new EventHandler(add_pedido_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_date;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_cliente;
        internal Label lbl_fecha;
        internal CheckBox chk_secuencia;
        internal Label lbl_total;
        internal TextBox txt_total;
        internal ContextMenuStrip ContextMenuStrip1;
        internal ToolStripMenuItem EditarToolStripMenuItem;
        internal ToolStripMenuItem BorrarToolStripMenuItem;
        internal Button cmd_recargaprecios;
        internal ToolStripMenuItem RecargarPrecioToolStripMenuItem;
        internal Label lbl_nota1;
        internal TextBox txt_nota1;
        internal TextBox txt_nota2;
        internal Label lbl_nota2;
        internal TextBox txt_impuestos;
        internal Label lbl_impuestos;
        internal Button cmd_emitir;
        internal TextBox txt_subTotal;
        internal Label lbl_subTotal;
        internal TextBox txt_totalO;
        internal Label lbl_markup;
        internal TextBox txt_markup;
        internal ComboBox cmb_clientes;
        internal PictureBox pic_searchCliente;
        internal CheckBox chk_remitos;
        internal ComboBox cmb_comprobantes;
        internal PictureBox pic_searchComprobante;
        internal Label lbl_comprobante;
        internal CheckBox chk_presupuesto;
        internal Label lbl_order;
        internal Label lbl_pedido;
        internal TextBox txt_totalDescuentos;
        internal Label lbl_totalDescuentos;
        internal Label lbl_noTaxNumber;
        internal CheckBox chk_esTest;
        internal ToolTip tt_descuento;
        internal Label lbl_noMarkupNoPresupuesto;
        internal ComboBox cmb_cc;
        internal PictureBox psearch_cc;
        internal Label lbl_cc;
        internal DataGridView dg_items;
        internal Label lbl_items;
        internal Button cmd_add_item;
        internal Button cmd_add_descuento;
        internal Button cmd_addItemManual;
        internal Button cmd_first;
        internal Button cmd_prev;
        internal Button cmd_next;
        internal Button cmd_last;
        internal TextBox txt_nPage;
        internal Button cmd_go;
        internal Label lbl_avisoAFIP_NC_ND;
        internal Button cmb_seleccionarComprobanteAnulación;
        internal Label lbl_comprobanteAsociado;
        internal TextBox txt_comprobanteAsociado;
    }
}


