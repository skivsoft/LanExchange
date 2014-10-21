using System;
using System.ComponentModel;
using System.Management;

namespace WMIViewer.Model
{
    [Localizable(false)]
    internal sealed class PropertyDataExt : IComparable<PropertyDataExt>
    {
        private readonly PropertyData m_Data;
        private readonly ParameterType m_ParameterType;
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
                    m_ParameterType = ParameterType.In;
                if (qd.Name.Equals("Out"))
                    m_ParameterType = ParameterType.Out;
                if (qd.Name.Equals("out"))
                    m_ParameterType = ParameterType.Return;
                if (qd.Name.Equals("ID"))
                    m_Id = (int) qd.Value;
            }
        }

        public ParameterType ParameterType
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

        public override bool Equals(object obj)
        {
            return CompareTo(obj as PropertyDataExt) == 0;
        }

        public override int GetHashCode()
        {
            return m_Id;
        }

        public static bool operator ==(PropertyDataExt left, PropertyDataExt right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(PropertyDataExt left, PropertyDataExt right)
        {
            return !(left == right);
        }

        public static bool operator <(PropertyDataExt left, PropertyDataExt right)
        {
            if (left == null)
                return true;
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(PropertyDataExt left, PropertyDataExt right)
        {
            if (left == null)
                return false;
            return left.CompareTo(right) > 0;
        }
    }
}