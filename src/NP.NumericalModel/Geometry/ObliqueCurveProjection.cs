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

            // Angle is measured from the vertical Y axis toward Z.
            // At 90 degrees, Z projects exactly along the old X direction.
            // Side chooses the +1/-1 projected region.
            double screenX = side * depth * Math.Sin(radians);
            double screenY = -point.Y + side * depth * Math.Cos(radians);

            return new Point2D(screenX, screenY);
        }

        public Point2D Project(Point2D point)
        {
            return Project(MapTo3D(point));
        }
    }
}
