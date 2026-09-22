using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Geometry;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class ObliqueCurveProjectionTests
    {
        [TestMethod]
        public void MapsExistingXCoordinateToZ()
        {
            ObliqueCurveProjection projection =
                new ObliqueCurveProjection(new ObliqueProjectionSettings(90.0, 1, 1.0));

            Point3D point = projection.MapTo3D(new Point2D(3.0, 2.0));

            Assert.AreEqual(0.0, point.X, 0.0000000001);
            Assert.AreEqual(2.0, point.Y, 0.0000000001);
            Assert.AreEqual(3.0, point.Z, 0.0000000001);
        }

        [TestMethod]
        public void NinetyDegreesPreservesTwoDimensionalShape()
        {
            ObliqueCurveProjection projection =
                new ObliqueCurveProjection(new ObliqueProjectionSettings(90.0, 1, 1.0));

            Point2D point = projection.Project(new Point2D(3.0, 2.0));

            Assert.AreEqual(3.0, point.X, 0.0000000001);
            Assert.AreEqual(2.0, point.Y, 0.0000000001);
        }

        [TestMethod]
        public void OppositeSideReversesProjectedZDirection()
        {
            ObliqueCurveProjection projection =
                new ObliqueCurveProjection(new ObliqueProjectionSettings(90.0, -1, 1.0));

            Point2D point = projection.Project(new Point2D(3.0, 2.0));

            Assert.AreEqual(-3.0, point.X, 0.0000000001);
            Assert.AreEqual(2.0, point.Y, 0.0000000001);
        }

        [TestMethod]
        public void AngleChangesProjectedZComponents()
        {
            ObliqueCurveProjection projection =
                new ObliqueCurveProjection(new ObliqueProjectionSettings(120.0, 1, 1.0));

            Point2D point = projection.Project(new Point2D(2.0, 4.0));

            Assert.AreEqual(Math.Sqrt(3.0), point.X, 0.0000000001);
            Assert.AreEqual(3.0, point.Y, 0.0000000001);
        }

        [TestMethod]
        public void DepthScaleControlsProjectedZLength()
        {
            ObliqueCurveProjection projection =
                new ObliqueCurveProjection(new ObliqueProjectionSettings(120.0, 1, 0.5));

            Point2D point = projection.Project(new Point2D(2.0, 4.0));

            Assert.AreEqual(Math.Sqrt(3.0) / 2.0, point.X, 0.0000000001);
            Assert.AreEqual(3.5, point.Y, 0.0000000001);
        }

        [TestMethod]
        public void RejectsInvalidProjectionSettings()
        {
            try
            {
                new ObliqueProjectionSettings(-1.0, 1, 1.0);
                Assert.Fail("Angle below zero must be rejected.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                new ObliqueProjectionSettings(90.0, 0, 1.0);
                Assert.Fail("Side must be -1 or +1.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                new ObliqueProjectionSettings(90.0, 1, -1.0);
                Assert.Fail("Negative depth scale must be rejected.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
