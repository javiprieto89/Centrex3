using System.Collections;
using System.Windows.Forms;

namespace Centrex
{
    public class clsgenerales
    {
        public class ListViewItemComparer : IComparer
        {

            private int col;

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
    }
}
