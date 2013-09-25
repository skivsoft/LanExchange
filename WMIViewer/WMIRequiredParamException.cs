using System;
using System.Globalization;

namespace WMIViewer
{
    [Serializable]
    public sealed class WMIRequiredParamException : Exception
    {
        private readonly string m_Marker;

        public WMIRequiredParamException(string marker)
        {
            m_Marker = marker;
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Command line parameter \"{0}\" not specified.", m_Marker);
            }
        }
    }
}
