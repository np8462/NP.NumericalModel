using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Geometry;
using NP.NumericalModel.Relation;
using NP.NumericalModel.Visualization;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class SvgGeometryRendererTests
    {
        [TestMethod]
        public void RendersGeometrySvg()
        {
            Point2D origin = new Point2D(0.0, 0.0);
            Circle circle = new Circle(origin, 3.0);
            CartesianPlane plane = new CartesianPlane(origin, 10.0, 10.0);
            Spiral spiral = new Spiral(origin, 3.0, 1.0);
            Line3D line = new Line3D(
                new Point3D(0.0, 0.0, 0.0),
                new Point3D(0.75, 1.0, 1.0));
            Ratio ratio = new Ratio(3, 4);

            string svg = SvgGeometryRenderer.RenderDemo(
                circle,
                plane,
                spiral,
                line,
                ratio,
                1100,
                800);

            Assert.IsTrue(svg.StartsWith("<svg"));
            Assert.IsTrue(svg.Contains("<circle"));
            Assert.IsTrue(svg.Contains("<path"));
            Assert.IsTrue(svg.Contains("3:4"));
            Assert.IsTrue(svg.Contains("π/4"));
            Assert.IsTrue(svg.Contains("3D"));
            Assert.IsTrue(svg.Contains("</svg>"));
        }

        [TestMethod]
        public void RejectsInvalidRendererArguments()
        {
            Point2D origin = new Point2D(0.0, 0.0);
            Circle circle = new Circle(origin, 3.0);
            CartesianPlane plane = new CartesianPlane(origin, 10.0, 10.0);
            Spiral spiral = new Spiral(origin, 3.0, 1.0);
            Line3D line = new Line3D(
                new Point3D(0.0, 0.0, 0.0),
                new Point3D(0.75, 1.0, 1.0));
            Ratio ratio = new Ratio(3, 4);

            try
            {
                SvgGeometryRenderer.RenderDemo(
                    null, plane, spiral, line, ratio, 100, 100);
                Assert.Fail("Null circle must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                SvgGeometryRenderer.RenderDemo(
                    circle, plane, spiral, line, ratio, 0, 100);
                Assert.Fail("Invalid width must be rejected.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
