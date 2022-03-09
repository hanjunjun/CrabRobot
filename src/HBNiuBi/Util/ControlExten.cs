using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBNiuBi.Util
{
    public static class ControlExten
    {
        public static void SetTextBox(this TextBox textbox, Action action)
        {
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(action);
            }
            else
            {
                action();
            }
        }

    }
}
