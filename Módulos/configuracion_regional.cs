using System;
using System.Globalization;
using System.Windows.Forms;

namespace Centrex
{

    internal class configuracion_regional
    {
        [STAThread()]
        public static void cambiarIdioma()
        {

            CultureInfo forceDotCulture;
            forceDotCulture = (CultureInfo)Application.CurrentCulture.Clone();
            forceDotCulture.NumberFormat.NumberDecimalSeparator = ".";
            forceDotCulture.NumberFormat.NumberGroupSeparator = ",";

            Application.CurrentCulture = forceDotCulture;
        }
    }
}
