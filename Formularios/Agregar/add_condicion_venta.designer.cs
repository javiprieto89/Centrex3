using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_condicion_venta : Form
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
            txt_recargo = new TextBox();
            lbl_recargo = new Label();
            chk_activo = new CheckBox();
            txt_condicion = new TextBox();
            chk_secuencia = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            lbl_condicion = new Label();
            txt_vencimiento = new TextBox();
            lbl_vencimiento = new Label();
            SuspendLayout();
            // 
            // txt_recargo
            // 
            txt_recargo.Location = new Point(174, 100);
            txt_recargo.Name = "txt_recargo";
            txt_recargo.Size = new Size(259, 20);
            txt_recargo.TabIndex = 2;
            // 
            // lbl_recargo
            // 
            lbl_recargo.AutoSize = true;
            lbl_recargo.Location = new Point(20, 104);
            lbl_recargo.Name = "lbl_recargo";
            lbl_recargo.Size = new Size(65, 13);
            lbl_recargo.TabIndex = 20;
            lbl_recargo.Text = "Recargo (%)";
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(23, 145);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(105, 17);
            chk_activo.TabIndex = 3;
            chk_activo.Text = "Condición activa";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // txt_condicion
            // 
            txt_condicion.Location = new Point(174, 36);
            txt_condicion.Name = "txt_condicion";
            txt_condicion.Size = new Size(259, 20);
            txt_condicion.TabIndex = 0;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(23, 177);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 4;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.Location = new Point(247, 220);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 6;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(149, 220);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 5;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // lbl_condicion
            // 
            lbl_condicion.AutoSize = true;
            lbl_condicion.Location = new Point(20, 36);
            lbl_condicion.Name = "lbl_condicion";
            lbl_condicion.Size = new Size(54, 13);
            lbl_condicion.TabIndex = 14;
            lbl_condicion.Text = "Condición";
            // 
            // txt_vencimiento
            // 
            txt_vencimiento.Location = new Point(174, 68);
            txt_vencimiento.Name = "txt_vencimiento";
            txt_vencimiento.Size = new Size(259, 20);
            txt_vencimiento.TabIndex = 1;
            // 
            // lbl_vencimiento
            // 
            lbl_vencimiento.AutoSize = true;
            lbl_vencimiento.Location = new Point(20, 70);
            lbl_vencimiento.Name = "lbl_vencimiento";
            lbl_vencimiento.Size = new Size(95, 13);
            lbl_vencimiento.TabIndex = 22;
            lbl_vencimiento.Text = "Vencimiento (días)";
            // 
            // add_condicion_venta
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 264);
            Controls.Add(txt_vencimiento);
            Controls.Add(lbl_vencimiento);
            Controls.Add(txt_recargo);
            Controls.Add(lbl_recargo);
            Controls.Add(chk_activo);
            Controls.Add(txt_condicion);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Controls.Add(lbl_condicion);
            Name = "add_condicion_venta";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar condición de venta";
            FormClosed += new FormClosedEventHandler(Add_marcai_FormClosed);
            Load += new EventHandler(Add_condicion_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal TextBox txt_recargo;
        internal Label lbl_recargo;
        internal CheckBox chk_activo;
        internal TextBox txt_condicion;
        internal CheckBox chk_secuencia;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal Label lbl_condicion;
        internal TextBox txt_vencimiento;
        internal Label lbl_vencimiento;
    }
}
