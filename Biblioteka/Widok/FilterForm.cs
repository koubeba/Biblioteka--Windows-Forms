using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Biblioteka.Model.Filter;

namespace Biblioteka
{
    class FilterForm : Form
    {
        private readonly ListForm listForm;
        private readonly DataTable dataTable;
        private readonly Dictionary<string, ComboBox> comboBoxes = new Dictionary<string, ComboBox>();
        private readonly Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();

        readonly private int textLabelLength = 100;
        readonly private int textFieldLength = 100;
        readonly private int rowHeight = 30;
        readonly private int rowGap = 10;
        readonly private int dropdownLength = 40;

        public FilterForm(ListForm listForm)
        {
            this.listForm = listForm;
            this.dataTable = listForm.dataTable;
            Text = dataTable.Name + ": filtruj";
        }

        public void Load()
        {
            int i = 0;

            // Dodanie TextBox dla każdego atrybutu
            foreach (var attr in this.dataTable.AttributeRow.Attributes)
            {
                Label textLabel = new Label();
                textLabel.Text = attr.Name;
                textLabel.Location = new Point(0, i * (rowHeight + rowGap));
                textLabel.Size = new Size(textLabelLength, rowHeight);

                ComboBox ruleTypeSelection = new ComboBox();
                ruleTypeSelection.Items.Add(FilterRuleType.EqualRule);
                ruleTypeSelection.Items.Add(FilterRuleType.GreaterThanRule);
                ruleTypeSelection.Items.Add(FilterRuleType.LesserThanRule);
                ruleTypeSelection.Items.Add(FilterRuleType.NotEqualThanRule);
                ruleTypeSelection.Size = new Size(dropdownLength, rowHeight);
                ruleTypeSelection.Location = new Point(textLabelLength, i * (rowHeight + rowGap));

                TextBox textBox = new TextBox();
                textBox.Location = new Point(textLabelLength + dropdownLength, i * (rowHeight + rowGap));
                textBox.Size = new Size(textFieldLength, rowHeight);

                ErrorProvider errorProvider = new ErrorProvider();
                errorProvider.DataSource = textBox;

                this.comboBoxes[attr.Name] = ruleTypeSelection;
                this.textBoxes[attr.Name] = textBox;

                this.Controls.Add(textLabel);
                this.Controls.Add(ruleTypeSelection);
                this.Controls.Add(textBox);

                i++;
            }

            // Przycisk Filtruj
            Button button = new Button();
            button.Text = "Filtruj";
            button.Location = new Point(0, i * (rowHeight + rowGap));
            button.Click += Button_Click;
            this.Controls.Add(button);
            // Dodanie możliwości zaakceptowania za pomocą Enter
            AcceptButton = button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            FilterRule filterRule = new FilterRule(this.listForm.dataTable.AttributeRow);
            comboBoxes.Keys.ToList()
                .FindAll(attributeName => !this.comboBoxes[attributeName].Text.Equals(String.Empty))
                .ForEach((attributeName =>
                {
                    Console.WriteLine(attributeName);
                    filterRule.AddPartialRule(attributeName,
                        new AttributePartialRule(comboBoxes[attributeName].Text, textBoxes[attributeName].Text));
                }));

            this.listForm.ApplyFilter(filterRule);
        }
    }
}
