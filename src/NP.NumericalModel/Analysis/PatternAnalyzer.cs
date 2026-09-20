using System;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Detects simple structural patterns from derived ratio relations.
    /// </summary>
    public class PatternAnalyzer
    {
        public PatternMatch Analyze(DerivedRelation relation)
        {
            if (relation == null) throw new ArgumentNullException("relation");

            if (relation.IsExact)
            {
                return new PatternMatch(
                    "ExactDivision",
                    "The ratio divides without a remainder.",
                    true);
            }

            return new PatternMatch(
                "RemainderDivision",
                "The ratio contains a whole part and a non-zero remainder.",
                true);
        }

        public PatternMatch AnalyzeReconstruction(DerivedRelation relation)
        {
            if (relation == null) throw new ArgumentNullException("relation");

            long reconstructed =
                relation.WholePart * relation.Denominator
                + relation.Remainder;

            bool valid =
                reconstructed == relation.Numerator
                && Math.Abs(relation.Remainder) < Math.Abs(relation.Denominator);

            if (valid)
            {
                return new PatternMatch(
                    "NumeratorReconstruction",
                    "The whole part and remainder reconstruct the original numerator.",
                    true);
            }

            return new PatternMatch(
                "NumeratorReconstruction",
                "The whole part and remainder do not reconstruct the original numerator.",
                false);
        }

        public PatternMatch Analyze(Ratio ratio)
        {
            if (ratio == null) throw new ArgumentNullException("ratio");

            RatioRelationDeriver deriver = new RatioRelationDeriver();
            return Analyze(deriver.Derive(ratio));
        }
    }
}
