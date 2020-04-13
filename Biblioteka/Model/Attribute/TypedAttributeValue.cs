using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class TypedAttributeValue<T>: AttributeValue where T: ICloneable
    {
        public new Object Value
        {
            get => base.Value;
            set
            {
                if (value.GetType() == typeof(T))
                {
                    base.Value = (T)value;
                }
                throw new ArgumentException("Provided argument should be of type " + typeof(T));
            }
        }

        public TypedAttributeValue(T value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                   typeof(T) == obj.GetType() &&
                   Value.Equals(((TypedAttributeValue<T>) obj).Value);
        }
    }
}
