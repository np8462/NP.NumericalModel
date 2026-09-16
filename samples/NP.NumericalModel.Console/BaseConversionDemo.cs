using System;
using System.Numerics;
using NP.NumericalModel.Base;

namespace NP.NumericalModel.ConsoleSample
{
    public class BaseConversionDemo
    {
        public static void Run()
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("Base Conversion and Decomposition Demo");
            System.Console.WriteLine("========================================");

            ShowConversion("101101", 2, 10, 8);
            ShowConversion("B6A", 16, 10, 2);

            ShowDecomposition("B6A", 16);
            ShowBoundary(6);
            ShowBoundary(10);

            System.Console.WriteLine();
            Console.WriteLine(
    "Press ENTER to exit.");

            Console.ReadLine();
        }

        private static void ShowConversion(
            string representation,
            int sourceBaseValue,
            int middleBaseValue,
            int targetBaseValue)
        {
            BaseSystem sourceBase = new BaseSystem(sourceBaseValue);
            BaseSystem middleBase = new BaseSystem(middleBaseValue);
            BaseSystem targetBase = new BaseSystem(targetBaseValue);

            BigInteger decimalValue = BaseConverter.ToDecimal(representation, sourceBase);
            string targetRepresentation = BaseConverter.Convert(
                representation,
                sourceBase,
                targetBase);

            System.Console.WriteLine(
                representation + "_" + sourceBaseValue
                + " -> " + decimalValue.ToString() + "_" + middleBaseValue
                + " -> " + targetRepresentation + "_" + targetBaseValue);
        }

        private static void ShowDecomposition(string representation, int baseValue)
        {
            BaseDecomposition decomposition = new BaseDecomposition(
                representation,
                new BaseSystem(baseValue));

            System.Console.WriteLine();
            System.Console.WriteLine(
                representation + "_" + baseValue
                + " = " + decomposition.GetFormula());
            System.Console.WriteLine(
                "Decimal = " + decomposition.DecimalValue.ToString());
        }

        private static void ShowBoundary(int baseValue)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(
                "Conceptual boundary: "
                + (baseValue - 1).ToString()
                + "."
                + (baseValue - 1).ToString()
                + " -> "
                + baseValue.ToString()
                + ":"
                + baseValue.ToString());
        }
    }
}
