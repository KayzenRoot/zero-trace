# ZT-ADR-011 | Bootstrap one empty Unity Cloud project for entitlement read-back

Status: **APPROVED — OWNER-AUTHORIZED, NARROW SCOPE ONLY**  
Date: 2026-11-26  
Applies to: `ZT-D01-WO-001-QA-01`, issue #28, PR #28.

## Context

The existing Unity organization `kayzenweb3dev` is selected in the official Unity Dashboard and the owner is recorded as its Primary Owner. The organization has no Unity Cloud project. The preflight showed US$0.00 spend for the previous seven days and no payment method, but the current billing-cycle report had no data and may be delayed. The organization dashboard did not expose the DevOps Free plan, UBA entitlement, effective quotas/usage, Windows Micro builder, or exact Editor patch availability before a project existed.

Public pricing and general documentation are not evidence of this account's entitlement or remaining quota. ADR-009 remains authoritative: Unity cloud CI is NOT QUALIFIED until QA-01 proves lawful entitlement, zero cost, limits, security, tests, artifact and trusted-branch integration. ADR-010 remains the local development decision and does not grant cloud entitlement.

## Owner authorization

On 2026-11-26, the owner explicitly authorized:
- this ADR and the bounded change to QA-01;
- creation of **exactly one empty Unity Cloud project** named `ZERO TRACE`;
- use of the **existing** Unity organization only.

The owner explicitly did **not** authorize:
- activating Unity Build Automation, Unity DevOps source control, or any other Unity service;
- starting or converting a trial, accepting new or changed terms, or changing subscription/plan;
- adding a payment method, enabling pay-as-you-go, changing billing or accepting a charge;
- creating credentials, tokens, webhooks, service accounts, GitHub Apps or secrets;
- configuring build targets, auto-build, running builds/tests, uploading artifacts or deploying;
- creating another account, organization or project;
- changing Unity project files/packages, promoting the checkpoint, merging PR #28, declaring QA-01/D01 complete, or starting D02.

## Decision

1. Permit creation of one empty project named `ZERO TRACE` in the existing `kayzenweb3dev` organization, solely to determine whether the Dashboard then exposes the account's plan, entitlement, quotas, usage and supported build options.
2. Project creation is a **read-back prerequisite only**. It does not activate UBA or any other service, prove a Free entitlement, authorize billing, or satisfy AC02–AC04.
3. Use the existing authenticated official Dashboard. Do not use undocumented APIs, browser-cookie extraction, generic automation, or alternate accounts.
4. If the flow requires a new/changed agreement, terms acceptance, trial, service activation, payment method, billing change, credential, webhook, build configuration or any other action outside the owner authorization, stop **before** that action and record the exact blocker. Do not click through or infer consent.
5. If a COPPA audience declaration appears, use only the owner's previously recorded answer that ZERO TRACE is not primarily directed to children under 13, and only if that recorded answer is visible/available to the operator. If it cannot be verified, stop and request owner reconfirmation; never guess.
6. After creation, perform read-only verification of the project name and organization and verify that no Unity service, UBA configuration, credential, webhook, billing change or build was activated. Capture sanitized evidence. Do not create a second project.
7. If the project does not expose the missing account entitlement/quota read-back, QA-01 remains BLOCKED. Do not escalate automatically to service activation.

## Consequences

- The project bootstrap may expose the Dashboard surfaces needed to inspect DevOps entitlement and usage; this is a hypothesis to verify, not a guaranteed outcome.
- No UBA capability, quota, cost or legal entitlement is inferred from successful project creation.
- AC02–AC04 remain BLOCKED until actual account read-back proves them. AC05–AC09 remain unexecuted or blocked until their own gates pass.
- ADR-009, ADR-010, the D01 Definition of Done, final QA-01 requirements, zero-new-cost constraint, 100 Windows Micro minute / 12.5 GB storage / 25 GB egress operational stops, no-auto-build rule and no-credential rule remain unchanged.
- No checkpoint promotion, merge, D02 or release is permitted by this ADR.

## Alternatives considered

- Continue waiting for the pre-project organization dashboard: rejected because it does not expose the needed project-scoped DevOps surfaces.
- Activate UBA to reveal entitlements: rejected because the owner has not authorized service activation and account entitlement/cost is unverified.
- Create another organization/account or use a trial/payment method: rejected as outside scope and contrary to zero-cost constraints.

## Reversal

Do not delete the project automatically. Any deletion, service activation, billing change, or expansion beyond this single empty project requires a separate explicit owner authorization and a reviewed Work Order/ADR. If unexpected services or charges appear, stop, preserve evidence and escalate to the owner; do not perform destructive cleanup.

## Approval record

Owner authorization in chat on 2026-11-26 explicitly approves this narrow ADR and one empty project creation. This approval is not approval of UBA activation, service terms, a paid/free plan, cloud builds, QA-01, D01, or D02.
