using System;

namespace Centrex
{
    public sealed partial class frmacercade
    {
        public frmacercade()
        {
            InitializeComponent();
        }

        private void frmacercade_Load(object sender, EventArgs e)
        {
            // Establezca el título del formulario.
            string ApplicationTitle;
            if (!string.IsNullOrEmpty(My.MyProject.Application.Info.Title))
            {
                ApplicationTitle = My.MyProject.Application.Info.Title;
            }
            else
            {
                ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.MyProject.Application.Info.AssemblyName);
            }
            Text = string.Format("Acerca de {0}", ApplicationTitle);
            // Inicialice todo el texto mostrado en el cuadro Acerca de.
            // TODO: personalice la información del ensamblado de la aplicación en el panel "Aplicación" del 
            // cuadro de diálogo propiedades del proyecto (bajo el menú "Proyecto").
            LabelProductName.Text = My.MyProject.Application.Info.ProductName;
            LabelVersion.Text = string.Format("Versión {0}", My.MyProject.Application.Info.Version.ToString());
            LabelCopyright.Text = My.MyProject.Application.Info.Copyright;
            LabelCompanyName.Text = My.MyProject.Application.Info.CompanyName;
            TextBoxDescription.Text = My.MyProject.Application.Info.Description;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
