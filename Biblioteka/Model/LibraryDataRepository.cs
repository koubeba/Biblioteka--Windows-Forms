using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    // TODO: singleton
    class LibraryDataRepository
    { 
        public Dictionary<String, DataTable> DataCollections { get; }

        public LibraryDataRepository()
        {
            this.DataCollections = new Dictionary<string, DataTable>();
        }
        public void AddDataCollection(String name, Attribute[] attributes)
        {
            DataCollections.Add(name, new DataTable(name, new AttributeRow(attributes)));
        }

        public void AddToCollection(String name, Object[] attributeValues)
        {
            // TODO: Sprawdź, czy kolekcja o takiej nazwie istnieje
            // TODO: Sprawdź, czy taki element już istnieje
            DataCollections[name].AddValueRow(attributeValues);
        }
    }
}
