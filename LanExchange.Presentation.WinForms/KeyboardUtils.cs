using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms
{
    [Localizable(false)]
    public static class KeyboardUtils
    {
        public static string KeyEventToString(KeyEventArgs e)
        {
            var result = new StringBuilder();
            if (e.Control)
                result.Append("Ctrl");
            if (e.Alt)
            {
                if (result.Length > 0)
                    result.Append("+");
                result.Append("Alt");
            }
            if (e.Shift)
            {
                if (result.Length > 0)
                    result.Append("+");
                result.Append("Shift");
            }
            if (result.Length > 0)
                result.Append("+");
            result.Append(e.KeyCode.ToString().Replace("Return", "Enter"));
            return result.ToString();
        }
    }
}
