using System;

namespace NP.NumericalModel.Interpretation
{
    public static class InterpretationDefinitionFactory
    {
        public static InterpretationDefinition CreateSevenBridgeDefinition()
        {
            InterpretationDefinition definition =
                new InterpretationDefinition(
                    "Seven Bridge Interpretation",
                    "1.0");

            NumericalConcept c1 =
                new NumericalConcept("23:32", null);

            NumericalConcept c2 =
                new NumericalConcept("#", 7);

            NumericalConcept c3 =
                new NumericalConcept("0.7", null);

            NumericalConcept c4 =
                new NumericalConcept("77", 77);

            NumericalConcept c5 =
                new NumericalConcept("3.14", null);

            NumericalConcept c6 =
                new NumericalConcept("42", 42);

            c1.AddMeaning("Structural relation");
            c2.AddMeaning("Sharp / Seven symbol");
            c3.AddMeaning("Relation to seven");
            c4.AddMeaning("Repeated seven state");
            c5.AddMeaning("Pi-related symbolic state");
            c6.AddMeaning("Numerical bridge");

            ConceptualRelation r1 =
                new ConceptualRelation(
                    c1,
                    c2,
                    "symbolic",
                    ":",
                    "23:32 -> #");

            ConceptualRelation r2 =
                new ConceptualRelation(
                    c2,
                    c3,
                    "transition",
                    ":",
                    "# -> 0.7");

            ConceptualRelation r3 =
                new ConceptualRelation(
                    c3,
                    c4,
                    "relation",
                    ":",
                    "0.7 -> 77");

            ConceptualRelation r4 =
                new ConceptualRelation(
                    c4,
                    c5,
                    "transition",
                    ":",
                    "77 -> 3.14");

            ConceptualRelation r5 =
                new ConceptualRelation(
                    c5,
                    c6,
                    "bridge",
                    ":",
                    "3.14 -> 42");

            RelationChain chain =
                new RelationChain();

            chain.AddConcept(c1);
            chain.AddConcept(c2);
            chain.AddConcept(c3);
            chain.AddConcept(c4);
            chain.AddConcept(c5);
            chain.AddConcept(c6);

            chain.AddRelation(r1);
            chain.AddRelation(r2);
            chain.AddRelation(r3);
            chain.AddRelation(r4);
            chain.AddRelation(r5);

            definition.AddConcept(c1);
            definition.AddConcept(c2);
            definition.AddConcept(c3);
            definition.AddConcept(c4);
            definition.AddConcept(c5);
            definition.AddConcept(c6);

            definition.AddRelation(r1);
            definition.AddRelation(r2);
            definition.AddRelation(r3);
            definition.AddRelation(r4);
            definition.AddRelation(r5);

            definition.AddChain(chain);

            return definition;
        }

        public static InterpretationDefinition CreateSelectedRelationsDefinition()
        {
            InterpretationDefinition definition =
                new InterpretationDefinition(
                    "Selected Numerical Relations",
                    "1.0");

            NumericalConcept one =
                new NumericalConcept("1", 1);
            one.AddMeaning("Conceptual unit");

            NumericalConcept same11 =
                new NumericalConcept("1:1", null);
            same11.AddMeaning("Reference state");

            NumericalConcept same22 =
                new NumericalConcept("2:2", null);
            same22.AddMeaning("Same-kind relation");

            NumericalConcept same33 =
                new NumericalConcept("3:3", null);
            same33.AddMeaning("Same-kind relation");

            NumericalConcept same44 =
                new NumericalConcept("4:4", null);
            same44.AddMeaning("Same boundary");

            NumericalConcept same55 =
                new NumericalConcept("5:5", null);
            same55.AddMeaning("Same-kind relation");

            NumericalConcept same66 =
                new NumericalConcept("6:6", null);
            same66.AddMeaning("Same-kind relation");

            NumericalConcept same77 =
                new NumericalConcept("7:7", null);
            same77.AddMeaning("Same-kind relation");

            NumericalConcept same88 =
                new NumericalConcept("8:8", null);
            same88.AddMeaning("Same-kind relation");

            NumericalConcept same99 =
                new NumericalConcept("9:9", null);
            same99.AddMeaning("Same-kind relation");

            NumericalConcept same1010 =
                new NumericalConcept("10:10", null);
            same1010.AddMeaning("Decimal reference state");

            NumericalConcept reverse1221 =
                new NumericalConcept("12:21", null);
            reverse1221.AddMeaning("Reverse pair");

            NumericalConcept reverse2332 =
                new NumericalConcept("23:32", null);
            reverse2332.AddMeaning("Reverse pair");

            NumericalConcept reverse1441 =
                new NumericalConcept("14:41", null);
            reverse1441.AddMeaning("Reverse pair with reference boundary");

            NumericalConcept reverse5665 =
                new NumericalConcept("56:65", null);
            reverse5665.AddMeaning("Reverse pair");

            NumericalConcept relation5448 =
                new NumericalConcept("54:48", null);
            relation5448.AddMeaning("Boundary relation 4:4");

            NumericalConcept relation6448 =
                new NumericalConcept("64:55", null);
            relation6448.AddMeaning("Selected conceptual relation");

            NumericalConcept chain565654 =
                new NumericalConcept("56:65:54", null);
            chain565654.AddMeaning("Continuous boundary chain");

            NumericalConcept chain56565448 =
                new NumericalConcept("56:65:54:48", null);
            chain56565448.AddMeaning("Extended continuous boundary chain");

            NumericalConcept eleven =
                new NumericalConcept("11", 11);
            eleven.AddMeaning("Threefold structural state");

            NumericalConcept eleven11 =
                new NumericalConcept("11:11", null);
            eleven11.AddMeaning("Divided structural state");

            NumericalConcept oneElevenOne =
                new NumericalConcept("1:11:1", null);
            oneElevenOne.AddMeaning("Unit with internal division");

            NumericalConcept eleven29 =
                new NumericalConcept("11:29", null);
            eleven29.AddMeaning("Selected conceptual relation");

            NumericalConcept oneThird =
                new NumericalConcept("1/3", null);
            oneThird.AddMeaning("One unit divided into three equal parts");

            NumericalConcept repeatingThird =
                new NumericalConcept("0.333...", null);
            repeatingThird.AddMeaning("Repeating decimal representation of 1/3");

            NumericalConcept goldenApproximation =
                new NumericalConcept("1.6+", null);
            goldenApproximation.AddMeaning("Positive approximation family");

            NumericalConcept hash =
                new NumericalConcept("#", 7);
            hash.AddMeaning("Sharp / Seven symbol");

            NumericalConcept ampersand =
                new NumericalConcept("&", 8);
            ampersand.AddMeaning("Eight symbol");

            NumericalConcept arc =
                new NumericalConcept("Arc", null);
            arc.AddMeaning("Structural arc symbol");

            NumericalConcept circleFourPlus =
                new NumericalConcept("④+", 4);
            circleFourPlus.AddMeaning("Four with circular totality and positive center");

            NumericalConcept three =
    new NumericalConcept("3", 3);
            three.AddMeaning("Threefold / stability concept");
            definition.AddConcept(three);

            definition.AddConcept(one);
            definition.AddConcept(same11);
            definition.AddConcept(same22);
            definition.AddConcept(same33);
            definition.AddConcept(same44);
            definition.AddConcept(same55);
            definition.AddConcept(same66);
            definition.AddConcept(same77);
            definition.AddConcept(same88);
            definition.AddConcept(same99);
            definition.AddConcept(same1010);

            definition.AddConcept(reverse1221);
            definition.AddConcept(reverse2332);
            definition.AddConcept(reverse1441);
            definition.AddConcept(reverse5665);
            definition.AddConcept(relation5448);
            definition.AddConcept(relation6448);

            definition.AddConcept(chain565654);
            definition.AddConcept(chain56565448);

            definition.AddConcept(eleven);
            definition.AddConcept(eleven11);
            definition.AddConcept(oneElevenOne);
            definition.AddConcept(eleven29);

            definition.AddConcept(oneThird);
            definition.AddConcept(repeatingThird);
            definition.AddConcept(goldenApproximation);

            definition.AddConcept(hash);
            definition.AddConcept(ampersand);
            definition.AddConcept(arc);
            definition.AddConcept(circleFourPlus);

    //        definition.AddRelation(
    //new ConceptualRelation(
    //    solarStability,
    //    three,
    //    "stability",
    //    "9.& -> 3",
    //    "Selected threefold stability relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    same11,
                    one,
                    "reference",
                    "1:1",
                    "Reference state around conceptual unit"));

            definition.AddRelation(
                new ConceptualRelation(
                    same44,
                    same44,
                    "same-boundary",
                    "4:4",
                    "Equal boundary relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    reverse1221,
                    reverse1221,
                    "reverse-pair",
                    "12:21",
                    "Reversed digit arrangement"));

            definition.AddRelation(
                new ConceptualRelation(
                    reverse2332,
                    reverse2332,
                    "reverse-pair",
                    "23:32",
                    "Reversed digit arrangement"));

            definition.AddRelation(
                new ConceptualRelation(
                    reverse1441,
                    reverse1441,
                    "reverse-pair",
                    "14:41",
                    "Reversed pair with structural boundary"));

            definition.AddRelation(
                new ConceptualRelation(
                    reverse5665,
                    reverse5665,
                    "reverse-pair",
                    "56:65",
                    "Reversed pair with boundary 6:6"));

            definition.AddRelation(
                new ConceptualRelation(
                    relation5448,
                    same44,
                    "boundary",
                    "54:48 -> 4:4",
                    "Adjacent boundary digits are equal"));

            definition.AddRelation(
                new ConceptualRelation(
                    relation6448,
                    chain565654,
                    "conceptual-bridge",
                    "64:55 -> 56:65:54",
                    "Selected conceptual relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    chain565654,
                    chain56565448,
                    "extension",
                    "56:65:54 -> 56:65:54:48",
                    "Extended continuous boundary chain"));

            definition.AddRelation(
                new ConceptualRelation(
                    oneThird,
                    repeatingThird,
                    "representation",
                    "1/3 = 0.333...",
                    "Exact fraction and repeating decimal representation"));

            definition.AddRelation(
                new ConceptualRelation(
                    oneElevenOne,
                    eleven11,
                    "structural",
                    "1:11:1 -> 11:11",
                    "Internal division of the conceptual unit"));

            definition.AddRelation(
                new ConceptualRelation(
                    goldenApproximation,
                    relation6448,
                    "conceptual-bridge",
                    "1.6+ <-> 64:55",
                    "Selected conceptual representation"));

            NumericalConcept fiveAnd =
                new NumericalConcept("5.0&", null);
            fiveAnd.AddMeaning(
                "Conceptual decimal relation involving five and ampersand");
            NumericalRepresentation piGoldenPath =
                new NumericalRepresentation(
                    "Pi-Golden Relation",
                    "3.14... x 1.6... -> 5.0&",
                    "ConceptualNumericalPath",
                    "A selected conceptual relation between the decimal expansions of 3.14... and 1.6..., associated with the five-symbol '&' state");
            definition.AddConcept(fiveAnd);
            definition.AddRepresentation(piGoldenPath);

            NumericalConcept ratio8167 =
    new NumericalConcept("8:16:7", null);
            ratio8167.AddMeaning("Selected relation associated with 1.618...");

            NumericalConcept ratio3167 =
                new NumericalConcept("3:16:7", null);
            ratio3167.AddMeaning("Selected relation associated with 3:7:10:10");

            NumericalConcept ratio371010 =
                new NumericalConcept("3:7:10:10", null);
            ratio371010.AddMeaning("Selected relation associated with golden approximation");

            NumericalConcept ratio1147293741221252 =
                new NumericalConcept("11:47:29:3:74:12:21:252", null);
            ratio1147293741221252.AddMeaning(
                "Selected multi-stage numerical relation");

            NumericalConcept relation1221252 =
                new NumericalConcept("12:21:252", null);
            relation1221252.AddMeaning(
                "Selected relation associated with 3:3");

            NumericalConcept same33To34 =
                new NumericalConcept("3:3 -> 34", null);
            same33To34.AddMeaning(
                "Selected conceptual transition");

            NumericalConcept relation3264 =
                new NumericalConcept("32:64", null);
            relation3264.AddMeaning(
                "Selected conceptual relation associated with 34");

            definition.AddConcept(ratio8167);
            definition.AddConcept(ratio3167);
            definition.AddConcept(ratio371010);
            definition.AddConcept(ratio1147293741221252);
            definition.AddConcept(relation1221252);
            definition.AddConcept(same33To34);
            definition.AddConcept(relation3264);

            definition.AddRelation(
    new ConceptualRelation(
        ratio8167,
        goldenApproximation,
        "conceptual-association",
        "8:16:7 -> 1.618...",
        "Selected relation associated with the golden ratio"));

            definition.AddRelation(
                new ConceptualRelation(
                    ratio3167,
                    ratio371010,
                    "conceptual-association",
                    "3:16:7 -> 3:7:10:10",
                    "Selected relation between the two representations"));

            definition.AddRelation(
                new ConceptualRelation(
                    ratio371010,
                    goldenApproximation,
                    "conceptual-association",
                    "3:7:10:10 -> 1.6+",
                    "Selected relation associated with golden approximation"));

            definition.AddRelation(
                new ConceptualRelation(
                    ratio1147293741221252,
                    relation1221252,
                    "chain",
                    "11:47:29:3:74:12:21:252",
                    "Selected multi-stage relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    relation1221252,
                    same33,
                    "conceptual-association",
                    "12:21:252 -> 3:3",
                    "Selected relation associated with threefold boundary"));

            definition.AddRelation(
                new ConceptualRelation(
                    same33To34,
                    relation3264,
                    "conceptual-transition",
                    "3:3 -> 34 -> 32:64",
                    "Selected conceptual transition"));
            definition.AddRelation(
    new ConceptualRelation(
        same33,
        relation3264,
        "conceptual-transition",
        "3:3 -> 34 -> 32:64",
        "Selected conceptual transition"));

            NumericalConcept piApproximation =
    new NumericalConcept("3.14", null);
            piApproximation.AddMeaning(
                "Decimal representation of pi used in the selected conceptual chain");

            NumericalConcept lunarRelation =
                new NumericalConcept("2:7", null);
            lunarRelation.AddMeaning(
                "Selected lunar decimal relation");

            NumericalConcept solarStability =
                new NumericalConcept("9.&", null);
            solarStability.AddMeaning(
                "Selected solar stability concept");

            NumericalConcept starFive =
                new NumericalConcept("5", 5);
            starFive.AddMeaning(
                "Unique five-point star concept");

            NumericalConcept fivePointResult =
                new NumericalConcept("5.0&", null);
            fivePointResult.AddMeaning(
                "Selected conceptual five-state representation");

            NumericalConcept arcOneToFour =
                new NumericalConcept("1:1+ -> 4", null);
            arcOneToFour.AddMeaning(
                "Selected arc and totality relation");

            NumericalConcept relation314ToFive =
                new NumericalConcept("3.14 -> 5", null);
            relation314ToFive.AddMeaning(
                "Selected conceptual transition from pi representation to fivefold state");

            definition.AddConcept(piApproximation);
            definition.AddConcept(lunarRelation);
            definition.AddConcept(solarStability);
            definition.AddConcept(starFive);
            definition.AddConcept(fivePointResult);
            definition.AddConcept(arcOneToFour);
            definition.AddConcept(relation314ToFive);

            definition.AddRelation(
    new ConceptualRelation(
        lunarRelation,
        solarStability,
        "conceptual-transition",
        "2:7 -> 9.&",
        "Selected lunar-to-solar stability relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    solarStability,
                    new NumericalConcept("3", 3),
                    "stability",
                    "9.& -> 3",
                    "Three as selected stability concept"));

            definition.AddRelation(
                new ConceptualRelation(
                    arcOneToFour,
                    piApproximation,
                    "formation",
                    "1:1+ -> 14 -> 3.14",
                    "Selected structural formation"));

            definition.AddRelation(
                new ConceptualRelation(
                    piApproximation,
                    starFive,
                    "conceptual-transition",
                    "3.14 -> 5",
                    "Selected fivefold conceptual relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    starFive,
                    goldenApproximation,
                    "aesthetic-relation",
                    "5 <-> 1.6+",
                    "Selected aesthetic relation between fivefold state and golden approximation"));

            definition.AddRelation(
                new ConceptualRelation(
                    piApproximation,
                    fivePointResult,
                    "conceptual-representation",
                    "3.14 -> 5.0&",
                    "Selected fivefold decimal-symbolic representation"));

    //        NumericalConcept relation3264 =
    //new NumericalConcept("32:64", null);
    //        relation3264.AddMeaning(
    //            "Selected relation associated with 1.5");

            NumericalConcept ratio15 =
                new NumericalConcept("1.5", null);
            ratio15.AddMeaning(
                "Decimal representation of three halves");

            //NumericalConcept oneThird =
            //    new NumericalConcept("1/3", null);
            //oneThird.AddMeaning(
            //    "One unit divided into three equal parts");

            NumericalConcept fourThirds =
                new NumericalConcept("4/3", null);
            fourThirds.AddMeaning(
                "Four thirds");

            NumericalConcept fiveThirds =
                new NumericalConcept("5/3", null);
            fiveThirds.AddMeaning(
                "Sum of one third and four thirds");

            NumericalConcept value27 =
                new NumericalConcept("2.7", null);
            value27.AddMeaning(
                "Selected decimal relation");

            NumericalConcept napier =
                new NumericalConcept("e", null);
            napier.AddMeaning(
                "Napier's constant");

            definition.AddConcept(relation3264);
            definition.AddConcept(ratio15);
            definition.AddConcept(fourThirds);
            definition.AddConcept(fiveThirds);
            definition.AddConcept(value27);
            definition.AddConcept(napier);

            definition.AddRelation(
    new ConceptualRelation(
        relation3264,
        ratio15,
        "numerical-association",
        "32:64 -> 1.5",
        "Selected numerical relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    oneThird,
                    fiveThirds,
                    "addition",
                    "1/3 + 4/3 = 5/3",
                    "Exact fractional relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    fourThirds,
                    fiveThirds,
                    "addition",
                    "4/3 + 1/3 = 5/3",
                    "Exact fractional relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    fiveThirds,
                    goldenApproximation,
                    "approximation",
                    "5/3 -> 1.6+",
                    "Selected approximation toward golden-family representation"));

            definition.AddRelation(
                new ConceptualRelation(
                    ratio15,
                    goldenApproximation,
                    "approximation",
                    "1.5 -> 1.6+",
                    "Selected incremental approximation"));

            definition.AddRelation(
                new ConceptualRelation(
                    value27,
                    piApproximation,
                    "conceptual-relation",
                    "2.7 -> 3.14",
                    "Selected conceptual relation"));

            definition.AddRelation(
                new ConceptualRelation(
                    piApproximation,
                    napier,
                    "conceptual-relation",
                    "3.14 -> e",
                    "Selected relation involving pi and Napier's constant"));

            return definition;
        }
    }
}