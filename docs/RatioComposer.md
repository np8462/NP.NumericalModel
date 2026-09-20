# Ratio Composer

RatioComposer combines numeric Ratio objects as rational components. It does not assign conceptual meaning to any component.

## Basic composition

For components with the same denominator:

    42:7 + 2:7 = 44:7

The composer preserves the resulting rational value.

For different denominators, ordinary rational addition is used:

    1:2 + 1:3 = 5:6

## Multiple components

The Combine method can compose several Ratio objects:

    42:7 + 2:7 + 1:7 = 45:7

The operation is performed sequentially through the existing Ratio primitive.

## Separation from interpretation

This class deliberately knows nothing about why a component exists.

For example, the model may later represent:

    44:7
      |
      +-- 42:7
      |
      +--  2:7

That component structure is different from simply creating Ratio(44, 7).
The composer provides the numeric operation needed to produce the combined ratio;
a later structural model can preserve the component history if that becomes
necessary.

No reference such as pi, geometry, or a user-defined numerical meaning is
introduced here.

## Current scope

The first implementation supports:

- adding two ratios;
- combining multiple ratios;
- different denominators;
- null and empty-input validation.

Reduction, provenance/history of components, and richer expression trees are
intentionally left for later stages if tests demonstrate that they are needed.
