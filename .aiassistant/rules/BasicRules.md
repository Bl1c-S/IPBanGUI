---
apply: always
---

# Core Directives

1. STRICT SCOPE
    - Execute only the explicit instructions provided.
    - Do not inspect, open, or explore directories, files, or projects outside the immediate scope of the current task.

2. INTENTIONAL NAVIGATION
    - Follow symbol definitions, references, and import/using paths only when strictly required.
    - Before reading or opening any file, you must explicitly confirm that its contents are directly necessary to resolve the current objective. Zero blind wandering or broad pattern scans.

3. LEVERAGE RIDER & MCP TOOLS
    - Prioritize JetBrains Rider's MCP (Model Context Protocol) tools and IDE capabilities (solution analysis, symbol lookup, go-to-implementation, references, and structure tree) to gather context.
    - Never crawl directories or parse source files manually when IDE indices or MCP tools can locate the target directly.