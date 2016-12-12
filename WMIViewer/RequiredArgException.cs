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
        private readonly string marker;

        public RequiredArgException()
        {
        }

        public RequiredArgException(string marker)
        {
            this.marker = marker;
        }

        public RequiredArgException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private RequiredArgException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            marker = info.GetString("Marker");
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, Resources.WmiRequiredParamException_Message, marker);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("Marker", marker);
            base.GetObjectData(info, context);
        }
    }
}
