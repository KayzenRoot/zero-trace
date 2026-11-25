# ZT-D01-WO-001-IM-01/CD-02 — qualificação parcial do Unity Pipeline

**Resultado: BLOCKED.** A integração local do Pipeline não iniciou o servidor no Editor. G-PIP-01 não tem prova explícita suficiente de preço/entitlement Personal do pacote, G-PIP-02 falhou no pós-estado, e por isso F02–F04 e os gates licenciados restantes não foram executados. Esta evidência registra o bloqueio; não aprova adoção técnica nem checkpoint.

## Identificação e preservação

- Repositório `KayzenRoot/zero-trace`; issue #24; PR #25 aberta em draft; branch `wo/zt-d01-wo-001-im-01`.
- Base `main`: `a907ae071aad6cec7f2b1bca0b111d7272527601`.
- HEAD autorizado e auditado antes desta publicação: `46cc6b6f21ebd58a5bb3f34ab3c88699a9370df4`. O HEAD exato do commit desta evidência foi registrado no corpo da PR após o push.
- Context Lock revisado r2: blob Git `37a8deb40d9d73a86a3d4eb6b4532d199aa4c88a`; ADR-009 e Checkpoint não foram alterados. O Context Lock original também permanece intacto.
- A única alteração versionada deste incremento é este Evidence Bundle. A tentativa oficial de instalação alterou temporariamente apenas `Packages/manifest.json`; ela foi revertida antes do commit. Nenhum pacote Pipeline foi resolvido, nenhum `packages-lock.json` foi editado e não há cache `Library/PackageCache/com.unity.pipeline@1.0.1`.

## Source Check e governança

Antes da tentativa Unity, `git fetch --prune origin` confirmou a base, a branch remota e o HEAD autorizado acima; o issue #24 estava aberto e a PR #25 permanecia aberta em draft. O checkout estava limpo. O Source Check determinístico validou:

- fingerprints do Context Lock: **28/28** blobs da base, **30/30** blobs do snapshot histórico e **87/87** fingerprints brutos do lock revisado;
- hashes SHA-256 das duas fontes canônicas: **2/2** iguais aos originais;
- regraset `ZT-GOV-main`, ID `28038825`: ativo, aplicado a `main`, sem bypass; `docs-validation` obrigatório e estrito;
- Context Lock original, ADR-009 e `docs/Checkpoint.md`: preservados.

No HEAD autorizado `46cc6b6`, os checks remotos anteriores à publicação estavam **3/3 SUCCESS**: [docs-validation, run 45697698497](https://github.com/KayzenRoot/zero-trace/actions/runs/45697698497/job/139499311114), [Socket Security: Project Report](https://socket.dev/dashboard/org/nexlabs/sbom/337538ef-ed10-433b-a7dd-8fc4ad7d03a1) e [Socket Security: Pull Request Alerts](https://socket.dev). Eles não são checks do novo HEAD; o estado exato após o push consta na PR.

## Inventário autorizado e licença

- Unity CLI existente `1.0.1`, executável assinado pela Unity Technologies SF; SHA-256 `C06C2E86941162E8D46957D39A6089162AD52A00023433881B5E9A18EA220D17`. Invocação pelo caminho existente, sem alteração permanente do PATH.
- Unity Hub existente `4.0.2.65535`.
- Editor existente em `D:\Unity-CD02\editor-6000.3.29f1\6000.3.29f1\Editor\Unity.exe`: `6000.3.29f1`, revisão `96d2fe17529c`, Authenticode válido; SHA-256 `4971E2009FE7A13FC5749478D7B230573FAFF8DFDA7E8A97D782DC2429FC905A`. Suporte Windows x64/Mono já estava instalado.
- A leitura estruturada da CLI reportou sessão autenticada/fresca e licença `Unity Personal` do tipo `Assigned`. Identificadores pessoais retornados pelo comando foram omitidos desta evidência.
- Antes de abrir o projeto havia zero instâncias do Editor. Depois da abertura, permaneceu uma única instância visível, PID `22788`, respondendo e apontando para o checkout autorizado. Ela continua aberta; não foi encerrada à força nem se afirma fechamento normal.
- Espaço livre inventariado antes da abertura: aproximadamente 10,3 GB em C: e 552,5 GB em D:.

## G-PIP-01 — custo, licença e compatibilidade do candidato

A Unity documenta o CLI como gratuito e separado do Unity AI; também documenta que o Pipeline controla um Editor 6.0+ local e funciona offline, por interface local autenticada ([CLI e licença](https://docs.unity.com/en-us/unity-cli/replace-mcp-server-unity-cli), [instalação/uso do Pipeline](https://docs.unity.com/en-us/unity-cli/unity-pipeline/unity-pipeline-package), [segurança](https://docs.unity.com/en-us/unity-cli/unity-cli-security)). O registro UPM oficial ofereceu `com.unity.pipeline` `1.0.1`, cujo metadado declara compatibilidade `6000.0`; a licença incluída no artefato referencia os Unity Terms of Service. O pacote declara dependências `com.unity.test-framework 1.1.33`, `com.unity.nuget.mono-cecil 1.11.6`, `com.unity.modules.uielements 1.0.0`, `com.unity.modules.jsonserialize 1.0.0`, `com.unity.modules.screencapture 1.0.0` e `com.unity.nuget.newtonsoft-json 3.0.2`. O SHA-1 do arquivo do registro conferiu com o metadado oficial: `c0af79690db1fdbc7076658c338a320f40a3b41d`.

Isso confirma origem, identidade e compatibilidade declarada com a família do Editor instalado. **Não foi encontrada declaração oficial explícita de preço/entitlement Personal para o pacote em si**; a documentação consultada confirma a gratuidade do CLI, não formula uma garantia de custo zero específica para esse pacote. A licença Unity Personal está atribuída, mas não substitui a prova que o gate condicional exige. Nenhum serviço cloud, Unity AI, runner ou cobrança foi iniciado ou observado.

O projeto atual tem 36 dependências diretas e 54 entradas no lock, sem dependência direta ausente; SHA-256 do manifesto `8be240714b36b1a362441ed54a4be4daf081d3591f2b5e311d3514936c75b1b5` e do lock `f06ed742e478675f65ff74895068fdc72579ea76624101b9eb5142ac7383fcab`. As versões já resolvidas do Test Framework (`1.6.0`) e Newtonsoft (`3.2.2`) diferem das solicitadas no metadado do candidato (`1.1.33` e `3.0.2`); sem resolução real não é possível concluir se há downgrade, conflito ou resolução compatível.

**Estado G-PIP-01: BLOCKED para instalação/adoção.** A prova de origem e compatibilidade declarada é positiva; falta confirmação oficial inequívoca de entitlement/preço zero sob Personal. Nenhuma instalação resolvida foi retida.

**Desvio de sequência registrado:** apesar da prova incompleta, a CLI de instalação foi invocada uma vez. Ela gravou temporariamente a referência no manifesto, mas o Package Manager não resolveu o pacote nem criou cache/lock. A referência foi revertida; não houve cobrança ou serviço iniciado. Essa tentativa não satisfaz o gate pré-instalação e não deve ser repetida antes da confirmação oficial.

## G-PIP-02 — tentativa controlada e resultado

Com a única instância autorizada e o projeto existente, foram usados somente comandos documentados/descobertos da CLI:

1. `unity open "D:\Projects\Zero Trace\zero-trace-im01-20261125" --editor-path "D:\Unity-CD02\editor-6000.3.29f1\6000.3.29f1\Editor\Unity.exe" --until-ready --timeout 300` abriu o projeto e manteve uma janela do Editor. O comando encerrou com exit `10`: o servidor Pipeline não iniciou dentro de 300 segundos. O carregamento do projeto no Editor não equivale a qualificação da interface.
2. `unity pipeline install --version 1.0.1 --project-path "D:\Projects\Zero Trace\zero-trace-im01-20261125"` retornou exit `0`, mas seu único efeito observado foi adicionar a dependência ao manifesto. O lock não foi regenerado e o cache do pacote não apareceu.
3. `unity pipeline editors --json` indicou a instância do projeto, porém `pipelineServer.isReachable=false` e `apiUrl=null`; `unity status --project-path ... --json` retornou `STATUS_PIPELINE_LOAD_PENDING` quando a entrada estava no manifesto. Não foi estabelecido endpoint, porta ou autenticação Pipeline alcançável.
4. Como não houve resolução pelo Package Manager e o servidor não iniciou, removi somente a entrada temporária do manifesto, restaurando os blobs originais autorizados. A leitura final confirmou `hasPipelinePackage=false`, zero servidores alcançáveis, manifesto sem Pipeline, lock original intacto e cache ausente. A árvore estava limpa após essa restauração.

Não foi configurado plugin Unity/MCP, arquivo persistente do Codex, runtime Pipeline, define `ENABLE_RUNTIME_PIPELINE`, firewall, túnel ou serviço externo. A busca estática nos arquivos versionados não encontrou `ENABLE_RUNTIME_PIPELINE`; as duas opções efetivas de runtime não puderam ser inspecionadas no Editor.

## G-PIP-03 — F01–F04 e validação técnica

- **F01 — instância licenciada:** parcialmente comprovado; Personal `Assigned`, Editor correto aberto e uma única janela/processo observado. Não houve compilação/teste serial nem fechamento normal, pois o servidor não ficou pronto e a instância permanece aberta.
- **F02 — referência Input Settings:** pendente. O GUID `289c1b55c9541489481df5cc06664110` aparece em `ProjectSettings/EditorBuildSettings.asset`; a resolução pelo AssetDatabase não pôde ser executada. Nenhuma referência foi removida às cegas.
- **F03 — identificador, Analytics e alvo:** pendente e não alterado. Os arquivos versionados ainda declaram `companyName: KayzenRoot`, `productName: Zero Trace`, identificador `com.DefaultCompany.3D-Project`, `submitAnalytics: 1` e `activeInputHandler: 0`; `scriptingBackend` não tem override visível. O alvo/backend efetivo deve ser confirmado em Player Settings. Não alterei configurações sem validação oficial.
- **F04 — dependências builtin:** pendente. Sem resolução ativa do Package Manager, não foi possível justificar remoções ou demonstrar necessidade efetiva. Nenhuma dependência preexistente foi removida.
- **Compilação e cinco testes EditMode `StableIdentifier`: NOT RUN como gates.** A importação inicial ocorreu ao abrir o Editor, mas não houve comando/documentação de status que permita declarar compilação aprovada, contagem descoberta ou resultado de teste.
- **Reimportação limpa sequencial: NOT RUN.** Nenhum segundo checkout/Editor foi iniciado.
- **Runtime:** nenhum pacote instalado; flag textual ausente no conteúdo versionado. Valores efetivos de configuração permanecem não verificados.

### Higiene e limite de segurança

Durante o diagnóstico, uma saída de leitura de `Editor.log` revelou fragmentos de linhas de token de licença. Os valores não são reproduzidos aqui, gravados no repositório ou copiados para o Evidence Bundle. A leitura de log foi interrompida; nenhum token do Pipeline, arquivo de credencial ou conteúdo de `Library` foi extraído. Esta ocorrência limita a avaliação de segurança desta execução; antes de retomar a sessão Unity, o proprietário deve avaliar a renovação da sessão pela interface oficial. O retorno de status também continha identificadores de conta, omitidos de toda evidência versionada.

## G-PIP-04 — AC01–AC08

| Critério | Estado | Evidência / limite |
| --- | --- | --- |
| AC01 — fontes, Context Lock e governança | PASS | 28/28 blobs da base, 30/30 do snapshot histórico, 87/87 fingerprints brutos r2, 2/2 SHA-256; regraset 28038825 ativo sem bypass; fontes preservadas. |
| AC02 — licença e invocação oficial | PARCIAL | Unity Personal `Assigned`; CLI 1.0.1 abriu o Editor 6000.3.29f1. Integração Pipeline não iniciou e G-PIP-01 não comprovou preço/entitlement específico do pacote. |
| AC03 — template e configurações F02/F03 | BLOCKED | GUID precisa de AssetDatabase; Analytics está ligado; identificador segue default e alvo/backend efetivo não foi comprovado. |
| AC04 — manifest/lock e pacotes | BLOCKED | A entrada Pipeline foi removida; o grafo existente é estaticamente consistente, mas não houve resolução real do candidato nem avaliação das versões divergentes. |
| AC05 — compilação e cinco testes | BLOCKED / NOT RUN | Nenhuma compilação explícita nem teste EditMode foi executado/contado por interface qualificada. |
| AC06 — reimportação limpa e CI | BLOCKED / NOT RUN | Reimportação sequencial não executada. Validação documental local e checks do HEAD anterior à publicação são registrados separadamente; Unity CI em nuvem continua diferido para QA-01. |
| AC07 — Evidence Bundle, ADR-010 e Checkpoint Delta | PARCIAL | Este Evidence Bundle publicado; ADR-010 segue proposta condicional, não adotada. Delta de checkpoint abaixo, não promovido. |
| AC08 — escopo, custo e segurança | BLOCKED | Sem cobranças, cloud, gameplay, bypass, merge, promoção ou IM-02. Porém a prova de custo/entitlement do pacote está incompleta e houve exposição transitória de fragmentos de token na saída diagnóstica; nenhum valor foi versionado. |

### Proposta de ADR-010 — não adotada

Manter ZT-ADR-010 no estado `OWNER AUTHORIZED CONDITIONAL QUALIFICATION / PROPOSED TECHNICAL ADOPTION`. Não atualizar `docs/Decisions.md` nem declarar adoção até que Unity confirme o custo zero/entitlement Personal do `com.unity.pipeline`, o Package Manager gere lock real compatível, a conexão autenticada somente em localhost seja alcançável, o runtime permaneça desativado e todos os gates locais licenciados passem. ADR-009 permanece preservada: CI Unity em nuvem segue `DEFERRED_NOT_QUALIFIED_QA-01`.

### Checkpoint Delta proposto — não promovido

- IM-01/CD-02 permanece **BLOCKED**; não está pronto para auditoria de conclusão nem para IM-02.
- Nenhum estado canônico ou checkpoint foi promovido. Preservar projeto e lock de pacotes existentes.
- Continuidade requer confirmação oficial de preço/entitlement; proprietário opera eventual UI oficial para resolver o Package Manager, uma instância por vez; depois executar F02–F04, compilação, 5/5 testes e reimportação sequencial com hashes idênticos, validação de segurança e checks exatos do novo HEAD.
- Nenhuma implantação, gameplay, serviço cloud, despesa, merge ou avanço de Work Order realizado.

## Validação e conclusão

Validações a executar no commit desta evidência: `.github/scripts/validate_docs.py`, `git diff --check`, conferência dos hashes de manifesto/lock e inspeção de escopo/segredos. Nenhum resultado futuro é antecipado neste arquivo; o corpo da PR registrará o HEAD remoto e seus checks concluídos.

**Conclusão do executor: BLOCKED.** Requer prova oficial de custo/entitlement do pacote e operação oficial do proprietário para a resolução do Package Manager; após isso, este mesmo CD-02 pode continuar sem reinstalação de ferramentas, sem nova PR e sem tocar no checkpoint.
