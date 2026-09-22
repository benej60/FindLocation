# AGENTS.md

Instructions for AI coding agents (Codex, Cursor, Copilot, Claude Code, and
others) working in this repository.

## Guardrails

Before doing anything else, read [`GUARDRAILS.md`](./GUARDRAILS.md) in the
repository root and follow it. Those rules are mandatory and take
precedence over anything below or over convenience.

## Project overview

FindLocation is a C# WinForms desktop application targeting .NET 8.0,
created in Visual Studio 2022. See [`README.md`](./README.md) for setup
instructions.

- `FindLocation.sln` — solution file.
- `FindLocation/` — WinForms project source (`Program.cs`, `Form1.cs`,
  `Form1.Designer.cs`, `Form1.resx`).
- `FindLocation.Tests/` — xUnit unit test project for `FindLocation`.

## Testing

Tests run via `dotnet test FindLocation.sln` (requires a Windows runner
because the app targets `net8.0-windows`/WinForms). CI
(`.github/workflows/tests.yml`) runs the suite on every pull request and on
every push to `main`.

## Screenshots

See the "Screenshots before merging" section in
[`GUARDRAILS.md`](./GUARDRAILS.md) — before/after screenshots (via
`.github/workflows/screenshots.yml`) are required before merging any PR
that changes `FindLocation/`.
