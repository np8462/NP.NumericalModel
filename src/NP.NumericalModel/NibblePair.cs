using System;

namespace NP.NumericalModel
{
    public class NibblePair
    {
        private readonly NibbleModel highNibble;
        private readonly NibbleModel lowNibble;

        public NibblePair(int highNibble, int lowNibble)
            : this(new NibbleModel(highNibble), new NibbleModel(lowNibble))
        {
        }

        public NibblePair(NibbleModel highNibble, NibbleModel lowNibble)
        {
            if (highNibble == null)
            {
                throw new ArgumentNullException("highNibble");
            }

            if (lowNibble == null)
            {
                throw new ArgumentNullException("lowNibble");
            }

            this.highNibble = highNibble;
            this.lowNibble = lowNibble;
        }

        public NibbleModel HighNibble
        {
            get { return highNibble; }
        }

        public NibbleModel LowNibble
        {
            get { return lowNibble; }
        }

        public byte ToByte()
        {
            return (byte)((highNibble.Value << 4) | lowNibble.Value);
        }

        public static NibblePair FromByte(byte value)
        {
            return new NibblePair(
                new NibbleModel(value >> 4),
                new NibbleModel(value & 15));
        }
    }
}
