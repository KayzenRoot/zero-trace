# ZT-ADR-012 — Unity DevOps recurring Free enrollment versus time-limited or paid trial

**Status:** PROPOSED — NOT AUTHORIZED FOR ACTIVATION

**Work Order:** ZT-D01-WO-001-QA-01/CD-01

**Date:** 2026-11-26

## Context

QA-01 investigates whether managed Unity Build Automation (UBA) can satisfy the existing zero-new-cost Windows CI gate under the project's Unity Personal workflow. A Unity Cloud project already exists under the narrow owner authorization recorded in ZT-ADR-011. Its earlier Dashboard read-back showed a Free-tier label and monthly usage information alongside generic free-trial wording. The current read-back of Products still shows no active products. No DevOps plan or service has been activated.

The evidence is intentionally account-minimal. No account, project, organization, email, payment, credential, cookie, or raw-log identifier is included here.

## Problem

Unity's public onboarding documentation says “Start free trial to use the free plan,” while the Free Plan documentation and public pricing describe a no-cost plan. Those pages do not establish whether the route shown to this account is a recurring Free entitlement, a time-limited trial, an evaluation that can convert, or a route governed by additional account-specific terms.

Public documents also do not establish this account's UBA entitlement under Unity Personal, exact Windows Micro and Editor availability, or an enforceable limit within the project's stricter operating envelope.

## Official documentary evidence

Sources were reviewed on 2026-11-26:

1. Unity DevOps Get started: https://docs.unity.com/en-us/devops/get-started-end-users — its onboarding text directs users to “Start free trial to use the free plan” or to subscribe to pay-as-you-go. It does not identify this account's enrollment state or conversion terms.
2. Unity DevOps Free plan: https://docs.unity.com/en-us/devops/pricing/free-plan — describes free plan access, lockout when limits are exceeded, read-only access without upgrade, possible deletion of cloud data after 30 days, and usage warning thresholds.
3. Unity pricing changes: https://unity.com/products/pricing-updates — describes public monthly Free allocations and states that continuing after the Free limits requires adding a payment card through the pay-as-you-go checkout. This is public pricing, not account-level entitlement or a hard user-defined cap.
4. Unity Terms of Service: https://unity.com/legal/terms-of-service — last updated 2026-11-16. The general terms cover free/evaluation access and permit automatic conversion of a trial where the applicable Offering Identification says so; they do not determine this account's route.
5. Unity Cloud Additional Terms: https://unity.com/legal/cloud-terms-of-service — last updated 2026-06-30. Unity DevOps and Build Automation are Unity Cloud Offerings; using them requires acceptance of applicable terms. The account-specific set and acceptance status were not established.
6. Unity Editor Software Terms: https://unity.com/legal/editor-terms-of-service/software — last updated 2026-11-16. Section 2.5 governs Unity Build Server and says it requires its own subscription; the Personal restriction for Build Server is not a ruling either for or against managed UBA.
7. Unity UBA build-minute failure guidance: https://docs.unity.com/en-us/build-automation/check-build-results/troubleshoot-build-failures/builds-failing-immediately — says the monthly build-minute cap is checked for an active subscription. It does not prove that the cap is available or effective for this Free account.

## Account-specific observations

- The existing BG01 evidence records a visible Free-tier label and monthly usage display, plus a generic trial-related notice. That earlier read-back is limited and may be delayed; its private financial values are not repeated here.
- The current official Dashboard Products view showed no active products.
- No read-back identified an explicit recurring Free enrollment, an account-specific automatic-conversion rule, UBA entitlement under Personal, or the applicable UBA terms.
- No build target, repository connection, build, test, artifact, or service activation was observed or performed during CD-01.

## Proposed decision

Treat “Start free trial” as an ambiguous onboarding label, not proof of a paid trial and not authorization to enroll. A Free route may be considered only after current official documentation and account-specific evidence establish all of the following:

- recurring Free entitlement and UBA availability for this Unity Personal account;
- no time-limited paid-trial dependency or automatic conversion;
- no payment method, paid consumption, unexpected charge, or additional unreviewed agreement;
- applicable terms identified and reviewed by the owner;
- an effective way to enforce the existing operational cost envelope;
- the exact Windows Micro builder, Windows x64/Mono target, and Editor 6000.3.29f1 are available;
- trusted-branch, test, artifact, privacy, and GitHub-status proof obligations are met.

Until this ADR is independently audited and explicitly approved, all current restrictions remain effective. Even after any terminology clarification, enabling a Unity service requires a separate explicit owner authorization. Any new terms may be reviewed and accepted only by the owner after separate authorization. This proposal does not bypass ZT-ADR-011 or change any QA-01 acceptance criterion.

## Alternatives

A. Keep the existing project inactive pending authoritative account-specific confirmation. **Preferred while blocked.**

B. Consider the recurring Free enrollment route after the above proof obligations, independent review, and separate owner authorization.

C. Paid consumption or a time-limited paid trial. **Rejected** under the current zero-cost gate.

D. Unity Build Server as a substitute for managed UBA under Personal. **Rejected**; the Software Terms treat Build Server separately and require its own subscription.

E. Defer cloud CI and use a local-only workflow. This would require a separate architectural decision and is not authorized here.

## Licensing

Managed UBA is documented as a Unity Cloud service and is distinct from Unity Build Server. The public materials reviewed establish Free-plan documentation and Windows Mono technical support, but do not state an account-specific UBA entitlement under Unity Personal or the applicable plan terms. No legal eligibility conclusion is made.

## Security

No UBA source-control connection, token, GitHub App, webhook, service account, secret, or build configuration was created. Unity's public-repository documentation describes credential-free source access; auto-build requires broader webhook permission. Public integration documentation describes collaboration notifications and webhooks, not a native GitHub check tied to the exact commit. Any status bridge would need a separately reviewed design, least-privilege authorization, and an explicit owner approval. The repository ruleset was not changed.

## Financial constraints

The project's internal stop limits remain unchanged: 100 Windows Micro minutes, 12.5 GB shared storage, 25 GB egress, and US$0 additional cost. Public monthly Free allocations do not replace these stricter limits.

Dashboard usage/cost reports can be delayed or estimated. Informational budget alerts do not stop a service. Public guidance associates the build-minute cap with an active subscription; concurrency limits queue work but do not cap total monthly consumption. A per-build API timeout can cancel a long job (with a 12-hour default when unset), but it does not bound cumulative monthly usage. Artifact-retention rules are evaluated periodically and do not impose a hard storage or egress ceiling. None of these controls has been established as an effective enforcement mechanism for the Free account's internal stop limits.

## Proof obligations

Before considering any service activation, verify without relying on a generic badge:

1. explicit current plan and UBA entitlement for this Unity Personal account;
2. no time limit, paid-trial conversion, payment method, pay-as-you-go route, or unreviewed terms;
3. owner review of the exact terms applicable to the account;
4. current remaining quotas and an enforceable 100-minute/12.5-GB/25-GB operating envelope, including cumulative use, storage and egress;
5. Windows Micro availability, Windows 11 builder OS, Windows Mono support, and exact Editor 6000.3.29f1 selector availability;
6. trusted main-only manual execution, clean/no-cache behavior, EditMode and PlayMode test settings, fail-on-test behavior, private artifact handling, and exact commit provenance;
7. a documented native GitHub status or an independently reviewed, explicitly approved status-bridge design;
8. independent QA-01 audit before any approval or checkpoint promotion.

## Future activation gate

This ADR is not an activation authorization. Any future activation requires: approved ADR-012; a separate owner instruction naming the precise service and action; owner review/acceptance of applicable terms; proof of the zero-cost and enforceable-limit gates; read-back of exact builder, Editor, branch and test configuration; and the separate approvals for credentials or status publishing, if required. Stop before any paid checkout, trial, changed terms, billing method, unsupported machine/version, missing cap, untrusted trigger, or unclear entitlement.

## Consequences

QA-01 remains BLOCKED. ZT-ADR-009 continues to reserve Unity cloud CI qualification for QA-01, ZT-ADR-010 remains local-development-only, and ZT-ADR-011 remains limited to the already-authorized empty-project bootstrap. D01 remains incomplete; D02 and release remain blocked. No cloud CI or artifact is claimed.

## Reversal

If the proposal is rejected or superseded, revert this ADR proposal and its Decision Ledger entry through a normal reviewable commit. No Unity Cloud resource, license, package, or account change is required to reverse this documentary proposal.

## Outstanding uncertainties

- Whether this account is enrolled in a recurring Free plan or only has a visible Free-tier entry point.
- Whether any trial period or automatic conversion applies to this account.
- Whether managed UBA is eligible under the account's Personal terms and entitlement.
- Whether cumulative usage can be kept inside the existing internal stop limits.
- Whether Windows Micro and exact Editor 6000.3.29f1 are selectable for this account.
- Whether UBA can produce a native GitHub commit status/check for the exact commit.

## Owner approval record

No owner approval for ADR-012 adoption or DevOps/UBA activation has been recorded. This document is a proposal only.
