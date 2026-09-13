using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    public class RelationEngine
    {
        private readonly RelationAnalyzer _relationAnalyzer;
        private readonly FactorRelationFinder _factorFinder;
        private readonly FactorialStateGenerator _stateGenerator;
        private readonly ConceptualRelationRule _conceptualRule;

        private readonly ConceptualRelationGraph _graph;

        private readonly int _maxDepth;

        public ConceptualRelationGraph Graph
        {
            get { return _graph; }
        }

        public RelationEngine()
            : this(6)
        {
        }

        public RelationEngine(int maxDepth)
        {
            if (maxDepth < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "maxDepth");
            }

            _maxDepth = maxDepth;

            _relationAnalyzer =
                new RelationAnalyzer(maxDepth);

            _factorFinder =
                new FactorRelationFinder();

            _stateGenerator =
                new FactorialStateGenerator();

            _conceptualRule =
                new ConceptualRelationRule(
                    "ConceptualAggregation");

            _graph =
                new ConceptualRelationGraph();
        }

        public ConceptualRelationGraph Analyze(int value)
        {
            HashSet<string> activePath =
                new HashSet<string>(
                    StringComparer.Ordinal);

            AnalyzeValue(
                value,
                0,
                activePath);

            return _graph;
        }

        private void AnalyzeValue(
            int value,
            int depth,
            HashSet<string> activePath)
        {
            if (depth > _maxDepth)
            {
                return;
            }

            string expression =
                value.ToString();

            _graph.GetOrAddNode(
                expression,
                "Value");

            /*
             * اگر همین expression در مسیر فعلی باشد،
             * وارد چرخه شده‌ایم.
             */
            if (activePath.Contains(expression))
            {
                return;
            }

            activePath.Add(expression);

            try
            {
                AnalyzeBasicRelations(
                    value,
                    depth,
                    activePath);

                AnalyzeFactors(
                    value,
                    depth,
                    activePath);

                AnalyzeStates(
                    value,
                    depth,
                    activePath);
            }
            finally
            {
                activePath.Remove(expression);
            }
        }

        private void AnalyzeBasicRelations(
            int value,
            int depth,
            HashSet<string> activePath)
        {
            RelationGraph graph =
                _relationAnalyzer.Analyze(value);

            foreach (RelationDerivation derivation
                in graph.Derivations)
            {
                _graph.AddEdge(
                    derivation.SourceValue.ToString(),
                    "Value",
                    derivation.ResultValue.ToString(),
                    "DerivedValue",
                    derivation.Rule,
                    depth);

                /*
                 * نتیجه‌ی RelationAnalyzer نیز می‌تواند
                 * خودش وارد تحلیل بعدی شود.
                 */
                AnalyzeValue(
                    derivation.ResultValue,
                    depth + 1,
                    activePath);
            }
        }

        private void AnalyzeFactors(
            int value,
            int depth,
            HashSet<string> activePath)
        {
            List<FactorRelation> factors =
                _factorFinder.Find(value);

            foreach (FactorRelation factor in factors)
            {
                string relationExpression =
                    factor.GetRepresentation();

                _graph.AddEdge(
                    value.ToString(),
                    "Value",
                    relationExpression,
                    "FactorRelation",
                    "FactorRelation",
                    depth);

                AnalyzeFactorRelation(
                    factor,
                    depth + 1,
                    activePath);
            }
        }

        private void AnalyzeFactorRelation(
            FactorRelation factor,
            int depth,
            HashSet<string> activePath)
        {
            if (depth > _maxDepth)
            {
                return;
            }

            int leftSum =
                GetDigitSum(factor.Left);

            int rightSum =
                GetDigitSum(factor.Right);

            string relationExpression =
                factor.GetRepresentation();

            /*
             * Left
             */
            _graph.AddEdge(
                relationExpression,
                "FactorRelation",
                factor.Left.ToString(),
                "PairLeft",
                "PairLeft",
                depth);

            _graph.AddEdge(
                factor.Left.ToString(),
                "PairLeft",
                leftSum.ToString(),
                "Value",
                "DigitSum",
                depth);

            /*
             * Right
             */
            _graph.AddEdge(
                relationExpression,
                "FactorRelation",
                factor.Right.ToString(),
                "PairRight",
                "PairRight",
                depth);

            _graph.AddEdge(
                factor.Right.ToString(),
                "PairRight",
                rightSum.ToString(),
                "Value",
                "DigitSum",
                depth);

            /*
             * ساخت رابطه‌ی جدید:
             *
             * 12 + 21
             *      ↓
             * 3 : 3
             */
            string derivedRelation =
                leftSum.ToString()
                + ":"
                + rightSum.ToString();

            _graph.AddEdge(
                relationExpression,
                "FactorRelation",
                derivedRelation,
                "DerivedRelation",
                "ComposeDigitSums",
                depth);

            /*
             * خروجی عددی استاندارد
             */
            int numericResult =
                leftSum + rightSum;

            _graph.AddEdge(
                derivedRelation,
                "DerivedRelation",
                numericResult.ToString(),
                "NumericResult",
                "NumericSum",
                depth);

            /*
             * خروجی مفهومی
             */
            int conceptualResult;

            if (_conceptualRule.TryApply(
                leftSum,
                rightSum,
                out conceptualResult))
            {
                _graph.AddEdge(
                    derivedRelation,
                    "DerivedRelation",
                    conceptualResult.ToString(),
                    "ConceptualResult",
                    _conceptualRule.Name,
                    depth);

                /*
                 * نتیجه‌ی مفهومی نیز یک مقدار واقعی
                 * برای ادامه‌ی تحلیل Engine است.
                 */
                AnalyzeValue(
                    conceptualResult,
                    depth + 1,
                    activePath);
            }
        }

        private void AnalyzeStates(
            int value,
            int depth,
            HashSet<string> activePath)
        {
            if (depth >= _maxDepth)
            {
                return;
            }

            List<AnalysisState> states;

            try
            {
                states =
                    _stateGenerator.Generate(value);
            }
            catch (NotSupportedException)
            {
                /*
                 * فعلاً Stateهای بیشتر از سه رقم
                 * توسط Generator تعریف نشده‌اند.
                 *
                 * Engine نباید به خاطر آن متوقف شود.
                 */
                return;
            }

            foreach (AnalysisState state in states)
            {
                if (state.Representation == value.ToString())
                {
                    continue;
                }

                _graph.AddEdge(
                    value.ToString(),
                    "Value",
                    state.Representation,
                    "AnalysisState",
                    "State",
                    depth);

                int stateValue;

                if (Int32.TryParse(
                    state.Representation,
                    out stateValue))
                {
                    AnalyzeValue(
                        stateValue,
                        depth + 1,
                        activePath);
                }
            }
        }

        private int GetDigitSum(int value)
        {
            int absoluteValue =
                Math.Abs(value);

            if (absoluteValue == 0)
            {
                return 0;
            }

            int sum = 0;

            while (absoluteValue > 0)
            {
                sum += absoluteValue % 10;
                absoluteValue /= 10;
            }

            return sum;
        }
    }
}