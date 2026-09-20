using NP.NumericalModel.Reference;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Integration
{
    /// <summary>
    /// Contains the complete result of verifying a ratio.
    /// </summary>
    public class RatioVerificationResult
    {
        public Ratio Ratio { get; private set; }
        public RatioDecomposition Decomposition { get; private set; }
        public VerificationResult Verification { get; private set; }

        public RatioVerificationResult(
            Ratio ratio,
            RatioDecomposition decomposition,
            VerificationResult verification)
        {
            Ratio = ratio;
            Decomposition = decomposition;
            Verification = verification;
        }
    }
}
