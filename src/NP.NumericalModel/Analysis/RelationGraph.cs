using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Represents a graph of numerical relations discovered by analysis.
    ///
    /// Equal numerical results may have different origins.
    /// The graph preserves those origins instead of collapsing them.
    /// </summary>
    public class RelationGraph
    {
        private readonly HashSet<int> _nodes;
        private readonly List<RelationDerivation> _derivations;

        public IEnumerable<int> Nodes
        {
            get { return _nodes; }
        }

        public IEnumerable<RelationDerivation> Derivations
        {
            get { return _derivations; }
        }

        public RelationGraph()
        {
            _nodes = new HashSet<int>();
            _derivations = new List<RelationDerivation>();
        }

        public void AddNode(int value)
        {
            _nodes.Add(value);
        }

        public void AddDerivation(RelationDerivation derivation)
        {
            if (derivation == null)
            {
                throw new ArgumentNullException("derivation");
            }

            AddNode(derivation.SourceValue);
            AddNode(derivation.ResultValue);

            _derivations.Add(derivation);
        }

        public List<RelationDerivation> FindDerivationsTo(int value)
        {
            List<RelationDerivation> result =
                new List<RelationDerivation>();

            foreach (RelationDerivation derivation in _derivations)
            {
                if (derivation.ResultValue == value)
                {
                    result.Add(derivation);
                }
            }

            return result;
        }

        public bool ContainsNode(int value)
        {
            return _nodes.Contains(value);
        }

        public override string ToString()
        {
            return "Nodes = "
                + _nodes.Count.ToString()
                + ", Derivations = "
                + _derivations.Count.ToString();
        }
    }
}