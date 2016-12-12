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
        private readonly string wmiObject;

        public ObjectNotFoundException()
        {
            
        }

        public ObjectNotFoundException(string wmiObject)
        {
            this.wmiObject = wmiObject;
        }

        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private ObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            wmiObject = info.GetString("WMIObject");
        }

        public override string Message
        {
            get { return string.Format(CultureInfo.InvariantCulture, Resources.ObjectNotFoundException_Message, wmiObject); }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("WMIObject", wmiObject);
            base.GetObjectData(info, context);
        }
    }
}
