using System;
using System.Numerics;
using NP.NumericalModel;
using NP.NumericalModel.ConsoleSample;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("=== NP.NumericalModel Console ===");
            System.Console.WriteLine();

            BaseSystemDemo.Run();
            BaseConversionDemo.Run();
            StructuralModelDemo.Run();
            BoundaryTransitionDemo.Run();
            RelationAnalyzerDemo.Run();
            FactorialStateDemo.Run();
            FactorRelationDemo.Run();
            ConceptualRelationDemo.Run();
            RelationEngineDemo.Run();

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
