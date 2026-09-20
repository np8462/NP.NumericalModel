# Generalization by Examples

The numerical model should grow by extracting reusable capabilities from different examples, not by creating a special class for every example.

## General rule

For each new example:

1. Try to represent it with the existing model.
2. Identify the smallest missing general concept if it cannot be represented.
3. Add that general capability and tests.
4. Re-run the existing test suite.
5. Keep domain-specific interpretation outside the mathematical core.

## Example families

| Family | Example | What it exercises |
| --- | --- | --- |
| Ratio | 44:7 | Composition / decomposition |
| Approximation | 22:7 ~= pi | Reference matching |
| Higher precision | 355:113 ~= pi | Precision |
| Geometry | arc / radius | General real value |
| Angle | pi/2 | Representation |
| Trigonometric | sin(1 degree) | Function application |
| Decimal | 6.28 | Real numeric value |
| Composition | 1/2 + 1/2 | Composition |
| Structural | 54:48 | Boundary relation |

## General real values

Interpretation.RealValue is intentionally small. It stores:

- a numeric double value;
- an expression describing how that value was represented;
- an optional unit;
- a generic Apply operation for numeric functions.

It does not contain special cases for pi, radians, sine, cosine, arcs, or any other named mathematical concept.

For example:

- 22:7 can be represented as a RealValue;
- 355:113 can be represented independently;
- 2*pi can be represented without creating a PiValue or ArcValue;
- pi/2 can be represented without creating a RadianValue;
- sin(1 degree) can be produced through the generic function application mechanism.

This keeps the new capability reusable while leaving interpretation to higher layers.

## Current boundary

RealValue is a representation and computation carrier, not a symbolic expression parser. It does not attempt to parse arbitrary mathematical strings. If later examples demonstrate a recurring need for symbolic expression construction or evaluation, that need should first be tested and then extracted as a separate general capability.

Structural relations remain independent because a boundary relation such as 54:48 is not the same kind of mathematical value as a real-number expression.
