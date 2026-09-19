using System;

namespace NP.NumericalModel.Interpretation
{
    public class SegmentDivision
    {
        public decimal TotalLength { get; private set; }
        public int DivisionCount { get; private set; }
        public int InternalPointCount { get; private set; }
        public decimal SegmentLength { get; private set; }

        public SegmentDivision(
            decimal totalLength,
            int divisionCount)
        {
            if (totalLength <= 0)
                throw new ArgumentOutOfRangeException("totalLength");

            if (divisionCount <= 0)
                throw new ArgumentOutOfRangeException("divisionCount");

            TotalLength = totalLength;
            DivisionCount = divisionCount;
            InternalPointCount = divisionCount - 1;
            SegmentLength = totalLength / divisionCount;
        }

        public override string ToString()
        {
            return TotalLength + " / " +
                   DivisionCount + " = " +
                   SegmentLength;
        }
    }
}