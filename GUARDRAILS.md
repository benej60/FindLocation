# Guardrails

These rules apply to any AI coding assistant working in this repository
(Claude Code, GitHub Copilot, Cursor, Codex, or any other agent). They are
mandatory and take precedence over convenience, speed, or an assistant's
own defaults.

## Security

- Never hardcode secrets, API keys, connection strings, or credentials.
  Use configuration/environment variables and keep them out of source
  control.
- Never commit files that contain secrets or personal data, even
  temporarily. Check `git status`/diff contents before staging or
  committing.
- Validate and sanitize any input that crosses a trust boundary (user
  input, files, network responses). Avoid classes of vulnerability such as
  injection, path traversal, and insecure deserialization.
- Don't disable or weaken security controls (TLS verification, auth
  checks, input validation) to make something "just work."

## Destructive and hard-to-reverse actions

- Never force-push, rewrite published history, or run `git reset --hard`,
  `git clean`, or similar destructive commands without explicit user
  confirmation.
- Never delete branches, files, or data without confirmation unless they
  were created as scratch/temporary output in the current session.
- Never skip git hooks (`--no-verify`) or bypass commit signing unless the
  user explicitly asks for it.
- Before any command that could discard uncommitted work, check
  `git status` first and stash or commit anything at risk.

## Scope and code quality

- Make the minimal change needed to satisfy the request. Don't add
  unrequested features, refactors, or abstractions.
- Don't add error handling, fallbacks, or validation for scenarios that
  cannot occur; don't leave half-finished implementations.
- Prefer editing existing files over creating new ones. Don't create
  documentation files unless explicitly requested.
- Keep commit messages clear and descriptive, and focus on the "why."

## Testing and verification

- Run the project's build/tests for anything you change, when they exist.
- Never skip, disable, or quarantine a failing test to make CI green;
  find and fix the root cause instead.

## Git workflow

- Do not push directly to `main`/`master` — work on a feature/task branch.
- Do not create a pull request unless explicitly asked to.
- Never merge or approve your own pull request.

## Uncertainty

- If a request is ambiguous, destructive, or would affect shared/external
  systems, ask before proceeding rather than guessing.
