# Ratio Manual

## Purpose

`Ratio` is the first numerical relation primitive in `NP.NumericalModel`.

It does not assign a conceptual meaning to a ratio. It only derives numerical facts that later analysis and interpretation can use.

## Basic usage

    Ratio ratio = new Ratio(22, 7);

    long quotient = ratio.Quotient;
    long remainder = ratio.Remainder;
    double value = ratio.DecimalValue;

For `22:7` the model derives:

- Quotient = `3`
- Remainder = `1`
- DecimalValue = `3.142857142857...`
- IsExact = `false`

## Exact division

    Ratio ratio = new Ratio(42, 7);

    Assert.AreEqual(6L, ratio.Quotient);
    Assert.AreEqual(0L, ratio.Remainder);
    Assert.IsTrue(ratio.IsExact);

## Reconstruction

A ratio can be decomposed and reconstructed:

    22 = 3 * 7 + 1

Use `ReconstructNumerator()` to recover the original numerator.

## Important design rule

`Ratio` does not claim that `22:7` means pi.

A later reference/verification layer may compare `22/7` with pi and classify the result as an approximation. The Ratio model itself only derives quotient, remainder, decimal value, and reconstruction.

## Current API

- `Numerator`
- `Denominator`
- `Quotient`
- `Remainder`
- `DecimalValue`
- `IsExact`
- `ReconstructNumerator()`
- `ToString()`

## Next step

The next layer will use `Ratio` to build `Decomposition`, so expressions such as `22:7 -> 3 + 1/7` can be represented as derived structure instead of a hard-coded conceptual relation.
