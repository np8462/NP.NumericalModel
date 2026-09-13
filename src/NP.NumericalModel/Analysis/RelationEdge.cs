using System;

namespace NP.NumericalModel.Analysis
{
    public class RelationEdge
    {
        public RelationNode Source { get; private set; }
        public RelationNode Target { get; private set; }
        public string Rule { get; private set; }
        public int Depth { get; private set; }

        public RelationEdge(
            RelationNode source,
            RelationNode target,
            string rule,
            int depth)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            Source = source;
            Target = target;
            Rule = rule;
            Depth = depth;
        }

        public override string ToString()
        {
            return Source.Expression
                + " --"
                + Rule
                + "--> "
                + Target.Expression;
        }
    }
}