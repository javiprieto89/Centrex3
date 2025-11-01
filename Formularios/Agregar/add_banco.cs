using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_banco : Form
    {
        public add_banco()
        {
            InitializeComponent();
        }

        private async void add_banco_Load(object sender, EventArgs e)
        {
            try
            {
                // ================================
                // 🔹 Cargar ComboBox de Países con EF
                // ================================
                using (var ctx = new CentrexDbContext())
                {
                    var queryPaises = ctx.PaisEntity
                        .OrderBy(p => p.Pais)
                        .Select(p => new
                        {
                            p.IdPais,
                            p.Pais
                        });

                    await CargarComboAsync(
                        cmb_pais,
                        queryPaises,
                        displayMember: "Pais",
                        valueMember: "IdPais",
                        soloActivos: true
                    );
                }

                // ================================
                // 🔹 Si estamos editando o borrando
                // ================================
                if (edicion || borrado)
                {
                    txt_nombre.Text = edita_banco?.Nombre ?? string.Empty;

                    if (edita_banco is not null)
                    {
                        cmb_pais.SelectedValue = edita_banco.IdPais;
                        txt_bancoN.Text = edita_banco.NBanco?.ToString() ?? string.Empty;
                        chk_activo.Checked = edita_banco.Activo;
                    }

                    chk_secuencia.Enabled = false;
                }

                // ================================
                // 🔹 Si estamos borrando, desactivar controles
                // ================================
                if (borrado)
                {
                    txt_nombre.Enabled = false;
                    cmb_pais.Enabled = false;
                    txt_bancoN.Enabled = false;
                    chk_activo.Enabled = false;
                    chk_secuencia.Enabled = false;
                    cmd_ok.Enabled = false;
                    cmd_exit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar formulario: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_nombre.Text))
                {
                    Interaction.MsgBox("El campo 'Nombre' es obligatorio.", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }

                var b = new BancoEntity
                {
                    Nombre = txt_nombre.Text.Trim(),
                    IdPais = Conversions.ToInteger(cmb_pais.SelectedValue),
                    NBanco = int.TryParse(txt_bancoN.Text, out var bancoNumero) ? bancoNumero : null,
                    Activo = chk_activo.Checked
                };

                // ================================
                // 🔹 Modo Edición
                // ================================
                if (edicion)
                {
                    b.IdBanco = edita_banco.IdBanco;
                    if (!bancos.UpdateBanco(b))
                    {
                        Interaction.MsgBox("Hubo un problema al actualizar el banco.", MsgBoxStyle.Exclamation, "Centrex");
                    }
                }
                else
                {
                    // ================================
                    // 🔹 Modo Alta
                    // ================================
                    bancos.AddBanco(b);
                }

                // ================================
                // 🔹 Reiniciar o cerrar
                // ================================
                if (chk_secuencia.Checked)
                {
                    txt_nombre.Text = "";
                    cmb_pais.SelectedIndex = -1;
                    txt_bancoN.Text = "";
                    chk_activo.Checked = true;
                }
                else
                {
                    closeandupdate(this);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al guardar el banco: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmb_pais_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Evita que el usuario escriba texto en el combo
            e.Handled = true;
        }
    }
}
