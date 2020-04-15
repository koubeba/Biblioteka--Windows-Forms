using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model.Attribute.Type
{
    abstract class AttributeType: ICloneable
    {
        public object Value { get; protected set; }
        public AttributeType(string value)
        {
            Value = value;
        }
        public AttributeType(object value)
        {
            Value = value;
        }
        public abstract string AttributeToString();
        public abstract object Clone();
    }
}
