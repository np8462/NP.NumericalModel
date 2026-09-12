using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Base;
using NP.NumericalModel.Structure;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class StructuralModelTests
    {
        [TestMethod]
        public void Base6_ShouldHaveMaximumDigit5()
        {
            BaseSystem base6 = new BaseSystem(6);

            Assert.AreEqual(5, base6.MaximumDigit);
            Assert.IsTrue(base6.IsValidDigit(5));
            Assert.IsFalse(base6.IsValidDigit(6));
        }

        [TestMethod]
        public void Base10_ShouldHaveMaximumDigit9()
        {
            BaseSystem base10 = new BaseSystem(10);

            Assert.AreEqual(9, base10.MaximumDigit);
            Assert.IsTrue(base10.IsValidDigit(9));
            Assert.IsFalse(base10.IsValidDigit(10));
        }

        [TestMethod]
        public void Base6_55_ShouldBeBoundary()
        {
            BaseSystem base6 = new BaseSystem(6);

            SubsetRelation relation =
                new SubsetRelation(
                    5,
                    5,
                    StructuralSeparator.Dot,
                    base6);

            Assert.IsTrue(relation.IsBoundary);
            Assert.AreEqual("5.5", relation.ToString());
        }

        [TestMethod]
        public void Base10_99_ShouldBeBoundary()
        {
            BaseSystem base10 = new BaseSystem(10);

            SubsetRelation relation =
                new SubsetRelation(
                    9,
                    9,
                    StructuralSeparator.Dot,
                    base10);

            Assert.IsTrue(relation.IsBoundary);
            Assert.AreEqual("9.9", relation.ToString());
        }

        [TestMethod]
        public void Base6_55_ShouldTransitionTo66()
        {
            BaseSystem base6 = new BaseSystem(6);

            SubsetRelation relation =
                new SubsetRelation(
                    5,
                    5,
                    StructuralSeparator.Dot,
                    base6);

            BoundaryTransition transition =
                new BoundaryTransition(relation);

            Assert.IsTrue(transition.CanTransition);
            Assert.AreEqual("6:6", transition.GetConceptualResult());
        }

        [TestMethod]
        public void Base6_6_ShouldNotBeAValidDigit()
        {
            BaseSystem base6 = new BaseSystem(6);

            Assert.IsFalse(base6.IsValidDigit(6));
        }

        [TestMethod]
        public void Base10_99_ShouldTransitionTo1010()
        {
            BaseSystem base10 = new BaseSystem(10);

            SubsetRelation relation =
                new SubsetRelation(
                    9,
                    9,
                    StructuralSeparator.Dot,
                    base10);

            BoundaryTransition transition =
                new BoundaryTransition(relation);

            Assert.IsTrue(transition.CanTransition);
            Assert.AreEqual("10:10", transition.GetConceptualResult());
        }
    }
}