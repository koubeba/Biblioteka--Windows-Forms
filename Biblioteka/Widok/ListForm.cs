using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Biblioteka.Model.Filter;
using Biblioteka.Widok;

namespace Biblioteka
{
    class ListForm : Form
    {
        public DataTable dataTable { get; }
        private readonly ToolStripStatusLabel statusStrip;
        private readonly MenuStrip menuStrip; 

        public MenuStrip FilterMenuStrip;
        private readonly ListView list = new ListView();
        public FilterRule FilterRule { get; set; }

        public ListForm(DataTable dataTable, ToolStripStatusLabel statusStrip, MenuStrip menuStrip)
        {
            this.dataTable = dataTable;
            Text = this.dataTable.Name;
            this.menuStrip = menuStrip;
            
            FilterMenuStrip = new MenuStrip();
            FilterMenuStrip.AllowMerge = true;

            FilterRule = new FilterRule(dataTable.AttributeRow);
            FilterRule.AddPartialRule("Imię", new AttributePartialRule(
                (value => value.ToString().First().Equals('U'))
                ));

            ToolStripMenuItem filterMenuItem = new ToolStripMenuItem("Filtruj");
            filterMenuItem.Click += (object sender, EventArgs e) =>
            {
                FilterForm newFilterForm = new FilterForm(this);
                newFilterForm.MdiParent = MdiParent;
                newFilterForm.Load();
                newFilterForm.Show();

            };
            ToolStripMenuItem unfilterMenuItem = new ToolStripMenuItem("Usuń filtr");
            unfilterMenuItem.Click += (sender, args) =>
            {
                FilterRule = null;
                InitializeListViewItems();
            };

            FilterMenuStrip.Items.Add(filterMenuItem);
            FilterMenuStrip.Items.Add(unfilterMenuItem);

            this.statusStrip = statusStrip;

            // Dodaj widok listy do kontrolek
            Controls.Add(list);
            list.View = View.Details;

            this.dataTable.NewRowAddedEvent += LibraryData_NewRowAddedEvent;
            this.dataTable.RowDeletedEvent += DataTableOnRowDeletedEvent;
            this.dataTable.RowEditedEvent += DataTable_RowEditedEvent;

            // Dodaj elementy kolekcji do listy
            this.InitializeListViewItems();
            // Ustaw widok listy na wypełniający całe okno
            list.SetBounds(0, 0, this.Size.Width, this.Size.Height);

            list.GridLines = true;
            list.MouseClick += List_MouseClick;

            this.FormClosing += ListForm_FormClosing;
            list.GotFocus += List_GotFocus;

            this.Activated += ListForm_Activated;
            this.Deactivate += ListForm_Deactivate;
        }

        private void ListForm_Deactivate(object sender, EventArgs e)
        {
            ToolStripManager.RevertMerge(menuStrip, this.FilterMenuStrip);
        }

        private void ListForm_Activated(object sender, EventArgs e)
        {
            ToolStripManager.Merge( this.FilterMenuStrip, menuStrip);
        }

        private void List_GotFocus(object sender, EventArgs e)
        {
            UpdateToolstripStatus();
        }

        private void ListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MdiParent.MdiChildren.Length == 1) e.Cancel = true;
                else e.Cancel = false;
            }
        }

        private void InitializeListViewItems()
        {
            this.list.Items.Clear();
            int index = 0;
            foreach (AttributeValueRow row in this.dataTable.AttributeValueRows)
            {
                FilterOneRow(index);
                index++;
            }

            // Dodaj kolumny do widoku listy
            // Kolumna indeksu
            this.list.Columns.Add(String.Empty, -2, HorizontalAlignment.Left);

            foreach (Attribute attribute in this.dataTable.AttributeRow.Attributes) this.list.Columns.Add(attribute.Name, -2, HorizontalAlignment.Left);
            UpdateToolstripStatus();
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
                    ListItemContextMenu menu = new ListItemContextMenu(list.FocusedItem, int.Parse(list.FocusedItem.Text));
                    menu.RowEditedEvent += RowEditedEvent;
                    menu.RowDeletedEvent += RowDeletedEvent;
                    menu.Show(Cursor.Position);
                }
            }
        }

        private void FilterOneRow(int rowIndex)
        {
            AttributeValueRow rowToAdd = dataTable.AttributeValueRows[rowIndex];
            if (FilterRule != null && !FilterRule.Filter(rowToAdd)) return; 
            addNewListViewItem(dataTable.AttributeValueRows[rowIndex], rowIndex);
        }

        private void RowDeletedEvent(object sender, ListViewItem item, int index)
        {
            dataTable.DeleteValueRow(index);
        }

        private void RowEditedEvent(object sender, ListViewItem item, int index)
        {
            EditListRowForm editForm = new EditListRowForm(dataTable, index);
            editForm.Load();
            editForm.MdiParent = this.MdiParent;
            editForm.Show();
        }

        private void addNewListViewItem(AttributeValueRow newRow, int index)
        {
            if (FilterRule != null && !FilterRule.Filter(newRow)) return;
            this.list.Items.Add(generateListViewItemForRow(newRow, index));
        }

        private void LibraryData_NewRowAddedEvent(object sender, AttributeValueRow newRow)
        {
            this.addNewListViewItem(newRow, this.dataTable.AttributeValueRows.Count-1);
            UpdateToolstripStatus();
        }

        private void DataTableOnRowDeletedEvent(object sender, int deletedrowindex)
        {
            list.Items.RemoveAt(deletedrowindex);
            InitializeListViewItems();
            UpdateToolstripStatus();
        }

        private void DataTable_RowEditedEvent(object sender, int editedRowIndex)
        {
            InitializeListViewItems();
            UpdateToolstripStatus();
        }

        private void UpdateToolstripStatus()
        { 
            statusStrip.Text = "Liczba elementów: " + this.list.Items.Count.ToString();
        }

        public void ApplyFilter(FilterRule rule)
        {
            FilterRule = rule;
            InitializeListViewItems();
            UpdateToolstripStatus();
        }
    }
}
