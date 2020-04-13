using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class TypedAttribute<T>: Attribute where T: ICloneable
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
