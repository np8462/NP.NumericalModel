using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Reference;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class ReferenceTests
    {
        [TestMethod]
        public void CompareWithPi()
        {
            Ratio ratio = new Ratio(22, 7);
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "Pi",
                ratio.DecimalValue,
                Math.PI,
                0.002);

            Assert.AreEqual(VerificationClassification.Approximate, result.Classification);
            Assert.IsTrue(result.IsMatch);
            Assert.IsTrue(result.Difference > 0.0);
        }

        [TestMethod]
        public void CompareWithE()
        {
            Ratio ratio = new Ratio(19, 7);
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "e",
                ratio.DecimalValue,
                Math.E,
                0.01);

            Assert.AreEqual(VerificationClassification.Approximate, result.Classification);
            Assert.IsTrue(result.IsMatch);
        }

        [TestMethod]
        public void CompareWithGoldenRatio()
        {
            Ratio ratio = new Ratio(89, 55);
            double goldenRatio = (1.0 + Math.Sqrt(5.0)) / 2.0;
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "Golden Ratio",
                ratio.DecimalValue,
                goldenRatio,
                0.001);

            Assert.AreEqual(VerificationClassification.Approximate, result.Classification);
            Assert.IsTrue(result.IsMatch);
        }

        [TestMethod]
        public void ExactReferenceIsClassifiedAsExact()
        {
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "Six",
                6.0,
                6.0,
                0.001);

            Assert.AreEqual(VerificationClassification.Exact, result.Classification);
            Assert.AreEqual(0.0, result.Difference);
            Assert.AreEqual(0.0, result.RelativeError);
        }

        [TestMethod]
        public void ValueOutsideToleranceIsNoMatch()
        {
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "Pi",
                3.0,
                Math.PI,
                0.001);

            Assert.AreEqual(VerificationClassification.NoMatch, result.Classification);
            Assert.IsFalse(result.IsMatch);
        }
    }
}
