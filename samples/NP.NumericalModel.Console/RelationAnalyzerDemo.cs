using System;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.ConsoleSample
{
    public class RelationAnalyzerDemo
    {
        public static void Run()
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("RelationAnalyzer Demo");
            System.Console.WriteLine("========================================");

            AnalyzeValue(74);
            AnalyzeValue(14);
            AnalyzeValue(252);

            System.Console.WriteLine();
        }

        private static void AnalyzeValue(int value)
        {
            RelationAnalyzer analyzer =
                new RelationAnalyzer(4);

            RelationGraph graph =
                analyzer.Analyze(value);

            System.Console.WriteLine(
                "Input: "
                + value);

            System.Console.WriteLine(
                "Graph: "
                + graph.ToString());

            System.Console.WriteLine(
                "Nodes:");

            foreach (int node in graph.Nodes)
            {
                System.Console.WriteLine(
                    "  "
                    + node);
            }

            System.Console.WriteLine(
                "Derivations:");

            foreach (RelationDerivation derivation
                in graph.Derivations)
            {
                System.Console.WriteLine(
                    "  "
                    + derivation.ToString());
            }

            System.Console.WriteLine();
        }
    }
}