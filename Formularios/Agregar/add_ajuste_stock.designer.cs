using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_ajuste_stock : Form
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
            cmb_items = new ComboBox();
            lbl_cantidad = new Label();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_item = new Label();
            pic_search_item = new PictureBox();
            pic_search_item.DoubleClick += new EventHandler(pic_search_item_Click);
            txt_cantidad = new TextBox();
            txt_cantidad.KeyPress += new KeyPressEventHandler(txt_cantidad_KeyPress);
            txt_notas = new TextBox();
            lbl_notas = new Label();
            ((System.ComponentModel.ISupportInitialize)pic_search_item).BeginInit();
            SuspendLayout();
            // 
            // cmb_items
            // 
            cmb_items.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_items.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_items.FormattingEnabled = true;
            cmb_items.Location = new Point(73, 23);
            cmb_items.Name = "cmb_items";
            cmb_items.Size = new Size(259, 21);
            cmb_items.TabIndex = 26;
            // 
            // lbl_cantidad
            // 
            lbl_cantidad.AutoSize = true;
            lbl_cantidad.Location = new Point(22, 66);
            lbl_cantidad.Name = "lbl_cantidad";
            lbl_cantidad.Size = new Size(49, 13);
            lbl_cantidad.TabIndex = 33;
            lbl_cantidad.Text = "Cantidad";
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(201, 279);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 31;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(103, 279);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 30;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_item
            // 
            lbl_item.AutoSize = true;
            lbl_item.Location = new Point(23, 31);
            lbl_item.Name = "lbl_item";
            lbl_item.Size = new Size(27, 13);
            lbl_item.TabIndex = 32;
            lbl_item.Text = "Item";
            // 
            // pic_search_item
            // 
            pic_search_item.Image = My.Resources.Resources.iconoLupa;
            pic_search_item.Location = new Point(338, 23);
            pic_search_item.Name = "pic_search_item";
            pic_search_item.Size = new Size(22, 22);
            pic_search_item.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_search_item.TabIndex = 50;
            pic_search_item.TabStop = false;
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(73, 63);
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(259, 20);
            txt_cantidad.TabIndex = 51;
            // 
            // txt_notas
            // 
            txt_notas.Location = new Point(73, 103);
            txt_notas.Multiline = true;
            txt_notas.Name = "txt_notas";
            txt_notas.Size = new Size(259, 121);
            txt_notas.TabIndex = 53;
            // 
            // lbl_notas
            // 
            lbl_notas.AutoSize = true;
            lbl_notas.Location = new Point(22, 106);
            lbl_notas.Name = "lbl_notas";
            lbl_notas.Size = new Size(35, 13);
            lbl_notas.TabIndex = 52;
            lbl_notas.Text = "Notas";
            // 
            // add_ajuste_stock
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 315);
            Controls.Add(txt_notas);
            Controls.Add(lbl_notas);
            Controls.Add(txt_cantidad);
            Controls.Add(pic_search_item);
            Controls.Add(cmb_items);
            Controls.Add(lbl_cantidad);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_item);
            Name = "add_ajuste_stock";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Relacionar Item - Impuestos";
            ((System.ComponentModel.ISupportInitialize)pic_search_item).EndInit();
            FormClosed += new FormClosedEventHandler(add_ajuste_stock_FormClosed);
            Load += new EventHandler(add_ajuste_stock_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal ComboBox cmb_items;
        internal Label lbl_cantidad;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_item;
        internal PictureBox pic_search_item;
        internal TextBox txt_cantidad;
        internal TextBox txt_notas;
        internal Label lbl_notas;
    }
}

