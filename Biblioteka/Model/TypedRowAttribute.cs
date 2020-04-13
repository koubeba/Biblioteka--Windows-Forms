using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class TypedRowAttribute<T>: RowAttribute, ICloneable where T: ICloneable
    {
        protected String attributeName { get; }
        protected T attributeValue;
        protected DataValidation<T> dataValidation;

        public TypedRowAttribute(string attributeName, DataValidation<T> dataValidation)
        {
            this.attributeName = attributeName;
            this.dataValidation = dataValidation;
        }

        override public Type GetAttributeType()
        {
            return typeof(T);
        }

        public override string GetAttributeName()
        {
            return this.attributeName;
        }

        public override bool ValidateValue(object value)
        {
            return this.dataValidation.Validate(value);
        }

        override public void SetAttributeValue(Object value)
        {
            this.attributeValue = (T)((T)value).Clone();
        }

        public override string ToString()
        {
            return attributeValue.ToString();
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType()) &&
                   this.equalsRowAttribute((TypedRowAttribute<T>)obj);
        }

        private bool equalsRowAttribute(TypedRowAttribute<T> other)
        {
            return this.attributeName.Equals(other.attributeName) &&
                   this.attributeValue.Equals(other.attributeValue);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
