using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class infoagregaitem : Form
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
            lbl_cantidad = new Label();
            lbl_precio = new Label();
            txt_cantidad = new TextBox();
            txt_cantidad.KeyPress += new KeyPressEventHandler(txt_cantidad_KeyPress);
            txt_precio = new TextBox();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            ToolTip1 = new ToolTip(components);
            GroupBox1 = new GroupBox();
            lbl_desc = new Label();
            lbldesc = new Label();
            lbl_stock = new Label();
            lbl_item = new Label();
            lblstock = new Label();
            lblitem = new Label();
            GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_cantidad
            // 
            lbl_cantidad.AutoSize = true;
            lbl_cantidad.Location = new Point(168, 162);
            lbl_cantidad.Margin = new Padding(2, 0, 2, 0);
            lbl_cantidad.Name = "lbl_cantidad";
            lbl_cantidad.Size = new Size(49, 13);
            lbl_cantidad.TabIndex = 1;
            lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_precio
            // 
            lbl_precio.AutoSize = true;
            lbl_precio.Location = new Point(289, 162);
            lbl_precio.Margin = new Padding(2, 0, 2, 0);
            lbl_precio.Name = "lbl_precio";
            lbl_precio.Size = new Size(37, 13);
            lbl_precio.TabIndex = 2;
            lbl_precio.Text = "Precio";
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(170, 194);
            txt_cantidad.Margin = new Padding(2);
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(88, 20);
            txt_cantidad.TabIndex = 0;
            ToolTip1.SetToolTip(txt_cantidad, "Si la cantidad ingresada es igual a -1, se cancela la operación");
            // 
            // txt_precio
            // 
            txt_precio.Location = new Point(291, 194);
            txt_precio.Margin = new Padding(2);
            txt_precio.Name = "txt_precio";
            txt_precio.Size = new Size(88, 20);
            txt_precio.TabIndex = 1;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(190, 247);
            cmd_ok.Margin = new Padding(2);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(70, 21);
            cmd_ok.TabIndex = 2;
            cmd_ok.Text = "Aceptar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(286, 247);
            cmd_exit.Margin = new Padding(2);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(70, 21);
            cmd_exit.TabIndex = 3;
            cmd_exit.Text = "Cancelar";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(lbl_desc);
            GroupBox1.Controls.Add(lbldesc);
            GroupBox1.Controls.Add(lbl_stock);
            GroupBox1.Controls.Add(lbl_item);
            GroupBox1.Controls.Add(lblstock);
            GroupBox1.Controls.Add(lblitem);
            GroupBox1.Location = new Point(18, 15);
            GroupBox1.Margin = new Padding(2);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Padding = new Padding(2);
            GroupBox1.Size = new Size(500, 115);
            GroupBox1.TabIndex = 4;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Información item";
            // 
            // lbl_desc
            // 
            lbl_desc.AutoSize = true;
            lbl_desc.Location = new Point(105, 50);
            lbl_desc.Margin = new Padding(2, 0, 2, 0);
            lbl_desc.Name = "lbl_desc";
            lbl_desc.Size = new Size(77, 13);
            lbl_desc.TabIndex = 8;
            lbl_desc.Text = "%descripción%";
            // 
            // lbldesc
            // 
            lbldesc.AutoSize = true;
            lbldesc.Location = new Point(31, 50);
            lbldesc.Margin = new Padding(2, 0, 2, 0);
            lbldesc.Name = "lbldesc";
            lbldesc.Size = new Size(66, 13);
            lbldesc.TabIndex = 7;
            lbldesc.Text = "Descripción:";
            // 
            // lbl_stock
            // 
            lbl_stock.AutoSize = true;
            lbl_stock.Location = new Point(106, 76);
            lbl_stock.Margin = new Padding(2, 0, 2, 0);
            lbl_stock.Name = "lbl_stock";
            lbl_stock.Size = new Size(49, 13);
            lbl_stock.TabIndex = 10;
            lbl_stock.Text = "%stock%";
            // 
            // lbl_item
            // 
            lbl_item.AutoSize = true;
            lbl_item.Location = new Point(106, 26);
            lbl_item.Margin = new Padding(2, 0, 2, 0);
            lbl_item.Name = "lbl_item";
            lbl_item.Size = new Size(42, 13);
            lbl_item.TabIndex = 6;
            lbl_item.Text = "%item%";
            // 
            // lblstock
            // 
            lblstock.AutoSize = true;
            lblstock.Location = new Point(32, 76);
            lblstock.Margin = new Padding(2, 0, 2, 0);
            lblstock.Name = "lblstock";
            lblstock.Size = new Size(38, 13);
            lblstock.TabIndex = 9;
            lblstock.Text = "Stock:";
            // 
            // lblitem
            // 
            lblitem.AutoSize = true;
            lblitem.Location = new Point(32, 26);
            lblitem.Margin = new Padding(2, 0, 2, 0);
            lblitem.Name = "lblitem";
            lblitem.Size = new Size(33, 13);
            lblitem.TabIndex = 5;
            lblitem.Text = "Item: ";
            // 
            // infoagregaitem
            // 
            AcceptButton = cmd_ok;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 284);
            Controls.Add(GroupBox1);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_precio);
            Controls.Add(txt_cantidad);
            Controls.Add(lbl_precio);
            Controls.Add(lbl_cantidad);
            Margin = new Padding(2);
            Name = "infoagregaitem";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Centrex";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            FormClosed += new FormClosedEventHandler(infoagregaitem_FormClosed);
            Load += new EventHandler(infoagregaitem_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_cantidad;
        internal Label lbl_precio;
        internal TextBox txt_cantidad;
        internal TextBox txt_precio;
        internal Button cmd_ok;
        internal Button cmd_exit;
        internal ToolTip ToolTip1;
        internal GroupBox GroupBox1;
        internal Label lbl_desc;
        internal Label lbldesc;
        internal Label lbl_stock;
        internal Label lbl_item;
        internal Label lblstock;
        internal Label lblitem;
    }
}


