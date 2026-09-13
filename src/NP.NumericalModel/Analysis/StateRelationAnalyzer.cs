using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Coordinates state generation and relation analysis.
    ///
    /// This class is intentionally small in the first version.
    /// It provides the bridge between factorial state generation
    /// and the existing RelationAnalyzer.
    /// </summary>
    public class StateRelationAnalyzer
    {
        private readonly FactorialStateGenerator _stateGenerator;
        private readonly RelationAnalyzer _relationAnalyzer;

        public StateRelationAnalyzer()
            : this(4)
        {
        }

        public StateRelationAnalyzer(int maxDepth)
        {
            _stateGenerator =
                new FactorialStateGenerator();

            _relationAnalyzer =
                new RelationAnalyzer(maxDepth);
        }

        public List<AnalysisState> GenerateStates(int value)
        {
            return _stateGenerator.Generate(value);
        }

        public RelationGraph AnalyzeState(int value)
        {
            return _relationAnalyzer.Analyze(value);
        }

        public int GetExpectedStateCount(int digitCount)
        {
            return _stateGenerator.GetExpectedStateCount(
                digitCount);
        }
    }
}