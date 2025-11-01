using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class add_comprobante : Form
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
            lbl_comprobante = new Label();
            lbl_puntoVenta = new Label();
            lbl_numero = new Label();
            lbl_tipoComprobante = new Label();
            chk_activo = new CheckBox();
            cmd_exit = new Button();
            cmd_exit.Click += new EventHandler(cmd_exit_Click);
            cmd_ok = new Button();
            cmd_ok.Click += new EventHandler(cmd_ok_Click);
            rb_fiscal = new RadioButton();
            rb_electronico = new RadioButton();
            rb_manual = new RadioButton();
            txt_comprobante = new TextBox();
            cmb_tipoComprobante = new ComboBox();
            cmb_tipoComprobante.KeyPress += new KeyPressEventHandler(cmb_tipoComprobante_KeyPress);
            nup_puntoVenta = new NumericUpDown();
            nup_numero = new NumericUpDown();
            chk_secuencia = new CheckBox();
            gb_tipoComprobante = new GroupBox();
            rb_presupuesto = new RadioButton();
            chk_testing = new CheckBox();
            nup_maxItems = new NumericUpDown();
            lbl_maxItems = new Label();
            chk_contabilizar = new CheckBox();
            TC = new TabControl();
            t_general = new TabPage();
            chk_mueveStock = new CheckBox();
            t_mipyme = new TabPage();
            lbl_alias_CBU_emisor = new Label();
            txt_alias_CBU_emisor = new TextBox();
            lbl_CBU_emisor = new Label();
            txt_CBU_emisor = new TextBox();
            chk_esMipyme = new CheckBox();
            chk_esMipyme.CheckedChanged += new EventHandler(chk_esMipyme_CheckedChanged);
            lbl_comprobanteRelacionado = new Label();
            cmb_comprobanteRelacionado = new ComboBox();
            lbl_modoMiPyme = new Label();
            cmb_modoMiPyme = new ComboBox();
            lbl_comprobanteAnulación = new Label();
            txt_comprobanteAnulacion = new TextBox();
            tt_comprobanteAnulacion = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)nup_puntoVenta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nup_numero).BeginInit();
            gb_tipoComprobante.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nup_maxItems).BeginInit();
            TC.SuspendLayout();
            t_general.SuspendLayout();
            t_mipyme.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_comprobante
            // 
            lbl_comprobante.AutoSize = true;
            lbl_comprobante.Location = new Point(12, 19);
            lbl_comprobante.Name = "lbl_comprobante";
            lbl_comprobante.Size = new Size(70, 13);
            lbl_comprobante.TabIndex = 0;
            lbl_comprobante.Text = "Comprobante";
            // 
            // lbl_puntoVenta
            // 
            lbl_puntoVenta.AutoSize = true;
            lbl_puntoVenta.Location = new Point(378, 95);
            lbl_puntoVenta.Name = "lbl_puntoVenta";
            lbl_puntoVenta.Size = new Size(80, 13);
            lbl_puntoVenta.TabIndex = 2;
            lbl_puntoVenta.Text = "Punto de venta";
            // 
            // lbl_numero
            // 
            lbl_numero.Location = new Point(12, 90);
            lbl_numero.Name = "lbl_numero";
            lbl_numero.Size = new Size(141, 26);
            lbl_numero.TabIndex = 3;
            lbl_numero.Text = "Último número de comprobante emitido";
            // 
            // lbl_tipoComprobante
            // 
            lbl_tipoComprobante.AutoSize = true;
            lbl_tipoComprobante.Location = new Point(12, 57);
            lbl_tipoComprobante.Name = "lbl_tipoComprobante";
            lbl_tipoComprobante.Size = new Size(108, 13);
            lbl_tipoComprobante.TabIndex = 4;
            lbl_tipoComprobante.Text = "Tipo de comprobante";
            // 
            // chk_activo
            // 
            chk_activo.AutoSize = true;
            chk_activo.Location = new Point(106, 283);
            chk_activo.Name = "chk_activo";
            chk_activo.Size = new Size(121, 17);
            chk_activo.TabIndex = 38;
            chk_activo.Text = "Comprobante activo";
            chk_activo.UseVisualStyleBackColor = true;
            // 
            // cmd_exit
            // 
            cmd_exit.DialogResult = DialogResult.Cancel;
            cmd_exit.Location = new Point(312, 453);
            cmd_exit.Name = "cmd_exit";
            cmd_exit.Size = new Size(75, 23);
            cmd_exit.TabIndex = 43;
            cmd_exit.Text = "Salir";
            cmd_exit.UseVisualStyleBackColor = true;
            // 
            // cmd_ok
            // 
            cmd_ok.Location = new Point(214, 453);
            cmd_ok.Name = "cmd_ok";
            cmd_ok.Size = new Size(75, 23);
            cmd_ok.TabIndex = 42;
            cmd_ok.Text = "Guardar";
            cmd_ok.UseVisualStyleBackColor = true;
            // 
            // rb_fiscal
            // 
            rb_fiscal.AutoSize = true;
            rb_fiscal.Location = new Point(144, 19);
            rb_fiscal.Name = "rb_fiscal";
            rb_fiscal.Size = new Size(52, 17);
            rb_fiscal.TabIndex = 44;
            rb_fiscal.TabStop = true;
            rb_fiscal.Text = "Fiscal";
            rb_fiscal.UseVisualStyleBackColor = true;
            // 
            // rb_electronico
            // 
            rb_electronico.AutoSize = true;
            rb_electronico.Location = new Point(252, 19);
            rb_electronico.Name = "rb_electronico";
            rb_electronico.Size = new Size(78, 17);
            rb_electronico.TabIndex = 45;
            rb_electronico.TabStop = true;
            rb_electronico.Text = "Electrónico";
            rb_electronico.UseVisualStyleBackColor = true;
            // 
            // rb_manual
            // 
            rb_manual.AutoSize = true;
            rb_manual.Location = new Point(144, 56);
            rb_manual.Name = "rb_manual";
            rb_manual.Size = new Size(60, 17);
            rb_manual.TabIndex = 46;
            rb_manual.TabStop = true;
            rb_manual.Text = "Manual";
            rb_manual.UseVisualStyleBackColor = true;
            // 
            // txt_comprobante
            // 
            txt_comprobante.Location = new Point(159, 12);
            txt_comprobante.MaxLength = 45;
            txt_comprobante.Name = "txt_comprobante";
            txt_comprobante.Size = new Size(388, 20);
            txt_comprobante.TabIndex = 49;
            // 
            // cmb_tipoComprobante
            // 
            cmb_tipoComprobante.FormattingEnabled = true;
            cmb_tipoComprobante.Location = new Point(159, 52);
            cmb_tipoComprobante.Name = "cmb_tipoComprobante";
            cmb_tipoComprobante.Size = new Size(388, 21);
            cmb_tipoComprobante.TabIndex = 52;
            // 
            // nup_puntoVenta
            // 
            nup_puntoVenta.Location = new Point(464, 93);
            nup_puntoVenta.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            nup_puntoVenta.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nup_puntoVenta.Name = "nup_puntoVenta";
            nup_puntoVenta.Size = new Size(87, 20);
            nup_puntoVenta.TabIndex = 53;
            nup_puntoVenta.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nup_numero
            // 
            nup_numero.Location = new Point(159, 93);
            nup_numero.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            nup_numero.Name = "nup_numero";
            nup_numero.Size = new Size(202, 20);
            nup_numero.TabIndex = 54;
            // 
            // chk_secuencia
            // 
            chk_secuencia.AutoSize = true;
            chk_secuencia.Location = new Point(16, 397);
            chk_secuencia.Name = "chk_secuencia";
            chk_secuencia.Size = new Size(108, 17);
            chk_secuencia.TabIndex = 55;
            chk_secuencia.Text = "Carga secuencial";
            chk_secuencia.UseVisualStyleBackColor = true;
            // 
            // gb_tipoComprobante
            // 
            gb_tipoComprobante.Controls.Add(rb_presupuesto);
            gb_tipoComprobante.Controls.Add(rb_fiscal);
            gb_tipoComprobante.Controls.Add(rb_electronico);
            gb_tipoComprobante.Controls.Add(rb_manual);
            gb_tipoComprobante.Location = new Point(106, 170);
            gb_tipoComprobante.Name = "gb_tipoComprobante";
            gb_tipoComprobante.Size = new Size(346, 95);
            gb_tipoComprobante.TabIndex = 57;
            gb_tipoComprobante.TabStop = false;
            gb_tipoComprobante.Text = "El comprobante es";
            // 
            // rb_presupuesto
            // 
            rb_presupuesto.AutoSize = true;
            rb_presupuesto.Location = new Point(252, 56);
            rb_presupuesto.Name = "rb_presupuesto";
            rb_presupuesto.Size = new Size(84, 17);
            rb_presupuesto.TabIndex = 47;
            rb_presupuesto.TabStop = true;
            rb_presupuesto.Text = "Presupuesto";
            rb_presupuesto.UseVisualStyleBackColor = true;
            // 
            // chk_testing
            // 
            chk_testing.AutoSize = true;
            chk_testing.Location = new Point(250, 283);
            chk_testing.Name = "chk_testing";
            chk_testing.Size = new Size(136, 17);
            chk_testing.TabIndex = 56;
            chk_testing.Text = "Comprobante de testeo";
            chk_testing.UseVisualStyleBackColor = true;
            // 
            // nup_maxItems
            // 
            nup_maxItems.Location = new Point(159, 131);
            nup_maxItems.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            nup_maxItems.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nup_maxItems.Name = "nup_maxItems";
            nup_maxItems.Size = new Size(202, 20);
            nup_maxItems.TabIndex = 59;
            nup_maxItems.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // lbl_maxItems
            // 
            lbl_maxItems.AutoSize = true;
            lbl_maxItems.Location = new Point(12, 131);
            lbl_maxItems.Name = "lbl_maxItems";
            lbl_maxItems.Size = new Size(129, 13);
            lbl_maxItems.TabIndex = 58;
            lbl_maxItems.Text = "Cantidad máxima de items";
            // 
            // chk_contabilizar
            // 
            chk_contabilizar.AutoSize = true;
            chk_contabilizar.Location = new Point(106, 315);
            chk_contabilizar.Name = "chk_contabilizar";
            chk_contabilizar.Size = new Size(80, 17);
            chk_contabilizar.TabIndex = 60;
            chk_contabilizar.Text = "Contabilizar";
            chk_contabilizar.UseVisualStyleBackColor = true;
            // 
            // TC
            // 
            TC.Controls.Add(t_general);
            TC.Controls.Add(t_mipyme);
            TC.Location = new Point(16, 12);
            TC.Name = "TC";
            TC.SelectedIndex = 0;
            TC.Size = new Size(570, 371);
            TC.TabIndex = 61;
            // 
            // t_general
            // 
            t_general.BackColor = SystemColors.Control;
            t_general.Controls.Add(chk_mueveStock);
            t_general.Controls.Add(lbl_comprobante);
            t_general.Controls.Add(chk_contabilizar);
            t_general.Controls.Add(lbl_puntoVenta);
            t_general.Controls.Add(nup_maxItems);
            t_general.Controls.Add(lbl_numero);
            t_general.Controls.Add(lbl_maxItems);
            t_general.Controls.Add(lbl_tipoComprobante);
            t_general.Controls.Add(chk_testing);
            t_general.Controls.Add(chk_activo);
            t_general.Controls.Add(gb_tipoComprobante);
            t_general.Controls.Add(txt_comprobante);
            t_general.Controls.Add(cmb_tipoComprobante);
            t_general.Controls.Add(nup_numero);
            t_general.Controls.Add(nup_puntoVenta);
            t_general.Location = new Point(4, 22);
            t_general.Name = "t_general";
            t_general.Padding = new Padding(3);
            t_general.Size = new Size(562, 345);
            t_general.TabIndex = 0;
            t_general.Text = "General";
            // 
            // chk_mueveStock
            // 
            chk_mueveStock.AutoSize = true;
            chk_mueveStock.Location = new Point(250, 315);
            chk_mueveStock.Name = "chk_mueveStock";
            chk_mueveStock.Size = new Size(88, 17);
            chk_mueveStock.TabIndex = 62;
            chk_mueveStock.Text = "Mueve stock";
            chk_mueveStock.UseVisualStyleBackColor = true;
            // 
            // t_mipyme
            // 
            t_mipyme.BackColor = SystemColors.Control;
            t_mipyme.Controls.Add(txt_comprobanteAnulacion);
            t_mipyme.Controls.Add(lbl_comprobanteAnulación);
            t_mipyme.Controls.Add(lbl_modoMiPyme);
            t_mipyme.Controls.Add(cmb_modoMiPyme);
            t_mipyme.Controls.Add(lbl_alias_CBU_emisor);
            t_mipyme.Controls.Add(txt_alias_CBU_emisor);
            t_mipyme.Controls.Add(lbl_CBU_emisor);
            t_mipyme.Controls.Add(txt_CBU_emisor);
            t_mipyme.Controls.Add(chk_esMipyme);
            t_mipyme.Controls.Add(lbl_comprobanteRelacionado);
            t_mipyme.Controls.Add(cmb_comprobanteRelacionado);
            t_mipyme.Location = new Point(4, 22);
            t_mipyme.Name = "t_mipyme";
            t_mipyme.Padding = new Padding(3);
            t_mipyme.Size = new Size(562, 345);
            t_mipyme.TabIndex = 1;
            t_mipyme.Text = "MiPyme";
            // 
            // lbl_alias_CBU_emisor
            // 
            lbl_alias_CBU_emisor.AutoSize = true;
            lbl_alias_CBU_emisor.Location = new Point(16, 134);
            lbl_alias_CBU_emisor.Name = "lbl_alias_CBU_emisor";
            lbl_alias_CBU_emisor.Size = new Size(87, 13);
            lbl_alias_CBU_emisor.TabIndex = 58;
            lbl_alias_CBU_emisor.Text = "Alias CBU emisor";
            // 
            // txt_alias_CBU_emisor
            // 
            txt_alias_CBU_emisor.Location = new Point(163, 127);
            txt_alias_CBU_emisor.MaxLength = 45;
            txt_alias_CBU_emisor.Name = "txt_alias_CBU_emisor";
            txt_alias_CBU_emisor.Size = new Size(388, 20);
            txt_alias_CBU_emisor.TabIndex = 59;
            // 
            // lbl_CBU_emisor
            // 
            lbl_CBU_emisor.AutoSize = true;
            lbl_CBU_emisor.Location = new Point(16, 74);
            lbl_CBU_emisor.Name = "lbl_CBU_emisor";
            lbl_CBU_emisor.Size = new Size(63, 13);
            lbl_CBU_emisor.TabIndex = 56;
            lbl_CBU_emisor.Text = "CBU Emisor";
            // 
            // txt_CBU_emisor
            // 
            txt_CBU_emisor.Location = new Point(163, 67);
            txt_CBU_emisor.MaxLength = 45;
            txt_CBU_emisor.Name = "txt_CBU_emisor";
            txt_CBU_emisor.Size = new Size(388, 20);
            txt_CBU_emisor.TabIndex = 57;
            // 
            // chk_esMipyme
            // 
            chk_esMipyme.AutoSize = true;
            chk_esMipyme.Location = new Point(19, 23);
            chk_esMipyme.Name = "chk_esMipyme";
            chk_esMipyme.Size = new Size(145, 17);
            chk_esMipyme.TabIndex = 55;
            chk_esMipyme.Text = "Es comprobante MiPyME";
            chk_esMipyme.UseVisualStyleBackColor = true;
            // 
            // lbl_comprobanteRelacionado
            // 
            lbl_comprobanteRelacionado.AutoSize = true;
            lbl_comprobanteRelacionado.Location = new Point(16, 299);
            lbl_comprobanteRelacionado.Name = "lbl_comprobanteRelacionado";
            lbl_comprobanteRelacionado.Size = new Size(128, 13);
            lbl_comprobanteRelacionado.TabIndex = 53;
            lbl_comprobanteRelacionado.Text = "Comprobante relacionado";
            lbl_comprobanteRelacionado.Visible = false;
            // 
            // cmb_comprobanteRelacionado
            // 
            cmb_comprobanteRelacionado.FormattingEnabled = true;
            cmb_comprobanteRelacionado.Location = new Point(163, 294);
            cmb_comprobanteRelacionado.Name = "cmb_comprobanteRelacionado";
            cmb_comprobanteRelacionado.Size = new Size(388, 21);
            cmb_comprobanteRelacionado.TabIndex = 54;
            cmb_comprobanteRelacionado.Visible = false;
            // 
            // lbl_modoMiPyme
            // 
            lbl_modoMiPyme.AutoSize = true;
            lbl_modoMiPyme.Location = new Point(16, 244);
            lbl_modoMiPyme.Name = "lbl_modoMiPyme";
            lbl_modoMiPyme.Size = new Size(74, 13);
            lbl_modoMiPyme.TabIndex = 61;
            lbl_modoMiPyme.Text = "Modo MiPyme";
            // 
            // cmb_modoMiPyme
            // 
            cmb_modoMiPyme.FormattingEnabled = true;
            cmb_modoMiPyme.Location = new Point(163, 239);
            cmb_modoMiPyme.Name = "cmb_modoMiPyme";
            cmb_modoMiPyme.Size = new Size(388, 21);
            cmb_modoMiPyme.TabIndex = 62;
            // 
            // lbl_comprobanteAnulación
            // 
            lbl_comprobanteAnulación.AutoSize = true;
            lbl_comprobanteAnulación.Location = new Point(16, 187);
            lbl_comprobanteAnulación.Name = "lbl_comprobanteAnulación";
            lbl_comprobanteAnulación.Size = new Size(160, 13);
            lbl_comprobanteAnulación.TabIndex = 63;
            lbl_comprobanteAnulación.Text = "¿Es comprobante de anulación?";
            tt_comprobanteAnulacion.SetToolTip(lbl_comprobanteAnulación, "Si el cliente ya anuló el comprobante ponga una S, caso contrario ponga una N");
            // 
            // txt_comprobanteAnulacion
            // 
            txt_comprobanteAnulacion.Location = new Point(194, 180);
            txt_comprobanteAnulacion.MaxLength = 1;
            txt_comprobanteAnulacion.Name = "txt_comprobanteAnulacion";
            txt_comprobanteAnulacion.Size = new Size(63, 20);
            txt_comprobanteAnulacion.TabIndex = 62;
            tt_comprobanteAnulacion.SetToolTip(txt_comprobanteAnulacion, "Si el cliente ya anuló el comprobante ponga una S, caso contrario ponga una N");
            // 
            // add_comprobante
            // 
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 507);
            Controls.Add(TC);
            Controls.Add(chk_secuencia);
            Controls.Add(cmd_exit);
            Controls.Add(cmd_ok);
            Name = "add_comprobante";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar comprobante";
            ((System.ComponentModel.ISupportInitialize)nup_puntoVenta).EndInit();
            ((System.ComponentModel.ISupportInitialize)nup_numero).EndInit();
            gb_tipoComprobante.ResumeLayout(false);
            gb_tipoComprobante.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nup_maxItems).EndInit();
            TC.ResumeLayout(false);
            t_general.ResumeLayout(false);
            t_general.PerformLayout();
            t_mipyme.ResumeLayout(false);
            t_mipyme.PerformLayout();
            FormClosed += new FormClosedEventHandler(add_comprobante_FormClosed);
            Load += new EventHandler(add_comprobante_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        internal Label lbl_comprobante;
        internal Label lbl_puntoVenta;
        internal Label lbl_numero;
        internal Label lbl_tipoComprobante;
        internal CheckBox chk_activo;
        internal Button cmd_exit;
        internal Button cmd_ok;
        internal RadioButton rb_fiscal;
        internal RadioButton rb_electronico;
        internal RadioButton rb_manual;
        internal TextBox txt_comprobante;
        internal ComboBox cmb_tipoComprobante;
        internal NumericUpDown nup_puntoVenta;
        internal NumericUpDown nup_numero;
        internal CheckBox chk_secuencia;
        internal GroupBox gb_tipoComprobante;
        internal RadioButton rb_presupuesto;
        internal CheckBox chk_testing;
        internal NumericUpDown nup_maxItems;
        internal Label lbl_maxItems;
        internal CheckBox chk_contabilizar;
        internal TabControl TC;
        internal TabPage t_general;
        internal TabPage t_mipyme;
        internal Label lbl_alias_CBU_emisor;
        internal TextBox txt_alias_CBU_emisor;
        internal Label lbl_CBU_emisor;
        internal TextBox txt_CBU_emisor;
        internal CheckBox chk_esMipyme;
        internal Label lbl_comprobanteRelacionado;
        internal ComboBox cmb_comprobanteRelacionado;
        internal CheckBox chk_mueveStock;
        internal Label lbl_modoMiPyme;
        internal ComboBox cmb_modoMiPyme;
        internal TextBox txt_comprobanteAnulacion;
        internal ToolTip tt_comprobanteAnulacion;
        internal Label lbl_comprobanteAnulación;
    }
}


