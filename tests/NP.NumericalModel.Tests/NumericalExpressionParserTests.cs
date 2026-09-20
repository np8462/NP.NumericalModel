using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Interpretation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class NumericalExpressionParserTests
    {
        [TestMethod]
        public void ParsesFortyTwoOverSevenPlusTwoOverSeven()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("42:7 + 2:7");

            Assert.AreEqual(44.0 / 7.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void RespectsArithmeticPrecedence()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("1 + 2 * 3");

            Assert.AreEqual(7.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParsesNestedArithmetic()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("(42:7 + 2:7) + 1:7");

            Assert.AreEqual(45.0 / 7.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParsesPiAndDivision()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("pi/2");

            Assert.AreEqual(Math.PI / 2.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParsesTrigonometricFunction()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("sin(pi/2)");

            Assert.AreEqual(1.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void PreservesExpressionText()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("42:7 + 2:7");

            Assert.AreEqual(
                "(42 / 7 + 2 / 7)",
                result.Text);
        }

        [TestMethod]
        public void ConvertsToRealValue()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            RealValue value =
                parser.Parse("22:7").ToRealValue();

            Assert.AreEqual(22.0 / 7.0, value.Value, 0.0000000001);
            Assert.AreEqual("(22 / 7)", value.Expression);
        }

        [TestMethod]
        public void ParsesFractionSubtraction()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("1/2 - 1/3");

            Assert.AreEqual(1.0 / 6.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParsesFractionMultiplication()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("2/3 * 3/4");

            Assert.AreEqual(1.0 / 2.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParsesFractionDivision()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("5/6 / 2/3");

            Assert.AreEqual(5.0 / 4.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParsesUnarySigns()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("-2 + +3");

            Assert.AreEqual(1.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ParenthesesChangeArithmeticPrecedence()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression result =
                parser.Parse("(1 + 2) * 3");

            Assert.AreEqual(9.0, result.Value, 0.0000000001);
        }

        [TestMethod]
        public void ColonAndSlashHaveSameDivisionMeaning()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            NumericalExpression colonResult = parser.Parse("5:2");
            NumericalExpression slashResult = parser.Parse("5/2");

            Assert.AreEqual(slashResult.Value, colonResult.Value, 0.0000000001);
            Assert.AreEqual(slashResult.Text, colonResult.Text);
        }

        [TestMethod]
        public void RejectsUnknownIdentifier()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            try
            {
                parser.Parse("tau");
                Assert.Fail("Unknown identifiers must be rejected.");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsMalformedExpression()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            try
            {
                parser.Parse("1 +");
                Assert.Fail("Malformed expressions must be rejected.");
            }
            catch (FormatException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void RejectsNullExpression()
        {
            NumericalExpressionParser parser = new NumericalExpressionParser();

            try
            {
                parser.Parse(null);
                Assert.Fail("A null expression must be rejected.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
