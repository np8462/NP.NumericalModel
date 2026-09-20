namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Represents a relation derived from a ratio and its decomposition.
    /// </summary>
    public class DerivedRelation
    {
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }
        public long WholePart { get; private set; }
        public long Remainder { get; private set; }

        public DerivedRelation(long numerator, long denominator, long wholePart, long remainder)
        {
            Numerator = numerator;
            Denominator = denominator;
            WholePart = wholePart;
            Remainder = remainder;
        }

        public bool IsExact
        {
            get { return Remainder == 0; }
        }

        public string ToStringValue()
        {
            if (IsExact)
                return WholePart.ToString();

            return WholePart.ToString() + " + "
                + Remainder.ToString() + "/" + Denominator.ToString();
        }

        public override string ToString()
        {
            return ToStringValue();
        }
    }
}
