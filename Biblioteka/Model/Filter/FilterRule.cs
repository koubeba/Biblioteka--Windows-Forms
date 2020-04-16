using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model.Filter
{
    class FilterRule
    {
        private readonly AttributeRow attributeRow;
        private readonly Dictionary<string, AttributePartialRule> partialRules;

        public FilterRule(AttributeRow attributeRow)
        {
            this.attributeRow = attributeRow;
            partialRules = new Dictionary<string, AttributePartialRule>();
        }

        public void AddPartialRule(string key, AttributePartialRule rule)
        {
            partialRules[key] = rule;
        }

        public bool Filter(AttributeValueRow value)
        {
            if (attributeRow.Attributes.Length != value.AttributeValues.Length) return false;

            for (int index = 0; index < attributeRow.Attributes.Length; ++index)
            {
                // If there is a partial rule, apply
                string key = attributeRow.Attributes[index].Name;
                if (partialRules.ContainsKey(key) && !partialRules[key].Filter(value.AttributeValues[index]))
                    return false;
            }
            return true;
        }
    }
}
