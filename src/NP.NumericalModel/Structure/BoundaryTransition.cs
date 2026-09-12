using NP.NumericalModel.Base;

namespace NP.NumericalModel.Structure
{
    /// <summary>
    /// Represents an NP-specific conceptual boundary transition.
    ///
    /// Example:
    /// 5.5 [Base=6]  -> 6:6
    /// 9.9 [Base=10] -> 10:10
    ///
    /// This is a model-specific interpretation, not a conventional
    /// arithmetic identity.
    /// </summary>
    public class BoundaryTransition
    {
        public SubsetRelation Source { get; private set; }

        public int NextOrder
        {
            get
            {
                return Source.BaseSystem.Base;
            }
        }

        public BoundaryTransition(SubsetRelation source)
        {
            Source = source;
        }

        public bool CanTransition
        {
            get
            {
                return Source.IsBoundary;
            }
        }

        public string GetConceptualResult()
        {
            if (!CanTransition)
            {
                return null;
            }

            return NextOrder.ToString()
                + ":"
                + NextOrder.ToString();
        }
    }
}