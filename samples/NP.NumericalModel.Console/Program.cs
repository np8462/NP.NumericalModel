using System;
using System.Numerics;
using NP.NumericalModel;
using NP.NumericalModel.ConsoleSample;


namespace NP.NumericalModel.ConsoleSample
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    PreviousTests.Run();
        //}

        static void Main(string[] args)
        {
            System.Console.WriteLine("=== NP.NumericalModel Console ===");
            System.Console.WriteLine();

            BaseSystemDemo.Run();

            StructuralModelDemo.Run();

            BoundaryTransitionDemo.Run();

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}

//namespace NP.NumericalModel.ConsoleSample
//{
//    internal class Program
//    {
//        private static void Main(string[] args)
//        {
//            ByteModel byteModel = new ByteModel(0x4A);
//            NibblePair nibblePair = byteModel.NibblePair;
//            NibblePair pairFromByte = NibblePair.FromByte((byte)byteModel.Value);

//            Console.WriteLine("=== NP.NumericalModel Console Sample ===");
//            Console.WriteLine();
//            Console.WriteLine("Byte:");
//            Console.WriteLine("Decimal: " + byteModel.Value);
//            Console.WriteLine("Hex: " + nibblePair.HighNibble.HexValue + nibblePair.LowNibble.HexValue);
//            Console.WriteLine();
//            Console.WriteLine("High Nibble:");
//            Console.WriteLine("Decimal: " + byteModel.HighNibble.Value);
//            Console.WriteLine("Hex: " + byteModel.HighNibble.HexValue);
//            Console.WriteLine();
//            Console.WriteLine("Low Nibble:");
//            Console.WriteLine("Decimal: " + byteModel.LowNibble.Value);
//            Console.WriteLine("Hex: " + byteModel.LowNibble.HexValue);
//            Console.WriteLine();
//            Console.WriteLine("NibblePair -> Byte:");
//            Console.WriteLine(nibblePair.HighNibble.HexValue + " + " + nibblePair.LowNibble.HexValue + " = " + nibblePair.ToByte().ToString("X2"));
//            Console.WriteLine("Decimal: " + nibblePair.ToByte());
//            Console.WriteLine();
//            Console.WriteLine("Byte -> NibblePair:");
//            Console.WriteLine(byteModel.Value.ToString("X2") + " -> High: " + pairFromByte.HighNibble.HexValue + ", Low: " + pairFromByte.LowNibble.HexValue);
//            Console.WriteLine();
//            Console.WriteLine("Round Trip:");
//            Console.WriteLine(byteModel.Value.ToString("X2") + " -> Nibbles -> " + pairFromByte.ToByte().ToString("X2"));
//            Console.WriteLine();
//            Console.WriteLine("ByteReader:");
//            DisplayReaderValues();
//            Console.WriteLine();
//            Console.WriteLine("Press ENTER to exit...");
//            Console.ReadLine();
//        }

//        private static void DisplayReaderValues()
//        {
//            ByteReader reader = new ByteReader(new byte[] { 0x4A, 0x10, 0xFF });

//            while (reader.ReadNext())
//            {
//                ByteModel currentByte = reader.CurrentByte;

//                Console.WriteLine("Byte: " + currentByte.Value.ToString("X2"));
//                Console.WriteLine("Decimal: " + currentByte.Value);
//                Console.WriteLine("High Nibble: " + currentByte.HighNibble.HexValue);
//                Console.WriteLine("Low Nibble: " + currentByte.LowNibble.HexValue);
//                Console.WriteLine();
//            }
//        }
//    }
//}