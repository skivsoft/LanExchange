using System;
using System.Globalization;

namespace WMIViewer
{
    [Serializable]
    public sealed class WMIObjectNotFoundException : Exception
    {
        private readonly string m_WMIObject;

        public WMIObjectNotFoundException(string wmiObject)
        {
            m_WMIObject = wmiObject;
        }

        public override string Message
        {
            get { return string.Format(CultureInfo.InvariantCulture, "WMI property \"{0}\" not found.", m_WMIObject); }
        }
    }
}
