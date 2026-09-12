using System;
using NP.NumericalModel.Base;

namespace NP.NumericalModel.ConsoleSample
{
    public class BaseSystemDemo
    {
        public static void Run()
        {
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("BaseSystem Demo");
            System.Console.WriteLine("========================================");

            BaseSystem base2 = new BaseSystem(2);
            BaseSystem base6 = new BaseSystem(6);
            BaseSystem base8 = new BaseSystem(8);
            BaseSystem base10 = new BaseSystem(10);
            BaseSystem base16 = new BaseSystem(16);

            ShowBase(base2);
            ShowBase(base6);
            ShowBase(base8);
            ShowBase(base10);
            ShowBase(base16);

            System.Console.WriteLine();
        }

        private static void ShowBase(BaseSystem baseSystem)
        {
            System.Console.WriteLine(
                "Base = "
                + baseSystem.Base
                + " | Maximum Digit = "
                + baseSystem.MaximumDigit);

            System.Console.WriteLine(
                "  IsValidDigit(Max) = "
                + baseSystem.IsValidDigit(baseSystem.MaximumDigit));

            System.Console.WriteLine(
                "  IsValidDigit(Base) = "
                + baseSystem.IsValidDigit(baseSystem.Base));

            System.Console.WriteLine();
        }
    }
}