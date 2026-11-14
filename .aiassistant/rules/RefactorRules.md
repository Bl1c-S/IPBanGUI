---
apply: manually
---

# AI Coding & Refactoring Rules (JetBrains Rider)

You are an expert software engineer and code quality reviewer. Adhere strictly to the following rules when analyzing, writing, or refactoring code in this solution.

## 1. Clean Code & Idiomatic C# / .NET
- **Readability & Standards:** Follow standard Microsoft / C# naming conventions (PascalCase for methods and properties, camelCase for local variables and parameters, `_camelCase` or `m_camelCase` for private fields).
- **Single Responsibility Principle (SRP):** Ensure methods and classes have a single, well-defined focus. Keep methods concise (ideally under 15–25 lines).
- **Early Returns & Guard Clauses:** Eliminate deep indentation and cyclomatic complexity by using guard clauses (`if (arg is null) throw ...;` or early `return;`).
- **Pattern Matching & Modern Syntax:** Leverage modern language constructs where clarity permits (e.g., switch expressions, property patterns, null-coalescing `??`, and null-conditional `?.` operators).

## 2. Duplicate Detection & Code Smells
- **DRY (Don't Repeat Yourself):** Actively scan for repetitive logic, identical LINQ queries, duplicate validation rules, or repeated object-mapping routines. Proactively suggest extracting them into reusable extension methods, private helper functions, or shared services.
- **Dead Code Elimination:** Flag unused `using` directives, unreferenced local variables, commented-out dead code, and unreachable paths.
- **LINQ & Allocation Smells:** Watch for multiple enumerations of `IEnumerable<T>`, premature materialization (excessive `.ToList()`), or allocation overhead in tight loops.

## 3. String Extraction & Magic Value Handling
- **Magic Strings & Numbers:** Never leave hardcoded strings or numeric literals scattered inside business logic or service methods.
- **Extraction Rules:**
    1. Extract configuration names, message templates, or domain constants into descriptive `const` values or static classes (e.g., `private const int MaxRetryAttempts = 3;`).
    2. For client-facing text or messages, direct them to resource files (`.resx`) or dedicated localization providers according to project architecture.
- **String Formatting:** Prefer interpolated strings (`$"{entity.Name}: {status}"`) over `string.Concat` or `string.Format`.

## 4. Reliability, Null Safety & Resource Management
- **Nullable Reference Types (NRT):** Respect NRT warnings (`#nullable enable`). Avoid suppressing warnings using the null-forgiving operator (`!`) unless an upstream guarantee exists.
- **Resource Cleanup (IDisposable):** Ensure objects implementing `IDisposable` or `IAsyncDisposable` are handled via `using` declarations/statements to prevent leaks (streams, HTTP connections, DB contexts).
- **Async/Await Correctness:**
    - Always propagate cancellation tokens (`CancellationToken ct = default`) where available.
    - Avoid sync-over-async anti-patterns like `.Result` or `.Wait()`.
    - Handle exceptions cleanly without swallowing details; rethrow preserving call stack (`throw;` instead of `throw ex;`).

## 5. Refactoring Output Format
When proposing refactored code:
1. **Summary of Improvements:** Briefly list what was refactored (e.g., *simplified nested conditionals into pattern matching, eliminated multiple enumerations*).
2. **Before vs. After (or Diff):** Provide focused code blocks highlighting the before and after states.
3. **Justification:** Briefly state why the new structure is more maintainable, idiomatic, or performant.