using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Analysis;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class PatternAnalyzerTests
    {
        [TestMethod]
        public void DetectsExactDivisionPattern()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            PatternMatch match = analyzer.Analyze(new Ratio(42, 7));

            Assert.IsTrue(match.IsMatch);
            Assert.AreEqual("ExactDivision", match.Name);
            Assert.AreEqual(
                "The ratio divides without a remainder.",
                match.Description);
        }

        [TestMethod]
        public void DetectsRemainderDivisionPattern()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            PatternMatch match = analyzer.Analyze(new Ratio(22, 7));

            Assert.IsTrue(match.IsMatch);
            Assert.AreEqual("RemainderDivision", match.Name);
            Assert.AreEqual(
                "The ratio contains a whole part and a non-zero remainder.",
                match.Description);
        }

        [TestMethod]
        public void DetectsFiveOverThreeAsRemainderDivision()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            PatternMatch match = analyzer.Analyze(new Ratio(5, 3));

            Assert.AreEqual("RemainderDivision", match.Name);
            Assert.IsTrue(match.IsMatch);
        }

        [TestMethod]
        public void DetectsNumeratorReconstructionForTwentyTwoOverSeven()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            DerivedRelation relation =
                new RatioRelationDeriver().Derive(new Ratio(22, 7));

            PatternMatch match = analyzer.AnalyzeReconstruction(relation);

            Assert.IsTrue(match.IsMatch);
            Assert.AreEqual("NumeratorReconstruction", match.Name);
            Assert.AreEqual(
                "The whole part and remainder reconstruct the original numerator.",
                match.Description);
        }

        [TestMethod]
        public void DetectsNumeratorReconstructionForExactDivision()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            DerivedRelation relation =
                new RatioRelationDeriver().Derive(new Ratio(42, 7));

            PatternMatch match = analyzer.AnalyzeReconstruction(relation);

            Assert.IsTrue(match.IsMatch);
        }

        [TestMethod]
        public void RejectsInvalidNumeratorReconstruction()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            DerivedRelation relation =
                new DerivedRelation(22, 7, 3, 2);

            PatternMatch match = analyzer.AnalyzeReconstruction(relation);

            Assert.IsFalse(match.IsMatch);
            Assert.AreEqual("NumeratorReconstruction", match.Name);
        }

        [TestMethod]
        public void RejectsNullDerivedRelationForReconstruction()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();

            try
            {
                analyzer.AnalyzeReconstruction(null);
                Assert.Fail("A null derived relation must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DetectsConsistentRatioDecompositionForTwentyTwoOverSeven()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            Ratio ratio = new Ratio(22, 7);
            DerivedRelation relation =
                new RatioRelationDeriver().Derive(ratio);

            PatternMatch match = analyzer.AnalyzeConsistency(ratio, relation);

            Assert.IsTrue(match.IsMatch);
            Assert.AreEqual("RatioDecompositionConsistency", match.Name);
        }

        [TestMethod]
        public void DetectsConsistentRatioDecompositionForExactDivision()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            Ratio ratio = new Ratio(42, 7);
            DerivedRelation relation =
                new RatioRelationDeriver().Derive(ratio);

            PatternMatch match = analyzer.AnalyzeConsistency(ratio, relation);

            Assert.IsTrue(match.IsMatch);
        }

        [TestMethod]
        public void RejectsInconsistentWholePart()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            Ratio ratio = new Ratio(22, 7);
            DerivedRelation relation =
                new DerivedRelation(22, 7, 4, 1);

            PatternMatch match = analyzer.AnalyzeConsistency(ratio, relation);

            Assert.IsFalse(match.IsMatch);
        }

        [TestMethod]
        public void RejectsInconsistentRemainder()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            Ratio ratio = new Ratio(22, 7);
            DerivedRelation relation =
                new DerivedRelation(22, 7, 3, 2);

            PatternMatch match = analyzer.AnalyzeConsistency(ratio, relation);

            Assert.IsFalse(match.IsMatch);
        }

        [TestMethod]
        public void RejectsNullRatioForConsistency()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            DerivedRelation relation =
                new RatioRelationDeriver().Derive(new Ratio(22, 7));

            try
            {
                analyzer.AnalyzeConsistency(null, relation);
                Assert.Fail("A null ratio must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsNullDerivedRelationForConsistency()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();
            Ratio ratio = new Ratio(22, 7);

            try
            {
                analyzer.AnalyzeConsistency(ratio, null);
                Assert.Fail("A null derived relation must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsNullRatio()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();

            try
            {
                analyzer.Analyze((Ratio)null);
                Assert.Fail("A null ratio must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsNullDerivedRelation()
        {
            PatternAnalyzer analyzer = new PatternAnalyzer();

            try
            {
                analyzer.Analyze((DerivedRelation)null);
                Assert.Fail("A null derived relation must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
