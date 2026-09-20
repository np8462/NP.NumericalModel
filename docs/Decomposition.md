# Decomposition Manual

## Purpose

`RatioDecomposition` derives a mixed-number structure from an existing `Ratio`.

It does not assign a conceptual meaning to the ratio. It only exposes the numerical decomposition:

    numerator = wholePart * denominator + remainder

The remainder is represented as a new `Ratio`.

## 22:7

    Ratio ratio = new Ratio(22, 7);
    RatioDecomposition decomposition = new RatioDecomposition(ratio);

The model derives:

- WholePart = `3`
- Remainder = `1`
- RemainderRatio = `1:7`
- Mixed number = `3 + 1/7`
- DecimalValue = approximately `3.142857142857...`

This is derived from the ratio itself; no value such as pi is hard-coded.

## 42:7

    RatioDecomposition decomposition =
        new RatioDecomposition(new Ratio(42, 7));

The model derives:

- WholePart = `6`
- Remainder = `0`
- IsExact = `true`
- Mixed number = `6`

## 5:3

    RatioDecomposition decomposition =
        new RatioDecomposition(new Ratio(5, 3));

The model derives:

- WholePart = `1`
- Remainder = `2`
- RemainderRatio = `2:3`
- Mixed number = `1 + 2/3`
- DecimalValue = approximately `1.666666666666...`

## Reconstruction

The original numerator can be reconstructed:

    22 = 3 * 7 + 1

Use:

    decomposition.ReconstructNumerator()

The result is `22`.

## Design rule

`RatioDecomposition` is a numerical layer.

For example, the decomposition of `22:7` does not claim that the ratio represents pi. A later reference-matching layer can independently compare its decimal value with pi.

## Current API

- `Ratio`
- `WholePart`
- `Remainder`
- `RemainderRatio`
- `IsExact`
- `DecimalValue`
- `ReconstructNumerator()`
- `ToMixedNumberString()`
- `ToString()`

## Next step

The next stage can use these derived parts for independent reference comparison and verification.
