namespace NP.NumericalModel.Structure
{
    /// <summary>
    /// A structural separator used by NP.NumericalModel.
    ///
    /// The separator itself does not imply a conventional
    /// mathematical operation such as decimal point or division.
    /// Its interpretation belongs to the structural model.
    /// </summary>
    public class StructuralSeparator
    {
        public string Symbol { get; private set; }

        public StructuralSeparator(string symbol)
        {
            Symbol = symbol;
        }

        public override string ToString()
        {
            return Symbol;
        }

        public static StructuralSeparator Dot
        {
            get { return new StructuralSeparator("."); }
        }

        public static StructuralSeparator Colon
        {
            get { return new StructuralSeparator(":"); }
        }
    }
}