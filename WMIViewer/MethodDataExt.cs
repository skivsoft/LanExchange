using System;
using System.ComponentModel;
using System.Globalization;
using System.Management;
using System.Collections.Generic;
using System.Text;
using WMIViewer.Model;

namespace WMIViewer
{
    public sealed class MethodDataExt
    {
        private readonly MethodData data;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="data"></param>
        public MethodDataExt(MethodData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            this.data = data;
        }

        public bool HasQualifier(string name)
        {
            foreach (var qd in data.Qualifiers)
                if (qd.Name.Equals(name))
                    return true;
            return false;
        }

        [Localizable(false)]
        public override string ToString()
        {
            var list = new List<PropertyDataExt>();
            if (data.InParameters != null)
                foreach (var pd in data.InParameters.Properties)
                    list.Add(new PropertyDataExt(pd));
            if (data.OutParameters != null)
                foreach (var pd in data.OutParameters.Properties)
                    list.Add(new PropertyDataExt(pd));
            list.Sort();
            //string sReturn = string.Empty;
            var sb = new StringBuilder();
            int numArgs = 0;
            foreach (var prop in list)
            {
                if (prop.ParameterType == ParameterType.Return)
                {
                    continue;
                }
                numArgs++;
                if (sb.Length > 0)
                    sb.Append(", ");
                if (prop.ParameterType == ParameterType.Out)
                    sb.Append("out ");
                sb.Append(prop.Name);
            }
            return numArgs == 0 ? data.Name : string.Format(CultureInfo.InvariantCulture, "{0} ({1})", data.Name, sb);
        }
    }
}
