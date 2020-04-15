using System;
using System.Windows.Forms;

namespace Biblioteka
{
    class ListForm : Form
    {
        private readonly DataTable dataTable;
        private readonly ListView list = new ListView();

        public ListForm(DataTable dataTable)
        {
            this.dataTable = dataTable;
            this.Text = this.dataTable.Name;

            // Dodaj widok listy do kontrolek
            Controls.Add(list);
            list.View = View.Details;

            // TODO: dodać delegate
            this.dataTable.NewRowAddedEvent += LibraryData_NewRowAddedEvent;

            // TODO: check if attributes are a subset of libraryData attributes

            // Dodaj elementy kolekcji do listy
            this.initializeListViewItems();
            // Ustaw widok listy na wypełniający całe okno
            list.SetBounds(0, 0, this.Size.Width, this.Size.Height);

            list.GridLines = true;

        }

        private void initializeListViewItems()
        {
            int index = 1;
            foreach (AttributeValueRow row in this.dataTable.AttributeValueRows)
            {
                this.list.Items.Add(generateListViewItemForRow(row, index++));
            }

            // Dodaj kolumny do widoku listy
            // Kolumna indeksu
            this.list.Columns.Add(String.Empty, -2, HorizontalAlignment.Left);

            foreach (Attribute attribute in this.dataTable.AttributeRow.Attributes) this.list.Columns.Add(attribute.Name, -2, HorizontalAlignment.Left);
        }

        private ListViewItem generateListViewItemForRow(AttributeValueRow row, int index)
        {
            ListViewItem listViewItem = new ListViewItem(index.ToString());
            foreach (AttributeValue val in row.AttributeValues)
            {
                listViewItem.SubItems.Add(val.ToString());
            }
            return listViewItem;
        }

        private void addNewListViewItem(AttributeValueRow newRow)
        {
            // TODO
            this.list.Items.Add(generateListViewItemForRow(newRow, this.list.Items.Count+1));
        }

        private void LibraryData_NewRowAddedEvent(object sender, AttributeValueRow newRow)
        {
            this.addNewListViewItem(newRow);
        }
    }
}
