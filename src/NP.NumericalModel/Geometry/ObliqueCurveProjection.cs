using System;

namespace NP.NumericalModel.Geometry
{
    public class ObliqueCurveProjection
    {
        private readonly ObliqueProjectionSettings settings;

        public ObliqueProjectionSettings Settings
        {
            get { return settings; }
        }

        public ObliqueCurveProjection(ObliqueProjectionSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            this.settings = settings;
        }

        public Point3D MapTo3D(Point2D point)
        {
            if (point == null)
                throw new ArgumentNullException("point");

            // The existing 2D X coordinate becomes the 3D Z coordinate.
            // The existing Y coordinate remains the vertical coordinate.
            return new Point3D(0.0, point.Y, point.X);
        }

        public Point2D Project(Point3D point)
        {
            if (point == null)
                throw new ArgumentNullException("point");

            double radians = settings.Angle * Math.PI / 180.0;
            double side = settings.Side;
            double depth = settings.DepthScale * point.Z;

            // Angle is measured from the vertical Y axis toward the
            // projected Z direction. This returns mathematical 2D
            // coordinates; the renderer can later map Y to screen pixels.
            //
            // At 90 degrees, Z projects exactly along the old X direction
            // and Y remains unchanged.
            double projectedX = side * depth * Math.Sin(radians);
            double projectedY = point.Y + side * depth * Math.Cos(radians);

            return new Point2D(projectedX, projectedY);
        }

        public Point2D Project(Point2D point)
        {
            return Project(MapTo3D(point));
        }
    }
}
