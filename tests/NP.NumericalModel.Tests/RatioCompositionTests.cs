using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class RatioCompositionTests
    {
        [TestMethod]
        public void PreservesFortyTwoAndTwoComponents()
        {
            Ratio first = new Ratio(42, 7);
            Ratio second = new Ratio(2, 7);

            RatioComposition composition =
                new RatioComposition(first, second);

            Assert.AreEqual(2, composition.Components.Count);
            Assert.AreSame(first, composition.Components[0]);
            Assert.AreSame(second, composition.Components[1]);
        }

        [TestMethod]
        public void ProducesCombinedResult()
        {
            RatioComposition composition =
                new RatioComposition(
                    new Ratio(42, 7),
                    new Ratio(2, 7));

            Assert.AreEqual(44L, composition.Result.Numerator);
            Assert.AreEqual(7L, composition.Result.Denominator);
        }

        [TestMethod]
        public void KeepsComponentHistoryDifferentFromResult()
        {
            Ratio first = new Ratio(42, 7);
            Ratio second = new Ratio(2, 7);

            RatioComposition composition =
                new RatioComposition(first, second);

            Assert.AreNotSame(first, composition.Result);
            Assert.AreNotSame(second, composition.Result);
            Assert.AreEqual(42L, composition.Components[0].Numerator);
            Assert.AreEqual(2L, composition.Components[1].Numerator);
            Assert.AreEqual(44L, composition.Result.Numerator);
        }

        [TestMethod]
        public void ContainsRecognizesEquivalentRatio()
        {
            RatioComposition composition =
                new RatioComposition(
                    new Ratio(42, 7),
                    new Ratio(2, 7));

            Assert.IsTrue(composition.Contains(new Ratio(42, 7)));
            Assert.IsTrue(composition.Contains(new Ratio(2, 7)));
            Assert.IsFalse(composition.Contains(new Ratio(1, 7)));
        }

        [TestMethod]
        public void RejectsInvalidComponents()
        {
            try
            {
                new RatioComposition(
                    new Ratio(42, 7),
                    null);

                Assert.Fail("A null ratio component must be rejected.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
