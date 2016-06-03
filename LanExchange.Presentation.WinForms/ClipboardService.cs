using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms
{
    internal sealed class ClipboardService : IClipboardService
    {
        public IClipboardDataObject GetDataObject()
        {
            return new ClipboardDataObject(Clipboard.GetDataObject());
        }
    }
}
