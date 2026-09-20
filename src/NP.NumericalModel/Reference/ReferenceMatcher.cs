using System;

namespace NP.NumericalModel.Reference
{
    /// <summary>
    /// Compares a computed numerical value with an independently supplied reference.
    /// </summary>
    public class ReferenceMatcher
    {
        public VerificationResult Compare(
            string referenceName,
            double computedValue,
            double referenceValue,
            double tolerance)
        {
            if (referenceName == null) throw new ArgumentNullException("referenceName");
            if (tolerance < 0) throw new ArgumentOutOfRangeException("tolerance");

            double difference = Math.Abs(computedValue - referenceValue);
            double relativeError;

            if (referenceValue == 0.0)
                relativeError = difference;
            else
                relativeError = difference / Math.Abs(referenceValue);

            VerificationClassification classification;

            if (difference == 0.0)
                classification = VerificationClassification.Exact;
            else if (difference <= tolerance)
                classification = VerificationClassification.Approximate;
            else
                classification = VerificationClassification.NoMatch;

            return new VerificationResult(
                referenceName,
                computedValue,
                referenceValue,
                difference,
                relativeError,
                classification);
        }
    }
}
