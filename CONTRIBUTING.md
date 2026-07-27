# Contributing to PayStack.Net

Thanks for considering a contribution. This is an unofficial, community-maintained .NET SDK for the [Paystack](https://paystack.com/docs/api/) API — not published or endorsed by Paystack itself.

## Before starting

- **Bugs in Paystack's own API** (wrong status codes, a field that doesn't match its docs, an endpoint behaving unexpectedly) belong with Paystack support, not this repo. This SDK is a thin, typed wrapper — it can't fix upstream behavior, only reflect it accurately (and, where reasonable, work around it, e.g. the lenient boolean converter for endpoints that inconsistently serialize `0`/`1` instead of `true`/`false`).
- **Bugs in this SDK** (wrong model shape, incorrect resilience behavior, a broken Refit interface, docs that don't match the code) are exactly what this repo is for.
- **Feature requests** for new Paystack resources are welcome once Paystack's own docs cover them.

Open an issue before a large PR (a new resource client, a resilience change, a webhook model change) so the approach can be agreed on first. Small, obvious fixes (a typo, a missing null check, a doc correction) can go straight to a PR.

## Project layout

```
src/PayStack.Net/
  Resources/           Refit interfaces, one per resource (ITransactionsClient, ICustomersClient, ...).
  Models/Requests/      Request DTOs.
  Models/Responses/     Response DTOs.
  Models/Common/        Shared sub-objects and the string-constant classes for enum-shaped fields.
  Http/                  Auth/logging/idempotency DelegatingHandlers, resilience exception types.
  Webhooks/              Signature verification, event envelope, ASP.NET Core convenience extension.
  Configuration/         PayStackOptions, PayStackEnvironment.
tests/PayStack.Net.Tests/  xUnit tests — webhook verification, event parsing, options resolution.
samples/PayStack.Net.Sample/  A runnable minimal-API sample (checkout, verification, webhook receiver).
```

## Adding or updating a resource

There's no code generator here — every DTO and Refit interface is hand-written directly from Paystack's published API reference. When Paystack's docs change:

1. Update the affected Refit interface(s) under `Resources/` and the DTOs under `Models/Requests/`/`Models/Responses/`.
2. Follow the existing conventions closely: enum-shaped API fields stay `string` + a companion `static class PayStack<Thing>` of `const string` values, never a C# `enum` (this keeps the SDK forward-compatible with new values Paystack adds without a breaking release — see the README's "Design notes"). Money fields are `long`/`long?` in the currency's subunit unless the specific endpoint documents amount as a `string`. Loosely-structured or polymorphic fields (a field that's a bare id on one endpoint and an expanded object on another) are typed `System.Text.Json.JsonElement?`.
3. Every interface method and public DTO needs an XML doc comment (`<summary>`, `<param>` for every parameter) — this SDK's whole value proposition is IntelliSense without leaving the IDE.
4. Update `README.md` and, if the change is significant enough that future AI-assisted contributions would benefit from knowing about it, the `.claude/skills/paystack-net/SKILL.md` reference.

## Building and testing

```bash
dotnet build PayStack.Net.sln
dotnet test tests/PayStack.Net.Tests/PayStack.Net.Tests.csproj
```

The library targets `net8.0;net9.0;net10.0`; a PR shouldn't introduce anything that only compiles on one of them without a good reason.

New behavior needs a test. In particular:
- Anything touching **webhook signature verification** needs a test — this is the one place a subtle bug is a real security hole, not just an inconvenience.
- Anything touching **idempotency key handling** on money-movement endpoints (transfers, refunds, recipient creation) needs a test.
- A packaging/build-output change (anything under `Directory.Build.props` or the library's `.csproj` metadata) should be verified by inspecting a local `dotnet pack -c Release` output for accidental local-path leakage before merging — see "Releasing" below.

## Code style

Follow `DevelopmentRules.md` at the repo root for anything not already covered by existing conventions in the codebase (naming, XML doc comments on every public member, structured logging, no premature abstraction). When in doubt, match the surrounding code rather than introducing a new pattern.

- Every public type and member needs an XML doc comment.
- Enum-shaped API fields stay `string` + a companion constants class, never a C# `enum` — see the README's "Design notes" for why.
- Don't add a dependency (NuGet package) without discussing it in an issue first — this SDK's dependency footprint is deliberately small.

## Pull requests

- Keep PRs focused — one resource, one bug, one feature.
- Include the `dotnet test` output (or CI passing) and, for anything that changes request/response shapes, a note on what part of Paystack's docs it's based on.
- Update `README.md` alongside any user-visible change — an SDK feature without a doc example doesn't really exist for most users.

## Releasing (maintainers only)

Publishing is tag-triggered, not automatic on every merge to `master` — see `.github/workflows/publish.yml`. To ship a release:

1. Merge whatever's going into the release into `master`.
2. Tag it: `git tag v1.1.0 && git push origin v1.1.0`. The tag's version (not the `<Version>` in the `.csproj`) becomes the package version.
3. CI builds, tests, packs, and publishes to NuGet.org automatically via **Trusted Publishing** (OIDC) — no API key is stored in this repo at all.

Publishing uses NuGet.org's [Trusted Publishing](https://learn.microsoft.com/nuget/nuget-org/trusted-publishing): the `publish` job requests a short-lived (1 hour) NuGet API key at run time by exchanging GitHub's OIDC token for one, scoped to a NuGet.org policy that only trusts runs of *this exact workflow file* (`publish.yml`) under *this exact GitHub Actions environment* (`nuget`). There's no long-lived secret to leak, rotate, or restrict access to — the trust boundary is "can you make `publish.yml` run under the `nuget` environment," which is controlled entirely by GitHub repo/environment permissions (Settings → Environments → `nuget` → protection rules), not by who can see a secret.

**First-time setup**, if the Trusted Publishing policy doesn't exist yet: nuget.org account → *Trusted Publishing* → add a policy for this repo, workflow file `publish.yml`, environment `nuget`. The policy starts in a 7-day "pending" state and only becomes permanent after the first successful publish through it.

## Reporting a security issue

See `SECURITY.md` — please don't open a public issue for a signature-verification bypass or similar; email instead.
