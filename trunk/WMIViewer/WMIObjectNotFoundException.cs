using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using WMIViewer.Properties;

namespace WMIViewer
{
    [Serializable]
    public sealed class WMIObjectNotFoundException : Exception
    {
        private readonly string m_WMIObject;

        public WMIObjectNotFoundException()
        {
            
        }

        public WMIObjectNotFoundException(string wmiObject)
        {
            m_WMIObject = wmiObject;
        }

        public WMIObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private WMIObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_WMIObject = info.GetString("WMIObject");
        }        

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("WMIObject", m_WMIObject);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get { return string.Format(CultureInfo.InvariantCulture, Resources.WMIObjectNotFoundException_Message, m_WMIObject); }
        }
    }
}
