# Derived Relation

This stage introduces the smallest derived-relation capability after Integration.

Current flow:

    Ratio
      |
      v
    RatioDecomposition
      |
      v
    RatioRelationDeriver
      |
      v
    DerivedRelation

RatioRelationDeriver does not duplicate division. It uses the existing Ratio and
RatioDecomposition classes and exposes the derived whole/remainder relation as
a separate model object.

Examples:

    22:7 -> 3 + 1/7
    42:7 -> 6
    5:3  -> 1 + 2/3

This stage intentionally does not decide that a derived relation represents a
named mathematical constant or a conceptual meaning. Reference comparison is
still handled by ReferenceMatcher, and broader pattern analysis will be added
only when a concrete tested capability requires it.

Design rule:

A derived relation is a result produced by the model, not an interpretation
assigned to it. This keeps mathematical derivation separate from conceptual
patterns.
