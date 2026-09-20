using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Interpretation;
using NP.NumericalModel.Reference;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    /// <summary>
    /// Phase 2 probes: exercise different example families through the
    /// existing public API before adding another core capability.
    /// </summary>
    [TestClass]
    public class GeneralizationByExamplesPhase2Tests
    {
        [TestMethod]
        public void RatioExample_ExactAndRemainder()
        {
            Ratio ratio = new Ratio(44, 7);
            RatioDecomposition decomposition = new RatioDecomposition(ratio);

            Assert.AreEqual(6L, decomposition.WholePart);
            Assert.AreEqual(2L, decomposition.Remainder);
            Assert.AreEqual(44L, decomposition.ReconstructNumerator());
        }

        [TestMethod]
        public void ApproximationExample_UsesExistingReferenceMatcher()
        {
            RealValue value = RealValue.FromRatio(new Ratio(22, 7));
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "pi",
                value.Value,
                Math.PI,
                0.002);

            Assert.AreEqual(
                VerificationClassification.Approximate,
                result.Classification);
        }

        [TestMethod]
        public void HigherPrecisionExample_UsesSameReferenceApi()
        {
            RealValue value = RealValue.FromRatio(new Ratio(355, 113));
            ReferenceMatcher matcher = new ReferenceMatcher();

            VerificationResult result = matcher.Compare(
                "pi",
                value.Value,
                Math.PI,
                0.0000001);

            Assert.AreEqual(
                VerificationClassification.Approximate,
                result.Classification);
        }

        [TestMethod]
        public void CompositionExample_UsesExistingRatioComposer()
        {
            RatioComposer composer = new RatioComposer();

            Ratio result = composer.Add(
                new Ratio(42, 7),
                new Ratio(2, 7));

            Assert.AreEqual(44L, result.Numerator);
            Assert.AreEqual(7L, result.Denominator);
        }

        [TestMethod]
        public void DecimalAndGeometryExamples_UseExistingRealValue()
        {
            RealValue decimalValue =
                RealValue.FromExpression("6.28", 6.28);

            RealValue arc =
                RealValue.FromExpression("2*pi", 2.0 * Math.PI);

            RealValue angle =
                RealValue.FromExpression("pi/2", Math.PI / 2.0);

            Assert.AreEqual(6.28, decimalValue.Value, 0.0000000001);
            Assert.AreEqual(2.0 * Math.PI, arc.Value, 0.0000000001);
            Assert.AreEqual(Math.PI / 2.0, angle.Value, 0.0000000001);
        }

        [TestMethod]
        public void FunctionExample_UsesExistingGenericApply()
        {
            RealValue angle =
                RealValue.FromExpression("1 degree", Math.PI / 180.0);

            RealValue sine = angle.Apply("sin", Math.Sin);

            Assert.AreEqual(
                Math.Sin(Math.PI / 180.0),
                sine.Value,
                0.0000000001);
            Assert.AreEqual("sin(1 degree)", sine.Expression);
        }

        [TestMethod]
        public void ExactCompositionExample_ProducesCanonicalResult()
        {
            RatioComposer composer = new RatioComposer();
            Ratio result = composer.Add(
                new Ratio(1, 2),
                new Ratio(1, 2));

            RealValue value = RealValue.FromRatio(result);

            Assert.AreEqual(1L, result.Numerator);
            Assert.AreEqual(1L, result.Denominator);
            Assert.AreEqual(1.0, value.Value, 0.0000000001);
        }

        [TestMethod]
        public void RepresentationAndCalculationRemainSeparate()
        {
            RealValue value =
                RealValue.FromExpression("sin(1 degree)", 0.01745240643728351);

            Assert.AreEqual(
                "sin(1 degree)",
                value.Expression);

            Assert.AreEqual(
                0.01745240643728351,
                value.Value,
                0.0000000001);
        }
    }
}
