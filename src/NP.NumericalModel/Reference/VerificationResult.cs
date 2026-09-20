namespace NP.NumericalModel.Reference
{
    /// <summary>
    /// Stores the result of comparing a computed value with a reference value.
    /// </summary>
    public class VerificationResult
    {
        public string ReferenceName { get; private set; }
        public double ComputedValue { get; private set; }
        public double ReferenceValue { get; private set; }
        public double Difference { get; private set; }
        public double RelativeError { get; private set; }
        public VerificationClassification Classification { get; private set; }

        public bool IsMatch
        {
            get { return Classification != VerificationClassification.NoMatch; }
        }

        public VerificationResult(
            string referenceName,
            double computedValue,
            double referenceValue,
            double difference,
            double relativeError,
            VerificationClassification classification)
        {
            ReferenceName = referenceName;
            ComputedValue = computedValue;
            ReferenceValue = referenceValue;
            Difference = difference;
            RelativeError = relativeError;
            Classification = classification;
        }
    }
}
