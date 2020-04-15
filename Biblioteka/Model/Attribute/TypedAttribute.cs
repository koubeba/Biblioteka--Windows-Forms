using System;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    class TypedAttribute<T> : Attribute where T : AttributeType
    {
        public new string Name => base.Name;

        public new Type Type => base.Type;

        public TypedAttribute(string name, TypedDataValidation<T> dataValidation)
        {
            base.Name = name;
            base.Type = typeof(T);
            base.DataValidation = dataValidation;
        }
        public override bool Equals(object obj)
        {
            return obj != null &&
                   this.Type == obj.GetType() &&
                   this.EqualsAttribute((TypedAttribute<T>)obj);
        }

        private bool EqualsAttribute(TypedAttribute<T> other)
        {
            return other.Type == typeof(T) &&
                    this.Name.Equals(other.Name);
        }
    }
}
