using System;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Represents one analytical state generated from a numerical value.
    ///
    /// A state preserves its textual structure and its kind.
    /// It is not itself a mathematical operation.
    /// </summary>
    public class AnalysisState
    {
        public int SourceValue { get; private set; }

        public string Representation { get; private set; }

        public string Kind { get; private set; }

        public AnalysisState(
            int sourceValue,
            string representation,
            string kind)
        {
            if (representation == null)
            {
                throw new ArgumentNullException("representation");
            }

            if (kind == null)
            {
                throw new ArgumentNullException("kind");
            }

            SourceValue = sourceValue;
            Representation = representation;
            Kind = kind;
        }

        public override string ToString()
        {
            return Representation;
        }
    }
}