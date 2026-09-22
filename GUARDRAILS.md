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
- Add or update unit tests in `FindLocation.Tests` for any behavior you add
  or change. The `Tests` GitHub Actions workflow
  (`.github/workflows/tests.yml`) runs the test suite automatically on
  every pull request (before merge) and on every push to `main` (after
  merge) — do not merge a PR with a failing test run.

## Git workflow

- Every change goes through a pull request — do not push directly to
  `main`/`master`. Work on a feature/task branch and open a PR for it.
- Open one pull request per logical change; do not batch unrelated
  changes into a single PR.
- Never merge or approve your own pull request on your own initiative.
  Only merge a PR you authored when the user explicitly instructs you to
  merge it in that conversation.

## Screenshots before merging

- Before merging a pull request that changes the running application
  (anything under `FindLocation/`), get before (base branch) and after
  (PR head) screenshots of the app and show them in the conversation. The
  `PR Screenshots` GitHub Actions workflow
  (`.github/workflows/screenshots.yml`) produces these automatically: it
  builds and briefly launches the app on a `windows-latest` runner (the
  app is WinForms and can only run on Windows, which this repo's
  contributors may not always have locally) and uploads `before.png` /
  `after.png` as workflow artifacts for that PR's run. It also pushes them
  to the `screenshots` branch (`latest/before.png`, `latest/after.png`,
  overwritten each run) since workflow artifacts are hosted on Azure Blob
  Storage, which some sandboxed agent environments' network policy
  blocks — the `screenshots` branch is fetchable through the ordinary
  GitHub API/contents endpoint instead. The workflow can also be run
  on-demand (`workflow_dispatch`, no PR needed) to get a single current
  screenshot, e.g. to answer "show me the main form".
- A PR that doesn't touch `FindLocation/` (docs, CI, config) has nothing
  to screenshot — say so instead of attaching identical or fabricated
  images.
- Never fabricate, describe-instead-of-show, or reuse a stale screenshot.
  If the screenshot workflow hasn't run yet or failed, say that plainly
  and get it green before merging.

## Downloadable binary

- When opening a pull request (or whenever a runnable local build is
  otherwise requested), give a download link to a binary of the app that
  can be run locally. The `Build Binary` GitHub Actions workflow
  (`.github/workflows/build-binary.yml`) produces this automatically: it
  publishes a self-contained, single-file `win-x64` build of `FindLocation`
  on a `windows-latest` runner and pushes it to the `builds` branch
  (`latest/FindLocation.exe`, overwritten each run), for the same reason
  screenshots are pushed to the `screenshots` branch — workflow artifacts
  are hosted on Azure Blob Storage, which some sandboxed agent
  environments' network policy blocks, while a plain branch is fetchable
  through the ordinary GitHub API/contents endpoint or a
  `raw.githubusercontent.com` link. The workflow can also be run on-demand
  (`workflow_dispatch`, no PR needed).
- Trigger the workflow (or wait for it to run on the PR) before handing out
  the link, and make sure it actually succeeded — never fabricate or reuse
  a stale/unrelated binary link.

## Uncertainty

- If a request is ambiguous, destructive, or would affect shared/external
  systems, ask before proceeding rather than guessing.
