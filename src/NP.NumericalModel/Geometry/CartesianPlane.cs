using System;

namespace NP.NumericalModel.Geometry
{
    public class CartesianPlane
    {
        public Point2D Origin { get; private set; }
        public double XAxisLength { get; private set; }
        public double YAxisLength { get; private set; }

        public CartesianPlane(
            Point2D origin,
            double xAxisLength,
            double yAxisLength)
        {
            if (origin == null)
                throw new ArgumentNullException("origin");

            if (xAxisLength < 0.0)
                throw new ArgumentOutOfRangeException("xAxisLength");

            if (yAxisLength < 0.0)
                throw new ArgumentOutOfRangeException("yAxisLength");

            Origin = origin;
            XAxisLength = xAxisLength;
            YAxisLength = yAxisLength;
        }

        public Point2D Point(double x, double y)
        {
            return new Point2D(
                Origin.X + x,
                Origin.Y + y);
        }
    }
}
