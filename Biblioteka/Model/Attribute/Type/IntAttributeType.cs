using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model.Attribute.Type
{
    class IntAttributeType : AttributeGenericType<short>
    {
        public IntAttributeType(object value): base((short)value)
        { }

        public IntAttributeType(string value): base(short.Parse(value))
        { }
    }
}
