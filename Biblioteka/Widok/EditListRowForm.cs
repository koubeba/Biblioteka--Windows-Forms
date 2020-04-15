using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Biblioteka
{
    class EditListRowForm : Form
    {
        private readonly DataTable dataTable;
        private int editedIndex;
        private readonly Dictionary<Attribute, TextBox> textBoxes = new Dictionary<Attribute, TextBox>();

        readonly private int textLabelLength = 100;
        readonly private int textFieldLength = 150;
        readonly private int rowHeight = 25;
        readonly private int rowGap = 10;

        public EditListRowForm(DataTable dataTable, int editedIndex)
        {
            this.dataTable = dataTable;
            this.editedIndex = editedIndex;
            Text = dataTable.Name + ": edytuj rekord " + editedIndex;
        }

        public void Load()
        {
            int i = 0;
            AttributeValueRow editedRow = dataTable.AttributeValueRows[editedIndex];

            // Dodanie TextBox dla każdego atrybutu
            foreach (var attr in this.dataTable.AttributeRow.Attributes)
            {
                Label textLabel = new Label();
                textLabel.Text = attr.Name;
                textLabel.Location = new Point(0, i * (rowHeight + rowGap));
                textLabel.Size = new Size(textLabelLength, rowHeight);

                TextBox textBox = new TextBox();
                textBox.Location = new Point(textLabelLength, i * (rowHeight + rowGap));
                textBox.Size = new Size(textFieldLength, rowHeight);
                textBox.Text = editedRow.AttributeValues[i].ToString();

                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.DataSource = textBox;

                textBox.Validating += (object sender, System.ComponentModel.CancelEventArgs e) =>
                {
                    if (!attr.DataValidation.ValidateString(textBox.Text))
                    {
                        e.Cancel = true;
                        errorProvider.SetError(textBox, "Nieprawidłowa wartość atrybutu " + attr.Name);
                    }
                };
                textBox.Validated += (object sender, EventArgs e) =>
                {
                    errorProvider.Clear();
                };

                this.textBoxes[attr] = textBox;

                this.Controls.Add(textLabel);
                this.Controls.Add(textBox);

                i++;
            }

            // Przycisk Dodaj
            Button button = new Button();
            button.Text = "Edytuj";
            button.Location = new Point(0, i * (rowHeight + rowGap));
            button.Click += Button_Click;
            this.Controls.Add(button);

            // Dodanie możliwości zaakceptowania za pomocą Enter
            AcceptButton = button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.dataTable.EditValueRowFromString(editedIndex, textBoxes.Select(o => o.Value.Text).ToArray());
        }
    }
}
