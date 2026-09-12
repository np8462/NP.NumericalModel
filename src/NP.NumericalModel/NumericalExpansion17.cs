using System;
using System.Numerics;

namespace NP.NumericalModel
{
    public class NumericalExpansion17
    {
        public const int NibbleCount = 17;

        private readonly ByteModel originalByte;
        private readonly NibbleModel[] nibbles;

        public NumericalExpansion17(ByteModel originalByte, int[] nibbleValues)
        {
            if (originalByte == null)
            {
                throw new ArgumentNullException("originalByte");
            }

            if (nibbleValues == null)
            {
                throw new ArgumentNullException("nibbleValues");
            }

            if (nibbleValues.Length != NibbleCount)
            {
                throw new ArgumentException("The expansion must contain exactly 17 nibble values.", "nibbleValues");
            }

            this.originalByte = originalByte;
            nibbles = new NibbleModel[NibbleCount];

            for (int index = 0; index < NibbleCount; index++)
            {
                nibbles[index] = new NibbleModel(nibbleValues[index]);
            }

            if (!IsSumEqualToOriginalByte())
            {
                throw new ArgumentException("The sum of the 17 nibble values must equal the original byte value.", "nibbleValues");
            }
        }

        public ByteModel OriginalByte
        {
            get { return originalByte; }
        }

        public int OriginalByteValue
        {
            get { return originalByte.Value; }
        }

        public NibbleModel GetNibble(int index)
        {
            if (index < 0 || index >= NibbleCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return nibbles[index];
        }

        public int[] GetNibbleValues()
        {
            int[] values = new int[NibbleCount];

            for (int index = 0; index < NibbleCount; index++)
            {
                values[index] = nibbles[index].Value;
            }

            return values;
        }

        public int GetSum()
        {
            int sum = 0;

            for (int index = 0; index < NibbleCount; index++)
            {
                sum += nibbles[index].Value;
            }

            return sum;
        }

        public bool IsSumEqualToOriginalByte()
        {
            return GetSum() == originalByte.Value;
        }

        public static BigInteger CalculateValidOrderedStateCount(ByteModel originalByte)
        {
            if (originalByte == null)
            {
                throw new ArgumentNullException("originalByte");
            }

            return CalculateValidOrderedStateCount(originalByte.Value);
        }

        public static BigInteger CalculateValidOrderedStateCount(int originalByteValue)
        {
            if (originalByteValue < byte.MinValue || originalByteValue > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException("originalByteValue");
            }

            BigInteger[] stateCounts = new BigInteger[originalByteValue + 1];
            stateCounts[0] = BigInteger.One;

            for (int nibbleIndex = 0; nibbleIndex < NibbleCount; nibbleIndex++)
            {
                BigInteger[] nextStateCounts = new BigInteger[originalByteValue + 1];

                for (int sum = 0; sum <= originalByteValue; sum++)
                {
                    for (int nibbleValue = 0; nibbleValue <= 15 && sum + nibbleValue <= originalByteValue; nibbleValue++)
                    {
                        nextStateCounts[sum + nibbleValue] += stateCounts[sum];
                    }
                }

                stateCounts = nextStateCounts;
            }

            return stateCounts[originalByteValue];
        }
    }
}
