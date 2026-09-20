using System;

namespace NP.NumericalModel.Geometry
{
    public class Point2D
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Point2D other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            double dx = other.X - X;
            double dy = other.Y - Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }
    }
}
