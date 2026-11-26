# ZT-D01-WO-001-QA-01 | Evidence Bundle template (NOT RUN)

- Date/time and operator; issue #28; PR/branch; base main, planning snapshot, final tested SHA.
- Context Lock read-back; 122 project blobs; GDD and Mission Catalog SHA-256; package manifest/lock; ADR-009/010; effective ruleset.
- Unity ID/org entitlement read-back without personal identifiers; plan name; UBA entitlement; applicable terms/version/date reviewed by owner; confirm no trial, pay-as-you-go, payment prompt, card entry, upgrade or new account.
- Public pricing baseline and actual Dashboard quotas: Windows Micro minutes, Linux/Mac (unused), shared storage, egress; usage before/after; cap settings; operational stop limits and warnings; no overage.
- Exact UBA connection method; whether public GitHub repo connected without token; if token needed, type, repo scope and minimum permissions only (never record value); owner authorization; auto-build branch filter and webhook state.
- Builder OS/tier, Unity Editor version, target architecture, cache mode, exact Git SHA, target configuration and build ID.
- EditMode/PlayMode test names, passed/failed/skipped counts, XML/report locations, failure policy and logs sanitized. Package example tests reported separately from product coverage.
- Windows x64 artifact path/size/SHA-256; no public sharing; owner download; no signing credentials.
- Player smoke: startup scene, en-US default, EN↔PT-BR switch, startup errors, missing assets/scripts; owner/operator and evidence.
- Pipeline Runtime flags and scripting defines; artifact inspection for developer Pipeline server/Roslyn/runtime assemblies; any required removal/correction.
- Exact GitHub status context/commit SHA and CI link; ruleset 28038825 read-back, required check and no bypass; post-merge docs-validation.
- Cost/licensing/security risks, incident notes, stop events, rollback and cleanup/retention.
- AC01–AC10 matrix PASS/FAIL/BLOCKED/NOT RUN; no claims beyond evidence.
- Proposed Checkpoint Delta (not promoted) and verdict request. QA-01 cannot close D01 until independent APPROVED.
