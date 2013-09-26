using System;
using System.ComponentModel;
using System.Management;

namespace WMIViewer
{
    [Localizable(false)]
    public sealed class PropertyDataExt : IComparable<PropertyDataExt>
    {
        private readonly PropertyData m_Data;
        private readonly WmiParameterType m_ParameterType;
        private readonly int m_Id;

        public PropertyDataExt(PropertyData data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            m_Data = data;
            m_Id = 100;
            foreach (var qd in m_Data.Qualifiers)
            {
                if (qd.Name.Equals("In") || qd.Name.Equals("in"))
                    m_ParameterType = WmiParameterType.In;
                if (qd.Name.Equals("Out"))
                    m_ParameterType = WmiParameterType.Out;
                if (qd.Name.Equals("out"))
                    m_ParameterType = WmiParameterType.Return;
                if (qd.Name.Equals("ID"))
                    m_Id = (int) qd.Value;
            }
        }

        public WmiParameterType ParameterType
        {
            get { return m_ParameterType; }
        }

        public int Id
        {
            get { return m_Id; }
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
            if (other == null)
                return 1;
            return Id - other.Id;
        }
    }
}
