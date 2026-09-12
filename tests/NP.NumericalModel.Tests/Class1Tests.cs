using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NP.NumericalModel.Tests
{
    [TestClass]
    public class Class1Tests
    {
        [TestMethod]
        public void Class1CanBeCreated()
        {
            global::NP.NumericalModel.Class1 instance = new global::NP.NumericalModel.Class1();

            Assert.IsNotNull(instance);
        }
    }

    [TestClass]
    public class NibbleModelTests
    {
        [TestMethod]
        public void NibbleAcceptsValuesFromZeroToFifteen()
        {
            global::NP.NumericalModel.NibbleModel zero = new global::NP.NumericalModel.NibbleModel(0);
            global::NP.NumericalModel.NibbleModel fifteen = new global::NP.NumericalModel.NibbleModel(15);

            Assert.AreEqual(0, zero.Value);
            Assert.AreEqual(15, fifteen.Value);
            Assert.AreEqual("F", fifteen.HexValue);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void NibbleRejectsValuesAboveFifteen()
        {
            new global::NP.NumericalModel.NibbleModel(16);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void NibbleRejectsValuesBelowZero()
        {
            new global::NP.NumericalModel.NibbleModel(-1);
        }
    }

    [TestClass]
    public class ByteModelTests
    {
        [TestMethod]
        public void NibblePairConvertsFourAndAToByte4A()
        {
            global::NP.NumericalModel.NibblePair pair = new global::NP.NumericalModel.NibblePair(
                new global::NP.NumericalModel.NibbleModel(4),
                new global::NP.NumericalModel.NibbleModel(10));

            Assert.AreEqual((byte)0x4A, pair.ToByte());
        }

        [TestMethod]
        public void Byte4AHasHighNibbleFourAndLowNibbleA()
        {
            global::NP.NumericalModel.ByteModel byteModel = new global::NP.NumericalModel.ByteModel(0x4A);

            Assert.AreEqual(4, byteModel.HighNibble.Value);
            Assert.AreEqual("A", byteModel.LowNibble.HexValue);
            Assert.AreEqual((byte)0x4A, byteModel.NibblePair.ToByte());
        }
    }

    [TestClass]
    public class ByteReaderTests
    {
        [TestMethod]
        public void ReaderReadsInputBytesInOrder()
        {
            global::NP.NumericalModel.ByteReader reader = new global::NP.NumericalModel.ByteReader(
                new byte[] { 0x4A, 0x10, 0xFF });

            Assert.IsTrue(reader.ReadNext());
            Assert.AreEqual(0x4A, reader.CurrentByte.Value);
            Assert.AreEqual(0x4A, reader.CurrentSample.ByteModel.Value);
            Assert.IsTrue(reader.ReadNext());
            Assert.AreEqual(0x10, reader.CurrentByte.Value);
            Assert.IsTrue(reader.ReadNext());
            Assert.AreEqual(0xFF, reader.CurrentByte.Value);
            Assert.IsFalse(reader.ReadNext());
        }
    }
}
