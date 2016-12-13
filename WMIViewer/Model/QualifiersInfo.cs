using System.ComponentModel;
using System.Management;

namespace WMIViewer.Model
{
    internal class QualifiersInfo
    {
        [Localizable(false)]
        public QualifiersInfo(QualifierDataCollection qualifiers)
        {
            IsReadOnly = true;
            Description = string.Empty;
            foreach (var qd in qualifiers)
            {
                // class qualifiers
                if (qd.Name.Equals("Association")) IsAssociation = true;
                if (qd.Name.Equals("dynamic")) IsDynamic = true;
                if (qd.Name.ToUpperInvariant().Equals("GENERICPERFCTR")) IsPerf = true;
                if (qd.Name.Equals("SupportsUpdate")) IsSupportsUpdate = true;
                if (qd.Name.Equals("SupportsCreate")) IsSupportsCreate = true;
                if (qd.Name.Equals("SupportsDelete")) IsSupportsDelete = true;

                // property qualifiers
                if (qd.Name.Equals("CIM_Key")) IsCimKey = true;
                if (qd.Name.Equals("write")) IsReadOnly = false;
                if (qd.Name.Equals("Description")) Description = qd.Value.ToString();
            }
        }

        public bool IsAssociation { get; private set; }

        public bool IsDynamic { get; private set; }

        public bool IsPerf { get; private set; }

        public bool IsSupportsUpdate { get; }

        public bool IsSupportsCreate { get; }

        public bool IsSupportsDelete { get; }

        public bool IsSupportsModify
        {
            get { return IsSupportsUpdate || IsSupportsCreate || IsSupportsDelete; }
        }

        public bool IsCimKey { get; private set; }

        public bool IsReadOnly { get; private set; }

        public string Description { get; private set; }
    }
}
