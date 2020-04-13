using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteka
{
    class ListFormFactory
    {
        private DataTable dataTable;

        public ListFormFactory(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }

        public String GetFormName()
        {
            return this.dataTable.Name;
        }

        public ListForm CreateForm()
        {
            return new ListForm(this.dataTable);
        }
    }
}
