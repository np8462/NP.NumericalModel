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

        public PatternMatch AnalyzeConsistency(
            Ratio ratio,
            DerivedRelation relation)
        {
            if (ratio == null) throw new ArgumentNullException("ratio");
            if (relation == null) throw new ArgumentNullException("relation");

            bool sameRatio =
                ratio.Numerator == relation.Numerator
                && ratio.Denominator == relation.Denominator;

            bool sameDecomposition =
                ratio.Quotient == relation.WholePart
                && ratio.Remainder == relation.Remainder;

            long reconstructed =
                relation.WholePart * relation.Denominator
                + relation.Remainder;

            bool validRemainder =
                Math.Abs(relation.Remainder) < Math.Abs(relation.Denominator);

            bool consistent =
                sameRatio
                && sameDecomposition
                && reconstructed == relation.Numerator
                && validRemainder;

            if (consistent)
            {
                return new PatternMatch(
                    "RatioDecompositionConsistency",
                    "The ratio, whole part, remainder, and reconstruction are mutually consistent.",
                    true);
            }

            return new PatternMatch(
                "RatioDecompositionConsistency",
                "The ratio, whole part, remainder, and reconstruction are not mutually consistent.",
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
