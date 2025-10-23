using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_pago : Form
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
            lbl_fechaCarga1 = new Label();
            lbl_fechaCobro = new Label();
            lbl_fecha = new Label();
            dtp_fechaPago = new DateTimePicker();
            cmb_proveedor = new ComboBox();
            cmb_proveedor.SelectionChangeCommitted += new EventHandler(cmb_proveedor_SelectionChangeCommitted);
            cmb_proveedor.KeyPress += new KeyPressEventHandler(cmb_proveedor_KeyPress);
            lbl_proveedor = new Label();
            chk_efectivo = new CheckBox();
            chk_efectivo.CheckedChanged += new EventHandler(chk_efectivo_CheckedChanged);
            lbl_comoPaga = new Label();
            chk_cheque = new CheckBox();
            chk_cheque.CheckedChanged += new EventHandler(chk_cheque_CheckedChanged);
            lbl_dineroCuenta1 = new Label();
            lbl_dineroCuenta = new Label();
            txt_efectivo = new TextBox();
            txt_efectivo.Leave += new EventHandler(txt_efectivo_Leave);
            lbl_facturasPagar = new Label();
            chklb_facturasPendientes = new CheckedListBox();
            cmd_exit = new Button();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmb_cc = new ComboBox();
            cmb_cc.SelectionChangeCommitted += new EventHandler(cmb_cc_SelectionChangeCommitted);
            cmb_cc.KeyPress += new KeyPressEventHandler(cmb_cc_KeyPress);
            lbl_ccp = new Label();
            cmd_verCheques = new Button();
            cmd_verCheques.Click += new EventHandler(cmd_verCheques_Click);
            lbl_importePago = new Label();
            lbl_pago = new Label();
            lblpeso1 = new Label();
            lbl_borrarbusqueda = new Label();
            lbl_borrarbusqueda.DoubleClick += new EventHandler(lbl_borrarbusqueda_DoubleClick);
            pic_searchCCProveedor = new PictureBox();
            pic_searchCCProveedor.Click += new EventHandler(pic_searchCCProveedor_Click);
            pic_searchProveedor = new PictureBox();
            pic_searchProveedor.Click += new EventHandler(pic_proveedorProveedor_Click);
            TabControl1 = new TabControl();
            cheques = new TabPage();
            lbl_totalCh = new Label();
            lbl_totalCheques = new Label();
            lbl_buscarCheque = new Label();
            txt_search = new TextBox();
            lbl_chSel = new Label();
            dg_viewCH = new DataGridView();
            dg_viewCH.CellValueChanged += new DataGridViewCellEventHandler(dg_viewCH_CellValueChanged);
            dg_viewCH.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dg_viewCH_CellMouseDoubleClick);
            dg_viewCH.CurrentCellDirtyStateChanged += new EventHandler(dg_viewCH_CurrentCellDirtyStateChanged);
            cmd_addCheques = new Button();
            cmd_addCheques.Click += new EventHandler(cmd_addCheques_Click);
            transferencias = new TabPage();
            lbl_totalTransferencia = new Label();
            lbl_totalTransferencias = new Label();
            lbl_buscarTransferencia = new Label();
            txt_searchTransferencia = new TextBox();
            dg_viewTransferencia = new DataGridView();
            dg_viewTransferencia.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dg_viewTransferencia_CellMouseDoubleClick);
            cmd_addTransferencia = new Button();
            cmd_addTransferencia.Click += new EventHandler(cmd_addTransferencia_Click);
            chk_transferencia = new CheckBox();
            chk_transferencia.CheckedChanged += new EventHandler(chk_transferencia_CheckedChanged);
            tb_nFC_importe = new TabPage();
            dg_view_nFC_importes = new DataGridView();
            dg_view_nFC_importes.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dg_view_nFC_importes_EditingControlShowing);
            fecha = new DataGridViewTextBoxColumn();
            factura = new DataGridViewTextBoxColumn();
            importe = new DataGridViewTextBoxColumn();
            lbl_aplicaFc = new Label();
            notas = new TabPage();
            txt_notas = new TextBox();
            lbl_notas = new Label();
            ((System.ComponentModel.ISupportInitialize)pic_searchCCProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).BeginInit();
            TabControl1.SuspendLayout();
            cheques.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewCH).BeginInit();
            transferencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewTransferencia).BeginInit();
            tb_nFC_importe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_view_nFC_importes).BeginInit();
            notas.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_fechaCarga1
            // 
            lbl_fechaCarga1.AutoSize = true;
            lbl_fechaCarga1.Location = new Point(18, 23);
            lbl_fechaCarga1.Name = "lbl_fechaCarga1";
            lbl_fechaCarga1.Size = new Size(85, 13);
            lbl_fechaCarga1.TabIndex = 0;
            lbl_fechaCarga1.Text = "Fecha de carga:";
            // 
            // lbl_fechaCobro
            // 
            lbl_fechaCobro.AutoSize = true;
            lbl_fechaCobro.Location = new Point(18, 62);
            lbl_fechaCobro.Name = "lbl_fechaCobro";
            lbl_fechaCobro.Size = new Size(82, 13);
            lbl_fechaCobro.TabIndex = 1;
            lbl_fechaCobro.Text = "Fecha de pago:";
            // 
            // lbl_fecha
            // 
            lbl_fecha.AutoSize = true;
            lbl_fecha.Location = new Point(173, 23);
            lbl_fecha.Name = "lbl_fecha";
            lbl_fecha.Size = new Size(50, 13);
            lbl_fecha.TabIndex = 2;
            lbl_fecha.Text = "%carga%";
            // 
            // dtp_fechaPago
            // 
            dtp_fechaPago.Format = DateTimePickerFormat.Short;
            dtp_fechaPago.Location = new Point(176, 55);
            dtp_fechaPago.Name = "dtp_fechaPago";
            dtp_fechaPago.Size = new Size(104, 20);
            dtp_fechaPago.TabIndex = 0;
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(176, 93);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(343, 21);
            cmb_proveedor.TabIndex = 1;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(18, 101);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 636;
            lbl_proveedor.Text = "Proveedor";
            // 
            // chk_efectivo
            // 
            chk_efectivo.AutoSize = true;
            chk_efectivo.Enabled = false;
            chk_efectivo.Location = new Point(18, 272);
            chk_efectivo.Name = "chk_efectivo";
            chk_efectivo.Size = new Size(65, 17);
            chk_efectivo.TabIndex = 3;
            chk_efectivo.Text = "Efectivo";
            chk_efectivo.UseVisualStyleBackColor = true;
            // 
            // lbl_comoPaga
            // 
            lbl_comoPaga.AutoSize = true;
            lbl_comoPaga.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_comoPaga.Location = new Point(18, 228);
            lbl_comoPaga.Name = "lbl_comoPaga";
            lbl_comoPaga.Size = new Size(242, 16);
            lbl_comoPaga.TabIndex = 640;
            lbl_comoPaga.Text = "¿Cómo se va a constituir el pago?";
            // 
            // chk_cheque
            // 
            chk_cheque.AutoSize = true;
            chk_cheque.Enabled = false;
            chk_cheque.Location = new Point(18, 18);
            chk_cheque.Name = "chk_cheque";
            chk_cheque.Size = new Size(63, 17);
            chk_cheque.TabIndex = 7;
            chk_cheque.Text = "Cheque";
            chk_cheque.UseVisualStyleBackColor = true;
            // 
            // lbl_dineroCuenta1
            // 
            lbl_dineroCuenta1.AutoSize = true;
            lbl_dineroCuenta1.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_dineroCuenta1.ForeColor = Color.Black;
            lbl_dineroCuenta1.Location = new Point(18, 186);
            lbl_dineroCuenta1.Name = "lbl_dineroCuenta1";
            lbl_dineroCuenta1.Size = new Size(90, 15);
            lbl_dineroCuenta1.TabIndex = 643;
            lbl_dineroCuenta1.Text = "Saldo de CC.";
            // 
            // lbl_dineroCuenta
            // 
            lbl_dineroCuenta.AutoSize = true;
            lbl_dineroCuenta.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_dineroCuenta.ForeColor = Color.Green;
            lbl_dineroCuenta.Location = new Point(163, 186);
            lbl_dineroCuenta.Name = "lbl_dineroCuenta";
            lbl_dineroCuenta.Size = new Size(24, 16);
            lbl_dineroCuenta.TabIndex = 644;
            lbl_dineroCuenta.Text = "$$";
            // 
            // txt_efectivo
            // 
            txt_efectivo.Enabled = false;
            txt_efectivo.Location = new Point(257, 269);
            txt_efectivo.Name = "txt_efectivo";
            txt_efectivo.Size = new Size(285, 20);
            txt_efectivo.TabIndex = 4;
            // 
            // lbl_facturasPagar
            // 
            lbl_facturasPagar.AutoSize = true;
            lbl_facturasPagar.Location = new Point(619, 18);
            lbl_facturasPagar.Name = "lbl_facturasPagar";
            lbl_facturasPagar.Size = new Size(148, 13);
            lbl_facturasPagar.TabIndex = 648;
            lbl_facturasPagar.Text = "Facturas pendientes de cobro";
            // 
            // chklb_facturasPendientes
            // 
            chklb_facturasPendientes.Enabled = false;
            chklb_facturasPendientes.FormattingEnabled = true;
            chklb_facturasPendientes.Location = new Point(622, 46);
            chklb_facturasPendientes.Name = "chklb_facturasPendientes";
            chklb_facturasPendientes.Size = new Size(464, 259);
            chklb_facturasPendientes.TabIndex = 649;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(307, 741);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 11;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(209, 741);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 10;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmb_cc
            // 
            cmb_cc.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_cc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_cc.FormattingEnabled = true;
            cmb_cc.Location = new Point(176, 132);
            cmb_cc.Name = "cmb_cc";
            cmb_cc.Size = new Size(343, 21);
            cmb_cc.TabIndex = 2;
            // 
            // lbl_ccp
            // 
            lbl_ccp.AutoSize = true;
            lbl_ccp.Location = new Point(18, 140);
            lbl_ccp.Name = "lbl_ccp";
            lbl_ccp.Size = new Size(153, 13);
            lbl_ccp.TabIndex = 653;
            lbl_ccp.Text = "Cuenta corriente del proveedor";
            // 
            // cmd_verCheques
            // 
            cmd_verCheques.Enabled = false;
            cmd_verCheques.Location = new Point(391, 18);
            cmd_verCheques.Name = "cmd_verCheques";
            cmd_verCheques.Size = new Size(127, 38);
            cmd_verCheques.TabIndex = 7;
            cmd_verCheques.Text = "Seleccionar cheques";
            cmd_verCheques.UseVisualStyleBackColor = true;
            cmd_verCheques.Visible = false;
            // 
            // lbl_importePago
            // 
            lbl_importePago.AutoSize = true;
            lbl_importePago.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_importePago.ForeColor = Color.Green;
            lbl_importePago.Location = new Point(206, 703);
            lbl_importePago.Name = "lbl_importePago";
            lbl_importePago.Size = new Size(24, 16);
            lbl_importePago.TabIndex = 662;
            lbl_importePago.Text = "$$";
            // 
            // lbl_pago
            // 
            lbl_pago.AutoSize = true;
            lbl_pago.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_pago.ForeColor = Color.Black;
            lbl_pago.Location = new Point(18, 703);
            lbl_pago.Name = "lbl_pago";
            lbl_pago.Size = new Size(148, 15);
            lbl_pago.TabIndex = 661;
            lbl_pago.Text = "Importe total del pago";
            // 
            // lblpeso1
            // 
            lblpeso1.AutoSize = true;
            lblpeso1.Location = new Point(238, 269);
            lblpeso1.Name = "lblpeso1";
            lblpeso1.Size = new Size(13, 13);
            lblpeso1.TabIndex = 663;
            lblpeso1.Text = "$";
            // 
            // lbl_borrarbusqueda
            // 
            lbl_borrarbusqueda.Anchor = AnchorStyles.Right;
            lbl_borrarbusqueda.AutoSize = true;
            lbl_borrarbusqueda.Enabled = false;
            lbl_borrarbusqueda.Location = new Point(-12, 427);
            lbl_borrarbusqueda.Margin = new Padding(2, 0, 2, 0);
            lbl_borrarbusqueda.Name = "lbl_borrarbusqueda";
            lbl_borrarbusqueda.Size = new Size(12, 13);
            lbl_borrarbusqueda.TabIndex = 667;
            lbl_borrarbusqueda.Text = "x";
            // 
            // pic_searchCCProveedor
            // 
            pic_searchCCProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchCCProveedor.Location = new Point(525, 131);
            pic_searchCCProveedor.Name = "pic_searchCCProveedor";
            pic_searchCCProveedor.Size = new Size(22, 22);
            pic_searchCCProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCCProveedor.TabIndex = 654;
            pic_searchCCProveedor.TabStop = false;
            // 
            // pic_searchProveedor
            // 
            pic_searchProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchProveedor.Location = new Point(524, 92);
            pic_searchProveedor.Name = "pic_searchProveedor";
            pic_searchProveedor.Size = new Size(22, 22);
            pic_searchProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchProveedor.TabIndex = 637;
            pic_searchProveedor.TabStop = false;
            // 
            // TabControl1
            // 
            TabControl1.Controls.Add(cheques);
            TabControl1.Controls.Add(transferencias);
            TabControl1.Controls.Add(tb_nFC_importe);
            TabControl1.Controls.Add(notas);
            TabControl1.Location = new Point(18, 307);
            TabControl1.Name = "TabControl1";
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(560, 386);
            TabControl1.TabIndex = 670;
            // 
            // cheques
            // 
            cheques.BackColor = SystemColors.Control;
            cheques.Controls.Add(lbl_totalCh);
            cheques.Controls.Add(lbl_totalCheques);
            cheques.Controls.Add(lbl_buscarCheque);
            cheques.Controls.Add(txt_search);
            cheques.Controls.Add(lbl_chSel);
            cheques.Controls.Add(cmd_verCheques);
            cheques.Controls.Add(dg_viewCH);
            cheques.Controls.Add(cmd_addCheques);
            cheques.Controls.Add(chk_cheque);
            cheques.Location = new Point(4, 22);
            cheques.Name = "cheques";
            cheques.Padding = new Padding(3);
            cheques.Size = new Size(552, 360);
            cheques.TabIndex = 0;
            cheques.Text = "Cheques";
            // 
            // lbl_totalCh
            // 
            lbl_totalCh.AutoSize = true;
            lbl_totalCh.Location = new Point(194, 322);
            lbl_totalCh.Name = "lbl_totalCh";
            lbl_totalCh.Size = new Size(19, 13);
            lbl_totalCh.TabIndex = 676;
            lbl_totalCh.Text = "$$";
            // 
            // lbl_totalCheques
            // 
            lbl_totalCheques.AutoSize = true;
            lbl_totalCheques.Location = new Point(9, 322);
            lbl_totalCheques.Name = "lbl_totalCheques";
            lbl_totalCheques.Size = new Size(146, 13);
            lbl_totalCheques.TabIndex = 675;
            lbl_totalCheques.Text = "Total cheques seleccionados";
            // 
            // lbl_buscarCheque
            // 
            lbl_buscarCheque.AutoSize = true;
            lbl_buscarCheque.Location = new Point(15, 83);
            lbl_buscarCheque.Name = "lbl_buscarCheque";
            lbl_buscarCheque.Size = new Size(84, 13);
            lbl_buscarCheque.TabIndex = 674;
            lbl_buscarCheque.Text = "Buscar cheques";
            // 
            // txt_search
            // 
            txt_search.Enabled = false;
            txt_search.Location = new Point(250, 83);
            txt_search.Name = "txt_search";
            txt_search.Size = new Size(268, 20);
            txt_search.TabIndex = 673;
            // 
            // lbl_chSel
            // 
            lbl_chSel.AutoSize = true;
            lbl_chSel.Location = new Point(15, 57);
            lbl_chSel.Name = "lbl_chSel";
            lbl_chSel.Size = new Size(177, 13);
            lbl_chSel.TabIndex = 672;
            lbl_chSel.Text = "Cheques disponibles/seleccionados";
            // 
            // dg_viewCH
            // 
            dg_viewCH.AllowUserToAddRows = false;
            dg_viewCH.AllowUserToDeleteRows = false;
            dg_viewCH.AllowUserToOrderColumns = true;
            dg_viewCH.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewCH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewCH.Enabled = false;
            dg_viewCH.Location = new Point(12, 109);
            dg_viewCH.MultiSelect = false;
            dg_viewCH.Name = "dg_viewCH";
            dg_viewCH.ReadOnly = true;
            dg_viewCH.RowHeadersVisible = false;
            dg_viewCH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewCH.Size = new Size(511, 197);
            dg_viewCH.TabIndex = 670;
            // 
            // cmd_addCheques
            // 
            cmd_addCheques.Enabled = false;
            cmd_addCheques.Location = new Point(404, 312);
            cmd_addCheques.Name = "cmd_addCheques";
            cmd_addCheques.Size = new Size(119, 23);
            cmd_addCheques.TabIndex = 671;
            cmd_addCheques.Text = "Ingresar cheques";
            cmd_addCheques.UseVisualStyleBackColor = true;
            // 
            // transferencias
            // 
            transferencias.BackColor = SystemColors.Control;
            transferencias.Controls.Add(lbl_totalTransferencia);
            transferencias.Controls.Add(lbl_totalTransferencias);
            transferencias.Controls.Add(lbl_buscarTransferencia);
            transferencias.Controls.Add(txt_searchTransferencia);
            transferencias.Controls.Add(dg_viewTransferencia);
            transferencias.Controls.Add(cmd_addTransferencia);
            transferencias.Controls.Add(chk_transferencia);
            transferencias.Location = new Point(4, 22);
            transferencias.Name = "transferencias";
            transferencias.Padding = new Padding(3);
            transferencias.Size = new Size(552, 360);
            transferencias.TabIndex = 1;
            transferencias.Text = "Transferencias";
            // 
            // lbl_totalTransferencia
            // 
            lbl_totalTransferencia.AutoSize = true;
            lbl_totalTransferencia.Location = new Point(145, 337);
            lbl_totalTransferencia.Name = "lbl_totalTransferencia";
            lbl_totalTransferencia.Size = new Size(19, 13);
            lbl_totalTransferencia.TabIndex = 719;
            lbl_totalTransferencia.Text = "$$";
            // 
            // lbl_totalTransferencias
            // 
            lbl_totalTransferencias.AutoSize = true;
            lbl_totalTransferencias.Location = new Point(18, 337);
            lbl_totalTransferencias.Name = "lbl_totalTransferencias";
            lbl_totalTransferencias.Size = new Size(100, 13);
            lbl_totalTransferencias.TabIndex = 718;
            lbl_totalTransferencias.Text = "Total transferencias";
            // 
            // lbl_buscarTransferencia
            // 
            lbl_buscarTransferencia.AutoSize = true;
            lbl_buscarTransferencia.Location = new Point(18, 49);
            lbl_buscarTransferencia.Name = "lbl_buscarTransferencia";
            lbl_buscarTransferencia.Size = new Size(104, 13);
            lbl_buscarTransferencia.TabIndex = 717;
            lbl_buscarTransferencia.Text = "Buscar transferencia";
            // 
            // txt_searchTransferencia
            // 
            txt_searchTransferencia.Enabled = false;
            txt_searchTransferencia.Location = new Point(265, 46);
            txt_searchTransferencia.Name = "txt_searchTransferencia";
            txt_searchTransferencia.Size = new Size(268, 20);
            txt_searchTransferencia.TabIndex = 714;
            // 
            // dg_viewTransferencia
            // 
            dg_viewTransferencia.AllowUserToAddRows = false;
            dg_viewTransferencia.AllowUserToDeleteRows = false;
            dg_viewTransferencia.AllowUserToOrderColumns = true;
            dg_viewTransferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewTransferencia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewTransferencia.Enabled = false;
            dg_viewTransferencia.Location = new Point(18, 72);
            dg_viewTransferencia.MultiSelect = false;
            dg_viewTransferencia.Name = "dg_viewTransferencia";
            dg_viewTransferencia.ReadOnly = true;
            dg_viewTransferencia.RowHeadersVisible = false;
            dg_viewTransferencia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewTransferencia.Size = new Size(511, 242);
            dg_viewTransferencia.TabIndex = 715;
            // 
            // cmd_addTransferencia
            // 
            cmd_addTransferencia.Enabled = false;
            cmd_addTransferencia.Location = new Point(414, 327);
            cmd_addTransferencia.Name = "cmd_addTransferencia";
            cmd_addTransferencia.Size = new Size(119, 23);
            cmd_addTransferencia.TabIndex = 716;
            cmd_addTransferencia.Text = "Ingresar transferencia";
            cmd_addTransferencia.UseVisualStyleBackColor = true;
            // 
            // chk_transferencia
            // 
            chk_transferencia.AutoSize = true;
            chk_transferencia.Enabled = false;
            chk_transferencia.Location = new Point(18, 18);
            chk_transferencia.Name = "chk_transferencia";
            chk_transferencia.Size = new Size(180, 17);
            chk_transferencia.TabIndex = 713;
            chk_transferencia.Text = "Transferencia/depósito bancario";
            chk_transferencia.UseVisualStyleBackColor = true;
            // 
            // tb_nFC_importe
            // 
            tb_nFC_importe.BackColor = SystemColors.Control;
            tb_nFC_importe.Controls.Add(dg_view_nFC_importes);
            tb_nFC_importe.Controls.Add(lbl_aplicaFc);
            tb_nFC_importe.Location = new Point(4, 22);
            tb_nFC_importe.Name = "tb_nFC_importe";
            tb_nFC_importe.Size = new Size(552, 360);
            tb_nFC_importe.TabIndex = 3;
            tb_nFC_importe.Text = "Nº FC / Importe";
            // 
            // dg_view_nFC_importes
            // 
            dg_view_nFC_importes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_nFC_importes.Columns.AddRange(new DataGridViewColumn[] { fecha, factura, importe });
            dg_view_nFC_importes.Enabled = false;
            dg_view_nFC_importes.Location = new Point(21, 48);
            dg_view_nFC_importes.Name = "dg_view_nFC_importes";
            dg_view_nFC_importes.Size = new Size(514, 112);
            dg_view_nFC_importes.TabIndex = 677;
            // 
            // fecha
            // 
            fecha.HeaderText = "Fecha";
            fecha.Name = "fecha";
            // 
            // factura
            // 
            factura.HeaderText = "Factura";
            factura.Name = "factura";
            // 
            // importe
            // 
            importe.HeaderText = "Importe";
            importe.Name = "importe";
            // 
            // lbl_aplicaFc
            // 
            lbl_aplicaFc.AutoSize = true;
            lbl_aplicaFc.Location = new Point(18, 21);
            lbl_aplicaFc.Name = "lbl_aplicaFc";
            lbl_aplicaFc.Size = new Size(194, 13);
            lbl_aplicaFc.TabIndex = 676;
            lbl_aplicaFc.Text = "Facturas a las que aplica y sus importes";
            // 
            // notas
            // 
            notas.BackColor = SystemColors.Control;
            notas.Controls.Add(txt_notas);
            notas.Controls.Add(lbl_notas);
            notas.Location = new Point(4, 22);
            notas.Name = "notas";
            notas.Size = new Size(552, 360);
            notas.TabIndex = 2;
            notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Enabled = false;
            txt_notas.Location = new Point(18, 38);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(518, 181);
            txt_notas.TabIndex = 677;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(18, 18);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 678;
            lbl_notas.Text = "Notas";
            // 
            // add_pago
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 785);
            Controls.Add(TabControl1);
            Controls.Add(lbl_borrarbusqueda);
            Controls.Add(lblpeso1);
            Controls.Add(lbl_importePago);
            Controls.Add(lbl_pago);
            Controls.Add(cmb_cc);
            Controls.Add(pic_searchCCProveedor);
            Controls.Add(lbl_ccp);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(chklb_facturasPendientes);
            Controls.Add(lbl_facturasPagar);
            Controls.Add(txt_efectivo);
            Controls.Add(lbl_dineroCuenta);
            Controls.Add(lbl_dineroCuenta1);
            Controls.Add(lbl_comoPaga);
            Controls.Add(chk_efectivo);
            Controls.Add(cmb_proveedor);
            Controls.Add(pic_searchProveedor);
            Controls.Add(lbl_proveedor);
            Controls.Add(dtp_fechaPago);
            Controls.Add(lbl_fecha);
            Controls.Add(lbl_fechaCobro);
            Controls.Add(lbl_fechaCarga1);
            Name = "add_pago";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar pagos";
            ((System.ComponentModel.ISupportInitialize)pic_searchCCProveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).EndInit();
            TabControl1.ResumeLayout(false);
            cheques.ResumeLayout(false);
            cheques.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewCH).EndInit();
            transferencias.ResumeLayout(false);
            transferencias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewTransferencia).EndInit();
            tb_nFC_importe.ResumeLayout(false);
            tb_nFC_importe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_view_nFC_importes).EndInit();
            notas.ResumeLayout(false);
            notas.PerformLayout();
            Load += new EventHandler(add_pago_Load);
            FormClosed += new FormClosedEventHandler(add_pago_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_fechaCarga1;
        internal Label lbl_fechaCobro;
        internal Label lbl_fecha;
        internal DateTimePicker dtp_fechaPago;
        internal ComboBox cmb_proveedor;
        internal PictureBox pic_searchProveedor;
        internal Label lbl_proveedor;
        internal CheckBox chk_efectivo;
        internal Label lbl_comoPaga;
        internal CheckBox chk_cheque;
        internal Label lbl_dineroCuenta1;
        internal Label lbl_dineroCuenta;
        internal TextBox txt_efectivo;
        internal Label lbl_facturasPagar;
        internal CheckedListBox chklb_facturasPendientes;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal ComboBox cmb_cc;
        internal PictureBox pic_searchCCProveedor;
        internal Label lbl_ccp;
        internal Button cmd_verCheques;
        internal Label lbl_importePago;
        internal Label lbl_pago;
        internal Label lblpeso1;
        internal Label lbl_borrarbusqueda;
        internal TabControl TabControl1;
        internal TabPage cheques;
        internal Label lbl_totalCh;
        internal Label lbl_totalCheques;
        internal Label lbl_buscarCheque;
        internal TextBox txt_search;
        internal Label lbl_chSel;
        internal DataGridView dg_viewCH;
        internal Button cmd_addCheques;
        internal TabPage transferencias;
        internal TabPage notas;
        internal TabPage tb_nFC_importe;
        internal TextBox txt_notas;
        internal Label lbl_notas;
        internal Label lbl_totalTransferencias;
        internal Label lbl_buscarTransferencia;
        internal TextBox txt_searchTransferencia;
        internal DataGridView dg_viewTransferencia;
        internal Button cmd_addTransferencia;
        internal CheckBox chk_transferencia;
        internal Label lbl_totalTransferencia;
        internal DataGridView dg_view_nFC_importes;
        internal DataGridViewTextBoxColumn fecha;
        internal DataGridViewTextBoxColumn factura;
        internal DataGridViewTextBoxColumn importe;
        internal Label lbl_aplicaFc;
    }
}

