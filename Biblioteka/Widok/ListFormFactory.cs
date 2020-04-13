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
        private String formName;
        private RowAttribute[] attributes { get; }
        private LibraryData libraryData { get; }

        public ListFormFactory(LibraryData libraryData, string formName)
        {
            this.libraryData = libraryData;
            this.formName = formName;
        }

        public String GetFormName()
        {
            return this.formName;
        }

        public ListForm CreateForm()
        {
            return new ListForm(this.formName, this.libraryData);
        }
    }
}
