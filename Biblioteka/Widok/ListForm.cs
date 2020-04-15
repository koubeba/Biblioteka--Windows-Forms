using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Biblioteka.Widok;

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

            this.dataTable.NewRowAddedEvent += LibraryData_NewRowAddedEvent;
            this.dataTable.RowDeletedEvent += DataTableOnRowDeletedEvent;
            this.dataTable.RowEditedEvent += DataTable_RowEditedEvent;

                // TODO: check if attributes are a subset of libraryData attributes

            // Dodaj elementy kolekcji do listy
            this.initializeListViewItems();
            // Ustaw widok listy na wypełniający całe okno
            list.SetBounds(0, 0, this.Size.Width, this.Size.Height);

            list.GridLines = true;
            list.MouseClick += List_MouseClick;

            this.FormClosing += ListForm_FormClosing;

        }

        private void ListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MdiParent.MdiChildren.Length == 1) e.Cancel = true;
                else e.Cancel = false;
            }
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

        private void List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (list.FocusedItem.Bounds.Contains(e.Location))
                {
                    ListItemContextMenu menu = new ListItemContextMenu(list.FocusedItem);
                    menu.RowEditedEvent += RowEditedEvent;
                    menu.RowDeletedEvent += RowDeletedEvent;
                    menu.Show(Cursor.Position);
                }
            }
        }

        private void RowDeletedEvent(object sender, ListViewItem item)
        {
            dataTable.DeleteValueRow(item.Index);
        }

        private void RowEditedEvent(object sender, ListViewItem item)
        {
            EditListRowForm editForm = new EditListRowForm(dataTable, item.Index);
            editForm.Load();
            editForm.MdiParent = this.MdiParent;
            editForm.Show();
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

        private void DataTableOnRowDeletedEvent(object sender, int deletedrowindex)
        {
            list.Items.RemoveAt(deletedrowindex);
        }

        private void DataTable_RowEditedEvent(object sender, int editedRowIndex)
        {
            list.Items.RemoveAt(editedRowIndex);
            list.Items.Insert(editedRowIndex,
                generateListViewItemForRow(dataTable.AttributeValueRows[editedRowIndex], editedRowIndex + 1));
        }
    }
}
