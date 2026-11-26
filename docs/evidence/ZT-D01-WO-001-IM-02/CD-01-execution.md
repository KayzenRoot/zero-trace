# ZT-D01-WO-001-IM-02/CD-01 — Evidências de correção

Data: 2026-11-26
Repositório: KayzenRoot/zero-trace
Issue: #26 | PR existente: #27 (draft/open) | branch: wo/zt-d01-wo-001-im-02
Base main: 048922542f2752970dc812a300b300e4ea54dd7e
Planning HEAD informado: 9a82ce06fd7a22c4f3618d1c8ef9857561ca29d2
Snapshot de fingerprints do Context Lock: 1639b87c4090a344e775e8de8bc6871024371318
HEAD remoto antes da publicação: ce9fba2df5333a2ba7050a344963917e5709866b
HEAD do candidato de implementação e testes: 9ec7ca5f35753d5d077b1370df3b272ef340109e
Resultado local: PASS para F01–F03 e AC01–AC08; aguardando checks exatos do HEAD publicado nesta PR.
Checkpoint: delta proposto e não promovido, em CD-01-checkpoint-delta.md.

## Resultado

O candidato corrige os três achados delimitados na auditoria CD-01. A inicialização espera pelo UIDocument, valida os recursos e a primeira renderização antes de declarar prontidão ou registrar callbacks. A falha permanece visível e indisponível até recuperação válida; disable, destroy e re-enable cancelam ou recriam o ciclo sem listeners duplicados. O idioma corrente agora possui rótulo localizado, além do estado visual; o foco de teclado usa um contorno próprio, distinto do traço de seleção.

Nenhum gameplay, cena adicional, pacote, configuração permanente do Git, ADR ou fonte canônica foi alterado. A camada Domain, os cinco testes StableIdentifier e ADR-009/ADR-010 permanecem intactos.

## Source Check e Context Lock

- O branch local é wo/zt-d01-wo-001-im-02 e deriva do HEAD remoto de entrada ce9fba2.
- O main remoto continua em 048922542f2752970dc812a300b300e4ea54dd7e, conferido por git ls-remote e igual à base do lock.
- 91/91 fingerprints da base e 93/93 fingerprints do snapshot de planejamento conferem, sem divergências.
- Os SHA-256 brutos das fontes canônicas conferem: GDD v0.2.0 4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1; catálogo 836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365.
- O manifesto [CD-01-tested-files.sha256](CD-01-tested-files.sha256) confirma 7/7 arquivos de código, tabelas e testes no candidato 9ec7ca5.
- O Context Lock mantém predecessor IM-01 aprovado em 048922542f2752970dc812a300b300e4ea54dd7e. O ruleset ZT-GOV-main é 28038825, enforcement active, main protegido, sem bypass, com docs-validation obrigatório, conforme o readback de governança do lock.

## Correções delimitadas

### F01 — inicialização e ciclo de vida

StartupController aguarda o documento visual, valida view e tabelas e conclui o render inicial determinístico em en-US antes de definir IsReady ou inscrever SelectedLocaleChanged. Eventos repetidos ou não suportados são ignorados. A falha de recursos transita para estado localizado e seguro, sem declarar prontidão. OnDisable/OnDestroy cancelam a inicialização e removem subscriptions e callbacks; re-enable volta a vincular uma única vez e mantém o locale suportado selecionado.

### F02 — idioma e foco não exclusivamente cromáticos

A interface exibe “Language: English (US)” ou “Idioma: Português (Brasil)” conforme a seleção atual. A seleção também mantém seu tratamento visual. O foco de teclado é mostrado por contorno branco de 3 px; o traço roxo de 4 px permanece reservado à seleção. A indicação do idioma não depende da cor.

### F03 — regressões de comportamento

Os testes agora exercitam a inicialização com locale emitido antes do bind, falha real com UIDocument ausente, disable/re-enable enquanto a inicialização está pendente, locale durante a falha, recuperação e ciclos repetidos. As asserções observam estado e conteúdo do StartupController e da UI, em vez de inferir chamadas do controller a partir de um observador global não relacionado. Os testes de UI distinguem dispatch sintético de teclado/clique do smoke manual observado na janela do Editor.

## Matriz AC01–AC08

| Critério | Estado | Evidência |
|---|---|---|
| AC01 — Source Check e governança | PASS | Base remota igual ao lock; 91/91 + 93/93 fingerprints; 2/2 SHA-256; regra 28038825 active/sem bypass conforme lock. |
| AC02 — cena e Editor | PASS | SampleScene é a cena inicial registrada; Editor 6000.3.29f1 no Windows x64, URP 17.3.0, Mono; UIDocument, PanelSettings, UXML e USS carregados sem referências ausentes. |
| AC03 — localização | PASS | Locales en-US e pt-BR, default inglês, troca EN→PT→EN, chaves requeridas idênticas e não vazias, fallback localizado, status de indisponibilidade separado do status pronto. |
| AC04 — entrada, foco e layout | PASS | Smoke visual e de mouse/Tab/Enter observado; foco distinguível da seleção; medidas automatizadas em 1920×1080 e logical 1280×720 com scale 1.5; expansão textual de 35%; sem clipping/overlap nas asserções. |
| AC05 — compilação e testes | PASS | Compilação sem erro; EditMode 10/10 e PlayMode 2/2 no candidato, também repetidos após reimportação limpa. Os 10 EditMode incluem 5 StableIdentifier, 4 StartupLocalization e 1 exemplo de pacote Addressables (não é cobertura do produto). |
| AC06 — reimportação e integridade | PASS | Clone isolado começou sem Library, importou sequencialmente em 251 s, recompilou up_to_date e repetiu EditMode/PlayMode; manifesto 7/7; pacotes, GUIDs, referências e Domain preservados; validação documental e diff-check locais aprovados. |
| AC07 — evidência e checkpoint | PASS local | Este bundle, hash manifest e delta não promovido acompanham a mesma PR. Os checks exatos do commit de evidência serão confirmados no estado atual da PR após push; os checks do HEAD anterior não são atribuídos ao novo SHA. |
| AC08 — escopo e segurança | PASS | Sem dependências, despesas, cloud, credenciais, gameplay, bypass, alteração de ADR ou promoção. Runtime Pipeline segue desativado. |

## Execuções locais e observações

- Unity CLI existente: 1.0.1. Unity Pipeline existente: 1.0.1. Editor: 6000.3.29f1. Nenhuma instalação ou ativação foi feita nesta continuação.
- O proprietário confirmou a licença Unity Personal ativa. O Editor conectou como ready pela rota local autorizada, sem erro de licença. Não foram lidos arquivos de credenciais ou logs brutos.
- A compilação do candidato concluiu sem erro. O EditMode final reportou 10 aprovados, 0 falhas e 0 skips; quatro testes de localização cobrem locales, paridade/expansão, estado indisponível e fallback. O exemplo do pacote Addressables é reportado separadamente e não é contado como teste de produto.
- PlayMode reportou 2 aprovados, 0 falhas e 0 skips: MissingStartupDocumentRemainsUnavailableAcrossDisableAndLocaleChanges e StartupSceneLocalizesAndAcceptsInputFocusAndExpansion.
- O checkout isolado limpo não possuía Library no início da importação registrada. A primeira importação levou 251 s; recompilação retornou up_to_date e os testes foram repetidos sequencialmente. Este ciclo usou uma instância do Editor por vez. A reconferência feita ao retomar encontrou uma instância conectada ao clone limpo e a fechou normalmente pelo CLI, sem cenas não salvas; o status final foi zero instâncias.
- A inspeção manual no Editor observou estado inglês inicial, clique de mouse para Português e Tab + Enter de volta ao Inglês. O indicador textual mudou junto com o locale e o contorno branco do foco permaneceu distinto da borda de seleção. O código de teste solicitou capturas em Temp/IM02/CD-01; a pasta e os PNGs não foram materializados nos checkouts. Nenhuma imagem inexistente é apresentada como evidência. AC04 é sustentado pelas medidas/assertions de layout e pelo smoke real de UI registrado nesta execução.
- O status da consola amostrado na reconexão reportou zero erros de compilação e zero consoleErrors; havia um warning agregado. O detalhe não foi buscado, evitando expor logs. O resultado de testes não registrou falhas ou skips inexplicados.
- O clone de execução preserva, fora do stage, ProjectSettings/SceneTemplateSettings.json gerado pelo Editor. O arquivo foi mantido intacto; não foi incluído no commit.

## Integridade, dependências e CI

O diff de implementação entre o HEAD de entrada ce9fba2 e 9ec7ca5 contém sete arquivos, todos no controller de apresentação, USS, três tabelas de localização e dois testes de apresentação. Packages/manifest.json, packages-lock.json, Assets/ZeroTrace/Domain, StableIdentifierTests.cs, ADR-009, ADR-010 e docs/Checkpoint.md não diferem. A validação local de documentação retornou DOCS_VALIDATION_OK. git diff --check não apontou whitespace inválido.

Runtime Pipeline permaneceu desativado; nenhuma define de release foi adicionada. ADR-009 conserva QA-01 como gate para CI Unity em cloud; neste WO não houve runner, serviço cloud ou player build. O PR #27 permanece draft/open e sem merge. Antes da publicação, 3/3 checks estavam verdes no HEAD remoto anterior; esse estado não substitui a confirmação dos checks no novo HEAD publicado. O resultado exato pós-push será registrado na descrição da PR e consultado diretamente na PR #27.

## Limitações e decisão

Não foram preservadas imagens PNG no bundle. Em compensação, os testes medem o espaço visual do root nos dois tamanhos, o aumento representativo de texto e as condições de clipping/overlap; o smoke manual confirmou teclado, mouse, indicação de idioma e foco. Não há alegação de testes físicos em dispositivo, cobertura total de acessibilidade, build Windows ou CI Unity cloud.

Resultado: READY FOR INDEPENDENT AUDIT após a confirmação de CI no HEAD de evidência. ADR-009/010 permanecem canônicas. Sem merge ou promoção de checkpoint.
