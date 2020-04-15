using System;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    static class TypedAttributeValueFactory
    {
        public static TypedAttributeValue<T> GetAttributeValue<T>(object value) where T : AttributeType
        {
            return new TypedAttributeValue<T>(value);
        }
    }
}
