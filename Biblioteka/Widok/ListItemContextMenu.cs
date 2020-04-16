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
        private int index;

        public delegate void RowEditedDelegate(object sender, ListViewItem item, int index);
        public RowEditedDelegate RowEditedEvent;

        public delegate void RowDeletedDelegate(object sender, ListViewItem item, int index);
        public RowDeletedDelegate RowDeletedEvent;
        public ListItemContextMenu(ListViewItem item, int index)
        {
            this.item = item;
            this.index = index;

            ToolStripMenuItem editItem = new ToolStripMenuItem("Edytuj");
            editItem.Click += EditItem_Click;

            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += DeleteItem_Click;

            Items.Add(editItem);
            Items.Add(deleteItem);
        }

        private void EditItem_Click(object sender, EventArgs e)
        {
            RowEditedEvent?.Invoke(this, item, index);
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            RowDeletedEvent?.Invoke(this, item, index);
        }
    }
}
