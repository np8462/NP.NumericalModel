using System;
using System.Collections.Generic;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.ConsoleSample
{
    public class FactorialStateDemo
    {
        public static void Run()
        {
            System.Console.WriteLine(
                "========================================");

            System.Console.WriteLine(
                "Factorial State Demo");

            System.Console.WriteLine(
                "========================================");

            int value = 252;

            FactorialStateGenerator generator =
                new FactorialStateGenerator();

            int expected =
                generator.GetExpectedStateCount(3);

            List<AnalysisState> states =
                generator.Generate(value);

            System.Console.WriteLine(
                "Input: "
                + value);

            System.Console.WriteLine(
                "Digit Count: 3");

            System.Console.WriteLine(
                "Expected States: "
                + expected);

            System.Console.WriteLine(
                "Generated States: "
                + states.Count);

            System.Console.WriteLine();

            int i;

            for (i = 0; i < states.Count; i++)
            {
                AnalysisState state = states[i];

                System.Console.WriteLine(
                    "State "
                    + (i + 1).ToString()
                    + ": "
                    + state.Representation
                    + " | Kind = "
                    + state.Kind);
            }

            System.Console.WriteLine();
            Console.WriteLine(
    "Press ENTER to exit.");

            Console.ReadLine();
        }
    }
}