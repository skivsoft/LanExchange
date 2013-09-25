using System;

namespace WMIViewer
{
    class WMIObjectNotFoundException : Exception
    {
        private string m_WMIObject;

        public WMIObjectNotFoundException(string wmiObject)
        {
            m_WMIObject = wmiObject;
        }

        public override string Message
        {
            get { return string.Format("WMI property \"{0}\" not found.", m_WMIObject ); }
        }
    }
}
