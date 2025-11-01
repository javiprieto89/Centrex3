using System;
using System.Windows.Forms;

namespace Centrex
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 🔹 Si estás en .NET6 o superior, asegurate de tener esta línea:
            ApplicationConfiguration.Initialize();

            // 🔹 Arranca tu formulario principal:
            Application.Run(new main());
        }
    }
}
