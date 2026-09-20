using System;
using NP.NumericalModel.Reference;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Integration
{
    /// <summary>
    /// Integrates ratio decomposition with reference verification.
    /// </summary>
    public class RatioVerification
    {
        private readonly ReferenceMatcher referenceMatcher;

        public RatioVerification()
        {
            referenceMatcher = new ReferenceMatcher();
        }

        public RatioVerificationResult Verify(
            Ratio ratio,
            string referenceName,
            double referenceValue,
            double tolerance)
        {
            if (ratio == null) throw new ArgumentNullException("ratio");

            RatioDecomposition decomposition =
                new RatioDecomposition(ratio);

            VerificationResult verification =
                referenceMatcher.Compare(
                    referenceName,
                    decomposition.DecimalValue,
                    referenceValue,
                    tolerance);

            return new RatioVerificationResult(
                ratio,
                decomposition,
                verification);
        }
    }
}
