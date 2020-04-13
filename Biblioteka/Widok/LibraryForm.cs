using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteka
{
    public partial class LibraryForm : Form
    {

        private List<ListFormFactory> availableChildrenForms;
        private LibraryDataRepository repository = new LibraryDataRepository();

        private DataValidation<String> basicStringValidation = new DataValidation<String>((String newVal) => { return newVal.IsNormalized() && newVal.Length != 0; });

        public LibraryForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.repository.AddDataCollection("Autorzy", new RowAttribute[] { 
                new TypedRowAttribute<String>("Imię", basicStringValidation), 
                new TypedRowAttribute<String>("Nazwisko", basicStringValidation) 
            });
            this.repository.AddToCollection("Autorzy", new String[] { "Ursula", "Le Guin" });
            this.repository.AddToCollection("Autorzy", new String[] { "Fumiko", "Enchi" });

            this.repository.AddDataCollection("Książki", new RowAttribute[] { 
                new TypedRowAttribute<String>("Tytuł", basicStringValidation), 
                new TypedRowAttribute<String>("Rok wydania", basicStringValidation) 
            });
            this.repository.AddToCollection("Książki", new String[] { "Maski", "1958" });
        }

        private void initializeChildrenForms()
        {
            this.availableChildrenForms = new List<ListFormFactory>();
            foreach (var pair in this.repository.GetKeyValuePairs())
            {
                this.availableChildrenForms.Add(new ListFormFactory(pair.Value, pair.Key));
            }
            
            this.availableChildrenForms.ForEach(
                form => dodajWidokToolStripMenuItem.DropDownItems.Add(createMenuItemOptionForForm(form))
            );
        }

        private void initializeAddRowOptions()
        {
            this.dodajRekordToolStripMenuItem.DropDownItems.AddRange(this.repository.GetKeyValuePairs()
                .Select(pair => createAddRowOptionForCollectionRow(pair.Key, pair.Value)).ToArray());
        }

        private void addChildrenViewOption(ListFormFactory newFormOption)
        {
            this.availableChildrenForms.Add(newFormOption);
            dodajWidokToolStripMenuItem.DropDownItems.Add(createMenuItemOptionForForm(newFormOption));
        }

        private ToolStripMenuItem createAddRowOptionForCollectionRow(String collectionName, LibraryData collection)
        {
            return new ToolStripMenuItem(collectionName, null, (object sender, EventArgs e) => {
                AddNewListRowForm newDialog = new AddNewListRowForm(collection);
                newDialog.Load();
                newDialog.MdiParent = this;
                newDialog.Show();
            });
        }

        private ToolStripMenuItem createMenuItemOptionForForm(ListFormFactory form)
        {
            return new ToolStripMenuItem(form.GetFormName(), null, (object sender, EventArgs e) => { 
                Form newForm = form.CreateForm();
                newForm.MdiParent = this;
                newForm.Show();
            });
        }

        private void LibraryForm_Load(object sender, EventArgs e)
        {
            this.initializeChildrenForms();
            this.initializeAddRowOptions();
        }

    }
}
