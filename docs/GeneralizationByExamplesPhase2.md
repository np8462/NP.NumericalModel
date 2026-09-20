# Generalization by Examples — Phase 2

## Purpose

Phase 2 is a verification stage, not a feature-expansion stage.

The goal is to run several different numerical example families through the
current public API and identify what is genuinely missing before adding a new
core class or operation.

## Example matrix

| Family | Example | Existing API path | Result |
| --- | --- | --- | --- |
| Ratio decomposition | `44:7` | Ratio -> RatioDecomposition | Supported |
| Approximation | `22:7 ~ pi` | Ratio -> RealValue -> ReferenceMatcher | Supported |
| Higher precision | `355:113 ~ pi` | Ratio -> RealValue -> ReferenceMatcher | Supported |
| Composition | `42:7 + 2:7` | RatioComposer | Supported |
| Exact composition | `1:2 + 1:2 = 1:1` | RatioComposer -> RealValue | Supported |
| Decimal value | `6.28` | RealValue.FromExpression | Supported |
| Geometry-style value | `2*pi` | RealValue.FromExpression | Supported as supplied numeric value |
| Angle | `pi/2` | RealValue.FromExpression | Supported as supplied numeric value |
| Function application | `sin(1 degree)` | RealValue.Apply | Supported when numeric function is supplied |

The executable probes are in
`tests/NP.NumericalModel.Tests/GeneralizationByExamplesPhase2Tests.cs`.

## What the current API can actually do

The current model can already separate several concerns:

1. A ratio can be represented and decomposed.
2. Ratios can be composed numerically.
3. A computed numeric value can be compared with an external reference.
4. A real value can preserve both its numeric value and its input expression.
5. A supplied numeric function can be applied without adding a special class
   for sine, pi, angles, or geometry.

This is important: being able to preserve an expression string is not the
same as being able to parse or evaluate that expression.

For example:

`RealValue.FromExpression("pi/2", Math.PI / 2.0)`

stores the expression and its already-computed value. It does not calculate
`pi/2` from the string.

## Genuine gaps found before further development

### 1. Expression parsing/evaluation

If the project requirement becomes:

> Give the model a string such as `42:7 + 2:7`, `pi/2`, or
> `sin(1 degree)` and have it calculate the result automatically.

then the current API is insufficient.

This would be a new general capability, not a special-case class for pi or
sine. It should only be added if automatic expression evaluation is actually
needed.

### 2. General arithmetic composition

`RatioComposer` currently models addition of rational components. It does
not provide a general arithmetic expression model for subtraction,
multiplication, division, or nested operations.

This is a real gap only if the example matrix requires those operations as
first-class model operations. We should not add them merely because they
exist mathematically.

### 3. Units are preserved but not modeled

`RealValue` can carry an optional unit string, but the current API does not
perform unit conversion or dimensional validation.

Again, this should remain a gap until examples demonstrate a requirement for
unit-aware computation.

## What is NOT a gap

The following do not currently justify new classes:

- pi-specific handling
- sine-specific handling
- arc-specific handling
- a special class for `44:7`
- a special class for decimal values
- a special class for angles

The current APIs can represent these examples without embedding their
conceptual meanings into the numerical core.

## Phase 2 conclusion

The current model passes the first cross-family representation and analysis
probe without requiring another core capability.

Therefore the next development step should **not** be automatic expression
parsing yet.

The next useful step is to select examples that deliberately stress the
identified gaps, especially:

- arithmetic beyond addition
- nested/composed expressions
- expressions whose result must be calculated from text rather than supplied
  as a precomputed `double`

Only when those examples fail for a concrete reason should the corresponding
general capability be added.
