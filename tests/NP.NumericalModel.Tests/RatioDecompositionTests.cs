using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class RatioDecompositionTests
    {
        [TestMethod]
        public void CanDecomposeTwentyTwoOverSeven()
        {
            RatioDecomposition decomposition =
                new RatioDecomposition(new Ratio(22, 7));

            Assert.AreEqual(3L, decomposition.WholePart);
            Assert.AreEqual(1L, decomposition.Remainder);
            Assert.AreEqual(1L, decomposition.RemainderRatio.Numerator);
            Assert.AreEqual(7L, decomposition.RemainderRatio.Denominator);
        }

        [TestMethod]
        public void CanRepresentExactDivision()
        {
            RatioDecomposition decomposition =
                new RatioDecomposition(new Ratio(42, 7));

            Assert.AreEqual(6L, decomposition.WholePart);
            Assert.AreEqual(0L, decomposition.Remainder);
            Assert.IsTrue(decomposition.IsExact);
            Assert.AreEqual("6", decomposition.ToMixedNumberString());
        }

        [TestMethod]
        public void CanDecomposeFiveOverThree()
        {
            RatioDecomposition decomposition =
                new RatioDecomposition(new Ratio(5, 3));

            Assert.AreEqual(1L, decomposition.WholePart);
            Assert.AreEqual(2L, decomposition.Remainder);
            Assert.AreEqual(2L, decomposition.RemainderRatio.Numerator);
            Assert.AreEqual(3L, decomposition.RemainderRatio.Denominator);
        }

        [TestMethod]
        public void CanRepresentMixedNumber()
        {
            RatioDecomposition decomposition =
                new RatioDecomposition(new Ratio(22, 7));

            Assert.AreEqual("3 + 1/7", decomposition.ToMixedNumberString());
            Assert.AreEqual("3 + 1/7", decomposition.ToString());
        }

        [TestMethod]
        public void CanReconstructOriginalNumerator()
        {
            RatioDecomposition decomposition =
                new RatioDecomposition(new Ratio(22, 7));

            Assert.AreEqual(22L, decomposition.ReconstructNumerator());
            Assert.AreEqual(22.0 / 7.0, decomposition.DecimalValue, 0.000000000000001);
        }
    }
}
