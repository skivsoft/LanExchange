using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace LanExchange.Utils
{
	public class DynamicObject : ICustomTypeDescriptor
	{
        private string _Filter = String.Empty;
        private readonly PropertyDescriptorCollection _FilteredPropertyDescriptors = new PropertyDescriptorCollection(null);
        private readonly PropertyDescriptorCollection _FullPropertyDescriptors = new PropertyDescriptorCollection(null);

        public void AddProperty<T>(
          string name,
          T value,
          string displayName,
          string description,
          string category,
          bool readOnly,
          IEnumerable<Attribute> attributes)
        {
            var attrs = attributes == null ? new List<Attribute>()
                                           : new List<Attribute>(attributes);

            if (!String.IsNullOrEmpty(displayName))
                attrs.Add(new DisplayNameAttribute(displayName));

            if (!String.IsNullOrEmpty(description))
                attrs.Add(new DescriptionAttribute(description));

            if (!String.IsNullOrEmpty(category))
                attrs.Add(new CategoryAttribute(category));

            if (readOnly)
                attrs.Add(new ReadOnlyAttribute(true));

            _FullPropertyDescriptors.Add(new GenericPropertyDescriptor<T>(
              name, value, attrs.ToArray()));
        }

        public void AddPropertyNull<T>(
          string name,
          string displayName,
          string description,
          string category,
          bool readOnly,
          IEnumerable<Attribute> attributes)
        {
            var attrs = attributes == null ? new List<Attribute>()
                                           : new List<Attribute>(attributes);

            if (!String.IsNullOrEmpty(displayName))
                attrs.Add(new DisplayNameAttribute(displayName));

            if (!String.IsNullOrEmpty(description))
                attrs.Add(new DescriptionAttribute(description));

            if (!String.IsNullOrEmpty(category))
                attrs.Add(new CategoryAttribute(category));

            if (readOnly)
                attrs.Add(new ReadOnlyAttribute(true));

            _FullPropertyDescriptors.Add(new GenericPropertyDescriptor<T>(
              name, attrs.ToArray()));
        }

        public void AddProperty<T>(
          string name,
          T value,
          string description,
          string category,
          bool readOnly)
        {
            AddProperty<T>(name, value, name, description, category, readOnly, null);
        }

        public void AddPropertyNull<T>(
          string name,
          string description,
          string category,
          bool readOnly)
        {
            AddPropertyNull<T>(name, name, description, category, readOnly, null);
        }


        public void RemoveProperty(string propertyName)
        {
            var descriptor = _FullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                _FullPropertyDescriptors.Remove(descriptor);
            else
                throw new Exception("Property is not found");
        }


        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value.Trim().ToLower();
                if (_Filter.Length != 0)
                    FilterProperties(_Filter);
            }
        }

        private void FilterProperties(string filter)
        {
            _FilteredPropertyDescriptors.Clear();

            foreach (var descriptor in _FullPropertyDescriptors)
                if (((PropertyDescriptor)descriptor).Name.ToLower().IndexOf(filter) > -1)
                    _FilteredPropertyDescriptors.Add(((PropertyDescriptor)descriptor));
        }


        public object this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetPropertyValue(propertyName, value); }
        }

        private object GetPropertyValue(string propertyName)
        {
            var descriptor = _FullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                return descriptor.GetValue(null);
            else
                throw new Exception("Property is not found");
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            var descriptor = _FullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                descriptor.SetValue(null, value);
            else
                throw new Exception("Property is not found");
        }



		#region Implementation of ICustomTypeDescriptor

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return _Filter.Length != 0 ? _FilteredPropertyDescriptors : _FullPropertyDescriptors;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		#endregion
	}

    public class GenericPropertyDescriptor<T> : PropertyDescriptor
    {
        private T _value;

        public GenericPropertyDescriptor(string name, Attribute[] attrs)
            : base(name, attrs)
        {
        }

        public GenericPropertyDescriptor(string name, T value, Attribute[] attrs)
            : base(name, attrs)
        {
            _value = value;
        }
        protected GenericPropertyDescriptor(MemberDescriptor descr)
            : base(descr)
        {
            
        }
        protected GenericPropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
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
            return _value;
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
            _value = (T)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }


    //public class GenericPropertyDescriptor<T> : PropertyDescriptor
    //{
    //    private T m_value;

    //    public GenericPropertyDescriptor(string name, Attribute[] attrs)
    //        : base(name, attrs)
    //    {
    //    }

    //    public GenericPropertyDescriptor(string name, T value, Attribute[] attrs)
    //        : base(name, attrs)
    //    {
    //        this.m_value = value;
    //    }

    //    public override bool CanResetValue(object component)
    //    {
    //        return false;
    //    }

    //    public override System.Type ComponentType
    //    {
    //        get
    //        {
    //            return typeof(GenericPropertyDescriptor<T>);
    //        }
    //    }

    //    public override object GetValue(object component)
    //    {
    //        return this.m_value;
    //    }

    //    public override bool IsReadOnly
    //    {
    //        get
    //        {
    //            foreach (Attribute attribute in this.AttributeArray)
    //            {
    //                if (attribute is ReadOnlyAttribute)
    //                {
    //                    return true;
    //                }
    //            }
    //            return false;
    //        }
    //    }

    //    public override System.Type PropertyType
    //    {
    //        get
    //        {
    //            return typeof(T);
    //        }
    //    }

    //    public override void ResetValue(object component)
    //    {
    //    }

    //    public override void SetValue(object component, object value)
    //    {
    //        this.m_value = (T)value;
    //    }

    //    public override bool ShouldSerializeValue(object component)
    //    {
    //        return false;
    //    }
    //}
}
