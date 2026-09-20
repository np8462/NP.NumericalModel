using System;
using NP.NumericalModel.Relation;

namespace NP.NumericalModel.Interpretation
{
    /// <summary>
    /// Represents a real numeric value together with an optional expression
    /// and unit. The value is mathematical data; the expression and unit
    /// preserve how that value was represented without assigning meaning to it.
    /// </summary>
    public class RealValue
    {
        public double Value { get; private set; }
        public string Expression { get; private set; }
        public string Unit { get; private set; }

        public RealValue(double value)
            : this(value, value.ToString(), null)
        {
        }

        public RealValue(double value, string expression)
            : this(value, expression, null)
        {
        }

        public RealValue(double value, string expression, string unit)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            Value = value;
            Expression = expression;
            Unit = unit;
        }

        public static RealValue FromRatio(Ratio ratio)
        {
            if (ratio == null)
                throw new ArgumentNullException("ratio");

            return new RealValue(ratio.DecimalValue, ratio.ToString());
        }

        public static RealValue FromExpression(
            string expression,
            double value)
        {
            return new RealValue(value, expression);
        }

        public RealValue Apply(
            string operation,
            Func<double, double> function)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            if (function == null)
                throw new ArgumentNullException("function");

            return new RealValue(
                function(Value),
                operation + "(" + Expression + ")",
                Unit);
        }

        public override string ToString()
        {
            if (Unit == null || Unit.Length == 0)
                return Expression + " = " + Value.ToString();

            return Expression + " = " + Value.ToString() + " " + Unit;
        }
    }
}
