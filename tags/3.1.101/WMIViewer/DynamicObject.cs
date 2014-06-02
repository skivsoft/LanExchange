using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace WMIViewer
{
    [Localizable(false)]
    public sealed class DynamicObject : ICustomTypeDescriptor
	{
        private readonly string m_Filter = string.Empty;
        private readonly PropertyDescriptorCollection m_FilteredPropertyDescriptors = new PropertyDescriptorCollection(null);
        private readonly PropertyDescriptorCollection m_FullPropertyDescriptors = new PropertyDescriptorCollection(null);

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

            m_FullPropertyDescriptors.Add(new GenericPropertyDescriptor<T>(
              name, value, attrs.ToArray()));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public void AddPropertyNull<T>(string name, string displayName, string description,
            string category, bool readOnly, IEnumerable<Attribute> attributes)
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

            m_FullPropertyDescriptors.Add(new GenericPropertyDescriptor<T>(
              name, attrs.ToArray()));
        }

        public void AddProperty<T>(
          string name,
          T value,
          string description,
          string category,
          bool readOnly)
        {
            AddProperty(name, value, name, description, category, readOnly, null);
        }

        public void RemoveProperty(string propertyName)
        {
            var descriptor = m_FullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                m_FullPropertyDescriptors.Remove(descriptor);
            else
                throw new ObjectNotFoundException(propertyName);
        }

        public object this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetPropertyValue(propertyName, value); }
        }

        private object GetPropertyValue(string propertyName)
        {
            var descriptor = m_FullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                return descriptor.GetValue(new object());
            throw new ObjectNotFoundException(propertyName);
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            var descriptor = m_FullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                descriptor.SetValue(null, value);
            else
                throw new ObjectNotFoundException(propertyName);
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
            return m_Filter.Length != 0 ? m_FilteredPropertyDescriptors : m_FullPropertyDescriptors;
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
}
