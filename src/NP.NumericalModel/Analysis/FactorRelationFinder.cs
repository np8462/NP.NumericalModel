using System;
using System.Collections.Generic;

namespace NP.NumericalModel.Analysis
{
    /// <summary>
    /// Finds integer factor relations for a value.
    ///
    /// Every discovered factor pair is preserved.
    /// No factor pair is considered the unique meaning of the value.
    /// </summary>
    public class FactorRelationFinder
    {
        public List<FactorRelation> Find(int value)
        {
            List<FactorRelation> result =
                new List<FactorRelation>();

            int absoluteValue = Math.Abs(value);

            if (absoluteValue < 2)
            {
                return result;
            }

            int left;

            for (left = 1;
                 left <= absoluteValue / left;
                 left++)
            {
                if (absoluteValue % left != 0)
                {
                    continue;
                }

                int right =
                    absoluteValue / left;

                result.Add(
                    new FactorRelation(
                        value,
                        left,
                        right));
            }

            return result;
        }
    }
}