using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    abstract class RowAttribute: ICloneable
    {
        abstract public Type GetAttributeType();
        abstract public String GetAttributeName();
        abstract public void SetAttributeValue(Object value);
        abstract public bool ValidateValue(Object value);
        abstract public object Clone();
    }
}
