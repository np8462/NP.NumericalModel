using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Geometry;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class GeometryTests
    {
        [TestMethod]
        public void Point2DDistanceIsCalculated()
        {
            Point2D a = new Point2D(0.0, 0.0);
            Point2D b = new Point2D(3.0, 4.0);

            Assert.AreEqual(5.0, a.DistanceTo(b), 0.0000000001);
        }

        [TestMethod]
        public void Point3DDistanceIsCalculated()
        {
            Point3D a = new Point3D(0.0, 0.0, 0.0);
            Point3D b = new Point3D(2.0, 3.0, 6.0);

            Assert.AreEqual(7.0, a.DistanceTo(b), 0.0000000001);
        }

        [TestMethod]
        public void Line2DProvidesLengthAndMidpoint()
        {
            Line2D line = new Line2D(
                new Point2D(0.0, 0.0),
                new Point2D(4.0, 2.0));

            Assert.AreEqual(Math.Sqrt(20.0), line.Length, 0.0000000001);
            Assert.AreEqual(2.0, line.Midpoint.X, 0.0000000001);
            Assert.AreEqual(1.0, line.Midpoint.Y, 0.0000000001);
        }

        [TestMethod]
        public void Line3DProvidesLengthAndMidpoint()
        {
            Line3D line = new Line3D(
                new Point3D(0.0, 0.0, 0.0),
                new Point3D(2.0, 3.0, 6.0));

            Assert.AreEqual(7.0, line.Length, 0.0000000001);
            Assert.AreEqual(1.0, line.Midpoint.X, 0.0000000001);
            Assert.AreEqual(1.5, line.Midpoint.Y, 0.0000000001);
            Assert.AreEqual(3.0, line.Midpoint.Z, 0.0000000001);
        }

        [TestMethod]
        public void CircleProvidesBasicGeometry()
        {
            Circle circle = new Circle(new Point2D(0.0, 0.0), 2.0);

            Assert.AreEqual(4.0, circle.Diameter, 0.0000000001);
            Assert.AreEqual(4.0 * Math.PI, circle.Circumference, 0.0000000001);
            Assert.AreEqual(4.0 * Math.PI, circle.Area, 0.0000000001);
            Assert.IsTrue(circle.Contains(new Point2D(1.0, 1.0)));
            Assert.IsFalse(circle.Contains(new Point2D(3.0, 0.0)));
        }

        [TestMethod]
        public void SpiralRadiusGrowsPerTurn()
        {
            Spiral spiral = new Spiral(
                new Point2D(0.0, 0.0),
                1.0,
                2.0);

            Assert.AreEqual(1.0, spiral.RadiusAt(0.0), 0.0000000001);
            Assert.AreEqual(3.0, spiral.RadiusAt(2.0 * Math.PI), 0.0000000001);
            Assert.AreEqual(5.0, spiral.RadiusAt(4.0 * Math.PI), 0.0000000001);
        }

        [TestMethod]
        public void SpiralPointAtQuarterTurnIsCorrect()
        {
            Spiral spiral = new Spiral(
                new Point2D(0.0, 0.0),
                1.0,
                0.0);

            Point2D point = spiral.PointAt(Math.PI / 2.0);

            Assert.AreEqual(0.0, point.X, 0.0000000001);
            Assert.AreEqual(1.0, point.Y, 0.0000000001);
        }

        [TestMethod]
        public void CartesianPlaneMapsCoordinatesFromOrigin()
        {
            CartesianPlane plane = new CartesianPlane(
                new Point2D(10.0, 20.0),
                100.0,
                80.0);

            Point2D point = plane.Point(3.0, -4.0);

            Assert.AreEqual(13.0, point.X, 0.0000000001);
            Assert.AreEqual(16.0, point.Y, 0.0000000001);
        }

        [TestMethod]
        public void RatioCanDriveAThreeDimensionalLine()
        {
            NP.NumericalModel.Relation.Ratio ratio =
                new NP.NumericalModel.Relation.Ratio(3, 4);

            Line3D line = new Line3D(
                new Point3D(0.0, 0.0, 0.0),
                new Point3D(
                    ratio.DecimalValue,
                    1.0,
                    1.0));

            Assert.AreEqual(0.75, line.End.X, 0.0000000001);
            Assert.AreEqual(Math.Sqrt(2.5625), line.Length, 0.0000000001);
        }

        [TestMethod]
        public void RejectsNegativeCircleRadius()
        {
            try
            {
                new Circle(new Point2D(0.0, 0.0), -1.0);
                Assert.Fail("Negative radius must be rejected.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsNullGeometryArguments()
        {
            try
            {
                new Line2D(null, new Point2D(1.0, 1.0));
                Assert.Fail("Null start point must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                new CartesianPlane(null, 10.0, 10.0);
                Assert.Fail("Null origin must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
