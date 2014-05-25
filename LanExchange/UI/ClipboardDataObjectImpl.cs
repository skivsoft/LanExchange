using System;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI.WinForms
{
    internal class ClipboardDataObjectImpl : IClipboardDataObject
    {
        public ClipboardDataObjectImpl(IDataObject dataObject)
        {
            if (dataObject == null)
                throw new ArgumentNullException("dataObject");
            DataObject = dataObject;
        }

        public IDataObject DataObject { get; private set; }

        public object GetData(string format, bool autoConvert)
        {
            return DataObject.GetData(format, autoConvert);
        }

        public object GetData(string format)
        {
            return DataObject.GetData(format);
        }

        public object GetData(Type format)
        {
            return DataObject.GetData(format);
        }

        public void SetData(string format, bool autoConvert, object data)
        {
            DataObject.SetData(format, autoConvert, data);
        }

        public void SetData(string format, object data)
        {
            DataObject.SetData(format, data);
        }

        public void SetData(Type format, object data)
        {
            DataObject.SetData(format, data);
        }

        public void SetData(object data)
        {
            DataObject.SetData(data);
        }

        public bool GetDataPresent(string format, bool autoConvert)
        {
            return DataObject.GetDataPresent(format, autoConvert);
        }

        public bool GetDataPresent(string format)
        {
            return DataObject.GetDataPresent(format);
        }

        public bool GetDataPresent(Type format)
        {
            return DataObject.GetDataPresent(format);
        }

        public string[] GetFormats(bool autoConvert)
        {
            return DataObject.GetFormats(autoConvert);
        }

        public string[] GetFormats()
        {
            return DataObject.GetFormats();
        }
    }
}