# Integration

The integration stage composes the existing numerical model capabilities without duplicating their logic.

Current flow:

    Ratio
      |
      v
    RatioDecomposition
      |
      v
    ReferenceMatcher
      |
      v
    RatioVerificationResult

## RatioVerification

`RatioVerification` accepts an existing `Ratio`, decomposes it, and compares the resulting decimal value with an independently supplied reference.

Example:

    Ratio ratio = new Ratio(22, 7);
    RatioVerification verification = new RatioVerification();

    RatioVerificationResult result =
        verification.Verify(ratio, "Pi", Math.PI, 0.002);

The result contains:

- `Ratio`: the original ratio.
- `Decomposition`: the whole part and remainder ratio.
- `Verification`: the independent reference comparison.

For `22:7`, the decomposition is `3 + 1/7` and the decimal value is compared with `Math.PI`.

The integration layer does not claim that `22:7` equals pi. It only reports the classification produced by `ReferenceMatcher` using the supplied tolerance.

## Exact example

    RatioVerificationResult result =
        verification.Verify(
            new Ratio(42, 7),
            "Six",
            6.0,
            0.001);

This produces an `Exact` verification because the computed value and reference value are equal.

## Design rule

Integration composes existing capabilities:

- `Ratio` performs ratio arithmetic.
- `RatioDecomposition` exposes the mixed-number decomposition.
- `ReferenceMatcher` performs reference comparison.
- `RatioVerification` coordinates them.

Future analysis stages should build on this integration rather than copying these responsibilities into another class.
