using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class TypedDataValidation<T>: DataValidation
    {
        private readonly Func<T, bool> validationFunction;

        public TypedDataValidation(Func<T, bool> validationFunction)
        {
            this.validationFunction = validationFunction;
        }

        public override bool Validate(Object value)
        {
            return value.GetType() == typeof(T) && this.validationFunction((T)value);
        }
    }
}
