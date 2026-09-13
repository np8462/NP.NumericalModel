using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Generates analytical states for a numerical value.
    ///
    /// The expected number of states is based on:
    ///
    ///     n! - n + 1
    ///
    /// where n is the number of digits.
    ///
    /// For the current three-digit model, the defined states are:
    ///
    /// 252
    /// 25
    /// 52
    /// 2:5:2
    ///
    /// More general state-generation rules can be added later
    /// without changing the analyzer architecture.
    /// </summary>
    public class FactorialStateGenerator
    {
        public int GetExpectedStateCount(int digitCount)
        {
            if (digitCount < 1)
            {
                throw new ArgumentOutOfRangeException("digitCount");
            }

            long factorial = 1;
            int i;

            for (i = 2; i <= digitCount; i++)
            {
                factorial *= i;
            }

            long count = factorial - digitCount + 1;

            if (count > Int32.MaxValue)
            {
                throw new OverflowException(
                    "The calculated state count is too large.");
            }

            return (int)count;
        }

        public List<AnalysisState> Generate(int value)
        {
            List<AnalysisState> states =
                new List<AnalysisState>();

            string text = Math.Abs(value).ToString();

            int digitCount = text.Length;

            if (digitCount == 1)
            {
                states.Add(
                    new AnalysisState(
                        value,
                        text,
                        "SingleDigit"));

                return states;
            }

            if (digitCount == 2)
            {
                states.Add(
                    new AnalysisState(
                        value,
                        text,
                        "Full"));

                return states;
            }

            if (digitCount == 3)
            {
                string firstTwo =
                    text.Substring(0, 2);

                string lastTwo =
                    text.Substring(1, 2);

                states.Add(
                    new AnalysisState(
                        value,
                        text,
                        "Full"));

                states.Add(
                    new AnalysisState(
                        value,
                        firstTwo,
                        "TwoDigitLeft"));

                states.Add(
                    new AnalysisState(
                        value,
                        lastTwo,
                        "TwoDigitRight"));

                states.Add(
                    new AnalysisState(
                        value,
                        text[0].ToString()
                            + ":"
                            + text[1].ToString()
                            + ":"
                            + text[2].ToString(),
                        "FullRelation"));

                return states;
            }

            /*
             * For four or more digits we deliberately do not
             * invent the state-generation semantics yet.
             *
             * The factorial count is already defined, but the
             * exact meaning of every generated state must be
             * agreed before implementation.
             */
            throw new NotSupportedException(
                "State generation for more than three digits " +
                "has not been defined yet.");
        }
    }
}