using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class ListRow: ICloneable
    {
        protected RowAttribute[] rowAttributes;

        public ListRow(RowAttribute[] attributes, Object[] values)
        {
            this.rowAttributes = attributes.Select((attr) => (RowAttribute)attr.Clone()).ToArray();

            int i = 0;
            foreach (var val in values)
            {
                this.rowAttributes[i].SetAttributeValue(val);
                ++i;
            }
        }

        public 
        
        // Zwraca napis utworzony ze wszystkich atrybutów
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var attr in rowAttributes) result.Append(attr.ToString() + ", ");
            return result.ToString();
        }

        // Zwraca listę napisów poszczególnych atrybutów
        public String[] GetAttributeStrings()
        {
            return this.rowAttributes.Select((attr) => attr.ToString()).ToArray();
        }

        public override bool Equals(object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
