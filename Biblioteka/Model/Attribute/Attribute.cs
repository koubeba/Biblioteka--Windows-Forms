using System;

namespace Biblioteka
{
    abstract class Attribute
    {
        public virtual string Name { get; protected set; }
        public virtual Type Type { get; protected set; }
        public virtual DataValidation DataValidation { get; protected set; }
        public virtual bool Validate(Object value)
        {
            return value != null && DataValidation != null && DataValidation.Validate(value);
        }
        public virtual bool ValidateString(string value)
        {
            return value != null && DataValidation != null && DataValidation.ValidateString(value);
        }
    }
}
