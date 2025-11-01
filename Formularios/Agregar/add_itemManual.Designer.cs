using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_itemManual : Form
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
            lbl_item = new Label();
            txt_item = new TextBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            txt_precio = new TextBox();
            txt_precio.KeyPress += new KeyPressEventHandler(txt_precio_KeyPress);
            txt_cantidad = new TextBox();
            txt_cantidad.KeyPress += new KeyPressEventHandler(txt_cantidad_KeyPress);
            lbl_precio = new Label();
            lbl_cantidad = new Label();
            tt_noGuarda = new ToolTip(components);
            SuspendLayout();
            // 
            // lbl_item
            // 
            lbl_item.AutoSize = true;
            lbl_item.Location = new Point(22, 31);
            lbl_item.Name = "lbl_item";
            lbl_item.Size = new Size(64, 13);
            lbl_item.TabIndex = 0;
            lbl_item.Text = "Item manual";
            // 
            // txt_item
            // 
            txt_item.Location = new Point(108, 24);
            txt_item.MaxLength = 54;
            txt_item.Name = "txt_item";
            txt_item.Size = new Size(307, 20);
            txt_item.TabIndex = 1;
            tt_noGuarda.SetToolTip(txt_item, "RECUERDE: Este item estará solamente asociado a este pedido y no se guardará como" + " un item más por lo cual no podrá volver a encontrarlo");
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(231, 179);
            cmd_exit.Margin = new Padding(2);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(70, 21);
            cmd_exit.TabIndex = 9;
            cmd_exit.Text = "Cancelar";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(135, 179);
            cmd_ok.Margin = new Padding(2);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(70, 21);
            cmd_ok.TabIndex = 7;
            cmd_ok.Text = "Aceptar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // txt_precio
            // 
            txt_precio.Location = new Point(236, 126);
            txt_precio.Margin = new Padding(2);
            txt_precio.Name = "txt_precio";
            txt_precio.Size = new Size(88, 20);
            txt_precio.TabIndex = 5;
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(115, 126);
            txt_cantidad.Margin = new Padding(2);
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(88, 20);
            txt_cantidad.TabIndex = 4;
            // 
            // lbl_precio
            // 
            lbl_precio.AutoSize = true;
            lbl_precio.Location = new Point(234, 94);
            lbl_precio.Margin = new Padding(2, 0, 2, 0);
            lbl_precio.Name = "lbl_precio";
            lbl_precio.Size = new Size(37, 13);
            lbl_precio.TabIndex = 8;
            lbl_precio.Text = "Precio";
            // 
            // lbl_cantidad
            // 
            lbl_cantidad.AutoSize = true;
            lbl_cantidad.Location = new Point(113, 94);
            lbl_cantidad.Margin = new Padding(2, 0, 2, 0);
            lbl_cantidad.Name = "lbl_cantidad";
            lbl_cantidad.Size = new Size(49, 13);
            lbl_cantidad.TabIndex = 6;
            lbl_cantidad.Text = "Cantidad";
            // 
            // tt_noGuarda
            // 
            tt_noGuarda.Tag = "";
            // 
            // add_itemManual
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 224);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(txt_precio);
            Controls.Add(txt_cantidad);
            Controls.Add(lbl_precio);
            Controls.Add(lbl_cantidad);
            Controls.Add(txt_item);
            Controls.Add(lbl_item);
            Name = "add_itemManual";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agregar item manual";
            ResumeLayout(false);
            PerformLayout();

        }

        internal Label lbl_item;
        internal TextBox txt_item;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal TextBox txt_precio;
        internal TextBox txt_cantidad;
        internal Label lbl_precio;
        internal Label lbl_cantidad;
        internal ToolTip tt_noGuarda;
    }
}


