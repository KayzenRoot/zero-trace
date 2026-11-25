# Dependency DAG (GDD §22)

D00 design/governance approved; effective on main after PR #1 integration. D01+ blocked until dependencies approved, merged and Context Lock refreshed; no implementation authorization implied. These are parent WOs; split large nodes into independently auditable child WOs before execution. D19 not required for D18.

| WO | Modules | Title | Prerequisites | Model | Risk | Issue |
|---|---|---|---|---|---|---|
| ZT-M00-WO-001 | M00 | Source Pack and visual reconciliation | none | Sol 6.2 | ELEVATED | [#2](https://github.com/KayzenRoot/zero-trace/issues/2) |
| ZT-D01-WO-001 | M01/M32/M03a | Unity foundation | D00 | Luna 6 | STANDARD | [#3](https://github.com/KayzenRoot/zero-trace/issues/3) |
| ZT-D02-WO-001 | M02 | Simulation core | D01 | Sol 6.2 | ELEVATED | [#4](https://github.com/KayzenRoot/zero-trace/issues/4) |
| ZT-D03-WO-001 | M04/M05/M06 | NULL OS desktop, files, terminal | D02 | Luna 6 | ELEVATED | [#5](https://github.com/KayzenRoot/zero-trace/issues/5) |
| ZT-D04-WO-001 | M07/M08 | Virtual network and sandbox prototype | D02,D03 | Sol 6.2 | HIGH_ASSURANCE | [#6](https://github.com/KayzenRoot/zero-trace/issues/6) |
| ZT-D05-WO-001 | M09/M11 | Security and forensic foundations | D04 | Sol 6.2 | ELEVATED | [#7](https://github.com/KayzenRoot/zero-trace/issues/7) |
| ZT-D06-WO-001 | M17a | Mission orchestration without payout | D02,D03 | Luna 6 | STANDARD | [#8](https://github.com/KayzenRoot/zero-trace/issues/8) |
| ZT-D07-WO-001 | M21a/M22a | Offline economy and ledger | D02 | Sol 6.2 | HIGH_ASSURANCE | [#9](https://github.com/KayzenRoot/zero-trace/issues/9) |
| ZT-D08-WO-001 | M17b/M19a/M13a | Mission reward, NPC and hardware slices | D06,D07,D05 | Sol 6.2 | HIGH_ASSURANCE | [#10](https://github.com/KayzenRoot/zero-trace/issues/10) |
| ZT-D09-WO-001 | M14a/M15a/M18a | V0.1 vertical slice | D03,D04,D05,D08 | Luna 6 | ELEVATED | [#11](https://github.com/KayzenRoot/zero-trace/issues/11) |
| ZT-D10-WO-001 | M10a/M23a/M14b/M18b | V0.2 public demo | D09 | Luna 6 | ELEVATED | [#12](https://github.com/KayzenRoot/zero-trace/issues/12) |
| ZT-D11-WO-001 | M08–22/M25 | Professional career expansion | D10 | Sol 6.2 | ELEVATED | [#13](https://github.com/KayzenRoot/zero-trace/issues/13) |
| ZT-D12-WO-001 | M30a | Steam identity adapter | D01 | Sol 6.2 | HIGH_ASSURANCE | [#14](https://github.com/KayzenRoot/zero-trace/issues/14) |
| ZT-D13-WO-001 | M26 | Multiplayer session and authorization | D02,D12 | Sol 6.2 | HIGH_ASSURANCE | [#15](https://github.com/KayzenRoot/zero-trace/issues/15) |
| ZT-D14-WO-001 | M27 | Persistent online state and restore | D11,D13 | Sol 6.2 | HIGH_ASSURANCE | [#16](https://github.com/KayzenRoot/zero-trace/issues/16) |
| ZT-D15-WO-001 | M23b/M24a/M28/M29/M30b | Online market, coop and arena pilot | D14 | Sol 6.2 | HIGH_ASSURANCE | [#17](https://github.com/KayzenRoot/zero-trace/issues/17) |
| ZT-D16-WO-001 | M34/M35 | V0.5 Early Access qualification | D15 | Sol 6.2 | HIGH_ASSURANCE | [#18](https://github.com/KayzenRoot/zero-trace/issues/18) |
| ZT-D17-WO-001 | M23c/M24/M25/M28/M29/M31/M33 | Full content and endgame | D16 | Luna 6 | ELEVATED | [#19](https://github.com/KayzenRoot/zero-trace/issues/19) |
| ZT-D18-WO-001 | M34/M35 | V1.0 final qualification | D17 | Sol 6.2 | HIGH_ASSURANCE | [#20](https://github.com/KayzenRoot/zero-trace/issues/20) |
| ZT-D19-WO-001 | M30c | Conditional Steam Community Market | D15 + external gate | Sol 6.2 | HIGH_ASSURANCE | [#21](https://github.com/KayzenRoot/zero-trace/issues/21) |
