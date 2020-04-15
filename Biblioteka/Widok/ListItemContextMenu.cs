using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Biblioteka.Widok
{
    class ListItemContextMenu: ContextMenuStrip
    {
        private ListViewItem item;

        public delegate void RowEditedDelegate(object sender, ListViewItem item);
        public RowEditedDelegate RowEditedEvent;

        public delegate void RowDeletedDelegate(object sender, ListViewItem item);
        public RowDeletedDelegate RowDeletedEvent;
        public ListItemContextMenu(ListViewItem item)
        {
            this.item = item;

            ToolStripMenuItem editItem = new ToolStripMenuItem("Edytuj");
            editItem.Click += EditItem_Click;

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += DeleteItem_Click;

            Items.Add(editItem);
            Items.Add(deleteItem);
        }

        private void EditItem_Click(object sender, EventArgs e)
        {
            RowEditedEvent?.Invoke(this, item);
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            RowDeletedEvent?.Invoke(this, item);
        }
    }
}
