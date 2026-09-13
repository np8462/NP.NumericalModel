using System;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Represents one derived relation discovered during numerical analysis.
    ///
    /// A derivation preserves:
    /// source value,
    /// rule used,
    /// resulting value,
    /// and recursion depth.
    /// </summary>
    public class RelationDerivation
    {
        public int SourceValue { get; private set; }

        public string Rule { get; private set; }

        public int ResultValue { get; private set; }

        public int Depth { get; private set; }

        public RelationDerivation(
            int sourceValue,
            string rule,
            int resultValue,
            int depth)
        {
            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            SourceValue = sourceValue;
            Rule = rule;
            ResultValue = resultValue;
            Depth = depth;
        }

        public override string ToString()
        {
            return SourceValue.ToString()
                + " --"
                + Rule
                + "--> "
                + ResultValue.ToString();
        }
    }
}