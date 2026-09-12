using System;

namespace NP.NumericalModel
{
    public class ByteSample
    {
        private readonly ByteModel byteModel;

        public ByteSample(ByteModel byteModel)
        {
            if (byteModel == null)
            {
                throw new ArgumentNullException("byteModel");
            }

            this.byteModel = byteModel;
        }

        public ByteModel ByteModel
        {
            get { return byteModel; }
        }
    }
}
