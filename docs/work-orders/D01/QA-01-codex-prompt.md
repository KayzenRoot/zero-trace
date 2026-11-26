# Codex one-shot | ZT-D01-WO-001-QA-01

**Owner authorized QA-01 and the narrow ADR-011 bootstrap on 2026-11-26.** Begin with fresh Source Check. You may create exactly one empty Unity Cloud project named `ZERO TRACE` in the existing `kayzenweb3dev` organization, solely to expose account read-back. This is not authorization to activate UBA/other services, accept new terms, start trials, create credentials/webhooks, change billing, configure builds, run cloud builds/tests or implement code.

Repository: `KayzenRoot/zero-trace`
Issue: #28
Branch: `wo/zt-d01-wo-001-qa-01-exec`
Base main at execution start: `1b72c6773fc9edd39c44239ec127a75d7ca9d158`

Read in full:
- `docs/work-orders/D01/ZT-D01-WO-001-QA-01.md`
- `docs/locks/ZT-D01-WO-001-QA-01.json`
- `docs/adr/ZT-ADR-009.md`
- `docs/adr/ZT-ADR-010.md`
- `docs/Checkpoint.md`
- `docs/Security.md`
- `docs/DefinitionOfDone.md`

Execution sequence:
1. Fresh Source Check and Context Lock. Inspect GitHub main, PR/issue, 122 project blobs, 24 critical fingerprints, GDD/catalog hashes, package lock and effective ruleset. Current base is `1b72c6773fc9edd39c44239ec127a75d7ca9d158`; the prior planning PR #29 is merged. Stop if stale.
2. Perform read-only owner-operated Unity Dashboard preflight. Reuse only the existing account/organization. Do not accept terms, start trials, activate services, create tokens, add cards, upgrade or configure billing.
3. Under approved ADR-011, create exactly one empty project named `ZERO TRACE` in the existing `kayzenweb3dev` organization, solely to expose project-scoped account read-back. The owner has explicitly authorized this one action. If the official flow requires any new/changed terms, trial, UBA/other service activation, payment/billing, credentials/webhooks or build configuration, stop before that action. Do not create a second project. For a COPPA question, use the previously recorded owner answer (not primarily directed to children under 13) only if that record is available; otherwise stop for owner reconfirmation.
4. Read back project name and organization and confirm no service, credential, webhook, billing change or build was activated. Then inspect whether the Dashboard now exposes Personal/UBA entitlement, explicit Free plan, quotas/usage, Windows Micro and exact Editor 6000.3.29f1.
5. If any entitlement, license, cost or account condition remains unclear, publish BLOCKED evidence and stop. Do not guess.
6. UBA configuration is NOT AUTHORIZED by ADR-011. Do not activate UBA even if a Free plan appears; report findings for independent audit and separate owner authorization. The GitHub repository is confirmed public; official UBA docs state public repositories connect without a token. Use that path; do not create a PAT. If authentication/auto-build requires a token, stop for owner approval of exact least-privilege scope; never generate or expose credentials. No forks/untrusted branches.
5. Set one trusted main-only Windows x64 target, Windows Micro, exact Editor 6000.3.29f1, clean/no-cache, UBA EditMode+PlayMode tests, fail build on any test failure. Do not install UBA Editor package or alter Packages.
6. Enforce operational stops: <=100 Windows Micro minutes, <=12.5 GB storage, <=25 GB egress for this attempt. If a cap cannot be enforced/read back, do not run automatically. No overage or paid plan.
7. Run only the authorized build/tests. Capture exact commit, test reports, artifact hash and usage. Privately download one artifact, perform owner-operated Windows player and locale smoke. Inspect release guard and Pipeline artifacts/defines. No Steam or public distribution.
8. Verify exact GitHub status and ruleset. Do not add a required status until the actual context is proven. Preserve docs-validation and no-bypass protections.
9. Publish sanitized Evidence Bundle and proposed Checkpoint Delta. Commit/push only scoped evidence and authorized corrections on the same QA-01 branch/PR. Do not merge or promote checkpoint before independent audit.

Constraints: exactly one empty project bootstrap only; no UBA/other service activation, no new/changed terms, no trial, no billing/payment, no credential/token/webhook, no build configuration, no cloud build/test, no Unity Build Server, no local headless Personal Editor, no paid plan, no new subscription, no new package, no secret in code/logs/PR, no bypass, no gameplay, no D02/release. Preserve ADR-009 and ADR-010. Preserve ADR-009 and ADR-010.

Model routing: Sol 6.2 for licensing/cost/security ambiguity; Luna 6 for bounded configuration and deterministic execution once all gates pass. If the model or Dashboard suggests a paid path, stop.

Stop on stale lock, unknown entitlement, billing prompt, unavailable Editor/builder, usage cap issue, broad token, untrusted trigger, test/build failure, secret exposure or HIGH/CRITICAL finding. Report in pt-BR.
