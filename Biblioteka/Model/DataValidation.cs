using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class DataValidation<T>
    {
        protected Func<T, bool> validationFunction;

        public DataValidation(Func<T, bool> validationFunction)
        {
            this.validationFunction = validationFunction;
        }

        public bool Validate(Object value)
        {
            return value.GetType() == typeof(T) && this.validationFunction((T)value);
        }
    }
}
