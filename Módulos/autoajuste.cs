using System.Windows.Forms;

namespace Centrex
{
    static class autoajuste
    {
        private static double[] ctrlwidth_viejo; // ancho viejo control
        private static double[] ctrlheight_viejo; // largo viejo control

        private static double[] ctrlwidth_actual; // ancho actual control
        private static double[] ctrlheight_actual; // largo actual control

        public static double[] ctrlwidth_nuevo; // ancho nuevo control
        public static double[] ctrlheight_nuevo; // largo nuevo control

        private static double frmwidth_viejo; // ancho viejo formulario
        private static double frmheight_viejo; // largo viejo formulario

        private static double frmwidth_actual; // ancho actual formulario
        private static double frmheight_actual; // largo actual formulario

        private static double frmwidth_nuevo; // ancho nuevo formulario
        private static double frmheight_nuevo; // largo nuevo formulario

        private static double[] ctrlx_viejo; // distancia vertical vieja control
        private static double[] ctrly_viejo; // distancia horizontal vieja control

        private static double[] ctrlx_actual; // distancia vertical actual control
        private static double[] ctrly_actual; // distancia horizontal actual control

        public static double[] ctrlx_nuevo; // distancia vertical nueva control
        public static double[] ctrly_nuevo; // distancia horizontal nueva control

        private static double frmx_viejo; // distancia vertical vieja formulario
        private static double frmy_viejo; // distancia horizontal vieja formulario

        private static double frmx_actual; // distancia vertical actual formulario
        private static double frmy_actual; // distancia horizontal actual formulario

        private static double frmx_nuevo; // distancia vertical nueva formulario
        private static double frmy_nuevo; // distancia horizontal nueva formulario

        private static int cctrl; // cantidad de controles en el formulario

        public static void ajustarcontrol(Form frm, bool toma)
        {
            if (toma)
            {
                tomarvalores(frm);
            }
            else
            {
                setearvalores(frm);
            }
        }

        private static void tomarvalores(Form frm)
        {

            // obtengo la cantidad de controles del formulario
            cctrl = frm.Controls.Count - 1;

            ctrlwidth_viejo = new double[cctrl + 1]; // ancho viejo control
            ctrlheight_viejo = new double[cctrl + 1]; // largo viejo control

            ctrlwidth_actual = new double[cctrl + 1]; // ancho actual control
            ctrlheight_actual = new double[cctrl + 1]; // largo actual control

            ctrlwidth_nuevo = new double[cctrl + 1]; // ancho nuevo control
            ctrlheight_nuevo = new double[cctrl + 1]; // largo nuevo control

            ctrlx_viejo = new double[cctrl + 1]; // distancia vertical vieja control
            ctrly_viejo = new double[cctrl + 1]; // distancia horizontal vieja control

            ctrlx_actual = new double[cctrl + 1]; // distancia vertical actual control
            ctrly_actual = new double[cctrl + 1]; // distancia horizontal actual control

            ctrlx_nuevo = new double[cctrl + 1]; // distancia vertical nueva control
            ctrly_nuevo = new double[cctrl + 1]; // distancia horizontal nueva control

            // tomo el tamaño actual del formulario
            frmwidth_actual = frm.Width;
            frmheight_actual = frm.Height;

            // tomo la posición actual del formulario
            frmx_actual = frm.Location.X;
            frmy_actual = frm.Location.Y;
            if (frmx_actual == 0d)
                frmx_actual = 1d;
            if (frmy_actual == 0d)
                frmy_actual = 1d;

            for (int i = 0, loopTo = cctrl; i <= loopTo; i++)
            {
                // tomo el tamaño actual del control
                ctrlwidth_actual[i] = frm.Controls[i].Width;
                ctrlheight_actual[i] = frm.Controls[i].Height;

                // tomo la posición actual del control
                ctrlx_actual[i] = frm.Controls[i].Location.X;
                ctrly_actual[i] = frm.Controls[i].Location.Y;
            }
        }

        private static void setearvalores(Form frm)
        {
            int i;
            double x, y;

            // tomo el nuevo tamaño del formulario
            frmwidth_nuevo = frm.Width;
            frmheight_nuevo = frm.Height;

            // tomo la nueva posición del formulario
            frmx_nuevo = frm.Location.X;
            frmy_nuevo = frm.Location.Y;
            if (frmx_nuevo == 0d)
                frmx_nuevo = 1d;
            if (frmy_nuevo == 0d)
                frmy_nuevo = 1d;

            // los valores actuales pasan a ser viejos, me van a quedar los viejos siendo los originales

            frmwidth_viejo = frmwidth_actual;
            frmheight_viejo = frmheight_actual;

            frmx_viejo = frmx_actual;
            frmy_viejo = frmy_actual;
            // If frmwidth_viejo = frmwidth_nuevo And frmheight_viejo = frmheight_nuevo Then Exit Sub

            var loopTo = cctrl;
            for (i = 0; i <= loopTo; i++)
            {
                ctrlwidth_viejo[i] = ctrlwidth_actual[i];
                ctrlheight_viejo[i] = ctrlheight_actual[i];

                ctrlx_viejo[i] = ctrlx_actual[i];
                ctrly_viejo[i] = ctrly_actual[i];
            }

            // calculo el nuevo tamaño del control
            var loopTo1 = cctrl;
            for (i = 0; i <= loopTo1; i++)
            {
                // ancho
                ctrlwidth_nuevo[i] = frmwidth_nuevo * ctrlwidth_viejo[i] / frmwidth_viejo;
                // alto
                ctrlheight_nuevo[i] = frmheight_nuevo * ctrlheight_viejo[i] / frmheight_viejo;

                // calculo la nueva posición del control

                x = ctrlx_viejo[i] * 100d / frmwidth_viejo / 100d;
                y = ctrly_viejo[i] * 100d / frmheight_viejo / 100d;
                ctrlx_nuevo[i] = frmwidth_nuevo * x;
                ctrly_nuevo[i] = frmheight_nuevo * y;

                // ctrlx_nuevo = (frmx_nuevo * ctrlx_viejo) / frmx_viejo
                // ctrly_nuevo = (frmy_nuevo * ctrly_viejo) / frmy_viejo

                // asigno los valores
                // tamaño
                // frm.Controls.Item(i).Width = ctrlwidth_nuevo(i)
                // frm.Controls.Item(i).Height = ctrlheight_nuevo(i)

                // posición
                // frm.Controls.Item(i).Left = ctrlx_nuevo(i)
                // frm.Controls.Item(i).Top = ctrly_nuevo(i)
            }
        }
    }
}
