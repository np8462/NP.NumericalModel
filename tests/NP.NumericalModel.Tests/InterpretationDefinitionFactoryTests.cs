using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Interpretation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class InterpretationDefinitionFactoryTests
    {
        [TestMethod]
        public void CreateSevenBridgeDefinitionCreatesExpectedDefinition()
        {
            InterpretationDefinition definition =
                InterpretationDefinitionFactory.CreateSevenBridgeDefinition();

            Assert.IsNotNull(definition);

            Assert.AreEqual(
                "Seven Bridge Interpretation",
                definition.Name);

            Assert.AreEqual(
                "1.0",
                definition.Version);

            Assert.AreEqual(6, definition.Concepts.Count);
            Assert.AreEqual(5, definition.Relations.Count);
            Assert.AreEqual(1, definition.Chains.Count);
        }

        [TestMethod]
        public void SevenBridgeDefinitionContainsExpectedConcepts()
        {
            InterpretationDefinition definition =
                InterpretationDefinitionFactory.CreateSevenBridgeDefinition();

            Assert.AreEqual("23:32", definition.Concepts[0].Symbol);
            Assert.IsFalse(definition.Concepts[0].NumericValue.HasValue);

            Assert.AreEqual("#", definition.Concepts[1].Symbol);
            Assert.AreEqual(7, definition.Concepts[1].NumericValue.Value);

            Assert.AreEqual("0.7", definition.Concepts[2].Symbol);
            Assert.IsFalse(definition.Concepts[2].NumericValue.HasValue);

            Assert.AreEqual("77", definition.Concepts[3].Symbol);
            Assert.AreEqual(77, definition.Concepts[3].NumericValue.Value);

            Assert.AreEqual("3.14", definition.Concepts[4].Symbol);
            Assert.IsFalse(definition.Concepts[4].NumericValue.HasValue);

            Assert.AreEqual("42", definition.Concepts[5].Symbol);
            Assert.AreEqual(42, definition.Concepts[5].NumericValue.Value);
        }

        [TestMethod]
        public void SevenBridgeDefinitionContainsExpectedRelations()
        {
            InterpretationDefinition definition =
                InterpretationDefinitionFactory.CreateSevenBridgeDefinition();

            Assert.AreEqual(
                "23:32 --symbolic--> #",
                definition.Relations[0].ToString());

            Assert.AreEqual(
                "symbolic",
                definition.Relations[0].RelationType);

            Assert.AreEqual(
                "23:32 -> #",
                definition.Relations[0].Interpretation);


            Assert.AreEqual(
                "# --transition--> 0.7",
                definition.Relations[1].ToString());

            Assert.AreEqual(
                "transition",
                definition.Relations[1].RelationType);

            Assert.AreEqual(
                "# -> 0.7",
                definition.Relations[1].Interpretation);


            Assert.AreEqual(
                "0.7 --relation--> 77",
                definition.Relations[2].ToString());

            Assert.AreEqual(
                "relation",
                definition.Relations[2].RelationType);

            Assert.AreEqual(
                "0.7 -> 77",
                definition.Relations[2].Interpretation);


            Assert.AreEqual(
                "77 --transition--> 3.14",
                definition.Relations[3].ToString());

            Assert.AreEqual(
                "transition",
                definition.Relations[3].RelationType);

            Assert.AreEqual(
                "77 -> 3.14",
                definition.Relations[3].Interpretation);


            Assert.AreEqual(
                "3.14 --bridge--> 42",
                definition.Relations[4].ToString());

            Assert.AreEqual(
                "bridge",
                definition.Relations[4].RelationType);

            Assert.AreEqual(
                "3.14 -> 42",
                definition.Relations[4].Interpretation);
        }

        [TestMethod]
        public void SevenBridgeDefinitionContainsExpectedChain()
        {
            InterpretationDefinition definition =
                InterpretationDefinitionFactory.CreateSevenBridgeDefinition();

            RelationChain chain = definition.Chains[0];

            Assert.AreEqual(6, chain.Concepts.Count);
            Assert.AreEqual(5, chain.Relations.Count);

            Assert.AreEqual("23:32", chain.Concepts[0].Symbol);
            Assert.AreEqual("#", chain.Concepts[1].Symbol);
            Assert.AreEqual("0.7", chain.Concepts[2].Symbol);
            Assert.AreEqual("77", chain.Concepts[3].Symbol);
            Assert.AreEqual("3.14", chain.Concepts[4].Symbol);
            Assert.AreEqual("42", chain.Concepts[5].Symbol);

            Assert.AreEqual(
                "23:32 -> #",
                chain.Relations[0].Interpretation);

            Assert.AreEqual(
                "# -> 0.7",
                chain.Relations[1].Interpretation);

            Assert.AreEqual(
                "0.7 -> 77",
                chain.Relations[2].Interpretation);

            Assert.AreEqual(
                "77 -> 3.14",
                chain.Relations[3].Interpretation);

            Assert.AreEqual(
                "3.14 -> 42",
                chain.Relations[4].Interpretation);
        }

        [TestMethod]
        public void SevenBridgeDefinitionContainsExpectedMeanings()
        {
            InterpretationDefinition definition =
                InterpretationDefinitionFactory.CreateSevenBridgeDefinition();

            Assert.AreEqual(
                "Structural relation",
                definition.Concepts[0].Meanings[0]);

            Assert.AreEqual(
                "Sharp / Seven symbol",
                definition.Concepts[1].Meanings[0]);

            Assert.AreEqual(
                "Relation to seven",
                definition.Concepts[2].Meanings[0]);

            Assert.AreEqual(
                "Repeated seven state",
                definition.Concepts[3].Meanings[0]);

            Assert.AreEqual(
                "Pi-related symbolic state",
                definition.Concepts[4].Meanings[0]);

            Assert.AreEqual(
                "Numerical bridge",
                definition.Concepts[5].Meanings[0]);
        }
    }
}