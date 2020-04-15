using System;
using System.Windows.Forms;

namespace Biblioteka
{
    class ListFormFactory
    {
        private readonly DataTable dataTable;
        private readonly ToolStripStatusLabel statusLabel;

        public ListFormFactory(DataTable dataTable, ToolStripStatusLabel statusLabel)
        {
            this.dataTable = dataTable;
            this.statusLabel = statusLabel;
        }

        public String GetFormName()
        {
            return this.dataTable.Name;
        }

        public ListForm CreateForm()
        {
            return new ListForm(this.dataTable, this.statusLabel);
        }
    }
}
