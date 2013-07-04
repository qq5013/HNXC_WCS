namespace THOK.ParamUtil
{
    using System;
    using System.ComponentModel;

    public class BaseObject : ICustomTypeDescriptor
    {
        private PropertyDescriptorCollection globalizedProps;

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            if (this.globalizedProps == null)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
                this.globalizedProps = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor descriptor in properties)
                {
                    this.globalizedProps.Add(new BasePropertyDescriptor(descriptor));
                }
            }
            return this.globalizedProps;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (this.globalizedProps == null)
            {
                PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(this, attributes, true);
                this.globalizedProps = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor descriptor in descriptors)
                {
                    this.globalizedProps.Add(new BasePropertyDescriptor(descriptor));
                }
            }
            return this.globalizedProps;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}

