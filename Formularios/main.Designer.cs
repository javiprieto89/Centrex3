using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class main : Form
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
            TreeNode treeNode1 = new TreeNode("Condiciones de compra");
            TreeNode treeNode2 = new TreeNode("Conceptos de compra");
            TreeNode treeNode3 = new TreeNode("Compras", new TreeNode[] { treeNode1, treeNode2 });
            TreeNode treeNode4 = new TreeNode("Clientes");
            TreeNode treeNode5 = new TreeNode("CC. Clientes");
            TreeNode treeNode6 = new TreeNode("Clientes", new TreeNode[] { treeNode4, treeNode5 });
            TreeNode treeNode7 = new TreeNode("Proveedores");
            TreeNode treeNode8 = new TreeNode("CC. Proveedores");
            TreeNode treeNode9 = new TreeNode("Proveedores", new TreeNode[] { treeNode7, treeNode8 });
            TreeNode treeNode10 = new TreeNode("Marcas");
            TreeNode treeNode11 = new TreeNode("Categorías");
            TreeNode treeNode12 = new TreeNode("Productos");
            TreeNode treeNode13 = new TreeNode("Productos asociados");
            TreeNode treeNode14 = new TreeNode("Productos", new TreeNode[] { treeNode10, treeNode11, treeNode12, treeNode13 });
            TreeNode treeNode15 = new TreeNode("Comprobantes");
            TreeNode treeNode16 = new TreeNode("Consultas personalizadas");
            TreeNode treeNode17 = new TreeNode("Caja");
            TreeNode treeNode18 = new TreeNode("Bancos");
            TreeNode treeNode19 = new TreeNode("Cuentas bancarias");
            TreeNode treeNode20 = new TreeNode("Bancos", new TreeNode[] { treeNode18, treeNode19 });
            TreeNode treeNode21 = new TreeNode("Cheques recibidos");
            TreeNode treeNode22 = new TreeNode("Cheques emitidos");
            TreeNode treeNode23 = new TreeNode("Cartera de cheques");
            TreeNode treeNode24 = new TreeNode("Depositar ch. recibidos");
            TreeNode treeNode25 = new TreeNode("Rechazar ch. recibidos");
            TreeNode treeNode26 = new TreeNode("Cheques", new TreeNode[] { treeNode21, treeNode22, treeNode23, treeNode24, treeNode25 });
            TreeNode treeNode27 = new TreeNode("Impuestos");
            TreeNode treeNode28 = new TreeNode("Items - Impuestos");
            TreeNode treeNode29 = new TreeNode("Impuestos", new TreeNode[] { treeNode27, treeNode28 });
            TreeNode treeNode30 = new TreeNode("Archivos", new TreeNode[] { treeNode3, treeNode6, treeNode9, treeNode14, treeNode15, treeNode16, treeNode17, treeNode20, treeNode26, treeNode29 });
            TreeNode treeNode31 = new TreeNode("Ordenes de compra");
            TreeNode treeNode32 = new TreeNode("Comprobantes de compras");
            TreeNode treeNode33 = new TreeNode("Pagos");
            TreeNode treeNode34 = new TreeNode("Compras", new TreeNode[] { treeNode31, treeNode32, treeNode33 });
            TreeNode treeNode35 = new TreeNode("Ajustes de stock");
            TreeNode treeNode36 = new TreeNode("Ingreso de mercadería");
            TreeNode treeNode37 = new TreeNode("Stock", new TreeNode[] { treeNode35, treeNode36 });
            TreeNode treeNode38 = new TreeNode("Producción");
            TreeNode treeNode39 = new TreeNode("Nuevo pedido");
            TreeNode treeNode40 = new TreeNode("Pedidos");
            TreeNode treeNode41 = new TreeNode("Cobros");
            TreeNode treeNode42 = new TreeNode("Ventas", new TreeNode[] { treeNode39, treeNode40, treeNode41 });
            TreeNode treeNode43 = new TreeNode("Exportaciones S.I.A.p");
            TreeNode treeNode44 = new TreeNode("Stock");
            TreeNode treeNode45 = new TreeNode("Movimientos de stock");
            TreeNode treeNode46 = new TreeNode("Productos", new TreeNode[] { treeNode44, treeNode45 });
            TreeNode treeNode47 = new TreeNode("CC. Proveedores");
            TreeNode treeNode48 = new TreeNode("Proveedores", new TreeNode[] { treeNode47 });
            TreeNode treeNode49 = new TreeNode("CC. Clientes");
            TreeNode treeNode50 = new TreeNode("Clientes", new TreeNode[] { treeNode49 });
            TreeNode treeNode51 = new TreeNode("Último comprobante");
            TreeNode treeNode52 = new TreeNode("Información factura");
            TreeNode treeNode53 = new TreeNode("Pruebas AFIP");
            TreeNode treeNode54 = new TreeNode("MercadoPago QR");
            TreeNode treeNode55 = new TreeNode("Factura electrónica", new TreeNode[] { treeNode51, treeNode52, treeNode53, treeNode54 });
            TreeNode treeNode56 = new TreeNode("Personalizadas");
            TreeNode treeNode57 = new TreeNode("Exportaciones S.I.A.p");
            TreeNode treeNode58 = new TreeNode("Consultas", new TreeNode[] { treeNode46, treeNode48, treeNode50, treeNode55, treeNode56, treeNode57 });
            TreeNode treeNode59 = new TreeNode("Configuración");
            TreeNode treeNode60 = new TreeNode("Permisos");
            TreeNode treeNode61 = new TreeNode("Perfiles");
            TreeNode treeNode62 = new TreeNode("Usuarios");
            TreeNode treeNode63 = new TreeNode("Asignar permisos a perfiles");
            TreeNode treeNode64 = new TreeNode("Asignar perfiles a usuarios");
            TreeNode treeNode65 = new TreeNode("Seguridad", new TreeNode[] { treeNode59, treeNode60, treeNode61, treeNode62, treeNode63, treeNode64 });
            TreeNode treeNode66 = new TreeNode("Configuración", new TreeNode[] { treeNode65 });
            TreeNode treeNode67 = new TreeNode("Acerca de...");
            TreeNode treeNode68 = new TreeNode("Centrex", new TreeNode[] { treeNode30, treeNode34, treeNode37, treeNode38, treeNode42, treeNode43, treeNode58, treeNode66, treeNode67 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            cmd_add = new Button();
            lbl_clientes = new Label();
            lbl_proveedores = new Label();
            lbl_items = new Label();
            lbl_pedidos = new Label();
            lbl_usuarios = new Label();
            cmd_search = new Button();
            cmd_refresh = new Button();
            lblbusqueda = new Label();
            chk_historicos = new CheckBox();
            ToolTip1 = new ToolTip(components);
            lbl_borrarbusqueda = new Label();
            Treev = new TreeView();
            cmsPreciosMasivo = new ContextMenuStrip(components);
            ActualizaciónMasivaDePreciosToolStripMenuItem = new ToolStripMenuItem();
            cmd_pedido = new Button();
            cmd_addcliente = new Button();
            chk_rpt = new CheckBox();
            tooltip_advanceseach = new ToolTip(components);
            dg_view = new DataGridView();
            cmsGeneral = new ContextMenuStrip(components);
            EditarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem = new ToolStripMenuItem();
            TerminarPedidoToolStripMenuItem = new ToolStripMenuItem();
            DeshabilitarItemToolStripMenuItem = new ToolStripMenuItem();
            MostrarFacturaToolStripMenuItem = new ToolStripMenuItem();
            DuplicarPedidoToolStripMenuItem = new ToolStripMenuItem();
            MostrarInformaciónDeAFIPToolStripMenuItem = new ToolStripMenuItem();
            AnularToolStripMenuItem = new ToolStripMenuItem();
            StatusStrip1 = new StatusStrip();
            tss_version = new ToolStripStatusLabel();
            tss_separador1 = new ToolStripStatusLabel();
            tss_dbInfo = new ToolStripStatusLabel();
            tss_separador2 = new ToolStripStatusLabel();
            tss_usuario_logueado = new ToolStripStatusLabel();
            tss_separador3 = new ToolStripStatusLabel();
            tss_hora = new ToolStripStatusLabel();
            Timer1 = new Timer(components);
            chk_test = new CheckBox();
            BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            cmd_go = new Button();
            txt_nPage = new TextBox();
            cmd_last = new Button();
            cmd_next = new Button();
            cmd_prev = new Button();
            cmd_first = new Button();
            txt_search = new TextBox();
            pic_search = new PictureBox();
            pic = new PictureBox();
            Button1 = new Button();
            tick_closeProgram = new Timer(components);
            cmsPreciosMasivo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_view).BeginInit();
            cmsGeneral.SuspendLayout();
            StatusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_search).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            // 
            // cmd_add
            // 
            cmd_add.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_add.Location = new Point(27, 1085);
            cmd_add.Margin = new Padding(4, 5, 4, 5);
            cmd_add.Name = "cmd_add";
            cmd_add.Size = new Size(284, 65);
            cmd_add.TabIndex = 3;
            cmd_add.Text = "Agregar";
            cmd_add.UseVisualStyleBackColor = true;
            cmd_add.Click += cmd_add_Click;
            // 
            // lbl_clientes
            // 
            lbl_clientes.Location = new Point(0, 0);
            lbl_clientes.Margin = new Padding(4, 0, 4, 0);
            lbl_clientes.Name = "lbl_clientes";
            lbl_clientes.Size = new Size(133, 35);
            lbl_clientes.TabIndex = 71;
            // 
            // lbl_proveedores
            // 
            lbl_proveedores.Location = new Point(0, 0);
            lbl_proveedores.Margin = new Padding(4, 0, 4, 0);
            lbl_proveedores.Name = "lbl_proveedores";
            lbl_proveedores.Size = new Size(133, 35);
            lbl_proveedores.TabIndex = 72;
            // 
            // lbl_items
            // 
            lbl_items.Location = new Point(0, 0);
            lbl_items.Margin = new Padding(4, 0, 4, 0);
            lbl_items.Name = "lbl_items";
            lbl_items.Size = new Size(133, 35);
            lbl_items.TabIndex = 73;
            // 
            // lbl_pedidos
            // 
            lbl_pedidos.Location = new Point(0, 0);
            lbl_pedidos.Margin = new Padding(4, 0, 4, 0);
            lbl_pedidos.Name = "lbl_pedidos";
            lbl_pedidos.Size = new Size(133, 35);
            lbl_pedidos.TabIndex = 74;
            // 
            // lbl_usuarios
            // 
            lbl_usuarios.Location = new Point(0, 0);
            lbl_usuarios.Margin = new Padding(4, 0, 4, 0);
            lbl_usuarios.Name = "lbl_usuarios";
            lbl_usuarios.Size = new Size(133, 35);
            lbl_usuarios.TabIndex = 75;
            // 
            // cmd_search
            // 
            cmd_search.Location = new Point(0, 0);
            cmd_search.Margin = new Padding(4, 5, 4, 5);
            cmd_search.Name = "cmd_search";
            cmd_search.Size = new Size(100, 35);
            cmd_search.TabIndex = 76;
            // 
            // cmd_refresh
            // 
            cmd_refresh.Location = new Point(0, 0);
            cmd_refresh.Margin = new Padding(4, 5, 4, 5);
            cmd_refresh.Name = "cmd_refresh";
            cmd_refresh.Size = new Size(100, 35);
            cmd_refresh.TabIndex = 77;
            // 
            // lblbusqueda
            // 
            lblbusqueda.AutoSize = true;
            lblbusqueda.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblbusqueda.Location = new Point(17, 45);
            lblbusqueda.Margin = new Padding(4, 0, 4, 0);
            lblbusqueda.Name = "lblbusqueda";
            lblbusqueda.Size = new Size(80, 17);
            lblbusqueda.TabIndex = 18;
            lblbusqueda.Text = "Búsqueda";
            // 
            // chk_historicos
            // 
            chk_historicos.AutoSize = true;
            chk_historicos.Location = new Point(15, 102);
            chk_historicos.Margin = new Padding(3, 2, 3, 2);
            chk_historicos.Name = "chk_historicos";
            chk_historicos.Size = new Size(183, 24);
            chk_historicos.TabIndex = 20;
            chk_historicos.Text = "Ver históricos/inactivos";
            chk_historicos.UseVisualStyleBackColor = true;
            chk_historicos.CheckedChanged += chk_historicos_CheckedChanged;
            // 
            // lbl_borrarbusqueda
            // 
            lbl_borrarbusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_borrarbusqueda.AutoSize = true;
            lbl_borrarbusqueda.Location = new Point(1431, 45);
            lbl_borrarbusqueda.Name = "lbl_borrarbusqueda";
            lbl_borrarbusqueda.Size = new Size(16, 20);
            lbl_borrarbusqueda.TabIndex = 68;
            lbl_borrarbusqueda.Text = "x";
            ToolTip1.SetToolTip(lbl_borrarbusqueda, "Borrar búsqueda");
            // 
            // Treev
            // 
            Treev.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Treev.ContextMenuStrip = cmsPreciosMasivo;
            Treev.Location = new Point(15, 135);
            Treev.Margin = new Padding(3, 2, 3, 2);
            Treev.Name = "Treev";
            treeNode1.Name = "condiciones_compra";
            treeNode1.Text = "Condiciones de compra";
            treeNode2.Name = "conceptos_compra";
            treeNode2.Text = "Conceptos de compra";
            treeNode3.Name = "archivo_compras";
            treeNode3.Text = "Compras";
            treeNode4.Name = "clientes";
            treeNode4.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode4.Text = "Clientes";
            treeNode5.Name = "archivoCCClientes";
            treeNode5.Text = "CC. Clientes";
            treeNode6.Name = "archivoClientes";
            treeNode6.Text = "Clientes";
            treeNode7.Name = "proveedores";
            treeNode7.Text = "Proveedores";
            treeNode8.Name = "archivoCCProveedores";
            treeNode8.Text = "CC. Proveedores";
            treeNode9.Name = "archivoProveedores";
            treeNode9.Text = "Proveedores";
            treeNode10.Name = "marcas_items";
            treeNode10.Text = "Marcas";
            treeNode11.Name = "tipos_items";
            treeNode11.Text = "Categorías";
            treeNode12.Name = "items";
            treeNode12.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode12.Text = "Productos";
            treeNode13.Name = "asocItems";
            treeNode13.Text = "Productos asociados";
            treeNode14.Name = "archivoitems";
            treeNode14.Text = "Productos";
            treeNode15.Name = "comprobantes";
            treeNode15.Text = "Comprobantes";
            treeNode16.Name = "archivoconsultas";
            treeNode16.Text = "Consultas personalizadas";
            treeNode17.Name = "caja";
            treeNode17.Text = "Caja";
            treeNode18.Name = "bancos";
            treeNode18.Text = "Bancos";
            treeNode19.Name = "cuentas_bancarias";
            treeNode19.Text = "Cuentas bancarias";
            treeNode20.Name = "archivoBancos";
            treeNode20.Text = "Bancos";
            treeNode21.Name = "chRecibidos";
            treeNode21.Text = "Cheques recibidos";
            treeNode22.Name = "chEmitidos";
            treeNode22.Text = "Cheques emitidos";
            treeNode23.Name = "chCartera";
            treeNode23.Text = "Cartera de cheques";
            treeNode24.Name = "depositarCH";
            treeNode24.Text = "Depositar ch. recibidos";
            treeNode25.Name = "rechazarCH";
            treeNode25.Text = "Rechazar ch. recibidos";
            treeNode26.Name = "cheques";
            treeNode26.Text = "Cheques";
            treeNode27.Name = "impuestos";
            treeNode27.Text = "Impuestos";
            treeNode28.Name = "itemsImpuestos";
            treeNode28.Text = "Items - Impuestos";
            treeNode29.Name = "archivoImpuestos";
            treeNode29.Text = "Impuestos";
            treeNode30.Name = "archivos";
            treeNode30.Text = "Archivos";
            treeNode31.Name = "ordenesCompras";
            treeNode31.Text = "Ordenes de compra";
            treeNode32.Name = "comprobantes_compras";
            treeNode32.Text = "Comprobantes de compras";
            treeNode33.Name = "pagos";
            treeNode33.Text = "Pagos";
            treeNode34.Name = "archivocompras";
            treeNode34.Text = "Compras";
            treeNode35.Name = "ajustes_stock";
            treeNode35.Text = "Ajustes de stock";
            treeNode36.Name = "registros_stock";
            treeNode36.Text = "Ingreso de mercadería";
            treeNode37.Name = "stock_menu";
            treeNode37.Text = "Stock";
            treeNode38.Name = "produccion";
            treeNode38.Text = "Producción";
            treeNode39.Name = "nuevopedido";
            treeNode39.Text = "Nuevo pedido";
            treeNode40.Name = "pedidos";
            treeNode40.NodeFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            treeNode40.Text = "Pedidos";
            treeNode41.Name = "cobros";
            treeNode41.Text = "Cobros";
            treeNode42.Name = "ventas";
            treeNode42.Text = "Ventas";
            treeNode43.Name = "exportSiap";
            treeNode43.Text = "Exportaciones S.I.A.p";
            treeNode44.Name = "stock";
            treeNode44.Text = "Stock";
            treeNode45.Name = "movStock";
            treeNode45.Text = "Movimientos de stock";
            treeNode46.Name = "consultasProductos";
            treeNode46.Text = "Productos";
            treeNode47.Name = "ccProveedores";
            treeNode47.Text = "CC. Proveedores";
            treeNode48.Name = "consultasProveedores";
            treeNode48.Text = "Proveedores";
            treeNode49.Name = "ccClientes";
            treeNode49.Text = "CC. Clientes";
            treeNode50.Name = "consultasClientes";
            treeNode50.Text = "Clientes";
            treeNode51.Name = "ultimoComprobante";
            treeNode51.Text = "Último comprobante";
            treeNode52.Name = "info_fc";
            treeNode52.Text = "Información factura";
            treeNode53.Name = "pruebasAFIP";
            treeNode53.Text = "Pruebas AFIP";
            treeNode54.Name = "mercadopagoQR";
            treeNode54.Text = "MercadoPago QR";
            treeNode55.Name = "consultasFE";
            treeNode55.Text = "Factura electrónica";
            treeNode56.Name = "cpersonalizadas";
            treeNode56.Text = "Personalizadas";
            treeNode57.Name = "exportSiap";
            treeNode57.Text = "Exportaciones S.I.A.p";
            treeNode58.Name = "consultas";
            treeNode58.Text = "Consultas";
            treeNode59.Name = "configuracion";
            treeNode59.Text = "Configuración";
            treeNode60.Name = "permisos";
            treeNode60.Text = "Permisos";
            treeNode61.Name = "perfiles";
            treeNode61.Text = "Perfiles";
            treeNode62.Name = "usuarios";
            treeNode62.Text = "Usuarios";
            treeNode63.Name = "permisos_a_perfiles";
            treeNode63.Text = "Asignar permisos a perfiles";
            treeNode64.Name = "perfiles_a_usuarios";
            treeNode64.Text = "Asignar perfiles a usuarios";
            treeNode65.Name = "seguridad";
            treeNode65.Text = "Seguridad";
            treeNode66.Name = "cfg_menu";
            treeNode66.Text = "Configuración";
            treeNode67.Name = "acercade";
            treeNode67.Text = "Acerca de...";
            treeNode68.Name = "root";
            treeNode68.Text = "Centrex";
            Treev.Nodes.AddRange(new TreeNode[] { treeNode68 });
            Treev.Size = new Size(295, 922);
            Treev.TabIndex = 23;
            Treev.NodeMouseClick += Treev_NodeMouseClick;
            Treev.MouseClick += Treev_MouseClick;
            // 
            // cmsPreciosMasivo
            // 
            cmsPreciosMasivo.ImageScalingSize = new Size(20, 20);
            cmsPreciosMasivo.Items.AddRange(new ToolStripItem[] { ActualizaciónMasivaDePreciosToolStripMenuItem });
            cmsPreciosMasivo.Name = "ContextMenuStrip1";
            cmsPreciosMasivo.Size = new Size(291, 28);
            // 
            // ActualizaciónMasivaDePreciosToolStripMenuItem
            // 
            ActualizaciónMasivaDePreciosToolStripMenuItem.Name = "ActualizaciónMasivaDePreciosToolStripMenuItem";
            ActualizaciónMasivaDePreciosToolStripMenuItem.Size = new Size(290, 24);
            ActualizaciónMasivaDePreciosToolStripMenuItem.Text = "Actualización masiva de precios";
            // 
            // cmd_pedido
            // 
            cmd_pedido.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_pedido.Location = new Point(317, 1085);
            cmd_pedido.Margin = new Padding(4, 5, 4, 5);
            cmd_pedido.Name = "cmd_pedido";
            cmd_pedido.Size = new Size(284, 65);
            cmd_pedido.TabIndex = 25;
            cmd_pedido.Text = "Nuevo pedido";
            cmd_pedido.UseVisualStyleBackColor = true;
            cmd_pedido.Click += cmd_pedido_Click;
            // 
            // cmd_addcliente
            // 
            cmd_addcliente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_addcliente.Location = new Point(608, 1085);
            cmd_addcliente.Margin = new Padding(4, 5, 4, 5);
            cmd_addcliente.Name = "cmd_addcliente";
            cmd_addcliente.Size = new Size(284, 65);
            cmd_addcliente.TabIndex = 28;
            cmd_addcliente.Text = "Nuevo cliente";
            cmd_addcliente.UseVisualStyleBackColor = true;
            cmd_addcliente.Click += cmd_addcliente_Click;
            // 
            // chk_rpt
            // 
            chk_rpt.AutoSize = true;
            chk_rpt.Checked = true;
            chk_rpt.CheckState = CheckState.Checked;
            chk_rpt.Location = new Point(203, 102);
            chk_rpt.Margin = new Padding(4, 5, 4, 5);
            chk_rpt.Name = "chk_rpt";
            chk_rpt.Size = new Size(152, 24);
            chk_rpt.TabIndex = 50;
            chk_rpt.Text = "Mostrar impresión";
            chk_rpt.UseVisualStyleBackColor = true;
            chk_rpt.CheckedChanged += chk_rpt_CheckedChanged;
            // 
            // tooltip_advanceseach
            // 
            tooltip_advanceseach.ForeColor = Color.Red;
            // 
            // dg_view
            // 
            dg_view.AllowUserToAddRows = false;
            dg_view.AllowUserToDeleteRows = false;
            dg_view.AllowUserToOrderColumns = true;
            dg_view.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dg_view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_view.ContextMenuStrip = cmsGeneral;
            dg_view.EnableHeadersVisualStyles = false;
            dg_view.Location = new Point(317, 139);
            dg_view.Margin = new Padding(4, 5, 4, 5);
            dg_view.MultiSelect = false;
            dg_view.Name = "dg_view";
            dg_view.ReadOnly = true;
            dg_view.RowHeadersVisible = false;
            dg_view.RowHeadersWidth = 51;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.Size = new Size(1188, 920);
            dg_view.TabIndex = 53;
            dg_view.CellMouseDoubleClick += dg_view_CellMouseDoubleClick;
            // 
            // cmsGeneral
            // 
            cmsGeneral.ImageScalingSize = new Size(28, 28);
            cmsGeneral.Items.AddRange(new ToolStripItem[] { EditarToolStripMenuItem, BorrarToolStripMenuItem, TerminarPedidoToolStripMenuItem, DeshabilitarItemToolStripMenuItem, MostrarFacturaToolStripMenuItem, DuplicarPedidoToolStripMenuItem, MostrarInformaciónDeAFIPToolStripMenuItem, AnularToolStripMenuItem });
            cmsGeneral.Name = "ContextMenuStrip";
            cmsGeneral.Size = new Size(268, 196);
            // 
            // EditarToolStripMenuItem
            // 
            EditarToolStripMenuItem.Name = "EditarToolStripMenuItem";
            EditarToolStripMenuItem.Size = new Size(267, 24);
            EditarToolStripMenuItem.Text = "Editar";
            EditarToolStripMenuItem.Visible = false;
            // 
            // BorrarToolStripMenuItem
            // 
            BorrarToolStripMenuItem.Name = "BorrarToolStripMenuItem";
            BorrarToolStripMenuItem.Size = new Size(267, 24);
            BorrarToolStripMenuItem.Text = "Borrar";
            // 
            // TerminarPedidoToolStripMenuItem
            // 
            TerminarPedidoToolStripMenuItem.Name = "TerminarPedidoToolStripMenuItem";
            TerminarPedidoToolStripMenuItem.Size = new Size(267, 24);
            TerminarPedidoToolStripMenuItem.Text = "Cerrar pedido";
            // 
            // DeshabilitarItemToolStripMenuItem
            // 
            DeshabilitarItemToolStripMenuItem.Name = "DeshabilitarItemToolStripMenuItem";
            DeshabilitarItemToolStripMenuItem.Size = new Size(267, 24);
            DeshabilitarItemToolStripMenuItem.Text = "Desactivar item";
            // 
            // MostrarFacturaToolStripMenuItem
            // 
            MostrarFacturaToolStripMenuItem.Name = "MostrarFacturaToolStripMenuItem";
            MostrarFacturaToolStripMenuItem.Size = new Size(267, 24);
            MostrarFacturaToolStripMenuItem.Text = "Ver pedido";
            // 
            // DuplicarPedidoToolStripMenuItem
            // 
            DuplicarPedidoToolStripMenuItem.Name = "DuplicarPedidoToolStripMenuItem";
            DuplicarPedidoToolStripMenuItem.Size = new Size(267, 24);
            DuplicarPedidoToolStripMenuItem.Text = "Duplicar pedido";
            // 
            // MostrarInformaciónDeAFIPToolStripMenuItem
            // 
            MostrarInformaciónDeAFIPToolStripMenuItem.Name = "MostrarInformaciónDeAFIPToolStripMenuItem";
            MostrarInformaciónDeAFIPToolStripMenuItem.Size = new Size(267, 24);
            MostrarInformaciónDeAFIPToolStripMenuItem.Text = "Mostrar información de AFIP";
            MostrarInformaciónDeAFIPToolStripMenuItem.Visible = false;
            // 
            // AnularToolStripMenuItem
            // 
            AnularToolStripMenuItem.Name = "AnularToolStripMenuItem";
            AnularToolStripMenuItem.Size = new Size(267, 24);
            AnularToolStripMenuItem.Text = "Anular";
            AnularToolStripMenuItem.Click += AnularToolStripMenuItem_Click;
            // 
            // StatusStrip1
            // 
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            StatusStrip1.Items.AddRange(new ToolStripItem[] { tss_version, tss_separador1, tss_dbInfo, tss_separador2, tss_usuario_logueado, tss_separador3, tss_hora });
            StatusStrip1.Location = new Point(0, 1189);
            StatusStrip1.Name = "StatusStrip1";
            StatusStrip1.Padding = new Padding(1, 0, 19, 0);
            StatusStrip1.Size = new Size(1515, 26);
            StatusStrip1.TabIndex = 54;
            StatusStrip1.Text = "StatusStrip1";
            // 
            // tss_version
            // 
            tss_version.Name = "tss_version";
            tss_version.Size = new Size(80, 20);
            tss_version.Text = "%versión%";
            // 
            // tss_separador1
            // 
            tss_separador1.BorderSides = ToolStripStatusLabelBorderSides.Left;
            tss_separador1.BorderStyle = Border3DStyle.Etched;
            tss_separador1.Name = "tss_separador1";
            tss_separador1.Size = new Size(4, 20);
            // 
            // tss_dbInfo
            // 
            tss_dbInfo.Name = "tss_dbInfo";
            tss_dbInfo.Size = new Size(77, 20);
            tss_dbInfo.Text = "%dbInfo%";
            // 
            // tss_separador2
            // 
            tss_separador2.BorderSides = ToolStripStatusLabelBorderSides.Left;
            tss_separador2.BorderStyle = Border3DStyle.Etched;
            tss_separador2.Name = "tss_separador2";
            tss_separador2.Size = new Size(4, 20);
            // 
            // tss_usuario_logueado
            // 
            tss_usuario_logueado.Name = "tss_usuario_logueado";
            tss_usuario_logueado.Size = new Size(151, 20);
            tss_usuario_logueado.Text = "%usuario_logueado%";
            // 
            // tss_separador3
            // 
            tss_separador3.BorderSides = ToolStripStatusLabelBorderSides.Left;
            tss_separador3.BorderStyle = Border3DStyle.Etched;
            tss_separador3.Name = "tss_separador3";
            tss_separador3.Size = new Size(4, 20);
            // 
            // tss_hora
            // 
            tss_hora.Name = "tss_hora";
            tss_hora.Size = new Size(63, 20);
            tss_hora.Text = "%hora%";
            // 
            // Timer1
            // 
            Timer1.Enabled = true;
            Timer1.Interval = 1000;
            // 
            // chk_test
            // 
            chk_test.AutoSize = true;
            chk_test.Location = new Point(355, 102);
            chk_test.Margin = new Padding(4, 5, 4, 5);
            chk_test.Name = "chk_test";
            chk_test.Size = new Size(99, 24);
            chk_test.TabIndex = 55;
            chk_test.Text = "Modo test";
            chk_test.UseVisualStyleBackColor = true;
            chk_test.Visible = false;
            chk_test.CheckedChanged += chk_test_CheckedChanged;
            // 
            // BackgroundWorker1
            // 
            BackgroundWorker1.WorkerReportsProgress = true;
            // 
            // cmd_go
            // 
            cmd_go.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_go.Enabled = false;
            cmd_go.Location = new Point(1467, 99);
            cmd_go.Margin = new Padding(4, 5, 4, 5);
            cmd_go.Name = "cmd_go";
            cmd_go.Size = new Size(39, 31);
            cmd_go.TabIndex = 66;
            cmd_go.Text = "Ir";
            cmd_go.UseVisualStyleBackColor = true;
            // 
            // txt_nPage
            // 
            txt_nPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nPage.Enabled = false;
            txt_nPage.Location = new Point(1375, 99);
            txt_nPage.Margin = new Padding(4, 5, 4, 5);
            txt_nPage.Name = "txt_nPage";
            txt_nPage.Size = new Size(83, 27);
            txt_nPage.TabIndex = 65;
            // 
            // cmd_last
            // 
            cmd_last.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_last.Enabled = false;
            cmd_last.Location = new Point(1328, 99);
            cmd_last.Margin = new Padding(4, 5, 4, 5);
            cmd_last.Name = "cmd_last";
            cmd_last.Size = new Size(39, 31);
            cmd_last.TabIndex = 64;
            cmd_last.Text = ">>|";
            cmd_last.UseVisualStyleBackColor = true;
            cmd_last.Click += cmd_last_Click;
            // 
            // cmd_next
            // 
            cmd_next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_next.Enabled = false;
            cmd_next.Location = new Point(1267, 99);
            cmd_next.Margin = new Padding(4, 5, 4, 5);
            cmd_next.Name = "cmd_next";
            cmd_next.Size = new Size(53, 31);
            cmd_next.TabIndex = 63;
            cmd_next.Text = ">>";
            cmd_next.UseVisualStyleBackColor = true;
            cmd_next.Click += cmd_next_Click;
            // 
            // cmd_prev
            // 
            cmd_prev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_prev.Enabled = false;
            cmd_prev.Location = new Point(1205, 99);
            cmd_prev.Margin = new Padding(4, 5, 4, 5);
            cmd_prev.Name = "cmd_prev";
            cmd_prev.Size = new Size(53, 31);
            cmd_prev.TabIndex = 62;
            cmd_prev.Text = "<<";
            cmd_prev.UseVisualStyleBackColor = true;
            cmd_prev.Click += cmd_prev_Click;
            // 
            // cmd_first
            // 
            cmd_first.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_first.Enabled = false;
            cmd_first.Location = new Point(1159, 99);
            cmd_first.Margin = new Padding(4, 5, 4, 5);
            cmd_first.Name = "cmd_first";
            cmd_first.Size = new Size(39, 31);
            cmd_first.TabIndex = 61;
            cmd_first.Text = "|<<";
            cmd_first.UseVisualStyleBackColor = true;
            cmd_first.Click += cmd_first_Click;
            // 
            // txt_search
            // 
            txt_search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txt_search.Location = new Point(109, 34);
            txt_search.Margin = new Padding(4, 5, 4, 5);
            txt_search.Name = "txt_search";
            txt_search.Size = new Size(1313, 27);
            txt_search.TabIndex = 67;
            // 
            // pic_search
            // 
            pic_search.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pic_search.Image = (Image)resources.GetObject("pic_search.Image");
            pic_search.Location = new Point(1455, 34);
            pic_search.Margin = new Padding(4, 5, 4, 5);
            pic_search.Name = "pic_search";
            pic_search.Size = new Size(22, 22);
            pic_search.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_search.TabIndex = 69;
            pic_search.TabStop = false;
            // 
            // pic
            // 
            pic.Image = (Image)resources.GetObject("pic.Image");
            pic.Location = new Point(385, 341);
            pic.Margin = new Padding(3, 2, 3, 2);
            pic.Name = "pic";
            pic.Size = new Size(996, 525);
            pic.TabIndex = 24;
            pic.TabStop = false;
            // 
            // Button1
            // 
            Button1.Location = new Point(561, 92);
            Button1.Margin = new Padding(4, 5, 4, 5);
            Button1.Name = "Button1";
            Button1.Size = new Size(137, 38);
            Button1.TabIndex = 70;
            Button1.Text = "Button1";
            Button1.UseVisualStyleBackColor = true;
            Button1.Visible = false;
            // 
            // tick_closeProgram
            // 
            tick_closeProgram.Interval = 600000;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1515, 1215);
            Controls.Add(Button1);
            Controls.Add(txt_search);
            Controls.Add(pic_search);
            Controls.Add(lbl_borrarbusqueda);
            Controls.Add(cmd_go);
            Controls.Add(txt_nPage);
            Controls.Add(cmd_last);
            Controls.Add(cmd_next);
            Controls.Add(cmd_prev);
            Controls.Add(cmd_first);
            Controls.Add(chk_test);
            Controls.Add(StatusStrip1);
            Controls.Add(chk_rpt);
            Controls.Add(cmd_addcliente);
            Controls.Add(cmd_pedido);
            Controls.Add(pic);
            Controls.Add(Treev);
            Controls.Add(chk_historicos);
            Controls.Add(lblbusqueda);
            Controls.Add(cmd_add);
            Controls.Add(lbl_clientes);
            Controls.Add(lbl_proveedores);
            Controls.Add(lbl_items);
            Controls.Add(lbl_pedidos);
            Controls.Add(lbl_usuarios);
            Controls.Add(cmd_search);
            Controls.Add(cmd_refresh);
            Controls.Add(dg_view);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Centrex";
            WindowState = FormWindowState.Maximized;
            FormClosed += main_FormClosed;
            Load += main_Load;
            cmsPreciosMasivo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            cmsGeneral.ResumeLayout(false);
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_search).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        // Friend WithEvents Centrex As WindowsApplication1.Centrex
        internal Button cmd_add;
        // Friend WithEvents ClienteTableAdapter1 As WindowsApplication1.Database1DataSetTableAdapters.clienteTableAdapter
        // Friend WithEvents Database1DataSet As WindowsApplication1.Database1DataSet
        internal Label lbl_clientes;
        internal Label lbl_proveedores;
        internal Label lbl_items;
        internal Label lbl_pedidos;
        internal Label lbl_usuarios;
        internal Button cmd_search;
        internal Button cmd_refresh;
        internal Label lblbusqueda;
        internal CheckBox chk_historicos;
        internal ToolTip ToolTip1;
        internal TreeView Treev;
        internal PictureBox pic;
        internal Button cmd_pedido;
        internal Button cmd_addcliente;
        internal CheckBox chk_rpt;
        internal ToolTip tooltip_advanceseach;
        internal DataGridView dg_view;
        internal ContextMenuStrip cmsPreciosMasivo;
        internal ToolStripMenuItem ActualizaciónMasivaDePreciosToolStripMenuItem;
        internal StatusStrip StatusStrip1;
        internal ToolStripStatusLabel tss_version;
        internal ToolStripStatusLabel tss_separador3;
        internal ToolStripStatusLabel tss_hora;
        internal Timer Timer1;
        internal CheckBox chk_test;
        internal ContextMenuStrip cmsGeneral;
        internal ToolStripMenuItem EditarToolStripMenuItem;
        internal ToolStripMenuItem BorrarToolStripMenuItem;
        internal ToolStripMenuItem TerminarPedidoToolStripMenuItem;
        internal ToolStripMenuItem DeshabilitarItemToolStripMenuItem;
        internal ToolStripMenuItem MostrarFacturaToolStripMenuItem;
        internal ToolStripMenuItem DuplicarPedidoToolStripMenuItem;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
        internal ToolStripMenuItem MostrarInformaciónDeAFIPToolStripMenuItem;
        internal Button cmd_go;
        internal TextBox txt_nPage;
        internal Button cmd_last;
        internal Button cmd_next;
        internal Button cmd_prev;
        internal Button cmd_first;
        internal TextBox txt_search;
        internal PictureBox pic_search;
        internal Label lbl_borrarbusqueda;
        internal Button Button1;
        internal ToolStripStatusLabel tss_separador1;
        internal ToolStripStatusLabel tss_dbInfo;
        internal Timer tick_closeProgram;
        internal ToolStripMenuItem AnularToolStripMenuItem;
        internal ToolStripStatusLabel tss_separador2;
        internal ToolStripStatusLabel tss_usuario_logueado;
    }
}


