# راهنمای NP.NumericalModel

## 1. هدف پروژه

`NP.NumericalModel` یک مدل عددی/ساختاری است که عدد را فقط به‌عنوان یک مقدار حسابی در نظر نمی‌گیرد؛ بلکه رابطه، ساختار، حالت‌های تحلیلی و روابط مفهومی را نیز قابل مدل‌سازی می‌کند.

این پروژه روی .NET Framework 4.8 و syntax سازگار با C# قدیمی توسعه داده می‌شود تا با Visual Studio 2012 قابل ساخت باشد.

---

## 2. BaseSystem

مسئول تعریف دستگاه مبنا است.

### ساخت

```csharp
BaseSystem base6 = new BaseSystem(6);
BaseSystem base10 = new BaseSystem(10);
```

### اعضای مهم

```csharp
base6.Base
base6.MaximumDigit
base6.IsValidDigit(5)
```

در Base 6، رقم‌های معتبر `0..5` هستند و `MaximumDigit` برابر `5` است.

در Base 10، رقم‌های معتبر `0..9` هستند و `MaximumDigit` برابر `9` است.

`BaseSystem` فقط اعتبار عددی پایه را مشخص می‌کند و معنای مفهومی نمادها را تعیین نمی‌کند.

---

## 3. StructuralSeparator

جداکننده را به‌عنوان یک عنصر ساختاری مدل می‌کند.

```csharp
StructuralSeparator dot =
    StructuralSeparator.Dot;

StructuralSeparator colon =
    StructuralSeparator.Colon;
```

نمادهای فعلی:

```text
.
:
```

در مدل، جداکننده صرفاً به معنی decimal point یا division فرض نمی‌شود؛ معنای آن می‌تواند در لایه تحلیل تعیین شود.

---

## 4. SubsetRelation

یک رابطه والد/فرزند را در یک Base مشخص مدل می‌کند.

```csharp
BaseSystem base6 = new BaseSystem(6);

SubsetRelation relation =
    new SubsetRelation(
        5,
        5,
        StructuralSeparator.Dot,
        base6);
```

### اعضای مهم

```csharp
relation.Parent
relation.Child
relation.Separator
relation.BaseSystem
relation.IsBoundary
```

برای `5.5` در Base 6، `IsBoundary` برابر `true` است.

---

## 5. BoundaryTransition

مرز یک Base را به انتقال مفهومی مرتبه بعد متصل می‌کند.

```csharp
BoundaryTransition transition =
    new BoundaryTransition(relation);

if (transition.CanTransition)
{
    string result =
        transition.GetConceptualResult();
}
```

نمونه مفهومی:

```text
5.5 [Base=6] -> 6:6
9.9 [Base=10] -> 10:10
```

این خروجی یک قاعده مفهومی مدل است، نه یک عملیات استاندارد اعشاری.

---

## 6. RelationAnalyzer

روابط پایه‌ای عددی را استخراج می‌کند.

### ساخت

```csharp
RelationAnalyzer analyzer =
    new RelationAnalyzer(4);
```

عدد `4` حداکثر عمق تحلیل است.

### تحلیل یک عدد

```csharp
RelationGraph graph =
    analyzer.Analyze(74);
```

نمونه روابط:

```text
74 --DigitSum--> 11
11 --DigitSum--> 2
74 --DigitDifference--> 3
11 --DigitDifference--> 0
```

### تحلیل یک رابطه

```csharp
RelationGraph graph =
    analyzer.AnalyzeRelation(7, 4);
```

`RelationAnalyzer` روابط عددی پایه را تولید می‌کند و خودش مسئول تفسیر مفاهیم خاص پروژه نیست.

---

## 7. RelationGraph و RelationDerivation

`RelationGraph` گراف روابط عددی ساده است.

```csharp
foreach (RelationDerivation derivation
    in graph.Derivations)
{
    Console.WriteLine(
        derivation.ToString());
}
```

هر `RelationDerivation` شامل این اطلاعات است:

```text
SourceValue
Rule
ResultValue
Depth
```

مثلاً:

```text
74 --DigitSum--> 11
```

---

## 8. FactorRelationFinder

تمام جفت‌های فاکتوری یک عدد را پیدا می‌کند.

```csharp
FactorRelationFinder finder =
    new FactorRelationFinder();

List<FactorRelation> relations =
    finder.Find(252);
```

برای `252` نمونه‌ای از خروجی:

```text
252 -> 1:252
252 -> 2:126
252 -> 3:84
252 -> 4:63
252 -> 6:42
252 -> 7:36
252 -> 9:28
252 -> 12:21
252 -> 14:18
```

`12:21` مهم است، چون:

```text
12 × 21 = 252
12 -> 1+2 = 3
21 -> 2+1 = 3
12:21 -> 3:3
```

هیچ جفت خاصی به‌عنوان تنها جواب hard-code نشده است.

---

## 9. FactorRelation

یک رابطه فاکتوری را نگه می‌دارد.

```csharp
FactorRelation relation =
    new FactorRelation(252, 12, 21);

Console.WriteLine(
    relation.GetRepresentation());
```

خروجی:

```text
12:21
```

اطلاعات رابطه:

```text
SourceValue = 252
Left = 12
Right = 21
```

---

## 10. FactorialStateGenerator

حالت‌های تحلیلی ساختاری یک عدد را تولید می‌کند.

فرمول تعداد حالت‌های مورد انتظار برای `n` رقم:

```text
n! - n + 1
```

در حال حاضر تولید عملی برای اعداد یک تا سه‌رقمی تعریف شده است.

### نمونه 252

```csharp
FactorialStateGenerator generator =
    new FactorialStateGenerator();

List<AnalysisState> states =
    generator.Generate(252);
```

حالت‌ها:

```text
252
25
52
2:5:2
```

نکته: `2:5:2` سه عدد مستقل نیست؛ یک حالت ساختاری واحد است.

---

## 11. AnalysisState

یک حالت تحلیلی را نگه می‌دارد.

اعضای اصلی:

```text
SourceValue
Representation
Kind
```

مثلاً:

```text
SourceValue = 252
Representation = 25
Kind = TwoDigitLeft
```

یا:

```text
SourceValue = 252
Representation = 2:5:2
Kind = FullRelation
```

---

## 12. StateDecomposer

برای Stateهایی که نمایش عددی دارند، روابط فاکتوری آن‌ها را استخراج می‌کند.

```csharp
StateDecomposer decomposer =
    new StateDecomposer();

List<FactorRelation> factors =
    decomposer.FindFactorRelations(state);
```

برای State ساختاری مانند `2:5:2` که عدد معمولی نیست، فعلاً رابطه فاکتوری عددی تولید نمی‌شود. تحلیل ساختاری آن برای مرحله بعدی پروژه در نظر گرفته شده است.

---

## 13. ConceptualRelationRule

قواعد مفهومی را از عملیات استاندارد عددی جدا می‌کند.

قاعده فعلی پروژه:

```text
3:3 -> 4
```

این `4` نتیجه جمع استاندارد `3+3` نیست.

برای همان رابطه، نتیجه عددی استاندارد نیز وجود دارد:

```text
3:3 -> 6       NumericSum
3:3 -> 4       ConceptualAggregation
```

نمونه استفاده:

```csharp
ConceptualRelationRule rule =
    new ConceptualRelationRule(
        "ConceptualAggregation");

int result;

bool applied =
    rule.TryApply(3, 3, out result);
```

در این مدل، معنای مفهومی باید به‌عنوان یک Rule مستقل باقی بماند و با ریاضیات استاندارد مخلوط نشود.

---

## 14. ConceptualRelationGraph

گراف عمومی‌تر پروژه است و می‌تواند Nodeهای عددی، رابطه‌ای و مفهومی را کنار هم نگه دارد.

```csharp
ConceptualRelationGraph graph =
    new ConceptualRelationGraph();
```

ایجاد Node:

```csharp
RelationNode node =
    graph.GetOrAddNode(
        "252",
        "Value");
```

ایجاد Edge:

```csharp
graph.AddEdge(
    "252",
    "Value",
    "12:21",
    "FactorRelation",
    "FactorRelation",
    0);
```

جستجوی روابط:

```csharp
graph.FindOutgoing("252");
graph.FindIncoming("3:3");
```

این Graph یک Tree نیست؛ یک Node می‌تواند از چند مسیر مختلف قابل دسترسی باشد.

---

## 15. RelationEngine

`RelationEngine` لایه هماهنگ‌کننده است. کلاس‌های تحلیلی موجود را کنار هم قرار می‌دهد.

```csharp
RelationEngine engine =
    new RelationEngine(6);

ConceptualRelationGraph graph =
    engine.Analyze(252);
```

در یک تحلیل، Engine می‌تواند روابطی مانند این را به یک Graph مشترک وارد کند:

```text
252 -> DigitSum -> 9
252 -> FactorRelation -> 12:21
12:21 -> 3:3
3:3 -> NumericSum -> 6
3:3 -> ConceptualAggregation -> 4
252 -> State -> 25
252 -> State -> 52
252 -> State -> 2:5:2
```

این کلاس در واقع نقطه اتصال بخش‌های مختلف مدل است.

---

## 16. مسیر مهم آزمایشی 252

نمونه‌ای که برای تست چندمرحله‌ای استفاده شده است:

```text
252
  -> 12:21
      -> 12 -> 3
      -> 21 -> 3
      -> 3:3
          -> 6       (NumericSum)
          -> 4       (ConceptualAggregation)
```

همچنین:

```text
252 -> 25 -> 3
252 -> 52 -> 3
```

بنابراین مقدار `3` از چند مسیر مختلف در Graph ظاهر می‌شود. این یکی از دلایل استفاده از Graph به‌جای Tree است.

---

## 17. RelationNode و RelationEdge

`RelationNode` یک عبارت را همراه با نوع آن نگه می‌دارد:

```text
Expression
Kind
```

`RelationEdge` رابطه بین دو Node را نگه می‌دارد:

```text
Source
Target
Rule
Depth
```

نمونه:

```text
3:3 --ConceptualAggregation--> 4
```

---

## 18. SVG و نمایش Graph

نمایش SVG فقط یک لایه روی مدل است و نباید در منطق عددی دخالت کند.

دو کلاس فعلی:

```text
GraphLayout
SvgGraphRenderer
```

### GraphLayout

موقعیت Nodeها را تعیین می‌کند:

```csharp
GraphLayout layout =
    new GraphLayout();

layout.NodeWidth = 110;
layout.NodeHeight = 42;
layout.HorizontalSpacing = 50;
layout.VerticalSpacing = 80;
layout.Margin = 50;

layout.Build(graph);
```

### SvgGraphRenderer

Graph و Layout را به SVG تبدیل می‌کند:

```csharp
SvgGraphRenderer renderer =
    new SvgGraphRenderer();

renderer.ShowRules = true;
renderer.ShowKinds = false;

renderer.Render(
    graph,
    layout,
    "252-relations.svg");
```

SVG فعلی یک Renderer ساده و پارامتریک است. خوانایی Layout در گراف‌های بزرگ هنوز قابل بهبود است، اما مدل Graph مستقل از آن باقی می‌ماند.

---

## 19. Demo کامل RelationEngine + SVG

نمونه ساده:

```csharp
using System;
using NP.NumericalModel.Analysis;

namespace NP.NumericalModel.ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            RelationEngine engine =
                new RelationEngine(6);

            ConceptualRelationGraph graph =
                engine.Analyze(252);

            foreach (RelationEdge edge
                in graph.Edges)
            {
                Console.WriteLine(
                    edge.ToString());
            }

            GraphLayout layout =
                new GraphLayout();

            layout.NodeWidth = 110;
            layout.NodeHeight = 42;
            layout.HorizontalSpacing = 50;
            layout.VerticalSpacing = 80;
            layout.Margin = 50;

            layout.Build(graph);

            SvgGraphRenderer renderer =
                new SvgGraphRenderer();

            renderer.ShowRules = true;
            renderer.ShowKinds = false;

            renderer.Render(
                graph,
                layout,
                "252-relations.svg");

            Console.WriteLine();
            Console.WriteLine(graph.ToString());
            Console.ReadLine();
        }
    }
}
```

---

## 20. اصل معماری پروژه

مدل فعلی را می‌توان به شکل زیر دید:

```text
BaseSystem
    ↓
Structural structure
    ↓
RelationAnalyzer ───────┐
FactorRelationFinder ───┤
FactorialStateGenerator ┤
ConceptualRelationRule ─┤
                        ↓
                  RelationEngine
                        ↓
             ConceptualRelationGraph
                        ↓
                GraphLayout
                        ↓
              SvgGraphRenderer
```

لایه نمایش (`SVG`) نباید قوانین عددی جدید ایجاد کند.

قواعد مفهومی نیز نباید با عملیات استاندارد ریاضی یکی فرض شوند.

این تفکیک اجازه می‌دهد در مراحل بعدی مفاهیمی مانند `.`، `:`، `&`، `#`، پایه‌های مختلف، روابط ساختاری و روابط دنباله‌ای بدون خراب کردن هسته فعلی به مدل اضافه شوند.

---

## 21. وضعیت فعلی

نمونه اصلی آزمایش `252` نشان داده است که Engine می‌تواند روابط چندمرحله‌ای، Factor Relation، State، Numeric Result و Conceptual Result را در یک Graph مشترک جمع کند.

در نسخه فعلی تمرکز روی **مدل‌سازی و کشف رابطه** است؛ SVG صرفاً ابزار کمکی برای مشاهده Graph است.
