using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model.Filter
{
    class AttributePartialRule
    {
        public Func<AttributeValue, bool> PartialRule { get; }

        public AttributePartialRule(Func<AttributeValue, bool> partialRule)
        {
            PartialRule = partialRule;
        }

        public AttributePartialRule(string ruleType, string value)
        {
            Console.WriteLine("PArtial rule filter type: " + ruleType);
            switch (ruleType)
            {
                case FilterRuleType.EqualRule:
                    PartialRule = (AttributeValue v) => v.ToString().Equals(value);
                       break;
                case FilterRuleType.GreaterThanRule:
                    PartialRule = (AttributeValue v) => v.ToString().CompareTo(value) == 1;
                    break;
                case FilterRuleType.LesserThanRule:
                    PartialRule = (AttributeValue v) => v.ToString().CompareTo(value) == -1;
                    break;
                case FilterRuleType.NotEqualThanRule:
                    PartialRule = (AttributeValue v) => !v.ToString().Equals(value);
                    break;
            }
            
        }

        public bool Filter(AttributeValue value)
        {
            if (PartialRule != null) return PartialRule(value);
            else return true;
        }
    }
}
