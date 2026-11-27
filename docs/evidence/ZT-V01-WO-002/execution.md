# ZT-V01-WO-002 — Evidence Bundle

**Estado:** execução local concluída; PR #33 continua em draft e aguarda auditoria independente. Este registro não declara aprovação independente nem autoriza merge.
**Data:** 2026-11-27
**Repositório:** `KayzenRoot/zero-trace`
**Issue relacionada:** #32 (planejamento integrado pela PR #32)
**Branch:** `feat/zt-v01-wo-002-starter-apartment`
**PR de execução:** [#33](https://github.com/KayzenRoot/zero-trace/pull/33)
**Base `main`:** `79c9f0f55a520736b47ce7cba64d8c70bd591682`
**HEAD de código testado:** `06b6274b99c5ea981bf3b8636a7ec6270554e7f0`
**Context Lock:** [ZT-V01-WO-002.json](../../locks/ZT-V01-WO-002.json)
**Checkpoint Delta:** [proposta não promovida](checkpoint-delta.md)

O HEAD de código acima contém integralmente a implementação e os testes executados. Este Evidence Bundle é publicado em commit documental subsequente; o corpo da PR registra o HEAD final de publicação e seus checks exatos.

## Source Check, lock e governança

- Source Check fresco confirmou `origin/main` em `79c9f0f55a520736b47ce7cba64d8c70bd591682`, igual à base travada.
- O Context Lock de execução permanece vinculado à base: **33/33 fingerprints Git críticos**, **141/141 blobs do projeto Unity**, **2/2 SHA-256 canônicos**. Checkpoint: blob `6e65d473058f1b8854289af4cd05e34ee6f15edf`, inalterado.
- SHA-256 das fontes canônicas conferem com `docs/Sources.md`: GDD `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1`; Mission Catalog `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365`.
- A PR #32 foi integrada por squash no HEAD `5ba041ac643314a0bf6825ced6459572a3df6ffb`; a autorização direta do proprietário para executar esta WO é posterior e está registrada no Context Lock. ADR-014 não foi modificada.
- Ruleset `ZT-GOV-main`, ID `28038825`: ativo em `refs/heads/main`, sem bypass actors; bypass do usuário `never`; deleção e non-fast-forward proibidos, histórico linear, resolução de conversas e squash exigidos, `docs-validation` obrigatório.
- PR #28 permanece aberta/em draft na branch `wo/zt-d01-wo-001-qa-01-exec`, HEAD protegido `dd1ab1e2d2359e9a0d4a90cb68ca4ef1552beee1`, base `1b72c6773fc9edd39c44239ec127a75d7ca9d158`; seus três checks conhecidos no HEAD protegido foram SUCCESS (3/3). Nenhuma alteração foi feita nela.
- O arquivo local preexistente `ProjectSettings/SceneTemplateSettings.json` foi preservado fora dos commits; SHA-256 `bb9098b3bfcde78d93e264b96f7b77b5430e64e8c57a7aad7f5a5d9c3945e16a`.

## Implementação e escopo

- `Assets/ZeroTrace/Presentation/Prototype/LocalPrototypeRoomFactory.cs`: substitui os grandes inlays neon por acabamento discreto; adiciona porta cenográfica, janela estática, sofá compacto, tapete, luminária local e armazenamento modesto. Paredes/colliders originais continuam contínuos atrás da porta e da janela. O computador de recuperação continua sendo o único alvo interativo; seus materiais e os demais materiais runtime usam acabamento pouco especular. Todos os objetos e luzes novos são criados sob o root runtime existente.
- `Assets/ZeroTrace/Tests/PlayMode/LocalFirstPlayablePlayModeTests.cs`: estende os testes de limites para as regiões da porta e da janela e adiciona regressões de composição/monitor único/raio de interação e restauração de câmera/root.
- Capturas PlayMode reais estão em `screenshots/` e descritas abaixo.
- Nenhuma alteração em `LocalFirstPlayableController`, `LocalPrototypePlayerMotor`, `SampleScene`, domínio, save schema, localização, pacotes, manifest, lock, versão do Editor, Checkpoint, ADR-009/010/013/014 ou PR #28.

## Editor, compilação, testes e Console

- Unity CLI existente **1.0.1**; um único Editor oficial conectado, `6000.3.29f1`, `ready`, `compiling=false`, Play Mode encerrado ao final. Nenhuma instalação, atualização, ativação ou mudança de licença.
- A licença Personal é sustentada pela confirmação prévia do proprietário e pelo Editor já aberto sem erro/prompt de licença; não foi feita consulta de conta nem leitura de credenciais.
- `unity recompile --project-path .`: a primeira execução retornou os três avisos abaixo; após o pequeno ajuste final de material, a segunda execução terminou com código zero, mas informou que não conseguiu ler o Console. Em seguida, o Editor registrou `compilationFailed=false`, `compiling=false`, e ambas as suítes completas passaram no código final, comprovando compilação válida do candidato.
- `unity command --project-path . test run --mode editor --async-tests true --timeout 600`; `unity command --project-path . test status --detail summary`: **29/29 EditMode PASS**, 0 falhas, 0 skips, 0 inconclusivos.
- `unity command --project-path . test run --mode playmode --async-tests true --timeout 600`; após a troca de domínio, `unity status --until-ready --timeout 120 --project-path .` e `unity command --project-path . test status --detail summary`: **10/10 PlayMode PASS**, 0 falhas, 0 skips, 0 inconclusivos. A primeira consulta de status ocorreu enquanto o Pipeline reconectava; a leitura após `ready` confirmou a execução completa. Nenhum teste foi repetido por esse motivo.
- Os testes cobrem startup, movimento/visão, ciclo do computador, apps e exercício sintético, retry e save/load, localização, foco, colisão das quatro paredes e das regiões de entrada/janela, safe spawn/recuperação, alvo de interação sem obstrução, monitor único e saída que restaura câmera e remove o root.
- Diagnósticos de compilação: **3 warnings CS0618 preexistentes**, em `StartupController.cs` linhas 300, 318 e 326; nenhum warning nos arquivos alterados. Não são falhas de compilação.
- A suíte PlayMode emitiu **1 Error intencional** do teste negativo existente `StartupController` (“startup interface unavailable”); o teste passou. Captura posterior do Console indicou `compilationFailed=false`, `compiling=false` e zero erros/avisos ativos.
- Uma leitura anterior registrou três eventos `HandleStatusRequest` de conexão interrompida do servidor Unity Pipeline, temporalmente coincidentes com probes de metadata/`--help` que excederam o limite de resposta. Foram classificados como diagnósticos de transporte da ferramenta, não como erros de compilação ou do código da sala; não reapareceram na amostra pós-testes. A CLI recuperou e permaneceu conectada.
- Não foi executado Player build.

## Inspeção visual e capturas

As imagens foram produzidas pela Unity CLI oficial (`capture game_view --source camera`) com a Main Camera durante Play Mode e foram inspecionadas visualmente. As duas capturas workstation/residential usam o spawn e FOV de jogo padrão; a captura overview reposiciona somente o jogador/câmera durante Play Mode para mostrar a entrada, sem gravar cena ou transformar essa pose em gameplay.

| Captura | Conteúdo | Resolução | SHA-256 |
| --- | --- | --- | --- |
| [workstation-residential-1280x720.png](screenshots/workstation-residential-1280x720.png) | Mesa/computador, sofá, janela e armazenamento no spawn padrão | 1280×720 | `68740770e51dc05009bd6b34ace6ae345adebb23deac07d2d13c175a91cb5cf4` |
| [workstation-residential-1920x1080.png](screenshots/workstation-residential-1920x1080.png) | Mesmo enquadramento no spawn padrão | 1920×1080 | `668bc7e04286f647dba10e8245e1c0d78b60e037e7bba6c2e19b3006ffd19f1d` |
| [room-overview-entry-1920x1080.png](screenshots/room-overview-entry-1920x1080.png) | Entrada, mesa e cue residencial por ângulo de inspeção runtime | 1920×1080 | `dcef7dfa124eec97a2b861a71b383b294f47104decbf8437d590b52b10e316b0` |

Scorecard local (1–5; revisão do executor, não auditoria independente): leitura de apartamento inicial **4**; legibilidade das pistas residenciais **4**; hierarquia/paleta **4**; proporção dos equipamentos **5**; composição/enquadramento **3**; coerência com primitivas originais **4**. A limitação restante é o acabamento propositalmente simples de protótipo e o teto visível em primeira pessoa; nada foi acrescentado fora da WO para disfarçar essa limitação.

## Integridade, segurança e operações

- `Packages/manifest.json`, `Packages/packages-lock.json` e `ProjectSettings/ProjectVersion.txt` permanecem idênticos à base: URP 17.3.0, Localization 1.5.14, Pipeline 1.0.1 e Editor `6000.3.29f1`.
- GDD e catálogo mantêm os hashes canônicos acima; Checkpoint e decisões aprovadas não foram alterados.
- A inspeção do diff confirma apenas fábrica visual, regressões PlayMode e PNGs de evidência. Não há nova dependência, string de UI/localização, acesso de rede/processo, asset importado, credencial ou serviço.
- Não há build, Unity Cloud/UBA/DevOps, publicação, deploy, assinatura, trial ou despesa. Custo adicional: **US$ 0**.
- `python .github/scripts/validate_docs.py`: **DOCS_VALIDATION_OK**, 420 briefs originais e verificações documentais reportadas.
- `git diff --check origin/main`: PASS. `git diff --exit-code origin/main -- Packages/manifest.json Packages/packages-lock.json ProjectSettings/ProjectVersion.txt`: PASS, sem diferenças.
- Source Check do lock: **33/33** blobs críticos, **141/141** arquivos Unity, SHA-256 do GDD e catálogo e blob do Checkpoint conferem.
- Busca escopada de segredos nos arquivos textuais da WO: **0 correspondências**. Busca estática nas linhas novas de C#: **0 novas chamadas de filesystem, rede, processo, socket, credenciais ou UnityEditor**.

## Critérios AC01–AC17

| Critério | Estado | Evidência |
| --- | --- | --- |
| AC01 — Source Check e Context Lock | PASS | Base e lock exatos; 33/33 fingerprints, 141/141 blobs Unity, 2/2 SHA-256. |
| AC02 — autorização | PASS | ADR-014 integrada na base e instrução direta do proprietário. |
| AC03 — alinhamento com Visual Master | PASS (executor) | Capturas reais de ambiente modesto de um único cômodo; sem equipamento premium. |
| AC04 — composição residencial | PASS | Entrada, janela e sofá estáticos visíveis nas capturas; teste confirma objetos sob o root. |
| AC05 — limite de equipamento | PASS | Teste conta um monitor físico e um collider alvo no monitor de recuperação. |
| AC06 — qualidade visual | PASS (executor) | Paleta Obsidian/Carbon/Frost com acentos discretos e scorecard registrado. |
| AC07 — preservação do fluxo | PASS | 29/29 EditMode e 10/10 PlayMode, incluindo apps, locale, exercício e save/load. |
| AC08 — colisão e segurança | PASS | Parede contínua; testes tentam sair pelas quatro paredes e pelas regiões da porta/janela; recovery e raio passam. |
| AC09 — ciclo de vida | PASS | Teste verifica câmera restaurada e root runtime removido. |
| AC10 — dependências | PASS | Manifest, lock e versão do Editor idênticos à base. |
| AC11 — testes | PASS | 29/29 EditMode; 10/10 PlayMode; nenhum skip/fail/inconclusivo. |
| AC12 — execução local | PASS com nota de ferramenta | Editor único e compilação válida pelos testes; três warnings legados e eventos Pipeline transitórios documentados; nenhum erro ativo pós-execução. |
| AC13 — capturas | PASS | PNGs reais em 1280×720 e 1920×1080, com SHA-256. |
| AC14 — docs/segurança | PASS local | `DOCS_VALIDATION_OK`, `git diff --check`, dependências/fontes íntegras, 0 correspondências na busca escopada de segredos e 0 chamadas de boundary nas linhas novas. |
| AC15 — custo/operação | PASS | US$ 0; sem cloud, trial, termos, pagamento, build ou deploy. |
| AC16 — PR #28 | PASS | Leitura final: PR #28 aberta/draft no HEAD `dd1ab1e2d2359e9a0d4a90cb68ca4ef1552beee1`; docs-validation, Socket Project Report e Socket PR Alerts SUCCESS (3/3); sem mutação. |
| AC17 — evidências/checkpoint | PASS | Evidence Bundle publicado; Checkpoint Delta segue proposta não promovida. |

## Checkpoint Delta e disposição

Propõe-se registrar somente a leitura visual local/offline de uma sala do protótipo técnico como apartamento inicial modesto. `docs/Checkpoint.md` permanece inalterado; D01 segue incompleto, QA-01 não qualificado, D02 e D09 continuam bloqueados. O Delta não é promoção, missão canônica, vertical slice, release nem aprovação de assets de produção.

**Disposição:** candidato submetido para auditoria independente na PR #33, ainda em draft. Sem merge, promoção de checkpoint ou avanço de Work Order.
