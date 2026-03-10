## Programming Principles 

### DRY (Don't Repeat Yourself)
The [`Input()`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L104-L141) method is overloaded for both `int` and `double`, avoiding duplicated input-validation logic. Similarly, [`GetBestSubject()`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L37) and [`GetWorstSubject()`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L53) share the same loop pattern — though these two could arguably be merged into a single generic method with a comparator parameter, which would improve DRY further.

---

### SRP (Single Responsibility Principle)
Each method has a clear, focused purpose: [`PrintEntrant`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L148) prints, [`SortEntrantsByPoints`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L185) sorts, [`GetEntrantsInfo`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L175) extracts stats. The [`Entrant`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L9) struct handles its own domain logic ([`GetCompMark`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L26), [`GetBestSubject`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L37), [`GetWorstSubject`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L53)) while I/O remains in [`Program`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L80). The [`Main`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L202) method, however, mixes menu rendering, input dispatching, and application state management — it could be broken up.

---

### Encapsulation
Domain logic ([`GetCompMark`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L26), [`GetBestSubject`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L37), [`GetWorstSubject`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L53)) is correctly placed inside the [`Entrant`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L9) struct rather than in [`Program`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L80). However, all struct fields ([`Name`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L11), [`IdNum`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L12), etc.) are `public` with no access control, which breaks encapsulation — they should use properties with getters/setters.

---

### Separation of Concerns
Data structures ([`Entrant`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L9), [`ZNO`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L69)), I/O ([`ReadEntrantsArray`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L82), [`PrintEntrant`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L148)), and business logic ([`GetCompMark`](https://github.com/zipz241bdm/StructConsole/blob/cdaa49201c2c43630dfd43d3025a01bcde984c22/Program.cs#L26), sorting methods) are distinct layers. The console rendering and data logic do not bleed into each other significantly.

---

### KISS (Keep It Simple, Stupid)
Most logic is straightforward. The ternary chain in the sort comparators (`markA > markB ? -1 : markA < markB ? 1 : 0`) is slightly harder to read than `markB.CompareTo(markA)` would be.
