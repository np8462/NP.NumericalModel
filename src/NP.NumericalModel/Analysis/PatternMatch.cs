namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Describes a simple numerical pattern detected in a derived relation.
    /// </summary>
    public class PatternMatch
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsMatch { get; private set; }

        public PatternMatch(string name, string description, bool isMatch)
        {
            Name = name;
            Description = description;
            IsMatch = isMatch;
        }
    }
}
