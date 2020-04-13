using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class ListRowFactory
    {
        protected RowAttribute[] attributes;

        public ListRowFactory(RowAttribute[] attributes)
        {
            this.attributes = attributes;
        }

        protected void Validate(Object[] attributeValues)
        {
            if (!this.attributes.Length.Equals(attributeValues.Length))
                throw new ArgumentException("Podana lista wartości atrybutów jest nieprawidłowej długości. Oczekiwana jest lista długości " + this.attributes.Length);

            if (!this.checkAttributeList(attributeValues))
                throw new ArgumentException("Podane wartości argumentów sa nieprawidłowych typów");

        }

        protected bool checkAttributeList(Object[] attributeValues)
        {
            // Sprawdź, czy typy wartości atrybutów odpowiadają oczekiwanym
            int i = 0;
            foreach (var atrVal in attributeValues)
            {
                if (!atrVal.GetType().Equals(this.attributes[i].GetAttributeType())) return false;
                ++i;
            }
            return true;
        }

        public ListRow CreateListRow(Object[] attributeValues)
        {
            Validate(attributeValues);

            // Ponieważ wiemy, że typy przekazanych atrybutów są zgodne, możemy bezpiecznie rzutować typy.
            return new ListRow(this.attributes, attributeValues);
        }
    }
}
