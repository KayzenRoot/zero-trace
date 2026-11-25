# ZT-D01-WO-001-PF-01/CD-01 | Evidence Bundle — execução 2026-11-25

**Resultado do executor: BLOCKED — Unity, licença e rota CI Unity não qualificados.** A recuperação CD-01 passou no checkout isolado. AC01 e AC02 passaram; AC03, AC04 e AC05 estão bloqueados pelas evidências locais ausentes; AC06 passou localmente e requer confirmação do CI no HEAD publicado; AC07 é uma entrega parcial; AC08 passou no escopo documental. A recomendação final é **BLOCKED**, mantendo PR #23 em draft e issue #22 aberta.

Data da captura: 2026-11-25, America/Sao_Paulo. Repositório: `KayzenRoot/zero-trace`. Base autorizada: `152d9c12ce65fb19c7daba8442e3c0e77f2822db`. Snapshot de planejamento: `34ec3b38411bbd96bd732d761907dc122846b6ae`. HEAD de entrada validado: `5b880033d15cfdf97fe3e96635e0a76787962c7f`. Branch: `docs/zt-d01-wo-001-pf-01`. PR #23 aberta, draft, sem merge; issue #22 aberta. O HEAD do commit que publica este bundle e seus checks são registrados no corpo atualizado da PR, evitando alegar antecipadamente resultados ainda não executados.

## AC01 — Context Lock e bytes exatos: PASS

- Clone isolado criado em diretório novo fora do checkout original, usando `git clone --no-checkout`; o checkout original não foi reutilizado, sobrescrito ou modificado.
- Materialização da branch feita com opções restritas à invocação: `git -c core.autocrlf=false -c core.eol=lf switch --track origin/docs/zt-d01-wo-001-pf-01`. Nenhuma configuração permanente foi escrita. `core.autocrlf` e `core.eol` locais não estão definidos no clone; `core.autocrlf=true` continua vindo da configuração de sistema existente e não foi alterado.
- Clone isolado antes das edições: branch e HEAD acima; `git status --porcelain=v1` vazio. Checkout original preservado em `aa25f417e4b1ddb03c67a90544deea8e3a59fa45`, também limpo; o blob do Context Lock nele e no HEAD validado é `7538a0c234d5ba8e8fca4acd84399d173bb9924c`.
- 25/25 `base_git_blobs` conferem com `origin/main`; 27/27 `plan_git_blobs` conferem com o snapshot; 27/27 hashes de blob calculados nos bytes do checkout com `git hash-object --no-filters` conferem com o lock. A tabela abaixo contém hash esperado e observado para cada caminho.
- A transcrição GDD da evidência histórica tinha erro. Valor correto, obtido do lock imutável e dos bytes do clone LF: `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1` (64 hex). O valor correto do catálogo é `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365` (64 hex). Os dois SHA-256 raw conferem 2/2. Esta nova evidência corrige a transcrição; a evidência histórica e os bytes canônicos não foram reescritos.
- `git check-attr` confirma `text` e `eol=lf`, sem `filter` ou `working-tree-encoding`, para as duas fontes. `git ls-files --eol` mostra `i/lf w/lf attr/text eol=lf` em ambas.
- Read-back GitHub confirmou o repositório público, PR #23 aberta/draft/base `main`/head `5b8800…`/sem merge e issue #22 aberta. Ruleset `ZT-GOV-main` ID `28038825`: enforcement `active`, escopo `refs/heads/main`, `bypass_actors=[]`. O endpoint efetivo da branch devolveu 5/5 regras do mesmo ID: `deletion`, `non_fast_forward`, `required_linear_history`, `pull_request` (squash-only, resolução de conversas obrigatória) e `required_status_checks` (`docs-validation`, estrito). A execução D00 `45531364841` no base exato `152d9c1…` concluiu `success`. No HEAD de entrada, `docs-validation` e os dois checks Socket estavam `success` (3/3).
- Desde o planejamento, as únicas alterações foram evidências autorizadas: o registro anterior `docs/evidence/ZT-D01-WO-001-PF-01.md` e `docs/evidence/ZT-D01-WO-001-PF-01/CD-01.md`. Desde o HEAD da evidência histórica `aa25f4…`, somente `CD-01.md` foi acrescentado. O Context Lock, fontes e decisões continuam intactos.

| Caminho do lock | Git blob esperado | Blob dos bytes raw | Resultado |
|---|---|---|---|
| `AGENTS.md` | `61146fc4e55d5c22aacd374e150767aa8b73964f` | `61146fc4e55d5c22aacd374e150767aa8b73964f` | PASS |
| `.gitattributes` | `7b8bf1bfbdf82fd8a33ff6bde32ce2aa37557035` | `7b8bf1bfbdf82fd8a33ff6bde32ce2aa37557035` | PASS |
| `.gitignore` | `fb53ed468046fe360f8d3eb8d767617aaba3995e` | `fb53ed468046fe360f8d3eb8d767617aaba3995e` | PASS |
| `.github/workflows/docs-validation.yml` | `45fff86aa7f4a93b97b0ac009b0b5ee3b51c87c7` | `45fff86aa7f4a93b97b0ac009b0b5ee3b51c87c7` | PASS |
| `.github/scripts/validate_docs.py` | `7cc501fbd5bc6962a4129e239bb050c1e7579b59` | `7cc501fbd5bc6962a4129e239bb050c1e7579b59` | PASS |
| `.github/governance/main-ruleset.proposed.json` | `1e04782b073e147e2107c9c087dbca012512b1b8` | `1e04782b073e147e2107c9c087dbca012512b1b8` | PASS |
| `docs/Checkpoint.md` | `a396e0f1376cde11d3ada5c1e43806fdeb97a19e` | `a396e0f1376cde11d3ada5c1e43806fdeb97a19e` | PASS |
| `docs/Decisions.md` | `e2c151609d2d2ad5f854b24e0edbd5fff8a1dfaa` | `e2c151609d2d2ad5f854b24e0edbd5fff8a1dfaa` | PASS |
| `docs/Scope.md` | `dc623557691b86dd077c3804c613436448d8b390` | `dc623557691b86dd077c3804c613436448d8b390` | PASS |
| `docs/DefinitionOfDone.md` | `3c75f595ee3c9e0164f78e1756f208a43d8d5388` | `3c75f595ee3c9e0164f78e1756f208a43d8d5388` | PASS |
| `docs/Architecture.md` | `9011df962baacb971c644c7764f795a77a7330ba` | `9011df962baacb971c644c7764f795a77a7330ba` | PASS |
| `docs/Requirements.md` | `d7d77cf0931201b4d6f26840a8a7a15a429dfca7` | `d7d77cf0931201b4d6f26840a8a7a15a429dfca7` | PASS |
| `docs/Preflight.md` | `254a7c05e3eab01adb3560ca76561a27917b60e3` | `254a7c05e3eab01adb3560ca76561a27917b60e3` | PASS |
| `docs/TestPlan.md` | `6b4ca99b77f62c859f5919d54dfdfeec9892215c` | `6b4ca99b77f62c859f5919d54dfdfeec9892215c` | PASS |
| `docs/Governance.md` | `e535131e2d3f9a06545392a92a602720d8fb5a35` | `e535131e2d3f9a06545392a92a602720d8fb5a35` | PASS |
| `docs/ContextLock.md` | `2141e3248853b955311b1fa2900bdc837fd09784` | `2141e3248853b955311b1fa2900bdc837fd09784` | PASS |
| `docs/ModelRouting.md` | `22609ed0a8450c31f41ad6ae46bbfc051b6437ee` | `22609ed0a8450c31f41ad6ae46bbfc051b6437ee` | PASS |
| `docs/Sources.md` | `e43f413ddf3236422c2f28ef6f1c920d16d4c63e` | `e43f413ddf3236422c2f28ef6f1c920d16d4c63e` | PASS |
| `docs/Security.md` | `87ae6725d0f5ddd990655928aaa90fc717150d96` | `87ae6725d0f5ddd990655928aaa90fc717150d96` | PASS |
| `docs/VisualSpec.md` | `edbde793457fc344d60da1bbf8fdcf8b34cfe7d8` | `edbde793457fc344d60da1bbf8fdcf8b34cfe7d8` | PASS |
| `docs/Backlog.md` | `77cc92285a88e2edf01ff3fde7ea17bb15d5c01e` | `77cc92285a88e2edf01ff3fde7ea17bb15d5c01e` | PASS |
| `docs/Deployment.md` | `8962c39c545251b9a6349102e2f9b4753d65a972` | `8962c39c545251b9a6349102e2f9b4753d65a972` | PASS |
| `design/ZERO_TRACE_GDD_MASTER_v0.2.0.md` | `0506c803c75c584e7f4bc6c5d975a6fe7690da08` | `0506c803c75c584e7f4bc6c5d975a6fe7690da08` | PASS |
| `design/ZERO_TRACE_Mission_Catalog_v0.2.0.json` | `f6f0d0b42d6d0b22ff608ba83d712e3cd9e8c9e4` | `f6f0d0b42d6d0b22ff608ba83d712e3cd9e8c9e4` | PASS |
| `docs/work-orders/ZT-D01-WO-001.md` | `23c9a6276512ae6136dee0e95af8b4c74659a4f3` | `23c9a6276512ae6136dee0e95af8b4c74659a4f3` | PASS |
| `docs/work-orders/D01/ZT-D01-WO-001-PF-01.md` | `a64077921f6ccd2187c37a2e09880c7e0db03fda` | `a64077921f6ccd2187c37a2e09880c7e0db03fda` | PASS |
| `docs/work-orders/D01/PF-01-evidence-template.md` | `03aea6ab780ed9066829fe62a6c17018919b2bc1` | `03aea6ab780ed9066829fe62a6c17018919b2bc1` | PASS |

| Fonte canônica | SHA-256 no lock | SHA-256 raw observado | Resultado |
|---|---|---|---|
| `design/ZERO_TRACE_GDD_MASTER_v0.2.0.md` | `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1` | `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1` | PASS |
| `design/ZERO_TRACE_Mission_Catalog_v0.2.0.json` | `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365` | `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365` | PASS |

## AC02 — Inventário local redigido: PASS

| Item | Valor observado | Fonte / limite |
|---|---|---|
| SO | Windows 11 Pro, versão 10.0.26300, build 26300, 64 bits | `Win32_OperatingSystem`; sem nome de usuário ou identificador de máquina |
| CPU | AMD Ryzen 3 4300GE with Radeon Graphics; 4 núcleos, 8 processadores lógicos | `Win32_Processor` |
| RAM | 15,1 GiB | memória física reportada por `Win32_ComputerSystem` |
| GPU | NVIDIA GeForce RTX 5050; também há Parsec Virtual Display Adapter | `Win32_VideoController`; VRAM **desconhecida**: valor WMI `4293918720` bytes está próximo do limite de 32 bits e não é confiável; não foi inferida capacidade |
| Espaço livre | C: 9,2 GiB; D: 544,1 GiB | volumes de arquivo; caminho temporário usa C:, sem publicar caminhos privados |
| Ferramentas | Git 2.55.0.windows.5; Python 3.13.15; GitHub CLI 2.102.0; Codex CLI 0.160.1 | comandos locais de versão |
| Unity Hub/Editor | não encontrados nas localizações padrão consultadas; nenhum processo ou entrada de desinstalação Unity Hub/Editor | verificação somente leitura de PATH, processos, caminhos padrão e chaves padrão de desinstalação; não prova ausência em qualquer diretório personalizado |

## AC03 — Editor, módulos e pacotes: BLOCKED

Nenhuma instalação válida de Hub/Editor, versão local ou módulo Windows x64 foi evidenciada nas fontes locais consultadas. Nenhum Editor foi aberto. Não se afirma que `Windows Build Support (Mono)` ou `Windows Build Support (IL2CPP)` esteja instalado. A página de lançamento da versão candidata oferece `Windows Build Support (Mono)`; a referência oficial do Hub identifica os módulos Windows `windows-mono` e `windows-il2cpp`. Isso é apenas informação para qualificação futura, não prova local.

A documentação oficial consultada em 2026-11-25 lista Unity 6.3 LTS com suporte até dezembro de 2027. `6000.3.29f1`, publicada em 2026-11-19, é mantida como **candidata documental não aprovada**. Se Windows desktop x64 vier a ser adotado, Mono é o backend padrão para plataformas Windows compatíveis e favorece iteração; IL2CPP continua candidato alternativo condicionado a módulo instalado e necessidade técnica. Nenhum backend foi selecionado ou testado nesta estação.

| Candidato | Compatibilidade oficial observada para Unity 6000.3 | Estado nesta execução |
|---|---|---|
| URP (`com.unity.render-pipelines.universal`) | pacote core documentado em Unity 6.3; core packages ficam fixados à versão correspondente do Editor. A documentação de pacote está na linha 17.3; a versão efetiva fica vinculada ao Editor exato. | Compatibilidade documental; versão efetiva, instalação e resolução exata pendentes |
| UI Toolkit / UIElements | documentação 6000.3 cobre UI de runtime; `com.unity.modules.uielements` aparece como módulo built-in fornecido com o Editor, sem pin independente de pacote registry | Compatibilidade documental; Editor e módulo local ausentes |
| Localization (`com.unity.localization`) | `1.5.14` consta como released para Editor 6000.3 | Candidato exato documentado; não instalado nem resolvido no projeto |
| Unity Test Framework (`com.unity.test-framework`) | pacote core documentado para 6000.3 e fixado à versão do Editor; documentação de pacote na linha 1.6 | Compatibilidade documental; versão efetiva vem com o Editor e não foi comprovada localmente |

Nenhum `Packages/manifest.json`, `packages-lock.json`, resolução de dependências ou teste Unity foi criado/executado. A fonte oficial explica que os pacotes core acompanham a versão do Editor; por isso URP e Test Framework não recebem aqui um patch independente inventado.

Fontes oficiais, consultadas em 2026-11-25: [suporte Unity 6](https://unity.com/releases/unity-6/support); [release 6000.3.29f1](https://unity.com/releases/editor/whats-new/6000.3.29f1); [módulos do Unity Hub](https://docs.unity.com/en-us/hub/hub-cli-reference); [scripting backends 6000.3](https://docs.unity.com/en-us/engine/6000.3/manual/programming-environment/scripting-backends/intro); [URP core 6000.3](https://docs.unity.com/en-us/engine/6000.3/manual/packages-list/packages-all/pack-core/com-unity-render-pipelines-universal); [Test Framework core 6000.3](https://docs.unity.com/en-us/engine/6000.3/manual/packages-list/packages-all/pack-core/com-unity-test-framework); [Localization 1.5.14 para 6000.3](https://docs.unity.com/en-us/engine/6000.3/manual/packages-list/packages-all/pack-safe/com-unity-localization); [pacotes built-in](https://docs.unity.com/en-us/engine/6000.3/manual/packages-list/packages-all/pack-build); [UI Toolkit runtime](https://docs.unity.com/en-us/engine/6000.3/manual/uitoolkits/uielements/uie-support-for-runtime-ui/uie-get-started-with-runtime-ui).

## AC04 — Licença e elegibilidade: BLOCKED

O estado de licença local ativa não pôde ser confirmado porque Hub/Editor não estão disponíveis nos locais consultados. Não foram lidos arquivos de licença, tokens ou registros financeiros e nenhuma ativação/desativação foi feita. A elegibilidade do proprietário permanece **desconhecida** e exige confirmação do proprietário conforme os termos atuais. Os termos definem o limiar Unity Personal em US$ 200.000 de Total Finances no período móvel de 12 meses, com definição específica para indivíduo, entidade e serviços a clientes. Não se presume elegibilidade a partir de repositório público ou plano sem evidência. Fonte oficial: [Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software), consultada em 2026-11-25.

## AC05 — CI Unity gratuita, legal e segura: BLOCKED

A API confirmou repositório público. GitHub informa que runners hospedados padrão são gratuitos para repositórios públicos; a validação de documentação já usa Actions. Isso não concede licença Unity. Os termos atuais exigem assinatura Unity Build Server paga correspondente ao tier do Editor para processos automatizados headless e proíbem Unity Personal com Build Server. Unity Build Automation tem plano gratuito limitado; ao exceder limites, o serviço bloqueia o projeto e a continuidade pode exigir upgrade. Não foi comprovada assinatura, cota ou rota Unity gratuita aplicável a esta conta; nenhum serviço foi habilitado ou configurado.

A rota não pode ser aprovada como zero custo até licença e elegibilidade serem confirmadas e o método de runner ser qualificado. Runners self-hosted não foram registrados. Para futuros PRs externos, `pull_request` oferece token `GITHUB_TOKEN` somente leitura e não disponibiliza Actions secrets; não executar código de PR não confiável com segredos por `pull_request_target`. Nenhum workflow ou segredo foi alterado.

Fontes oficiais, consultadas em 2026-11-25: [GitHub Actions — billing and usage](https://docs.github.com/en/actions/concepts/billing-and-usage); [Unity Build Server nos termos](https://unity.com/legal/editor-terms-of-service/software); [Unity DevOps free plan e limites](https://docs.unity.com/en-us/devops/pricing/free-plan); [isolamento de PR e `pull_request_target`](https://docs.github.com/en/actions/reference/security/securely-using-pull_request_target).

## AC06 — Validação documental e CI: PASS local / CI do novo HEAD registrado na PR

- `python .github/scripts/validate_docs.py` — exit 0; saída `DOCS_VALIDATION_OK: 420 original briefs; 356 sequential prerequisites; release matrix; 20-node canonical DAG; 30 apps; 3 static SVGs; proposed governance; lock checks`.
- `git diff --check origin/main...HEAD` — exit 0 no HEAD de entrada; será repetido depois deste arquivo.
- No HEAD de entrada `5b8800…`: `docs-validation`, `Socket Security: Project Report` e `Socket Security: Pull Request Alerts` estavam SUCCESS (3/3). A execução D00 `45531364841` em `152d9c1…` estava SUCCESS.
- O estado final de checks do commit que publica esta evidência será anotado no corpo da PR #23 com SHA exato e links após o push. Nenhum teste, build ou smoke Unity foi executado ou alegado; EditMode, PlayMode, build Windows e smoke de locale: NOT RUN.

## AC07 — ADR-001 proposta e Checkpoint Delta não promovido

### ADR-001 — PROPOSTA, não ratificada

**Contexto:** D01 mantém Unity 6.3 / `6000.3.29f1`, URP e UI Toolkit como candidatos. A documentação atual sustenta a compatibilidade para Unity 6000.3, mas a estação não tem instalação de Hub/Editor/módulos comprovada, licença e elegibilidade não foram confirmadas, e não existe rota CI Unity demonstrada como gratuita e licenciada.

**Alternativas:** (A) ratificar agora Editor, backend, pacotes e CI; (B) escolher outra versão suportada depois de qualificar a estação; (C) manter os candidatos documentais e adiar a ratificação até comprovar Editor/módulos, licença, package resolution e CI.

**Recomendação proposta:** C. Manter `6000.3.29f1` apenas como candidata. Mono pode ser avaliado como padrão para Windows desktop se o módulo estiver disponível. URP e Test Framework permanecem condicionados ao Editor exato; Localization 1.5.14 é apenas candidata compatível; UI Toolkit usa suporte integrado documentado. Não aprovar arquitetura, dependências, backend, CI ou licença neste PF-01.

**Restrições e riscos:** sem prova local de Editor/módulos/licença e elegibilidade; sem rota CI Unity zero custo demonstrada. A implementação e a resolução do manifest pertencem a etapa futura sob autorização própria. Não houve instalação nem criação de projeto.

**Verificação futura / reversão:** após confirmação do proprietário e autorização de etapa apropriada, comprovar instalação/licença elegível, componentes Windows x64, resolver e registrar versões/lock de pacote, e validar rota CI legal sem novas despesas. A proposta pode ser rejeitada sem efeito, pois nenhum artefato de projeto ou configuração foi criado.

### Checkpoint Delta — proposto, não promovido

Manter D01/PF-01 como **BLOCKED** por ausência de evidência local de Unity, módulos, licença/elegibilidade e rota CI Unity zero custo. Não editar `docs/Checkpoint.md`, não promover checkpoint, não criar projeto Unity, não iniciar IM-01 e manter issue #22 aberta/PR #23 draft. Exigir auditoria independente após o preenchimento dos gates.

## AC08 — Limites, privacidade e risco: PASS no escopo desta execução

O diff desta execução contém somente esta nova evidência sanitizada. Nenhuma fonte canônica, Context Lock, decisão, checkpoint, workflow, código ou configuração permanente foi alterada. Não foram instalados/atualizados ferramentas, ativadas licenças, configurados runners/credenciais, criadas despesas, executados builds/testes Unity, usados bypass ou feito merge. Nenhum segredo, nome de usuário, caminho privado ou dado financeiro foi publicado. Não foi executado scanner de segurança de código; portanto este PASS cobre os limites e a revisão documental do AC08, não uma certificação geral de segurança. Nenhum HIGH/CRITICAL foi identificado no escopo documental alterado.

## Arquivos, comandos e recomendação

- Arquivo desta execução: `docs/evidence/ZT-D01-WO-001-PF-01/CD-01-execution-2026-11-25.md`. O relatório histórico `docs/evidence/ZT-D01-WO-001-PF-01.md`, `docs/locks/ZT-D01-WO-001-PF-01.json`, GDD e catálogo foram preservados.
- Comandos de recovery/preflight: `git clone --no-checkout`; `git fetch origin`; `git -c core.autocrlf=false -c core.eol=lf switch --track origin/docs/zt-d01-wo-001-pf-01`; `git status --porcelain=v1`; `git hash-object --no-filters -- <path>` para os 27 caminhos; `Get-FileHash -Algorithm SHA256` para GDD e catálogo; `git check-attr text eol filter working-tree-encoding -- <fontes>`; `git ls-files --eol -- <fontes>`; `python .github/scripts/validate_docs.py`; `git diff --check origin/main...HEAD`. Nenhuma configuração Git foi persistida.
- Limitação decisiva: AC03–AC05 bloqueados; sem Unity, licença ou CI Unity qualificados. A aprovação de AC01 não altera esses bloqueios.
- **Recomendação do executor: BLOCKED.** Publicar esta evidência parcial na branch e PR existentes, ler os checks no HEAD exato publicado e parar. Não fazer merge, promoção de checkpoint ou avanço para IM-01.