using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms
{
    internal sealed class WaitingService : IWaitingService
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