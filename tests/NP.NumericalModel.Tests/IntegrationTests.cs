using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Integration;
using NP.NumericalModel.Reference;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void CanIntegrateTwentyTwoOverSevenWithPi()
        {
            Ratio ratio = new Ratio(22, 7);
            RatioVerification verification = new RatioVerification();

            RatioVerificationResult result =
                verification.Verify(
                    ratio,
                    "Pi",
                    Math.PI,
                    0.002);

            Assert.AreSame(ratio, result.Ratio);
            Assert.AreEqual(3L, result.Decomposition.WholePart);
            Assert.AreEqual(1L, result.Decomposition.Remainder);
            Assert.AreEqual("3 + 1/7", result.Decomposition.ToMixedNumberString());
            Assert.AreEqual(
                VerificationClassification.Approximate,
                result.Verification.Classification);
        }

        [TestMethod]
        public void CanIntegrateExactDivisionWithSix()
        {
            RatioVerification verification = new RatioVerification();

            RatioVerificationResult result =
                verification.Verify(
                    new Ratio(42, 7),
                    "Six",
                    6.0,
                    0.001);

            Assert.AreEqual(6L, result.Decomposition.WholePart);
            Assert.AreEqual(0L, result.Decomposition.Remainder);
            Assert.IsTrue(result.Decomposition.IsExact);
            Assert.AreEqual(
                VerificationClassification.Exact,
                result.Verification.Classification);
        }

        [TestMethod]
        public void CanIntegrateFiveOverThree()
        {
            RatioVerification verification = new RatioVerification();

            RatioVerificationResult result =
                verification.Verify(
                    new Ratio(5, 3),
                    "Five Thirds",
                    5.0 / 3.0,
                    0.0);

            Assert.AreEqual(1L, result.Decomposition.WholePart);
            Assert.AreEqual(2L, result.Decomposition.Remainder);
            Assert.AreEqual(
                VerificationClassification.Exact,
                result.Verification.Classification);
        }

        [TestMethod]
        public void RejectsNullRatio()
        {
            RatioVerification verification = new RatioVerification();

            try
            {
                verification.Verify(null, "Six", 6.0, 0.001);
                Assert.Fail("A null ratio must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
