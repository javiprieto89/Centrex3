using System;

namespace Centrex
{
    public partial class dbselect
    {
        public dbselect()
        {
            InitializeComponent();
        }

        private void dbselect_Load(object sender, EventArgs e)
        {
            cmb_selectdb.SelectedIndex = 0;
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            switch (cmb_selectdb.SelectedIndex)
            {
                case 0:
                    {
                        VariablesGlobales.basedb = "Centrex";
                        break;
                    }
                case 1:
                    {
                        VariablesGlobales.basedb = "CentrexTest";
                        break;
                    }
            }
            My.MyProject.Forms.main.ShowDialog();
            Dispose();
        }
    }
}
