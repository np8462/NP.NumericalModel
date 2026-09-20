using System;

namespace NP.NumericalModel.Relation
{
    /// <summary>
    /// Represents a numeric ratio and exposes its quotient, remainder,
    /// decimal value, and exact reconstruction.
    /// </summary>
    public class Ratio
    {
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }

        public long Quotient { get { return Numerator / Denominator; } }
        public long Remainder { get { return Numerator % Denominator; } }
        public double DecimalValue { get { return (double)Numerator / (double)Denominator; } }
        public bool IsExact { get { return Remainder == 0; } }

        public Ratio(long numerator, long denominator)
        {
            if (denominator == 0) throw new DivideByZeroException();
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            Numerator = numerator;
            Denominator = denominator;
        }

        public long ReconstructNumerator()
        {
            return Quotient * Denominator + Remainder;
        }

        public override string ToString()
        {
            return Numerator.ToString() + ":" + Denominator.ToString();
        }
    }
}
