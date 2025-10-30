using System;
using System.Windows.Forms;
using Centrex; // 👈 IMPORTANTE: agrega esto para acceder a tu formulario main

namespace Centrex
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 🔹 Si estás en .NET 6 o superior, asegurate de tener esta línea:
            ApplicationConfiguration.Initialize();

            // 🔹 Arranca tu formulario principal:
            Application.Run(new main());
        }
    }
}
