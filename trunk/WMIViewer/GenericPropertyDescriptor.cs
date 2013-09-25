using System;
using System.ComponentModel;

namespace WMIViewer
{
    public sealed class GenericPropertyDescriptor<T> : PropertyDescriptor
    {
        private T m_Value;

        public GenericPropertyDescriptor(string name, Attribute[] attrs)
            : base(name, attrs)
        {
        }

        public GenericPropertyDescriptor(string name, T value, Attribute[] attrs)
            : base(name, attrs)
        {
            m_Value = value;
        }

        public GenericPropertyDescriptor(MemberDescriptor descr)
            : base(descr)
        {
            
        }

        public GenericPropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
            : base(descr, attrs)
        {
            
        }
         

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get
            {
                return typeof(GenericPropertyDescriptor<T>);
            }
        }

        public override object GetValue(object component)
        {
            return m_Value;
        }

        public override bool IsReadOnly
        {
            get
            {
                return Array.Exists(AttributeArray, attr => attr is ReadOnlyAttribute);
            }
        }

        public override Type PropertyType
        {
            get
            {
                return typeof(T);
            }
        }

        public override void ResetValue(object component)
        {
            SetValue(component, null);
        }

        public override void SetValue(object component, object value)
        {
            m_Value = (T)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}