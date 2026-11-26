# ADR-011 owner authorization and Unity Cloud project bootstrap | 2026-11-26

Work Order: `ZT-D01-WO-001-QA-01`  
Issue/PR: #28 (execution PR remains draft)  
Branch: `wo/zt-d01-wo-001-qa-01-exec`  
ADR: `docs/adr/ZT-ADR-011.md`

## Owner authorization

The owner explicitly authorized ADR-011 and exactly one empty Unity Cloud project named **ZERO TRACE** in the existing organization **kayzenweb3dev**.

The authorization does not extend to:
- activating Unity Build Automation, Unity DevOps source control or any other service;
- starting/converting a trial, accepting new/changed terms, or changing subscription;
- payment method, pay-as-you-go, billing changes or charges;
- credentials, tokens, webhooks, service accounts, GitHub Apps or secrets;
- build targets, auto-build, cloud builds/tests, artifacts or deployments;
- a second project, account or organization;
- code/package changes, checkpoint promotion, PR merge, QA-01/D01 approval or D02.

If the official creation flow requires any prohibited step, stop before that step. No consent may be inferred.

## Basis and purpose

The prior owner-operated Dashboard preflight showed the existing organization selected, no project, US$0.00 spend over the preceding seven days, no payment method, and no data in the current billing-cycle report (with a warning that reporting may be delayed up to 24 hours). It did not expose explicit DevOps Free plan, UBA entitlement, actual quotas/usage, Windows Micro builder, or exact Editor patch availability.

The one-project bootstrap is intended only to expose project-scoped account read-back. It is not evidence that UBA is free, entitled, licensed or safe to activate.

## Execution sequence

1. Fresh Source Check and PR/branch/Context Lock verification.
2. Reuse the existing authenticated account and organization only.
3. Create exactly one empty project named `ZERO TRACE` in `kayzenweb3dev`, only if the official UI allows it without new/changed terms, trial, service activation, billing/payment, credentials, webhook or build configuration.
4. If a COPPA audience question appears, use the previously recorded owner answer that the project is not primarily directed to children under 13 only if that record is available to the operator; otherwise stop and request owner reconfirmation.
5. Read back project name and organization.
6. Verify no Unity service/UBA, build configuration, credential, webhook, billing change or build was activated.
7. Inspect whether project-scoped Dashboard pages now expose UBA entitlement, explicit Free plan, current quota/usage, Windows Micro and exact Editor `6000.3.29f1`.
8. If any required read-back remains absent, keep AC02–AC04 BLOCKED. Do not activate UBA; report the gap for independent audit and separate owner authorization.

## Execution status in this evidence commit

**AUTHORIZED / NOT YET EXECUTED.** The GitHub documentation and Work Order have been updated on the existing PR #28 branch. The browser connector available in this Chat session provides read-only tab inspection and reported that no originating browser context is attached; it does not provide a supported browser-control action here. Therefore no Unity Dashboard write was attempted and no project creation is claimed. The owner can run the updated one-shot prompt in the already-authenticated Codex Desktop browser, or perform the single project creation manually under ADR-011.

No UBA/service, trial, terms acceptance, credential, webhook, billing/payment change, build/test or artifact was created. ADR-009 and ADR-010 remain unchanged. The canonical checkpoint remains untouched and unpromoted.

## Verification checklist for the executor

- [ ] Existing organization `kayzenweb3dev` confirmed.
- [ ] Exactly one project named `ZERO TRACE` created.
- [ ] Project organization matches the existing organization.
- [ ] No service activated.
- [ ] No trial or new/changed terms accepted.
- [ ] No payment/billing method or setting changed.
- [ ] No credential, token, webhook or secret created.
- [ ] No build target, auto-build, build, test or artifact created.
- [ ] Sanitized project metadata read-back attached.
- [ ] Project-scoped plan/entitlement/quota read-back attached, or explicitly recorded unavailable.

**Current verdict:** ADR-011 approved for narrow bootstrap; BG01 pending execution; QA-01 remains BLOCKED; D01 incomplete; D02/release blocked.

## GitHub read-back after the ADR-011 documentation update

- PR #28 HEAD at the final pre-lock audit: `18f18d94a6f9e1ec2e3933e85d5bee5863f6a7c2`.
- Base `main`: `1b72c6773fc9edd39c44239ec127a75d7ca9d158`; unchanged.
- PR remains open and draft; no merge.
- Exact-HEAD checks: `docs-validation`, `Socket Security: Project Report`, and `Socket Security: Pull Request Alerts` all completed successfully (3/3).
- Changed paths: 11 documentation-only files; zero changes under `Assets/`, `Packages/` or `ProjectSettings/`.
- The six paths listed in `authorized_branch_deltas` in the Context Lock were read back and their Git blob SHAs matched exactly.
- Ruleset 28038825 remains active with zero bypass, squash-only, linear history and required `docs-validation`.
- No Unity Dashboard write or project creation was performed in this run. The empty-project action remains authorized but pending execution in the authenticated Codex Desktop browser session. No project existence is claimed.

