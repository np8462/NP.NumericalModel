using System;

namespace NP.NumericalModel
{
    public class ByteModel
    {
        private readonly byte value;

        public ByteModel(int value)
        {
            if (value < byte.MinValue || value > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException("value", "A byte value must be between 0 and 255.");
            }

            this.value = (byte)value;
        }

        public ByteModel(NibbleModel highNibble, NibbleModel lowNibble)
            : this(new NibblePair(highNibble, lowNibble).ToByte())
        {
        }

        public int Value
        {
            get { return value; }
        }

        public NibbleModel HighNibble
        {
            get { return new NibbleModel(value >> 4); }
        }

        public NibbleModel LowNibble
        {
            get { return new NibbleModel(value & 15); }
        }

        public NibblePair NibblePair
        {
            get { return new NibblePair(HighNibble, LowNibble); }
        }

        public static ByteModel FromNibbles(NibbleModel highNibble, NibbleModel lowNibble)
        {
            return new ByteModel(highNibble, lowNibble);
        }
    }
}
