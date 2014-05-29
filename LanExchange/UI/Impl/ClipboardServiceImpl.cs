using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI.Impl
{
    internal class ClipboardServiceImpl : IClipboardService
    {
        public IClipboardDataObject GetDataObject()
        {
            return new ClipboardDataObjectImpl(Clipboard.GetDataObject());
        }
    }
}
