using System;

namespace NP.NumericalModel.Geometry
{
    public class Line2D
    {
        public Point2D Start { get; private set; }
        public Point2D End { get; private set; }

        public double Length
        {
            get { return Start.DistanceTo(End); }
        }

        public Point2D Midpoint
        {
            get
            {
                return new Point2D(
                    (Start.X + End.X) / 2.0,
                    (Start.Y + End.Y) / 2.0);
            }
        }

        public Line2D(Point2D start, Point2D end)
        {
            if (start == null)
                throw new ArgumentNullException("start");

            if (end == null)
                throw new ArgumentNullException("end");

            Start = start;
            End = end;
        }
    }
}
