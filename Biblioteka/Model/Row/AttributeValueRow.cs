namespace Biblioteka
{
    class AttributeValueRow : Row<AttributeValue>
    {
        public AttributeValue[] AttributeValues => values;

        public AttributeValueRow(AttributeValue[] attributeValues)
        {
            values = attributeValues;
        }
    }
}
