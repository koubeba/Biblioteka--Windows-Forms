using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    abstract class Row<T>
    {
        protected T[] values;

        public override bool Equals(object obj)
        {
            return obj != null &&
                   obj.GetType() == typeof(Row<T>) &&
                   ValueArraysEqual((Row<T>)obj);
        }

        protected bool ValueArraysEqual(Row<T> otherRow)
        {
            return !values.Except(otherRow.values).Any();
        }
    }
}
