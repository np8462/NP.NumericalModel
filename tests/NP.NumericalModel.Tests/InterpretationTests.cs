using Microsoft.VisualStudio.TestTools.UnitTesting;
using NP.NumericalModel.Interpretation;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class NumericalConceptTests
    {
        [TestMethod]
        public void ConceptStoresSymbolAndNumericValue()
        {
            NumericalConcept concept =
                new NumericalConcept("42", 42);

            Assert.AreEqual("42", concept.Symbol);
            Assert.AreEqual(42, concept.NumericValue);
        }

        [TestMethod]
        public void ConceptCanStoreMultipleMeanings()
        {
            NumericalConcept concept =
                new NumericalConcept("4", 4);

            concept.AddMeaning("Being");
            concept.AddMeaning("Life");
            concept.AddMeaning("Aggregate");

            Assert.AreEqual(3, concept.Meanings.Count);
            Assert.AreEqual("Being", concept.Meanings[0]);
            Assert.AreEqual("Life", concept.Meanings[1]);
            Assert.AreEqual("Aggregate", concept.Meanings[2]);
        }

        [TestMethod]
        public void ConceptCanExistWithoutNumericValue()
        {
            NumericalConcept concept =
                new NumericalConcept("#", null);

            Assert.AreEqual("#", concept.Symbol);
            Assert.IsNull(concept.NumericValue);
        }
    }

    [TestClass]
    public class ConceptualRelationTests
    {
        [TestMethod]
        public void RelationConnectsSourceAndTarget()
        {
            NumericalConcept source =
                new NumericalConcept("23:32", null);

            NumericalConcept target =
                new NumericalConcept("#", 7);

            ConceptualRelation relation =
                new ConceptualRelation(
                    source,
                    target,
                    "symbolic",
                    ":",
                    "23:32 -> #");

            Assert.AreSame(source, relation.Source);
            Assert.AreSame(target, relation.Target);
            Assert.AreEqual("symbolic", relation.RelationType);
            Assert.AreEqual(":", relation.Symbol);
            Assert.AreEqual("23:32 -> #", relation.Interpretation);
        }

        [TestMethod]
        public void RelationToStringShowsSourceAndTarget()
        {
            NumericalConcept source =
                new NumericalConcept("3.14", null);

            NumericalConcept target =
                new NumericalConcept("42", 42);

            ConceptualRelation relation =
                new ConceptualRelation(
                    source,
                    target,
                    "bridge",
                    ":",
                    "3.14 -> 42");

            Assert.AreEqual(
                "3.14 --bridge--> 42",
                relation.ToString());
        }
    }

    [TestClass]
    public class RelationChainTests
    {
        [TestMethod]
        public void ChainStoresConceptsAndRelations()
        {
            NumericalConcept c1 =
                new NumericalConcept("23:32", null);

            NumericalConcept c2 =
                new NumericalConcept("#", 7);

            ConceptualRelation relation =
                new ConceptualRelation(
                    c1,
                    c2,
                    "symbolic",
                    ":",
                    "23:32 -> #");

            RelationChain chain =
                new RelationChain();

            chain.AddConcept(c1);
            chain.AddConcept(c2);
            chain.AddRelation(relation);

            Assert.AreEqual(2, chain.Concepts.Count);
            Assert.AreEqual(1, chain.Relations.Count);
            Assert.AreSame(c1, chain.Concepts[0]);
            Assert.AreSame(c2, chain.Concepts[1]);
            Assert.AreSame(relation, chain.Relations[0]);
        }

        [TestMethod]
        public void ChainCanRepresentTheSixConceptSequence()
        {
            NumericalConcept c1 =
                new NumericalConcept("23:32", null);

            NumericalConcept c2 =
                new NumericalConcept("#", 7);

            NumericalConcept c3 =
                new NumericalConcept("0.7", null);

            NumericalConcept c4 =
                new NumericalConcept("77", 77);

            NumericalConcept c5 =
                new NumericalConcept("3.14", null);

            NumericalConcept c6 =
                new NumericalConcept("42", 42);

            RelationChain chain =
                new RelationChain();

            chain.AddConcept(c1);
            chain.AddConcept(c2);
            chain.AddConcept(c3);
            chain.AddConcept(c4);
            chain.AddConcept(c5);
            chain.AddConcept(c6);

            Assert.AreEqual(6, chain.Concepts.Count);
            Assert.AreEqual("23:32", chain.Concepts[0].Symbol);
            Assert.AreEqual("#", chain.Concepts[1].Symbol);
            Assert.AreEqual("0.7", chain.Concepts[2].Symbol);
            Assert.AreEqual("77", chain.Concepts[3].Symbol);
            Assert.AreEqual("3.14", chain.Concepts[4].Symbol);
            Assert.AreEqual("42", chain.Concepts[5].Symbol);
        }
    }

    [TestClass]
    public class InterpretationDefinitionTests
    {
        [TestMethod]
        public void DefinitionStoresNameAndVersion()
        {
            InterpretationDefinition definition =
                new InterpretationDefinition(
                    "Numerical Relations",
                    "1.0");

            Assert.AreEqual(
                "Numerical Relations",
                definition.Name);

            Assert.AreEqual(
                "1.0",
                definition.Version);
        }

        [TestMethod]
        public void DefinitionStoresConceptsRelationsAndChains()
        {
            InterpretationDefinition definition =
                new InterpretationDefinition(
                    "Numerical Relations",
                    "1.0");

            NumericalConcept source =
                new NumericalConcept("23:32", null);

            NumericalConcept target =
                new NumericalConcept("#", 7);

            ConceptualRelation relation =
                new ConceptualRelation(
                    source,
                    target,
                    "symbolic",
                    ":",
                    "23:32 -> #");

            RelationChain chain =
                new RelationChain();

            chain.AddConcept(source);
            chain.AddConcept(target);
            chain.AddRelation(relation);

            definition.AddConcept(source);
            definition.AddConcept(target);
            definition.AddRelation(relation);
            definition.AddChain(chain);

            Assert.AreEqual(2, definition.Concepts.Count);
            Assert.AreEqual(1, definition.Relations.Count);
            Assert.AreEqual(1, definition.Chains.Count);
            Assert.AreSame(chain, definition.Chains[0]);
        }
    }
}