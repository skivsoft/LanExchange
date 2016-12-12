using System;
using System.ComponentModel;
using System.Management;

namespace WMIViewer.Model
{
    [Localizable(false)]
    internal sealed class PropertyDataExt : IComparable<PropertyDataExt>
    {
        private readonly PropertyData data;
        private readonly ParameterType parameterType;
        private readonly int id;

        public PropertyDataExt(PropertyData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            this.data = data;
            id = 100;
            foreach (var qd in this.data.Qualifiers)
            {
                if (qd.Name.Equals("In") || qd.Name.Equals("in"))
                    parameterType = ParameterType.In;
                if (qd.Name.Equals("Out"))
                    parameterType = ParameterType.Out;
                if (qd.Name.Equals("out"))
                    parameterType = ParameterType.Return;
                if (qd.Name.Equals("ID"))
                    id = (int)qd.Value;
            }
        }

        public ParameterType ParameterType
        {
            get { return parameterType; }
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return data.Name; }
        }

        public CimType PropType
        {
            get { return data.Type; }
        }

        public QualifierDataCollection Qualifiers
        {
            get { return data.Qualifiers; }
        }

        public object Value
        {
            get { return data.Value; }   
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
            return id;
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