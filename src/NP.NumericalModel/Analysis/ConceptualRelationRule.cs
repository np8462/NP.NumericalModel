using System;

namespace NP.NumericalModel.Analysis
{
    public class ConceptualRelationRule
    {
        public string Name { get; private set; }

        public ConceptualRelationRule(
            string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public bool TryApply(
            int left,
            int right,
            out int conceptualResult)
        {
            conceptualResult = 0;

            /*
             * نخستین قاعده مفهومی مدل:
             *
             * 3:3 -> 4
             *
             * این جمع استاندارد ریاضی نیست.
             * 4 در اینجا نتیجه Conceptual Aggregation است.
             */
            if (left == 3 && right == 3)
            {
                conceptualResult = 4;
                return true;
            }

            return false;
        }
    }
}