using System;

namespace NP.NumericalModel.Interpretation
{
    /// <summary>
    /// Represents a numerical expression and its evaluated value.
    /// </summary>
    public class NumericalExpression
    {
        private readonly NumericalExpression left;
        private readonly NumericalExpression right;
        private readonly Func<double, double, double> binaryFunction;
        private readonly Func<double, double> unaryFunction;
        private readonly double constantValue;
        private readonly string text;

        public string Text { get { return text; } }

        public double Value
        {
            get
            {
                if (unaryFunction != null)
                    return unaryFunction(left.Value);

                if (binaryFunction != null)
                    return binaryFunction(left.Value, right.Value);

                return constantValue;
            }
        }

        private NumericalExpression(double value, string text)
        {
            constantValue = value;
            this.text = text;
        }

        private NumericalExpression(
            NumericalExpression left,
            NumericalExpression right,
            Func<double, double> function,
            string text)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            if (function == null) throw new ArgumentNullException("function");

            this.left = left;
            this.right = right;
            binaryFunction = function;
            this.text = text;
        }

        private NumericalExpression(
            NumericalExpression operand,
            Func<double, double> function,
            string text)
        {
            if (operand == null) throw new ArgumentNullException("operand");
            if (function == null) throw new ArgumentNullException("function");

            left = operand;
            unaryFunction = function;
            this.text = text;
        }

        public static NumericalExpression Constant(double value, string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            return new NumericalExpression(value, text);
        }

        public NumericalExpression Add(NumericalExpression other)
        {
            return Binary(other, "+", delegate(double a, double b) { return a + b; });
        }

        public NumericalExpression Subtract(NumericalExpression other)
        {
            return Binary(other, "-", delegate(double a, double b) { return a - b; });
        }

        public NumericalExpression Multiply(NumericalExpression other)
        {
            return Binary(other, "*", delegate(double a, double b) { return a * b; });
        }

        public NumericalExpression Divide(NumericalExpression other)
        {
            return Binary(other, "/", delegate(double a, double b) { return a / b; });
        }

        private NumericalExpression Binary(
            NumericalExpression other,
            string operation,
            Func<double, double, double> function)
        {
            if (other == null) throw new ArgumentNullException("other");

            return new NumericalExpression(
                this,
                other,
                function,
                "(" + Text + " " + operation + " " + other.Text + ")");
        }

        public NumericalExpression Apply(
            string operation,
            Func<double, double> function)
        {
            if (operation == null) throw new ArgumentNullException("operation");
            if (function == null) throw new ArgumentNullException("function");

            return new NumericalExpression(
                this,
                function,
                operation + "(" + Text + ")");
        }

        public RealValue ToRealValue()
        {
            return new RealValue(Value, Text);
        }

        public override string ToString()
        {
            return Text + " = " + Value.ToString();
        }
    }
}
