# Ratio Composition

RatioComposition preserves both the numeric result of a ratio composition and
the components that produced that result.

## Example

    42:7 + 2:7 = 44:7

The composition stores:

    Components
      42:7
       2:7

and separately stores:

    Result
      44:7

This is intentionally different from creating only:

    Ratio(44, 7)

The result has the same numeric value, but the composition also preserves
the component history.

## Separation from interpretation

RatioComposition is still a mathematical/numeric structure. It does not
assign conceptual meaning to 42, 2, 7, or 44.

For example, a later interpretation layer may inspect the components, but
this class itself only knows that several Ratio objects were composed.

## Current scope

The first implementation supports:

- preserving the original Ratio component instances;
- calculating the combined Ratio through RatioComposer;
- exposing the component collection as read-only;
- checking whether an equivalent Ratio is present;
- rejecting empty or null component lists.

No expression tree, conceptual interpretation, or provenance beyond the
direct component list is introduced at this stage.
