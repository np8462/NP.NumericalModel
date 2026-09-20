using System;

namespace NP.NumericalModel.Geometry
{
    public class Spiral
    {
        public Point2D Center { get; private set; }
        public double StartRadius { get; private set; }
        public double GrowthPerTurn { get; private set; }

        public Spiral(Point2D center, double startRadius, double growthPerTurn)
        {
            if (center == null)
                throw new ArgumentNullException("center");

            if (startRadius < 0.0)
                throw new ArgumentOutOfRangeException("startRadius");

            Center = center;
            StartRadius = startRadius;
            GrowthPerTurn = growthPerTurn;
        }

        public double RadiusAt(double angle)
        {
            return StartRadius + GrowthPerTurn * angle / (2.0 * Math.PI);
        }

        public Point2D PointAt(double angle)
        {
            double radius = RadiusAt(angle);

            return new Point2D(
                Center.X + radius * Math.Cos(angle),
                Center.Y + radius * Math.Sin(angle));
        }
    }
}
