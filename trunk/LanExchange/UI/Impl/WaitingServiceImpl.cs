using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI.Impl
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
