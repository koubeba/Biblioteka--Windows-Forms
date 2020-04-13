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
        private LibraryData data;
        private Dictionary<RowAttribute, TextBox> textBoxes = new Dictionary<RowAttribute, TextBox>();

        readonly private int textLabelLength = 100;
        readonly private int textFieldLength = 150;
        readonly private int rowHeight = 25;
        readonly private int rowGap = 10;

        public AddNewListRowForm(LibraryData data)
        {
            this.data = data;
            this.Text = data.Name + ": dodaj nowy rekord";
        }

        public void Load()
        {
            int i = 0;
            foreach (var attr in this.data.Attributes) {
                Label textLabel = new Label();
                textLabel.Text = attr.GetAttributeName();
                textLabel.Location = new Point(0, i * (rowHeight + rowGap));
                textLabel.Size = new Size(textLabelLength, rowHeight);

                TextBox textBox = new TextBox();
                textBox.Location = new Point(textLabelLength, i * (rowHeight + rowGap));
                textBox.Size = new Size(textFieldLength, rowHeight);

                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.DataSource = textBox;

                textBox.Validating += (object sender, System.ComponentModel.CancelEventArgs e) =>
                {
                    if (!attr.ValidateValue(textBox.Text))
                    {
                        e.Cancel = true;
                        errorProvider.SetError(textBox, "Nieprawidłowa wartość atrybutu " + attr.GetAttributeName());
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
            if (this.ValidateChildren())
                this.data.addRow(this.textBoxes.Select((pair) => pair.Value.Text).ToArray());
        }
    }
}
