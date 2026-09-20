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
        private readonly string operation;
        private readonly int precedence;

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
            precedence = 3;
        }

        private NumericalExpression(
            NumericalExpression left,
            NumericalExpression right,
            Func<double, double, double> function,
            string operation,
            string text,
            int precedence)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            if (function == null) throw new ArgumentNullException("function");

            this.left = left;
            this.right = right;
            binaryFunction = function;
            this.operation = operation;
            this.text = text;
            this.precedence = precedence;
        }

        private NumericalExpression(
            NumericalExpression operand,
            Func<double, double> function,
            string operation,
            string text)
        {
            if (operand == null) throw new ArgumentNullException("operand");
            if (function == null) throw new ArgumentNullException("function");

            left = operand;
            unaryFunction = function;
            this.operation = operation;
            this.text = text;
            precedence = 3;
        }

        public static NumericalExpression Constant(double value, string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            return new NumericalExpression(value, text);
        }

        public NumericalExpression Add(NumericalExpression other)
        {
            return Binary(other, "+", delegate(double a, double b) { return a + b; }, 1);
        }

        public NumericalExpression Subtract(NumericalExpression other)
        {
            return Binary(other, "-", delegate(double a, double b) { return a - b; }, 1);
        }

        public NumericalExpression Multiply(NumericalExpression other)
        {
            return Binary(other, "*", delegate(double a, double b) { return a * b; }, 2);
        }

        public NumericalExpression Divide(NumericalExpression other)
        {
            return Binary(other, "/", delegate(double a, double b) { return a / b; }, 2);
        }

        private NumericalExpression Binary(
            NumericalExpression other,
            string operation,
            Func<double, double, double> function,
            int precedence)
        {
            if (other == null) throw new ArgumentNullException("other");

            string leftText = FormatOperand(this, false, operation);
            string rightText = FormatOperand(other, true, operation);

            return new NumericalExpression(
                this,
                other,
                function,
                operation,
                "(" + leftText + " " + operation + " " + rightText + ")",
                precedence);
        }

        private static string FormatOperand(
            NumericalExpression operand,
            bool isRight,
            string parentOperation)
        {
            bool needsParentheses = operand.precedence < GetPrecedence(parentOperation);

            if (!needsParentheses && isRight && operand.precedence == GetPrecedence(parentOperation))
            {
                if (parentOperation == "-" || parentOperation == "/")
                    needsParentheses = true;
            }

            if (needsParentheses)
                return "(" + operand.Text + ")";

            return RemoveOuterParentheses(operand.Text);
        }

        private static int GetPrecedence(string operation)
        {
            if (operation == "+" || operation == "-")
                return 1;

            return 2;
        }

        private static string RemoveOuterParentheses(string value)
        {
            if (value == null || value.Length < 2)
                return value;

            if (value[0] != '(' || value[value.Length - 1] != ')')
                return value;

            int depth = 0;
            int i;
            for (i = 0; i < value.Length; i++)
            {
                if (value[i] == '(')
                    depth++;
                else if (value[i] == ')')
                    depth--;

                if (depth == 0 && i < value.Length - 1)
                    return value;
            }

            return value.Substring(1, value.Length - 2);
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
                operation,
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
