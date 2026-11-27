# ZT-V01-WO-002 — planning Evidence Bundle

- **Status:** PLANNING ONLY — IMPORTANT / OPTIONAL; NOT AUTHORIZED FOR EXECUTION
- **Date:** 2026-11-27
- **Repository:** KayzenRoot/zero-trace
- **Issue:** #32
- **Planning branch:** docs/zt-v01-wo-002-apartment-planning
- **Base main:** 8b7a76ca4155a164f788fc9f417af54ab9a373d6
- **Predecessor:** ZT-V01-WO-001 / PR #30, merged at 0bba2b6ccde6ed263a39b16d444b6094e5292483
- **Planning PR:** #32 (draft)
- **Context Lock:** docs/locks/ZT-V01-WO-002-planning.json

## Source Check

- Fresh main read-back: 8b7a76ca4155a164f788fc9f417af54ab9a373d6.
- PR #30 is closed/merged; PR #31 is closed/merged. PR #28 remains open/draft at protected HEAD dd1ab1e2d2359e9a0d4a90cb68ca4ef1552beee1; its base is the earlier 1b72c6773fc9edd39c44239ec127a75d7ca9d158. No change to PR #28 is included.
- Live ruleset 28038825 (ZT-GOV-main) is active on refs/heads/main, squash-only, with no bypass actors. Required status context is docs-validation.
- On current main SHA 8b7a76ca4155a164f788fc9f417af54ab9a373d6, docs-validation check-run 140480725879 is SUCCESS and Socket Security: Project Report check-run 140480739188 is SUCCESS. No Socket Security: Pull Request Alerts check-run was observed on this merge SHA; this bundle does not claim 3/3 checks on main.
- docs/Sources.md identifies GDD SHA-256 4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1 and mission catalog SHA-256 836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365. The main docs-validation run passed; its validator recomputes both hashes against the original bytes.
- Planning Context Lock verification: all 29 critical Git blob fingerprints were re-read from main and matched 29/29; the GDD and mission-catalog Git blob IDs also match. ADR-011 and ADR-012 blob IDs were re-read on PR #28's protected branch and match the recorded values.
- Canonical Checkpoint remains D01 incomplete, QA-01 unqualified/planning-only and not authorized for execution, D02/D09 blocked. The formal D01 gate is not changed by this optional visual proposal.

## Source findings

- Visual Master v0.2.0 is normative for this visual direction: modest starter apartment, one monitor, recovery computer; the premium hero apartment is an upgrade reference; no premium rack at start.
- Current LocalPrototypeRoomFactory blob on main: 7dffdcfbb3ae671c6d8f65eef23bc201e5bb9392. It creates a generic enclosed room (floor, four walls, ceiling), desk/keyboard, one training computer and monitor, shelf/storage case, one point light, and a first-person player. It has no explicit entrance, window or residential/living cue.
- Current LocalFirstPlayableController blob: 99e885abc9b5f2c42605cb58d7011ae6ebe231d7. The existing desktop/apps/computer lifecycle are outside the proposed visual change.
- Current LocalPrototypePlayerMotor blob: d9ef215744823900b93dc2c4455867a2229d06cd. Safe bounds, recovery and camera movement are existing contracts.
- Current PlayMode test file blob: 51c406f71c5af318c1292ec8ea1f1c5bb9287e05. It covers prototype entry, computer lifecycle, synthetic apps/exercise, locale, wall boundaries and safe recovery. These tests must remain and be extended only if execution is separately authorized.
- Current SampleScene blob: c8b6b44a277d33ea563c95dc9386daebaea85bc4. This proposal plans no authored scene change.
- ZT-V01-WO-001 already meets its technical prototype scope; this optional WO is a visual refinement, not a correction to the merged Work Order and not a release requirement.

## Classification

**IMPORTANT / OPTIONAL, not NECESSARY.** The existing technical prototype can function without this visual refinement. The proposal is justified only to express the normative starter-apartment direction more clearly. It is intentionally excluded from the canonical D00–D19 dependency DAG and does not change the D01 QA-01 gate or any release milestone.

## Planning-only work performed

- Created issue #32 to track the proposal.
- Prepared proposed ZT-ADR-014 and complete ZT-V01-WO-002.
- Prepared a planning-only Context Lock with exact base SHA, source fingerprints, source manifest hashes, protected PR #28 state and stale-lock rules.
- Added an optional IMPORTANT section to docs/Backlog.md without adding a D00–D19 DAG node.
- Added a PROPOSED entry to docs/Decisions.md.
- No Unity project file, code, scene, package manifest, package lock, GDD, mission catalog, VisualSpec or Checkpoint was modified.
- No Unity Editor, Unity Cloud, UBA, DevOps, trial, new terms, credential, payment, build, publication or deployment was used.

## Proposed Checkpoint Delta

**No canonical Checkpoint change. NOT PROMOTED.** If this optional visual Work Order is later executed and independently approved, the only candidate delta is a note that the local technical prototype's generated room was visually refined. D01 remains incomplete; QA-01 remains unqualified/not authorized; D02 and D09 remain blocked. No canonical mission, release, build or production-asset claim is made.

## Remaining gates

This bundle records planning only. The planning PR must pass documentation validation and exact-head CI and receive an independent audit. Even after that PR is merged, execution requires explicit owner approval of ADR-014, a fresh execution Context Lock on then-current main, a licensed local preflight and a separate owner instruction.
