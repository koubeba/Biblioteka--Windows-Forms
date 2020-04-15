using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    class DataTable
    {
        public string Name { get; }
        public AttributeRow AttributeRow { get; }
        public List<AttributeValueRow> AttributeValueRows { get; }

        public delegate void NewRowAddedDelegate(object sender, AttributeValueRow addedRow);

        public event NewRowAddedDelegate NewRowAddedEvent;

        public DataTable(string name, AttributeRow attributeRow)
        {
            Name = name;
            AttributeRow = attributeRow;
            AttributeValueRows = new List<AttributeValueRow>();
        }

        public AttributeValueRow AddValueRowFromString(string[] values)
        {
            if (AttributeRow.ValidateString(values))
            {
                try
                {
                    // TODO: zrobić
                    AttributeValueRow newRow = new AttributeValueRow(AttributeRow.Attributes.ToList().Select(
                        (Attribute attribute, int index) =>
                        {
                            return typeof(TypedAttributeValueFactory)
                                .GetMethod("GetAttributeValue", BindingFlags.Public | BindingFlags.Static)
                                .MakeGenericMethod(attribute.Type)
                                .Invoke(null, new object[] {values[index]});
                        }).OfType<AttributeValue>().ToArray());

                    this.AttributeValueRows.Add(newRow);
                    NewRowAddedEvent?.Invoke(this, newRow);
                    return newRow;
                }
                catch (NullReferenceException e)
                {
                    // TODO: obsłużyć jakoś wyjątek
                    return null;
                }
            }
            return null;
        }

        public AttributeValueRow AddValueRow(Object[] values)
        {
            //if (AttributeRow.Validate(values))
            //{
                try
                {
                    // TODO: zrobić
                    AttributeValueRow newRow = new AttributeValueRow(values.OfType<AttributeType>().ToList().Select(
                        (AttributeType o) =>
                        {
                            return typeof(TypedAttributeValueFactory)
                                .GetMethod("GetAttributeValue", BindingFlags.Public | BindingFlags.Static)
                                .MakeGenericMethod(o.GetType())
                                .Invoke(null, new object[] { o });
                        }).OfType<AttributeValue>().ToArray());
                    this.AttributeValueRows.Add(newRow);
                    NewRowAddedEvent?.Invoke(this, newRow);
                    return newRow;
                }
                catch (NullReferenceException e)
                {
                    // TODO: obsłużyć jakoś wyjątek
                    return null;
                }
            //}
            //return null;
        }
    }
}
