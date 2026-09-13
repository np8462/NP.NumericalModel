using System;
using System.Collections.Generic;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.ConsoleSample
{
    public class FactorRelationDemo
    {
        public static void Run()
        {
            System.Console.WriteLine(
                "========================================");

            System.Console.WriteLine(
                "Factor Relation Demo");

            System.Console.WriteLine(
                "========================================");

            FactorRelationFinder finder =
                new FactorRelationFinder();

            List<FactorRelation> relations =
                finder.Find(252);

            System.Console.WriteLine(
                "Input: 252");

            System.Console.WriteLine(
                "Factor Relations:");

            foreach (FactorRelation relation in relations)
            {
                System.Console.WriteLine(
                    "  "
                    + relation.ToString());
            }

            System.Console.WriteLine();
        }
    }
}