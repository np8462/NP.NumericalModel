# Numerical Expression Evaluation

This capability is the first concrete response to the Phase 2 gap:

> Text -> Numerical Expression -> Evaluation

## Scope

The model now supports a small general arithmetic expression language:

- numbers and decimal numbers
- `+`
- `-`
- `*`
- `/`
- `:` as a numeric ratio/division operator
- parentheses
- `pi`
- `sin(...)`
- `cos(...)`
- `tan(...)`

The parser uses ordinary arithmetic precedence:

`*`, `/`, and `:` are evaluated before `+` and `-`.

## Architecture

```
text
  |
  v
NumericalExpressionParser
  |
  v
NumericalExpression
  |
  +--> Value
  |
  +--> Text
  |
  v
RealValue
```

The expression model keeps calculation separate from conceptual interpretation.

For example:

`42:7 + 2:7`

is parsed and evaluated numerically as:

`44 / 7`

No special class for `42`, `44`, or `pi) is introduced.

## Examples

```
42:7 + 2:7       -> 6.285714...
(42:7 + 2:7)+1:7 -> 6.428571...
1 + 2 * 3        -> 7
pi/2             -> 1.570796...
sin(pi/2)        -> 1
```

## Deliberate boundary

The parser is intentionally small.

It does not yet provide:

- variables
- arbitrary user-defined functions
- symbolic algebra
- unit conversion
- dimensional analysis
- implicit multiplication
- automatic interpretation of words such as `degree`

Those are separate capabilities and should only be added when examples demonstrate that they are required.

## Important distinction

The parser is now able to calculate an expression from its text.

That is different from the previous `RealValue.FromExpression` API, where the
caller supplied both the expression text and its already-calculated value.

This change therefore represents a genuine new capability rather than another
representation wrapper.
