using System;
using NP.NumericalModel.Geometry;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.ConsoleSample
{
    public static class GeometryModelDemo
    {
        public static void Run()
        {
            Point2D origin = new Point2D(0.0, 0.0);
            CartesianPlane plane = new CartesianPlane(origin, 10.0, 10.0);

            Circle circle = new Circle(origin, 3.0);

            Ratio ratio = new Ratio(3, 4);

            Line3D line = new Line3D(
                new Point3D(0.0, 0.0, 0.0),
                new Point3D(ratio.DecimalValue, 1.0, 1.0));

            Spiral spiral = new Spiral(origin, 3.0, 1.0);
            Point2D spiralPoint = spiral.PointAt(Math.PI / 2.0);

            Console.WriteLine("Geometry Model Demo");
            Console.WriteLine("-------------------");
            Console.WriteLine("Cartesian origin : " + plane.Origin);
            Console.WriteLine("Circle radius    : " + circle.Radius);
            Console.WriteLine("Circle area      : " + circle.Area);
            Console.WriteLine("3:4 ratio        : " + ratio);
            Console.WriteLine("3:4 value        : " + ratio.DecimalValue);
            Console.WriteLine("3D line end      : " + line.End);
            Console.WriteLine("3D line length   : " + line.Length);
            Console.WriteLine("Spiral point     : " + spiralPoint);
            Console.WriteLine();
            Console.WriteLine("This demo models geometry only; it does not assign conceptual meaning to the numbers.");
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
