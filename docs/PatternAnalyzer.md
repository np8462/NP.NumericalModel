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

The first capability is intentionally structural rather than conceptual.

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

## Why this is the first pattern

This analyzer does not claim that a ratio represents pi, e, the golden ratio,
or any user-defined conceptual meaning. It only detects a property that is
already explicitly present in the derived relation: whether the remainder is
zero.

This keeps PatternAnalyzer testable and objective. Later pattern rules can be
added one at a time when they have a clear input, output, and test.
