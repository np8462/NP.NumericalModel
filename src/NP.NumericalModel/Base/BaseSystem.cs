using NP.NumericalModel.Interpretation;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NP.NumericalModel.Base
{
    /// <summary>
    /// Represents the basic properties of a positional number base.
    ///
    /// Standard foundation:
    /// A base B uses digits from 0 through B - 1.
    ///
    /// This class intentionally does not define any NP-specific
    /// semantic interpretation.
    /// </summary>
    public class BaseSystem
    {
        public int Base { get; private set; }

        public int MaximumDigit
        {
            get { return Base - 1; }
        }

        public BaseSystem(int numberBase)
        {
            if (numberBase < 2)
            {
                throw new ArgumentOutOfRangeException("numberBase");
            }

            Base = numberBase;
        }

        public bool IsValidDigit(int digit)
        {
            return digit >= 0 && digit < Base;
        }

        public override string ToString()
        {
            return Base.ToString();
        }
    }

    //public static class BaseConverter
    //{
    //    public static BigInteger ToDecimal(
    //        string representation,
    //        BaseSystem sourceBase)
    //    {
    //        BigInteger result = 0;

    //        for (int i = 0; i < representation.Length; i++)
    //        {
    //            int digit = SymbolToDigit(representation[i]);

    //            if (!sourceBase.IsValidDigit(digit))
    //                throw new ArgumentException("Invalid digit.");

    //            result = result * sourceBase.Base + digit;
    //        }

    //        return result;
    //    }

    //    public static string FromDecimal(
    //        BigInteger value,
    //        BaseSystem targetBase)
    //    {
    //        if (value < 0)
    //            throw new ArgumentOutOfRangeException("value");

    //        if (value == 0)
    //            return "0";

    //        StringBuilder result = new StringBuilder();

    //        while (value > 0)
    //        {
    //            BigInteger remainder = value % targetBase.Base;

    //            result.Insert(0, DigitToSymbol((int)remainder));
    //            value = value / targetBase.Base;
    //        }

    //        return result.ToString();
    //    }

    //    public static string Convert(
    //        string representation,
    //        BaseSystem sourceBase,
    //        BaseSystem targetBase)
    //    {
    //        BigInteger decimalValue =
    //            ToDecimal(representation, sourceBase);

    //        return FromDecimal(decimalValue, targetBase);
    //    }

    //    public static int SymbolToDigit(char symbol)
    //    {
    //        if (symbol >= '0' && symbol <= '9')
    //            return symbol - '0';

    //        if (symbol >= 'A' && symbol <= 'Z')
    //            return symbol - 'A' + 10;

    //        if (symbol >= 'a' && symbol <= 'z')
    //            return symbol - 'a' + 10;

    //        throw new ArgumentException("Invalid digit symbol.");
    //    }

    //    private static char DigitToSymbol(int digit)
    //    {
    //        if (digit < 10)
    //            return (char)('0' + digit);

    //        return (char)('A' + digit - 10);
    //    }
    //}

    public class BaseComposition
    {
        public IList<BaseValue> Values { get; private set; }

        public BaseComposition()
        {
            Values = new List<BaseValue>();
        }

        public void Add(BaseValue value)
        {
            Values.Add(value);
        }

        public BigInteger SumDecimal()
        {
            BigInteger result = 0;

            for (int i = 0; i < Values.Count; i++)
            {
                result += Values[i].DecimalValue;
            }

            return result;
        }
    }

    //public class BaseDecomposition
    //{
    //    public BaseSystem BaseSystem { get; private set; }

    //    public string Representation { get; private set; }

    //    public IList<int> Digits { get; private set; }

    //    public IList<BigInteger> Contributions { get; private set; }

    //    public BigInteger DecimalValue { get; private set; }

    //    public BaseDecomposition(
    //        string representation,
    //        BaseSystem baseSystem)
    //    {
    //        BaseSystem = baseSystem;
    //        Representation = representation;

    //        Digits = new List<int>();
    //        Contributions = new List<BigInteger>();

    //        Calculate();
    //    }

    //    private void Calculate()
    //    {
    //        BigInteger value = 0;

    //        for (int i = 0; i < Representation.Length; i++)
    //        {
    //            int digit = BaseConverter.SymbolToDigit(Representation[i]);

    //            if (!BaseSystem.IsValidDigit(digit))
    //                throw new ArgumentException("Invalid digit.");

    //            int power = Representation.Length - 1 - i;

    //            BigInteger contribution =
    //                digit * BigInteger.Pow(BaseSystem.Base, power);

    //            Digits.Add(digit);
    //            Contributions.Add(contribution);

    //            value += contribution;
    //        }

    //        DecimalValue = value;
    //    }
    //}

    public class BaseValue
    {
        public BaseSystem BaseSystem { get; private set; }

        public string Representation { get; private set; }

        public BigInteger DecimalValue { get; private set; }

        public BaseValue(
            string representation,
            BaseSystem baseSystem)
        {
            BaseSystem = baseSystem;
            Representation = representation;

            DecimalValue =
                BaseConverter.ToDecimal(
                    representation,
                    baseSystem);
        }
    }

    //BaseSystem binary = new BaseSystem(2);
    //BaseSystem octal = new BaseSystem(8);
    //BaseSystem hex = new BaseSystem(16);

    //BaseComposition composition =
    //    new BaseComposition();

    //composition.Add(
    //    new BaseValue("101", binary));

    //composition.Add(
    //    new BaseValue("5", octal));

    //composition.Add(
    //    new BaseValue("A", hex));

    public class BaseTransformation
    {
        public string SourceRepresentation { get; private set; }

        public BaseSystem SourceBase { get; private set; }

        public string TargetRepresentation { get; private set; }

        public BaseSystem TargetBase { get; private set; }

        public BigInteger DecimalValue { get; private set; }

        public BaseTransformation(
            string sourceRepresentation,
            BaseSystem sourceBase,
            BaseSystem targetBase)
        {
            SourceRepresentation = sourceRepresentation;
            SourceBase = sourceBase;
            TargetBase = targetBase;

            DecimalValue =
                BaseConverter.ToDecimal(
                    sourceRepresentation,
                    sourceBase);

            TargetRepresentation =
                BaseConverter.FromDecimal(
                    DecimalValue,
                    targetBase);
        }
    }

    //public class NumericalConcept
    //{
    //    public string Symbol { get; set; }
    //    public int? NumericValue { get; set; }

    //    public string Representation { get; set; }
    //    public string Interpretation { get; set; }

    //    public List<NumericalRelation> Relations { get; private set; }

    //    public NumericalConcept()
    //    {
    //        Relations = new List<NumericalRelation>();
    //    }
    //}

    public class NumericalRelation
    {
        public NumericalConcept Source { get; set; }
        public NumericalConcept Target { get; set; }

        public string RelationType { get; set; }
        public string Symbol { get; set; }
        public string Interpretation { get; set; }
    }

    public class NumericalInterpretation
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<NumericalConcept> Concepts { get; private set; }
        public List<NumericalRelation> Relations { get; private set; }

        public NumericalInterpretation()
        {
            Concepts = new List<NumericalConcept>();
            Relations = new List<NumericalRelation>();
        }
    }

    //public class InterpretationDefinition
    //{
    //    public string Name { get; set; }
    //    public string Version { get; set; }

    //    public List<NumericalConcept> Concepts { get; private set; }
    //    public List<ConceptualRelation> Relations { get; private set; }

    //    public InterpretationDefinition()
    //    {
    //        Concepts = new List<NumericalConcept>();
    //        Relations = new List<ConceptualRelation>();
    //    }
    //}
}