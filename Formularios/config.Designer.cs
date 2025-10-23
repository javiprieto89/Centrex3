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
        private System.ComponentModel.IContainer components;

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
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            tctrl = new TabControl();
            tctrl.Selecting += new TabControlCancelEventHandler(tctrl_Selecting);
            tconfrgl = new TabPage();
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
            cmd_abrirCarpeta.Click += new EventHandler(cmd_abrirCarpeta_Click);
            cmd_elegirRuta = new Button();
            cmd_elegirRuta.Click += new EventHandler(cmd_elegirRuta_Click);
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
            gb_fechaProceso = new GroupBox();
            dtp_fecha_sistema = new DateTimePicker();
            cmd_cierre_diario = new Button();
            cmd_cierre_diario.Click += new EventHandler(cmd_cierre_diario_Click);
            gb_regional.SuspendLayout();
            tctrl.SuspendLayout();
            tconfrgl.SuspendLayout();
            gb_paginador.SuspendLayout();
            tpedidos.SuspendLayout();
            GroupBox2.SuspendLayout();
            tBackup.SuspendLayout();
            tsql.SuspendLayout();
            gb_fechaProceso.SuspendLayout();
            SuspendLayout();
            // 
            // gb_regional
            // 
            gb_regional.Controls.Add(rdb_coma);
            gb_regional.Controls.Add(rdb_punto);
            gb_regional.Controls.Add(lbl_decimal);
            gb_regional.Location = new Point(19, 16);
            gb_regional.Margin = new Padding(2);
            gb_regional.Name = "gb_regional";
            gb_regional.Padding = new Padding(2);
            gb_regional.Size = new Size(756, 106);
            gb_regional.TabIndex = 0;
            gb_regional.TabStop = false;
            gb_regional.Text = "Configuración regional";
            // 
            // rdb_coma
            // 
            rdb_coma.AutoSize = true;
            rdb_coma.Enabled = false;
            rdb_coma.Location = new Point(41, 76);
            rdb_coma.Margin = new Padding(2);
            rdb_coma.Name = "rdb_coma";
            rdb_coma.Size = new Size(52, 17);
            rdb_coma.TabIndex = 2;
            rdb_coma.Text = "Coma";
            rdb_coma.UseVisualStyleBackColor = true;
            // 
            // rdb_punto
            // 
            rdb_punto.AutoSize = true;
            rdb_punto.Checked = true;
            rdb_punto.Location = new Point(41, 54);
            rdb_punto.Margin = new Padding(2);
            rdb_punto.Name = "rdb_punto";
            rdb_punto.Size = new Size(53, 17);
            rdb_punto.TabIndex = 1;
            rdb_punto.TabStop = true;
            rdb_punto.Text = "Punto";
            rdb_punto.UseVisualStyleBackColor = true;
            // 
            // lbl_decimal
            // 
            lbl_decimal.AutoSize = true;
            lbl_decimal.Location = new Point(24, 28);
            lbl_decimal.Margin = new Padding(2, 0, 2, 0);
            lbl_decimal.Name = "lbl_decimal";
            lbl_decimal.Size = new Size(95, 13);
            lbl_decimal.TabIndex = 0;
            lbl_decimal.Text = "Separador decimal";
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(271, 500);
            cmd_ok.Margin = new Padding(2);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(134, 28);
            cmd_ok.TabIndex = 1;
            cmd_ok.Text = "Aceptar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(429, 500);
            cmd_exit.Margin = new Padding(2);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(134, 28);
            cmd_exit.TabIndex = 2;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // tctrl
            // 
            tctrl.Controls.Add(tconfrgl);
            tctrl.Controls.Add(tpedidos);
            tctrl.Controls.Add(tBackup);
            tctrl.Controls.Add(tsql);
            tctrl.Location = new Point(9, 10);
            tctrl.Margin = new Padding(2);
            tctrl.Name = "tctrl";
            tctrl.SelectedIndex = 0;
            tctrl.Size = new Size(816, 447);
            tctrl.TabIndex = 3;
            // 
            // tconfrgl
            // 
            tconfrgl.BackColor = SystemColors.Control;
            tconfrgl.Controls.Add(gb_fechaProceso);
            tconfrgl.Controls.Add(gb_paginador);
            tconfrgl.Controls.Add(gb_regional);
            tconfrgl.Location = new Point(4, 22);
            tconfrgl.Margin = new Padding(2);
            tconfrgl.Name = "tconfrgl";
            tconfrgl.Padding = new Padding(2);
            tconfrgl.Size = new Size(808, 421);
            tconfrgl.TabIndex = 0;
            tconfrgl.Text = "Miscelaneas";
            // 
            // gb_paginador
            // 
            gb_paginador.Controls.Add(txt_itPerPage);
            gb_paginador.Controls.Add(Label1);
            gb_paginador.Location = new Point(26, 147);
            gb_paginador.Margin = new Padding(2);
            gb_paginador.Name = "gb_paginador";
            gb_paginador.Padding = new Padding(2);
            gb_paginador.Size = new Size(756, 61);
            gb_paginador.TabIndex = 2;
            gb_paginador.TabStop = false;
            gb_paginador.Text = "Paginador";
            // 
            // txt_itPerPage
            // 
            txt_itPerPage.Location = new Point(173, 25);
            txt_itPerPage.Name = "txt_itPerPage";
            txt_itPerPage.Size = new Size(132, 20);
            txt_itPerPage.TabIndex = 1;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(24, 28);
            Label1.Margin = new Padding(2, 0, 2, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(144, 13);
            Label1.TabIndex = 0;
            Label1.Text = "Cantidad de items por página";
            // 
            // tpedidos
            // 
            tpedidos.BackColor = SystemColors.Control;
            tpedidos.Controls.Add(GroupBox2);
            tpedidos.Location = new Point(4, 22);
            tpedidos.Margin = new Padding(2);
            tpedidos.Name = "tpedidos";
            tpedidos.Padding = new Padding(2);
            tpedidos.Size = new Size(808, 421);
            tpedidos.TabIndex = 1;
            tpedidos.Text = "Pedidos";
            // 
            // GroupBox2
            // 
            GroupBox2.Controls.Add(lbl_pclientes);
            GroupBox2.Controls.Add(cmb_clientes);
            GroupBox2.Controls.Add(chk_cliente);
            GroupBox2.Location = new Point(14, 14);
            GroupBox2.Margin = new Padding(2);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Padding = new Padding(2);
            GroupBox2.Size = new Size(756, 106);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Cliente predeterminado en pedidos";
            // 
            // lbl_pclientes
            // 
            lbl_pclientes.AutoSize = true;
            lbl_pclientes.Location = new Point(17, 58);
            lbl_pclientes.Margin = new Padding(2, 0, 2, 0);
            lbl_pclientes.Name = "lbl_pclientes";
            lbl_pclientes.Size = new Size(39, 13);
            lbl_pclientes.TabIndex = 2;
            lbl_pclientes.Text = "Cliente";
            // 
            // cmb_clientes
            // 
            cmb_clientes.FormattingEnabled = true;
            cmb_clientes.Location = new Point(60, 58);
            cmb_clientes.Margin = new Padding(2);
            cmb_clientes.Name = "cmb_clientes";
            cmb_clientes.Size = new Size(269, 21);
            cmb_clientes.TabIndex = 1;
            // 
            // chk_cliente
            // 
            chk_cliente.AutoSize = true;
            chk_cliente.Location = new Point(20, 30);
            chk_cliente.Margin = new Padding(2);
            chk_cliente.Name = "chk_cliente";
            chk_cliente.Size = new Size(158, 17);
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
            tBackup.Location = new Point(4, 22);
            tBackup.Margin = new Padding(2);
            tBackup.Name = "tBackup";
            tBackup.Size = new Size(808, 421);
            tBackup.TabIndex = 2;
            tBackup.Text = "Backup";
            // 
            // lbl_nombreBackup
            // 
            lbl_nombreBackup.AutoSize = true;
            lbl_nombreBackup.Location = new Point(25, 61);
            lbl_nombreBackup.Name = "lbl_nombreBackup";
            lbl_nombreBackup.Size = new Size(155, 13);
            lbl_nombreBackup.TabIndex = 7;
            lbl_nombreBackup.Text = "Nombre del archivo del backup";
            // 
            // txt_nombreBackup
            // 
            txt_nombreBackup.Location = new Point(215, 58);
            txt_nombreBackup.Name = "txt_nombreBackup";
            txt_nombreBackup.Size = new Size(248, 20);
            txt_nombreBackup.TabIndex = 6;
            // 
            // cmd_abrirCarpeta
            // 
            cmd_abrirCarpeta.Location = new Point(565, 17);
            cmd_abrirCarpeta.Margin = new Padding(2);
            cmd_abrirCarpeta.Name = "cmd_abrirCarpeta";
            cmd_abrirCarpeta.Size = new Size(84, 21);
            cmd_abrirCarpeta.TabIndex = 5;
            cmd_abrirCarpeta.Text = "Abrir carpeta actual";
            cmd_abrirCarpeta.UseVisualStyleBackColor = true;
            // 
            // cmd_elegirRuta
            // 
            cmd_elegirRuta.Location = new Point(477, 17);
            cmd_elegirRuta.Margin = new Padding(2);
            cmd_elegirRuta.Name = "cmd_elegirRuta";
            cmd_elegirRuta.Size = new Size(84, 21);
            cmd_elegirRuta.TabIndex = 2;
            cmd_elegirRuta.Text = "Elegir carpeta";
            cmd_elegirRuta.UseVisualStyleBackColor = true;
            // 
            // txt_rutaBackup
            // 
            txt_rutaBackup.Location = new Point(215, 18);
            txt_rutaBackup.Name = "txt_rutaBackup";
            txt_rutaBackup.Size = new Size(248, 20);
            txt_rutaBackup.TabIndex = 1;
            // 
            // lbl_ruta
            // 
            lbl_ruta.AutoSize = true;
            lbl_ruta.Location = new Point(25, 25);
            lbl_ruta.Name = "lbl_ruta";
            lbl_ruta.Size = new Size(176, 13);
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
            tsql.Location = new Point(4, 22);
            tsql.Margin = new Padding(2);
            tsql.Name = "tsql";
            tsql.Size = new Size(808, 421);
            tsql.TabIndex = 3;
            tsql.Text = "SQL";
            // 
            // txtserver
            // 
            txtserver.Location = new Point(148, 65);
            txtserver.Margin = new Padding(2);
            txtserver.Name = "txtserver";
            txtserver.Size = new Size(305, 20);
            txtserver.TabIndex = 7;
            // 
            // txtuser
            // 
            txtuser.Location = new Point(148, 98);
            txtuser.Margin = new Padding(2);
            txtuser.Name = "txtuser";
            txtuser.Size = new Size(305, 20);
            txtuser.TabIndex = 6;
            // 
            // txtpassword
            // 
            txtpassword.Location = new Point(148, 130);
            txtpassword.Margin = new Padding(2);
            txtpassword.Name = "txtpassword";
            txtpassword.Size = new Size(305, 20);
            txtpassword.TabIndex = 5;
            // 
            // lblpassword
            // 
            lblpassword.AutoSize = true;
            lblpassword.Location = new Point(17, 134);
            lblpassword.Margin = new Padding(2, 0, 2, 0);
            lblpassword.Name = "lblpassword";
            lblpassword.Size = new Size(61, 13);
            lblpassword.TabIndex = 4;
            lblpassword.Text = "Contraseña";
            // 
            // txtdb
            // 
            txtdb.Location = new Point(148, 34);
            txtdb.Margin = new Padding(2);
            txtdb.Name = "txtdb";
            txtdb.Size = new Size(305, 20);
            txtdb.TabIndex = 3;
            // 
            // lblruta
            // 
            lblruta.AutoSize = true;
            lblruta.Location = new Point(17, 69);
            lblruta.Margin = new Padding(2, 0, 2, 0);
            lblruta.Name = "lblruta";
            lblruta.Size = new Size(81, 13);
            lblruta.TabIndex = 2;
            lblruta.Text = "Ruta al servidor";
            // 
            // lblusuario
            // 
            lblusuario.AutoSize = true;
            lblusuario.Location = new Point(17, 102);
            lblusuario.Margin = new Padding(2, 0, 2, 0);
            lblusuario.Name = "lblusuario";
            lblusuario.Size = new Size(43, 13);
            lblusuario.TabIndex = 1;
            lblusuario.Text = "Usuario";
            // 
            // lbldb
            // 
            lbldb.AutoSize = true;
            lbldb.Location = new Point(17, 37);
            lbldb.Margin = new Padding(2, 0, 2, 0);
            lbldb.Name = "lbldb";
            lbldb.Size = new Size(96, 13);
            lbldb.TabIndex = 0;
            lbldb.Text = "Nombre de la base";
            // 
            // gb_fechaProceso
            // 
            gb_fechaProceso.Controls.Add(cmd_cierre_diario);
            gb_fechaProceso.Controls.Add(dtp_fecha_sistema);
            gb_fechaProceso.Location = new Point(26, 241);
            gb_fechaProceso.Margin = new Padding(2);
            gb_fechaProceso.Name = "gb_fechaProceso";
            gb_fechaProceso.Padding = new Padding(2);
            gb_fechaProceso.Size = new Size(756, 101);
            gb_fechaProceso.TabIndex = 3;
            gb_fechaProceso.TabStop = false;
            gb_fechaProceso.Text = "Fecha de proceso del sistema";
            // 
            // dtp_fecha_sistema
            // 
            dtp_fecha_sistema.Location = new Point(27, 26);
            dtp_fecha_sistema.Name = "dtp_fecha_sistema";
            dtp_fecha_sistema.Size = new Size(144, 20);
            dtp_fecha_sistema.TabIndex = 4;
            // 
            // cmd_cierre_diario
            // 
            cmd_cierre_diario.Location = new Point(27, 56);
            cmd_cierre_diario.Margin = new Padding(2);
            cmd_cierre_diario.Name = "cmd_cierre_diario";
            cmd_cierre_diario.Size = new Size(144, 28);
            cmd_cierre_diario.TabIndex = 4;
            cmd_cierre_diario.Text = "Aceptar";
            cmd_cierre_diario.UseVisualStyleBackColor = true;
            // 
            // config
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 563);
            Controls.Add(tctrl);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Margin = new Padding(2);
            Name = "config";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Configuración";
            gb_regional.ResumeLayout(false);
            gb_regional.PerformLayout();
            tctrl.ResumeLayout(false);
            tconfrgl.ResumeLayout(false);
            gb_paginador.ResumeLayout(false);
            gb_paginador.PerformLayout();
            tpedidos.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            tBackup.ResumeLayout(false);
            tBackup.PerformLayout();
            tsql.ResumeLayout(false);
            tsql.PerformLayout();
            gb_fechaProceso.ResumeLayout(false);
            Load += new EventHandler(config_Load);
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


