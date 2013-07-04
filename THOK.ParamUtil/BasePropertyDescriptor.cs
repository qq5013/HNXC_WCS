namespace THOK.ParamUtil
{
    using System;
    using System.ComponentModel;

    public class BasePropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor basePropertyDescriptor;

        public BasePropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
        {
            this.basePropertyDescriptor = basePropertyDescriptor;
        }

        public override bool CanResetValue(object component)
        {
            return this.basePropertyDescriptor.CanResetValue(component);
        }

        public override object GetValue(object component)
        {
            return this.basePropertyDescriptor.GetValue(component);
        }

        public override void ResetValue(object component)
        {
            this.basePropertyDescriptor.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            this.basePropertyDescriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return this.basePropertyDescriptor.ShouldSerializeValue(component);
        }

        public override Type ComponentType
        {
            get
            {
                return this.basePropertyDescriptor.ComponentType;
            }
        }

        public override string Description
        {
            get
            {
                return this.basePropertyDescriptor.Description;
            }
        }

        public override string DisplayName
        {
            get
            {
                string str = "";
                foreach (Attribute attribute in this.basePropertyDescriptor.Attributes)
                {
                    if (attribute is ChineseAttribute)
                    {
                        str = attribute.ToString();
                        break;
                    }
                }
                if (str == "")
                {
                    return this.basePropertyDescriptor.Name;
                }
                return str;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this.basePropertyDescriptor.IsReadOnly;
            }
        }

        public override string Name
        {
            get
            {
                return this.basePropertyDescriptor.Name;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return this.basePropertyDescriptor.PropertyType;
            }
        }
    }
}

