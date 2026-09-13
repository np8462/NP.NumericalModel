using System;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Represents a factor relation between two integer values.
    ///
    /// Example:
    /// 12 * 21 = 252
    ///
    /// This class represents the discovered relation
    /// 12:21 without assigning a conventional meaning
    /// to the colon.
    /// </summary>
    public class FactorRelation
    {
        public int SourceValue { get; private set; }

        public int Left { get; private set; }

        public int Right { get; private set; }

        public FactorRelation(
            int sourceValue,
            int left,
            int right)
        {
            SourceValue = sourceValue;
            Left = left;
            Right = right;
        }

        public string GetRepresentation()
        {
            return Left.ToString()
                + ":"
                + Right.ToString();
        }

        public override string ToString()
        {
            return SourceValue.ToString()
                + " -> "
                + GetRepresentation();
        }
    }
}