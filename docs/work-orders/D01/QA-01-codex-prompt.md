# Codex one-shot | ZT-D01-WO-001-QA-01

**Owner authorized QA-01 on 2026-11-26.** Begin with fresh Source Check and read-only preflight only. Do not activate services, accept terms, create credentials/webhooks, change billing, run cloud builds or implement code until all specific gates pass and owner-operated consent is completed.

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
2. Perform read-only owner-operated Unity Dashboard preflight. Do not create accounts/orgs, accept terms, start trials, activate services, create tokens, add cards, upgrade or configure billing. Owner operates login/terms. Read back Personal eligibility, UBA entitlement, explicit Free plan, exact quotas/usage, Windows Micro and Editor 6000.3.29f1 availability.
3. If any entitlement, license, cost or account condition is unclear, publish BLOCKED evidence and stop. Do not guess.
4. Only after a separate explicit owner execution instruction, and only after every preflight gate is proven, configure UBA Free. The GitHub repository is confirmed public; official UBA docs state public repositories connect without a token. Use that path; do not create a PAT. If authentication/auto-build requires a token, stop for owner approval of exact least-privilege scope; never generate or expose credentials. No forks/untrusted branches.
5. Set one trusted main-only Windows x64 target, Windows Micro, exact Editor 6000.3.29f1, clean/no-cache, UBA EditMode+PlayMode tests, fail build on any test failure. Do not install UBA Editor package or alter Packages.
6. Enforce operational stops: <=100 Windows Micro minutes, <=12.5 GB storage, <=25 GB egress for this attempt. If a cap cannot be enforced/read back, do not run automatically. No overage or paid plan.
7. Run only the authorized build/tests. Capture exact commit, test reports, artifact hash and usage. Privately download one artifact, perform owner-operated Windows player and locale smoke. Inspect release guard and Pipeline artifacts/defines. No Steam or public distribution.
8. Verify exact GitHub status and ruleset. Do not add a required status until the actual context is proven. Preserve docs-validation and no-bypass protections.
9. Publish sanitized Evidence Bundle and proposed Checkpoint Delta. Commit/push only scoped evidence and authorized corrections on the same QA-01 branch/PR. Do not merge or promote checkpoint before independent audit.

Constraints: no Unity Build Server, no local headless Personal Editor, no paid plan, no new subscription, no payment method, no new package, no secret in code/logs/PR, no bypass, no gameplay, no D02/release. Preserve ADR-009 and ADR-010.

Model routing: Sol 6.2 for licensing/cost/security ambiguity; Luna 6 for bounded configuration and deterministic execution once all gates pass. If the model or Dashboard suggests a paid path, stop.

Stop on stale lock, unknown entitlement, billing prompt, unavailable Editor/builder, usage cap issue, broad token, untrusted trigger, test/build failure, secret exposure or HIGH/CRITICAL finding. Report in pt-BR.
