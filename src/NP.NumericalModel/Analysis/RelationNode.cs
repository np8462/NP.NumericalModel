using System;

namespace NP.NumericalModel.Analysis
{
    public class RelationNode
    {
        public string Expression { get; private set; }
        public string Kind { get; private set; }

        public RelationNode(
            string expression,
            string kind)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (kind == null)
            {
                throw new ArgumentNullException("kind");
            }

            Expression = expression;
            Kind = kind;
        }

        public override string ToString()
        {
            return Expression;
        }
    }
}