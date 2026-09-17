//using System;
//using NP.NumericalModel.Interpretation;

//namespace NP.NumericalModel.ConsoleSample
//{
//    class InterpretationDemo
//    {
//        public static void Run()
//        {
//            NumericalConcept c1 =
//                new NumericalConcept("23:32", null);

//            NumericalConcept c2 =
//                new NumericalConcept("#", 7);

//            NumericalConcept c3 =
//                new NumericalConcept("0.7", null);

//            NumericalConcept c4 =
//                new NumericalConcept("77", 77);

//            NumericalConcept c5 =
//                new NumericalConcept("3.14", null);

//            NumericalConcept c6 =
//                new NumericalConcept("42", 42);

//            c1.AddMeaning("Structural relation");
//            c2.AddMeaning("Sharp / Seven symbol");
//            c3.AddMeaning("Relation to seven");
//            c4.AddMeaning("Repeated seven state");
//            c5.AddMeaning("Pi-related symbolic state");
//            c6.AddMeaning("Numerical bridge");

//            ConceptualRelation r1 =
//                new ConceptualRelation(
//                    c1,
//                    c2,
//                    "symbolic",
//                    ":",
//                    "23:32 -> #");

//            ConceptualRelation r2 =
//                new ConceptualRelation(
//                    c2,
//                    c3,
//                    "transition",
//                    ":",
//                    "# -> 0.7");

//            ConceptualRelation r3 =
//                new ConceptualRelation(
//                    c3,
//                    c4,
//                    "relation",
//                    ":",
//                    "0.7 -> 77");

//            ConceptualRelation r4 =
//                new ConceptualRelation(
//                    c4,
//                    c5,
//                    "transition",
//                    ":",
//                    "77 -> 3.14");

//            ConceptualRelation r5 =
//                new ConceptualRelation(
//                    c5,
//                    c6,
//                    "bridge",
//                    ":",
//                    "3.14 -> 42");

//            RelationChain chain =
//                new RelationChain();

//            chain.AddConcept(c1);
//            chain.AddConcept(c2);
//            chain.AddConcept(c3);
//            chain.AddConcept(c4);
//            chain.AddConcept(c5);
//            chain.AddConcept(c6);

//            chain.AddRelation(r1);
//            chain.AddRelation(r2);
//            chain.AddRelation(r3);
//            chain.AddRelation(r4);
//            chain.AddRelation(r5);

//            Console.WriteLine(
//                "========================================");

//            Console.WriteLine(
//                "Interpretation Demo");

//            Console.WriteLine(
//                "========================================");

//            Console.WriteLine();

//            Console.WriteLine(
//                "Chain:");

//            Console.WriteLine(
//                "23:32 -> # -> 0.7 -> 77 -> 3.14 -> 42");

//            Console.WriteLine();

//            foreach (ConceptualRelation relation in chain.Relations)
//            {
//                Console.WriteLine(
//                    relation.ToString());

//                Console.WriteLine(
//                    "  Interpretation: " +
//                    relation.Interpretation);
//            }

//            Console.WriteLine();

//            Console.WriteLine(
//                "Concept meanings:");

//            foreach (NumericalConcept concept in chain.Concepts)
//            {
//                Console.WriteLine(
//                    "  " + concept.Symbol);

//                foreach (string meaning in concept.Meanings)
//                {
//                    Console.WriteLine(
//                        "    - " + meaning);
//                }
//            }

//            Console.WriteLine();

//            Console.WriteLine(
//                "Press ENTER to exit.");

//            Console.ReadLine();
//        }

//    }
//}

using System;
using NP.NumericalModel.Interpretation;

namespace NP.NumericalModel.ConsoleSample
{
    public static class InterpretationDemo
    {
        public static void Run()
        {
            InterpretationDefinition definition =
                InterpretationDefinitionFactory.CreateSevenBridgeDefinition();

            Console.WriteLine("=== Interpretation Definition ===");
            Console.WriteLine("Name: " + definition.Name);
            Console.WriteLine("Version: " + definition.Version);
            Console.WriteLine();

            Console.WriteLine("Concepts:");

            for (int i = 0; i < definition.Concepts.Count; i++)
            {
                NumericalConcept concept = definition.Concepts[i];

                Console.WriteLine(
                    "  [" + i + "] " +
                    concept.Symbol +
                    " = " +
                    (concept.NumericValue.HasValue
                        ? concept.NumericValue.Value.ToString()
                        : "null"));

                for (int j = 0; j < concept.Meanings.Count; j++)
                {
                    Console.WriteLine(
                        "      Meaning: " +
                        concept.Meanings[j]);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Relations:");

            for (int i = 0; i < definition.Relations.Count; i++)
            {
                ConceptualRelation relation =
                    definition.Relations[i];

                Console.WriteLine(
                    "  " +
                    relation.ToString());

                Console.WriteLine(
                    "      Type: " +
                    relation.RelationType);

                Console.WriteLine(
                    "      Symbol: " +
                    relation.Symbol);

                Console.WriteLine(
                    "      Interpretation: " +
                    relation.Interpretation);
            }

            Console.WriteLine();
            Console.WriteLine("Chains:");

            for (int i = 0; i < definition.Chains.Count; i++)
            {
                RelationChain chain =
                    definition.Chains[i];

                Console.WriteLine(
                    "  Chain " + (i + 1) + ":");

                for (int j = 0; j < chain.Concepts.Count; j++)
                {
                    Console.Write(
                        chain.Concepts[j].Symbol);

                    if (j < chain.Concepts.Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("=== End ===");
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}