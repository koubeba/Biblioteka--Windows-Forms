using System.Security.Permissions;

namespace Biblioteka
{
    class AttributeValueRow : Row<AttributeValue>
    {
        public AttributeValue[] AttributeValues => values;

        public AttributeValueRow(AttributeValue[] attributeValues)
        {
            values = attributeValues;
        }

        public void SetNewValues(AttributeValue[] newAttributeValues)
        {
            values = newAttributeValues;
        }
    }
}
