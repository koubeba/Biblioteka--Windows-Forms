using System;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    internal abstract class AttributeValue : ICloneable
    {
        public AttributeType Value { get; protected set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return Value.AttributeToString();
        }
    }
}