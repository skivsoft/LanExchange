using System;
using System.ComponentModel;

namespace WMIViewer.Model
{
    public sealed class GenericPropertyDescriptor<T> : PropertyDescriptor
    {
        private T value;

        public GenericPropertyDescriptor(string name, Attribute[] attributes)
            : base(name, attributes)
        {
        }

        public GenericPropertyDescriptor(string name, T value, Attribute[] attributes)
            : base(name, attributes)
        {
            this.value = value;
        }

        public GenericPropertyDescriptor(MemberDescriptor description)
            : base(description)
        {
        }

        public GenericPropertyDescriptor(MemberDescriptor description, Attribute[] attributes)
            : base(description, attributes)
        {
        }

        public override Type ComponentType
        {
            get
            {
                return typeof(GenericPropertyDescriptor<T>);
            }
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

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return value;
        }

        public override void ResetValue(object component)
        {
            SetValue(component, null);
        }

        public override void SetValue(object component, object value)
        {
            this.value = (T)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}