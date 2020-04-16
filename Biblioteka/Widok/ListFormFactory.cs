using System;
using System.Windows.Forms;

namespace Biblioteka
{
    class ListFormFactory
    {
        private readonly DataTable dataTable;
        private readonly ToolStripStatusLabel statusLabel;
        private readonly MenuStrip menuStrip;

        public ListFormFactory(DataTable dataTable, ToolStripStatusLabel statusLabel, MenuStrip menuStrip)
        {
            this.dataTable = dataTable;
            this.statusLabel = statusLabel;
            this.menuStrip = menuStrip;
        }

        public String GetFormName()
        {
            return this.dataTable.Name;
        }

        public ListForm CreateForm()
        {
            return new ListForm(this.dataTable, this.statusLabel, this.menuStrip);
        }
    }
}
