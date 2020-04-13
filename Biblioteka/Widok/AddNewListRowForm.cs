using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Biblioteka
{
    class AddNewListRowForm: Form
    {
        private readonly DataTable dataTable;
        private readonly Dictionary<Attribute, TextBox> textBoxes = new Dictionary<Attribute, TextBox>();

        readonly private int textLabelLength = 100;
        readonly private int textFieldLength = 150;
        readonly private int rowHeight = 25;
        readonly private int rowGap = 10;

        public AddNewListRowForm(DataTable dataTable)
        {
            this.dataTable = dataTable;
            Text = dataTable.Name + ": dodaj nowy rekord";
        }

        public void Load()
        {
            int i = 0;
            foreach (var attr in this.dataTable.AttributeRow.Attributes) {
                Label textLabel = new Label();
                textLabel.Text = attr.Name;
                textLabel.Location = new Point(0, i * (rowHeight + rowGap));
                textLabel.Size = new Size(textLabelLength, rowHeight);

                TextBox textBox = new TextBox();
                textBox.Location = new Point(textLabelLength, i * (rowHeight + rowGap));
                textBox.Size = new Size(textFieldLength, rowHeight);

                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.DataSource = textBox;

                textBox.Validating += (object sender, System.ComponentModel.CancelEventArgs e) =>
                {
                    if (!attr.Validate(textBox.Text))
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

            Button button = new Button();
            button.Text = "Dodaj";
            button.Location = new Point(0, i * (rowHeight + rowGap));
            button.Click += Button_Click;
            this.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // TODO: dodaj
        }
    }
}
