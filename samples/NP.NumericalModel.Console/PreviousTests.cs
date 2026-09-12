using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace NP.NumericalModel.ConsoleSample
{
   public static class PreviousTests
    {
        public static void Run()
        {
            ByteModel byteModel = new ByteModel(0x4A);
            NibblePair nibblePair = byteModel.NibblePair;
            NibblePair pairFromByte = NibblePair.FromByte((byte)byteModel.Value);

            Console.WriteLine("=== NP.NumericalModel Console Sample ===");
            Console.WriteLine();

            Console.WriteLine("Byte:");
            Console.WriteLine("Decimal: " + byteModel.Value);
            Console.WriteLine("Hex: " + nibblePair.HighNibble.HexValue + nibblePair.LowNibble.HexValue);
            Console.WriteLine();

            Console.WriteLine("High Nibble:");
            Console.WriteLine("Decimal: " + byteModel.HighNibble.Value);
            Console.WriteLine("Hex: " + byteModel.HighNibble.HexValue);
            Console.WriteLine();

            Console.WriteLine("Low Nibble:");
            Console.WriteLine("Decimal: " + byteModel.LowNibble.Value);
            Console.WriteLine("Hex: " + byteModel.LowNibble.HexValue);
            Console.WriteLine();

            Console.WriteLine("NibblePair -> Byte:");
            Console.WriteLine(
                nibblePair.HighNibble.HexValue +
                " + " +
                nibblePair.LowNibble.HexValue +
                " = " +
                nibblePair.ToByte().ToString("X2"));

            Console.WriteLine("Decimal: " + nibblePair.ToByte());
            Console.WriteLine();

            Console.WriteLine("Byte -> NibblePair:");
            Console.WriteLine(
                byteModel.Value.ToString("X2") +
                " -> High: " +
                pairFromByte.HighNibble.HexValue +
                ", Low: " +
                pairFromByte.LowNibble.HexValue);

            Console.WriteLine();

            Console.WriteLine("Round Trip:");
            Console.WriteLine(
                byteModel.Value.ToString("X2") +
                " -> Nibbles -> " +
                pairFromByte.ToByte().ToString("X2"));

            Console.WriteLine();

            Console.WriteLine("ByteReader:");
            DisplayReaderValues();

            Console.WriteLine();

            // ---------------------------------------------------------
            // NumericalExpansion17 Test
            // ---------------------------------------------------------

            Console.WriteLine("=== NumericalExpansion17 Test ===");
            Console.WriteLine();

            ByteModel expansionByte = new ByteModel(0xA0);

            // 0xA0 = 160 decimal
            // Exactly 17 nibble values.
            //
            // This is only a valid sample expansion.
            // The important condition is:
            // 17 values, each 0..15, with total = 160.
            int[] expansionValues = new int[]
            {
                10, 10, 10, 10, 10, 10, 10, 10, 10,
                10, 10, 10, 10, 10, 10, 10, 0
            };

            NumericalExpansion17 expansion =
                new NumericalExpansion17(expansionByte, expansionValues);

            Console.WriteLine("Original Byte:");
            Console.WriteLine("Decimal: " + expansion.OriginalByteValue);
            Console.WriteLine("Hex: " + expansion.OriginalByteValue.ToString("X2"));
            Console.WriteLine();

            Console.WriteLine("Expansion Count:");
            Console.WriteLine(NumericalExpansion17.NibbleCount);
            Console.WriteLine();

            Console.WriteLine("17 Nibble Values:");

            int[] values = expansion.GetNibbleValues();

            for (int index = 0; index < values.Length; index++)
            {
                Console.WriteLine(
                    "Expansion[" + index + "] = " + values[index]);
            }

            Console.WriteLine();

            Console.WriteLine("Expansion Sum:");
            Console.WriteLine(expansion.GetSum());
            Console.WriteLine();

            Console.WriteLine("Expected Sum:");
            Console.WriteLine(expansion.OriginalByteValue);
            Console.WriteLine();

            Console.WriteLine("Sum Verification:");
            Console.WriteLine(expansion.IsSumEqualToOriginalByte());
            Console.WriteLine();

            Console.WriteLine("Valid Ordered State Count:");

            BigInteger stateCount =
                NumericalExpansion17.CalculateValidOrderedStateCount(expansionByte);

            Console.WriteLine(stateCount);
            Console.WriteLine();

            Console.WriteLine("NumericalExpansion17 Test Completed.");
            Console.WriteLine();

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }

        private static void DisplayReaderValues()
        {
            ByteReader reader =
                new ByteReader(new byte[] { 0x4A, 0x10, 0xFF });

            while (reader.ReadNext())
            {
                ByteModel currentByte = reader.CurrentByte;

                Console.WriteLine(
                    "Byte: " + currentByte.Value.ToString("X2"));

                Console.WriteLine(
                    "Decimal: " + currentByte.Value);

                Console.WriteLine(
                    "High Nibble: " + currentByte.HighNibble.HexValue);

                Console.WriteLine(
                    "Low Nibble: " + currentByte.LowNibble.HexValue);

                Console.WriteLine();
            }
        }
    }
}