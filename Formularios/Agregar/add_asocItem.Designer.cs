using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_asocItem : Form
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
            txt_descriptItem = new TextBox();
            txt_descriptAsocItem = new TextBox();
            lbl_item = new Label();
            lbl_asocItem = new Label();
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            chk_secuencia = new CheckBox();
            pic_searchItem = new PictureBox();
            pic_searchItem.Click += new EventHandler(pic_searchItem_Click);
            pic_searchAsocItem = new PictureBox();
            pic_searchAsocItem.Click += new EventHandler(pic_searchAsocItem_Click);
            lbl_cantidad = new Label();
            txt_cantidad = new TextBox();
            txt_cantidad.KeyPress += new KeyPressEventHandler(txt_cantidad_KeyPress);
            txt_asocItem = new TextBox();
            txt_item = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pic_searchItem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchAsocItem).BeginInit();
            SuspendLayout();
            // 
            // txt_descriptItem
            // 
            txt_descriptItem.BackColor = SystemColors.Window;
            txt_descriptItem.Location = new Point(170, 54);
            txt_descriptItem.Name = "txt_descriptItem";
            txt_descriptItem.ReadOnly = true;
            txt_descriptItem.Size = new Size(489, 20);
            txt_descriptItem.TabIndex = 1;
            // 
            // txt_descriptAsocItem
            // 
            txt_descriptAsocItem.BackColor = SystemColors.Window;
            txt_descriptAsocItem.Location = new Point(170, 126);
            txt_descriptAsocItem.Name = "txt_descriptAsocItem";
            txt_descriptAsocItem.ReadOnly = true;
            txt_descriptAsocItem.Size = new Size(489, 20);
            txt_descriptAsocItem.TabIndex = 3;
            // 
            // lbl_item
            // 
            lbl_item.AutoSize = true;
            lbl_item.Location = new Point(31, 27);
            lbl_item.Name = "lbl_item";
            lbl_item.Size = new Size(50, 13);
            lbl_item.TabIndex = 2;
            lbl_item.Text = "Producto";
            // 
            // lbl_asocItem
            // 
            lbl_asocItem.AutoSize = true;
            lbl_asocItem.Location = new Point(31, 99);
            lbl_asocItem.Name = "lbl_asocItem";
            lbl_asocItem.Size = new Size(96, 13);
            lbl_asocItem.TabIndex = 3;
            lbl_asocItem.Text = "Producto asociado";
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(248, 247);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(218, 47);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Asociar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Checked = true;
            chk_secuencia.CheckState = CheckState.Checked;
            chk_secuencia.Location = new Point(34, 327);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 6;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // pic_searchItem
            // 
            pic_searchItem.Image = My.Resources.Resources.iconoLupa;
            pic_searchItem.Location = new Point(677, 52);
            pic_searchItem.Name = "pic_searchItem";
            pic_searchItem.Size = new Size(22, 22);
            pic_searchItem.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchItem.TabIndex = 49;
            pic_searchItem.TabStop = false;
            // 
            // pic_searchAsocItem
            // 
            pic_searchAsocItem.Image = My.Resources.Resources.iconoLupa;
            pic_searchAsocItem.Location = new Point(677, 126);
            pic_searchAsocItem.Name = "pic_searchAsocItem";
            pic_searchAsocItem.Size = new Size(22, 22);
            pic_searchAsocItem.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_searchAsocItem.TabIndex = 50;
            pic_searchAsocItem.TabStop = false;
            // 
            // lbl_cantidad
            // 
            lbl_cantidad.AutoSize = true;
            lbl_cantidad.Location = new Point(31, 175);
            lbl_cantidad.Name = "lbl_cantidad";
            lbl_cantidad.Size = new Size(49, 13);
            lbl_cantidad.TabIndex = 52;
            lbl_cantidad.Text = "Cantidad";
            // 
            // txt_cantidad
            // 
            txt_cantidad.BackColor = SystemColors.Window;
            txt_cantidad.Location = new Point(34, 205);
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(61, 20);
            txt_cantidad.TabIndex = 4;
            // 
            // txt_asocItem
            // 
            txt_asocItem.BackColor = SystemColors.Window;
            txt_asocItem.Location = new Point(26, 126);
            txt_asocItem.Name = "txt_asocItem";
            txt_asocItem.ReadOnly = true;
            txt_asocItem.Size = new Size(116, 20);
            txt_asocItem.TabIndex = 2;
            // 
            // txt_item
            // 
            txt_item.BackColor = SystemColors.Window;
            txt_item.Location = new Point(26, 54);
            txt_item.Name = "txt_item";
            txt_item.ReadOnly = true;
            txt_item.Size = new Size(116, 20);
            txt_item.TabIndex = 0;
            // 
            // add_asocItem
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(715, 354);
            Controls.Add(txt_item);
            Controls.Add(txt_asocItem);
            Controls.Add(lbl_cantidad);
            Controls.Add(txt_cantidad);
            Controls.Add(pic_searchAsocItem);
            Controls.Add(pic_searchItem);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_asocItem);
            Controls.Add(lbl_item);
            Controls.Add(txt_descriptAsocItem);
            Controls.Add(txt_descriptItem);
            Name = "add_asocItem";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Asociar productos";
            ((System.ComponentModel.ISupportInitialize)pic_searchItem).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_searchAsocItem).EndInit();
            Load += new EventHandler(add_asocItem_Load);
            FormClosed += new FormClosedEventHandler(add_asocItem_FormClosed);
            ResumeLayout(false);
            PerformLayout();

        }

        internal TextBox txt_descriptItem;
        internal TextBox txt_descriptAsocItem;
        internal Label lbl_item;
        internal Label lbl_asocItem;
        internal Button cmd_ok;
        internal CheckBox chk_secuencia;
        internal PictureBox pic_searchItem;
        internal PictureBox pic_searchAsocItem;
        internal Label lbl_cantidad;
        internal TextBox txt_cantidad;
        internal TextBox txt_asocItem;
        internal TextBox txt_item;
    }
}


