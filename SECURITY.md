# Security Policy

## Scope

This repo is an unofficial .NET SDK for the Paystack API. Two very different kinds of "security issue" can come up:

- **A vulnerability in this SDK** — e.g. a bypass in `PayStackWebhookSignatureVerifier`'s signature check, a logging path that could leak a secret key, an insecure default. **Report this here**, privately (see below).
- **A vulnerability in Paystack's platform itself** (their API, dashboard, or infrastructure) is out of scope for this repo — report it directly to Paystack. This SDK is a thin client; it can't fix upstream security issues, only reflect their behavior.

## Reporting a vulnerability in this SDK

Please **do not** open a public GitHub issue for a security vulnerability. Instead, use GitHub's private vulnerability reporting (Security tab → "Report a vulnerability") on this repository, or email the maintainers directly if that's not available.

Include:
- The affected version(s).
- A minimal reproduction (a failing test is ideal — see `PayStackWebhookSignatureVerifierTests.cs` for the style used here).
- The impact as understood (e.g. "an attacker who can guess X can forge a webhook delivery").

Reports are acknowledged within a few days, with a fix targeted before any public disclosure.

## Supported versions

Only the latest published major version receives security fixes. Given this SDK tracks Paystack's own evolving API closely, pinning to an old version for a long time isn't recommended regardless of security fixes.
