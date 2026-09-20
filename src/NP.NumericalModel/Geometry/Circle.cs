using System;

namespace NP.NumericalModel.Geometry
{
    public class Circle
    {
        public Point2D Center { get; private set; }
        public double Radius { get; private set; }

        public double Diameter
        {
            get { return Radius * 2.0; }
        }

        public double Circumference
        {
            get { return 2.0 * Math.PI * Radius; }
        }

        public double Area
        {
            get { return Math.PI * Radius * Radius; }
        }

        public Circle(Point2D center, double radius)
        {
            if (center == null)
                throw new ArgumentNullException("center");

            if (radius < 0.0)
                throw new ArgumentOutOfRangeException("radius");

            Center = center;
            Radius = radius;
        }

        public bool Contains(Point2D point)
        {
            if (point == null)
                throw new ArgumentNullException("point");

            return Center.DistanceTo(point) <= Radius;
        }
    }
}
