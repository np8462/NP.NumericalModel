using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    public class ConceptualRelationGraph
    {
        private readonly Dictionary<string, RelationNode> _nodes;
        private readonly List<RelationEdge> _edges;

        public IEnumerable<RelationNode> Nodes
        {
            get { return _nodes.Values; }
        }

        public IEnumerable<RelationEdge> Edges
        {
            get { return _edges; }
        }

        public ConceptualRelationGraph()
        {
            _nodes =
                new Dictionary<string, RelationNode>(
                    StringComparer.Ordinal);

            _edges =
                new List<RelationEdge>();
        }

        public RelationNode GetOrAddNode(
            string expression,
            string kind)
        {
            RelationNode node;

            if (_nodes.TryGetValue(
                expression,
                out node))
            {
                return node;
            }

            node = new RelationNode(
                expression,
                kind);

            _nodes.Add(
                expression,
                node);

            return node;
        }

        public RelationEdge AddEdge(
            string sourceExpression,
            string sourceKind,
            string targetExpression,
            string targetKind,
            string rule,
            int depth)
        {
            RelationNode source =
                GetOrAddNode(
                    sourceExpression,
                    sourceKind);

            RelationNode target =
                GetOrAddNode(
                    targetExpression,
                    targetKind);

            RelationEdge edge =
                new RelationEdge(
                    source,
                    target,
                    rule,
                    depth);

            _edges.Add(edge);

            return edge;
        }

        public bool ContainsNode(
            string expression)
        {
            return _nodes.ContainsKey(expression);
        }

        public List<RelationEdge> FindOutgoing(
            string expression)
        {
            List<RelationEdge> result =
                new List<RelationEdge>();

            foreach (RelationEdge edge in _edges)
            {
                if (edge.Source.Expression == expression)
                {
                    result.Add(edge);
                }
            }

            return result;
        }

        public List<RelationEdge> FindIncoming(
            string expression)
        {
            List<RelationEdge> result =
                new List<RelationEdge>();

            foreach (RelationEdge edge in _edges)
            {
                if (edge.Target.Expression == expression)
                {
                    result.Add(edge);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return "Nodes = "
                + _nodes.Count.ToString()
                + ", Edges = "
                + _edges.Count.ToString();
        }
    }
}