using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Biblioteka
{
    class DataTable
    {
        public string Name { get; }
        public AttributeRow AttributeRow { get; }
        public List<AttributeValueRow> AttributeValueRows { get; }

        public DataTable(string name, AttributeRow attributeRow)
        {
            Name = name;
            AttributeRow = attributeRow;
            AttributeValueRows = new List<AttributeValueRow>();
        }

        public AttributeValueRow AddValueRow(Object[] values)
        {
            if (AttributeRow.Validate(values))
            {
                try
                {
                    AttributeValueRow newRow = new AttributeValueRow(values.OfType<ICloneable>().ToList().Select(
                        (ICloneable o) =>
                        {
                            return typeof(TypedAttributeValueFactory)
                                .GetMethod("GetAttributeValue", BindingFlags.Public | BindingFlags.Static)
                                .MakeGenericMethod(o.GetType())
                                .Invoke(null, new object[] {o});
                        }).OfType<AttributeValue>().ToArray());
                    this.AttributeValueRows.Add(newRow);
                }
                catch (NullReferenceException e)
                {
                    // TODO: obsłużyć jakoś wyjątek
                    return null;
                }
            }
            return null;
        }
    }
}
