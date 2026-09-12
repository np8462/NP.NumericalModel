using System;

namespace NP.NumericalModel
{
    public class ByteReader
    {
        private readonly byte[] values;
        private int position;
        private ByteModel currentByte;
        private ByteSample currentSample;

        public ByteReader(byte[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            this.values = (byte[])values.Clone();
        }

        public ByteModel CurrentByte
        {
            get { return currentByte; }
        }

        public ByteSample CurrentSample
        {
            get { return currentSample; }
        }

        public bool ReadNext()
        {
            if (position >= values.Length)
            {
                return false;
            }

            currentByte = new ByteModel(values[position]);
            currentSample = new ByteSample(currentByte);
            position++;
            return true;
        }
    }
}
