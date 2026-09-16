using System;

namespace NP.NumericalModel.Interpretation
{
    public class ConceptualRelation
    {
        public NumericalConcept Source { get; private set; }

        public NumericalConcept Target { get; private set; }

        public string RelationType { get; private set; }

        public string Symbol { get; private set; }

        public string Interpretation { get; private set; }

        public ConceptualRelation(
            NumericalConcept source,
            NumericalConcept target,
            string relationType,
            string symbol,
            string interpretation)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (relationType == null)
            {
                throw new ArgumentNullException("relationType");
            }

            Source = source;
            Target = target;
            RelationType = relationType;
            Symbol = symbol;
            Interpretation = interpretation;
        }

        public override string ToString()
        {
            return Source.Symbol
                + " --"
                + RelationType
                + "--> "
                + Target.Symbol;
        }
    }
}