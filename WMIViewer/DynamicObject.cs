using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WMIViewer
{
    [Localizable(false)]
    public sealed class DynamicObject : ICustomTypeDescriptor
    {
        private readonly string filter = string.Empty;
        private readonly PropertyDescriptorCollection filteredPropertyDescriptors = new PropertyDescriptorCollection(null);
        private readonly PropertyDescriptorCollection fullPropertyDescriptors = new PropertyDescriptorCollection(null);

        public object this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetPropertyValue(propertyName, value); }
        }

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

            if (!string.IsNullOrEmpty(displayName))
                attrs.Add(new DisplayNameAttribute(displayName));

            if (!string.IsNullOrEmpty(description))
                attrs.Add(new DescriptionAttribute(description));

            if (!string.IsNullOrEmpty(category))
                attrs.Add(new CategoryAttribute(category));

            if (readOnly)
                attrs.Add(new ReadOnlyAttribute(true));

            fullPropertyDescriptors.Add(new GenericPropertyDescriptor<T>(
              name, value, attrs.ToArray()));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
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

            if (!string.IsNullOrEmpty(displayName))
                attrs.Add(new DisplayNameAttribute(displayName));

            if (!string.IsNullOrEmpty(description))
                attrs.Add(new DescriptionAttribute(description));

            if (!string.IsNullOrEmpty(category))
                attrs.Add(new CategoryAttribute(category));

            if (readOnly)
                attrs.Add(new ReadOnlyAttribute(true));

            fullPropertyDescriptors.Add(new GenericPropertyDescriptor<T>(
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
            var descriptor = fullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                fullPropertyDescriptors.Remove(descriptor);
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
            return filter.Length != 0 ? filteredPropertyDescriptors : fullPropertyDescriptors;
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

        private object GetPropertyValue(string propertyName)
        {
            var descriptor = fullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                return descriptor.GetValue(new object());
            throw new ObjectNotFoundException(propertyName);
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            var descriptor = fullPropertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                descriptor.SetValue(null, value);
            else
                throw new ObjectNotFoundException(propertyName);
        }
    }
}
