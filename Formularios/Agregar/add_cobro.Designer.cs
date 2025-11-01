using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_cobro : Form
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
            lbl_fechaCarga1 = new Label();
            lbl_fechaCarga = new Label();
            cmd_exit = new Button();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_importePago = new Label();
            lbl_pago = new Label();
            lbl_aplicaFc = new Label();
            tbControl = new TabControl();
            tb_cobro = new TabPage();
            chk_cobro_no_oficial = new CheckBox();
            lblpeso1 = new Label();
            cmb_cc = new ComboBox();
            cmb_cc.SelectionChangeCommitted += new EventHandler(cmb_cc_SelectionChangeCommitted);
            pic_searchCCCliente = new PictureBox();
            lbl_ccc = new Label();
            txt_efectivo = new TextBox();
            lbl_saldo = new Label();
            lbl_saldo1 = new Label();
            lbl_comoPaga = new Label();
            chk_efectivo = new CheckBox();
            chk_efectivo.CheckedChanged += new EventHandler(chk_efectivo_CheckedChanged);
            cmb_cliente = new ComboBox();
            cmb_cliente.SelectionChangeCommitted += new EventHandler(cmb_cliente_SelectionChangeCommitted);
            pic_searchCliente = new PictureBox();
            lbl_cliente = new Label();
            dtp_fechaCobro = new DateTimePicker();
            lbl_fechaCobro = new Label();
            tb_cheques = new TabPage();
            lbl_totalCh = new Label();
            lbl_totalCheques = new Label();
            lbl_borrarbusquedaCH = new Label();
            lbl_buscarCheque = new Label();
            txt_searchCH = new TextBox();
            lbl_chSel = new Label();
            dg_viewCH = new DataGridView();
            cms_general = new ContextMenuStrip(components);
            EditarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem = new ToolStripMenuItem();
            cmd_verCheques = new Button();
            cmd_addCheques = new Button();
            chk_cheque = new CheckBox();
            chk_cheque.CheckedChanged += new EventHandler(chk_cheque_CheckedChanged);
            tb_transferencias = new TabPage();
            lbl_totalTransferencia = new Label();
            lbl_totalTransferencias = new Label();
            lbl_borrarbusquedaTransferencia = new Label();
            lbl_buscarTransferencia = new Label();
            txt_searchTransferencia = new TextBox();
            dg_viewTransferencia = new DataGridView();
            cmd_addTransferencia = new Button();
            chk_transferencia = new CheckBox();
            chk_transferencia.CheckedChanged += new EventHandler(chk_transferencia_CheckedChanged);
            tb_retenciones = new TabPage();
            lbl_totalRetencion = new Label();
            lbl_totalRetenciones = new Label();
            lbl_borrarbusquedaRetencion = new Label();
            lbl_buscarRetencion = new Label();
            txt_searchRetencion = new TextBox();
            dg_viewRetencion = new DataGridView();
            cmd_addRetencion = new Button();
            chk_retenciones = new CheckBox();
            chk_retenciones.CheckedChanged += new EventHandler(chk_retenciones_CheckedChanged);
            tb_nFC_importe = new TabPage();
            dg_view_nFC_importes = new DataGridView();
            fecha = new DataGridViewTextBoxColumn();
            factura = new DataGridViewTextBoxColumn();
            importe = new DataGridViewTextBoxColumn();
            tb_notas = new TabPage();
            txt_notas = new TextBox();
            lbl_notas = new Label();
            txt_firmante = new TextBox();
            lbl_firmante = new Label();
            tbControl.SuspendLayout();
            tb_cobro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_searchCCCliente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchCliente).BeginInit();
            tb_cheques.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewCH).BeginInit();
            cms_general.SuspendLayout();
            tb_transferencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewTransferencia).BeginInit();
            tb_retenciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewRetencion).BeginInit();
            tb_nFC_importe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_view_nFC_importes).BeginInit();
            tb_notas.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_fechaCarga1
            // 
            lbl_fechaCarga1.AutoSize = true;
            lbl_fechaCarga1.Location = new Point(30, 23);
            lbl_fechaCarga1.Name = "lbl_fechaCarga1";
            lbl_fechaCarga1.Size = new Size(85, 13);
            lbl_fechaCarga1.TabIndex = 0;
            lbl_fechaCarga1.Text = "Fecha de carga:";
            // 
            // lbl_fechaCarga
            // 
            lbl_fechaCarga.AutoSize = true;
            lbl_fechaCarga.Location = new Point(147, 23);
            lbl_fechaCarga.Name = "lbl_fechaCarga";
            lbl_fechaCarga.Size = new Size(50, 13);
            lbl_fechaCarga.TabIndex = 2;
            lbl_fechaCarga.Text = "%carga%";
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(302, 593);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 11;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(204, 593);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Emitir";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_importePago
            // 
            lbl_importePago.AutoSize = true;
            lbl_importePago.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_importePago.ForeColor = Color.Green;
            lbl_importePago.Location = new Point(201, 511);
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
            lbl_pago.Location = new Point(30, 511);
            lbl_pago.Name = "lbl_pago";
            lbl_pago.Size = new Size(148, 15);
            lbl_pago.TabIndex = 661;
            lbl_pago.Text = "Importe total del pago";
            // 
            // lbl_aplicaFc
            // 
            lbl_aplicaFc.AutoSize = true;
            lbl_aplicaFc.Location = new Point(20, 20);
            lbl_aplicaFc.Name = "lbl_aplicaFc";
            lbl_aplicaFc.Size = new Size(194, 13);
            lbl_aplicaFc.TabIndex = 674;
            lbl_aplicaFc.Text = "Facturas a las que aplica y sus importes";
            // 
            // tbControl
            // 
            tbControl.Controls.Add(tb_cobro);
            tbControl.Controls.Add(tb_cheques);
            tbControl.Controls.Add(tb_transferencias);
            tbControl.Controls.Add(tb_retenciones);
            tbControl.Controls.Add(tb_nFC_importe);
            tbControl.Controls.Add(tb_notas);
            tbControl.Location = new Point(30, 50);
            tbControl.Name = "tbControl";
            tbControl.SelectedIndex = 0;
            tbControl.Size = new Size(584, 430);
            tbControl.TabIndex = 0;
            // 
            // tb_cobro
            // 
            tb_cobro.BackColor = SystemColors.Control;
            tb_cobro.Controls.Add(chk_cobro_no_oficial);
            tb_cobro.Controls.Add(lblpeso1);
            tb_cobro.Controls.Add(cmb_cc);
            tb_cobro.Controls.Add(pic_searchCCCliente);
            tb_cobro.Controls.Add(lbl_ccc);
            tb_cobro.Controls.Add(txt_efectivo);
            tb_cobro.Controls.Add(lbl_saldo);
            tb_cobro.Controls.Add(lbl_saldo1);
            tb_cobro.Controls.Add(lbl_comoPaga);
            tb_cobro.Controls.Add(chk_efectivo);
            tb_cobro.Controls.Add(cmb_cliente);
            tb_cobro.Controls.Add(pic_searchCliente);
            tb_cobro.Controls.Add(lbl_cliente);
            tb_cobro.Controls.Add(dtp_fechaCobro);
            tb_cobro.Controls.Add(lbl_fechaCobro);
            tb_cobro.Location = new Point(4, 22);
            tb_cobro.Name = "tb_cobro";
            tb_cobro.Padding = new Padding(3);
            tb_cobro.Size = new Size(576, 404);
            tb_cobro.TabIndex = 0;
            tb_cobro.Text = "Cobro";
            // 
            // chk_cobro_no_oficial
            // 
            chk_cobro_no_oficial.AutoSize = true;
            chk_cobro_no_oficial.Enabled = false;
            chk_cobro_no_oficial.Location = new Point(268, 138);
            chk_cobro_no_oficial.Name = "chk_cobro_no_oficial";
            chk_cobro_no_oficial.Size = new Size(99, 17);
            chk_cobro_no_oficial.TabIndex = 692;
            chk_cobro_no_oficial.Text = "Cobro no oficial";
            chk_cobro_no_oficial.UseVisualStyleBackColor = true;
            // 
            // lblpeso1
            // 
            lblpeso1.AutoSize = true;
            lblpeso1.Location = new Point(230, 225);
            lblpeso1.Name = "lblpeso1";
            lblpeso1.Size = new Size(13, 13);
            lblpeso1.TabIndex = 675;
            lblpeso1.Text = "$";
            // 
            // cmb_cc
            // 
            cmb_cc.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_cc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_cc.FormattingEnabled = true;
            cmb_cc.Location = new Point(163, 91);
            cmb_cc.Name = "cmb_cc";
            cmb_cc.Size = new Size(343, 21);
            cmb_cc.TabIndex = 2;
            // 
            // pic_searchCCCliente
            // 
            pic_searchCCCliente.Image = My.Resources.Resources.iconoLupa;
            pic_searchCCCliente.Location = new Point(512, 91);
            pic_searchCCCliente.Name = "pic_searchCCCliente";
            pic_searchCCCliente.Size = new Size(22, 22);
            pic_searchCCCliente.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCCCliente.TabIndex = 691;
            pic_searchCCCliente.TabStop = false;
            // 
            // lbl_ccc
            // 
            lbl_ccc.AutoSize = true;
            lbl_ccc.Location = new Point(20, 91);
            lbl_ccc.Name = "lbl_ccc";
            lbl_ccc.Size = new Size(136, 13);
            lbl_ccc.TabIndex = 690;
            lbl_ccc.Text = "Cuenta corriente del cliente";
            // 
            // txt_efectivo
            // 
            txt_efectivo.Enabled = false;
            txt_efectivo.Location = new Point(249, 225);
            txt_efectivo.Name = "txt_efectivo";
            txt_efectivo.Size = new Size(285, 20);
            txt_efectivo.TabIndex = 4;
            // 
            // lbl_saldo
            // 
            lbl_saldo.AutoSize = true;
            lbl_saldo.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_saldo.ForeColor = Color.Green;
            lbl_saldo.Location = new Point(155, 177);
            lbl_saldo.Name = "lbl_saldo";
            lbl_saldo.Size = new Size(24, 16);
            lbl_saldo.TabIndex = 689;
            lbl_saldo.Text = "$$";
            // 
            // lbl_saldo1
            // 
            lbl_saldo1.AutoSize = true;
            lbl_saldo1.Font = new Font("Microsoft Sans Serif", 9.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_saldo1.ForeColor = Color.Black;
            lbl_saldo1.Location = new Point(20, 177);
            lbl_saldo1.Name = "lbl_saldo1";
            lbl_saldo1.Size = new Size(44, 15);
            lbl_saldo1.TabIndex = 688;
            lbl_saldo1.Text = "Saldo";
            // 
            // lbl_comoPaga
            // 
            lbl_comoPaga.AutoSize = true;
            lbl_comoPaga.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_comoPaga.Location = new Point(20, 137);
            lbl_comoPaga.Name = "lbl_comoPaga";
            lbl_comoPaga.Size = new Size(210, 16);
            lbl_comoPaga.TabIndex = 687;
            lbl_comoPaga.Text = "¿Cómo va a pagar el cliente?";
            // 
            // chk_efectivo
            // 
            chk_efectivo.AutoSize = true;
            chk_efectivo.Enabled = false;
            chk_efectivo.Location = new Point(23, 228);
            chk_efectivo.Name = "chk_efectivo";
            chk_efectivo.Size = new Size(65, 17);
            chk_efectivo.TabIndex = 3;
            chk_efectivo.Text = "Efectivo";
            chk_efectivo.UseVisualStyleBackColor = true;
            // 
            // cmb_cliente
            // 
            cmb_cliente.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_cliente.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_cliente.FormattingEnabled = true;
            cmb_cliente.Location = new Point(163, 52);
            cmb_cliente.Name = "cmb_cliente";
            cmb_cliente.Size = new Size(343, 21);
            cmb_cliente.TabIndex = 1;
            // 
            // pic_searchCliente
            // 
            pic_searchCliente.Image = My.Resources.Resources.iconoLupa;
            pic_searchCliente.Location = new Point(512, 52);
            pic_searchCliente.Name = "pic_searchCliente";
            pic_searchCliente.Size = new Size(22, 22);
            pic_searchCliente.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchCliente.TabIndex = 686;
            pic_searchCliente.TabStop = false;
            // 
            // lbl_cliente
            // 
            lbl_cliente.AutoSize = true;
            lbl_cliente.Location = new Point(20, 52);
            lbl_cliente.Name = "lbl_cliente";
            lbl_cliente.Size = new Size(39, 13);
            lbl_cliente.TabIndex = 685;
            lbl_cliente.Text = "Cliente";
            // 
            // dtp_fechaCobro
            // 
            dtp_fechaCobro.Format = DateTimePickerFormat.Short;
            dtp_fechaCobro.Location = new Point(163, 20);
            dtp_fechaCobro.Name = "dtp_fechaCobro";
            dtp_fechaCobro.Size = new Size(104, 20);
            dtp_fechaCobro.TabIndex = 0;
            // 
            // lbl_fechaCobro
            // 
            lbl_fechaCobro.AutoSize = true;
            lbl_fechaCobro.Location = new Point(20, 20);
            lbl_fechaCobro.Name = "lbl_fechaCobro";
            lbl_fechaCobro.Size = new Size(85, 13);
            lbl_fechaCobro.TabIndex = 671;
            lbl_fechaCobro.Text = "Fecha de cobro:";
            // 
            // tb_cheques
            // 
            tb_cheques.BackColor = SystemColors.Control;
            tb_cheques.Controls.Add(lbl_totalCh);
            tb_cheques.Controls.Add(lbl_totalCheques);
            tb_cheques.Controls.Add(lbl_borrarbusquedaCH);
            tb_cheques.Controls.Add(lbl_buscarCheque);
            tb_cheques.Controls.Add(txt_searchCH);
            tb_cheques.Controls.Add(lbl_chSel);
            tb_cheques.Controls.Add(dg_viewCH);
            tb_cheques.Controls.Add(cmd_verCheques);
            tb_cheques.Controls.Add(cmd_addCheques);
            tb_cheques.Controls.Add(chk_cheque);
            tb_cheques.Location = new Point(4, 22);
            tb_cheques.Name = "tb_cheques";
            tb_cheques.Size = new Size(576, 404);
            tb_cheques.TabIndex = 2;
            tb_cheques.Text = "Cheques";
            // 
            // lbl_totalCh
            // 
            lbl_totalCh.AutoSize = true;
            lbl_totalCh.Location = new Point(190, 351);
            lbl_totalCh.Name = "lbl_totalCh";
            lbl_totalCh.Size = new Size(19, 13);
            lbl_totalCh.TabIndex = 706;
            lbl_totalCh.Text = "$$";
            // 
            // lbl_totalCheques
            // 
            lbl_totalCheques.AutoSize = true;
            lbl_totalCheques.Location = new Point(17, 351);
            lbl_totalCheques.Name = "lbl_totalCheques";
            lbl_totalCheques.Size = new Size(146, 13);
            lbl_totalCheques.TabIndex = 705;
            lbl_totalCheques.Text = "Total cheques seleccionados";
            // 
            // lbl_borrarbusquedaCH
            // 
            lbl_borrarbusquedaCH.Anchor = AnchorStyles.Right;
            lbl_borrarbusquedaCH.AutoSize = true;
            lbl_borrarbusquedaCH.Enabled = false;
            lbl_borrarbusquedaCH.Location = new Point(519, -11);
            lbl_borrarbusquedaCH.Margin = new Padding(2, 0, 2, 0);
            lbl_borrarbusquedaCH.Name = "lbl_borrarbusquedaCH";
            lbl_borrarbusquedaCH.Size = new Size(12, 13);
            lbl_borrarbusquedaCH.TabIndex = 699;
            lbl_borrarbusquedaCH.Text = "x";
            // 
            // lbl_buscarCheque
            // 
            lbl_buscarCheque.AutoSize = true;
            lbl_buscarCheque.Location = new Point(23, 88);
            lbl_buscarCheque.Name = "lbl_buscarCheque";
            lbl_buscarCheque.Size = new Size(84, 13);
            lbl_buscarCheque.TabIndex = 704;
            lbl_buscarCheque.Text = "Buscar cheques";
            // 
            // txt_searchCH
            // 
            txt_searchCH.Enabled = false;
            txt_searchCH.Location = new Point(246, 85);
            txt_searchCH.Name = "txt_searchCH";
            txt_searchCH.Size = new Size(268, 20);
            txt_searchCH.TabIndex = 698;
            // 
            // lbl_chSel
            // 
            lbl_chSel.AutoSize = true;
            lbl_chSel.Location = new Point(22, 61);
            lbl_chSel.Name = "lbl_chSel";
            lbl_chSel.Size = new Size(177, 13);
            lbl_chSel.TabIndex = 703;
            lbl_chSel.Text = "Cheques disponibles/seleccionados";
            // 
            // dg_viewCH
            // 
            dg_viewCH.AllowUserToAddRows = false;
            dg_viewCH.AllowUserToDeleteRows = false;
            dg_viewCH.AllowUserToOrderColumns = true;
            dg_viewCH.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewCH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewCH.ContextMenuStrip = cms_general;
            dg_viewCH.Enabled = false;
            dg_viewCH.Location = new Point(20, 111);
            dg_viewCH.MultiSelect = false;
            dg_viewCH.Name = "dg_viewCH";
            dg_viewCH.ReadOnly = true;
            dg_viewCH.RowHeadersVisible = false;
            dg_viewCH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewCH.Size = new Size(511, 224);
            dg_viewCH.TabIndex = 700;
            // 
            // cms_general
            // 
            cms_general.Items.AddRange(new ToolStripItem[] { EditarToolStripMenuItem, BorrarToolStripMenuItem });
            cms_general.Name = "cms_general";
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
            // cmd_verCheques
            // 
            cmd_verCheques.Enabled = false;
            cmd_verCheques.Location = new Point(404, 20);
            cmd_verCheques.Name = "cmd_verCheques";
            cmd_verCheques.Size = new Size(127, 38);
            cmd_verCheques.TabIndex = 697;
            cmd_verCheques.Text = "Seleccionar cheques";
            cmd_verCheques.UseVisualStyleBackColor = true;
            cmd_verCheques.Visible = false;
            // 
            // cmd_addCheques
            // 
            cmd_addCheques.Enabled = false;
            cmd_addCheques.Location = new Point(404, 346);
            cmd_addCheques.Name = "cmd_addCheques";
            cmd_addCheques.Size = new Size(119, 23);
            cmd_addCheques.TabIndex = 701;
            cmd_addCheques.Text = "Ingresar cheques";
            cmd_addCheques.UseVisualStyleBackColor = true;
            // 
            // chk_cheque
            // 
            chk_cheque.AutoSize = true;
            chk_cheque.Enabled = false;
            chk_cheque.Location = new Point(20, 20);
            chk_cheque.Name = "chk_cheque";
            chk_cheque.Size = new Size(63, 17);
            chk_cheque.TabIndex = 702;
            chk_cheque.Text = "Cheque";
            chk_cheque.UseVisualStyleBackColor = true;
            // 
            // tb_transferencias
            // 
            tb_transferencias.BackColor = SystemColors.Control;
            tb_transferencias.Controls.Add(lbl_totalTransferencia);
            tb_transferencias.Controls.Add(lbl_totalTransferencias);
            tb_transferencias.Controls.Add(lbl_borrarbusquedaTransferencia);
            tb_transferencias.Controls.Add(lbl_buscarTransferencia);
            tb_transferencias.Controls.Add(txt_searchTransferencia);
            tb_transferencias.Controls.Add(dg_viewTransferencia);
            tb_transferencias.Controls.Add(cmd_addTransferencia);
            tb_transferencias.Controls.Add(chk_transferencia);
            tb_transferencias.Location = new Point(4, 22);
            tb_transferencias.Name = "tb_transferencias";
            tb_transferencias.Size = new Size(576, 404);
            tb_transferencias.TabIndex = 3;
            tb_transferencias.Text = "Transferencias";
            // 
            // lbl_totalTransferencia
            // 
            lbl_totalTransferencia.AutoSize = true;
            lbl_totalTransferencia.Location = new Point(131, 351);
            lbl_totalTransferencia.Name = "lbl_totalTransferencia";
            lbl_totalTransferencia.Size = new Size(19, 13);
            lbl_totalTransferencia.TabIndex = 713;
            lbl_totalTransferencia.Text = "$$";
            // 
            // lbl_totalTransferencias
            // 
            lbl_totalTransferencias.AutoSize = true;
            lbl_totalTransferencias.Location = new Point(17, 351);
            lbl_totalTransferencias.Name = "lbl_totalTransferencias";
            lbl_totalTransferencias.Size = new Size(100, 13);
            lbl_totalTransferencias.TabIndex = 712;
            lbl_totalTransferencias.Text = "Total transferencias";
            // 
            // lbl_borrarbusquedaTransferencia
            // 
            lbl_borrarbusquedaTransferencia.Anchor = AnchorStyles.Right;
            lbl_borrarbusquedaTransferencia.AutoSize = true;
            lbl_borrarbusquedaTransferencia.Enabled = false;
            lbl_borrarbusquedaTransferencia.Location = new Point(519, 60);
            lbl_borrarbusquedaTransferencia.Margin = new Padding(2, 0, 2, 0);
            lbl_borrarbusquedaTransferencia.Name = "lbl_borrarbusquedaTransferencia";
            lbl_borrarbusquedaTransferencia.Size = new Size(12, 13);
            lbl_borrarbusquedaTransferencia.TabIndex = 708;
            lbl_borrarbusquedaTransferencia.Text = "x";
            // 
            // lbl_buscarTransferencia
            // 
            lbl_buscarTransferencia.AutoSize = true;
            lbl_buscarTransferencia.Location = new Point(23, 60);
            lbl_buscarTransferencia.Name = "lbl_buscarTransferencia";
            lbl_buscarTransferencia.Size = new Size(104, 13);
            lbl_buscarTransferencia.TabIndex = 711;
            lbl_buscarTransferencia.Text = "Buscar transferencia";
            // 
            // txt_searchTransferencia
            // 
            txt_searchTransferencia.Enabled = false;
            txt_searchTransferencia.Location = new Point(246, 57);
            txt_searchTransferencia.Name = "txt_searchTransferencia";
            txt_searchTransferencia.Size = new Size(268, 20);
            txt_searchTransferencia.TabIndex = 707;
            // 
            // dg_viewTransferencia
            // 
            dg_viewTransferencia.AllowUserToAddRows = false;
            dg_viewTransferencia.AllowUserToDeleteRows = false;
            dg_viewTransferencia.AllowUserToOrderColumns = true;
            dg_viewTransferencia.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewTransferencia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewTransferencia.ContextMenuStrip = cms_general;
            dg_viewTransferencia.Enabled = false;
            dg_viewTransferencia.Location = new Point(20, 83);
            dg_viewTransferencia.MultiSelect = false;
            dg_viewTransferencia.Name = "dg_viewTransferencia";
            dg_viewTransferencia.ReadOnly = true;
            dg_viewTransferencia.RowHeadersVisible = false;
            dg_viewTransferencia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewTransferencia.Size = new Size(511, 224);
            dg_viewTransferencia.TabIndex = 709;
            // 
            // cmd_addTransferencia
            // 
            cmd_addTransferencia.Enabled = false;
            cmd_addTransferencia.Location = new Point(404, 346);
            cmd_addTransferencia.Name = "cmd_addTransferencia";
            cmd_addTransferencia.Size = new Size(119, 23);
            cmd_addTransferencia.TabIndex = 710;
            cmd_addTransferencia.Text = "Ingresar transferencia";
            cmd_addTransferencia.UseVisualStyleBackColor = true;
            // 
            // chk_transferencia
            // 
            chk_transferencia.AutoSize = true;
            chk_transferencia.Enabled = false;
            chk_transferencia.Location = new Point(20, 20);
            chk_transferencia.Name = "chk_transferencia";
            chk_transferencia.Size = new Size(180, 17);
            chk_transferencia.TabIndex = 678;
            chk_transferencia.Text = "Transferencia/depósito bancario";
            chk_transferencia.UseVisualStyleBackColor = true;
            // 
            // tb_retenciones
            // 
            tb_retenciones.BackColor = SystemColors.Control;
            tb_retenciones.Controls.Add(lbl_totalRetencion);
            tb_retenciones.Controls.Add(lbl_totalRetenciones);
            tb_retenciones.Controls.Add(lbl_borrarbusquedaRetencion);
            tb_retenciones.Controls.Add(lbl_buscarRetencion);
            tb_retenciones.Controls.Add(txt_searchRetencion);
            tb_retenciones.Controls.Add(dg_viewRetencion);
            tb_retenciones.Controls.Add(cmd_addRetencion);
            tb_retenciones.Controls.Add(chk_retenciones);
            tb_retenciones.Location = new Point(4, 22);
            tb_retenciones.Name = "tb_retenciones";
            tb_retenciones.Size = new Size(576, 404);
            tb_retenciones.TabIndex = 5;
            tb_retenciones.Text = "Retenciones";
            // 
            // lbl_totalRetencion
            // 
            lbl_totalRetencion.AutoSize = true;
            lbl_totalRetencion.Location = new Point(131, 351);
            lbl_totalRetencion.Name = "lbl_totalRetencion";
            lbl_totalRetencion.Size = new Size(19, 13);
            lbl_totalRetencion.TabIndex = 721;
            lbl_totalRetencion.Text = "$$";
            // 
            // lbl_totalRetenciones
            // 
            lbl_totalRetenciones.AutoSize = true;
            lbl_totalRetenciones.Location = new Point(17, 351);
            lbl_totalRetenciones.Name = "lbl_totalRetenciones";
            lbl_totalRetenciones.Size = new Size(89, 13);
            lbl_totalRetenciones.TabIndex = 720;
            lbl_totalRetenciones.Text = "Total retenciones";
            // 
            // lbl_borrarbusquedaRetencion
            // 
            lbl_borrarbusquedaRetencion.Anchor = AnchorStyles.Right;
            lbl_borrarbusquedaRetencion.AutoSize = true;
            lbl_borrarbusquedaRetencion.Enabled = false;
            lbl_borrarbusquedaRetencion.Location = new Point(519, 60);
            lbl_borrarbusquedaRetencion.Margin = new Padding(2, 0, 2, 0);
            lbl_borrarbusquedaRetencion.Name = "lbl_borrarbusquedaRetencion";
            lbl_borrarbusquedaRetencion.Size = new Size(12, 13);
            lbl_borrarbusquedaRetencion.TabIndex = 716;
            lbl_borrarbusquedaRetencion.Text = "x";
            // 
            // lbl_buscarRetencion
            // 
            lbl_buscarRetencion.AutoSize = true;
            lbl_buscarRetencion.Location = new Point(23, 60);
            lbl_buscarRetencion.Name = "lbl_buscarRetencion";
            lbl_buscarRetencion.Size = new Size(87, 13);
            lbl_buscarRetencion.TabIndex = 719;
            lbl_buscarRetencion.Text = "Buscar retencion";
            // 
            // txt_searchRetencion
            // 
            txt_searchRetencion.Enabled = false;
            txt_searchRetencion.Location = new Point(246, 57);
            txt_searchRetencion.Name = "txt_searchRetencion";
            txt_searchRetencion.Size = new Size(268, 20);
            txt_searchRetencion.TabIndex = 715;
            // 
            // dg_viewRetencion
            // 
            dg_viewRetencion.AllowUserToAddRows = false;
            dg_viewRetencion.AllowUserToDeleteRows = false;
            dg_viewRetencion.AllowUserToOrderColumns = true;
            dg_viewRetencion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewRetencion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewRetencion.ContextMenuStrip = cms_general;
            dg_viewRetencion.Enabled = false;
            dg_viewRetencion.Location = new Point(20, 83);
            dg_viewRetencion.MultiSelect = false;
            dg_viewRetencion.Name = "dg_viewRetencion";
            dg_viewRetencion.ReadOnly = true;
            dg_viewRetencion.RowHeadersVisible = false;
            dg_viewRetencion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewRetencion.Size = new Size(511, 224);
            dg_viewRetencion.TabIndex = 717;
            // 
            // cmd_addRetencion
            // 
            cmd_addRetencion.Enabled = false;
            cmd_addRetencion.Location = new Point(404, 346);
            cmd_addRetencion.Name = "cmd_addRetencion";
            cmd_addRetencion.Size = new Size(119, 23);
            cmd_addRetencion.TabIndex = 718;
            cmd_addRetencion.Text = "Ingresar retencion";
            cmd_addRetencion.UseVisualStyleBackColor = true;
            // 
            // chk_retenciones
            // 
            chk_retenciones.AutoSize = true;
            chk_retenciones.Enabled = false;
            chk_retenciones.Location = new Point(20, 20);
            chk_retenciones.Name = "chk_retenciones";
            chk_retenciones.Size = new Size(86, 17);
            chk_retenciones.TabIndex = 714;
            chk_retenciones.Text = "Retenciones";
            chk_retenciones.UseVisualStyleBackColor = true;
            // 
            // tb_nFC_importe
            // 
            tb_nFC_importe.BackColor = SystemColors.Control;
            tb_nFC_importe.Controls.Add(dg_view_nFC_importes);
            tb_nFC_importe.Controls.Add(lbl_aplicaFc);
            tb_nFC_importe.Location = new Point(4, 22);
            tb_nFC_importe.Name = "tb_nFC_importe";
            tb_nFC_importe.Padding = new Padding(3);
            tb_nFC_importe.Size = new Size(576, 404);
            tb_nFC_importe.TabIndex = 1;
            tb_nFC_importe.Text = "Nº FC / Importe";
            // 
            // dg_view_nFC_importes
            // 
            dg_view_nFC_importes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_nFC_importes.Columns.AddRange(new DataGridViewColumn[] { fecha, factura, importe });
            dg_view_nFC_importes.Enabled = false;
            dg_view_nFC_importes.Location = new Point(23, 47);
            dg_view_nFC_importes.Name = "dg_view_nFC_importes";
            dg_view_nFC_importes.Size = new Size(514, 112);
            dg_view_nFC_importes.TabIndex = 675;
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
            // tb_notas
            // 
            tb_notas.BackColor = SystemColors.Control;
            tb_notas.Controls.Add(txt_notas);
            tb_notas.Controls.Add(lbl_notas);
            tb_notas.Location = new Point(4, 22);
            tb_notas.Name = "tb_notas";
            tb_notas.Size = new Size(576, 404);
            tb_notas.TabIndex = 4;
            tb_notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Enabled = false;
            txt_notas.Location = new Point(20, 49);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(533, 98);
            txt_notas.TabIndex = 675;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(20, 20);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 676;
            lbl_notas.Text = "Notas";
            // 
            // txt_firmante
            // 
            txt_firmante.Location = new Point(88, 554);
            txt_firmante.Name = "txt_firmante";
            txt_firmante.Size = new Size(137, 20);
            txt_firmante.TabIndex = 663;
            txt_firmante.Text = "Bruno Tolfo";
            // 
            // lbl_firmante
            // 
            lbl_firmante.AutoSize = true;
            lbl_firmante.Location = new Point(30, 557);
            lbl_firmante.Name = "lbl_firmante";
            lbl_firmante.Size = new Size(47, 13);
            lbl_firmante.TabIndex = 664;
            lbl_firmante.Text = "Firmante";
            // 
            // add_cobro
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 646);
            Controls.Add(lbl_firmante);
            Controls.Add(txt_firmante);
            Controls.Add(tbControl);
            Controls.Add(lbl_importePago);
            Controls.Add(lbl_pago);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_fechaCarga);
            Controls.Add(lbl_fechaCarga1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "add_cobro";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar cobros";
            tbControl.ResumeLayout(false);
            tb_cobro.ResumeLayout(false);
            tb_cobro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_searchCCCliente).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchCliente).EndInit();
            tb_cheques.ResumeLayout(false);
            tb_cheques.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewCH).EndInit();
            cms_general.ResumeLayout(false);
            tb_transferencias.ResumeLayout(false);
            tb_transferencias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewTransferencia).EndInit();
            tb_retenciones.ResumeLayout(false);
            tb_retenciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewRetencion).EndInit();
            tb_nFC_importe.ResumeLayout(false);
            tb_nFC_importe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dg_view_nFC_importes).EndInit();
            tb_notas.ResumeLayout(false);
            tb_notas.PerformLayout();
            Load += new EventHandler(add_cobro_Load);
            FormClosed += new FormClosedEventHandler(add_cobro_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_fechaCarga1;
        internal Label lbl_fechaCarga;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_importePago;
        internal Label lbl_pago;
        internal Label lbl_aplicaFc;
        internal TabControl tbControl;
        internal TabPage tb_cobro;
        internal Label lblpeso1;
        internal ComboBox cmb_cc;
        internal PictureBox pic_searchCCCliente;
        internal Label lbl_ccc;
        internal TextBox txt_efectivo;
        internal Label lbl_saldo;
        internal Label lbl_saldo1;
        internal Label lbl_comoPaga;
        internal CheckBox chk_efectivo;
        internal ComboBox cmb_cliente;
        internal PictureBox pic_searchCliente;
        internal Label lbl_cliente;
        internal DateTimePicker dtp_fechaCobro;
        internal Label lbl_fechaCobro;
        internal TabPage tb_nFC_importe;
        internal TabPage tb_cheques;
        internal Label lbl_totalCh;
        internal Label lbl_totalCheques;
        internal Label lbl_borrarbusquedaCH;
        internal Label lbl_buscarCheque;
        internal TextBox txt_searchCH;
        internal Label lbl_chSel;
        internal DataGridView dg_viewCH;
        internal Button cmd_verCheques;
        internal Button cmd_addCheques;
        internal CheckBox chk_cheque;
        internal TabPage tb_transferencias;
        internal Label lbl_totalTransferencia;
        internal Label lbl_totalTransferencias;
        internal Label lbl_borrarbusquedaTransferencia;
        internal Label lbl_buscarTransferencia;
        internal TextBox txt_searchTransferencia;
        internal DataGridView dg_viewTransferencia;
        internal Button cmd_addTransferencia;
        internal CheckBox chk_transferencia;
        internal TabPage tb_notas;
        internal TextBox txt_notas;
        internal Label lbl_notas;
        internal ContextMenuStrip cms_general;
        internal ToolStripMenuItem EditarToolStripMenuItem;
        internal ToolStripMenuItem BorrarToolStripMenuItem;
        internal TabPage tb_retenciones;
        internal Label lbl_totalRetencion;
        internal Label lbl_totalRetenciones;
        internal Label lbl_borrarbusquedaRetencion;
        internal Label lbl_buscarRetencion;
        internal TextBox txt_searchRetencion;
        internal DataGridView dg_viewRetencion;
        internal Button cmd_addRetencion;
        internal CheckBox chk_retenciones;
        internal TextBox txt_firmante;
        internal Label lbl_firmante;
        internal DataGridView dg_view_nFC_importes;
        internal DataGridViewTextBoxColumn fecha;
        internal DataGridViewTextBoxColumn factura;
        internal DataGridViewTextBoxColumn importe;
        internal CheckBox chk_cobro_no_oficial;
    }
}


