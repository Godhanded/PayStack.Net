# C# Development Rules

## Architecture & Design

- Follow **Clean Architecture** with strict inward-only dependencies: `Domain` → `Application` → `Infrastructure` → `Api`. Nothing in an inner layer references an outer one.
- `Domain` has no dependencies on any other project or framework package — entities, `Result`/`Error`, domain events, and enums only.
- Use **CQRS with MediatR** for all commands and queries. One handler per file, organized as `Features/<Area>/Commands|Queries/<Verb><Noun>/`.
- Use a **generic repository** (`IRepository<TEntity>`, one EF Core-backed implementation) instead of a bespoke repository per entity. Its methods take `params` include expressions and predicate expressions directly (`GetByIdAsync(id, ct, includes)`, `ListAsync(predicate, ct, includes)`) — no specification pattern; a named spec class per query is ceremony most queries here (used from one call site) don't pay back. `Update()` re-attaches a detached/untracked entity. Reserve direct `IApplicationDbContext` access in handlers for genuinely cross-aggregate queries that don't belong on a single repository.
- Default to **read-only/untracked** repository methods for queries; use **tracked** methods only in command handlers that go on to mutate and save. If a specific filter/include combination becomes genuinely reused across several call sites, wrap it in a small static helper or extension method — don't add a spec framework pre-emptively.
- Business logic spanning multiple aggregates lives in an Application-layer **domain service**, not duplicated across handlers.
- Rely on EF Core's implicit per-`SaveChangesAsync` transaction as the default unit of work. Only use an explicit `BeginTransactionAsync`/`CommitAsync`/`RollbackAsync` when a handler genuinely needs more than one `SaveChangesAsync` call (or EF plus raw SQL) to succeed or fail together — check first whether restructuring into a single `SaveChangesAsync` call removes the need.
- Prefer **immutable records** where appropriate for DTOs and domain events.
- Apply **atomic transactions** — one `SaveChangesAsync` per command handler; a command's business-entity change and any outbox row it produces are written in the same call.

## Object Creation & Type Safety

- **Never use `new` outside an entity** — instantiate via a static factory `Create()` method with a private/protected constructor.
- `Create()` validates domain invariants before instantiation and returns the **Result pattern** whenever creation can fail.
- Entity behavior is exposed as named methods (`Cancel`, `MarkPaid`), never public setters. A behavior method that can be rejected returns `Result`.
- Everything is **statically typed** — no `dynamic`, no `object` where a concrete type will do.

## Error Handling

- **Always use the Result pattern** instead of throwing exceptions for expected failures (not found, validation, conflict, unauthorized, forbidden).
- `Error` carries a `Code`, a `Message`, and an `ErrorType` so controllers can map failures to HTTP status codes without string-matching.
- A **global exception handler** (`IExceptionHandler`) catches anything that still escapes as a real exception and serializes it as the same `Result`-shaped JSON body every other endpoint returns.
- Controllers return the **entire Result object** via a single `ToActionResult()` extension — never hand-roll status codes or return bare data/error strings.

## Validation

- Use **FluentValidation** for all input validation (DTOs, commands, queries), one validator per request in the same feature folder as the request.
- Wire validation into the MediatR pipeline as an `IPipelineBehavior` that short-circuits into a failed `Result` — controllers and handlers never call validators directly.
- Factory `Create()` methods additionally enforce domain-level rules that don't belong in a DTO validator.

## Control Flow

- Prefer **early returns/guard clauses** over nested if-else statements. A handler should read top-to-bottom as a checklist of preconditions, with the happy path at the bottom.

## Domain Events

- Entities **raise** domain events to describe facts about themselves (`MarkPaid()` raises `BookingConfirmedEvent`); they never know who reacts.
- Events are dispatched from exactly one place — an EF Core `SaveChangesInterceptor` (or an override of `SaveChangesAsync`) — **after** the transaction commits, via `IPublisher`.
- **Multiple handlers per event are expected.** Each side effect (email, push notification, analytics, etc.) is its own `INotificationHandler<TEvent>`; adding a side effect means adding a handler class, not editing an existing one.
- Logic whose failure must fail the whole command stays inline in the command handler with a `Result` — it is not a domain event.
- Non-critical event handlers catch and log their own exceptions rather than faulting the triggering request.

## Outbox Pattern

- Any call that leaves the process — payment provider calls, email, SMS, third-party webhooks — is written to an **outbox table** in the same `SaveChangesAsync` call as the business change that caused it, not called inline.
- A background worker polls unprocessed outbox rows, performs the external call, and marks the row processed or records the error for retry with backoff.
- External-call handlers must be **idempotent** (dedupe by outbox message id) since delivery is at-least-once, not exactly-once.
- Use the outbox specifically where "the external call happened but we crashed before recording it" would cause real harm (payments, payouts, transactional emails). Fire-and-forget domain event handlers are sufficient for effects that are safe to drop on failure.

## Background Job Processing

- Pick one job runner for the solution — **Hangfire** or **Quartz.NET** — and use it consistently rather than mixing runners across features.
- **Hangfire** is the default recommendation for outbox polling and general background work: built-in Postgres storage, a dashboard for visibility into queued/failed/retried jobs, and minimal setup for retry-with-backoff.
- **Quartz.NET** is the better fit when a project needs complex cron-style scheduling (multiple interdependent triggers, blackout calendars) or clustering guarantees beyond what Hangfire provides.
- Record the choice once at the project level; it isn't a per-feature decision.

## Logging

- Use **Serilog** for all logging, configured via `UseSerilog(...)` in `Program.cs`, with sinks read from configuration.
- Always include a **Seq** sink for structured log aggregation, plus a console sink for local development.
- Log with **structured properties** (`LogInformation("Booking {BookingId} confirmed", bookingId)`), never string-interpolated messages — interpolation loses the fields Seq indexes on.
- Never log secrets, tokens, or PII payloads. Log identifiers, not raw request/response bodies.
- Log exceptions with the exception object as the first argument (`LogError(ex, "...")`), not stringified into the message.

## Security

- Never expose sensitive data in logs or responses.
- Store and compare tokens/secrets using hashing where applicable (e.g. refresh tokens); never persist raw secrets.

## Documentation

- All controller actions have **XML documentation** (`<summary>`, `<response code="...">`) describing behavior and meaningful non-200 responses — this is consumer-facing API documentation, not implementation narration.
- Add comments elsewhere only where the *why* isn't obvious from the code (a workaround, a non-obvious invariant, a race condition being guarded against). Don't restate what the code already says.

## Code Quality

- Maintain consistent naming conventions.
- Keep methods focused and single-purpose.
- Write testable code with clear dependencies.
- **Never implement TODOs, stubs, or throw `NotImplementedException`** — all code must be production-ready. Stubs may only exist with explicit sign-off from the requester.

## Testing

- Domain entities get unit tests with no mocking — `Create()`, invariant-guarded behavior methods, etc. are pure and should be tested as such.
- Application handlers touching concurrency-sensitive state (e.g. claiming a limited resource, verifying a payment) get integration tests against a real database, not a mocked context — mocks can't reproduce races or constraint violations.
- "No tests" is not a shippable state for code touching money, availability, or auth.

## Performance

- Optimize critical code paths for performance without sacrificing readability.
- Avoid premature optimization — write clean, maintainable code first.
- Use **asynchronous programming** where appropriate to improve responsiveness.
- Cache results of expensive operations where it measurably helps.
