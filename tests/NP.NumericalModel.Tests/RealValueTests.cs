using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Interpretation;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class RealValueTests
    {
        [TestMethod]
        public void RepresentsFortyFourOverSeven()
        {
            RealValue value =
                RealValue.FromRatio(new Ratio(44, 7));

            Assert.AreEqual(44.0 / 7.0, value.Value, 0.0000000001);
            Assert.AreEqual("44:7", value.Expression);
        }

        [TestMethod]
        public void RepresentsPiApproximation()
        {
            RealValue value =
                RealValue.FromExpression("22:7", 22.0 / 7.0);

            Assert.AreEqual(22.0 / 7.0, value.Value, 0.0000000001);
            Assert.AreEqual("22:7", value.Expression);
        }

        [TestMethod]
        public void PreservesHigherPrecisionPiApproximation()
        {
            RealValue value =
                RealValue.FromExpression("355:113", 355.0 / 113.0);

            Assert.AreEqual(355.0 / 113.0, value.Value, 0.0000000001);
            Assert.AreEqual("355:113", value.Expression);
        }

        [TestMethod]
        public void RepresentsArcToRadiusRelation()
        {
            RealValue value =
                RealValue.FromExpression("2*pi", 2.0 * Math.PI);

            Assert.AreEqual(2.0 * Math.PI, value.Value, 0.0000000001);
            Assert.AreEqual("2*pi", value.Expression);
        }

        [TestMethod]
        public void RepresentsRadianExpression()
        {
            RealValue value =
                RealValue.FromExpression("pi/2", Math.PI / 2.0);

            Assert.AreEqual(Math.PI / 2.0, value.Value, 0.0000000001);
            Assert.AreEqual("pi/2", value.Expression);
        }

        [TestMethod]
        public void AppliesGeneralNumericFunction()
        {
            RealValue angle =
                RealValue.FromExpression("1 degree", Math.PI / 180.0);

            RealValue sine =
                angle.Apply("sin", Math.Sin);

            Assert.AreEqual(Math.Sin(Math.PI / 180.0), sine.Value, 0.0000000001);
            Assert.AreEqual("sin(1 degree)", sine.Expression);
        }

        [TestMethod]
        public void RepresentsDecimalValue()
        {
            RealValue value =
                RealValue.FromExpression("6.28", 6.28);

            Assert.AreEqual(6.28, value.Value, 0.0000000001);
            Assert.AreEqual("6.28", value.Expression);
        }

        [TestMethod]
        public void RepresentsComposedExactValue()
        {
            RatioComposer composer = new RatioComposer();
            Ratio result = composer.Add(new Ratio(1, 2), new Ratio(1, 2));

            RealValue value = RealValue.FromRatio(result);

            Assert.AreEqual(1.0, value.Value, 0.0000000001);
            Assert.AreEqual("1:1", value.Expression);
        }

        [TestMethod]
        public void RejectsNullRatio()
        {
            try
            {
                RealValue.FromRatio(null);
                Assert.Fail("A null ratio must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsNullFunction()
        {
            RealValue value =
                RealValue.FromExpression("x", 1.0);

            try
            {
                value.Apply("f", null);
                Assert.Fail("A null function must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
