# Pattern Analyzer

The Pattern Analyzer is the first analysis stage after derived relations.

Current flow:

    Ratio
      |
      v
    RatioDecomposition
      |
      v
    DerivedRelation
      |
      v
    PatternAnalyzer
      |
      v
    PatternMatch

The pattern rules are intentionally structural rather than conceptual.

## ExactDivision

For a ratio such as 42:7, the derived relation has a zero remainder.

The analyzer reports:

    Name: ExactDivision
    IsMatch: true

## RemainderDivision

For a ratio such as 22:7, the derived relation contains a whole part and a
non-zero remainder:

    3 + 1/7

The analyzer reports:

    Name: RemainderDivision
    IsMatch: true

The same structural rule applies to 5:3:

    1 + 2/3

## Numerator reconstruction pattern

The analyzer verifies the structural relationship between the four values in a
derived relation:

    Numerator = WholePart * Denominator + Remainder

For 22:7:

    22 = 3 * 7 + 1

For 42:7:

    42 = 6 * 7 + 0

The analyzer also checks that the absolute remainder is smaller than the
absolute denominator.

The result is reported as NumeratorReconstruction.

An intentionally inconsistent relation such as:

    Numerator = 22
    Denominator = 7
    WholePart = 3
    Remainder = 2

is rejected because:

    3 * 7 + 2 = 23

## Combined ratio decomposition consistency

The next pattern combines the existing Ratio and DerivedRelation instead of
creating another independent mathematical representation.

AnalyzeConsistency checks all of these structural conditions:

1. The numerator and denominator in the ratio are the same as those in the
   derived relation.
2. The WholePart equals the ratio quotient.
3. The Remainder equals the ratio remainder.
4. The whole part and remainder reconstruct the original numerator.
5. The remainder magnitude is smaller than the denominator magnitude.

For 22:7:

    Ratio:
        Numerator   = 22
        Denominator = 7

    DerivedRelation:
        WholePart = 3
        Remainder = 1

    Quotient/Remainder:
        22 / 7 = 3 remainder 1

    Reconstruction:
        3 * 7 + 1 = 22

Therefore the complete decomposition is structurally consistent.

For 42:7:

    42 / 7 = 6 remainder 0
    6 * 7 + 0 = 42

This is also consistent.

This rule deliberately does not assign a conceptual meaning to 22:7, 42:7,
or any other ratio. It only checks that the existing numerical objects agree
with each other.

## Important distinction

A ratio such as 44/7 can later be represented as a combination of components,
for example a whole component 42/7 plus a remainder component 2/7. That is a
different modeling level from the single Ratio(44, 7).

The current consistency pattern therefore does not assume that 44/7 is
identical to 42/7. It validates only the decomposition of the specific ratio
object it receives.

This distinction keeps component relations available for a later stage without
hard-coding them into the basic ratio model.

## Why this remains structural

The analyzer does not claim that a ratio represents pi, e, the golden ratio,
or any user-defined conceptual meaning. Reference matching remains a separate
stage.

Later pattern rules can be added one at a time when they have a clear input,
output, and test.
