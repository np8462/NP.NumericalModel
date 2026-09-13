using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Recursively analyzes numerical values and builds a relation graph.
    ///
    /// This first version deliberately uses only simple numerical rules.
    /// Model-specific semantic rules such as &, #, BaseComposition and
    /// paradigm transformations can be added later.
    /// </summary>
    public class RelationAnalyzer
    {
        private readonly int _maxDepth;

        public RelationAnalyzer()
            : this(4)
        {
        }

        public RelationAnalyzer(int maxDepth)
        {
            if (maxDepth < 0)
            {
                throw new ArgumentOutOfRangeException("maxDepth");
            }

            _maxDepth = maxDepth;
        }

        public RelationGraph Analyze(int value)
        {
            RelationGraph graph = new RelationGraph();

            AnalyzeValue(
                value,
                0,
                new HashSet<int>(),
                graph);

            return graph;
        }

        public RelationGraph AnalyzeRelation(
            int left,
            int right)
        {
            RelationGraph graph = new RelationGraph();

            AnalyzeValue(
                left,
                0,
                new HashSet<int>(),
                graph);

            AnalyzeValue(
                right,
                0,
                new HashSet<int>(),
                graph);

            return graph;
        }

        private void AnalyzeValue(
            int value,
            int depth,
            HashSet<int> path,
            RelationGraph graph)
        {
            graph.AddNode(value);

            if (depth >= _maxDepth)
            {
                return;
            }

            if (path.Contains(value))
            {
                return;
            }

            HashSet<int> nextPath =
                new HashSet<int>(path);

            nextPath.Add(value);

            AnalyzeDigitSum(
                value,
                depth,
                nextPath,
                graph);

            AnalyzeDigitDifference(
                value,
                depth,
                nextPath,
                graph);
        }

        private void AnalyzeDigitSum(
            int value,
            int depth,
            HashSet<int> path,
            RelationGraph graph)
        {
            int absoluteValue = Math.Abs(value);

            if (absoluteValue < 10)
            {
                return;
            }

            int sum = 0;
            int remaining = absoluteValue;

            while (remaining > 0)
            {
                sum += remaining % 10;
                remaining /= 10;
            }

            RelationDerivation derivation =
                new RelationDerivation(
                    value,
                    "DigitSum",
                    sum,
                    depth + 1);

            graph.AddDerivation(derivation);

            AnalyzeValue(
                sum,
                depth + 1,
                path,
                graph);
        }

        private void AnalyzeDigitDifference(
            int value,
            int depth,
            HashSet<int> path,
            RelationGraph graph)
        {
            int absoluteValue = Math.Abs(value);

            if (absoluteValue < 10)
            {
                return;
            }

            if (absoluteValue > 99)
            {
                return;
            }

            int tens = absoluteValue / 10;
            int ones = absoluteValue % 10;

            int difference = Math.Abs(tens - ones);

            RelationDerivation derivation =
                new RelationDerivation(
                    value,
                    "DigitDifference",
                    difference,
                    depth + 1);

            graph.AddDerivation(derivation);

            AnalyzeValue(
                difference,
                depth + 1,
                path,
                graph);
        }
    }
}