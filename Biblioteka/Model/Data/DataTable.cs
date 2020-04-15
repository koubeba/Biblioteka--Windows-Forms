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

        public delegate void RowDeletedDelegate(object sender, int deletedRowIndex);
        public event RowDeletedDelegate RowDeletedEvent;

        public delegate void RowEditedDelegate(object sender, int editedRowIndex);
        public event RowEditedDelegate RowEditedEvent;

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

        public void EditValueRowFromString(int editedIndex, string[] values)
        {
            if (AttributeRow.ValidateString(values))
            {
                try
                {
                    AttributeValue[] newValues = AttributeRow.Attributes.ToList().Select(
                        (Attribute attribute, int index) =>
                        {
                            return typeof(TypedAttributeValueFactory)
                                .GetMethod("GetAttributeValue", BindingFlags.Public | BindingFlags.Static)
                                .MakeGenericMethod(attribute.Type)
                                .Invoke(null, new object[] { values[index] });
                        }).OfType<AttributeValue>().ToArray();

                    AttributeValueRows[editedIndex].SetNewValues(newValues);
                    RowEditedEvent?.Invoke(this, editedIndex);
                }
                catch (NullReferenceException e)
                {
                    // TODO: obsłużyć jakoś wyjątek
                }
            }
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

        public void DeleteValueRow(int rowIndex)
        {
            AttributeValueRows.RemoveAt(rowIndex);
            RowDeletedEvent?.Invoke(this, rowIndex);
        }
    }
}
