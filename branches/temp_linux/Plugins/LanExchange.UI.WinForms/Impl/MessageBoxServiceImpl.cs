using System.Windows.Forms;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WinForms.Impl
{
    internal class MessageBoxServiceImpl : IMessageBoxService
    {
        public int AskQuestionFmt(string title, string format, params object[] args)
        {
            return (int)MessageBox.Show(string.Format(format, args), title,
                MessageBoxButtons.YesNoCancel, 
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2);
        }


        public bool IsYes(int result)
        {
            return result == (int) DialogResult.Yes;
        }

        public bool IsNo(int result)
        {
            return result == (int) DialogResult.No;
        }

        public bool IsCancel(int result)
        {
            return result == (int) DialogResult.Cancel;
        }
    }
}
