using System;

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
}