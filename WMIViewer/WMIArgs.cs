using System.Collections.Generic;
using System.Windows.Forms;

namespace WMIViewer
{
    public sealed class WMIArgs
    {
        public static string DefaultNamespaceName = @"ROOT\CIMV2";

        public static WMIArgs ParseFromCmdLine(IEnumerable<string> args)
        {
            const string COMPUTER_MARKER    = "/COMPUTER:";
            const string NAMESPACE_MARKER   = "/NAMESPACE:";
            const string CLASS_MARKER       = "/CLASS:";
            const string PROPERTY_MARKER    = "/PROPERTY:";
            const string METHOD_MARKER      = "/METHOD:";
            const string EDIT_CMD_MARKER    = "/EDIT";
            const string EXECUTE_CMD_MARKER = "/EXECUTE";

            var result = new WMIArgs();
            result.ComputerName = SystemInformation.ComputerName;
            foreach (var word in args)
            {
                var wordUpper = word.ToUpper();
                // computer
                if (wordUpper.StartsWith(COMPUTER_MARKER))
                {
                    result.ComputerName = word.Remove(0, COMPUTER_MARKER.Length);
                    continue;
                }
                // namespace
                if (wordUpper.StartsWith(NAMESPACE_MARKER))
                {
                    result.NamespaceName = word.Remove(0, NAMESPACE_MARKER.Length);
                    continue;
                }
                // class
                if (wordUpper.StartsWith(CLASS_MARKER))
                {
                    result.ClassName = word.Remove(0, CLASS_MARKER.Length);
                    continue;
                }
                // property
                if (wordUpper.StartsWith(PROPERTY_MARKER))
                {
                    result.PropertyName = word.Remove(0, PROPERTY_MARKER.Length);
                    continue;
                }
                // method
                if (wordUpper.StartsWith(METHOD_MARKER))
                {
                    result.MethodName = word.Remove(0, METHOD_MARKER.Length);
                    continue;
                }
                // edit property command
                if (wordUpper.Equals(EDIT_CMD_MARKER))
                {
                    result.StartCmd = WMIStartCommand.EditProperty;
                    continue;
                }
                // execute method command
                if (wordUpper.Equals(EXECUTE_CMD_MARKER))
                    result.StartCmd = WMIStartCommand.ExecuteMethod;
            }
            if (string.IsNullOrEmpty(result.NamespaceName))
                result.NamespaceName = DefaultNamespaceName;
            return result;
        }

        public string ComputerName { get; set; }
        public string NamespaceName { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public string MethodName { get; set; }
        public WMIStartCommand StartCmd { get; set; }
    }
}
