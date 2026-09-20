using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Analysis;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class DerivedRelationTests
    {
        [TestMethod]
        public void CanDeriveTwentyTwoOverSeven()
        {
            RatioRelationDeriver deriver = new RatioRelationDeriver();
            DerivedRelation relation = deriver.Derive(new Ratio(22, 7));

            Assert.AreEqual(22L, relation.Numerator);
            Assert.AreEqual(7L, relation.Denominator);
            Assert.AreEqual(3L, relation.WholePart);
            Assert.AreEqual(1L, relation.Remainder);
            Assert.IsFalse(relation.IsExact);
            Assert.AreEqual("3 + 1/7", relation.ToString());
        }

        [TestMethod]
        public void CanDeriveExactFortyTwoOverSeven()
        {
            RatioRelationDeriver deriver = new RatioRelationDeriver();
            DerivedRelation relation = deriver.Derive(new Ratio(42, 7));

            Assert.AreEqual(6L, relation.WholePart);
            Assert.AreEqual(0L, relation.Remainder);
            Assert.IsTrue(relation.IsExact);
            Assert.AreEqual("6", relation.ToString());
        }

        [TestMethod]
        public void CanDeriveFiveOverThree()
        {
            RatioRelationDeriver deriver = new RatioRelationDeriver();
            DerivedRelation relation = deriver.Derive(new Ratio(5, 3));

            Assert.AreEqual(1L, relation.WholePart);
            Assert.AreEqual(2L, relation.Remainder);
            Assert.AreEqual("1 + 2/3", relation.ToString());
        }

        [TestMethod]
        public void CanDeriveComposedFortyFourOverSeven()
        {
            RatioComposition composition =
                new RatioComposition(
                    new Ratio(42, 7),
                    new Ratio(2, 7));

            RatioRelationDeriver deriver = new RatioRelationDeriver();
            DerivedRelation relation = deriver.Derive(composition);

            Assert.AreEqual(44L, relation.Numerator);
            Assert.AreEqual(7L, relation.Denominator);
            Assert.AreEqual(6L, relation.WholePart);
            Assert.AreEqual(2L, relation.Remainder);
            Assert.IsFalse(relation.IsExact);
            Assert.AreEqual("6 + 2/7", relation.ToString());
        }

        [TestMethod]
        public void CanDeriveExactComposedOne()
        {
            RatioComposition composition =
                new RatioComposition(
                    new Ratio(1, 2),
                    new Ratio(1, 2));

            RatioRelationDeriver deriver = new RatioRelationDeriver();
            DerivedRelation relation = deriver.Derive(composition);

            Assert.AreEqual(1L, relation.Numerator);
            Assert.AreEqual(1L, relation.Denominator);
            Assert.AreEqual(1L, relation.WholePart);
            Assert.AreEqual(0L, relation.Remainder);
            Assert.IsTrue(relation.IsExact);
            Assert.AreEqual("1", relation.ToString());
        }

        [TestMethod]
        public void RejectsNullRatio()
        {
            RatioRelationDeriver deriver = new RatioRelationDeriver();

            try
            {
                deriver.Derive(null);
                Assert.Fail("A null ratio must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
