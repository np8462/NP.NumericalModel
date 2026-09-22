using System;

namespace NP.NumericalModel.Geometry
{
    public class ObliqueProjectionSettings
    {
        private double angle;

        public double Angle
        {
            get { return angle; }
            set
            {
                if (value < 0.0 || value > 180.0)
                    throw new ArgumentOutOfRangeException("value", "Angle must be between 0 and 180 degrees.");

                angle = value;
            }
        }

        public int Side { get; set; }

        public double DepthScale { get; set; }

        public ObliqueProjectionSettings()
        {
            angle = 90.0;
            Side = 1;
            DepthScale = 1.0;
        }

        public ObliqueProjectionSettings(double angle, int side, double depthScale)
        {
            Angle = angle;

            if (side != -1 && side != 1)
                throw new ArgumentOutOfRangeException("side", "Side must be -1 or +1.");

            if (depthScale < 0.0)
                throw new ArgumentOutOfRangeException("depthScale", "DepthScale cannot be negative.");

            Side = side;
            DepthScale = depthScale;
        }
    }
}
