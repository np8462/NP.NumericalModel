using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class RatioTests
    {
        [TestMethod]
        public void CanDivide()
        {
            Ratio ratio = new Ratio(42, 7);
            Assert.AreEqual(6L, ratio.Quotient);
            Assert.AreEqual(0L, ratio.Remainder);
            Assert.AreEqual(6.0, ratio.DecimalValue);
            Assert.IsTrue(ratio.IsExact);
        }

        [TestMethod]
        public void CanFindQuotientAndRemainder()
        {
            Ratio ratio = new Ratio(22, 7);
            Assert.AreEqual(3L, ratio.Quotient);
            Assert.AreEqual(1L, ratio.Remainder);
            Assert.IsFalse(ratio.IsExact);
        }

        [TestMethod]
        public void CanReconstructRatio()
        {
            Ratio ratio = new Ratio(22, 7);
            Assert.AreEqual(22L, ratio.ReconstructNumerator());
            Assert.AreEqual("22:7", ratio.ToString());
        }

        [TestMethod]
        public void RejectsZeroDenominator()
        {
            try
            {
                new Ratio(22, 0);
                Assert.Fail("A zero denominator must be rejected.");
            }
            catch (System.DivideByZeroException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void NormalizesNegativeDenominator()
        {
            Ratio ratio = new Ratio(22, -7);
            Assert.AreEqual(-22L, ratio.Numerator);
            Assert.AreEqual(7L, ratio.Denominator);
            Assert.AreEqual(-3L, ratio.Quotient);
            Assert.AreEqual(-1L, ratio.Remainder);
        }
    }
}
