using System;

namespace Biblioteka
{
    internal abstract class AttributeValue : ICloneable
    {
        private Object value;
        public virtual Object Value { get; protected set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}