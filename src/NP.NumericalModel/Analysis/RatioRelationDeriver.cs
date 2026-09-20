using System;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Derives a relation from an existing ratio decomposition.
    /// </summary>
    public class RatioRelationDeriver
    {
        public DerivedRelation Derive(Ratio ratio)
        {
            if (ratio == null) throw new ArgumentNullException("ratio");

            RatioDecomposition decomposition =
                new RatioDecomposition(ratio);

            return new DerivedRelation(
                ratio.Numerator,
                ratio.Denominator,
                decomposition.WholePart,
                decomposition.Remainder);
        }
    }
}
