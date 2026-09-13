using System;
using System.Collections.Generic;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.ConsoleSample
{
    class ConceptualRelationDemo
    {
       public static void Run()
        {
            ConceptualRelationAnalyzer analyzer =
                new ConceptualRelationAnalyzer(6);

            ConceptualRelationGraph graph =
                analyzer.Analyze(252);

            Console.WriteLine(
                "========================================");

            Console.WriteLine(
                "Conceptual Relation Demo");

            Console.WriteLine(
                "========================================");

            Console.WriteLine(
                "Input: 252");

            Console.WriteLine();

            foreach (RelationEdge edge in graph.Edges)
            {
                Console.WriteLine(
                    edge.ToString());
            }

            Console.WriteLine();

            Console.WriteLine(
                graph.ToString());

            Console.WriteLine();

            Console.WriteLine(
                "Press ENTER to exit.");

            Console.ReadLine();
        }
    }
}