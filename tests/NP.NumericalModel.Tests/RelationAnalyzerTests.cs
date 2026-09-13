using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class RelationAnalyzerTests
    {
        [TestMethod]
        public void Analyze74_ShouldFindDigitDifference3()
        {
            RelationAnalyzer analyzer =
                new RelationAnalyzer();

            RelationGraph graph =
                analyzer.Analyze(74);

            Assert.IsTrue(graph.ContainsNode(3));

            Assert.AreEqual(
                1,
                graph.FindDerivationsTo(3).Count);
        }

        [TestMethod]
        public void Analyze252_ShouldFindDigitSum9()
        {
            RelationAnalyzer analyzer =
                new RelationAnalyzer();

            RelationGraph graph =
                analyzer.Analyze(252);

            Assert.IsTrue(graph.ContainsNode(9));

            Assert.AreEqual(
                1,
                graph.FindDerivationsTo(9).Count);
        }

        [TestMethod]
        public void Analyze14_ShouldFindDigitSum5()
        {
            RelationAnalyzer analyzer =
                new RelationAnalyzer();

            RelationGraph graph =
                analyzer.Analyze(14);

            Assert.IsTrue(graph.ContainsNode(5));

            Assert.AreEqual(
                1,
                graph.FindDerivationsTo(5).Count);
        }

        [TestMethod]
        public void Analyze74_ShouldKeepDifferentDerivedValues()
        {
            RelationAnalyzer analyzer =
                new RelationAnalyzer();

            RelationGraph graph =
                analyzer.Analyze(74);

            Assert.IsTrue(graph.ContainsNode(11));
            Assert.IsTrue(graph.ContainsNode(3));
            Assert.IsTrue(graph.ContainsNode(2));
        }

        [TestMethod]
        public void Analyze_ShouldRespectMaximumDepth()
        {
            RelationAnalyzer analyzer =
                new RelationAnalyzer(0);

            RelationGraph graph =
                analyzer.Analyze(252);

            Assert.IsTrue(graph.ContainsNode(252));
            Assert.IsFalse(graph.ContainsNode(9));
        }
    }
}