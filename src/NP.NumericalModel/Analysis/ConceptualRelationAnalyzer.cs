using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    public class ConceptualRelationAnalyzer
    {
        private readonly FactorRelationFinder _factorFinder;
        private readonly RelationAnalyzer _relationAnalyzer;
        private readonly ConceptualRelationRule _conceptualRule;

        private readonly ConceptualRelationGraph _graph;

        private readonly int _maxDepth;

        public ConceptualRelationGraph Graph
        {
            get { return _graph; }
        }

        public ConceptualRelationAnalyzer()
            : this(6)
        {
        }

        public ConceptualRelationAnalyzer(
            int maxDepth)
        {
            if (maxDepth < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "maxDepth");
            }

            _maxDepth = maxDepth;

            _factorFinder =
                new FactorRelationFinder();

            _relationAnalyzer =
                new RelationAnalyzer(maxDepth);

            _conceptualRule =
                new ConceptualRelationRule(
                    "ConceptualAggregation");

            _graph =
                new ConceptualRelationGraph();
        }

        public ConceptualRelationGraph Analyze(
            int value)
        {
            HashSet<string> active =
                new HashSet<string>(
                    StringComparer.Ordinal);

            AnalyzeValue(
                value,
                0,
                active);

            return _graph;
        }

        private void AnalyzeValue(
            int value,
            int depth,
            HashSet<string> active)
        {
            string expression =
                value.ToString();

            _graph.GetOrAddNode(
                expression,
                "Value");

            if (depth >= _maxDepth)
            {
                return;
            }

            /*
             * جلوگیری از recursion loop
             */
            if (active.Contains(expression))
            {
                return;
            }

            active.Add(expression);

            List<FactorRelation> factors =
                _factorFinder.Find(value);

            foreach (FactorRelation factor in factors)
            {
                string relation =
                    factor.GetRepresentation();

                _graph.AddEdge(
                    expression,
                    "Value",
                    relation,
                    "FactorRelation",
                    "FactorRelation",
                    depth);

                AnalyzePair(
                    factor.Left,
                    factor.Right,
                    relation,
                    depth + 1,
                    active);
            }

            active.Remove(expression);
        }

        private void AnalyzePair(
            int left,
            int right,
            string relationExpression,
            int depth,
            HashSet<string> active)
        {
            if (depth > _maxDepth)
            {
                return;
            }

            /*
             * تحلیل سمت چپ
             */
            int leftSum =
                GetDigitSum(left);

            /*
             * تحلیل سمت راست
             */
            int rightSum =
                GetDigitSum(right);

            string leftExpression =
                left.ToString();

            string rightExpression =
                right.ToString();

            _graph.GetOrAddNode(
                leftExpression,
                "Value");

            _graph.GetOrAddNode(
                rightExpression,
                "Value");

            /*
             * left -> digit sum
             */
            _graph.AddEdge(
                relationExpression,
                "FactorRelation",
                leftExpression,
                "PairLeft",
                "PairLeft",
                depth);

            _graph.AddEdge(
                leftExpression,
                "PairLeft",
                leftSum.ToString(),
                "Value",
                "DigitSum",
                depth);

            /*
             * right -> digit sum
             */
            _graph.AddEdge(
                relationExpression,
                "FactorRelation",
                rightExpression,
                "PairRight",
                "PairRight",
                depth);

            _graph.AddEdge(
                rightExpression,
                "PairRight",
                rightSum.ToString(),
                "Value",
                "DigitSum",
                depth);

            /*
             * ساخت relation جدید:
             *
             * 12 -> 3
             * 21 -> 3
             *
             * بنابراین:
             *
             * 3:3
             */
            string resultRelation =
                leftSum.ToString()
                + ":"
                + rightSum.ToString();

            _graph.AddEdge(
                relationExpression,
                "FactorRelation",
                resultRelation,
                "DerivedRelation",
                "ComposeDigitSums",
                depth);

            /*
             * خروجی عددی استاندارد:
             *
             * 3 + 3 = 6
             *
             * ولی آن را از نتیجه مفهومی جدا نگه می‌داریم.
             */
            int numericResult =
                leftSum + rightSum;

            _graph.AddEdge(
                resultRelation,
                "DerivedRelation",
                numericResult.ToString(),
                "NumericResult",
                "NumericSum",
                depth);

            /*
             * خروجی مفهومی:
             *
             * 3:3 -> 4
             */
            int conceptualResult;

            if (_conceptualRule.TryApply(
                leftSum,
                rightSum,
                out conceptualResult))
            {
                _graph.AddEdge(
                    resultRelation,
                    "DerivedRelation",
                    conceptualResult.ToString(),
                    "ConceptualResult",
                    _conceptualRule.Name,
                    depth);

                /*
                 * خود 4 دوباره قابل تحلیل است.
                 */
                AnalyzeValue(
                    conceptualResult,
                    depth + 1,
                    active);
            }
        }

        private int GetDigitSum(
            int value)
        {
            int absoluteValue =
                Math.Abs(value);

            int sum = 0;

            if (absoluteValue == 0)
            {
                return 0;
            }

            while (absoluteValue > 0)
            {
                sum += absoluteValue % 10;
                absoluteValue /= 10;
            }

            return sum;
        }
    }
}