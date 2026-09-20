using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class RatioComposerTests
    {
        [TestMethod]
        public void CombinesFortyTwoOverSevenAndTwoOverSeven()
        {
            RatioComposer composer = new RatioComposer();

            Ratio result = composer.Add(
                new Ratio(42, 7),
                new Ratio(2, 7));

            Assert.AreEqual(44L, result.Numerator);
            Assert.AreEqual(49L, result.Denominator);
        }

        [TestMethod]
        public void CombinesFortyTwoOverSevenAndTwoOverSevenAsReducedRatio()
        {
            RatioComposer composer = new RatioComposer();

            Ratio result = composer.Add(
                new Ratio(42, 7),
                new Ratio(2, 7));

            Assert.AreEqual(44L, result.Numerator);
            Assert.AreEqual(7L, result.Denominator);
        }

        [TestMethod]
        public void CombinesRatiosWithDifferentDenominators()
        {
            RatioComposer composer = new RatioComposer();

            Ratio result = composer.Add(
                new Ratio(1, 2),
                new Ratio(1, 3));

            Assert.AreEqual(5L, result.Numerator);
            Assert.AreEqual(6L, result.Denominator);
        }

        [TestMethod]
        public void CombinesMultipleComponents()
        {
            RatioComposer composer = new RatioComposer();

            Ratio result = composer.Combine(
                new Ratio(42, 7),
                new Ratio(2, 7),
                new Ratio(1, 7));

            Assert.AreEqual(45L, result.Numerator);
            Assert.AreEqual(7L, result.Denominator);
        }

        [TestMethod]
        public void RejectsNullComponent()
        {
            RatioComposer composer = new RatioComposer();

            try
            {
                composer.Add(null, new Ratio(2, 7));
                Assert.Fail("A null ratio component must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsEmptyCombination()
        {
            RatioComposer composer = new RatioComposer();

            try
            {
                composer.Combine();
                Assert.Fail("An empty ratio combination must be rejected.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
