# ZT-D01-WO-001-IM-01/CD-02 — qualificação parcial do Unity Pipeline

**Estado histórico desta evidência antes da retomada de 25/11/2026: BLOCKED.** As observações e conclusões anteriores foram preservadas. A seção final “Retomada CD-02 — 25/11/2026” registra a renovação informada pelo proprietário, a resolução real do pacote, os ajustes feitos e os bloqueios atuais; ela substitui os estados anteriores apenas para os fatos que revalidou.

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

A FAQ oficial de preços da Unity declara CLI, MCP e plugin oficial gratuitos, sem limite de concorrência; distingue-os do assistente integrado, cujo trial Personal pode converter-se em US$ 10/mês. O MCP do CLI é construído sobre o Unity Pipeline. A documentação do pacote exige Editor 6.0+ e CLI e descreve a instalação local pelo Package Manager/CLI, com o projeto aberto ([FAQ de preços](https://unity.com/features/ai), [MCP do CLI](https://docs.unity.com/en-us/unity-cli/replace-mcp-server-unity-cli), [pacote Pipeline](https://docs.unity.com/en-us/unity-cli/unity-pipeline/unity-pipeline-package)). Os termos §2.7 subordinam o uso do Package Manager aos direitos do Editor e a eventuais termos do pacote; §2.5 exclui Unity Build Server com Personal, produto que não será usado nesta rota local de Editor gráfico ([Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software)). Combinadas com o metadado oficial UPM e a licença `Unity Terms of Service` do artefato, essas fontes sustentam custo zero para a rota local CLI/MCP/Pipeline — sem estender isso a outros produtos Unity. O metadado do candidato `com.unity.pipeline` `1.0.1` declara compatibilidade `6000.0` e dependências `com.unity.test-framework 1.1.33`, `com.unity.nuget.mono-cecil 1.11.6`, `com.unity.modules.uielements 1.0.0`, `com.unity.modules.jsonserialize 1.0.0`, `com.unity.modules.screencapture 1.0.0` e `com.unity.nuget.newtonsoft-json 3.0.2`; o SHA-1 do arquivo conferiu com o metadado do registro: `c0af79690db1fdbc7076658c338a320f40a3b41d`.

O candidato é oficial e compatível com a família declarada do Editor instalado; não há resolução de dependências nesta etapa. Nenhum serviço cloud, Unity AI, runner, trial ou cobrança foi iniciado ou observado. A página oficial diferencia explicitamente o trial pago do assistente integrado das ferramentas gratuitas de CLI/MCP/plugin; não abrir o assistente nem iniciar trial.

O projeto atual tem 36 dependências diretas e 54 entradas no lock, sem dependência direta ausente; SHA-256 do manifesto `8be240714b36b1a362441ed54a4be4daf081d3591f2b5e311d3514936c75b1b5` e do lock `f06ed742e478675f65ff74895068fdc72579ea76624101b9eb5142ac7383fcab`. As versões já resolvidas do Test Framework (`1.6.0`) e Newtonsoft (`3.2.2`) diferem das solicitadas no metadado do candidato (`1.1.33` e `3.0.2`); sem resolução real não é possível concluir se há downgrade, conflito ou resolução compatível.

**Estado G-PIP-01: PASS documental, condicionado à UI oficial.** A auditoria mais recente considerou as fontes combinadas suficientes para uma única tentativa local depois da contenção da sessão e da confirmação visível de ausência de licença separada, trial, compra ou cobrança. O candidato continua sem instalação resolvida.

**Desvio de sequência registrado:** na execução anterior, a CLI de instalação foi invocada uma vez antes de o gate documental ser fechado. Ela gravou temporariamente a referência no manifesto, mas o Package Manager não resolveu o pacote nem criou cache/lock. A referência foi revertida; não houve cobrança ou serviço iniciado. Não repetir essa tentativa pela CLI; seguir uma única instalação pelo Package Manager oficial após as precondições.

## G-PIP-02 — tentativa controlada e resultado

Com a única instância autorizada e o projeto existente, foram usados somente comandos documentados/descobertos da CLI:

1. `unity open "D:\Projects\Zero Trace\zero-trace-im01-20261125" --editor-path "D:\Unity-CD02\editor-6000.3.29f1\6000.3.29f1\Editor\Unity.exe" --until-ready --timeout 300` abriu o projeto e manteve uma janela do Editor. O comando encerrou com exit `10`: o servidor Pipeline não iniciou dentro de 300 segundos. O carregamento do projeto no Editor não equivale a qualificação da interface.
2. `unity pipeline install --version 1.0.1 --project-path "D:\Projects\Zero Trace\zero-trace-im01-20261125"` retornou exit `0`, mas seu único efeito observado foi adicionar a dependência ao manifesto. O lock não foi regenerado e o cache do pacote não apareceu.
3. `unity pipeline editors --json` indicou a instância do projeto, porém `pipelineServer.isReachable=false` e `apiUrl=null`; `unity status --project-path ... --json` retornou `STATUS_PIPELINE_LOAD_PENDING` quando a entrada estava no manifesto. Não foi estabelecido endpoint, porta ou autenticação Pipeline alcançável.
4. Como não houve resolução pelo Package Manager e o servidor não iniciou, removi somente a entrada temporária do manifesto, restaurando os blobs originais autorizados. A leitura final confirmou `hasPipelinePackage=false`, zero servidores alcançáveis, manifesto sem Pipeline, lock original intacto e cache ausente. A árvore estava limpa após essa restauração.

Não foi configurado plugin Unity/MCP, arquivo persistente do Codex, runtime Pipeline, define `ENABLE_RUNTIME_PIPELINE`, firewall, túnel ou serviço externo. A busca estática nos arquivos versionados não encontrou `ENABLE_RUNTIME_PIPELINE`; as duas opções efetivas de runtime não puderam ser inspecionadas no Editor.

## Contenção local do incidente (25/11/2026)

- **F01 — instância licenciada:** parcialmente comprovado; Personal `Assigned` foi observado antes da saída do Hub. Agora há um Editor principal visível e dois processos filhos dele; a licença efetiva após a saída precisa de revalidação. Não houve compilação/teste serial nem fechamento normal.
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
| AC02 — licença e invocação oficial | PARCIAL | Personal `Assigned` e abertura pelo CLI foram observados antes da saída do Hub. Agora a sessão local está encerrada; revalidar licença depois do login do proprietário. Integração Pipeline ainda não iniciou. |
| AC03 — template e configurações F02/F03 | BLOCKED | GUID precisa de AssetDatabase; Analytics está ligado; identificador segue default e alvo/backend efetivo não foi comprovado. |
| AC04 — manifest/lock e pacotes | BLOCKED | A entrada Pipeline foi removida; o grafo existente é estaticamente consistente, mas não houve resolução real do candidato nem avaliação das versões divergentes. |
| AC05 — compilação e cinco testes | BLOCKED / NOT RUN | Nenhuma compilação explícita nem teste EditMode foi executado/contado por interface qualificada. |
| AC06 — reimportação limpa e CI | BLOCKED / NOT RUN | Reimportação sequencial não executada. Validação documental local e checks do HEAD anterior à publicação são registrados separadamente; Unity CI em nuvem continua diferido para QA-01. |
| AC07 — Evidence Bundle, ADR-010 e Checkpoint Delta | PARCIAL | Este Evidence Bundle publicado; ADR-010 segue proposta condicional, não adotada. Delta de checkpoint abaixo, não promovido. |
| AC08 — escopo, custo e segurança | BLOCKED | Sem cobrança, cloud, gameplay, bypass, merge, promoção ou IM-02. G-PIP-01 tem prova documental condicional; falta confirmação visual sem oferta separada. A sessão do Hub foi encerrada localmente, mas revogação externa não foi verificada. Fragmentos e IDs não foram repetidos nem versionados. |

### Proposta de ADR-010 — não adotada

Manter ZT-ADR-010 no estado `OWNER AUTHORIZED CONDITIONAL QUALIFICATION / PROPOSED TECHNICAL ADOPTION`. Não atualizar `docs/Decisions.md` nem declarar adoção até o proprietário revalidar Personal na UI, o Package Manager gerar lock real compatível, a conexão autenticada somente em localhost ser alcançável, o runtime permanecer desativado e todos os gates locais licenciados passarem. ADR-009 permanece preservada: CI Unity em nuvem segue `DEFERRED_NOT_QUALIFIED_QA-01`.

### Checkpoint Delta proposto — não promovido

- IM-01/CD-02 permanece **BLOCKED**; não está pronto para auditoria de conclusão nem para IM-02.
- Nenhum estado canônico ou checkpoint foi promovido. Preservar projeto e lock de pacotes existentes.
- Continuidade requer confirmação oficial de preço/entitlement; proprietário opera eventual UI oficial para resolver o Package Manager, uma instância por vez; depois executar F02–F04, compilação, 5/5 testes e reimportação sequencial com hashes idênticos, validação de segurança e checks exatos do novo HEAD.
- Nenhuma implantação, gameplay, serviço cloud, despesa, merge ou avanço de Work Order realizado.

## Validação e conclusão

Validações a executar no commit desta evidência: `.github/scripts/validate_docs.py`, `git diff --check`, conferência dos hashes de manifesto/lock e inspeção de escopo/segredos. Nenhum resultado futuro é antecipado neste arquivo; o corpo da PR registrará o HEAD remoto e seus checks concluídos.

## Delta de estado após o retorno ao Hub — 25/11/2026

- O proprietário confirmou por mensagem que concluiu o login e que o Hub voltou sem trial ou cobrança. Isso é relato do proprietário; não prova ativação nem elegibilidade.
- Na UI oficial observada nesta retomada, Configurações > Licenças ainda exibia “Atualizando licenças…” e o controle Atualizar estava desabilitado. O Editor inicialmente exibiu “License returned”; após o login do proprietário, a janela normal do projeto reapareceu e os menus ficaram acessíveis. Dispensar o aviso ou abrir a janela normal não comprova licença ativa. A consulta estruturada anterior da CLI havia retornado o tipo Personal como `Assigned`, mas o Hub ainda pendente e a ausência de um status legível impedem tratar isso como licença operacional.
- A inspeção somente de leitura de Help > Licenses não produziu um estado verificável: a automação não conseguiu selecionar o item do menu, e nenhuma tela de licença foi confirmada. Não houve aceitação de termos nem alteração do projeto; a tentativa não será repetida nesta retomada.
- O inventário da UI mostrou uma única janela do Editor existente apontando para o projeto autorizado. Nenhuma instalação ou resolução de pacote foi iniciada nesta retomada. O Package Manager não pôde ser qualificado com o Editor sem licença utilizável; `com.unity.pipeline@1.0.1` continua ausente do manifesto, lock e cache.
- AC01 permanece PASS conforme o Source Check já registrado; não foi repetido o preflight completo. AC02 permanece BLOCKED até o Hub concluir a atualização e o Editor abrir sem aviso de licença retornada. AC03/F02–F04, AC04/resolução real, AC05/compilação e cinco testes EditMode, e AC06/reimportação sequencial não foram executados. AC07 permanece parcial; AC08 segue bloqueado pelos limites de licença e segurança descritos abaixo.
- Durante o inventário de janelas, a ferramenta de UI incluiu automaticamente metadados de redirecionamento de autenticação em seu resultado. Nenhum valor foi transcrito para este documento, commit ou PR, e nenhum log bruto foi consultado nesta retomada; não há verificação de revogação remota nem base para afirmar contenção completa fora do repositório. Não repetirei esses dados. A condição de segurança permanece aberta até o proprietário verificar sessões ativas/revogação na conta oficial Unity ou obter confirmação do suporte, se necessário.

### Estado atualizado dos critérios

| Critério | Estado nesta retomada |
| --- | --- |
| AC01 — fontes e Context Lock | PASS histórico registrado; sem novo Source Check completo |
| AC02 — licença e Editor oficial | BLOCKED: Hub em atualização pendente; janela do Editor voltou, mas licença ativa não foi comprovada |
| AC03 — configurações e correções F02–F04 | NOT RUN; dependem de Editor licenciado |
| AC04 — manifest, lock e Pipeline | BLOCKED; pacote não resolvido nem instalado |
| AC05 — compilação e cinco testes EditMode | NOT RUN |
| AC06 — reimportação limpa e sequência | NOT RUN |
| AC07 — Evidence Bundle, ADR-010 e Checkpoint Delta | PARCIAL; evidência atualizada, proposta não adotada e delta não promovido |
| AC08 — escopo, custo e segurança | BLOCKED; nenhum custo ou instalação nesta retomada, mas a revogação/contenção completa da sessão não está comprovada |

**Conclusão do executor: BLOCKED.** A continuidade técnica requer que a atualização da licença Personal termine sem trial/cobrança e que o Editor oficial abra em estado licenciado. Não repetir refresh, instalação ou tentativas de conexão enquanto isso não ocorrer. Após a licença, fazer uma única inspeção pelo Package Manager; cancelar se houver pagamento, trial separado ou downgrade não autorizado, e seguir somente a alternativa manual autorizada pela ADR-010 se a integração não qualificar. Nenhuma aceitação de termos, ativação de licença, revogação externa ou contato com suporte foi realizada nesta retomada.

## Retomada CD-02 — 25/11/2026

### Estado, base e preservação

- Repositório `KayzenRoot/zero-trace`, issue #24, PR #25 aberta em draft, branch `wo/zt-d01-wo-001-im-01`, base `main`.
- Base fixada: `a907ae071aad6cec7f2b1bca0b111d7272527601`. HEAD remoto/local verificado antes desta retomada: `0c513388cbb8ef2bc85cd7cbf77a93b77db812ea`.
- O SHA e os checks do commit que publica esta seção são verificáveis no histórico e no painel da PR #25; não se atribui a este texto um hash autorreferente.
- Context Lock r2, lock histórico, ADR-009, ADR-010, `docs/Checkpoint.md` e fontes canônicas não foram alterados. Nenhum arquivo de credencial, log bruto, segredo, identificador pessoal ou token foi consultado ou incluído nesta retomada. O registro histórico do incidente permanece; não há prova de revogação remota e ela não é presumida.

### Source Check fresco

`git fetch --prune origin` confirmou branch local e remota no mesmo HEAD e `origin/main` igual à base do lock. O cálculo direto dos IDs de blob do Git, sem filtros de checkout, e a comparação dos SHA-256 retornaram:

- 28/28 blobs da base main;
- 30/30 blobs do snapshot histórico de planejamento;
- 87/87 fingerprints brutos no snapshot r2 e 87/87 no HEAD auditado;
- 2/2 SHA-256 das fontes canônicas.

Não houve divergência. Esta verificação antecedeu somente as três alterações Unity listadas nesta evidência; os documentos governados e os Context Locks não foram modificados.

### Source Check reexecutado para esta publicação

A auditoria humana mais recente consultada na PR #25 é de 2026-11-25T22:04:52Z, disposition `BLOCKED / CORRECTION REQUIRED`, auditando o HEAD `56a1b3cf44ca78da5ba8d0ea47804b6c09714a49`. Após essa auditoria, revalidei o Source Check antes da publicação, com a branch e o remoto alinhados em `0c513388cbb8ef2bc85cd7cbf77a93b77db812ea` e `origin/main` em `a907ae071aad6cec7f2b1bca0b111d7272527601`:

- Base `main`: 28/28 blobs conferem com o mapa do lock.
- Snapshot de planejamento histórico: 30/30 blobs conferem no snapshot; 29/30 no HEAD atual. A única diferença é `.gitattributes`, cujo blob atual `77aed40d5ff7c519099149676d2591e04f4ae85f` é o valor expressamente autorizado por `authorized_prior_delta` no lock r2.
- Snapshot de alteração r2 `c29902ea768bafaf4431b7ea95500d0aec8a89ec`: 87/87 blobs conferem no snapshot e 87/87 no HEAD auditado.
- Fontes canônicas: 2/2 SHA-256 conferem. O blob do Context Lock histórico permanece `1662754980fe92f7ebaad51dada80c86778c3c10`; o lock r2 segue sem alteração.

Esta comparação refere-se aos commits publicados antes do novo commit de evidência. As alterações locais de pacote e Player Settings já listadas nesta execução permanecem dentro do delta autorizado e não alteram as fontes canônicas.

### Licença, processo e integração local

O proprietário informou renovação manual da licença Unity Personal no Hub 4.0.2 e ativação em 25/11/2026. Isso é evidência do proprietário, não leitura de conta. Com o Editor 6000.3.29f1 existente em D:, `unity open ... --until-ready --timeout 300` terminou com código 0 e estado `ready` em 19 segundos. `unity status` também retornou sucesso e estado `ready`; não apareceu bloqueio de licença na inicialização. Não foram lidos logs de licença nem arquivos de credenciais.

`unity editors running --json` reportou uma única instância de Editor para o projeto. Na árvore de processos havia dois workers de importação associados à instância, não duas janelas adicionais; não foi aberta uma segunda instância licenciada. O Editor permanece aberto após a recompilação.

A documentação oficial diz que o Unity Pipeline atende somente em loopback `127.0.0.1`, exige token válido para comandos e não habilita Player Runtime por padrão ([segurança do Unity CLI e Pipeline](https://docs.unity.com/en-us/unity-cli/unity-cli-security)). A CLI reportou servidor alcançável em `127.0.0.1:7800`; `unity status` e `unity recompile` conseguiram consultar/controlar o Editor com sucesso. Como a Unity documenta que comandos sem token válido são rejeitados, o sucesso dessas chamadas é evidência da autenticação local ativa; o valor do token não foi lido. Nenhum endereço externo, token ou conteúdo do arquivo de token foi lido ou publicado. A CLI é 1.0.1; a interface de comandos disponíveis do Editor não produziu manifesto válido nesta execução, então nenhum comando arbitrário foi enviado.

### G-PIP-01/G-PIP-02 — preço, compatibilidade e resolução UPM

O proprietário informou retorno ao Hub sem oferta de trial ou cobrança. No Package Manager oficial, a instalação pelo nome/versão exatos `com.unity.pipeline` `1.0.1` não apresentou tela de preço, trial ou licença separada. A UI indicou origem Unity Registry, assinatura Unity Technologies e versão 1.0.1. A rota documentada usa Editor 6.0 ou posterior e Unity CLI; a FAQ oficial lista CLI, MCP e plugin oficial como gratuitos e separa o trial do assistente integrado ([preços e ferramentas Unity](https://unity.com/features/ai), [pacote Unity Pipeline](https://docs.unity.com/en-us/unity-cli/unity-pipeline/unity-pipeline-package)). Os termos do Editor §2.7 continuam aplicáveis ao Package Manager ([termos do Editor Unity](https://unity.com/legal/editor-terms-of-service/software)).

O resultado real do UPM foi confirmado após a importação:

- `Packages/manifest.json`: pin direto `com.unity.pipeline: 1.0.1`;
- `Packages/packages-lock.json`: entrada `1.0.1`, origem `https://packages.unity.com`, dependências resolvidas pelo UPM;
- cache local `com.unity.pipeline@c0af79690db1` e `package.json` com nome `com.unity.pipeline`, versão `1.0.1` e compatibilidade declarada `6000.0`;
- versões efetivas mantidas: Test Framework builtin `1.6.0`, Newtonsoft `3.2.2` e Mono.Cecil `1.11.6`. O pacote declara mínimos `1.1.33`, `3.0.2` e `1.11.6`, respectivamente; não houve downgrade nem edição manual do lock.

O campo `license` do `package.json` instalado é nulo; por isso esta evidência não inventa um texto de licença do pacote. Aplicam-se os termos oficiais do Editor/Package Manager citados acima. Não foi aceita compra, trial, serviço cloud ou nova assinatura e não há despesa conhecida.

### F01–F04 e correções do template

- **F01 — instâncias/licença:** uma instância de Editor em execução e estado `ready`; licença Personal e data de renovação são relato do proprietário. A ocorrência histórica e a ausência de comprovação de revogação externa permanecem explícitas.
- **F02 — GUID de Input Settings:** `ProjectSettings/EditorBuildSettings.asset` ainda contém o GUID `289c1b55c9541489481df5cc06664110`. A busca estática não encontrou `.meta` correspondente em `Assets`, e `com.unity.inputsystem` não está no manifest nem no lock. A consulta exigida por `AssetDatabase` não foi obtida pela interface disponível; não removi a referência sem essa confirmação. O handler permanece no modo legado (`activeInputHandler: 0`); não acrescentei Input System nem gameplay.
- **F03 — identidade, Analytics e backend:** o `ProjectSettings.asset` serializado agora grava identificador Standalone provisório `com.kayzenroot.zerotrace`, `submitAnalytics: 0` e `scriptingBackend.Standalone: 0` (Mono); o backend Android existente ficou inalterado. O Editor reimportou esses valores e a recompilação ficou `up_to_date`. O identificador é uma proposta técnica de projeto, não decisão legal de marca/lançamento. `UnityConnectSettings` mantém Analytics desabilitado; `m_EngineDiagnosticsEnabled` continua ligado, portanto não se declara telemetria zero. A UI não confirmou a arquitetura ativa Windows x64. Não foram alteradas preferências globais.
- **F04 — dependências builtin:** o manifest contém 34 módulos builtin diretos. Mantive os padrões gerados, incluindo `androidjni`, `adaptiveperformance`, `unityanalytics`, `vr` e `xr`, para evitar limpeza ampla do template sem evidência de ganho e sem prejudicar a resolução. A conexão Unity Cloud continua desativada; manter o módulo builtin de Analytics não o habilita por si só. Nenhum pacote de terceiros foi adicionado.

O projeto não contém `ENABLE_RUNTIME_PIPELINE` nos arquivos versionados e esta execução não habilitou Runtime. A documentação oficial declara ambas as opções de Player Runtime desabilitadas por padrão. Como o painel Runtime não ficou acessível para leitura, os valores efetivos do painel não são apresentados como observados; não houve build de Player.

### Compilação, testes e importação limpa

- `unity recompile --project-path <checkout> --timeout 300 --json`: código 0, `status=up_to_date`, zero erros e zero avisos. Foi executado depois dos ajustes de Player Settings, com o Editor existente.
- `unity test <checkout> --editor-path <Editor existente> --mode EditMode --filter StableIdentifierTests --output <relatório temporário> --timeout 600 --json`: código 6 (`COMMAND_FAILED`); nenhum relatório NUnit foi criado. Portanto o número de testes aprovados é **não comprovado**, não 0/5 nem 5/5. Uma tentativa anterior tinha apenas erro de sintaxe (`INVALID_COMMAND_ARGS`) e não iniciou testes; a chamada corrigida acima foi a única execução efetiva e não será repetida por esta integração.
- A reabertura do mesmo checkout importou o pacote e recompilou, mas não equivale à reimportação sequencial em um checkout limpo. Essa etapa ficou bloqueada pela falha do Test Runner; nenhum segundo checkout ou Editor foi aberto.
- A interface do Unity não ficou acessível para operar AssetDatabase ou Test Runner manualmente. A alternativa manual prevista não pôde ser executada nesta sessão. Nenhum log foi usado para inferir causa além do código de saída e da ausência do relatório.

### Estado AC01–AC08

| Critério | Estado nesta retomada | Evidência e limite |
| --- | --- | --- |
| AC01 — fontes e Context Lock | PASS | Source Check fresco: 28/28 base, 30/30 histórico, 87/87 r2 no snapshot e no HEAD, 2/2 SHA-256; base/branch/remoto alinhados. |
| AC02 — licença e Editor oficial | PASS técnico / evidência do proprietário | Renovação Personal informada pelo proprietário; CLI abriu e reportou `ready`, sem erro de licença. Não prova revogação remota do incidente. |
| AC03 — template e F02/F03 | BLOCKED | F03 atualizado e recompilado; F02 depende de AssetDatabase. Arquitetura ativa Windows x64 e opções efetivas Runtime não foram lidas na UI. |
| AC04 — manifest, lock e pacotes | PASS | UPM resolveu a versão fixa 1.0.1, cache presente, origem Unity Registry e versões efetivas sem downgrade. |
| AC05 — compilação e cinco testes EditMode | BLOCKED | Recompilação PASS, 0 erros/avisos. `unity test` terminou com código 6 sem relatório; 5/5 não comprovado. |
| AC06 — reimportação limpa, integridade e CI | BLOCKED / parcial | Reabertura/importação do checkout atual, validação documental e diff local passaram; checkout limpo não foi iniciado. CI Unity em nuvem permanece diferido para QA-01 conforme ADR-009. |
| AC07 — Evidence Bundle, ADR-010 e Checkpoint Delta | PARCIAL | Esta atualização complementar; proposta ADR-010 continua não adotada; Checkpoint Delta abaixo não promovido. SHA final e checks exatos são publicados na PR #25. |
| AC08 — escopo, custo e segurança | BLOCKED | Sem despesa, cloud, gameplay, bypass ou divulgação de credenciais. O incidente permanece aberto; não há prova de contenção/revogação remota. |

### Validação local desta publicação

- `py -3 .github/scripts/validate_docs.py`: código 0, `DOCS_VALIDATION_OK`.
- `git diff --check`: passou sem erro de whitespace. O diff dos Context Locks, ADR-009, ADR-010 e checkpoint está vazio.
- Manifesto/lock: 37 dependências diretas, 55 entradas resolvidas, 0 dependências diretas ausentes e 0 versões diretas divergentes.
- Busca de padrões comuns de token/segredo no diff: 0 ocorrências. Os únicos caminhos versionados alterados são manifest, lock de pacotes, `ProjectSettings.asset` e este Evidence Bundle; nenhum log, cache, resultado de teste ou configuração de usuário foi incluído.
- O SHA final desta atualização e os checks remotos do HEAD exato são registrados na PR #25 depois do push.

### Proposta de adoção da ADR-010 — não adotada

Manter `ZT-ADR-010` como proposta condicional, sem atualizar `docs/Decisions.md`. A rota oficial local está qualificada para status e recompilação, com servidor loopback e CLI 1.0.1; a execução dos testes não foi qualificada. Não considerar adoção até AssetDatabase/F02, Test Runner 5/5, reimportação sequencial limpa, leitura efetiva de Runtime/target e gates de segurança restantes serem comprovados. ADR-009 permanece intacta: CI Unity em nuvem somente em QA-01.

### Checkpoint Delta — não promovido

- `ZT-D01-WO-001-IM-01/CD-02` permanece **BLOCKED**, sem aceitação final e sem avanço para IM-02.
- Não alterei o checkpoint canônico. Exigir AssetDatabase para o GUID, resultado real 5/5 EditMode, reimportação limpa, verificação do alvo/runtime e decisão do proprietário sobre a contenção do incidente antes de fechar o WO.
- Nenhum merge, despesa, serviço cloud, implementação de gameplay ou alteração em ADR-009 foi realizada.
