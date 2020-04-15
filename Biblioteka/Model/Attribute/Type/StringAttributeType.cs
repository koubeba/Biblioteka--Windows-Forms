using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model.Attribute.Type
{
    class StringAttributeType : AttributeGenericType<String>
    {
        public StringAttributeType(string value): base(value)
        { }

        public StringAttributeType(object value): base(value.ToString())
        { }
    }
}
