using System;
using System.Numerics;
using System.Text;

namespace NP.NumericalModel.Base
{
    /// <summary>
    /// Converts positional integer representations between numeric bases.
    /// The conversion is purely mathematical; semantic interpretations are
    /// intentionally handled outside this class.
    /// </summary>
    public static class BaseConverter
    {
        public static BigInteger ToDecimal(string representation, BaseSystem sourceBase)
        {
            if (representation == null)
            {
                throw new ArgumentNullException("representation");
            }

            if (sourceBase == null)
            {
                throw new ArgumentNullException("sourceBase");
            }

            if (representation.Length == 0)
            {
                throw new ArgumentException("Representation cannot be empty.", "representation");
            }

            BigInteger value = BigInteger.Zero;
            int index;

            for (index = 0; index < representation.Length; index++)
            {
                int digit = SymbolToDigit(representation[index]);

                if (!sourceBase.IsValidDigit(digit))
                {
                    throw new ArgumentException(
                        "Digit is not valid for the supplied base.",
                        "representation");
                }

                value = value * sourceBase.Base + digit;
            }

            return value;
        }

        public static string FromDecimal(BigInteger value, BaseSystem targetBase)
        {
            if (targetBase == null)
            {
                throw new ArgumentNullException("targetBase");
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            if (value == BigInteger.Zero)
            {
                return "0";
            }

            StringBuilder result = new StringBuilder();
            BigInteger remaining = value;

            while (remaining > BigInteger.Zero)
            {
                BigInteger remainder = remaining % targetBase.Base;
                result.Insert(0, DigitToSymbol((int)remainder));
                remaining = remaining / targetBase.Base;
            }

            return result.ToString();
        }

        public static string Convert(
            string representation,
            BaseSystem sourceBase,
            BaseSystem targetBase)
        {
            BigInteger decimalValue = ToDecimal(representation, sourceBase);
            return FromDecimal(decimalValue, targetBase);
        }

        //private static int ParseDigit(char value)
        //{
        //    if (value >= '0' && value <= '9')
        //    {
        //        return value - '0';
        //    }

        //    if (value >= 'A' && value <= 'Z')
        //    {
        //        return value - 'A' + 10;
        //    }

        //    if (value >= 'a' && value <= 'z')
        //    {
        //        return value - 'a' + 10;
        //    }

        //    return -1;
        //}

        //private static char FormatDigit(int value)
        //{
        //    if (value >= 0 && value <= 9)
        //    {
        //        return (char)('0' + value);
        //    }

        //    if (value >= 10 && value <= 35)
        //    {
        //        return (char)('A' + value - 10);
        //    }

        //    throw new ArgumentOutOfRangeException("value");
        //}

        public static int SymbolToDigit(char symbol)
        {
            if (symbol >= '0' && symbol <= '9')
                return symbol - '0';

            if (symbol >= 'A' && symbol <= 'Z')
                return symbol - 'A' + 10;

            if (symbol >= 'a' && symbol <= 'z')
                return symbol - 'a' + 10;

            throw new ArgumentException("Invalid digit symbol.");
        }

        private static char DigitToSymbol(int digit)
        {
            if (digit < 10)
                return (char)('0' + digit);

            return (char)('A' + digit - 10);
        }

    }
}
