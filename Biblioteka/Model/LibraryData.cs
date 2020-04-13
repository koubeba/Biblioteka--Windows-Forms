using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class LibraryData
    {
        readonly protected String name;
        public String Name
        {
            get { return this.name; }
        }
        readonly protected ListRowFactory listRowFactory;
        readonly protected RowAttribute[] attributes;
        public RowAttribute[] Attributes
        {
            get { return this.attributes; }
        }

        protected List<ListRow> rows;

        public List<ListRow> Rows
        {
            get { return this.rows; }
        }

        public delegate void NewRowAddedDelegate(object sender, ListRow addedRow);

        public event NewRowAddedDelegate NewRowAddedEvent;

        public LibraryData(String name, RowAttribute[] attributes)
        {
            this.name = name;
            this.attributes = attributes;
            this.rows = new List<ListRow>();
            this.listRowFactory = new ListRowFactory(attributes);
        }

        public Tuple<string, Type>[] GetAttributeData()
        {
            return this.attributes.Select((attr) => new Tuple<string, Type>(attr.GetAttributeName(), attr.GetAttributeType())).ToArray();
        }

        public List<ListRow> addRow(Object[] attributeValues)
        {
            ListRow addedRow = listRowFactory.CreateListRow(attributeValues);
            this.rows.Add(addedRow);

            // Zasygnalizuj dodanie nowego wiersza
            NewRowAddedEvent?.Invoke(this, addedRow);

            return this.rows;
        }
    }
}
