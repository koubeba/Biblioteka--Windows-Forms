using System;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    class TypedDataValidation<T> : DataValidation where T: AttributeType
    {
        private readonly Func<T, bool> validationFunction;
        private readonly Func<String, bool> stringValidationFunction;
        public TypedDataValidation(Func<T, bool> validationFunction, Func<String, bool> stringValidationFunction)
        {
            this.validationFunction = validationFunction;
            this.stringValidationFunction = stringValidationFunction;
        }

        public override bool ValidateString(string value)
        {
            return stringValidationFunction(value);
        }

        public override bool Validate(Object value)
        {
            return value is T convertedValue && validationFunction(convertedValue);
        }
    }
}
