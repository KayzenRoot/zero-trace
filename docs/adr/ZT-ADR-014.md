# ZT-ADR-014 | Starter apartment visual refinement for the local V01 prototype

Status: **PROPOSED — OWNER APPROVAL REQUIRED; NOT AUTHORIZED FOR EXECUTION**
Date: 2026-11-27
Applies to: ZT-V01-WO-002, issue #32, planning PR #32
Classification: **IMPORTANT / OPTIONAL**, outside the canonical D00–D19 dependency DAG

## Context

The approved source hierarchy places Checkpoint, Decisions, Scope, Definition of Done, Architecture and Requirements above supporting design material. The current canonical Checkpoint keeps D01 incomplete, QA-01 unqualified/not authorized for execution, and D02/D09 blocked. This proposal must not reorder or weaken those gates.

Visual Master v0.2.0 is the normative visual reconciliation. It explicitly specifies a modest starter apartment with one monitor and a recovery computer, says the premium hero apartment represents a later upgrade, and rejects premium hardware racks at the start. The historical v0.1.0 concept boards are non-normative.

ZT-V01-WO-001 is already integrated as a local/offline technical prototype. Its current LocalPrototypeRoomFactory procedurally creates an enclosed room with floor, four walls, ceiling, desk, keyboard, one training computer/monitor, shelf/storage case and one point light. It contains no clear entrance, window or living/sleeping cue. The current room meets the technical prototype's small-room requirement, but does not yet communicate the apartment composition described by the Visual Master.

The GDD v0.2.0 and mission catalog remain immutable. This ADR does not add a mission, mechanic, app, progression rule or release promise. The apartment layout details below are limited visual set dressing proposed for the already merged technical prototype.

## Proposed decision

Permit one optional, local/offline visual refinement under ZT-V01-WO-002:

1. Refine only the generated prototype room into a modest single-room studio-apartment composition.
2. Preserve the existing recovery computer, desk interaction, and exactly one visible monitor.
3. Add restrained residential cues: a simple entrance/door frame, a window with a static non-interactive exterior treatment, one compact living/sleeping prop, and modest storage. These objects are visual set dressing only.
4. Retain a modest workstation. Do not add a premium rack, additional monitor, high-end hardware, real brands, or luxury starting equipment.
5. Achieve visual quality through proportion, silhouette, material consistency, composition and restrained lighting, not expensive assets or feature expansion.
6. Use existing Unity primitives, existing project shaders/material capabilities and the existing package lock. No imported asset pack or new dependency.
7. Preserve the existing player, computer interaction, UI Toolkit desktop, synthetic exercise, localization, progress store and save format.

The door, window and furniture are not interactive. No outside traversal, sleeping, cooking, inventory, chores, NPC, weather or other gameplay is introduced. Rain is optional in the Visual Master and is excluded from this Work Order.

## Hard boundaries

- Applies only to ZT-V01-WO-002. It does not expand ZT-ADR-013, which remains limited to ZT-V01-WO-001.
- Classification remains IMPORTANT / OPTIONAL. This is not a NECESSARY release gate and is not added to the canonical D00–D19 dependency graph.
- D01 and QA-01 remain incomplete/unqualified; D02 and D09 remain blocked. This work does not replace, delay, satisfy or waive QA-01.
- No changes to the GDD, mission catalog, release scope, canonical mission status, D01 Checkpoint, or D00–D19 DAG.
- No changes to ZT-ADR-009, ZT-ADR-010, ZT-ADR-011, ZT-ADR-012 or ZT-ADR-013. Preserve PR #28 and its branch/HEAD.
- No Unity Cloud, UBA, DevOps activation, trial, new/changed terms, license activation/change, account/organization/project, credential, token, webhook, payment, paid asset, new package, package upgrade, external service, build, publication, deployment or distribution.
- No host filesystem/process/socket/network access, real targets, real attack behavior, personal data or external credentials.
- Use only one already licensed interactive Editor instance after a fresh execution preflight. If license, terms or service prompts appear, stop before interacting with them.

## Execution gate

This ADR is a proposal only. Execution requires all of the following:
1. Explicit owner approval of this ADR and the exact ZT-V01-WO-002 scope.
2. Independent audit and squash integration of the planning PR.
3. A fresh execution Context Lock on the then-current main, including all critical source and Unity project fingerprints.
4. A separate explicit owner instruction to execute ZT-V01-WO-002.
5. Successful local license/invocation preflight using the existing Editor and supported official local workflow.

Merging this planning package, if later approved, does not itself authorize implementation. No automatic transition from planning to execution is permitted.

## Consequences

If approved and later executed, the existing technical room will communicate the normative modest starter-apartment direction more clearly while retaining the one-computer prototype. The result remains technical prototype content, not a canonical mission, D01 completion, D02 simulation, D09 vertical slice, release build or production-ready apartment asset.

If the proposal is not approved, the current generic room remains valid for the already merged technical prototype; no code or Unity changes are needed.

## Alternatives

- Keep the current generic room: acceptable for the existing technical prototype, but leaves the starter-apartment visual direction unexpressed.
- Start with the premium hero apartment or a hardware rack: rejected because it contradicts Visual Master v0.2.0.
- Buy/import a marketplace apartment pack: rejected under the zero-cost and no-new-dependency constraints.
- Add apartment gameplay such as sleep, cooking, storage inventory or exterior exploration: rejected as scope expansion.

## Reversal

If later implemented, revert only the scoped room-factory and associated test changes through a normal reviewed revert. Do not delete user-local saves or perform destructive cleanup. No service, account, license, package or cloud resource is required for reversal.

## Approval record

No owner approval of ADR-014 adoption or ZT-V01-WO-002 execution is recorded. This document is a proposal prepared for review only.
