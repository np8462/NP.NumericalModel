using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Base;
using NP.NumericalModel.Structure;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class BaseModelTests
    {
        [TestMethod]
        public void BaseDefinesDigitsFromZeroToBaseMinusOne()
        {
            BaseSystem base6 = new BaseSystem(6);
            BaseSystem base10 = new BaseSystem(10);
            BaseSystem base16 = new BaseSystem(16);

            Assert.AreEqual(5, base6.MaximumDigit);
            Assert.AreEqual(9, base10.MaximumDigit);
            Assert.AreEqual(15, base16.MaximumDigit);
            Assert.IsTrue(base6.IsValidDigit(5));
            Assert.IsFalse(base6.IsValidDigit(6));
        }

        [TestMethod]
        public void BoundaryTransitionPreservesTheExistingConceptualRule()
        {
            SubsetRelation source6 = new SubsetRelation(
                5,
                5,
                StructuralSeparator.Colon,
                new BaseSystem(6));

            SubsetRelation source10 = new SubsetRelation(
                9,
                9,
                StructuralSeparator.Colon,
                new BaseSystem(10));

            BoundaryTransition transition6 = new BoundaryTransition(source6);
            BoundaryTransition transition10 = new BoundaryTransition(source10);

            Assert.IsTrue(transition6.CanTransition);
            Assert.AreEqual("6:6", transition6.GetConceptualResult());
            Assert.AreEqual("10:10", transition10.GetConceptualResult());
        }
        //[TestMethod]
        //public void BoundaryTransitionPreservesTheExistingConceptualRule()
        //{
        //    SubsetRelation source6 = new SubsetRelation(new BaseSystem(6), 5, 5);
        //    SubsetRelation source10 = new SubsetRelation(new BaseSystem(10), 9, 9);

        //    BoundaryTransition transition6 = new BoundaryTransition(source6);
        //    BoundaryTransition transition10 = new BoundaryTransition(source10);

        //    Assert.IsTrue(transition6.CanTransition);
        //    Assert.AreEqual("6:6", transition6.GetConceptualResult());
        //    Assert.AreEqual("10:10", transition10.GetConceptualResult());
        //}
    }

    [TestClass]
    public class BaseConversionTests
    {
        [TestMethod]
        public void Binary101101ConvertsToDecimal45()
        {
            Assert.AreEqual(
                new BigInteger(45),
                BaseConverter.ToDecimal("101101", new BaseSystem(2)));
        }

        [TestMethod]
        public void Decimal45ConvertsToOctal55()
        {
            Assert.AreEqual(
                "55",
                BaseConverter.FromDecimal(new BigInteger(45), new BaseSystem(8)));
        }

        [TestMethod]
        public void BinaryCanConvertToOctalThroughDecimalValue()
        {
            Assert.AreEqual(
                "55",
                BaseConverter.Convert("101101", new BaseSystem(2), new BaseSystem(8)));
        }

        [TestMethod]
        public void HexB6AConvertsToDecimal2922()
        {
            Assert.AreEqual(
                new BigInteger(2922),
                BaseConverter.ToDecimal("B6A", new BaseSystem(16)));
        }

        [TestMethod]
        public void Decimal255ConvertsToHexFF()
        {
            Assert.AreEqual(
                "FF",
                BaseConverter.FromDecimal(new BigInteger(255), new BaseSystem(16)));
        }

        [TestMethod]
        public void HexAndBinaryRepresentationsCanMeetAtTheSameDecimalValue()
        {
            BigInteger hexValue = BaseConverter.ToDecimal("B6A", new BaseSystem(16));
            BigInteger binaryValue = BaseConverter.ToDecimal("101101101010", new BaseSystem(2));

            Assert.AreEqual(hexValue, binaryValue);
            Assert.AreEqual("5552", BaseConverter.FromDecimal(hexValue, new BaseSystem(8)));
        }
    }

    [TestClass]
    public class BaseDecompositionTests
    {
        [TestMethod]
        public void HexB6AExposesItsPositionalContributions()
        {
            BaseDecomposition decomposition = new BaseDecomposition("B6A", new BaseSystem(16));

            Assert.AreEqual(3, decomposition.Digits.Count);
            Assert.AreEqual(11, decomposition.Digits[0]);
            Assert.AreEqual(6, decomposition.Digits[1]);
            Assert.AreEqual(10, decomposition.Digits[2]);
            Assert.AreEqual(new BigInteger(2816), decomposition.Contributions[0]);
            Assert.AreEqual(new BigInteger(96), decomposition.Contributions[1]);
            Assert.AreEqual(new BigInteger(10), decomposition.Contributions[2]);
            Assert.AreEqual(new BigInteger(2922), decomposition.DecimalValue);
        }

        [TestMethod]
        public void DecompositionFormulaFollowsPositionalBaseRule()
        {
            BaseDecomposition decomposition = new BaseDecomposition("101101", new BaseSystem(2));

            Assert.AreEqual("1*2^5 + 0*2^4 + 1*2^3 + 1*2^2 + 0*2^1 + 1*2^0", decomposition.GetFormula());
            Assert.AreEqual(new BigInteger(45), decomposition.DecimalValue);
        }

        [TestMethod]
        public void InventedBase14CanUseTheSameNumericDecompositionRule()
        {
            BaseDecomposition decomposition = new BaseDecomposition("A", new BaseSystem(14));

            Assert.AreEqual(10, decomposition.Digits[0]);
            Assert.AreEqual(new BigInteger(10), decomposition.DecimalValue);
        }
    }
}
