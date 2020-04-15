using System;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    class TypedAttributeValue<T> : AttributeValue where T : AttributeType
    {
        public TypedAttributeValue(Object value)
        {
            Console.WriteLine(value);
            if (value is null) throw new ArgumentNullException("Typed argument value cannot be null");
            
            // TODO: add exception handling
            if (value is T typedValue) Value = typedValue;
            else if (value is string stringValue) Value = (T)Activator.CreateInstance(typeof(T), stringValue);
            else Value = (T)Activator.CreateInstance(typeof(T), value);
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                   typeof(T) == obj.GetType() &&
                   Value.Equals(((TypedAttributeValue<T>)obj).Value);
        }
    }
}
