using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Decomposes an analytical state and discovers
    /// numerical relations recursively.
    /// </summary>
    public class StateDecomposer
    {
        private readonly FactorRelationFinder _factorFinder;

        public StateDecomposer()
        {
            _factorFinder =
                new FactorRelationFinder();
        }

        public List<FactorRelation> FindFactorRelations(
            AnalysisState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("state");
            }

            int value;

            if (!Int32.TryParse(
                state.Representation,
                out value))
            {
                return new List<FactorRelation>();
            }

            return _factorFinder.Find(value);
        }
    }
}