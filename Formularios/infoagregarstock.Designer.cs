using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class infoagregarstock : Form
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
            lblitem = new Label();
            lbl_item = new Label();
            lbl_preciolista = new Label();
            lbl_costo = new Label();
            lbl_cantidad = new Label();
            lbl_factura = new Label();
            lbl_factor = new Label();
            lbl_fecha = new Label();
            txt_fecha = new TextBox();
            txt_factor = new TextBox();
            txt_factor.LostFocus += new EventHandler(txt_factor_LostFocus);
            txt_preciolista = new TextBox();
            txt_costo = new TextBox();
            txt_costo.LostFocus += new EventHandler(txt_costo_LostFocus);
            txt_cantidad = new TextBox();
            txt_factura = new TextBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_proveedor = new Label();
            psearch_proveedor = new PictureBox();
            cmb_proveedor = new ComboBox();
            lbl_notas = new Label();
            txt_notas = new TextBox();
            psearch_item = new PictureBox();
            psearch_item.Click += new EventHandler(psearch_item_Click);
            ToolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_item).BeginInit();
            SuspendLayout();
            // 
            // lblitem
            // 
            lblitem.AutoSize = true;
            lblitem.Location = new Point(26, 24);
            lblitem.Margin = new Padding(2, 0, 2, 0);
            lblitem.Name = "lblitem";
            lblitem.Size = new Size(27, 13);
            lblitem.TabIndex = 0;
            lblitem.Text = "Item";
            // 
            // lbl_item
            // 
            lbl_item.AutoSize = true;
            lbl_item.Location = new Point(112, 24);
            lbl_item.Margin = new Padding(2, 0, 2, 0);
            lbl_item.Name = "lbl_item";
            lbl_item.Size = new Size(42, 13);
            lbl_item.TabIndex = 1;
            lbl_item.Text = "%item%";
            // 
            // lbl_preciolista
            // 
            lbl_preciolista.AutoSize = true;
            lbl_preciolista.Location = new Point(24, 270);
            lbl_preciolista.Margin = new Padding(2, 0, 2, 0);
            lbl_preciolista.Name = "lbl_preciolista";
            lbl_preciolista.Size = new Size(58, 13);
            lbl_preciolista.TabIndex = 2;
            lbl_preciolista.Text = "Precio lista";
            // 
            // lbl_costo
            // 
            lbl_costo.AutoSize = true;
            lbl_costo.Location = new Point(26, 202);
            lbl_costo.Margin = new Padding(2, 0, 2, 0);
            lbl_costo.Name = "lbl_costo";
            lbl_costo.Size = new Size(34, 13);
            lbl_costo.TabIndex = 3;
            lbl_costo.Text = "Costo";
            // 
            // lbl_cantidad
            // 
            lbl_cantidad.AutoSize = true;
            lbl_cantidad.Location = new Point(26, 167);
            lbl_cantidad.Margin = new Padding(2, 0, 2, 0);
            lbl_cantidad.Name = "lbl_cantidad";
            lbl_cantidad.Size = new Size(49, 13);
            lbl_cantidad.TabIndex = 4;
            lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_factura
            // 
            lbl_factura.AutoSize = true;
            lbl_factura.Location = new Point(26, 96);
            lbl_factura.Margin = new Padding(2, 0, 2, 0);
            lbl_factura.Name = "lbl_factura";
            lbl_factura.Size = new Size(43, 13);
            lbl_factura.TabIndex = 5;
            lbl_factura.Text = "Factura";
            // 
            // lbl_factor
            // 
            lbl_factor.AutoSize = true;
            lbl_factor.Location = new Point(26, 236);
            lbl_factor.Margin = new Padding(2, 0, 2, 0);
            lbl_factor.Name = "lbl_factor";
            lbl_factor.Size = new Size(37, 13);
            lbl_factor.TabIndex = 6;
            lbl_factor.Text = "Factor";
            // 
            // lbl_fecha
            // 
            lbl_fecha.AutoSize = true;
            lbl_fecha.Location = new Point(26, 61);
            lbl_fecha.Margin = new Padding(2, 0, 2, 0);
            lbl_fecha.Name = "lbl_fecha";
            lbl_fecha.Size = new Size(37, 13);
            lbl_fecha.TabIndex = 7;
            lbl_fecha.Text = "Fecha";
            // 
            // txt_fecha
            // 
            txt_fecha.Location = new Point(114, 58);
            txt_fecha.Margin = new Padding(2, 2, 2, 2);
            txt_fecha.Name = "txt_fecha";
            txt_fecha.Size = new Size(154, 20);
            txt_fecha.TabIndex = 0;
            // 
            // txt_factor
            // 
            txt_factor.Location = new Point(114, 232);
            txt_factor.Margin = new Padding(2, 2, 2, 2);
            txt_factor.Name = "txt_factor";
            txt_factor.Size = new Size(154, 20);
            txt_factor.TabIndex = 5;
            // 
            // txt_preciolista
            // 
            txt_preciolista.Location = new Point(114, 267);
            txt_preciolista.Margin = new Padding(2, 2, 2, 2);
            txt_preciolista.Name = "txt_preciolista";
            txt_preciolista.Size = new Size(154, 20);
            txt_preciolista.TabIndex = 6;
            // 
            // txt_costo
            // 
            txt_costo.Location = new Point(114, 197);
            txt_costo.Margin = new Padding(2, 2, 2, 2);
            txt_costo.Name = "txt_costo";
            txt_costo.Size = new Size(154, 20);
            txt_costo.TabIndex = 4;
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(114, 162);
            txt_cantidad.Margin = new Padding(2, 2, 2, 2);
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(154, 20);
            txt_cantidad.TabIndex = 3;
            // 
            // txt_factura
            // 
            txt_factura.Location = new Point(114, 93);
            txt_factura.Margin = new Padding(2, 2, 2, 2);
            txt_factura.Name = "txt_factura";
            txt_factura.Size = new Size(154, 20);
            txt_factura.TabIndex = 1;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(169, 426);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 9;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(71, 426);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 8;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(26, 131);
            lbl_proveedor.Margin = new Padding(2, 0, 2, 0);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 16;
            lbl_proveedor.Text = "Proveedor";
            // 
            // psearch_proveedor
            // 
            psearch_proveedor.Image = My.Resources.Resources.iconoLupa;
            psearch_proveedor.Location = new Point(273, 128);
            psearch_proveedor.Name = "psearch_proveedor";
            psearch_proveedor.Size = new Size(22, 22);
            psearch_proveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_proveedor.TabIndex = 72;
            psearch_proveedor.TabStop = false;
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(114, 128);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(154, 21);
            cmb_proveedor.TabIndex = 2;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(26, 306);
            lbl_notas.Margin = new Padding(2, 0, 2, 0);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 73;
            lbl_notas.Text = "Notas";
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(114, 302);
            txt_notas.Margin = new Padding(2, 2, 2, 2);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(154, 88);
            txt_notas.TabIndex = 7;
            txt_notas.WordWrap = false;
            // 
            // psearch_item
            // 
            psearch_item.Image = My.Resources.Resources.iconoLupa;
            psearch_item.Location = new Point(273, 24);
            psearch_item.Name = "psearch_item";
            psearch_item.Size = new Size(22, 22);
            psearch_item.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_item.TabIndex = 74;
            psearch_item.TabStop = false;
            ToolTip1.SetToolTip(psearch_item, "Modificar item");
            psearch_item.Visible = false;
            // 
            // infoagregarstock
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 473);
            Controls.Add(psearch_item);
            Controls.Add(txt_notas);
            Controls.Add(lbl_notas);
            Controls.Add(psearch_proveedor);
            Controls.Add(cmb_proveedor);
            Controls.Add(lbl_proveedor);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_factura);
            Controls.Add(txt_cantidad);
            Controls.Add(txt_costo);
            Controls.Add(txt_preciolista);
            Controls.Add(txt_factor);
            Controls.Add(txt_fecha);
            Controls.Add(lbl_fecha);
            Controls.Add(lbl_factor);
            Controls.Add(lbl_factura);
            Controls.Add(lbl_cantidad);
            Controls.Add(lbl_costo);
            Controls.Add(lbl_preciolista);
            Controls.Add(lbl_item);
            Controls.Add(lblitem);
            Margin = new Padding(2, 2, 2, 2);
            Name = "infoagregarstock";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Actualizar stock";
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_item).EndInit();
            Load += new EventHandler(infoagregarstock_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lblitem;
        internal Label lbl_item;
        internal Label lbl_preciolista;
        internal Label lbl_costo;
        internal Label lbl_cantidad;
        internal Label lbl_factura;
        internal Label lbl_factor;
        internal Label lbl_fecha;
        internal TextBox txt_fecha;
        internal TextBox txt_factor;
        internal TextBox txt_preciolista;
        internal TextBox txt_costo;
        internal TextBox txt_cantidad;
        internal TextBox txt_factura;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_proveedor;
        internal PictureBox psearch_proveedor;
        internal ComboBox cmb_proveedor;
        internal Label lbl_notas;
        internal TextBox txt_notas;
        internal PictureBox psearch_item;
        internal ToolTip ToolTip1;
    }
}


