using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Interpretation
{
    public class NumericalConcept
    {
        private readonly List<string> _meanings;

        public string Symbol { get; private set; }

        public int? NumericValue { get; private set; }

        public IList<string> Meanings
        {
            get { return _meanings.AsReadOnly(); }
        }

        public NumericalConcept(
            string symbol,
            int? numericValue)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException("symbol");
            }

            if (symbol.Length == 0)
            {
                throw new ArgumentException(
                    "Symbol cannot be empty.",
                    "symbol");
            }

            Symbol = symbol;
            NumericValue = numericValue;

            _meanings = new List<string>();
        }

        public void AddMeaning(string meaning)
        {
            if (meaning == null)
            {
                throw new ArgumentNullException("meaning");
            }

            _meanings.Add(meaning);
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}