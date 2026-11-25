# ZT-D01-WO-001-PF-01/CD-02 — Evidence Bundle — execução 2026-11-25

**Resultado: BLOCKED.** O Source Check fresco passou e a Unity CLI oficial foi instalada e verificada. A instalação do Editor 6000.3.29f1 parou antes do download com ELEVATION_CANCELLED (exit 6). Editor/módulo Windows não comprovados; licença/elegibilidade e rota Unity CI sem custo seguem bloqueadas. PR #23 permanece draft e issue #22 aberta.

Base 152d9c12ce65fb19c7daba8442e3c0e77f2822db; snapshot de planejamento 17357330cef527192952d73ef53a67b974b3d947; HEAD remoto de entrada d896b6e419951a8cb5c94af3a76efd87988df064; branch docs/zt-d01-wo-001-pf-01; PR #23 aberta/draft/sem merge; issue #22 aberta. Data: 2026-11-25, America/Sao_Paulo.

## AC01 — Context Lock e governança: PASS

Source Check executado em clone isolado limpo, sem reset:

- git fetch origin main docs/zt-d01-wo-001-pf-01 — exit 0; origin/main permaneceu em 152d9c12ce65fb19c7daba8442e3c0e77f2822db e branch remota em d896b6e419951a8cb5c94af3a76efd87988df064.
- Lock blob 7538a0c234d5ba8e8fca4acd84399d173bb9924c, igual ao esperado. base_git_blobs 25/25, plan_git_blobs 27/27 e blobs raw do checkout por git hash-object --no-filters 27/27.
- SHA-256 raw: GDD 4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1; catálogo 836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365. Ambos conferem com o lock. Esta evidência usa os hashes corretos; fontes e bundles históricos não foram reescritos.
- git check-attr eol retornou LF nas duas fontes. Checkout original preservado limpo em aa25f417e4b1ddb03c67a90544deea8e3a59fa45; Context Lock nele mantém o blob 7538a0c234d5ba8e8fca4acd84399d173bb9924c.
- Autenticação GitHub existente identificou KayzenRoot; PR/issue abertas e branch/base/HEAD correspondem ao escopo. Nenhum token foi emitido ou registrado.
- Ruleset ZT-GOV-main, ID 28038825: active, refs/heads/main, bypass_actors=[], docs-validation estrito. Read-back mantém cinco regras: sem exclusão/force-push, histórico linear, PR squash-only com resolução de conversas e status obrigatório.
- No HEAD de entrada d896b6e…, docs-validation run 45541000061 success e os dois checks Socket pass (3/3). D00 run 45531364841 no base exato também success.

## AC02 — Inventário e armazenamento: PASS com espaço revalidado

SO/CPU/RAM/GPU e versões de ferramentas permanecem conforme CD-01, sem repetir coleta de hardware. C: foi revalidado em 8,96 GiB livres e D: em 544,01 GiB antes da tentativa. Depois do cancelamento, C: permaneceu em 8,96 GiB e D: em 544,01 GiB. A Unity CLI é versão 1.0.1. A sondagem de Hub/Editor cobriu Appx, Start Menu, PATH, processos, registros e diretórios padrão; não cobre locais personalizados não informados.

## AC03 — Unity, Editor, módulos Windows e pacotes: BLOCKED

### Unity CLI

O instalador PowerShell Unity foi inspecionado, mas não executado: ele altera PATH do usuário, grava dados em %APPDATA% e pode configurar skills/agentes. Para evitar essas mudanças permanentes, foi usado o binário standalone do CDN oficial em diretório autocontido no volume D:, sem modificar PATH ou Git.

- Manifesto oficial: https://public-cdn.cloud.unity3d.com/hub/prod/cli/latest.json; versão 1.0.1; Windows x64; 45.356.456 bytes.
- SHA-256 esperado/observado: c06c2e86941162e8d46957d39a6089162ad52a00023433881b5e9a18ea220d17 (match). Authenticode Valid, signatário Unity Technologies SF.
- unity --version: 1.0.1, exit 0.

### Editor e módulos

unity install 6000.3.29f1 --dry-run --download-directory <destino-autocontido-em-D> --format json retornou exit 0: Windows x86_64, Editor download 4.205.154.408 bytes e tamanho instalado 8.401.586.572 bytes na API Unity; módulos extras selecionados: zero. A API informa release LTS publicada em 2026-11-19; Unity 6.3 LTS é suportada até dezembro de 2027.

A tentativa real foi somente unity install 6000.3.29f1 --download-directory <destino-autocontido-em-D> --format json, sem --yes, --accept-eula, login, ativação ou --no-elevate. Resultado: ELEVATION_CANCELLED — The Windows elevation prompt was cancelled or timed out. (exit 6). A pasta destino não apareceu, o espaço em C:/D: não mudou e nenhum processo Unity permaneceu ativo. Não houve download/instalação do Editor ou de módulos. Não foi tentada outra forma nem contorno do prompt.

Os metadados oficiais Windows x64 de 6000.3.29f1 não listam windows-mono como módulo instalável separado; listam Windows IL2CPP e Dedicated Server como opcionais. A página da release lista Windows Build Support (Mono) entre componentes, mas sem Editor instalado não é possível confirmar como o suporte está empacotado nesta máquina. Nenhum módulo é declarado instalado.

| Candidato | Compatibilidade documental de CD-01 | Estado CD-02 |
|---|---|---|
| URP com.unity.render-pipelines.universal | Core package documentado para Unity 6.3; versão vinculada ao Editor | Não instalado/resolvido |
| UI Toolkit / com.unity.modules.uielements | Runtime UI documentada em Unity 6.3; suporte built-in candidato | Não comprovado localmente |
| Localization com.unity.localization 1.5.14 | Release documentada para Unity 6000.3 | Não instalado/resolvido |
| Unity Test Framework com.unity.test-framework | Core package documentado para Unity 6.3; versão vinculada ao Editor | Não instalado/resolvido |

Fontes oficiais consultadas em 2026-11-25: [suporte Unity 6](https://unity.com/releases/unity-6/support), [release 6000.3.29f1](https://unity.com/releases/editor/whats-new/6000.3.29f1), [Unity Releases API](https://docs.unity.com/en-us/oas-release/1.0.0), [Unity CLI](https://docs.unity.com/en-us/unity-cli/use-unity-cli), [privacidade da CLI](https://docs.unity.com/en-us/unity-cli/data-collection-privacy-controls), [CLI release notes](https://docs.unity.com/en-us/unity-cli/release-notes/2026/october). Detalhes de compatibilidade/fontes dos pacotes permanecem em CD-01. Nenhum projeto, manifest/lock de pacote, build ou teste Unity foi criado/executado.

## AC04 — Licença e elegibilidade: BLOCKED

Licença ativa não comprovada: Hub/Editor ausentes e interface de licença não aberta. Elegibilidade do proprietário permanece unknown. Não houve login, aceite de termos, ativação, criação de conta, leitura de arquivo de licença/token ou consulta de dados financeiros.

Os termos atuais medem elegibilidade pelo Total Finances nos 12 meses móveis; Unity Personal é até US$ 200.000, com definições distintas por tipo de usuário/serviço. Não inferimos elegibilidade. Fonte: [Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software).

Ação do proprietário: após resolver a elevação, abrir a UI oficial de licença, confirmar elegibilidade como yes/no/unknown e aceitar pessoalmente os termos aplicáveis, se necessário; sem compartilhar valores, credenciais ou arquivos.

## AC05 — Viabilidade de CI Unity gratuito: BLOCKED

A página pública Unity DevOps mostra Free Plan com 200 minutos Windows Micro/mês, 100 Linux Micro e 100 Mac Standard. Ao exceder limite grátis, DevOps bloqueia o projeto em leitura; para continuar exige upgrade. Isso não comprova disponibilidade ou entitlement desta conta.

Os termos Unity Editor atualizados em 2026-11-16 definem Unity Build Server para automação headless de builds, testes e tarefas de agentes; exigem tier correspondente e proíbem Unity Personal com Build Server. Unity CLI é interface designada para agentes, sujeita aos demais termos, mas não concede licença de build nem entitlement CI. Sem prova owner-controlled da conta, não há rota zero-custo licenciada demonstrada.

GitHub Actions para docs segue verde, mas runner de documentação não concede licença Unity nem prova build/test Unity. Nenhum projeto Cloud/UBA foi ativado; sem billing, upgrade, runner, secret, integração ou workflow configurado.

Fontes oficiais: [preços públicos Unity](https://unity.com/products), [limites Free Plan](https://docs.unity.com/en-us/devops/pricing/free-plan), [Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software), [Unity Terms of Service §§17.2 e 26](https://unity.com/legal/terms-of-service). Ação do proprietário: verificar oferta/entitlements em modo de leitura na conta, sem ativar serviço/billing; auditoria deve confirmar cobertura de Windows, EditMode/PlayMode/build sem custo.

## AC06 — Validação documental e checks

python .github/scripts/validate_docs.py, git diff --check e checks remotos do SHA que publica este arquivo serão registrados após execução/publicação. Nenhum resultado pendente é declarado sucesso. EditMode, PlayMode, build Windows e locale smoke: NOT RUN.

## AC07 — ADR-001 proposta; Checkpoint Delta não promovido

### ADR-001 — PROPOSTA, não ratificada

Contexto: Unity 6.3/6000.3.29f1, Windows x64, Mono, URP, UI Toolkit, Localization 1.5.14, Test Framework e Unity Build Automation Free permanecem candidatos. A CLI foi instalada; Editor parou no UAC; módulo Windows, licença ativa, elegibilidade e CI Unity sem custo não foram comprovados.

Alternativas: A) contornar UAC — proibida; B) proprietário executar instalação oficial e decidir pessoalmente prompts/licença; C) selecionar outra stack em decisão futura. Recomendação: B; manter candidatos sem ratificar. Depois, evidenciar Editor/suporte Windows e gates de licença. Não aprovar CI até comprovar entitlement sem custo. Nenhuma decisão técnica é alterada.

Limitações/reversão: dry-run registrou tamanhos e destino D:; tentativa real cancelou antes do download. CLI standalone permanece em D: e PATH não mudou. Chamadas da CLI geraram metadados locais no APPDATA; tentativas de remover apenas esses dados foram bloqueadas pela política do executor. Permanecem fora do repositório. Proprietário pode removê-los manualmente se desejar restaurar a ausência anterior. Nenhuma conta Unity foi conectada.

### Checkpoint Delta — proposta não promovida

Manter PF-01 BLOCKED por falta de Editor/módulo Windows, licença/elegibilidade e rota CI Unity licenciada sem custo. Não alterar docs/Checkpoint.md, não promover checkpoint, manter issue #22 aberta e PR #23 draft. Exigir ação do proprietário e revisão independente.

## AC08 — Escopo, privacidade e risco: PASS com limitação local declarada

Único arquivo pretendido neste commit: esta evidência. Fontes canônicas, lock, planejamento, decisões, checkpoint, ruleset, workflow e código não foram alterados. Sem login/aceite/ativação Unity, despesas, runner/segredo, projeto, build/teste, bypass, merge, promoção ou IM-01. Nenhum token, credencial, dado financeiro, nome de usuário ou caminho absoluto foi incluído.

A primeira execução unity --version ocorreu antes das variáveis transitórias de telemetria. A CLI informa contagem anônima habilitada por padrão, sem usuário/máquina/projeto/caminho segundo a documentação Unity. UNITY_NO_CLI_INVOKED_TELEMETRY=1, UNITY_NO_CRASH_REPORT=1 e UNITY_NO_UPDATE_CHECK=1 foram usadas depois; transmissão de telemetria não verificada independentemente. Nenhuma conta Unity estava autenticada. Não houve leitura de credenciais/licenças nem scanner geral de segurança.

### Comandos e resultados locais

- git fetch origin main docs/zt-d01-wo-001-pf-01 — exit 0.
- Fingerprints, git hash-object --no-filters, Get-FileHash -Algorithm SHA256 e git check-attr eol — valores/contagens em AC01.
- unity.exe --version — exit 0, versão 1.0.1.
- unity install 6000.3.29f1 --dry-run --download-directory <D> --format json — exit 0; Editor 4.205.154.408 bytes; módulos extras zero.
- unity install 6000.3.29f1 --download-directory <D> --format json — exit 6, ELEVATION_CANCELLED; sem --yes, --accept-eula ou --no-elevate.
- Validação documental e checks exatos deste commit: a registrar após executar/publicar.

**Recomendação do executor: BLOCKED.** Publique esta evidência parcial na branch/PR existentes, mantenha a PR em draft e pare. O proprietário precisa aprovar a instalação oficial no prompt UAC e tratar licença/elegibilidade; a conta Unity deve comprovar entitlement CI gratuito antes de reavaliar o gate.
