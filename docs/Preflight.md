# ZT-M00-WO-001 | Pre-production preflight
Date: 2026-11-24 America/Sao_Paulo. This is documentation preflight, not qualification of a game build.

| Gate | Evidence | Status |
|---|---|---|
| P00 repository | KayzenRoot/zero-trace; main baseline 932c5b83b7b160c38138e2c1ed506102a0ffe178; PR #1, no implementation | PASS |
| P01 source | original GDD and mission JSON SHA-256 and Git blob read-back verified; 420 unique authored briefs | PASS |
| P02 art provenance | ten original concept PNG hashes recomputed, all match manifest; owner direction conditional approval | PASS for provenance; final design verification open |
| P03 engine | Unity 6.3 LTS supported through Dec 2027; 6000.3.29f1 published Nov 19 2026 is candidate, not pin | CANDIDATE |
| P04 licensing | Unity Personal eligibility threshold total finances <= US$200,000; actual owner eligibility, licenses, package terms not verified | OPEN before Unity installation/build |
| P05 workstation | Windows editor, installed version, modules, disk, CPU/GPU/RAM, git and Codex access not measured in owner's environment | D01 PREFLIGHT REQUIRED |
| P06 Steam | official store/build review, Coming Soon minimum 2 weeks, AI content survey and rights rules reviewed | DOCUMENTED; actual partner account, fees, AppID, release permissions remain conditional |
| P07 governance | desired ruleset versioned, connector cannot apply/read effective settings | BLOCKED |
| P08 budget | no new expenses authorized/created; actual online budget, region, hosting and scale unqualified | EXTERNAL GATE before online rollout |
| P09 IP | title, logo, font and asset trademark/licensing clearance unverified; no open-source license selected | OPEN before production asset use or public distribution |
| P10 education | reviewer and competency validation unqualified | GATE before public educational content |

D01 executor must inventory actual Unity Hub/Editor and supported Windows build module, editor license/eligibility evidence (no financial details in public repo), exact package lock, clean Git worktree and storage; do not install/update, pay, obtain a license or create services without the applicable authorization. Candidate can change via ADR, no automatic upgrade. Headless tests, EditMode, PlayMode and Windows artifact evidence are D01 deliverables, not D00 claims. CI Unity licensing/runner mode requires separate proof; do not put credentials in public workflows. Keep P07 blocked until real read-back.

Sources: https://unity.com/releases/unity-6/support ; https://unity.com/releases/editor/whats-new/6000.3.29f1 ; https://unity.com/pages/license-compliance ; https://partner.steamgames.com/doc/store/releasing ; https://partner.steamgames.com/doc/gettingstarted/contentsurvey .
