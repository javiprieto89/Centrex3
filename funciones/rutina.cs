using System;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    static class rutinas
    {
        public static void errordb()
        {
            MessageBox.Show(
               "Ocurrió un error en la base de datos, consulte con el programador",
                "Error de Base de Datos",
               MessageBoxButtons.OK,
           MessageBoxIcon.Error);
            Environment.Exit(0);
        }

        public static void erroradd()
        {
            MessageBox.Show(
                 "Ocurrió un error al ingresar datos a la base de datos, consulte con el programador",
                "Error al Agregar Datos",
                      MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
}
