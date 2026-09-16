using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Interpretation
{
    public class InterpretationDefinition
    {
        private readonly List<NumericalConcept> _concepts;
        private readonly List<ConceptualRelation> _relations;
        private readonly List<RelationChain> _chains;

        public string Name { get; private set; }

        public string Version { get; private set; }

        public IList<NumericalConcept> Concepts
        {
            get { return _concepts.AsReadOnly(); }
        }

        public IList<ConceptualRelation> Relations
        {
            get { return _relations.AsReadOnly(); }
        }

        public IList<RelationChain> Chains
        {
            get { return _chains.AsReadOnly(); }
        }

        public InterpretationDefinition(
            string name,
            string version)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (version == null)
            {
                throw new ArgumentNullException("version");
            }

            Name = name;
            Version = version;

            _concepts = new List<NumericalConcept>();
            _relations = new List<ConceptualRelation>();
            _chains = new List<RelationChain>();
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

        public void AddChain(RelationChain chain)
        {
            if (chain == null)
            {
                throw new ArgumentNullException("chain");
            }

            _chains.Add(chain);
        }
    }
}