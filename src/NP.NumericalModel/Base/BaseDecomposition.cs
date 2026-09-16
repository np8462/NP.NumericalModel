using System;
using System.Collections.Generic;
using System.Numerics;

namespace NP.NumericalModel.Base
{
    /// <summary>
    /// Describes the positional decomposition of a number representation.
    /// For digits d at positions p, the contribution is d * B^p.
    /// </summary>
    public class BaseDecomposition
    {
        private readonly int[] digits;
        private readonly BigInteger[] contributions;

        public BaseSystem BaseSystem { get; private set; }

        public string Representation { get; private set; }

        public IList<int> Digits
        {
            get { return Array.AsReadOnly(digits); }
        }

        public IList<BigInteger> Contributions
        {
            get { return Array.AsReadOnly(contributions); }
        }

        public BigInteger DecimalValue { get; private set; }

        public BaseDecomposition(
            string representation,
            BaseSystem baseSystem)
        {
            if (representation == null)
            {
                throw new ArgumentNullException("representation");
            }

            if (baseSystem == null)
            {
                throw new ArgumentNullException("baseSystem");
            }

            if (representation.Length == 0)
            {
                throw new ArgumentException(
                    "Representation cannot be empty.",
                    "representation");
            }

            Representation = representation;
            BaseSystem = baseSystem;

            List<int> digitList = new List<int>();
            List<BigInteger> contributionList =
                new List<BigInteger>();

            BigInteger value = BigInteger.Zero;

            int index;

            for (index = 0;
                 index < representation.Length;
                 index++)
            {
                int digit =
                    BaseConverter.SymbolToDigit(
                        representation[index]);

                if (!baseSystem.IsValidDigit(digit))
                {
                    throw new ArgumentException(
                        "Digit is not valid for the supplied base.",
                        "representation");
                }

                digitList.Add(digit);
            }

            for (index = 0;
                 index < digitList.Count;
                 index++)
            {
                int power =
                    digitList.Count - 1 - index;

                BigInteger contribution =
                    digitList[index] *
                    BigInteger.Pow(
                        baseSystem.Base,
                        power);

                contributionList.Add(contribution);

                value += contribution;
            }

            digits = digitList.ToArray();
            contributions = contributionList.ToArray();
            DecimalValue = value;
        }

        public string GetFormula()
        {
            int index;
            string result = "";

            for (index = 0;
                 index < digits.Length;
                 index++)
            {
                if (index > 0)
                {
                    result += " + ";
                }

                result +=
                    digits[index].ToString()
                    + "*"
                    + BaseSystem.Base.ToString()
                    + "^"
                    + (digits.Length - 1 - index).ToString();
            }

            return result;
        }
    }
}