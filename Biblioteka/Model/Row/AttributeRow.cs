using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteka
{
    class AttributeRow: Row<Attribute>
    {
        public Attribute[] Attributes => base.values;

        public AttributeRow(Attribute[] attributes)
        {
            base.values = attributes;
        }

        public bool Validate(Object[] values)
        {
            if (Attributes.Length == values.Length)
            {
                int index = 0;
                foreach (Attribute attr in Attributes)
                {
                    if (!attr.Validate(values[index])) return false;
                }
                return true;
            }
            return false;
        }
    }
}
