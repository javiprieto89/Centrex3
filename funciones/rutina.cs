using System;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{
    static class rutinas
    {

        public static void errordb()
        {
            Interaction.MsgBox("Ocurrió un error en la base de datos, consulte con el programador", (MsgBoxStyle)((int)MsgBoxStyle.Critical + (int)MsgBoxStyle.OkOnly));
            Environment.Exit(0);
        }

        public static void erroradd()
        {
            Interaction.MsgBox("Ocurrió un error en al ingresar datos a la base de datos, consulte con el programador", (MsgBoxStyle)((int)MsgBoxStyle.Critical + (int)MsgBoxStyle.OkOnly));
            Environment.Exit(0);
        }
    }
}
