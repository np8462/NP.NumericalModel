using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Interpretation
{
    public class RelationChain
    {
        private readonly List<NumericalConcept> _concepts;
        private readonly List<ConceptualRelation> _relations;

        public IList<NumericalConcept> Concepts
        {
            get { return _concepts.AsReadOnly(); }
        }

        public IList<ConceptualRelation> Relations
        {
            get { return _relations.AsReadOnly(); }
        }

        public RelationChain()
        {
            _concepts = new List<NumericalConcept>();
            _relations = new List<ConceptualRelation>();
        }

        public void AddConcept(NumericalConcept concept)
        {
            if (concept == null)
            {
                throw new ArgumentNullException("concept");
            }

            _concepts.Add(concept);
        }

        public void AddRelation(ConceptualRelation relation)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }

            _relations.Add(relation);
        }
    }
}