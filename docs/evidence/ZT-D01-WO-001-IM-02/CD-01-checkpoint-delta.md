# ZT-D01-WO-001-IM-02/CD-01 — Checkpoint Delta proposto

Estado: APPROVED_PENDING_INTEGRATION — delta aceito e registrado em `docs/Checkpoint.md`; efetivo em `main` após squash merge da PR #27 e checks pós-merge.
Data: 2026-11-26
Issue #26 | PR #27 draft/open | branch wo/zt-d01-wo-001-im-02

## Base e candidato

- Base main do Context Lock: 048922542f2752970dc812a300b300e4ea54dd7e.
- Planning HEAD: 9a82ce06fd7a22c4f3618d1c8ef9857561ca29d2.
- Snapshot de fingerprints: 1639b87c4090a344e775e8de8bc6871024371318.
- Implementação e testes locais: 9ec7ca5f35753d5d077b1370df3b272ef340109e.
- O HEAD final do bundle e os checks correspondentes devem ser lidos da PR #27 após a publicação; este texto não promove nem atualiza o Checkpoint canônico.

## Delta que poderá ser considerado após auditoria

1. Registrar que a correção F01–F03 do slice de inicialização foi publicada na PR #27, com ciclo seguro do StartupController, rótulo textual de idioma selecionado e regressões EditMode/PlayMode.
2. Registrar os resultados locais de compilação, localização e layout e a reimportação sequencial limpa descritos em CD-01-execution.md.
3. Manter IM-02 pendente até revisão independente, aprovação dos checks exatos, merge autorizado e atualização correspondente da fonte canônica.
4. Manter D01 incompleto. ADR-009 continua não qualificando CI Unity em cloud; QA-01 segue obrigatório para essa qualificação. ADR-010 permanece aprovada apenas para Pipeline local, com Runtime desativado.
5. Não admitir IM-03, QA-01, D02, gameplay, build/release ou promoção de checkpoint como parte deste delta.

## Condição de promoção

A auditoria independente aprovou IM-02 no HEAD `29e25209876cd25cf87f43c9fc5ea79b88d88ea0`; o audit e promotion lock foram adicionados à mesma PR. O checkpoint canônico agora contém o delta como aprovado pendente de integração. Nenhum status de merge é antecipado.
