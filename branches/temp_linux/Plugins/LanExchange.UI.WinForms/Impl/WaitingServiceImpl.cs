using System.Windows.Forms;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WinForms.Impl
{
    public class WaitingServiceImpl : IWaitingService
    {
        public void BeginWait()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void EndWait()
        {
            Cursor.Current = Cursors.Default;
        }
    }
}
