using System;
using NP.NumericalModel.Base;
using NP.NumericalModel.Structure;

namespace NP.NumericalModel.ConsoleSample
{
    public class StructuralModelDemo
    {
        public static void Run()
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("StructuralModel Demo");
            System.Console.WriteLine("========================================");

            BaseSystem base6 = new BaseSystem(6);
            BaseSystem base10 = new BaseSystem(10);

            SubsetRelation relation1 =
                new SubsetRelation(
                    2,
                    3,
                    StructuralSeparator.Dot,
                    base6);

            SubsetRelation relation2 =
                new SubsetRelation(
                    4,
                    8,
                    StructuralSeparator.Dot,
                    base10);

            SubsetRelation relation3 =
                new SubsetRelation(
                    5,
                    5,
                    StructuralSeparator.Dot,
                    base6);

            ShowRelation(relation1);
            ShowRelation(relation2);
            ShowRelation(relation3);

            System.Console.WriteLine();
            Console.WriteLine(
"Press ENTER to exit.");

            Console.ReadLine();
        }

        private static void ShowRelation(SubsetRelation relation)
        {
            System.Console.WriteLine(
                "Structure: "
                + relation.ToString());

            System.Console.WriteLine(
                "  Parent: "
                + relation.Parent);

            System.Console.WriteLine(
                "  Child: "
                + relation.Child);

            System.Console.WriteLine(
                "  Separator: "
                + relation.Separator.Symbol);

            System.Console.WriteLine(
                "  Base: "
                + relation.BaseSystem.Base);

            System.Console.WriteLine(
                "  Maximum Digit: "
                + relation.BaseSystem.MaximumDigit);

            System.Console.WriteLine(
                "  Is Boundary: "
                + relation.IsBoundary);

            System.Console.WriteLine();
        }
    }
}