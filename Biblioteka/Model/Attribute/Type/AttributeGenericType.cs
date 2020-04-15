using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model.Attribute.Type
{
    abstract class AttributeGenericType<T> : AttributeType
    {
        public AttributeGenericType(string value) : base(value)
        { }
        public AttributeGenericType(object value) : base(value)
        { }
        public override object Clone()
        {
            if (typeof(T).IsPrimitive) return Value;
            return MemberwiseClone();
        }

        public override string AttributeToString()
        {
            return Value.ToString();
        }
    }
}
