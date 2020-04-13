using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    class LibraryDataRepository
    {
        protected Dictionary<String, LibraryData> dataCollections = new Dictionary<String, LibraryData>();

        public Dictionary<String, LibraryData> GetKeyValuePairs()
        {
            return this.dataCollections;
        }

        public Dictionary<String, LibraryData> AddDataCollection(String name, RowAttribute[] attributes)
        {
            this.dataCollections.Add(name, new LibraryData(name, attributes));
            return this.dataCollections;
        }

        public LibraryData AddToCollection(String name, Object[] attributeValues)
        {
            // Sprawdź, czy taki element już istnieje
            this.dataCollections[name].Rows.Any(row => row.GetHashCode().Equals(attributeValues.GetHashCode()));
            this.dataCollections[name].addRow(attributeValues);
            return this.dataCollections[name];
        }

        public LibraryData GetCollection(String name)
        {
            return this.dataCollections[name];
        }
    }
}
