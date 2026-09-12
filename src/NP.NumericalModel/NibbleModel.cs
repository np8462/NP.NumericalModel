using System;

namespace NP.NumericalModel
{
    public class NibbleModel
    {
        private readonly int value;

        public NibbleModel(int value)
        {
            if (value < 0 || value > 15)
            {
                throw new ArgumentOutOfRangeException("value", "A nibble value must be between 0 and 15.");
            }

            this.value = value;
        }

        public int Value
        {
            get { return value; }
        }

        public string HexValue
        {
            get { return value.ToString("X"); }
        }

        public bool IsQuantitative
        {
            get { return value >= 0 && value <= 9; }
        }

        public bool IsQualitative
        {
            get { return value >= 10 && value <= 15; }
        }
    }
}
