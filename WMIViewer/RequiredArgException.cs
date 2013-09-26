using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using WMIViewer.Properties;

namespace WMIViewer
{
    [Serializable]
    public sealed class RequiredArgException : Exception
    {
        private readonly string m_Marker;

        //public WMIRequiredParamException

        public RequiredArgException()
        {
        }

        public RequiredArgException(string marker)
        {
            m_Marker = marker;
        }

        public RequiredArgException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private RequiredArgException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_Marker = info.GetString ("Marker");
        }        

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Marker", m_Marker);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, Resources.WmiRequiredParamException_Message, m_Marker);
            }
        }
    }
}
