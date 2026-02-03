using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zastita_informacija_projekat
{
    internal static class UIHelper
    {
        public static void CenterInParent(Control control, bool top = false)
        {
            if (control.Parent == null) return;

            control.Left = (control.Parent.ClientSize.Width - control.Width) / 2;
            if (top) 
                control.Top = (control.Parent.ClientSize.Height - control.Height) / 2;
        }
    }
}
