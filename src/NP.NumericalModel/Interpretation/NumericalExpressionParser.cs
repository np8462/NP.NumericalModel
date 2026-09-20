using System;
using System.Globalization;

namespace NP.NumericalModel.Interpretation
{
    /// <summary>
    /// Parses and evaluates a small general numerical expression language.
    /// Supported: +, -, *, /, :, parentheses, pi, and sin/cos/tan.
    /// A colon is treated as a numeric ratio/division operator.
    /// </summary>
    public class NumericalExpressionParser
    {
        private string expression;
        private int position;

        public NumericalExpression Parse(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (text.Trim().Length == 0)
                throw new ArgumentException("Expression cannot be empty.", "text");

            expression = text;
            position = 0;

            NumericalExpression result = ParseExpression();
            SkipWhitespace();

            if (position != expression.Length)
                throw Error("Unexpected token.");

            return result;
        }

        private NumericalExpression ParseExpression()
        {
            NumericalExpression result = ParseTerm();

            while (true)
            {
                SkipWhitespace();

                if (Match('+'))
                    result = result.Add(ParseTerm());
                else if (Match('-'))
                    result = result.Subtract(ParseTerm());
                else
                    return result;
            }
        }

        private NumericalExpression ParseTerm()
        {
            NumericalExpression result = ParseFactor();

            while (true)
            {
                SkipWhitespace();

                if (Match('*'))
                    result = result.Multiply(ParseFactor());
                else if (Match('/') || Match(':'))
                    result = result.Divide(ParseFactor());
                else
                    return result;
            }
        }

        private NumericalExpression ParseFactor()
        {
            SkipWhitespace();

            if (Match('+'))
                return ParseFactor();

            if (Match('-'))
            {
                NumericalExpression zero =
                    NumericalExpression.Constant(0.0, "0");
                return zero.Subtract(ParseFactor());
            }

            if (Match('('))
            {
                NumericalExpression result = ParseExpression();
                Expect(')');
                return result;
            }

            if (IsIdentifierStart(Current()))
            {
                string name = ParseIdentifier();

                if (name.Equals("pi", StringComparison.OrdinalIgnoreCase))
                    return NumericalExpression.Constant(Math.PI, name);

                SkipWhitespace();

                if (Match('('))
                {
                    NumericalExpression argument = ParseExpression();
                    Expect(')');

                    if (name.Equals("sin", StringComparison.OrdinalIgnoreCase))
                        return argument.Apply("sin", Math.Sin);

                    if (name.Equals("cos", StringComparison.OrdinalIgnoreCase))
                        return argument.Apply("cos", Math.Cos);

                    if (name.Equals("tan", StringComparison.OrdinalIgnoreCase))
                        return argument.Apply("tan", Math.Tan);

                    throw Error("Unknown function: " + name);
                }

                throw Error("Unknown identifier: " + name);
            }

            return ParseNumber();
        }

        private NumericalExpression ParseNumber()
        {
            SkipWhitespace();

            int start = position;
            bool hasDigits = false;

            while (char.IsDigit(Current()))
            {
                position++;
                hasDigits = true;
            }

            if (Current() == '.')
            {
                position++;

                while (char.IsDigit(Current()))
                {
                    position++;
                    hasDigits = true;
                }
            }

            if (!hasDigits)
                throw Error("Number expected.");

            string token = expression.Substring(start, position - start);
            double value;

            if (!double.TryParse(
                token,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value))
            {
                throw Error("Invalid number: " + token);
            }

            return NumericalExpression.Constant(value, token);
        }

        private string ParseIdentifier()
        {
            int start = position;

            while (char.IsLetter(Current()))
                position++;

            return expression.Substring(start, position - start);
        }

        private bool IsIdentifierStart(char value)
        {
            return char.IsLetter(value);
        }

        private char Current()
        {
            if (position >= expression.Length)
                return '\0';

            return expression[position];
        }

        private bool Match(char value)
        {
            if (Current() != value)
                return false;

            position++;
            return true;
        }

        private void Expect(char value)
        {
            if (!Match(value))
                throw Error("Expected '" + value + "'.");
        }

        private void SkipWhitespace()
        {
            while (char.IsWhiteSpace(Current()))
                position++;
        }

        private FormatException Error(string message)
        {
            return new FormatException(
                message + " Position: " + position.ToString());
        }
    }
}
