using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    static class TypedAttributeValueFactory
    {
        public static TypedAttributeValue<T> GetAttributeValue<T>(T value) where T: ICloneable
        {
            return new TypedAttributeValue<T>(value);
        }
    }
}
