using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_itemImpuesto : Form
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
            chk_activo = new CheckBox();
            cmb_items = new ComboBox();
            cmb_items.KeyPress += new KeyPressEventHandler(cmb_items_KeyPress);
            lbl_impuesto = new Label();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_item = new Label();
            cmb_impuestos = new ComboBox();
            cmb_impuestos.KeyPress += new KeyPressEventHandler(cmb_impuestos_KeyPress);
            pic_search_item = new PictureBox();
            pic_search_item.DoubleClick += new EventHandler(pic_search_item_Click);
            pic_search_impuestos = new PictureBox();
            pic_search_impuestos.DoubleClick += new EventHandler(pic_search_impuestos_Click);
            ((System.ComponentModel.ISupportInitialize)pic_search_item).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_search_impuestos).BeginInit();
            SuspendLayout();
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(25, 100);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(100, 17);
            chk_activo.TabIndex = 28;
            chk_activo.Text = "Relación activa";
            chk_activo.UseVisualStyleBackColor = true;
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
            // lbl_impuesto
            // 
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Location = new Point(22, 69);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.Size = new Size(50, 13);
            lbl_impuesto.TabIndex = 33;
            lbl_impuesto.Text = "Impuesto";
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(25, 123);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 29;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(203, 163);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 31;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(105, 163);
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
            // cmb_impuestos
            // 
            cmb_impuestos.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmb_impuestos.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_impuestos.FormattingEnabled = true;
            cmb_impuestos.Location = new Point(73, 61);
            cmb_impuestos.Name = "cmb_impuestos";
            cmb_impuestos.Size = new Size(259, 21);
            cmb_impuestos.TabIndex = 35;
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
            // pic_search_impuestos
            // 
            pic_search_impuestos.Image = My.Resources.Resources.iconoLupa;
            pic_search_impuestos.Location = new Point(338, 61);
            pic_search_impuestos.Name = "pic_search_impuestos";
            pic_search_impuestos.Size = new Size(22, 22);
            pic_search_impuestos.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_search_impuestos.TabIndex = 51;
            pic_search_impuestos.TabStop = false;
            // 
            // add_itemImpuesto
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 209);
            Controls.Add(pic_search_impuestos);
            Controls.Add(pic_search_item);
            Controls.Add(cmb_impuestos);
            Controls.Add(chk_activo);
            Controls.Add(cmb_items);
            Controls.Add(lbl_impuesto);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_item);
            Name = "add_itemImpuesto";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Relacionar Item - Impuestos";
            ((System.ComponentModel.ISupportInitialize)pic_search_item).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_search_impuestos).EndInit();
            FormClosed += new FormClosedEventHandler(add_modeloa_FormClosed);
            Load += new EventHandler(add_modeloa_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal CheckBox chk_activo;
        internal ComboBox cmb_items;
        internal Label lbl_impuesto;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_item;
        internal ComboBox cmb_impuestos;
        internal PictureBox pic_search_item;
        internal PictureBox pic_search_impuestos;
    }
}


