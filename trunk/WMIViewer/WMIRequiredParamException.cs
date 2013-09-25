using System;

namespace WMIViewer
{
    class WMIRequiredParamException : Exception
    {
        private string m_Marker;

        public WMIRequiredParamException(string marker)
        {
            m_Marker = marker;
        }

        public override string Message
        {
            get
            {
                return string.Format("Command line parameter \"{0}\" not specified.", m_Marker);
            }
        }
    }
}
