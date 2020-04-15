using System;

namespace Biblioteka
{
    abstract class DataValidation
    {
        public abstract bool ValidateString(string value);
        public abstract bool Validate(Object value);
    }
}
