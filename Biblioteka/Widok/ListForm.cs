using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteka
{
    class ListForm: Form
    {
        protected ListRowFactory listRowFactory;
        protected LibraryData libraryData;
        private ListView list = new ListView();

        public ListForm(String formName, LibraryData libraryData)
        {
            this.libraryData = libraryData;
            this.Text = formName;
            this.listRowFactory = new ListRowFactory(libraryData.Attributes);

            // Dodaj widok listy do kontrolek
            this.Controls.Add(list);
            this.list.View = View.Details;

            this.libraryData.NewRowAddedEvent += LibraryData_NewRowAddedEvent;

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
            foreach (var item in this.libraryData.Rows.ToArray())
            {
                this.list.Items.Add(generateListViewItemForRow(item, index++));
            }

            // Dodaj kolumny do widoku listy
            // Kolumna indeksu
            this.list.Columns.Add(String.Empty, -2, HorizontalAlignment.Left);

            foreach (var pair in this.libraryData.GetAttributeData()) this.list.Columns.Add(pair.Item1, -2, HorizontalAlignment.Left);
        }

        private ListViewItem generateListViewItemForRow(ListRow row, int index)
        {
            ListViewItem listViewItem = new ListViewItem(index.ToString());
            //listViewItem.Text = item.ToString();
            foreach (var attrString in row.GetAttributeStrings())
            {
                listViewItem.SubItems.Add(attrString);
            }
            return listViewItem;
        }

        private void addNewListViewItem(ListRow newRow)
        {
            this.list.Items.Add(generateListViewItemForRow(newRow, this.list.Items.Count+1));
        }

        private void LibraryData_NewRowAddedEvent(object sender, ListRow newRow)
        {
            this.addNewListViewItem(newRow);
        }
    }
}
