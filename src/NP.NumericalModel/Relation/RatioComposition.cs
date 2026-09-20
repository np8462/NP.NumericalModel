using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Relation
{
    /// <summary>
    /// Represents a composed ratio together with the ratio components
    /// that produced it.
    /// </summary>
    public class RatioComposition
    {
        private readonly List<Ratio> components;

        public IList<Ratio> Components
        {
            get { return components.AsReadOnly(); }
        }

        public Ratio Result { get; private set; }

        public RatioComposition(params Ratio[] components)
        {
            if (components == null)
                throw new ArgumentNullException("components");

            if (components.Length == 0)
                throw new ArgumentException(
                    "At least one ratio component is required.",
                    "components");

            this.components = new List<Ratio>();

            int i;
            for (i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                    throw new ArgumentException(
                        "Ratio components cannot be null.",
                        "components");

                this.components.Add(components[i]);
            }

            RatioComposer composer = new RatioComposer();
            Result = composer.Combine(components);
        }

        public bool Contains(Ratio ratio)
        {
            if (ratio == null)
                return false;

            int i;
            for (i = 0; i < components.Count; i++)
            {
                if (components[i].Numerator == ratio.Numerator
                    && components[i].Denominator == ratio.Denominator)
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return Result.ToString();
        }
    }
}
