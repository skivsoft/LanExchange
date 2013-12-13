using System.Windows.Forms;
using LanExchange.SDK;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace LanExchange.Plugin.Win7
{
    internal class Win7WaitingServiceImpl : IWaitingService
    {
        public void BeginWait()
        {
            Cursor.Current = Cursors.WaitCursor;
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
        }

        public void EndWait()
        {
            Cursor.Current = Cursors.Default;
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
        }
    }
}