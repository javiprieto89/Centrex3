using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_item : Form
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
            lbl_item = new Label();
            txt_item = new TextBox();
            txt_desc = new TextBox();
            lbl_desc = new Label();
            txt_costo = new TextBox();
            txt_costo.LostFocus += new EventHandler(txt_costo_LostFocus);
            lbl_costo = new Label();
            txt_prclista = new TextBox();
            lbl_preciolista = new Label();
            lbl_categoria = new Label();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            chk_secuencia = new CheckBox();
            cmb_cat = new ComboBox();
            cmb_cat.SelectedIndexChanged += new EventHandler(cmb_cat_SelectedIndexChanged);
            cmb_cat.KeyPress += new KeyPressEventHandler(cmb_cat_KeyPress);
            cmb_marca = new ComboBox();
            cmb_marca.KeyPress += new KeyPressEventHandler(cmb_marca_KeyPress);
            lbl_marca = new Label();
            cmb_proveedor = new ComboBox();
            cmb_proveedor.KeyPress += new KeyPressEventHandler(cmb_proveedor_KeyPress);
            lbl_proveedor = new Label();
            chk_activo = new CheckBox();
            txt_factor = new TextBox();
            txt_factor.LostFocus += new EventHandler(txt_factor_LostFocus);
            lbl_factor = new Label();
            lbl_cantidad = new Label();
            txt_cantidad = new TextBox();
            chk_descuento = new CheckBox();
            chk_descuento.CheckedChanged += new EventHandler(chk_descuento_CheckedChanged);
            psearch_proveedor = new PictureBox();
            psearch_proveedor.Click += new EventHandler(psearch_proveedor_Click);
            psearch_marca = new PictureBox();
            psearch_marca.Click += new EventHandler(psearch_marca_Click);
            psearch_tipo = new PictureBox();
            psearch_tipo.Click += new EventHandler(psearch_tipo_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            pic_search_iva = new PictureBox();
            pic_search_iva.DoubleClick += new EventHandler(pic_search_iva_Click);
            cmb_iva = new ComboBox();
            cmb_iva.KeyPress += new KeyPressEventHandler(cmb_iva_KeyPress);
            lbl_iva = new Label();
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_marca).BeginInit();
            ((System.ComponentModel.ISupportInitialize)psearch_tipo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_search_iva).BeginInit();
            SuspendLayout();
            // 
            // lbl_item
            // 
            lbl_item.AutoSize = true;
            lbl_item.Location = new Point(29, 65);
            lbl_item.Name = "lbl_item";
            lbl_item.Size = new Size(40, 13);
            lbl_item.TabIndex = 0;
            lbl_item.Text = "Código";
            // 
            // txt_item
            // 
            txt_item.Location = new Point(128, 61);
            txt_item.Name = "txt_item";
            txt_item.Size = new Size(259, 20);
            txt_item.TabIndex = 0;
            // 
            // txt_desc
            // 
            txt_desc.Location = new Point(128, 101);
            txt_desc.MaxLength = 54;
            txt_desc.Name = "txt_desc";
            txt_desc.Size = new Size(259, 20);
            txt_desc.TabIndex = 1;
            // 
            // lbl_desc
            // 
            lbl_desc.AutoSize = true;
            lbl_desc.Location = new Point(29, 105);
            lbl_desc.Name = "lbl_desc";
            lbl_desc.Size = new Size(50, 13);
            lbl_desc.TabIndex = 2;
            lbl_desc.Text = "Producto";
            // 
            // txt_costo
            // 
            txt_costo.Location = new Point(128, 141);
            txt_costo.Name = "txt_costo";
            txt_costo.Size = new Size(259, 20);
            txt_costo.TabIndex = 2;
            // 
            // lbl_costo
            // 
            lbl_costo.AutoSize = true;
            lbl_costo.Location = new Point(29, 145);
            lbl_costo.Name = "lbl_costo";
            lbl_costo.Size = new Size(34, 13);
            lbl_costo.TabIndex = 4;
            lbl_costo.Text = "Costo";
            // 
            // txt_prclista
            // 
            txt_prclista.Location = new Point(128, 221);
            txt_prclista.Name = "txt_prclista";
            txt_prclista.Size = new Size(259, 20);
            txt_prclista.TabIndex = 4;
            // 
            // lbl_preciolista
            // 
            lbl_preciolista.AutoSize = true;
            lbl_preciolista.Location = new Point(26, 225);
            lbl_preciolista.Name = "lbl_preciolista";
            lbl_preciolista.Size = new Size(73, 13);
            lbl_preciolista.TabIndex = 6;
            lbl_preciolista.Text = "Precio de lista";
            // 
            // lbl_categoria
            // 
            lbl_categoria.AutoSize = true;
            lbl_categoria.Location = new Point(26, 265);
            lbl_categoria.Name = "lbl_categoria";
            lbl_categoria.Size = new Size(54, 13);
            lbl_categoria.TabIndex = 8;
            lbl_categoria.Text = "Categoría";
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(227, 569);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 14;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(28, 525);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 12;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmb_cat
            // 
            cmb_cat.FormattingEnabled = true;
            cmb_cat.Location = new Point(128, 261);
            cmb_cat.Name = "cmb_cat";
            cmb_cat.Size = new Size(259, 21);
            cmb_cat.TabIndex = 5;
            // 
            // cmb_marca
            // 
            cmb_marca.FormattingEnabled = true;
            cmb_marca.Location = new Point(128, 302);
            cmb_marca.Name = "cmb_marca";
            cmb_marca.Size = new Size(259, 21);
            cmb_marca.TabIndex = 6;
            // 
            // lbl_marca
            // 
            lbl_marca.AutoSize = true;
            lbl_marca.Location = new Point(26, 305);
            lbl_marca.Name = "lbl_marca";
            lbl_marca.Size = new Size(37, 13);
            lbl_marca.TabIndex = 14;
            lbl_marca.Text = "Marca";
            // 
            // cmb_proveedor
            // 
            cmb_proveedor.FormattingEnabled = true;
            cmb_proveedor.Location = new Point(128, 343);
            cmb_proveedor.Name = "cmb_proveedor";
            cmb_proveedor.Size = new Size(259, 21);
            cmb_proveedor.TabIndex = 7;
            // 
            // lbl_proveedor
            // 
            lbl_proveedor.AutoSize = true;
            lbl_proveedor.Location = new Point(27, 345);
            lbl_proveedor.Name = "lbl_proveedor";
            lbl_proveedor.Size = new Size(56, 13);
            lbl_proveedor.TabIndex = 16;
            lbl_proveedor.Text = "Proveedor";
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(28, 478);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(78, 17);
            chk_activo.TabIndex = 11;
            chk_activo.Text = "Item activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_factor
            // 
            txt_factor.Location = new Point(128, 181);
            txt_factor.Name = "txt_factor";
            txt_factor.Size = new Size(259, 20);
            txt_factor.TabIndex = 3;
            // 
            // lbl_factor
            // 
            lbl_factor.AutoSize = true;
            lbl_factor.Location = new Point(26, 185);
            lbl_factor.Name = "lbl_factor";
            lbl_factor.Size = new Size(37, 13);
            lbl_factor.TabIndex = 41;
            lbl_factor.Text = "Factor";
            // 
            // lbl_cantidad
            // 
            lbl_cantidad.AutoSize = true;
            lbl_cantidad.Location = new Point(25, 385);
            lbl_cantidad.Name = "lbl_cantidad";
            lbl_cantidad.Size = new Size(49, 13);
            lbl_cantidad.TabIndex = 50;
            lbl_cantidad.Text = "Cantidad";
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(127, 384);
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(259, 20);
            txt_cantidad.TabIndex = 8;
            txt_cantidad.Text = "1000000";
            // 
            // chk_descuento
            // 
            chk_descuento.AutoSize = true;
            chk_descuento.Location = new Point(32, 21);
            chk_descuento.Name = "chk_descuento";
            chk_descuento.Size = new Size(106, 17);
            chk_descuento.TabIndex = 9;
            chk_descuento.Text = "Es un descuento";
            chk_descuento.UseVisualStyleBackColor = true;
            // 
            // psearch_proveedor
            // 
            psearch_proveedor.Image = My.Resources.Resources.iconoLupa;
            psearch_proveedor.Location = new Point(393, 342);
            psearch_proveedor.Name = "psearch_proveedor";
            psearch_proveedor.Size = new Size(22, 22);
            psearch_proveedor.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_proveedor.TabIndex = 28;
            psearch_proveedor.TabStop = false;
            // 
            // psearch_marca
            // 
            psearch_marca.Image = My.Resources.Resources.iconoLupa;
            psearch_marca.Location = new Point(393, 298);
            psearch_marca.Name = "psearch_marca";
            psearch_marca.Size = new Size(22, 22);
            psearch_marca.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_marca.TabIndex = 27;
            psearch_marca.TabStop = false;
            // 
            // psearch_tipo
            // 
            psearch_tipo.Image = My.Resources.Resources.iconoLupa;
            psearch_tipo.Location = new Point(393, 256);
            psearch_tipo.Name = "psearch_tipo";
            psearch_tipo.Size = new Size(22, 22);
            psearch_tipo.SizeMode = PictureBoxSizeMode.AutoSize;
            psearch_tipo.TabIndex = 26;
            psearch_tipo.TabStop = false;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(127, 569);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 51;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // pic_search_iva
            // 
            pic_search_iva.Image = My.Resources.Resources.iconoLupa;
            pic_search_iva.Location = new Point(393, 423);
            pic_search_iva.Name = "pic_search_iva";
            pic_search_iva.Size = new Size(22, 22);
            pic_search_iva.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_search_iva.TabIndex = 54;
            pic_search_iva.TabStop = false;
            // 
            // cmb_iva
            // 
            cmb_iva.FormattingEnabled = true;
            cmb_iva.Location = new Point(127, 424);
            cmb_iva.Name = "cmb_iva";
            cmb_iva.Size = new Size(259, 21);
            cmb_iva.TabIndex = 52;
            // 
            // lbl_iva
            // 
            lbl_iva.AutoSize = true;
            lbl_iva.Location = new Point(26, 425);
            lbl_iva.Name = "lbl_iva";
            lbl_iva.Size = new Size(33, 13);
            lbl_iva.TabIndex = 53;
            lbl_iva.Text = "I.V.A.";
            // 
            // add_item
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 619);
            Controls.Add(pic_search_iva);
            Controls.Add(cmb_iva);
            Controls.Add(lbl_iva);
            Controls.Add(cmd_ok);
            Controls.Add(chk_descuento);
            Controls.Add(chk_activo);
            Controls.Add(txt_cantidad);
            Controls.Add(lbl_cantidad);
            Controls.Add(txt_factor);
            Controls.Add(lbl_factor);
            Controls.Add(psearch_proveedor);
            Controls.Add(psearch_marca);
            Controls.Add(psearch_tipo);
            Controls.Add(cmb_proveedor);
            Controls.Add(lbl_proveedor);
            Controls.Add(cmb_marca);
            Controls.Add(lbl_marca);
            Controls.Add(cmb_cat);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(lbl_categoria);
            Controls.Add(txt_prclista);
            Controls.Add(lbl_preciolista);
            Controls.Add(txt_costo);
            Controls.Add(lbl_costo);
            Controls.Add(txt_desc);
            Controls.Add(lbl_desc);
            Controls.Add(txt_item);
            Controls.Add(lbl_item);
            Name = "add_item";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Insertar producto";
            ((System.ComponentModel.ISupportInitialize)psearch_proveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_marca).EndInit();
            ((System.ComponentModel.ISupportInitialize)psearch_tipo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_search_iva).EndInit();
            Load += new EventHandler(add_item_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_item;
        internal TextBox txt_item;
        internal TextBox txt_desc;
        internal Label lbl_desc;
        internal TextBox txt_costo;
        internal Label lbl_costo;
        internal TextBox txt_prclista;
        internal Label lbl_preciolista;
        internal Label lbl_categoria;
        internal Button cmd_exit;
        internal CheckBox chk_secuencia;
        internal ComboBox cmb_cat;
        internal ComboBox cmb_marca;
        internal Label lbl_marca;
        internal ComboBox cmb_proveedor;
        internal Label lbl_proveedor;
        internal PictureBox psearch_tipo;
        internal PictureBox psearch_marca;
        internal PictureBox psearch_proveedor;
        internal CheckBox chk_activo;
        internal TextBox txt_factor;
        internal Label lbl_factor;
        internal Label lbl_cantidad;
        internal TextBox txt_cantidad;
        internal CheckBox chk_descuento;
        internal Button cmd_ok;
        internal PictureBox pic_search_iva;
        internal ComboBox cmb_iva;
        internal Label lbl_iva;
    }
}


