using System;
using System.ComponentModel;
using Microsoft.Win32;

namespace LanExchange.Helpers
{
    public static class ProtocolHelper
    {
        [Localizable(false)]
        public static bool IsProtocol(string proto)
        {
            return (proto != null) && proto.EndsWith(":", StringComparison.Ordinal);
        }
    
        public static bool LookupInRegistry(string proto, out string fileName, out int iconIndex)
        {
            fileName = string.Empty;
            iconIndex = -1;
            if (!IsProtocol(proto)) return false;
            proto = proto.Remove(proto.Length - 1);
            var regKey = Registry.ClassesRoot.OpenSubKey(proto);
            if (regKey == null) return false;
            var obj = regKey.GetValue("URL Protocol");
            if (obj == null) return false;
            var regKey2 = regKey.OpenSubKey("DefaultIcon");
            if (regKey2 == null) return false;
            obj = regKey2.GetValue(null);
            if (obj == null) return false;
            var arr = obj.ToString().Split(',');
            fileName = arr[0];
            if (arr.Length > 1)
                Int32.TryParse(arr[1], out iconIndex);
            return true;
        }
    }
}