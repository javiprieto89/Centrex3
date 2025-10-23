using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_impuesto : Form
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
            txt_porcentaje = new TextBox();
            lbl_porcentaje = new Label();
            chk_activo = new CheckBox();
            txt_nombre = new TextBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_nombre = new Label();
            chk_esRetencion = new CheckBox();
            chk_esPercepcion = new CheckBox();
            SuspendLayout();
            // 
            // txt_porcentaje
            // 
            txt_porcentaje.Location = new Point(98, 67);
            txt_porcentaje.Name = "txt_porcentaje";
            txt_porcentaje.Size = new Size(259, 20);
            txt_porcentaje.TabIndex = 1;
            // 
            // lbl_porcentaje
            // 
            lbl_porcentaje.AutoSize = true;
            lbl_porcentaje.Location = new Point(20, 74);
            lbl_porcentaje.Name = "lbl_porcentaje";
            lbl_porcentaje.Size = new Size(75, 13);
            lbl_porcentaje.TabIndex = 20;
            lbl_porcentaje.Text = "Porcentaje (%)";
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(23, 166);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(101, 17);
            chk_activo.TabIndex = 4;
            chk_activo.Text = "Impuesto activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(98, 29);
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(259, 20);
            txt_nombre.TabIndex = 0;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(23, 198);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 5;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(201, 246);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 7;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(103, 246);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 6;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_nombre
            // 
            lbl_nombre.AutoSize = true;
            lbl_nombre.Location = new Point(20, 32);
            lbl_nombre.Name = "lbl_nombre";
            lbl_nombre.Size = new Size(50, 13);
            lbl_nombre.TabIndex = 19;
            lbl_nombre.Text = "Impuesto";
            // 
            // chk_esRetencion
            // 
            chk_esRetencion.AutoSize = true;
            chk_esRetencion.Location = new Point(62, 122);
            chk_esRetencion.Name = "chk_esRetencion";
            chk_esRetencion.Size = new Size(85, 17);
            chk_esRetencion.TabIndex = 2;
            chk_esRetencion.Text = "Es retención";
            chk_esRetencion.UseVisualStyleBackColor = true;
            // 
            // chk_esPercepcion
            // 
            chk_esPercepcion.AutoSize = true;
            chk_esPercepcion.Location = new Point(214, 122);
            chk_esPercepcion.Name = "chk_esPercepcion";
            chk_esPercepcion.Size = new Size(94, 17);
            chk_esPercepcion.TabIndex = 3;
            chk_esPercepcion.Text = "Es percepción";
            chk_esPercepcion.UseVisualStyleBackColor = true;
            // 
            // add_impuesto
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 292);
            Controls.Add(chk_esPercepcion);
            Controls.Add(chk_esRetencion);
            Controls.Add(txt_porcentaje);
            Controls.Add(lbl_porcentaje);
            Controls.Add(chk_activo);
            Controls.Add(txt_nombre);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_nombre);
            Name = "add_impuesto";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar impuesto";
            FormClosed += new FormClosedEventHandler(add_marcai_FormClosed);
            Load += new EventHandler(add_descuento_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TextBox txt_porcentaje;
        internal Label lbl_porcentaje;
        internal CheckBox chk_activo;
        internal TextBox txt_nombre;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_nombre;
        internal CheckBox chk_esRetencion;
        internal CheckBox chk_esPercepcion;
    }
}


