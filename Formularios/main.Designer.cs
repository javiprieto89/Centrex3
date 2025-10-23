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
        private System.ComponentModel.IContainer components;

        // NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        // Se puede modificar usando el Diseñador de Windows Forms.  
        // No lo modifique con el editor de código.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var TreeNode1 = new TreeNode("Condiciones de venta");
            var TreeNode2 = new TreeNode("Condiciones de compra");
            var TreeNode3 = new TreeNode("Conceptos de compra");
            var TreeNode4 = new TreeNode("Compras", new TreeNode[] { TreeNode2, TreeNode3 });
            var TreeNode5 = new TreeNode("Clientes");
            var TreeNode6 = new TreeNode("CC. Clientes");
            var TreeNode7 = new TreeNode("Clientes", new TreeNode[] { TreeNode5, TreeNode6 });
            var TreeNode8 = new TreeNode("Proveedores");
            var TreeNode9 = new TreeNode("CC. Proveedores");
            var TreeNode10 = new TreeNode("Proveedores", new TreeNode[] { TreeNode8, TreeNode9 });
            var TreeNode11 = new TreeNode("Marcas");
            var TreeNode12 = new TreeNode("Categorías");
            var TreeNode13 = new TreeNode("Productos");
            var TreeNode14 = new TreeNode("Productos asociados");
            var TreeNode15 = new TreeNode("Productos", new TreeNode[] { TreeNode11, TreeNode12, TreeNode13, TreeNode14 });
            var TreeNode16 = new TreeNode("Comprobantes");
            var TreeNode17 = new TreeNode("Consultas personalizadas");
            var TreeNode18 = new TreeNode("Caja");
            var TreeNode19 = new TreeNode("Bancos");
            var TreeNode20 = new TreeNode("Cuentas bancarias");
            var TreeNode21 = new TreeNode("Bancos", new TreeNode[] { TreeNode19, TreeNode20 });
            var TreeNode22 = new TreeNode("Cheques recibidos");
            var TreeNode23 = new TreeNode("Cheques emitidos");
            var TreeNode24 = new TreeNode("Cartera de cheques");
            var TreeNode25 = new TreeNode("Depositar ch. recibidos");
            var TreeNode26 = new TreeNode("Rechazar ch. recibidos");
            var TreeNode27 = new TreeNode("Cheques", new TreeNode[] { TreeNode22, TreeNode23, TreeNode24, TreeNode25, TreeNode26 });
            var TreeNode28 = new TreeNode("Impuestos");
            var TreeNode29 = new TreeNode("Items - Impuestos");
            var TreeNode30 = new TreeNode("Impuestos", new TreeNode[] { TreeNode28, TreeNode29 });
            var TreeNode31 = new TreeNode("Archivos", new TreeNode[] { TreeNode1, TreeNode4, TreeNode7, TreeNode10, TreeNode15, TreeNode16, TreeNode17, TreeNode18, TreeNode21, TreeNode27, TreeNode30 });
            var TreeNode32 = new TreeNode("Ordenes de compra");
            var TreeNode33 = new TreeNode("Comprobantes de compras");
            var TreeNode34 = new TreeNode("Pagos");
            var TreeNode35 = new TreeNode("Compras", new TreeNode[] { TreeNode32, TreeNode33, TreeNode34 });
            var TreeNode36 = new TreeNode("Ajustes de stock");
            var TreeNode37 = new TreeNode("Ingreso de mercadería");
            var TreeNode38 = new TreeNode("Stock", new TreeNode[] { TreeNode36, TreeNode37 });
            var TreeNode39 = new TreeNode("Producción");
            var TreeNode40 = new TreeNode("Nuevo pedido");
            var TreeNode41 = new TreeNode("Pedidos");
            var TreeNode42 = new TreeNode("Cobros");
            var TreeNode43 = new TreeNode("Ventas", new TreeNode[] { TreeNode40, TreeNode41, TreeNode42 });
            var TreeNode44 = new TreeNode("Exportaciones S.I.A.p");
            var TreeNode45 = new TreeNode("Stock");
            var TreeNode46 = new TreeNode("Movimientos de stock");
            var TreeNode47 = new TreeNode("Productos", new TreeNode[] { TreeNode45, TreeNode46 });
            var TreeNode48 = new TreeNode("CC. Proveedores");
            var TreeNode49 = new TreeNode("Proveedores", new TreeNode[] { TreeNode48 });
            var TreeNode50 = new TreeNode("CC. Clientes");
            var TreeNode51 = new TreeNode("Clientes", new TreeNode[] { TreeNode50 });
            var TreeNode52 = new TreeNode("Último comprobante");
            var TreeNode53 = new TreeNode("Información factura");
            var TreeNode54 = new TreeNode("Pruebas AFIP");
            var TreeNode55 = new TreeNode("MercadoPago QR");
            var TreeNode56 = new TreeNode("Factura electrónica", new TreeNode[] { TreeNode52, TreeNode53, TreeNode54, TreeNode55 });
            var TreeNode57 = new TreeNode("Personalizadas");
            var TreeNode58 = new TreeNode("Exportaciones S.I.A.p");
            var TreeNode59 = new TreeNode("Consultas", new TreeNode[] { TreeNode47, TreeNode49, TreeNode51, TreeNode56, TreeNode57, TreeNode58 });
            var TreeNode60 = new TreeNode("Configuración");
            var TreeNode61 = new TreeNode("Permisos");
            var TreeNode62 = new TreeNode("Perfiles");
            var TreeNode63 = new TreeNode("Usuarios");
            var TreeNode64 = new TreeNode("Asignar permisos a perfiles");
            var TreeNode65 = new TreeNode("Asignar perfiles a usuarios");
            var TreeNode66 = new TreeNode("Seguridad", new TreeNode[] { TreeNode60, TreeNode61, TreeNode62, TreeNode63, TreeNode64, TreeNode65 });
            var TreeNode67 = new TreeNode("Configuración", new TreeNode[] { TreeNode66 });
            var TreeNode68 = new TreeNode("Acerca de...");
            var TreeNode69 = new TreeNode("Centrex", new TreeNode[] { TreeNode31, TreeNode35, TreeNode38, TreeNode39, TreeNode43, TreeNode44, TreeNode59, TreeNode67, TreeNode68 });
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
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
            Treev.NodeMouseClick += new TreeNodeMouseClickEventHandler(Treev_NodeMouseClick);
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
            AnularToolStripMenuItem.Click += new EventHandler(AnularToolStripMenuItem_Click);
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
            cmd_last.Click += new EventHandler(cmd_last_Click);
            cmd_next = new Button();
            cmd_next.Click += new EventHandler(cmd_next_Click);
            cmd_prev = new Button();
            cmd_prev.Click += new EventHandler(cmd_prev_Click);
            cmd_first = new Button();
            cmd_first.Click += new EventHandler(cmd_first_Click);
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
            cmd_add.Location = new Point(27, 868);
            cmd_add.Margin = new Padding(4, 4, 4, 4);
            cmd_add.Name = "cmd_add";
            cmd_add.Size = new Size(284, 52);
            cmd_add.TabIndex = 3;
            cmd_add.Text = "Agregar";
            cmd_add.UseVisualStyleBackColor = true;
            // 
            // lbl_clientes
            // 
            lbl_clientes.Location = new Point(0, 0);
            lbl_clientes.Margin = new Padding(4, 0, 4, 0);
            lbl_clientes.Name = "lbl_clientes";
            lbl_clientes.Size = new Size(133, 28);
            lbl_clientes.TabIndex = 71;
            // 
            // lbl_proveedores
            // 
            lbl_proveedores.Location = new Point(0, 0);
            lbl_proveedores.Margin = new Padding(4, 0, 4, 0);
            lbl_proveedores.Name = "lbl_proveedores";
            lbl_proveedores.Size = new Size(133, 28);
            lbl_proveedores.TabIndex = 72;
            // 
            // lbl_items
            // 
            lbl_items.Location = new Point(0, 0);
            lbl_items.Margin = new Padding(4, 0, 4, 0);
            lbl_items.Name = "lbl_items";
            lbl_items.Size = new Size(133, 28);
            lbl_items.TabIndex = 73;
            // 
            // lbl_pedidos
            // 
            lbl_pedidos.Location = new Point(0, 0);
            lbl_pedidos.Margin = new Padding(4, 0, 4, 0);
            lbl_pedidos.Name = "lbl_pedidos";
            lbl_pedidos.Size = new Size(133, 28);
            lbl_pedidos.TabIndex = 74;
            // 
            // lbl_usuarios
            // 
            lbl_usuarios.Location = new Point(0, 0);
            lbl_usuarios.Margin = new Padding(4, 0, 4, 0);
            lbl_usuarios.Name = "lbl_usuarios";
            lbl_usuarios.Size = new Size(133, 28);
            lbl_usuarios.TabIndex = 75;
            // 
            // cmd_search
            // 
            cmd_search.Location = new Point(0, 0);
            cmd_search.Margin = new Padding(4, 4, 4, 4);
            cmd_search.Name = "cmd_search";
            cmd_search.Size = new Size(100, 28);
            cmd_search.TabIndex = 76;
            // 
            // cmd_refresh
            // 
            cmd_refresh.Location = new Point(0, 0);
            cmd_refresh.Margin = new Padding(4, 4, 4, 4);
            cmd_refresh.Name = "cmd_refresh";
            cmd_refresh.Size = new Size(100, 28);
            cmd_refresh.TabIndex = 77;
            // 
            // lblbusqueda
            // 
            lblbusqueda.AutoSize = true;
            lblbusqueda.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblbusqueda.Location = new Point(17, 36);
            lblbusqueda.Margin = new Padding(4, 0, 4, 0);
            lblbusqueda.Name = "lblbusqueda";
            lblbusqueda.Size = new Size(80, 17);
            lblbusqueda.TabIndex = 18;
            lblbusqueda.Text = "Búsqueda";
            // 
            // chk_historicos
            // 
            chk_historicos.AutoSize = true;
            chk_historicos.Location = new Point(15, 82);
            chk_historicos.Margin = new Padding(3, 2, 3, 2);
            chk_historicos.Name = "chk_historicos";
            chk_historicos.Size = new Size(175, 21);
            chk_historicos.TabIndex = 20;
            chk_historicos.Text = "Ver históricos/inactivos";
            chk_historicos.UseVisualStyleBackColor = true;
            // 
            // lbl_borrarbusqueda
            // 
            lbl_borrarbusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_borrarbusqueda.AutoSize = true;
            lbl_borrarbusqueda.Location = new Point(1431, 36);
            lbl_borrarbusqueda.Name = "lbl_borrarbusqueda";
            lbl_borrarbusqueda.Size = new Size(14, 17);
            lbl_borrarbusqueda.TabIndex = 68;
            lbl_borrarbusqueda.Text = "x";
            ToolTip1.SetToolTip(lbl_borrarbusqueda, "Borrar búsqueda");
            // 
            // Treev
            // 
            Treev.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Treev.ContextMenuStrip = cmsPreciosMasivo;
            Treev.Location = new Point(15, 108);
            Treev.Margin = new Padding(3, 2, 3, 2);
            Treev.Name = "Treev";
            TreeNode1.Name = "condiciones_venta";
            TreeNode1.Text = "Condiciones de venta";
            TreeNode2.Name = "condiciones_compra";
            TreeNode2.Text = "Condiciones de compra";
            TreeNode3.Name = "conceptos_compra";
            TreeNode3.Text = "Conceptos de compra";
            TreeNode4.Name = "archivo_compras";
            TreeNode4.Text = "Compras";
            TreeNode5.Name = "clientes";
            TreeNode5.NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            TreeNode5.Text = "Clientes";
            TreeNode6.Name = "archivoCCClientes";
            TreeNode6.Text = "CC. Clientes";
            TreeNode7.Name = "archivoClientes";
            TreeNode7.Text = "Clientes";
            TreeNode8.Name = "proveedores";
            TreeNode8.Text = "Proveedores";
            TreeNode9.Name = "archivoCCProveedores";
            TreeNode9.Text = "CC. Proveedores";
            TreeNode10.Name = "archivoProveedores";
            TreeNode10.Text = "Proveedores";
            TreeNode11.Name = "marcas_items";
            TreeNode11.Text = "Marcas";
            TreeNode12.Name = "tipos_items";
            TreeNode12.Text = "Categorías";
            TreeNode13.Name = "items";
            TreeNode13.NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            TreeNode13.Text = "Productos";
            TreeNode14.Name = "asocItems";
            TreeNode14.Text = "Productos asociados";
            TreeNode15.Name = "archivoitems";
            TreeNode15.Text = "Productos";
            TreeNode16.Name = "comprobantes";
            TreeNode16.Text = "Comprobantes";
            TreeNode17.Name = "archivoconsultas";
            TreeNode17.Text = "Consultas personalizadas";
            TreeNode18.Name = "caja";
            TreeNode18.Text = "Caja";
            TreeNode19.Name = "bancos";
            TreeNode19.Text = "Bancos";
            TreeNode20.Name = "cuentas_bancarias";
            TreeNode20.Text = "Cuentas bancarias";
            TreeNode21.Name = "archivoBancos";
            TreeNode21.Text = "Bancos";
            TreeNode22.Name = "chRecibidos";
            TreeNode22.Text = "Cheques recibidos";
            TreeNode23.Name = "chEmitidos";
            TreeNode23.Text = "Cheques emitidos";
            TreeNode24.Name = "chCartera";
            TreeNode24.Text = "Cartera de cheques";
            TreeNode25.Name = "depositarCH";
            TreeNode25.Text = "Depositar ch. recibidos";
            TreeNode26.Name = "rechazarCH";
            TreeNode26.Text = "Rechazar ch. recibidos";
            TreeNode27.Name = "cheques";
            TreeNode27.Text = "Cheques";
            TreeNode28.Name = "impuestos";
            TreeNode28.Text = "Impuestos";
            TreeNode29.Name = "itemsImpuestos";
            TreeNode29.Text = "Items - Impuestos";
            TreeNode30.Name = "archivoImpuestos";
            TreeNode30.Text = "Impuestos";
            TreeNode31.Name = "archivos";
            TreeNode31.Text = "Archivos";
            TreeNode32.Name = "ordenesCompras";
            TreeNode32.Text = "Ordenes de compra";
            TreeNode33.Name = "comprobantes_compras";
            TreeNode33.Text = "Comprobantes de compras";
            TreeNode34.Name = "pagos";
            TreeNode34.Text = "Pagos";
            TreeNode35.Name = "archivocompras";
            TreeNode35.Text = "Compras";
            TreeNode36.Name = "ajustes_stock";
            TreeNode36.Text = "Ajustes de stock";
            TreeNode37.Name = "registros_stock";
            TreeNode37.Text = "Ingreso de mercadería";
            TreeNode38.Name = "stock_menu";
            TreeNode38.Text = "Stock";
            TreeNode39.Name = "produccion";
            TreeNode39.Text = "Producción";
            TreeNode40.Name = "nuevopedido";
            TreeNode40.Text = "Nuevo pedido";
            TreeNode41.Name = "pedidos";
            TreeNode41.NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            TreeNode41.Text = "Pedidos";
            TreeNode42.Name = "cobros";
            TreeNode42.Text = "Cobros";
            TreeNode43.Name = "ventas";
            TreeNode43.Text = "Ventas";
            TreeNode44.Name = "exportSiap";
            TreeNode44.Text = "Exportaciones S.I.A.p";
            TreeNode45.Name = "stock";
            TreeNode45.Text = "Stock";
            TreeNode46.Name = "movStock";
            TreeNode46.Text = "Movimientos de stock";
            TreeNode47.Name = "consultasProductos";
            TreeNode47.Text = "Productos";
            TreeNode48.Name = "ccProveedores";
            TreeNode48.Text = "CC. Proveedores";
            TreeNode49.Name = "consultasProveedores";
            TreeNode49.Text = "Proveedores";
            TreeNode50.Name = "ccClientes";
            TreeNode50.Text = "CC. Clientes";
            TreeNode51.Name = "consultasClientes";
            TreeNode51.Text = "Clientes";
            TreeNode52.Name = "ultimoComprobante";
            TreeNode52.Text = "Último comprobante";
            TreeNode53.Name = "info_fc";
            TreeNode53.Text = "Información factura";
            TreeNode54.Name = "pruebasAFIP";
            TreeNode54.Text = "Pruebas AFIP";
            TreeNode55.Name = "mercadopagoQR";
            TreeNode55.Text = "MercadoPago QR";
            TreeNode56.Name = "consultasFE";
            TreeNode56.Text = "Factura electrónica";
            TreeNode57.Name = "cpersonalizadas";
            TreeNode57.Text = "Personalizadas";
            TreeNode58.Name = "exportSiap";
            TreeNode58.Text = "Exportaciones S.I.A.p";
            TreeNode59.Name = "consultas";
            TreeNode59.Text = "Consultas";
            TreeNode60.Name = "configuracion";
            TreeNode60.Text = "Configuración";
            TreeNode61.Name = "permisos";
            TreeNode61.Text = "Permisos";
            TreeNode62.Name = "perfiles";
            TreeNode62.Text = "Perfiles";
            TreeNode63.Name = "usuarios";
            TreeNode63.Text = "Usuarios";
            TreeNode64.Name = "permisos_a_perfiles";
            TreeNode64.Text = "Asignar permisos a perfiles";
            TreeNode65.Name = "perfiles_a_usuarios";
            TreeNode65.Text = "Asignar perfiles a usuarios";
            TreeNode66.Name = "seguridad";
            TreeNode66.Text = "Seguridad";
            TreeNode67.Name = "cfg_menu";
            TreeNode67.Text = "Configuración";
            TreeNode68.Name = "acercade";
            TreeNode68.Text = "Acerca de...";
            TreeNode69.Name = "root";
            TreeNode69.Text = "Centrex";
            Treev.Nodes.AddRange(new TreeNode[] { TreeNode69 });
            Treev.Size = new Size(295, 738);
            Treev.TabIndex = 23;
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
            cmd_pedido.Location = new Point(317, 868);
            cmd_pedido.Margin = new Padding(4, 4, 4, 4);
            cmd_pedido.Name = "cmd_pedido";
            cmd_pedido.Size = new Size(284, 52);
            cmd_pedido.TabIndex = 25;
            cmd_pedido.Text = "Nuevo pedido";
            cmd_pedido.UseVisualStyleBackColor = true;
            // 
            // cmd_addcliente
            // 
            cmd_addcliente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmd_addcliente.Location = new Point(608, 868);
            cmd_addcliente.Margin = new Padding(4, 4, 4, 4);
            cmd_addcliente.Name = "cmd_addcliente";
            cmd_addcliente.Size = new Size(284, 52);
            cmd_addcliente.TabIndex = 28;
            cmd_addcliente.Text = "Nuevo cliente";
            cmd_addcliente.UseVisualStyleBackColor = true;
            // 
            // chk_rpt
            // 
            chk_rpt.AutoSize = true;
            chk_rpt.Checked = true;
            chk_rpt.CheckState = CheckState.Checked;
            chk_rpt.Location = new Point(203, 82);
            chk_rpt.Margin = new Padding(4, 4, 4, 4);
            chk_rpt.Name = "chk_rpt";
            chk_rpt.Size = new Size(143, 21);
            chk_rpt.TabIndex = 50;
            chk_rpt.Text = "Mostrar impresión";
            chk_rpt.UseVisualStyleBackColor = true;
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
            dg_view.Location = new Point(317, 111);
            dg_view.Margin = new Padding(4, 4, 4, 4);
            dg_view.MultiSelect = false;
            dg_view.Name = "dg_view";
            dg_view.ReadOnly = true;
            dg_view.RowHeadersVisible = false;
            dg_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_view.Size = new Size(1188, 736);
            dg_view.TabIndex = 53;
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
            // 
            // StatusStrip1
            // 
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            StatusStrip1.Items.AddRange(new ToolStripItem[] { tss_version, tss_separador1, tss_dbInfo, tss_separador2, tss_usuario_logueado, tss_separador3, tss_hora });
            StatusStrip1.Location = new Point(0, 947);
            StatusStrip1.Name = "StatusStrip1";
            StatusStrip1.Padding = new Padding(1, 0, 19, 0);
            StatusStrip1.Size = new Size(1515, 25);
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
            chk_test.Location = new Point(355, 82);
            chk_test.Margin = new Padding(4, 4, 4, 4);
            chk_test.Name = "chk_test";
            chk_test.Size = new Size(92, 21);
            chk_test.TabIndex = 55;
            chk_test.Text = "Modo test";
            chk_test.UseVisualStyleBackColor = true;
            chk_test.Visible = false;
            // 
            // BackgroundWorker1
            // 
            BackgroundWorker1.WorkerReportsProgress = true;
            // 
            // cmd_go
            // 
            cmd_go.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_go.Enabled = false;
            cmd_go.Location = new Point(1467, 79);
            cmd_go.Margin = new Padding(4, 4, 4, 4);
            cmd_go.Name = "cmd_go";
            cmd_go.Size = new Size(39, 25);
            cmd_go.TabIndex = 66;
            cmd_go.Text = "Ir";
            cmd_go.UseVisualStyleBackColor = true;
            // 
            // txt_nPage
            // 
            txt_nPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_nPage.Enabled = false;
            txt_nPage.Location = new Point(1375, 79);
            txt_nPage.Margin = new Padding(4, 4, 4, 4);
            txt_nPage.Name = "txt_nPage";
            txt_nPage.Size = new Size(83, 22);
            txt_nPage.TabIndex = 65;
            // 
            // cmd_last
            // 
            cmd_last.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_last.Enabled = false;
            cmd_last.Location = new Point(1328, 79);
            cmd_last.Margin = new Padding(4, 4, 4, 4);
            cmd_last.Name = "cmd_last";
            cmd_last.Size = new Size(39, 25);
            cmd_last.TabIndex = 64;
            cmd_last.Text = ">>|";
            cmd_last.UseVisualStyleBackColor = true;
            // 
            // cmd_next
            // 
            cmd_next.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_next.Enabled = false;
            cmd_next.Location = new Point(1267, 79);
            cmd_next.Margin = new Padding(4, 4, 4, 4);
            cmd_next.Name = "cmd_next";
            cmd_next.Size = new Size(53, 25);
            cmd_next.TabIndex = 63;
            cmd_next.Text = ">>";
            cmd_next.UseVisualStyleBackColor = true;
            // 
            // cmd_prev
            // 
            cmd_prev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_prev.Enabled = false;
            cmd_prev.Location = new Point(1205, 79);
            cmd_prev.Margin = new Padding(4, 4, 4, 4);
            cmd_prev.Name = "cmd_prev";
            cmd_prev.Size = new Size(53, 25);
            cmd_prev.TabIndex = 62;
            cmd_prev.Text = "<<";
            cmd_prev.UseVisualStyleBackColor = true;
            // 
            // cmd_first
            // 
            cmd_first.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmd_first.Enabled = false;
            cmd_first.Location = new Point(1159, 79);
            cmd_first.Margin = new Padding(4, 4, 4, 4);
            cmd_first.Name = "cmd_first";
            cmd_first.Size = new Size(39, 25);
            cmd_first.TabIndex = 61;
            cmd_first.Text = "|<<";
            cmd_first.UseVisualStyleBackColor = true;
            // 
            // txt_search
            // 
            txt_search.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txt_search.Location = new Point(109, 27);
            txt_search.Margin = new Padding(4, 4, 4, 4);
            txt_search.Name = "txt_search";
            txt_search.Size = new Size(1313, 22);
            txt_search.TabIndex = 67;
            // 
            // pic_search
            // 
            pic_search.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pic_search.Image = My.Resources.Resources.iconoLupa;
            pic_search.Location = new Point(1455, 27);
            pic_search.Margin = new Padding(4, 4, 4, 4);
            pic_search.Name = "pic_search";
            pic_search.Size = new Size(22, 22);
            pic_search.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_search.TabIndex = 69;
            pic_search.TabStop = false;
            // 
            // pic
            // 
            pic.Image = My.Resources.Resources.centrexlogo;
            pic.Location = new Point(385, 273);
            pic.Margin = new Padding(3, 2, 3, 2);
            pic.Name = "pic";
            pic.Size = new Size(996, 420);
            pic.TabIndex = 24;
            pic.TabStop = false;
            // 
            // Button1
            // 
            Button1.Location = new Point(561, 74);
            Button1.Margin = new Padding(4, 4, 4, 4);
            Button1.Name = "Button1";
            Button1.Size = new Size(137, 30);
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
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1515, 972);
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
            Margin = new Padding(4, 4, 4, 4);
            Name = "main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Centrex";
            WindowState = FormWindowState.Maximized;
            cmsPreciosMasivo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_view).EndInit();
            cmsGeneral.ResumeLayout(false);
            StatusStrip1.ResumeLayout(false);
            StatusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_search).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            Load += new EventHandler(main_Load);
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


