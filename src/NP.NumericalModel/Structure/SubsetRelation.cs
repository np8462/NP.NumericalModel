using System;
using NP.NumericalModel.Base;

namespace NP.NumericalModel.Structure
{
    /// <summary>
    /// Represents a structural relation between a parent value
    /// and a subordinate child value.
    ///
    /// This is an NP.NumericalModel structural concept and is
    /// intentionally separated from conventional decimal notation.
    /// </summary>
    public class SubsetRelation
    {
        public int Parent { get; private set; }

        public int Child { get; private set; }

        public StructuralSeparator Separator { get; private set; }

        public BaseSystem BaseSystem { get; private set; }

        public SubsetRelation(
            int parent,
            int child,
            StructuralSeparator separator,
            BaseSystem baseSystem)
        {
            if (baseSystem == null)
            {
                throw new ArgumentNullException("baseSystem");
            }

            if (!baseSystem.IsValidDigit(parent))
            {
                throw new ArgumentOutOfRangeException("parent");
            }

            if (!baseSystem.IsValidDigit(child))
            {
                throw new ArgumentOutOfRangeException("child");
            }

            Parent = parent;
            Child = child;
            Separator = separator;
            BaseSystem = baseSystem;
        }

        public bool IsBoundary
        {
            get
            {
                return Parent == BaseSystem.MaximumDigit &&
                       Child == BaseSystem.MaximumDigit;
            }
        }

        public override string ToString()
        {
            return Parent.ToString()
                + Separator.Symbol
                + Child.ToString();
        }
    }
}