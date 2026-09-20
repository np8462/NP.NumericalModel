using System;

namespace NP.NumericalModel.Geometry
{
    public class Line3D
    {
        public Point3D Start { get; private set; }
        public Point3D End { get; private set; }

        public double Length
        {
            get { return Start.DistanceTo(End); }
        }

        public Point3D Midpoint
        {
            get
            {
                return new Point3D(
                    (Start.X + End.X) / 2.0,
                    (Start.Y + End.Y) / 2.0,
                    (Start.Z + End.Z) / 2.0);
            }
        }

        public Line3D(Point3D start, Point3D end)
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
