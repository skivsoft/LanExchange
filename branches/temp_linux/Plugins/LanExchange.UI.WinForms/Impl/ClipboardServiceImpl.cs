using System.Windows.Forms;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WinForms.Impl
{
    internal class ClipboardServiceImpl : IClipboardService
    {
        public IClipboardDataObject GetDataObject()
        {
            return new ClipboardDataObjectImpl(Clipboard.GetDataObject());
        }
    }
}
