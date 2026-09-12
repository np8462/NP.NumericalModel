using System;
using NP.NumericalModel.Base;
using NP.NumericalModel.Structure;

namespace NP.NumericalModel.ConsoleSample
{
    public class BoundaryTransitionDemo
    {
        public static void Run()
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("BoundaryTransition Demo");
            System.Console.WriteLine("========================================");

            TestBoundary(6);
            TestBoundary(10);

            System.Console.WriteLine();
        }

        private static void TestBoundary(int numberBase)
        {
            BaseSystem baseSystem = new BaseSystem(numberBase);

            int maximumDigit = baseSystem.MaximumDigit;

            SubsetRelation relation =
                new SubsetRelation(
                    maximumDigit,
                    maximumDigit,
                    StructuralSeparator.Dot,
                    baseSystem);

            BoundaryTransition transition =
                new BoundaryTransition(relation);

            System.Console.WriteLine(
                "Base: "
                + numberBase);

            System.Console.WriteLine(
                "Source: "
                + relation.ToString());

            System.Console.WriteLine(
                "Maximum Digit: "
                + maximumDigit);

            System.Console.WriteLine(
                "Is Boundary: "
                + relation.IsBoundary);

            System.Console.WriteLine(
                "Can Transition: "
                + transition.CanTransition);

            System.Console.WriteLine(
                "Conceptual Result: "
                + transition.GetConceptualResult());

            System.Console.WriteLine();
        }
    }
}