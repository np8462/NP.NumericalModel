using System;

namespace NP.NumericalModel.Interpretation
{
    public class NumericalRepresentation
    {
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public string Type { get; private set; }
        public string Meaning { get; private set; }

        public NumericalRepresentation(
            string name,
            string symbol,
            string type,
            string meaning)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (symbol == null)
                throw new ArgumentNullException("symbol");

            if (type == null)
                throw new ArgumentNullException("type");

            if (meaning == null)
                throw new ArgumentNullException("meaning");

            Name = name;
            Symbol = symbol;
            Type = type;
            Meaning = meaning;
        }

        public override string ToString()
        {
            return Symbol + " = " + Meaning;
        }
    }
}