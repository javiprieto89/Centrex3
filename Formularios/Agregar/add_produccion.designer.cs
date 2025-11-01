using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_produccion : Form
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
            cmd_add_item = new Button();
            cmd_add_item.Click += new EventHandler(cmd_add_item_Click);
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_proveedor = new Label();
            cms_general = new ContextMenuStrip(components);
            EditarToolStripMenuItem = new ToolStripMenuItem();
            BorrarToolStripMenuItem = new ToolStripMenuItem();
            chk_secuencia = new CheckBox();
            txt_nota = new TextBox();
            lbl_nota = new Label();
            cmb_proveedor = new ComboBox();
            dg_viewProduccion = new DataGridView();
            lbl_fechaCarga = new Label();
            lbl_fecha1 = new Label();
            lbl_fechaRecepcion = new Label();
            lbl_fecha3 = new Label();
            lbl_fechaEnvio = new Label();
            lbl_fecha2 = new Label();
            lbl_produccion = new Label();
            lbl_nProduccion = new Label();
            cmd_enviar = new Button();
            cmd_enviar.Click += new EventHandler(cmd_enviar_Click);
            cms_enviado = new ContextMenuStrip(components);
            ModificarArtículoRecibidoToolStripMenuItem = new ToolStripMenuItem();
            ModificarArtículoRecibidoToolStripMenuItem.Click += new EventHandler(ModificarArtículoRecibidoToolStripMenuItem_Click);
            ModificarCantidadRecibidaToolStripMenuItem = new ToolStripMenuItem();
            ModificarCantidadRecibidaToolStripMenuItem.Click += new EventHandler(ModificarCantidadRecibidaToolStripMenuItem_Click);
            chk_imprimir = new CheckBox();
            pic_searchProveedor = new PictureBox();
            cms_general.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dg_viewProduccion).BeginInit();
            cms_enviado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).BeginInit();
            SuspendLayout();
            // 
            // cmd_add_item
            // 
            cmd_add_item.Location = new Point(14, 386);
            cmd_add_item.Name = "cmd_add_item";
            cmd_add_item.Size = new Size(133, 22);
            cmd_add_item.TabIndex = 1;
            cmd_add_item.Text = "Añadir producto";
            cmd_add_item.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(384, 586);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 5;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(160, 586);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 4;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(12, 126);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 41;
            lbl_proveedor.Text = "Proveedor";
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
            chk_secuencia.Location = new Point(20, 547);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 5;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // txt_nota
            // 
            txt_nota.Location = new Point(61, 432);
            txt_nota.MaxLength = 91;
            txt_nota.Multiline = true;
            txt_nota.Name = "txt_nota";
            txt_nota.Size = new Size(546, 57);
            txt_nota.TabIndex = 8;
            // 
            // lbl_nota
            // 
            lbl_nota.AutoSize = true;
            lbl_nota.Location = new Point(17, 452);
            lbl_nota.Name = "lbl_nota";
            lbl_nota.Size = new Size(38, 13);
            lbl_nota.TabIndex = 57;
            lbl_nota.Text = "Notas:";
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(78, 121);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(490, 21);
            cmb_proveedor.TabIndex = 635;
            // 
            // dg_viewProduccion
            // 
            dg_viewProduccion.AllowUserToAddRows = false;
            dg_viewProduccion.AllowUserToDeleteRows = false;
            dg_viewProduccion.AllowUserToOrderColumns = true;
            dg_viewProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dg_viewProduccion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_viewProduccion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg_viewProduccion.ContextMenuStrip = cms_general;
            dg_viewProduccion.Location = new Point(14, 155);
            dg_viewProduccion.MultiSelect = false;
            dg_viewProduccion.Name = "dg_viewProduccion";
            dg_viewProduccion.ReadOnly = true;
            dg_viewProduccion.RowHeadersVisible = false;
            dg_viewProduccion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg_viewProduccion.Size = new Size(592, 216);
            dg_viewProduccion.TabIndex = 640;
            // 
            // lbl_fechaCarga
            // 
            lbl_fechaCarga.AutoSize = true;
            lbl_fechaCarga.Location = new Point(134, 18);
            lbl_fechaCarga.Name = "lbl_fechaCarga";
            lbl_fechaCarga.Size = new Size(83, 13);
            lbl_fechaCarga.TabIndex = 662;
            lbl_fechaCarga.Text = "%fecha_carga%";
            // 
            // lbl_fecha1
            // 
            lbl_fecha1.AutoSize = true;
            lbl_fecha1.Location = new Point(12, 18);
            lbl_fecha1.Name = "lbl_fecha1";
            lbl_fecha1.Size = new Size(85, 13);
            lbl_fecha1.TabIndex = 661;
            lbl_fecha1.Text = "Fecha de carga:";
            // 
            // lbl_fechaRecepcion
            // 
            lbl_fechaRecepcion.AutoSize = true;
            lbl_fechaRecepcion.Location = new Point(134, 79);
            lbl_fechaRecepcion.Name = "lbl_fechaRecepcion";
            lbl_fechaRecepcion.Size = new Size(103, 13);
            lbl_fechaRecepcion.TabIndex = 660;
            lbl_fechaRecepcion.Text = "%fecha_recepcion%";
            lbl_fechaRecepcion.Visible = false;
            // 
            // lbl_fecha3
            // 
            lbl_fecha3.AutoSize = true;
            lbl_fecha3.Location = new Point(11, 79);
            lbl_fecha3.Name = "lbl_fecha3";
            lbl_fecha3.Size = new Size(105, 13);
            lbl_fecha3.TabIndex = 659;
            lbl_fecha3.Text = "Fecha de recepción:";
            // 
            // lbl_fechaEnvio
            // 
            lbl_fechaEnvio.AutoSize = true;
            lbl_fechaEnvio.Location = new Point(134, 48);
            lbl_fechaEnvio.Name = "lbl_fechaEnvio";
            lbl_fechaEnvio.Size = new Size(82, 13);
            lbl_fechaEnvio.TabIndex = 658;
            lbl_fechaEnvio.Text = "%fecha_envio%";
            // 
            // lbl_fecha2
            // 
            lbl_fecha2.AutoSize = true;
            lbl_fecha2.Location = new Point(12, 48);
            lbl_fecha2.Name = "lbl_fecha2";
            lbl_fecha2.Size = new Size(86, 13);
            lbl_fecha2.TabIndex = 657;
            lbl_fecha2.Text = "Fecha de envío:";
            // 
            // lbl_produccion
            // 
            lbl_produccion.AutoSize = true;
            lbl_produccion.Location = new Point(435, 18);
            lbl_produccion.Name = "lbl_produccion";
            lbl_produccion.Size = new Size(114, 13);
            lbl_produccion.TabIndex = 642;
            lbl_produccion.Text = "Pedido de producción:";
            lbl_produccion.Visible = false;
            // 
            // lbl_nProduccion
            // 
            lbl_nProduccion.AutoSize = true;
            lbl_nProduccion.Location = new Point(550, 18);
            lbl_nProduccion.Name = "lbl_nProduccion";
            lbl_nProduccion.Size = new Size(55, 13);
            lbl_nProduccion.TabIndex = 643;
            lbl_nProduccion.Text = "%pedido%";
            lbl_nProduccion.Visible = false;
            // 
            // cmd_enviar
            // 
            cmd_enviar.Location = new Point(257, 586);
            cmd_enviar.Name = "cmd_enviar";
            cmd_enviar.Size = new Size(105, 23);
            cmd_enviar.TabIndex = 663;
            cmd_enviar.Text = "Guardar y enviar";
            cmd_enviar.UseVisualStyleBackColor = true;
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
            // chk_imprimir
            // 
            chk_imprimir.AutoSize = true;
            chk_imprimir.Location = new Point(20, 510);
            chk_imprimir.Name = "chk_imprimir";
            chk_imprimir.Size = new Size(167, 17);
            chk_imprimir.TabIndex = 664;
            chk_imprimir.Text = "Imprimir pedido de producción";
            chk_imprimir.UseVisualStyleBackColor = true;
            // 
            // pic_searchProveedor
            // 
            pic_searchProveedor.Image = My.Resources.Resources.iconoLupa;
            pic_searchProveedor.Location = new Point(584, 121);
            pic_searchProveedor.Name = "pic_searchProveedor";
            pic_searchProveedor.Size = new Size(22, 22);
            pic_searchProveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchProveedor.TabIndex = 48;
            pic_searchProveedor.TabStop = false;
            // 
            // add_produccion
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmd_exit;
            ClientSize = new Size(618, 626);
            ControlBox = false;
            Controls.Add(chk_imprimir);
            Controls.Add(cmd_enviar);
            Controls.Add(lbl_fechaCarga);
            Controls.Add(lbl_fecha1);
            Controls.Add(lbl_fechaRecepcion);
            Controls.Add(lbl_fecha3);
            Controls.Add(lbl_fechaEnvio);
            Controls.Add(lbl_fecha2);
            Controls.Add(lbl_nProduccion);
            Controls.Add(lbl_produccion);
            Controls.Add(dg_viewProduccion);
            Controls.Add(cmb_proveedor);
            Controls.Add(txt_nota);
            Controls.Add(lbl_nota);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_add_item);
            Controls.Add(pic_searchProveedor);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_proveedor);
            Margin = new Padding(2);
            Name = "add_produccion";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Carga de pedido de producción";
            cms_general.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dg_viewProduccion).EndInit();
            cms_enviado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic_searchProveedor).EndInit();
            Load += new EventHandler(add_produccion_Load);
            FormClosed += new FormClosedEventHandler(add_produccion_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Button cmd_add_item;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_proveedor;
        internal CheckBox chk_secuencia;
        internal ContextMenuStrip cms_general;
        internal ToolStripMenuItem EditarToolStripMenuItem;
        internal ToolStripMenuItem BorrarToolStripMenuItem;
        internal TextBox txt_nota;
        internal Label lbl_nota;
        internal ComboBox cmb_proveedor;
        internal PictureBox pic_searchProveedor;
        internal DataGridView dg_viewProduccion;
        internal Label lbl_fechaCarga;
        internal Label lbl_fecha1;
        internal Label lbl_fechaRecepcion;
        internal Label lbl_fecha3;
        internal Label lbl_fechaEnvio;
        internal Label lbl_fecha2;
        internal Label lbl_produccion;
        internal Label lbl_nProduccion;
        internal Button cmd_enviar;
        internal ContextMenuStrip cms_enviado;
        internal ToolStripMenuItem ModificarArtículoRecibidoToolStripMenuItem;
        internal ToolStripMenuItem ModificarCantidadRecibidaToolStripMenuItem;
        internal CheckBox chk_imprimir;
    }
}
