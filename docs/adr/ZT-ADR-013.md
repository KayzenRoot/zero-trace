# ZT-ADR-013 | Local-first technical playable prototype

Status: **OWNER APPROVED FOR ZT-V01-WO-001 ONLY**
Date: 2026-11-26
Scope: local, offline, non-release prototype on an isolated branch and PR.

## Context

Canonical `main` at `1b72c6773fc9edd39c44239ec127a75d7ca9d158` records D01 as incomplete, QA-01 as planning/execution blocked pending account-specific Unity DevOps entitlement, terms, enforceable cost limits, tests and a trusted GitHub status. D02 and D09 remain blocked. PR #28 is open and draft; its proposed ADR-012 says a local-only route requires a separate decision and is not authorized by that QA-01 proposal.

The owner instruction supplied on 2026-11-26 explicitly authorizes a separate local Work Order, Context Lock, branch and PR, the smallest governance decision to permit local prototyping, use of the already installed Unity Personal Editor, implementation, local persistence, tests and an independent PR. The same instruction forbids new cost, cloud activation, trial, changed terms, credentials, release, deployment, merge and changes to PR #28.

The canonical GDD lists CAM-001 “First Boot” and CAM-002 “Lost Directory” as `BRIEF`, with missing design fields and D09 release gates. Therefore this slice is a technical prototype and is not a completed canonical mission or release content.

## Decision

Authorize ZT-V01-WO-001 to create one local, offline, clearly labeled technical playable prototype using the existing Unity 6000.3.29f1 installation and the already resolved project packages. This authorization covers implementation, local Editor Play Mode, EditMode/PlayMode tests and local-only persistence for synthetic game data.

The prototype may present a small first-person room, keyboard/mouse movement, a fictitious computer with OFF/BOOTING/READY/SHUTDOWN states, NULL OS-style UI Toolkit desktop, fictional Mail/File Manager/Academy surfaces, and one bounded synthetic-file exercise with retry, feedback and local progress save/load. This is a demonstrator only; it does not instantiate production apps, the canonical campaign, or the broader D02 simulation core.

## Hard boundaries

- No Unity Cloud, Build Automation, DevOps, cloud project/service, external build, remote target or network access.
- No trial, payment, subscription, card, new/changed terms, license activation/change, account, credential, token, webhook or secret.
- Use the existing Personal license and one interactive Editor instance only. Stop the affected Editor operation if a license or terms prompt appears.
- No new package or dependency, no package upgrade, and no change to ADR-009, ADR-010, ADR-011 or ADR-012.
- The runtime exercise uses invented filenames and in-memory rules. It must not read or write arbitrary host files, launch processes, open sockets, contact real hosts, execute commands or run real attacks.
- Save only prototype progress under the Unity application’s own persistent-data location. Do not include account or personal data.
- No multiplayer, economy, Steam, external services, commercial release, distribution, build publication, deployment or visibility change.
- No checkpoint promotion, D01/D02/D09 completion claim, D02 admission, merge or change to PR #28.

## Consequences

This decision permits only the isolated V01 branch/PR while the existing D01/D02/D09 gates remain authoritative and unchanged. The proposal is effective for this execution by the owner instruction; canonical adoption is pending normal PR review and merge. It does not waive independent review or exact-head checks.

The prototype can be reverted by reverting its scoped files/commit. No service, license, package or account change is needed for rollback. Local progress files, if any, remain within the application-owned persistent-data location and are not part of the repository.

## Alternatives

1. Wait for QA-01 cloud qualification before any local implementation: not selected because the owner explicitly authorized an independent local path and cloud qualification is unrelated to running a licensed local Editor.
2. Activate or trial Unity Cloud/UBA: rejected because account entitlement, terms and enforceable cost controls remain unverified and the owner prohibited activation.
3. Treat CAM-001/CAM-002 as finished release missions: rejected because the source catalog marks them `BRIEF` and keeps their release gate at D09.

## Approval record

Owner authorization was supplied directly with the ZT-V01-WO-001 execution prompt on 2026-11-26. This record makes no claim of independent audit, D01 completion, cloud qualification, canonical mission approval or release approval.