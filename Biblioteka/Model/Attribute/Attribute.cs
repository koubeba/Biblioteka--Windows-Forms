using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    abstract class Attribute
    {
        private readonly string name;
        private readonly Type type;
        private readonly DataValidation dataValidation;

        public virtual string Name { get; protected set; }
        public virtual Type Type { get; protected set; }
        public virtual DataValidation DataValidation { get; protected set; }
        public virtual bool Validate(Object value)
        {
            return value != null  && dataValidation != null && dataValidation.Validate(value);
        }
    }
}
