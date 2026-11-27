# ZT-V01-WO-002 — Evidence Bundle de execução

**Estado:** PREFLIGHT LOCAL CONCLUÍDO; implementação e validação visual ainda pendentes.
**Data:** 2026-11-27
**Repositório:** KayzenRoot/zero-trace
**Issue relacionada:** #32 (item de planejamento já integrado)
**Branch de execução:** `feat/zt-v01-wo-002-starter-apartment`
**Base `main`:** `79c9f0f55a520736b47ce7cba64d8c70bd591682`
**Context Lock de execução:** [ZT-V01-WO-002.json](../locks/ZT-V01-WO-002.json)

Este Evidence Bundle será atualizado no mesmo branch ao concluir as mudanças autorizadas. Nenhuma aprovação final ou aprovação independente desta execução é declarada neste estágio.

## Source Check e governança

- `git fetch origin main` confirmou `origin/main` em `79c9f0f55a520736b47ce7cba64d8c70bd591682`; a branch foi criada diretamente desse commit.
- PR #32 está integrada por squash, HEAD `5ba041ac643314a0bf6825ced6459572a3df6ffb`. A comparação da base do planejamento `8b7a76ca4155a164f788fc9f417af54ab9a373d6` com a `main` atual mostra exatamente os seis arquivos documentais da PR #32. O Checkpoint não mudou: blob `6e65d473058f1b8854289af4cd05e34ee6f15edf`.
- A auditoria de planejamento publicada na issue #32 registrou **APPROVED PARA REVISÃO DO PROPRIETÁRIO**, sem autorização de merge ou execução. Depois desse registro, o proprietário aprovou e integrou a PR #32 e agora autorizou diretamente este Work Order. A revisão automática Copilot não foi concluída por limite de quota; a auditoria independente da PR de execução continua pendente.
- PR #28 permanece aberta e em draft na branch `wo/zt-d01-wo-001-qa-01-exec`, HEAD protegido `dd1ab1e2d2359e9a0d4a90cb68ca4ef1552beee1`, base original `1b72c6773fc9edd39c44239ec127a75d7ca9d158`. Seus checks no HEAD protegido: `docs-validation`, `Socket Security: Project Report` e `Socket Security: Pull Request Alerts` SUCCESS (3/3). Nenhuma alteração foi feita nela.
- PR #32 passou nos checks exatos do seu HEAD `5ba041ac643314a0bf6825ced6459572a3df6ffb`: `docs-validation` 140514745422, `Socket Security: Project Report` 140514750548 e `Socket Security: Pull Request Alerts` 140514771959 — SUCCESS (3/3).
- Na `main` atual, `docs-validation` 140515077010 e `Socket Security: Project Report` 140515125904 são SUCCESS. `Socket Security: Pull Request Alerts` não foi observado no commit de merge da `main`; não se declara 3/3 nessa SHA.
- Ruleset `ZT-GOV-main`: ID 28038825, enforcement active, alvo `refs/heads/main`, zero bypass actors, bypass do usuário atual `never`; protege contra deleção e non-fast-forward, exige histórico linear, PR com threads resolvidas, merge squash e status obrigatório `docs-validation`.
- Fingerprints atuais: 33/33 blobs Git críticos e 141/141 arquivos Unity rastreados conferem com a base. O lock de planejamento anterior continua histórico e foi revalidado 29/29 contra seu snapshot próprio `8b7a76ca4155a164f788fc9f417af54ab9a373d6`; não é usado como lock de execução.
- SHA-256 das fontes canônicas conferem com `docs/Sources.md`: GDD `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1`; Mission Catalog `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365`.
- ADR-011 e ADR-012 em PR #28 conferem com os blobs travados no planejamento. ZT-ADR-013 e ADR-009/010 permanecem intactos.
- Um arquivo local preexistente não rastreado, `ProjectSettings/SceneTemplateSettings.json`, foi preservado fora do commit; SHA-256 `bb9098b3bfcde78d93e264b96f7b77b5430e64e8c57a7aad7f5a5d9c3945e16a`.

## Preflight local

- Unity CLI oficial existente: 1.0.1. Nenhuma atualização ou instalação foi executada.
- Um único Editor conectado: Unity 6000.3.29f1, estado `ready`, Play Mode `stopped`, sem compilação pendente, `autoRefresh=enabled` e `runInBackground=true`.
- A licença Personal foi previamente confirmada pelo proprietário; nesta execução o Editor existente abriu e respondeu sem prompt/erro de licença. Não houve reativação, login, leitura de credenciais ou inspeção de logs brutos. A consulta de licença da CLI foi evitada porque, se o cliente local não estiver presente, a CLI pode baixá-lo automaticamente.
- Nenhum prompt de trial, termos, cloud ou pagamento apareceu durante esta execução. Não houve acesso ao Unity Cloud/UBA/DevOps.
- Dependências atuais preservadas: URP 17.3.0, Localization 1.5.14, Pipeline 1.0.1; `Packages/manifest.json`, `Packages/packages-lock.json` e `ProjectSettings/ProjectVersion.txt` permanecem na base. A composição prevista usa primitivas e capacidades já instaladas, sem pacote ou asset externo.
- Inventário de testes existente pelo Editor: EditMode 29; PlayMode 8. Baseline antes da implementação: EditMode **29/29 PASS**, PlayMode **8/8 PASS**, zero falhas/ignorados.
- Console antes dos testes: `compilationFailed=false`, 1 erro retido. Depois dos testes: 1 erro, 0 warnings, sem falha de compilação. O único evento novo desde o cursor pré-operação correspondeu ao teste negativo existente `[StartupController] Startup interface is unavailable.`; os testes que o emitem passaram. A verificação de status confirmou o Editor `ready` novamente após a troca normal de domínio do PlayMode.
- Custo adicional: US$ 0. Sem build, cloud, deployment, importação de assets, alteração de dependências ou mudança de licença.
- A instância usada para o baseline ficou fora do Play Mode ao final.

## Critérios de aceitação — leitura do estágio atual

| Critério | Estado atual | Evidência |
| --- | --- | --- |
| AC01 — Source Check / Context Lock | PASS | Base exata, 33/33 blobs críticos, 141/141 Unity, 2/2 hashes SHA-256 e governança registrada. |
| AC02 — autorização | PASS | PR #32 integrada; ADR-014 e Work Order na base; instrução direta do proprietário para executar esta WO. |
| AC03–AC09 — visual, composição, equipamento, preservação e geometria | PENDENTE | Implementação e inspeção PlayMode ainda não iniciadas. |
| AC10 — integridade de dependências | PASS de baseline | Manifest, lock e versão do Editor preservados na base; revalidar no HEAD final. |
| AC11 — testes | Baseline PASS | EditMode 29/29 e PlayMode 8/8 antes da mudança; repetir após o candidato final e adicionar regressões do apartamento. |
| AC12 — execução local | PASS de baseline | Editor existente pronto; testes licenciados concluídos; revalidar após as alterações. |
| AC13 — capturas visuais | PENDENTE | Nenhuma captura pós-implementação ainda. |
| AC14 — documentação e segurança | PENDENTE | Executar validações e buscas escopadas no candidato final. |
| AC15 — custo e operação | PASS de baseline | US$ 0; sem cloud, build ou deploy até aqui. |
| AC16 — PR #28 | PASS | Aberta/draft no HEAD exato protegido; checks 3/3 SUCCESS; sem mutação. |
| AC17 — Evidence Bundle / Checkpoint | EM ANDAMENTO | Lock e evidência inicial preparados; Checkpoint Delta abaixo não promovido. |

## Diff, implementação e capturas

Ainda não houve mudança de código, cena, pacote ou asset. A atualização fica limitada ao Context Lock de execução e a esta evidência inicial; as evidências de implementação e capturas reais serão adicionadas após a validação do candidato.

## Checkpoint Delta — proposta NÃO PROMOVIDA

Propor somente o registro de um refinamento visual local/offline da sala técnica para apartamento inicial modesto, caso os critérios e auditoria independente sejam aprovados. `docs/Checkpoint.md` permanece inalterado; D01 segue incompleto, QA-01 não qualificado, D02 e D09 bloqueados. O refinamento não representa missão canônica, vertical slice, release ou conclusão de qualquer gate do DAG.

## Limitações e riscos atuais

- A execução ainda não foi implementada nem recebeu auditoria independente.
- O estado da licença corrente é sustentado pela confirmação anterior do proprietário e pelo funcionamento do Editor já aberto, sem reativação ou consulta que possa instalar o cliente de licenciamento. Não há alegação de verificação da conta Unity por API.
- A entrada Error de Console é deliberada no teste negativo aprovado; será distinguida de erros de compilação e de novas falhas no candidato final.
- A confirmação visual, interação por raio, limites, restauração de câmera e screenshots ainda dependem da execução das regressões específicas do WO-002.
