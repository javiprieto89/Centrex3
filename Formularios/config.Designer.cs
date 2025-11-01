using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class config : Form
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
            gb_regional = new GroupBox();
            rdb_coma = new RadioButton();
            rdb_punto = new RadioButton();
            lbl_decimal = new Label();
            cmd_ok = new Button();
            cmd_exit = new Button();
            tctrl = new TabControl();
            tconfrgl = new TabPage();
            gb_fechaProceso = new GroupBox();
            cmd_cierre_diario = new Button();
            dtp_fecha_sistema = new DateTimePicker();
            gb_paginador = new GroupBox();
            txt_itPerPage = new TextBox();
            Label1 = new Label();
            tpedidos = new TabPage();
            GroupBox2 = new GroupBox();
            lbl_pclientes = new Label();
            cmb_clientes = new ComboBox();
            chk_cliente = new CheckBox();
            tBackup = new TabPage();
            lbl_nombreBackup = new Label();
            txt_nombreBackup = new TextBox();
            cmd_abrirCarpeta = new Button();
            cmd_elegirRuta = new Button();
            txt_rutaBackup = new TextBox();
            lbl_ruta = new Label();
            tsql = new TabPage();
            txtserver = new TextBox();
            txtuser = new TextBox();
            txtpassword = new TextBox();
            lblpassword = new Label();
            txtdb = new TextBox();
            lblruta = new Label();
            lblusuario = new Label();
            lbldb = new Label();
            FolderBrowserDialog1 = new FolderBrowserDialog();
            gb_regional.SuspendLayout();
            tctrl.SuspendLayout();
            tconfrgl.SuspendLayout();
            gb_fechaProceso.SuspendLayout();
            gb_paginador.SuspendLayout();
            tpedidos.SuspendLayout();
            GroupBox2.SuspendLayout();
            tBackup.SuspendLayout();
            tsql.SuspendLayout();
            SuspendLayout();
            // 
            // gb_regional
            // 
            gb_regional.Controls.Add(rdb_coma);
            gb_regional.Controls.Add(rdb_punto);
            gb_regional.Controls.Add(lbl_decimal);
            gb_regional.Location = new Point(25, 25);
            gb_regional.Name = "gb_regional";
            gb_regional.Size = new Size(1008, 163);
            gb_regional.TabIndex = 0;
            gb_regional.TabStop = false;
            gb_regional.Text = "Configuración regional";
            // 
            // rdb_coma
            // 
            rdb_coma.AutoSize = true;
            rdb_coma.Enabled = false;
            rdb_coma.Location = new Point(55, 117);
            rdb_coma.Name = "rdb_coma";
            rdb_coma.Size = new Size(69, 24);
            rdb_coma.TabIndex = 2;
            rdb_coma.Text = "Coma";
            rdb_coma.UseVisualStyleBackColor = true;
            // 
            // rdb_punto
            // 
            rdb_punto.AutoSize = true;
            rdb_punto.Checked = true;
            rdb_punto.Location = new Point(55, 83);
            rdb_punto.Name = "rdb_punto";
            rdb_punto.Size = new Size(68, 24);
            rdb_punto.TabIndex = 1;
            rdb_punto.TabStop = true;
            rdb_punto.Text = "Punto";
            rdb_punto.UseVisualStyleBackColor = true;
            // 
            // lbl_decimal
            // 
            lbl_decimal.AutoSize = true;
            lbl_decimal.Location = new Point(32, 43);
            lbl_decimal.Name = "lbl_decimal";
            lbl_decimal.Size = new Size(135, 20);
            lbl_decimal.TabIndex = 0;
            lbl_decimal.Text = "Separador decimal";
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(361, 769);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(179, 43);
            cmd_ok.TabIndex = 1;
            cmd_ok.Text = "Aceptar";
            cmd_ok.UseVisualStyleBackColor = true;
            cmd_ok.Click += cmd_ok_Click;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(572, 769);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(179, 43);
            cmd_exit.TabIndex = 2;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            cmd_exit.Click += cmd_exit_Click;
            // 
            // tctrl
            // 
            tctrl.Controls.Add(tconfrgl);
            tctrl.Controls.Add(tpedidos);
            tctrl.Controls.Add(tBackup);
            tctrl.Controls.Add(tsql);
            tctrl.Location = new Point(12, 15);
            tctrl.Name = "tctrl";
            tctrl.SelectedIndex = 0;
            tctrl.Size = new Size(1088, 688);
            tctrl.TabIndex = 3;
            tctrl.Selecting += tctrl_Selecting;
            // 
            // tconfrgl
            // 
            tconfrgl.BackColor = SystemColors.Control;
            tconfrgl.Controls.Add(gb_fechaProceso);
            tconfrgl.Controls.Add(gb_paginador);
            tconfrgl.Controls.Add(gb_regional);
            tconfrgl.Location = new Point(4, 29);
            tconfrgl.Name = "tconfrgl";
            tconfrgl.Padding = new Padding(3, 3, 3, 3);
            tconfrgl.Size = new Size(1080, 655);
            tconfrgl.TabIndex = 0;
            tconfrgl.Text = "Miscelaneas";
            // 
            // gb_fechaProceso
            // 
            gb_fechaProceso.Controls.Add(cmd_cierre_diario);
            gb_fechaProceso.Controls.Add(dtp_fecha_sistema);
            gb_fechaProceso.Location = new Point(35, 371);
            gb_fechaProceso.Name = "gb_fechaProceso";
            gb_fechaProceso.Size = new Size(1008, 155);
            gb_fechaProceso.TabIndex = 3;
            gb_fechaProceso.TabStop = false;
            gb_fechaProceso.Text = "Fecha de proceso del sistema";
            // 
            // cmd_cierre_diario
            // 
            cmd_cierre_diario.Location = new Point(36, 86);
            cmd_cierre_diario.Name = "cmd_cierre_diario";
            cmd_cierre_diario.Size = new Size(192, 43);
            cmd_cierre_diario.TabIndex = 4;
            cmd_cierre_diario.Text = "Aceptar";
            cmd_cierre_diario.UseVisualStyleBackColor = true;
            cmd_cierre_diario.Click += cmd_cierre_diario_Click;
            // 
            // dtp_fecha_sistema
            // 
            dtp_fecha_sistema.Location = new Point(36, 40);
            dtp_fecha_sistema.Margin = new Padding(4, 5, 4, 5);
            dtp_fecha_sistema.Name = "dtp_fecha_sistema";
            dtp_fecha_sistema.Size = new Size(191, 27);
            dtp_fecha_sistema.TabIndex = 4;
            // 
            // gb_paginador
            // 
            gb_paginador.Controls.Add(txt_itPerPage);
            gb_paginador.Controls.Add(Label1);
            gb_paginador.Location = new Point(35, 226);
            gb_paginador.Name = "gb_paginador";
            gb_paginador.Size = new Size(1008, 94);
            gb_paginador.TabIndex = 2;
            gb_paginador.TabStop = false;
            gb_paginador.Text = "Paginador";
            // 
            // txt_itPerPage
            // 
            txt_itPerPage.Location = new Point(231, 38);
            txt_itPerPage.Margin = new Padding(4, 5, 4, 5);
            txt_itPerPage.Name = "txt_itPerPage";
            txt_itPerPage.Size = new Size(175, 27);
            txt_itPerPage.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(32, 43);
            Label1.Name = "Label1";
            Label1.Size = new Size(207, 20);
            Label1.TabIndex = 0;
            Label1.Text = "Cantidad de items por página";
            // 
            // tpedidos
            // 
            tpedidos.BackColor = SystemColors.Control;
            tpedidos.Controls.Add(GroupBox2);
            tpedidos.Location = new Point(4, 29);
            tpedidos.Name = "tpedidos";
            tpedidos.Padding = new Padding(3, 3, 3, 3);
            tpedidos.Size = new Size(1080, 655);
            tpedidos.TabIndex = 1;
            tpedidos.Text = "Pedidos";
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(lbl_pclientes);
            GroupBox2.Controls.Add(cmb_clientes);
            GroupBox2.Controls.Add(chk_cliente);
            GroupBox2.Location = new Point(19, 22);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(1008, 163);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Cliente predeterminado en pedidos";
            // 
            // lbl_pclientes
            // 
            lbl_pclientes.AutoSize = true;
            lbl_pclientes.Location = new Point(23, 89);
            lbl_pclientes.Name = "lbl_pclientes";
            lbl_pclientes.Size = new Size(55, 20);
            lbl_pclientes.TabIndex = 2;
            lbl_pclientes.Text = "Cliente";
            // 
            // cmb_clientes
            // 
            cmb_clientes.FormattingEnabled = true;
            cmb_clientes.Location = new Point(80, 89);
            cmb_clientes.Name = "cmb_clientes";
            cmb_clientes.Size = new Size(357, 28);
            cmb_clientes.TabIndex = 1;
            // 
            // chk_cliente
            // 
            chk_cliente.AutoSize = true;
            chk_cliente.Location = new Point(27, 46);
            chk_cliente.Name = "chk_cliente";
            chk_cliente.Size = new Size(220, 24);
            chk_cliente.TabIndex = 0;
            chk_cliente.Text = "Usar cliente predeterminado";
            chk_cliente.UseVisualStyleBackColor = true;
            // 
            // tBackup
            // 
            tBackup.BackColor = SystemColors.Control;
            tBackup.Controls.Add(lbl_nombreBackup);
            tBackup.Controls.Add(txt_nombreBackup);
            tBackup.Controls.Add(cmd_abrirCarpeta);
            tBackup.Controls.Add(cmd_elegirRuta);
            tBackup.Controls.Add(txt_rutaBackup);
            tBackup.Controls.Add(lbl_ruta);
            tBackup.Location = new Point(4, 29);
            tBackup.Name = "tBackup";
            tBackup.Size = new Size(1080, 655);
            tBackup.TabIndex = 2;
            tBackup.Text = "Backup";
            tBackup.Click += tBackup_Click;
            // 
            // lbl_nombreBackup
            // 
            lbl_nombreBackup.AutoSize = true;
            lbl_nombreBackup.Location = new Point(33, 94);
            lbl_nombreBackup.Margin = new Padding(4, 0, 4, 0);
            lbl_nombreBackup.Name = "lbl_nombreBackup";
            lbl_nombreBackup.Size = new Size(218, 20);
            lbl_nombreBackup.TabIndex = 7;
            lbl_nombreBackup.Text = "Nombre del archivo del backup";
            // 
            // txt_nombreBackup
            // 
            txt_nombreBackup.Location = new Point(287, 89);
            txt_nombreBackup.Margin = new Padding(4, 5, 4, 5);
            txt_nombreBackup.Name = "txt_nombreBackup";
            txt_nombreBackup.Size = new Size(329, 27);
            txt_nombreBackup.TabIndex = 6;
            // 
            // cmd_abrirCarpeta
            // 
            cmd_abrirCarpeta.Location = new Point(753, 26);
            cmd_abrirCarpeta.Name = "cmd_abrirCarpeta";
            cmd_abrirCarpeta.Size = new Size(112, 32);
            cmd_abrirCarpeta.TabIndex = 5;
            cmd_abrirCarpeta.Text = "Abrir carpeta actual";
            cmd_abrirCarpeta.UseVisualStyleBackColor = true;
            cmd_abrirCarpeta.Click += cmd_abrirCarpeta_Click;
            // 
            // cmd_elegirRuta
            // 
            cmd_elegirRuta.Location = new Point(636, 26);
            cmd_elegirRuta.Name = "cmd_elegirRuta";
            cmd_elegirRuta.Size = new Size(112, 32);
            cmd_elegirRuta.TabIndex = 2;
            cmd_elegirRuta.Text = "Elegir carpeta";
            cmd_elegirRuta.UseVisualStyleBackColor = true;
            cmd_elegirRuta.Click += cmd_elegirRuta_Click;
            // 
            // txt_rutaBackup
            // 
            txt_rutaBackup.Location = new Point(287, 28);
            txt_rutaBackup.Margin = new Padding(4, 5, 4, 5);
            txt_rutaBackup.Name = "txt_rutaBackup";
            txt_rutaBackup.Size = new Size(329, 27);
            txt_rutaBackup.TabIndex = 1;
            // 
            // lbl_ruta
            // 
            lbl_ruta.AutoSize = true;
            lbl_ruta.Location = new Point(33, 38);
            lbl_ruta.Margin = new Padding(4, 0, 4, 0);
            lbl_ruta.Name = "lbl_ruta";
            lbl_ruta.Size = new Size(240, 20);
            lbl_ruta.TabIndex = 0;
            lbl_ruta.Text = "Ruta donde se almacena el backup";
            // 
            // tsql
            // 
            tsql.BackColor = SystemColors.Control;
            tsql.Controls.Add(txtserver);
            tsql.Controls.Add(txtuser);
            tsql.Controls.Add(txtpassword);
            tsql.Controls.Add(lblpassword);
            tsql.Controls.Add(txtdb);
            tsql.Controls.Add(lblruta);
            tsql.Controls.Add(lblusuario);
            tsql.Controls.Add(lbldb);
            tsql.Location = new Point(4, 29);
            tsql.Name = "tsql";
            tsql.Size = new Size(1080, 655);
            tsql.TabIndex = 3;
            tsql.Text = "SQL";
            // 
            // txtserver
            // 
            txtserver.Location = new Point(197, 100);
            txtserver.Name = "txtserver";
            txtserver.Size = new Size(405, 27);
            txtserver.TabIndex = 7;
            // 
            // txtuser
            // 
            txtuser.Location = new Point(197, 151);
            txtuser.Name = "txtuser";
            txtuser.Size = new Size(405, 27);
            txtuser.TabIndex = 6;
            // 
            // txtpassword
            // 
            txtpassword.Location = new Point(197, 200);
            txtpassword.Name = "txtpassword";
            txtpassword.Size = new Size(405, 27);
            txtpassword.TabIndex = 5;
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Location = new Point(23, 206);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(83, 20);
            lblpassword.TabIndex = 4;
            lblpassword.Text = "Contraseña";
            // 
            // txtdb
            // 
            txtdb.Location = new Point(197, 52);
            txtdb.Name = "txtdb";
            txtdb.Size = new Size(405, 27);
            txtdb.TabIndex = 3;
            // 
            // lblruta
            // 
            lblruta.AutoSize = true;
            lblruta.Location = new Point(23, 106);
            lblruta.Name = "lblruta";
            lblruta.Size = new Size(112, 20);
            lblruta.TabIndex = 2;
            lblruta.Text = "Ruta al servidor";
            // 
            // lblusuario
            // 
            lblusuario.AutoSize = true;
            lblusuario.Location = new Point(23, 157);
            lblusuario.Name = "lblusuario";
            lblusuario.Size = new Size(59, 20);
            lblusuario.TabIndex = 1;
            lblusuario.Text = "Usuario";
            // 
            // lbldb
            // 
            lbldb.AutoSize = true;
            lbldb.Location = new Point(23, 57);
            lbldb.Name = "lbldb";
            lbldb.Size = new Size(136, 20);
            lbldb.TabIndex = 0;
            lbldb.Text = "Nombre de la base";
            // 
            // config
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 866);
            Controls.Add(tctrl);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Name = "config";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configuración";
            Load += config_Load;
            gb_regional.ResumeLayout(false);
            gb_regional.PerformLayout();
            tctrl.ResumeLayout(false);
            tconfrgl.ResumeLayout(false);
            gb_fechaProceso.ResumeLayout(false);
            gb_paginador.ResumeLayout(false);
            gb_paginador.PerformLayout();
            tpedidos.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            tBackup.ResumeLayout(false);
            tBackup.PerformLayout();
            tsql.ResumeLayout(false);
            tsql.PerformLayout();
            ResumeLayout(false);

        }
        internal GroupBox gb_regional;
        internal RadioButton rdb_coma;
        internal RadioButton rdb_punto;
        internal Label lbl_decimal;
        internal Button cmd_ok;
        internal Button cmd_exit;
        internal TabControl tctrl;
        internal TabPage tconfrgl;
        internal TabPage tpedidos;
        internal TabPage tBackup;
        internal GroupBox GroupBox2;
        internal Label lbl_pclientes;
        internal ComboBox cmb_clientes;
        internal CheckBox chk_cliente;
        internal TabPage tsql;
        internal TextBox txtserver;
        internal TextBox txtuser;
        internal TextBox txtpassword;
        internal Label lblpassword;
        internal TextBox txtdb;
        internal Label lblruta;
        internal Label lblusuario;
        internal Label lbldb;
        internal TextBox txt_rutaBackup;
        internal Label lbl_ruta;
        internal Button cmd_elegirRuta;
        internal Button cmd_abrirCarpeta;
        internal Label lbl_nombreBackup;
        internal TextBox txt_nombreBackup;
        internal FolderBrowserDialog FolderBrowserDialog1;
        internal GroupBox gb_paginador;
        internal TextBox txt_itPerPage;
        internal Label Label1;
        internal GroupBox gb_fechaProceso;
        internal DateTimePicker dtp_fecha_sistema;
        internal Button cmd_cierre_diario;
    }
}


