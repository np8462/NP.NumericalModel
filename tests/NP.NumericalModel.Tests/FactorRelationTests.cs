using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class FactorRelationTests
    {
        [TestMethod]
        public void Find252_ShouldContain12And21()
        {
            FactorRelationFinder finder =
                new FactorRelationFinder();

            List<FactorRelation> relations =
                finder.Find(252);

            bool found = false;

            foreach (FactorRelation relation in relations)
            {
                if (relation.Left == 12 &&
                    relation.Right == 21)
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void Find252_ShouldContain14And18()
        {
            FactorRelationFinder finder =
                new FactorRelationFinder();

            List<FactorRelation> relations =
                finder.Find(252);

            bool found = false;

            foreach (FactorRelation relation in relations)
            {
                if (relation.Left == 14 &&
                    relation.Right == 18)
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void FactorRelation12And21_ShouldRepresentAs12Colon21()
        {
            FactorRelation relation =
                new FactorRelation(
                    252,
                    12,
                    21);

            Assert.AreEqual(
                "12:21",
                relation.GetRepresentation());
        }
    }
}