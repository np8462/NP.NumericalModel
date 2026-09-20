using System;

namespace NP.NumericalModel.Relation
{
    /// <summary>
    /// Combines ratios as rational numeric components without assigning
    /// conceptual meaning to the components.
    /// </summary>
    public class RatioComposer
    {
        public Ratio Add(Ratio left, Ratio right)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");

            long numerator =
                left.Numerator * right.Denominator
                + right.Numerator * left.Denominator;

            long denominator =
                left.Denominator * right.Denominator;

            long gcd = GreatestCommonDivisor(numerator, denominator);

            numerator /= gcd;
            denominator /= gcd;

            return new Ratio(numerator, denominator);
        }

        public Ratio Combine(params Ratio[] components)
        {
            if (components == null)
                throw new ArgumentNullException("components");

            if (components.Length == 0)
                throw new ArgumentException(
                    "At least one ratio component is required.",
                    "components");

            Ratio result = components[0];

            if (result == null)
                throw new ArgumentException(
                    "Ratio components cannot be null.",
                    "components");

            int i;
            for (i = 1; i < components.Length; i++)
            {
                if (components[i] == null)
                    throw new ArgumentException(
                        "Ratio components cannot be null.",
                        "components");

                result = Add(result, components[i]);
            }

            return result;
        }

        private long GreatestCommonDivisor(long left, long right)
        {
            left = Math.Abs(left);
            right = Math.Abs(right);

            while (right != 0)
            {
                long remainder = left % right;
                left = right;
                right = remainder;
            }

            return left;
        }
    }
}
