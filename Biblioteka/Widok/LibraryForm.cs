using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Biblioteka.Model.Attribute.Type;

namespace Biblioteka
{
    public partial class LibraryForm : Form
    {

        private List<ListFormFactory> availableChildrenForms;
        private readonly LibraryDataRepository repository = new LibraryDataRepository();

        private readonly TypedDataValidation<StringAttributeType> basicStringValidation = 
            new TypedDataValidation<StringAttributeType>(
                newVal => ((string)newVal.Value).Length != 0,
                stringVal => stringVal.IsNormalized() && stringVal.Length != 0
                );
        private readonly TypedDataValidation<IntAttributeType> dateValidation = new TypedDataValidation<IntAttributeType>(
            newVal => (short)newVal.Value >= 1600 && (short)newVal.Value <= 2020,
            stringVal =>
            {
                if (short.TryParse(stringVal, out short parsed)) return parsed >= 1600 && parsed <= 2020;
                return false;
            });

        public LibraryForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.repository.AddDataCollection("Autorzy", new Attribute[] {
                new TypedAttribute<StringAttributeType>("Imię", basicStringValidation),
                new TypedAttribute<StringAttributeType>("Nazwisko", basicStringValidation)
            });
            this.repository.AddToCollection("Autorzy", new AttributeType[]
            {
                new StringAttributeType("Ursula"), 
                new StringAttributeType("Le Guin")
            });
            this.repository.AddToCollection("Autorzy", new AttributeType[]
            {
                new StringAttributeType("Fumiko"), 
                new StringAttributeType("Enchi")
            });

            this.repository.AddDataCollection("Książki", new Attribute[] {
                new TypedAttribute<StringAttributeType>("Tytuł", basicStringValidation),
                new TypedAttribute<IntAttributeType>("Rok wydania", dateValidation)
            });
            this.repository.AddToCollection("Książki", new AttributeType[]
            {
                new StringAttributeType("Maski"), 
                new IntAttributeType((short)1958)
            });
        }

        private void initializeChildrenForms()
        {
            this.availableChildrenForms = new List<ListFormFactory>();
            foreach (KeyValuePair<string, DataTable> pair in this.repository.DataCollections)
            {
                this.availableChildrenForms.Add(new ListFormFactory(pair.Value, ElementsCount));
            }

            this.availableChildrenForms.ForEach(
                form => dodajWidokToolStripMenuItem.DropDownItems.Add(createMenuItemOptionForForm(form))
            );
        }

        private void initializeAddRowOptions()
        {
            this.dodajRekordToolStripMenuItem.DropDownItems.AddRange(this.repository.DataCollections
                .Select(pair => createAddRowOptionForCollectionRow(pair.Value)).ToArray());
        }

        private void addChildrenViewOption(ListFormFactory newFormOption)
        {
            this.availableChildrenForms.Add(newFormOption);
            dodajWidokToolStripMenuItem.DropDownItems.Add(createMenuItemOptionForForm(newFormOption));
        }

        private ToolStripMenuItem createAddRowOptionForCollectionRow(DataTable collection)
        {
            return new ToolStripMenuItem(collection.Name, null, (object sender, EventArgs e) =>
            {
                AddNewListRowForm newDialog = new AddNewListRowForm(collection);
                newDialog.Load();
                newDialog.MdiParent = this;
                newDialog.Show();
            });
        }

        private ToolStripMenuItem createMenuItemOptionForForm(ListFormFactory form)
        {
            return new ToolStripMenuItem(form.GetFormName(), null, (object sender, EventArgs e) =>
            {
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
