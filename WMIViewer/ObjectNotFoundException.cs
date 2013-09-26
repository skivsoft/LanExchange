using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using WMIViewer.Properties;

namespace WMIViewer
{
    [Serializable]
    public sealed class ObjectNotFoundException : Exception
    {
        private readonly string m_WMIObject;

        public ObjectNotFoundException()
        {
            
        }

        public ObjectNotFoundException(string wmiObject)
        {
            m_WMIObject = wmiObject;
        }

        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private ObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_WMIObject = info.GetString("WMIObject");
        }        

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("WMIObject", m_WMIObject);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get { return string.Format(CultureInfo.InvariantCulture, Resources.WMIObjectNotFoundException_Message, m_WMIObject); }
        }
    }
}
