# Reference Manual

## Purpose

The Reference layer compares a value derived by the numerical model with an independently supplied reference value.

It does not generate the reference from the model and it does not assign conceptual meaning to the result.

The comparison produces one of:

- `Exact`
- `Approximate`
- `NoMatch`

## 22:7 and pi

    Ratio ratio = new Ratio(22, 7);

    ReferenceMatcher matcher = new ReferenceMatcher();

    VerificationResult result = matcher.Compare(
        "Pi",
        ratio.DecimalValue,
        Math.PI,
        0.001);

The model first derives the value of `22:7`.

The Reference layer then independently compares that value with `Math.PI`.

The result is classified as `Approximate`, not `Exact`.

## 19:7 and e

    Ratio ratio = new Ratio(19, 7);

    VerificationResult result = matcher.Compare(
        "e",
        ratio.DecimalValue,
        Math.E,
        0.01);

This provides an independent comparison with the mathematical constant `e`.

## 89:55 and the golden ratio

The golden ratio can be represented independently as:

    (1 + sqrt(5)) / 2

The ratio `89:55` can then be compared with that reference.

    Ratio ratio = new Ratio(89, 55);

The comparison is approximate within the supplied tolerance.

## Tolerance

Tolerance is supplied by the caller.

For a nonzero reference:

    RelativeError = |computed - reference| / |reference|

The classification is:

- `Exact`: difference is exactly zero.
- `Approximate`: difference is nonzero but within tolerance.
- `NoMatch`: difference is outside tolerance.

## Important design rule

The Reference layer must remain independent from the numerical derivation.

For example:

    22:7 -> 3 + 1/7 -> 3.142857...

is derived by the model.

Then:

    3.142857... -> compare with Math.PI

is verification.

The model does not contain a rule saying that `22:7` means pi.

## Current API

### ReferenceMatcher

- `Compare(referenceName, computedValue, referenceValue, tolerance)`

### VerificationResult

- `ReferenceName`
- `ComputedValue`
- `ReferenceValue`
- `Difference`
- `RelativeError`
- `Classification`
- `IsMatch`

### VerificationClassification

- `Exact`
- `Approximate`
- `NoMatch`

## Next step

The next stage can connect Ratio + RatioDecomposition + ReferenceMatcher into an integration flow without introducing hard-coded conceptual relations.
