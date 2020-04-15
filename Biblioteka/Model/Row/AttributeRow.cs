using System;
using System.Windows.Forms.VisualStyles;

namespace Biblioteka
{
    class AttributeRow : Row<Attribute>
    {
        public Attribute[] Attributes => base.values;

        public AttributeRow(Attribute[] attributes)
        {
            base.values = attributes;
        }

        // TODO: rewrite using delegates

        public bool Validate(Object[] values)
        {
            if (Attributes.Length == values.Length)
            {
                int index = 0;
                while (index<Attributes.Length)
                {
                    if (!Attributes[index].Validate(values[index])) return false;
                    index++;
                }
                return true;
            }
            return false;
        }

        public bool ValidateString(string[] values)
        {
            if (Attributes.Length == values.Length)
            {
                int index = 0;
                while (index < Attributes.Length)
                {
                    if (!Attributes[index].ValidateString(values[index])) return false;
                    index++;
                }
                return true;
            }
            return false;
        }
    }
}
