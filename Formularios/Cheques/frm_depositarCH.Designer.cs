using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frm_depositarCH : Form
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
            cmb_banco = new ComboBox();
            cmb_banco.SelectionChangeCommitted += new EventHandler(cmb_banco_SelectionChangeCommitted);
            lbl_Banco = new Label();
            lbl_nCH = new Label();
            txt_nCH_cartera = new TextBox();
            txt_nCH_cartera.KeyDown += new KeyEventHandler(txt_nCH_cartera_KeyDown);
            txt_nCH_cartera.Leave += new EventHandler(txt_nCH_cartera_Leave);
            lbl_importeCH_cartera = new Label();
            txt_importeCH_cartera = new TextBox();
            txt_importeCH_cartera.KeyDown += new KeyEventHandler(txt_importeCH_cartera_KeyDown);
            txt_importeCH_cartera.Leave += new EventHandler(txt_importeCH_cartera_Leave);
            dg_view_chCartera = new DataGridView();
            cmd_depositar = new Button();
            cmd_depositar.Click += new EventHandler(cmd_depositar_Click);
            dg_view_chDepositados = new DataGridView();
            cmd_filtrarCH_cartera = new Button();
            cmd_filtrarCH_cartera.Click += new EventHandler(cmd_filtrarCH_cartera_Click);
            group_chCartera = new GroupBox();
            gp_cartera = new GroupBox();
            lbl_desde_cartera = new Label();
            dtp_desde_cartera = new DateTimePicker();
            lbl_hasta_cartera = new Label();
            dtp_hasta_cartera = new DateTimePicker();
            chk_hastaSiempre_cartera = new CheckBox();
            chk_desdeSiempre_cartera = new CheckBox();
            cmd_go_cartera = new Button();
            cmd_go_cartera.Click += new EventHandler(cmd_go_Click_cartera);
            txt_nPage_cartera = new TextBox();
            txt_nPage_cartera.KeyDown += new KeyEventHandler(txt_nPage_KeyDown_cartera);
            txt_nPage_cartera.Click += new EventHandler(txt_nPage_Click_cartera);
            cmd_last_cartera = new Button();
            cmd_last_cartera.Click += new EventHandler(cmd_last_Click_cartera);
            cmd_next_cartera = new Button();
            cmd_next_cartera.Click += new EventHandler(cmd_next_Click_cartera);
            cmd_prev_cartera = new Button();
            cmd_prev_cartera.Click += new EventHandler(cmd_prev_Click_cartera);
            cmd_first_cartera = new Button();
            cmd_first_cartera.Click += new EventHandler(cmd_first_Click_cartera);
            group_chDepositados = new GroupBox();
            gp_depositado = new GroupBox();
            lbl_desde_depositado = new Label();
            chk_hastaSiempre_depositado = new CheckBox();
            chk_hastaSiempre_depositado.CheckedChanged += new EventHandler(chk_hastaSiempre_depositado_CheckedChanged);
            chk_hastaSiempre_depositado.Leave += new EventHandler(chk_hastaSiempre_depositado_Leave);
            dtp_hasta_depositado = new DateTimePicker();
            dtp_hasta_depositado.Leave += new EventHandler(dtp_hasta_depositado_Leave);
            chk_desdeSiempre_depositado = new CheckBox();
            chk_desdeSiempre_depositado.CheckedChanged += new EventHandler(chk_desdeSiempre_depositado_CheckedChanged);
            chk_desdeSiempre_depositado.Leave += new EventHandler(chk_desdeSiempre_depositado_Leave);
            lbl_hasta_depositado = new Label();
            dtp_desde_depositado = new DateTimePicker();
            dtp_desde_depositado.Leave += new EventHandler(dtp_desde_depositado_Leave);
            cmb_cuentaBancaria = new ComboBox();
            cmb_cuentaBancaria.Leave += new EventHandler(cmb_cuentaBancaria_Leave);
            cmb_cuentaBancaria.SelectionChangeCommitted += new EventHandler(cmb_cuentaBancaria_SelectionChangeCommitted);
            lbl_CuentaBancaria = new Label();
            cmd_go_depositado = new Button();
            cmd_go_depositado.Click += new EventHandler(cmd_go_Click_depositado);
            txt_nPage_depositado = new TextBox();
            txt_nPage_depositado.KeyDown += new KeyEventHandler(txt_nPage_KeyDown_depositado);
            txt_nPage_depositado.Click += new EventHandler(txt_nPage_Click_depositado);
            cmd_last_depositado = new Button();
            cmd_last_depositado.Click += new EventHandler(cmd_last_Click_depositado);
            cmd_next_depositado = new Button();
            cmd_next_depositado.Click += new EventHandler(cmd_next_Click_depositado);
            cmd_prev_depositado = new Button();
            cmd_prev_depositado.Click += new EventHandler(cmd_prev_Click_depositado);
            cmd_first_depositado = new Button();
            cmd_first_depositado.Click += new EventHandler(cmd_first_Click_depositado);
            cmd_filtrarCH_depositado = new Button();
            txt_nCH_depositado = new TextBox();
            txt_nCH_depositado.KeyDown += new KeyEventHandler(txt_nCH_depositado_KeyDown);
            txt_nCH_depositado.Leave += new EventHandler(txt_nCH_depositado_Leave);
            Label4 = new Label();
            lbl_importeCH_depositado = new Label();
            txt_importeCH_depositado = new TextBox();
            txt_importeCH_depositado.KeyDown += new KeyEventHandler(txt_importeCH_depositado_KeyDown);
            txt_importeCH_depositado.Leave += new EventHandler(txt_importeCH_depositado_Leave);
            cmd_acreditar = new Button();
            cmd_anularDeposito = new Button();
            cmd_anularDeposito.Click += new EventHandler(cmd_anularDeposito_Click);
            ((System.ComponentModel.ISupportInitialize)dg_view_chCartera).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dg_view_chDepositados).BeginInit();
            group_chCartera.SuspendLayout();
            gp_cartera.SuspendLayout();
            group_chDepositados.SuspendLayout();
            gp_depositado.SuspendLayout();
            SuspendLayout();
            // 
            // cmb_banco
            // 
            cmb_banco.FormattingEnabled = true;
            cmb_banco.Location = new Point(141, 26);
            cmb_banco.Name = "cmb_banco";
            cmb_banco.Size = new Size(263, 21);
            cmb_banco.TabIndex = 0;
            // 
            // lbl_Banco
            // 
            lbl_Banco.AutoSize = true;
            lbl_Banco.Location = new Point(12, 34);
            lbl_Banco.Name = "lbl_Banco";
            lbl_Banco.Size = new Size(38, 13);
            lbl_Banco.TabIndex = 1;
            lbl_Banco.Text = "Banco";
            // 
            // lbl_nCH
            // 
            lbl_nCH.AutoSize = true;
            lbl_nCH.Location = new Point(18, 169);
            lbl_nCH.Name = "lbl_nCH";
            lbl_nCH.Size = new Size(98, 13);
            lbl_nCH.TabIndex = 129;
            lbl_nCH.Text = "Número de cheque";
            // 
            // txt_nCH_cartera
            // 
            txt_nCH_cartera.Location = new Point(127, 168);
            txt_nCH_cartera.Name = "txt_nCH_cartera";
            txt_nCH_cartera.Size = new Size(263, 20);
            txt_nCH_cartera.TabIndex = 130;
            // 
            // lbl_importeCH_cartera
            // 
            lbl_importeCH_cartera.AutoSize = true;
            lbl_importeCH_cartera.Location = new Point(18, 226);
            lbl_importeCH_cartera.Name = "lbl_importeCH_cartera";
            lbl_importeCH_cartera.Size = new Size(98, 13);
            lbl_importeCH_cartera.TabIndex = 131;
            lbl_importeCH_cartera.Text = "Importe del cheque";
            // 
            // txt_importeCH_cartera
            // 
            txt_importeCH_cartera.Location = new Point(127, 225);
            txt_importeCH_cartera.Name = "txt_importeCH_cartera";
            txt_importeCH_cartera.Size = new Size(263, 20);
            txt_importeCH_cartera.TabIndex = 132;
            // 
            // dg_view_chCartera
            // 
            dg_view_chCartera.AllowUserToAddRows = false;
            dg_view_chCartera.AllowUserToDeleteRows = false;
            dg_view_chCartera.AllowUserToOrderColumns = true;
            dg_view_chCartera.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view_chCartera.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_chCartera.Location = new Point(436, 19);
            dg_view_chCartera.Name = "dg_view_chCartera";
            dg_view_chCartera.ReadOnly = true;
            dg_view_chCartera.RowHeadersVisible = false;
            dg_view_chCartera.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view_chCartera.Size = new Size(624, 253);
            dg_view_chCartera.TabIndex = 133;
            // 
            // cmd_depositar
            // 
            cmd_depositar.Location = new Point(218, 346);
            cmd_depositar.Name = "cmd_depositar";
            cmd_depositar.Size = new Size(140, 49);
            cmd_depositar.TabIndex = 135;
            cmd_depositar.Text = "Depositar cheques seleccionados";
            cmd_depositar.UseVisualStyleBackColor = true;
            // 
            // dg_view_chDepositados
            // 
            dg_view_chDepositados.AllowUserToAddRows = false;
            dg_view_chDepositados.AllowUserToDeleteRows = false;
            dg_view_chDepositados.AllowUserToOrderColumns = true;
            dg_view_chDepositados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view_chDepositados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view_chDepositados.Location = new Point(425, 16);
            dg_view_chDepositados.Name = "dg_view_chDepositados";
            dg_view_chDepositados.ReadOnly = true;
            dg_view_chDepositados.RowHeadersVisible = false;
            dg_view_chDepositados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view_chDepositados.Size = new Size(639, 277);
            dg_view_chDepositados.TabIndex = 136;
            // 
            // cmd_filtrarCH_cartera
            // 
            cmd_filtrarCH_cartera.Location = new Point(21, 275);
            cmd_filtrarCH_cartera.Name = "cmd_filtrarCH_cartera";
            cmd_filtrarCH_cartera.Size = new Size(147, 23);
            cmd_filtrarCH_cartera.TabIndex = 138;
            cmd_filtrarCH_cartera.Text = "Filtrar cheques";
            cmd_filtrarCH_cartera.UseVisualStyleBackColor = true;
            // 
            // group_chCartera
            // 
            group_chCartera.Controls.Add(gp_cartera);
            group_chCartera.Controls.Add(cmd_go_cartera);
            group_chCartera.Controls.Add(txt_nPage_cartera);
            group_chCartera.Controls.Add(cmd_last_cartera);
            group_chCartera.Controls.Add(cmd_next_cartera);
            group_chCartera.Controls.Add(cmd_prev_cartera);
            group_chCartera.Controls.Add(cmd_first_cartera);
            group_chCartera.Controls.Add(dg_view_chCartera);
            group_chCartera.Controls.Add(cmd_filtrarCH_cartera);
            group_chCartera.Controls.Add(txt_importeCH_cartera);
            group_chCartera.Controls.Add(lbl_importeCH_cartera);
            group_chCartera.Controls.Add(lbl_nCH);
            group_chCartera.Controls.Add(txt_nCH_cartera);
            group_chCartera.Location = new Point(16, 12);
            group_chCartera.Name = "group_chCartera";
            group_chCartera.Size = new Size(1083, 322);
            group_chCartera.TabIndex = 139;
            group_chCartera.TabStop = false;
            group_chCartera.Text = "Cheques en cartera";
            // 
            // gp_cartera
            // 
            gp_cartera.Controls.Add(lbl_desde_cartera);
            gp_cartera.Controls.Add(dtp_desde_cartera);
            gp_cartera.Controls.Add(lbl_hasta_cartera);
            gp_cartera.Controls.Add(dtp_hasta_cartera);
            gp_cartera.Controls.Add(chk_hastaSiempre_cartera);
            gp_cartera.Controls.Add(chk_desdeSiempre_cartera);
            gp_cartera.Location = new Point(6, 30);
            gp_cartera.Name = "gp_cartera";
            gp_cartera.Size = new Size(406, 117);
            gp_cartera.TabIndex = 145;
            gp_cartera.TabStop = false;
            gp_cartera.Text = "Fecha de cobro";
            // 
            // lbl_desde_cartera
            // 
            lbl_desde_cartera.AutoSize = true;
            lbl_desde_cartera.Location = new Point(55, 27);
            lbl_desde_cartera.Name = "lbl_desde_cartera";
            lbl_desde_cartera.Size = new Size(38, 13);
            lbl_desde_cartera.TabIndex = 130;
            lbl_desde_cartera.Text = "Desde";
            // 
            // dtp_desde_cartera
            // 
            dtp_desde_cartera.Enabled = false;
            dtp_desde_cartera.Location = new Point(122, 26);
            dtp_desde_cartera.Name = "dtp_desde_cartera";
            dtp_desde_cartera.Size = new Size(263, 20);
            dtp_desde_cartera.TabIndex = 129;
            // 
            // lbl_hasta_cartera
            // 
            lbl_hasta_cartera.AutoSize = true;
            lbl_hasta_cartera.Location = new Point(55, 79);
            lbl_hasta_cartera.Name = "lbl_hasta_cartera";
            lbl_hasta_cartera.Size = new Size(35, 13);
            lbl_hasta_cartera.TabIndex = 132;
            lbl_hasta_cartera.Text = "Hasta";
            // 
            // dtp_hasta_cartera
            // 
            dtp_hasta_cartera.Enabled = false;
            dtp_hasta_cartera.Location = new Point(122, 78);
            dtp_hasta_cartera.Name = "dtp_hasta_cartera";
            dtp_hasta_cartera.Size = new Size(263, 20);
            dtp_hasta_cartera.TabIndex = 131;
            // 
            // chk_hastaSiempre_cartera
            // 
            chk_hastaSiempre_cartera.AutoSize = true;
            chk_hastaSiempre_cartera.Location = new Point(13, 76);
            chk_hastaSiempre_cartera.Name = "chk_hastaSiempre_cartera";
            chk_hastaSiempre_cartera.Size = new Size(15, 14);
            chk_hastaSiempre_cartera.TabIndex = 133;
            chk_hastaSiempre_cartera.UseVisualStyleBackColor = true;
            // 
            // chk_desdeSiempre_cartera
            // 
            chk_desdeSiempre_cartera.AutoSize = true;
            chk_desdeSiempre_cartera.Location = new Point(13, 26);
            chk_desdeSiempre_cartera.Name = "chk_desdeSiempre_cartera";
            chk_desdeSiempre_cartera.Size = new Size(15, 14);
            chk_desdeSiempre_cartera.TabIndex = 134;
            chk_desdeSiempre_cartera.UseVisualStyleBackColor = true;
            // 
            // cmd_go_cartera
            // 
            cmd_go_cartera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_go_cartera.Location = new Point(667, 278);
            cmd_go_cartera.Name = "cmd_go_cartera";
            cmd_go_cartera.Size = new Size(29, 20);
            cmd_go_cartera.TabIndex = 144;
            cmd_go_cartera.Text = "Ir";
            cmd_go_cartera.UseVisualStyleBackColor = true;
            // 
            // txt_nPage_cartera
            // 
            txt_nPage_cartera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nPage_cartera.Location = new Point(598, 278);
            txt_nPage_cartera.Name = "txt_nPage_cartera";
            txt_nPage_cartera.Size = new Size(63, 20);
            txt_nPage_cartera.TabIndex = 143;
            // 
            // cmd_last_cartera
            // 
            cmd_last_cartera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_last_cartera.Location = new Point(563, 278);
            cmd_last_cartera.Name = "cmd_last_cartera";
            cmd_last_cartera.Size = new Size(29, 20);
            cmd_last_cartera.TabIndex = 142;
            cmd_last_cartera.Text = ">>|";
            cmd_last_cartera.UseVisualStyleBackColor = true;
            // 
            // cmd_next_cartera
            // 
            cmd_next_cartera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_next_cartera.Location = new Point(517, 278);
            cmd_next_cartera.Name = "cmd_next_cartera";
            cmd_next_cartera.Size = new Size(40, 20);
            cmd_next_cartera.TabIndex = 141;
            cmd_next_cartera.Text = ">>";
            cmd_next_cartera.UseVisualStyleBackColor = true;
            // 
            // cmd_prev_cartera
            // 
            cmd_prev_cartera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_prev_cartera.Location = new Point(471, 278);
            cmd_prev_cartera.Name = "cmd_prev_cartera";
            cmd_prev_cartera.Size = new Size(40, 20);
            cmd_prev_cartera.TabIndex = 140;
            cmd_prev_cartera.Text = "<<";
            cmd_prev_cartera.UseVisualStyleBackColor = true;
            // 
            // cmd_first_cartera
            // 
            cmd_first_cartera.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_first_cartera.Location = new Point(436, 278);
            cmd_first_cartera.Name = "cmd_first_cartera";
            cmd_first_cartera.Size = new Size(29, 20);
            cmd_first_cartera.TabIndex = 139;
            cmd_first_cartera.Text = "|<<";
            cmd_first_cartera.UseVisualStyleBackColor = true;
            // 
            // group_chDepositados
            // 
            group_chDepositados.Controls.Add(gp_depositado);
            group_chDepositados.Controls.Add(cmb_cuentaBancaria);
            group_chDepositados.Controls.Add(lbl_CuentaBancaria);
            group_chDepositados.Controls.Add(cmd_go_depositado);
            group_chDepositados.Controls.Add(txt_nPage_depositado);
            group_chDepositados.Controls.Add(dg_view_chDepositados);
            group_chDepositados.Controls.Add(cmd_last_depositado);
            group_chDepositados.Controls.Add(cmd_next_depositado);
            group_chDepositados.Controls.Add(cmd_prev_depositado);
            group_chDepositados.Controls.Add(cmd_first_depositado);
            group_chDepositados.Controls.Add(cmb_banco);
            group_chDepositados.Controls.Add(lbl_Banco);
            group_chDepositados.Controls.Add(cmd_filtrarCH_depositado);
            group_chDepositados.Controls.Add(txt_nCH_depositado);
            group_chDepositados.Controls.Add(Label4);
            group_chDepositados.Controls.Add(lbl_importeCH_depositado);
            group_chDepositados.Controls.Add(txt_importeCH_depositado);
            group_chDepositados.Location = new Point(12, 404);
            group_chDepositados.Name = "group_chDepositados";
            group_chDepositados.Size = new Size(1087, 354);
            group_chDepositados.TabIndex = 140;
            group_chDepositados.TabStop = false;
            group_chDepositados.Text = "Cheques depositados";
            // 
            // gp_depositado
            // 
            gp_depositado.Controls.Add(lbl_desde_depositado);
            gp_depositado.Controls.Add(chk_hastaSiempre_depositado);
            gp_depositado.Controls.Add(dtp_hasta_depositado);
            gp_depositado.Controls.Add(chk_desdeSiempre_depositado);
            gp_depositado.Controls.Add(lbl_hasta_depositado);
            gp_depositado.Controls.Add(dtp_desde_depositado);
            gp_depositado.Location = new Point(4, 108);
            gp_depositado.Name = "gp_depositado";
            gp_depositado.Size = new Size(412, 109);
            gp_depositado.TabIndex = 143;
            gp_depositado.TabStop = false;
            gp_depositado.Text = "Fecha de depósito";
            // 
            // lbl_desde_depositado
            // 
            lbl_desde_depositado.AutoSize = true;
            lbl_desde_depositado.Location = new Point(56, 30);
            lbl_desde_depositado.Name = "lbl_desde_depositado";
            lbl_desde_depositado.Size = new Size(38, 13);
            lbl_desde_depositado.TabIndex = 146;
            lbl_desde_depositado.Text = "Desde";
            // 
            // chk_hastaSiempre_depositado
            // 
            chk_hastaSiempre_depositado.AutoSize = true;
            chk_hastaSiempre_depositado.Location = new Point(28, 67);
            chk_hastaSiempre_depositado.Name = "chk_hastaSiempre_depositado";
            chk_hastaSiempre_depositado.Size = new Size(15, 14);
            chk_hastaSiempre_depositado.TabIndex = 149;
            chk_hastaSiempre_depositado.UseVisualStyleBackColor = true;
            // 
            // dtp_hasta_depositado
            // 
            dtp_hasta_depositado.Enabled = false;
            dtp_hasta_depositado.Location = new Point(137, 70);
            dtp_hasta_depositado.Name = "dtp_hasta_depositado";
            dtp_hasta_depositado.Size = new Size(263, 20);
            dtp_hasta_depositado.TabIndex = 147;
            // 
            // chk_desdeSiempre_depositado
            // 
            chk_desdeSiempre_depositado.AutoSize = true;
            chk_desdeSiempre_depositado.Location = new Point(28, 29);
            chk_desdeSiempre_depositado.Name = "chk_desdeSiempre_depositado";
            chk_desdeSiempre_depositado.Size = new Size(15, 14);
            chk_desdeSiempre_depositado.TabIndex = 150;
            chk_desdeSiempre_depositado.UseVisualStyleBackColor = true;
            // 
            // lbl_hasta_depositado
            // 
            lbl_hasta_depositado.AutoSize = true;
            lbl_hasta_depositado.Location = new Point(62, 70);
            lbl_hasta_depositado.Name = "lbl_hasta_depositado";
            lbl_hasta_depositado.Size = new Size(35, 13);
            lbl_hasta_depositado.TabIndex = 148;
            lbl_hasta_depositado.Text = "Hasta";
            // 
            // dtp_desde_depositado
            // 
            dtp_desde_depositado.Enabled = false;
            dtp_desde_depositado.Location = new Point(137, 29);
            dtp_desde_depositado.Name = "dtp_desde_depositado";
            dtp_desde_depositado.Size = new Size(263, 20);
            dtp_desde_depositado.TabIndex = 145;
            // 
            // cmb_cuentaBancaria
            // 
            cmb_cuentaBancaria.FormattingEnabled = true;
            cmb_cuentaBancaria.Location = new Point(141, 74);
            cmb_cuentaBancaria.Name = "cmb_cuentaBancaria";
            cmb_cuentaBancaria.Size = new Size(263, 21);
            cmb_cuentaBancaria.TabIndex = 162;
            // 
            // lbl_CuentaBancaria
            // 
            lbl_CuentaBancaria.AutoSize = true;
            lbl_CuentaBancaria.Location = new Point(12, 74);
            lbl_CuentaBancaria.Name = "lbl_CuentaBancaria";
            lbl_CuentaBancaria.Size = new Size(85, 13);
            lbl_CuentaBancaria.TabIndex = 163;
            lbl_CuentaBancaria.Text = "Cuenta bancaria";
            // 
            // cmd_go_depositado
            // 
            cmd_go_depositado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_go_depositado.Location = new Point(656, 303);
            cmd_go_depositado.Name = "cmd_go_depositado";
            cmd_go_depositado.Size = new Size(29, 20);
            cmd_go_depositado.TabIndex = 161;
            cmd_go_depositado.Text = "Ir";
            cmd_go_depositado.UseVisualStyleBackColor = true;
            // 
            // txt_nPage_depositado
            // 
            txt_nPage_depositado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nPage_depositado.Location = new Point(587, 303);
            txt_nPage_depositado.Name = "txt_nPage_depositado";
            txt_nPage_depositado.Size = new Size(63, 20);
            txt_nPage_depositado.TabIndex = 160;
            // 
            // cmd_last_depositado
            // 
            cmd_last_depositado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_last_depositado.Location = new Point(552, 303);
            cmd_last_depositado.Name = "cmd_last_depositado";
            cmd_last_depositado.Size = new Size(29, 20);
            cmd_last_depositado.TabIndex = 159;
            cmd_last_depositado.Text = ">>|";
            cmd_last_depositado.UseVisualStyleBackColor = true;
            // 
            // cmd_next_depositado
            // 
            cmd_next_depositado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_next_depositado.Location = new Point(506, 303);
            cmd_next_depositado.Name = "cmd_next_depositado";
            cmd_next_depositado.Size = new Size(40, 20);
            cmd_next_depositado.TabIndex = 158;
            cmd_next_depositado.Text = ">>";
            cmd_next_depositado.UseVisualStyleBackColor = true;
            // 
            // cmd_prev_depositado
            // 
            cmd_prev_depositado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_prev_depositado.Location = new Point(460, 303);
            cmd_prev_depositado.Name = "cmd_prev_depositado";
            cmd_prev_depositado.Size = new Size(40, 20);
            cmd_prev_depositado.TabIndex = 157;
            cmd_prev_depositado.Text = "<<";
            cmd_prev_depositado.UseVisualStyleBackColor = true;
            // 
            // cmd_first_depositado
            // 
            cmd_first_depositado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_first_depositado.Location = new Point(425, 303);
            cmd_first_depositado.Name = "cmd_first_depositado";
            cmd_first_depositado.Size = new Size(29, 20);
            cmd_first_depositado.TabIndex = 156;
            cmd_first_depositado.Text = "|<<";
            cmd_first_depositado.UseVisualStyleBackColor = true;
            // 
            // cmd_filtrarCH_depositado
            // 
            cmd_filtrarCH_depositado.Location = new Point(15, 312);
            cmd_filtrarCH_depositado.Name = "cmd_filtrarCH_depositado";
            cmd_filtrarCH_depositado.Size = new Size(147, 23);
            cmd_filtrarCH_depositado.TabIndex = 155;
            cmd_filtrarCH_depositado.Text = "Filtrar cheques";
            cmd_filtrarCH_depositado.UseVisualStyleBackColor = true;
            // 
            // txt_nCH_depositado
            // 
            txt_nCH_depositado.Location = new Point(141, 232);
            txt_nCH_depositado.Name = "txt_nCH_depositado";
            txt_nCH_depositado.Size = new Size(263, 20);
            txt_nCH_depositado.TabIndex = 152;
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(7, 232);
            Label4.Name = "Label4";
            Label4.Size = new Size(98, 13);
            Label4.TabIndex = 151;
            Label4.Text = "Número de cheque";
            // 
            // lbl_importeCH_depositado
            // 
            lbl_importeCH_depositado.AutoSize = true;
            lbl_importeCH_depositado.Location = new Point(6, 273);
            lbl_importeCH_depositado.Name = "lbl_importeCH_depositado";
            lbl_importeCH_depositado.Size = new Size(98, 13);
            lbl_importeCH_depositado.TabIndex = 153;
            lbl_importeCH_depositado.Text = "Importe del cheque";
            // 
            // txt_importeCH_depositado
            // 
            txt_importeCH_depositado.Location = new Point(141, 273);
            txt_importeCH_depositado.Name = "txt_importeCH_depositado";
            txt_importeCH_depositado.Size = new Size(263, 20);
            txt_importeCH_depositado.TabIndex = 154;
            // 
            // cmd_acreditar
            // 
            cmd_acreditar.Location = new Point(508, 346);
            cmd_acreditar.Name = "cmd_acreditar";
            cmd_acreditar.Size = new Size(140, 49);
            cmd_acreditar.TabIndex = 141;
            cmd_acreditar.Text = "Acreditar cheques seleccionados";
            cmd_acreditar.UseVisualStyleBackColor = true;
            // 
            // cmd_anularDeposito
            // 
            cmd_anularDeposito.Location = new Point(764, 346);
            cmd_anularDeposito.Name = "cmd_anularDeposito";
            cmd_anularDeposito.Size = new Size(140, 49);
            cmd_anularDeposito.TabIndex = 142;
            cmd_anularDeposito.Text = "Anular depósito";
            cmd_anularDeposito.UseVisualStyleBackColor = true;
            // 
            // frm_depositarCH
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1123, 806);
            Controls.Add(cmd_anularDeposito);
            Controls.Add(cmd_acreditar);
            Controls.Add(group_chDepositados);
            Controls.Add(group_chCartera);
            Controls.Add(cmd_depositar);
            Name = "frm_depositarCH";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Depositar cheque";
            ((System.ComponentModel.ISupportInitialize)dg_view_chCartera).EndInit();
            ((System.ComponentModel.ISupportInitialize)dg_view_chDepositados).EndInit();
            group_chCartera.ResumeLayout(false);
            group_chCartera.PerformLayout();
            gp_cartera.ResumeLayout(false);
            gp_cartera.PerformLayout();
            group_chDepositados.ResumeLayout(false);
            group_chDepositados.PerformLayout();
            gp_depositado.ResumeLayout(false);
            gp_depositado.PerformLayout();
            Load += new EventHandler(frm_depositarCH_Load);
            FormClosed += new FormClosedEventHandler(frm_depositarCH_FormClosed);
            ResumeLayout(false);

        }

        internal ComboBox cmb_banco;
        internal Label lbl_Banco;
        internal Label lbl_nCH;
        internal TextBox txt_nCH_cartera;
        internal Label lbl_importeCH_cartera;
        internal TextBox txt_importeCH_cartera;
        internal DataGridView dg_view_chCartera;
        internal Button cmd_depositar;
        internal DataGridView dg_view_chDepositados;
        internal Button cmd_filtrarCH_cartera;
        internal GroupBox group_chCartera;
        internal GroupBox group_chDepositados;
        internal Button cmd_acreditar;
        internal Button cmd_anularDeposito;
        internal Button cmd_go_cartera;
        internal TextBox txt_nPage_cartera;
        internal Button cmd_last_cartera;
        internal Button cmd_next_cartera;
        internal Button cmd_prev_cartera;
        internal Button cmd_first_cartera;
        internal DateTimePicker dtp_desde_depositado;
        internal Button cmd_filtrarCH_depositado;
        internal TextBox txt_nCH_depositado;
        internal Label lbl_desde_depositado;
        internal Label Label4;
        internal Label lbl_importeCH_depositado;
        internal Label lbl_hasta_depositado;
        internal CheckBox chk_desdeSiempre_depositado;
        internal DateTimePicker dtp_hasta_depositado;
        internal TextBox txt_importeCH_depositado;
        internal CheckBox chk_hastaSiempre_depositado;
        internal Button cmd_go_depositado;
        internal TextBox txt_nPage_depositado;
        internal Button cmd_last_depositado;
        internal Button cmd_next_depositado;
        internal Button cmd_prev_depositado;
        internal Button cmd_first_depositado;
        internal ComboBox cmb_cuentaBancaria;
        internal Label lbl_CuentaBancaria;
        internal GroupBox gp_cartera;
        internal Label lbl_desde_cartera;
        internal DateTimePicker dtp_desde_cartera;
        internal Label lbl_hasta_cartera;
        internal DateTimePicker dtp_hasta_cartera;
        internal CheckBox chk_hastaSiempre_cartera;
        internal CheckBox chk_desdeSiempre_cartera;
        internal GroupBox gp_depositado;
    }
}


