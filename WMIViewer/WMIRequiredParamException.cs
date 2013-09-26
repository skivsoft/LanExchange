using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using WMIViewer.Properties;

namespace WMIViewer
{
    [Serializable]
    public sealed class WMIRequiredParamException : Exception
    {
        private readonly string m_Marker;

        //public WMIRequiredParamException

        public WMIRequiredParamException()
        {
        }

        public WMIRequiredParamException(string marker)
        {
            m_Marker = marker;
        }

        public WMIRequiredParamException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private WMIRequiredParamException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_Marker = info.GetString ("Marker");
        }        

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Marker", m_Marker);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, Resources.WMIRequiredParamException_Message, m_Marker);
            }
        }
    }
}
