using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class AttributeValueRow: Row<AttributeValue>
    {
        public AttributeValue[] AttributeValues => base.values;

        public AttributeValueRow(AttributeValue[] attributeValues)
        {
            base.values = attributeValues;
        }
    }
}
