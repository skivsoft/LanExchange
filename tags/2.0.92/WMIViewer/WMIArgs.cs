using System.Collections.Generic;
using System.Windows.Forms;

namespace WMIViewer
{
    public sealed class WMIArgs
    {
        public static WMIArgs ParseFromCmdLine(IEnumerable<string> args)
        {
            const string COMPUTER_MARKER = "/COMPUTER:";

            var compName = SystemInformation.ComputerName;
            foreach (var word in args)
                if (word.ToUpper().StartsWith(COMPUTER_MARKER))
                {
                    var comp = word.Remove(0, COMPUTER_MARKER.Length);
                    if (!string.IsNullOrEmpty(comp))
                        compName = comp;
                    break;
                }
            return new WMIArgs(compName);
        }

        public WMIArgs(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Comment { get; set; }
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        
        public bool EditPropertyMode
        {
            get { return !string.IsNullOrEmpty(ClassName) && !string.IsNullOrEmpty(PropertyName); }
        }


    }
}
