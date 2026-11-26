# QA-01 Source Check and read-only preflight | 2026-11-26

Status: **SOURCE CHECK PASS / DASHBOARD PREFLIGHT BLOCKED PENDING OWNER-OPERATED READ-BACK**  
WO: `ZT-D01-WO-001-QA-01` · Issue #28 · Branch `wo/zt-d01-wo-001-qa-01-exec`  
Base main: `1b72c6773fc9edd39c44239ec127a75d7ca9d158`

## Owner authorization

Owner explicitly authorized QA-01 on 2026-11-26. This authorizes the Work Order and Source Check/read-only preflight. It does not authorize account/org creation, service activation, terms acceptance, credential/webhook creation, billing/payment, cloud build, or implementation before the specific gates pass.

## GitHub Source Check

| Check | Result |
|---|---|
| Current main | `1b72c6773fc9edd39c44239ec127a75d7ca9d158` |
| Previous Context Lock base | `590a91b28a71e657e0e8879d11f4ea1685bcdb99`, stale after planning PR #29 |
| Refreshed Context Lock | base and planning snapshot now reference current main |
| Unity project blobs | 122/122 unchanged |
| Critical source fingerprints | 24/24 unchanged |
| GDD v0.2.0 and Mission Catalog blobs | unchanged from the previously SHA-256-verified planning snapshot |
| Repository visibility | public |
| Default branch | main |
| Main protection | enabled |
| Ruleset 28038825 | active; zero bypass; linear history; squash only; `docs-validation` required |
| Planning PR #29 | merged at current main SHA |
| PR #29 post-merge checks | `docs-validation` SUCCESS; Socket Security Project Report SUCCESS |
| Project file changes since baseline | zero |

## Project/toolchain baseline

- Unity Editor: `6000.3.29f1`, revision `96d2fe17529c`.
- Platform: Windows x64 Mono (canonical project baseline).
- Direct packages: URP `17.3.0`, Localization `1.5.14`, Unity CLI/Pipeline `1.0.1`.
- Test Framework: built-in `1.6.0`.
- `Packages/manifest.json` and `Packages/packages-lock.json` unchanged.
- Build settings contain exactly one enabled startup scene: `Assets/Scenes/SampleScene.unity`.
- `ProjectSettings/ProjectSettings.asset`: `scriptingDefineSymbols: {}`; no scripting defines recorded.
- Addressables `m_BuildRemoteCatalog: 0`, `m_CCDEnabled: 0`; remote catalog is not enabled.
- Local Pipeline Runtime flags are not fully represented in tracked source. Actual local Editor/Package settings require owner/operator read-back; do not infer their state from the URP asset's `m_Enabled` field.

## Official Unity public documentation reviewed

1. Pricing update effective 2026-03-01 advertises per-organization monthly free baseline: 200 Windows Micro minutes, 100 Linux Micro minutes, 100 Mac Standard minutes, 25 GB shared DevOps storage, 100 GB egress. Exceeding limits can lock access; if no payment method is added, access remains blocked and data may be at risk. This is a public baseline, not proof of this account's plan or remaining usage.
2. Unity DevOps Free Plan: exceeding any service limit can lock the DevOps project; without upgrade it remains read-only and cloud storage may be deleted after 30 days; warning emails at 50%, 75%, and 90%.
3. UBA source-control docs: public Git repositories are automatically connected without a token. This repository is confirmed public; do not create a PAT or webhook for read access. Auto-build permissions are not required for a manual build.
4. UBA unit-test docs: EditMode and PlayMode supported; option to mark build failed if any test fails; test reports available through build history.
5. UBA supported versions docs: 6000.3 LTS listed Supported; official UBA versions are typically added within five business days of release. Unity 6000.3.29f1 was released 2026-11-19. Exact patch availability must be read back from the actual Dashboard build-version selector.
6. UBA build configuration docs: Unity version, branch, platform, builder OS/version, caching and auto-build are explicit configuration options. Auto-build remains disabled.
7. Current Unity Editor Software Terms last updated 2026-11-16 prohibit Unity Build Server use with Unity Personal and constrain Editor use across machines. UBA managed cloud is a separate candidate service; actual UBA entitlement and applicable terms must be verified. Do not use on-prem Build Server or local headless Personal Editor.

Official URLs:
- https://unity.com/pt/products/pricing-updates
- https://docs.unity.com/en-us/devops/pricing/free-plan
- https://docs.unity.com/en-us/build-automation/get-started-with-build-automation/connect-your-version-control-system
- https://docs.unity.com/en-us/build-automation/reference/unit-tests
- https://docs.unity.com/en-us/build-automation/reference/supported-unity-versions
- https://docs.unity.com/en-us/build-automation/basic-build-configuration/overview
- https://unity.com/legal/editor-terms-of-service/software

## Unverified account and local gates

| Gate | State | Required evidence |
|---|---|---|
| Existing Unity ID/org | UNVERIFIED | Owner-operated Dashboard overview; no new org/account |
| Explicit DevOps Free plan | UNVERIFIED | Dashboard plan/billing read-back; no trial/paygo/upgrade |
| UBA entitlement under current Unity Personal | UNVERIFIED | Existing account's UBA service entitlement and applicable terms |
| Windows Micro quota/current usage | UNVERIFIED | Dashboard usage and cap read-back |
| Shared storage/egress usage | UNVERIFIED | Dashboard usage read-back |
| Payment method/billing state | UNVERIFIED | Confirm no card/payment method and no consumption billing |
| Windows Micro builder | UNVERIFIED | Dashboard target configuration options |
| Exact Editor 6000.3.29f1 in UBA | UNVERIFIED | Dashboard version selector |
| Unity Personal license active locally | NOT OBSERVABLE HERE | Owner/operator checks existing Hub/Editor; no activation flow |
| One Editor instance / local Runtime flags | NOT OBSERVABLE HERE | Owner/operator read-back; Runtime off and one Editor only |
| Native UBA GitHub status/check | UNVERIFIED | No native commit-status integration documented in public pages reviewed |
| Least-privilege status bridge if needed | NOT AUTHORIZED | Requires exact service-account permission review and separate explicit owner approval |

## Cost/security controls

- Operational stop limits remain 100 Windows Micro minutes, 12.5 GB shared storage and 25 GB egress.
- No service or cloud project created/activated.
- No token, webhook, service account, GitHub Actions secret or API key created.
- No payment method, billing plan, trial or upgrade touched.
- No cloud build, artifact, code implementation or package change.
- No auto-build, fork/PR trigger, public artifact sharing or Steam upload.

## Verdict

**Source Check: PASS. Dashboard/account preflight: BLOCKED pending owner-operated read-back.** This is a controlled stop, not a product defect. No UBA activation/build is permitted until the actual plan, entitlement, billing state, quota, builder and exact Editor version are proven. If any gate requires payment, upgrade, trial conversion, Build Server, unclear license, or an unapproved credential, stop and keep QA-01/D01 BLOCKED.

D01 remains incomplete. D02 and release remain blocked.
