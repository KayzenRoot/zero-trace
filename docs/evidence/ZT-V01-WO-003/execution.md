# ZT-V01-WO-003 — Evidence Bundle de execução

**Estado:** implementação local concluída e submetida para auditoria independente; não é aprovação independente. A PR de implementação deve permanecer em draft e sem merge.
**Data:** 2026-11-27
**Repositório:** `KayzenRoot/zero-trace`
**Branch:** `feat/zt-v01-wo-003-null-terminal`
**Base `main`:** `6ee689a0591774073b8231b5f2cdb9e056f71808`
**HEAD de código testado:** `2b74749038838d949774c4b94bca3f5ebb107880`
**Context Lock:** [ZT-V01-WO-003.json](../../locks/ZT-V01-WO-003.json)
**Checkpoint Delta:** [proposta não promovida](checkpoint-delta.md)

O bundle e as capturas são acrescentados em commit documental posterior ao HEAD de código testado. Esse commit não altera o código C# coberto pelas suítes; o corpo da PR registra o HEAD final de publicação e os checks remotos exatos.

## Source Check, lock e preservação

- Source Check fresco: `origin/main` e o SHA da base do lock são ambos `6ee689a0591774073b8231b5f2cdb9e056f71808`.
- Context Lock: **35/35 fingerprints Git críticos**, **141/141 blobs do projeto Unity**, **2/2 SHA-256 canônicos** conferidos. O Checkpoint e ADR-009/010/013/014/015 permanecem byte a byte sem diff contra a base.
- GDD `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1`; o Mission Catalog é `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365`.
- PR #28 continua aberta/em draft, branch `wo/zt-d01-wo-001-qa-01-exec`, base `1b72c6773fc9edd39c44239ec127a75d7ca9d158`, HEAD `dd1ab1e2d2359e9a0d4a90cb68ca4ef1552beee1`; `docs-validation`, Socket Project Report e Socket Pull Request Alerts passaram (3/3). Nenhuma mutação foi feita nessa PR.
- O arquivo não rastreado `ProjectSettings/SceneTemplateSettings.json` foi preservado fora dos commits.



## Implementação e limites

- `Assets/ZeroTrace/Domain/LocalPrototypeTerminal.cs`: catálogo imutável dos três arquivos sintéticos existentes, snapshot tipado, dispatcher ordinal para sete comandos e sessão com quotas determinísticas.
- `Assets/ZeroTrace/Domain/LocalPrototypeFlow.cs`: enum `Terminal` anexado após os valores existentes e seleção do exercício vinculada ao ID compartilhado do catálogo.
- `Assets/ZeroTrace/Presentation/Prototype/LocalFirstPlayableController.cs` e `LocalPrototypeText.cs`: aba UI Toolkit, entrada/saída simples, Enter, histórico ↑/↓, ciclo Tab, scroll, limpar/fechar, estados/localização en-US e pt-BR. O File Manager lê o mesmo catálogo.
- `Assets/ZeroTrace/Tests/EditMode/LocalPrototypeTerminalTests.cs` e `Assets/ZeroTrace/Tests/PlayMode/LocalFirstPlayablePlayModeTests.cs`: cobertura de allowlist, aridade, entradas host/path/control/script, limites, estado, persistência, callbacks de UI, localização, ciclo de vida e capturas.
- Não há acesso de runtime a shell, filesystem, rede, socket, processo, reflexão ou APIs de UnityEditor. O único uso de arquivo nas mudanças é o teste PlayMode que grava PNGs solicitados em `docs/evidence/.../screenshots/`.
- Nenhum pacote, manifest, lock de pacotes, versão de Editor, cena, ADR ou Checkpoint canônico foi alterado. `com.unity.pipeline 1.0.1` permanece instalado, mas Runtime Pipeline segue desativado; busca de defines de ativação em `Assets/ProjectSettings` não encontrou correspondências.

## Editor, licença, compilação e testes

- Unity CLI **1.0.1** e um único Editor Unity **6000.3.29f1**, instalado em `D:`, foram reutilizados. `unity status` indicou somente um Editor `ready`; no encerramento, `editor.status` mostrou `playMode=stopped`, `compiling=false`.
- O proprietário confirmou renovação da licença Personal em 2026-11-25; o Editor abriu e operou sem prompt/erro de licença. Não foram lidos arquivos de licença/credenciais, nem alterada a conta ou a licença.
- `unity command script recompile_status`: `failed=false`, `compilationFailed=false`.
- `unity command test run --mode editor --async-tests true --timeout 900 --project-path <checkout> --caller plugin --skill unity-cli`; `unity command test status --detail summary`: **65/65 EditMode PASS**, 0 falhas, 0 skips, 0 inconclusivos.
- `unity command test run --mode playmode --async-tests true --timeout 900 --project-path <checkout> --caller plugin --skill unity-cli`; `unity command test status --detail summary`: **13/13 PlayMode PASS**, 0 falhas, 0 skips, 0 inconclusivos.
- As suítes são completas, sem filtros nem exclusões artificiais. Não foi feito Player build.
- Console foi preservado sem `console clear`. Snapshot final: `compilationFailed=false`, `consoleErrors=1`, `consoleWarnings=0`. O único erro agregado já existia no baseline anterior a esta implementação e corresponde ao aviso de metadados `SharedTableData Name Guid` divergente do GUID do asset; não é erro de compilação nem foi introduzido por esta WO. Não alego Console sem erros.

## Inspeção visual

As cinco capturas foram produzidas durante PlayMode local e inspecionadas visualmente. A captura 1280×720 foi obtida pelo comando oficial `unity command capture game_view` com `source=camera` após abrir `SampleScene`, iniciar o protótipo e selecionar Terminal; a cena foi encerrada normalmente e não ficou dirty.

| Captura | Conteúdo | Resolução | SHA-256 |
| --- | --- | --- | --- |
| [terminal-open-en-US-1280x720.png](screenshots/terminal-open-en-US-1280x720.png) | Terminal aberto em inglês; layout completo sem corte | 1280×720 | `2333b297b32e928c861e3abbb2642169d3fcb7090c74e6babbbeb4449edf7699` |
| [terminal-open-en-US.png](screenshots/terminal-open-en-US.png) | Terminal aberto em inglês | 1920×1080 | `456e897845c1d354b6c13886dabde73757fa4d7654080e9042fb0f9f0fc3c176` |
| [terminal-read-en-US.png](screenshots/terminal-read-en-US.png) | Leitura local de `recovery_manifest.txt` | 1920×1080 | `2ce28009633d95a43758aacdfa90765342138f6a2136fa8c12d177e83909cb3b` |
| [terminal-invalid-command-en-US.png](screenshots/terminal-invalid-command-en-US.png) | `os.shutdown` recusado como comando desconhecido | 1920×1080 | `4e9d215758708e2ca3c09e708908ce9c3d615aa087a84d4efb5dbf89a8278e99` |
| [terminal-pt-BR.png](screenshots/terminal-pt-BR.png) | Interface pt-BR e conteúdo sintético localizado | 1920×1080 | `c8bc300610843f5cd38171ed46ccbf572cb4aa9af65421b5707537853f64400c` |

## Integridade, segurança e validação documental

- Git blobs de `Packages/manifest.json` e `Packages/packages-lock.json` conferem com o lock; nenhum pacote novo, downgrade ou alteração de versão ocorreu.
- Busca estática nos arquivos C# de runtime: **0** referências aos limites proibidos (filesystem, processo, rede, socket, reflexão, UnityEditor, shell ou credenciais). Busca escopada de padrões comuns de segredos nos arquivos textuais alterados: **0 correspondências**.
- `git diff --check`: PASS. `python .github/scripts/validate_docs.py`: `DOCS_VALIDATION_OK` (420 briefs, matriz de releases, grafo de 20 nós, 30 apps e 3 SVGs).
- Os checks remotos exatos da publicação serão reportados no corpo da PR depois de concluídos; nenhum resultado de CI posterior é presumido nesta evidência.
- Não houve cloud, build, deploy, instalação, nova dependência, assinatura ou despesa. Custo adicional: **US$ 0**.

## Critérios AC01–AC11

| Critério | Estado | Evidência |
| --- | --- | --- |
| AC01 — base, Context Lock e fontes | PASS | Base exata; 35/35 fingerprints críticos, 141/141 blobs Unity e 2/2 SHA-256. |
| AC02 — comandos tipados e allowlist | PASS | Sete comandos; comparações ordinais e aridade exata; EditMode completo. |
| AC03 — catálogo e exercício existente | PASS | Exatamente os três IDs atuais; File Manager compartilha identidade/conteúdo; fluxo do exercício permanece File Manager-only. |
| AC04 — entradas inválidas | PASS | Casos desconhecidos, caminho, controles, scripts e excesso rejeitados; sem exceção nos testes. |
| AC05 — limites/determinismo | PASS | Input 256, 4 args, output 4096 por comando, histórico 50 e tela 32768 cobertos. |
| AC06 — interface, foco e idiomas | PASS | Enter, ↑/↓, Tab, scroll, clear/close, en-US/pt-BR em PlayMode; 1280×720 e 1920×1080 inspecionados. |
| AC07 — ciclo de vida e leitura | PASS | Sessão local sem save, disponível somente Ready/Terminal; reset em desligamento/saída; regressões do fluxo permanecem cobertas. |
| AC08 — compilação e suítes | PASS | `compilationFailed=false`; 65/65 EditMode e 13/13 PlayMode, sem skip/falha/inconclusivo. |
| AC09 — capturas reais | PASS | Cinco PNGs PlayMode, dimensões e SHA-256 acima. |
| AC10 — segurança, fontes, pacotes e CI | PASS | Fonte/lock/pacotes estáticos e `DOCS_VALIDATION_OK` conferidos; os checks remotos exatos do HEAD corrente são registrados no corpo da PR. |
| AC11 — operação e evidência | PASS | US$ 0, sem cloud/build/deploy; PR #28 inalterada; bundle e Checkpoint Delta proposto, não promovido. |

**Disposição do executor:** `READY FOR INDEPENDENT AUDIT` após docs-validation local e os três checks remotos do HEAD final passarem. Isso não afirma auditoria independente APPROVED nem autoriza merge ou promoção de checkpoint.