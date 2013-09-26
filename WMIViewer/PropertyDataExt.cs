using System;
using System.ComponentModel;
using System.Management;

namespace WMIViewer
{
    [Localizable(false)]
    public sealed class PropertyDataExt : IComparable<PropertyDataExt>
    {
        private readonly PropertyData m_Data;
        private readonly WMIParamType m_ParamType;
        private readonly int m_ID;

        public PropertyDataExt(PropertyData data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            m_Data = data;
            m_ID = 100;
            foreach (var qd in m_Data.Qualifiers)
            {
                if (qd.Name.Equals("In") || qd.Name.Equals("in"))
                    m_ParamType = WMIParamType.In;
                if (qd.Name.Equals("Out"))
                    m_ParamType = WMIParamType.Out;
                if (qd.Name.Equals("out"))
                    m_ParamType = WMIParamType.Return;
                if (qd.Name.Equals("ID"))
                    m_ID = (int) qd.Value;
            }
        }

        public WMIParamType ParamType
        {
            get { return m_ParamType; }
        }

        public int ID
        {
            get { return m_ID; }
        }

        public string Name
        {
            get { return m_Data.Name; }
        }

        public CimType PropType
        {
            get { return m_Data.Type; }
        }

        public QualifierDataCollection Qualifiers
        {
            get { return m_Data.Qualifiers; }
        }

        public object Value
        {
            get { return m_Data.Value; }   
        }

        public int CompareTo(PropertyDataExt other)
        {
            return ID - other.ID;
        }
    }
}
