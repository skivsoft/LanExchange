using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace WMIViewer.Model
{
    internal sealed class CmdLineArgs
    {
        public const string DefaultNamespaceName = @"ROOT\CIMV2";

        [Localizable(false)]
        public static CmdLineArgs ParseFromCmdLine(IEnumerable<string> args)
        {
            const string COMPUTER_MARKER    = "/COMPUTER:";
            const string NAMESPACE_MARKER   = "/NAMESPACE:";
            const string CLASS_MARKER       = "/CLASS:";
            const string PROPERTY_MARKER    = "/PROPERTY:";
            const string METHOD_MARKER      = "/METHOD:";
            const string EDIT_CMD_MARKER    = "/EDIT";
            const string EXECUTE_CMD_MARKER = "/EXECUTE";

            if (args == null)
                throw new ArgumentNullException(nameof(args));

            var result = new CmdLineArgs();
            result.ComputerName = SystemInformation.ComputerName;
            foreach (var word in args)
            {
                var wordUpper = word.ToUpper(CultureInfo.InvariantCulture);
                // computer
                if (wordUpper.StartsWith(COMPUTER_MARKER, StringComparison.OrdinalIgnoreCase))
                {
                    result.ComputerName = word.Remove(0, COMPUTER_MARKER.Length);
                    continue;
                }
                // namespace
                if (wordUpper.StartsWith(NAMESPACE_MARKER, StringComparison.OrdinalIgnoreCase))
                {
                    result.NamespaceName = word.Remove(0, NAMESPACE_MARKER.Length);
                    continue;
                }
                // class
                if (wordUpper.StartsWith(CLASS_MARKER, StringComparison.OrdinalIgnoreCase))
                {
                    result.ClassName = word.Remove(0, CLASS_MARKER.Length);
                    continue;
                }
                // property
                if (wordUpper.StartsWith(PROPERTY_MARKER, StringComparison.OrdinalIgnoreCase))
                {
                    result.PropertyName = word.Remove(0, PROPERTY_MARKER.Length);
                    continue;
                }
                // method
                if (wordUpper.StartsWith(METHOD_MARKER, StringComparison.OrdinalIgnoreCase))
                {
                    result.MethodName = word.Remove(0, METHOD_MARKER.Length);
                    continue;
                }
                // edit property command
                if (wordUpper.Equals(EDIT_CMD_MARKER, StringComparison.OrdinalIgnoreCase))
                {
                    result.StartCmd = CmdLineCommand.EditProperty;
                    continue;
                }
                // execute method command
                if (wordUpper.Equals(EXECUTE_CMD_MARKER, StringComparison.OrdinalIgnoreCase))
                    result.StartCmd = CmdLineCommand.ExecuteMethod;
            }
            if (string.IsNullOrEmpty(result.NamespaceName))
                result.NamespaceName = DefaultNamespaceName;
            switch(result.StartCmd)
            {
                case CmdLineCommand.EditProperty:
                    if (string.IsNullOrEmpty(result.ClassName))
                        throw new RequiredArgException(CLASS_MARKER);
                    if (string.IsNullOrEmpty(result.PropertyName))
                        throw new RequiredArgException(PROPERTY_MARKER);
                    var description = WmiClassList.Instance.GetPropertyDescription(result.ClassName, result.PropertyName);
                    if (string.IsNullOrEmpty(description))
                        throw new ObjectNotFoundException(string.Format(CultureInfo.InvariantCulture, @"{0}\{1}.{2}", result.NamespaceName, result.ClassName, result.PropertyName));
                    break;
                case CmdLineCommand.ExecuteMethod:
                    if (string.IsNullOrEmpty(result.ClassName))
                        throw new RequiredArgException(CLASS_MARKER);
                    if (string.IsNullOrEmpty(result.MethodName))
                        throw new RequiredArgException(METHOD_MARKER);
                    if (!WmiClassList.Instance.IsMethodExists(result.ClassName, result.MethodName))
                        throw new ObjectNotFoundException(string.Format(CultureInfo.InvariantCulture, @"{0}\{1}.{2}()", result.NamespaceName, result.ClassName, result.MethodName));
                    break;
            }
            return result;
        }

        public string ComputerName { get; set; }
        public string NamespaceName { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public string MethodName { get; set; }
        public CmdLineCommand StartCmd { get; set; }
    }
}
