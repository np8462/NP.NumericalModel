using System;

namespace NP.NumericalModel.Geometry
{
    public class Point3D
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double DistanceTo(Point3D other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            double dx = other.X - X;
            double dy = other.Y - Y;
            double dz = other.Z - Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
        }
    }
}
