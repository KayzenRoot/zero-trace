# ZT-D01-WO-001-IM-01/CD-02 — validação parcial de 25/11/2026

**Resultado: BLOCKED — validação licenciada e correções Unity F02–F04 não executadas.**

## Identificação e limites

- Repositório: `KayzenRoot/zero-trace`; PR #25 (aberta, draft); branch `wo/zt-d01-wo-001-im-01`.
- Base `main`: `a907ae071aad6cec7f2b1bca0b111d7272527601`.
- HEAD remoto e local examinado: `8d7f5a27a93d3c3248b593f511d144b955fe79dc`.
- Escopo: continuação de IM-01/CD-02, sem alteração da fundação Unity, Context Lock, ADR-009 ou checkpoint.
- Esta evidência registra somente verificações determinísticas de Git, documentação e arquivos versionados. Nenhum comando Unity, instalação, atualização, serviço, login, licença, gasto ou automação de interface foi executado.

## Source Check fresco

`git fetch --prune origin` confirmou `origin/main` em `a907ae071aad6cec7f2b1bca0b111d7272527601` e `origin/wo/zt-d01-wo-001-im-01` em `8d7f5a27a93d3c3248b593f511d144b955fe79dc`. O checkout estava limpo antes desta evidência.

- Mapa `base_git_blobs` do Context Lock: **28/28** blobs conferem com a base.
- Mapa `plan_git_blobs`: **30/30** blobs conferem com o snapshot `6d92d1c06c846ec997c53e44fefeb75abe397ff7`.
- Fingerprints brutos atuais contra o snapshot: **29/30** conferem. A única diferença é `.gitattributes`, blob planejado `7b8bf1bfbdf82fd8a33ff6bde32ce2aa37557035`, blob atual `77aed40d5ff7c519099149676d2591e04f4ae85f`. O diff é exclusivamente a exceção já documentada no CD-02 para whitespace de YAML serializado em `Assets/**/*.asset`, `Assets/**/*.meta` e `ProjectSettings/**/*.asset`; nenhum atributo foi estendido ao código-fonte. `git diff --check origin/main...HEAD` passou.
- SHA-256 das fontes originais: GDD `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1` e catálogo `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365`: **2/2** iguais ao lock e aos bytes atuais.
- Context Lock: blob `1662754980fe92f7ebaad51dada80c86778c3c10`, preservado. ADR-009: blob `f58bfed3ae22b96014e06a6c2f926c1b5ddb86c4`, preservado. `docs/Checkpoint.md`: blob `b54e3fbdc4e0e67bc007c08103587f767e2f4060`, preservado.
- O HEAD `8d7f5a2` tinha 3/3 checks concluídos com sucesso antes desta publicação: `docs-validation` ([run 45679124894](https://github.com/KayzenRoot/zero-trace/actions/runs/45679124894)), [Socket Security: Project Report](https://socket.dev/dashboard/org/nexlabs/sbom/1738d0e0-7b15-4a21-bb24-675ba1e6a8d1) e [Socket Security: Pull Request Alerts](https://socket.dev). Estes são checks do HEAD examinado, não de um HEAD futuro.

## Inventário seguro da sessão

No instante da consulta, `Get-Process -Name Unity` e `Get-Process -Name UnityHub` retornaram **0 processos** cada. Nenhum Editor foi aberto nesta retomada; portanto não houve concorrência de instâncias durante as verificações feitas. Esse inventário não comprova como as janelas anteriores foram fechadas e não é apresentado como prova de fechamento normal.

O inventário de ferramentas desta sessão não expôs uma integração Unity designada. O CD-02 requer operação do proprietário na interface oficial Hub/Editor quando essa integração não está disponível; não usei automação genérica de GUI, CLI Unity, modo headless ou Build Server. Consequentemente, a licença Personal não foi lida/confirmada nesta sessão e nenhuma ação dependente do Editor foi simulada.

## Verificações determinísticas do projeto

- `ProjectSettings/ProjectVersion.txt`: Editor declarado `6000.3.29f1`, revisão `96d2fe17529c`.
- `Packages/manifest.json`: 36 dependências diretas; `Packages/packages-lock.json`: 54 entradas. **36/36** versões diretas coincidem; nenhuma dependência direta falta no lock e não há nó de dependência ausente. SHA-256 do manifest `8be240714b36b1a362441ed54a4be4daf081d3591f2b5e311d3514936c75b1b5`; SHA-256 do lock `f06ed742e478675f65ff74895068fdc72579ea76624101b9eb5142ac7383fcab`.
- Integridade estática de `Assets/`: 9 arquivos não-meta e 14 `.meta`; nenhum arquivo não-meta está sem `.meta`, e não foram encontrados GUIDs repetidos. Nenhum caminho rastreado corresponde às pastas geradas Library, Temp, Obj, Logs, UserSettings ou Builds.
- A referência de Input Settings GUID `289c1b55c9541489481df5cc06664110` existe em `ProjectSettings/EditorBuildSettings.asset`; não há ocorrência do GUID em asset-meta versionado nem dependência direta `com.unity.inputsystem` no manifest. Isto **não** substitui a resolução pelo AssetDatabase; F02 permanece sem conclusão.
- F03 não foi corrigido: `submitAnalytics: 1`; o identificador `Standalone` permanece `com.DefaultCompany.3D-Project`; `scriptingBackend` não declara override para Standalone. Os valores efetivos, o estado do alvo Windows/Mono e a alteração por Player Settings precisam ser verificados na interface oficial.
- A integridade sintática e relacional do manifest/lock foi aprovada estaticamente; o Unity Package Manager não foi executado nesta sessão. A revisão de necessidade dos módulos builtin ainda não foi realizada; F04 permanece pendente.

## Validação documental

- `py -3 .github/scripts/validate_docs.py` — **exit 0**, `DOCS_VALIDATION_OK`. A ferramenta também emitiu a observação histórica `D00 historical lock; subsequent WOs require their own fresh execution lock`; este Work Order usa seu próprio Context Lock, verificado acima.
- `git diff --check origin/main...HEAD` — **exit 0** no HEAD examinado.
- `git check-attr whitespace` confirmou a exceção apenas nos padrões YAML listados; `.cs` permanece sem exceção.
- `git status --porcelain=v1` estava vazio antes de criar este arquivo.

## AC01–AC08 nesta retomada

| Critério | Estado | Evidência / limite |
| --- | --- | --- |
| AC01 — fontes e Context Lock | PASS com delta autorizado declarado | 28/28 base, 30/30 plano, 29/30 fingerprints brutos atuais (único delta `.gitattributes`), 2/2 SHA-256; lock, ADR-009 e checkpoint preservados. |
| AC02 — licença e invocação oficial | BLOCKED | Nenhuma leitura da licença Personal na UI; sem processos Unity ativos e sem integração Unity designada nesta sessão. |
| AC03 — configuração/fundação | BLOCKED para CD-02 | Versão serializada confirmada; F02/F03 não foram validados/corrigidos via interface nem por AssetDatabase. |
| AC04 — pacotes | PASS estático / resolução não reexecutada | 36/36 versões, 54 entradas e grafo consistente; Package Manager não foi executado. |
| AC05 — compilação e testes EditMode | BLOCKED; NOT RUN nesta retomada | Compilação e os cinco testes StableIdentifier não foram executados. O resultado técnico histórico de 5/5 com cinco Editors simultâneos continua não qualificado como prova licenciada. |
| AC06 — reimportação e checks | BLOCKED parcial | Reimportação limpa sequencial não executada; validação documental e `git diff --check` passaram. Checks remotos 3/3 referem-se ao HEAD examinado anterior à publicação. |
| AC07 — evidência e checkpoint | PARCIAL; não promovido | Este registro é complementar. A proposta ADR-001 e o Checkpoint Delta existentes permanecem históricos e não são promovidos. |
| AC08 — escopo e segurança | PASS para esta retomada documental | Sem instalação, custo, login, Editor, pacote alterado, gameplay, cloud, bypass, exclusão, merge ou IM-02. A conclusão geral CD-02 permanece bloqueada. |

## Ação para remover o bloqueio

O proprietário deve operar o Hub/Editor oficial manualmente, com uma única instância ativa por vez, e fornecer resultados sanitizados para: estado Personal ativo; resolução do GUID F02 pelo AssetDatabase; correções suportadas de F03; revisão de necessidade dos módulos F04; compilação e cinco testes EditMode; fechamento normal e contagem de processos; repetição sequencial em checkout limpo com os mesmos hashes de entrada. Não compartilhar tokens, dados da conta ou logs brutos. Após receber esses resultados, a continuidade deve permanecer nesta PR e neste CD-02.

**Conclusão:** BLOCKED. F02/F03/F04 e a validação licenciada continuam pendentes. Nenhuma alegação de compilação, teste ou reimportação foi feita nesta retomada.
