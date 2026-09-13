using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class FactorialStateGeneratorTests
    {
        [TestMethod]
        public void ThreeDigits_ShouldHaveFourExpectedStates()
        {
            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            int count =
                generator.GetExpectedStateCount(3);

            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void Generate252_ShouldProduceFourStates()
        {
            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            List<AnalysisState> states =
                generator.Generate(252);

            Assert.AreEqual(4, states.Count);
        }

        [TestMethod]
        public void Generate252_ShouldContainFullValue()
        {
            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            List<AnalysisState> states =
                generator.Generate(252);

            Assert.AreEqual("252",
                states[0].Representation);
        }

        [TestMethod]
        public void Generate252_ShouldContain25()
        {
            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            List<AnalysisState> states =
                generator.Generate(252);

            Assert.AreEqual("25",
                states[1].Representation);
        }

        [TestMethod]
        public void Generate252_ShouldContain52()
        {
            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            List<AnalysisState> states =
                generator.Generate(252);

            Assert.AreEqual("52",
                states[2].Representation);
        }

        [TestMethod]
        public void Generate252_ShouldContainFullRelation()
        {
            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            List<AnalysisState> states =
                generator.Generate(252);

            Assert.AreEqual("2:5:2",
                states[3].Representation);
        }
    }
}