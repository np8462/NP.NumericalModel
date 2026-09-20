using System;

namespace NP.NumericalModel.Relation
{
    /// <summary>
    /// Decomposes a ratio into its whole part and remainder ratio.
    /// </summary>
    public class RatioDecomposition
    {
        public Ratio Ratio { get; private set; }

        public long WholePart
        {
            get { return Ratio.Quotient; }
        }

        public long Remainder
        {
            get { return Ratio.Remainder; }
        }

        public Ratio RemainderRatio { get; private set; }

        public bool IsExact
        {
            get { return Ratio.IsExact; }
        }

        public double DecimalValue
        {
            get { return Ratio.DecimalValue; }
        }

        public RatioDecomposition(Ratio ratio)
        {
            if (ratio == null) throw new ArgumentNullException("ratio");

            Ratio = ratio;
            RemainderRatio = new Ratio(ratio.Remainder, ratio.Denominator);
        }

        public long ReconstructNumerator()
        {
            return WholePart * Ratio.Denominator + RemainderRatio.Numerator;
        }

        public string ToMixedNumberString()
        {
            if (IsExact)
                return WholePart.ToString();

            return WholePart.ToString()
                + " + "
                + RemainderRatio.Numerator.ToString()
                + "/"
                + RemainderRatio.Denominator.ToString();
        }

        public override string ToString()
        {
            return ToMixedNumberString();
        }
    }
}
