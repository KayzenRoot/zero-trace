# ZT-D01-WO-001-PF-01/CD-02 — complemento de instalação e CI

Data: 2026-11-25, America/Sao_Paulo
Base main: 152d9c12ce65fb19c7daba8442e3c0e77f2822db
HEAD de entrada auditado: 364b10325c1b145587892024398bd016b52c274f
Branch: docs/zt-d01-wo-001-pf-01
PR #23: aberta, draft, sem merge; issue #22: aberta
Lock original: blob 7538a0c234d5ba8e8fca4acd84399d173bb9924c, preservado

**Resultado do complemento: instalação local do Editor concluída; PF-01 continua BLOCKED por AC04 e AC05.** Esta evidência complementa CD-02-execution.md e a autorização CD-02-authorization.md. O SHA final que contém este arquivo e seus checks exatos serão registrados na descrição da mesma PR.

## AC01 — Source Check e governança: PASS

Fetch e conferência imediatamente antes desta alteração: origin/main permaneceu em 152d9c12ce65fb19c7daba8442e3c0e77f2822db; HEAD local/remoto da branch permaneceu em 364b10325c1b145587892024398bd016b52c274f; worktree limpo; PR #23 aberta/draft na branch autorizada e issue #22 aberta.

- Lock blob 7538a0c234d5ba8e8fca4acd84399d173bb9924c, sem alteração.
- Base: 25/25 blobs; planning snapshot 17357330cef527192952d73ef53a67b974b3d947: 27/27; bytes raw atuais dos 27 blobs: 27/27.
- GDD e catálogo: SHA-256 raw 2/2 iguais ao lock; atributos EOL LF.
- Ruleset ZT-GOV-main, ID 28038825, ativo, sem bypass, escopo main. Cinco proteções permanecem: exclusão, non-fast-forward, histórico linear, pull request e status obrigatório; docs-validation estrito.
- Checks do HEAD de entrada 364b10325c1b145587892024398bd016b52c274f: docs-validation passou (workflow run 45579644865); Socket Security: Project Report passou (check run 139086158815); Socket Security: Pull Request Alerts passou (check run 139086191511). O check do commit desta evidência será registrado na descrição da PR após o push. O check D00 do base, documentado anteriormente, permanece run 45531364841.

## AC02 — Ambiente e armazenamento: PASS com inventário herdado

SO rechecado: Windows 11 Pro, build 26300, 64 bits. CPU rechecada: AMD Ryzen 3 4300GE, arquitetura x64. RAM 15,1 GiB e GPU RTX 5050 permanecem do inventário CD-01; VRAM exata e driver gráfico não foram confirmados nesta execução.

Unity 6.3 requer Windows 10 21H1 ou posterior, CPU x64 com SSE2 e GPU com DX10/DX11/DX12/Vulkan. O SO atual atende ao requisito de versão; o Editor foi instalado e o payload Windows Mono foi verificado abaixo. Não foi feito teste de gameplay nem de driver/gráficos.

Espaço antes da instalação: C: 18.318.688.256 bytes livres; D: 579.326.844.928 bytes. Durante o staging, o menor valor observado em C: foi aproximadamente 5,95 GiB; após a conclusão, C: ficou com 18.255.310.848 bytes livres. D: terminou com 570.829.144.064 bytes livres; o diretório autocontido do Editor contém 8.401.606.864 bytes. Não houve alteração de TEMP, exclusão manual, limpeza ou reparticionamento.

## AC03 — Unity, Editor, suporte Windows e pacotes: PASS local do Editor; resolução de pacotes NOT RUN

A Unity CLI 1.0.1 previamente instalada foi reutilizada, não reinstalada. Verificação repetida: 45.356.456 bytes, SHA-256 c06c2e86941162e8d46957d39a6089162ad52a00023433881b5e9a18ea220d17 (igual ao hash original), Authenticode Valid, signatário Unity Technologies SF; unity --version retornou 1.0.1, exit 0.

O dry-run oficial para 6000.3.29f1 indicou Windows x86_64, download de 4.205.154.408 bytes e instalação de 8.401.586.572 bytes. O destino D: era inexistente antes da execução. A tentativa de usar o alias opcional windows-mono no dry-run informou que o módulo não estava disponível e selecionou zero módulos adicionais; nenhum módulo opcional foi forçado.

Após a aprovação manual do proprietário no prompt UAC, o comando oficial unity install 6000.3.29f1 --download-directory <destino autocontido em D> --format json concluiu com exit 0: success=true, UID 6000.3.29f1-x86_64, status installed. Não foram passados --yes, --accept-eula, --no-elevate ou --force. O usuário aprovou somente a elevação. Não apareceu solicitação de termos, login ou ativação; nenhuma dessas ações foi realizada.

Verificação pós-instalação, sem iniciar o Editor:
- Editor/Unity.exe existe em destino autocontido em D:. File version 6000.3.29.9884414; Authenticode Valid, signatário Unity Technologies SF.
- Editor/Data/PlaybackEngines/WindowsStandaloneSupport existe.
- Variações win64_player_development_mono e win64_player_nondevelopment_mono presentes.
- Tamanho observado do diretório: 8.401.606.864 bytes.
- A instalação por --download-directory é autocontida e não entra na lista de Editors/Hub. Por isso unity editors --installed retornou lista vazia (exit 0) e install-modules --list não encontrou um Editor registrado (exit 6); o caminho, versão, assinatura e suporte Windows foram verificados diretamente nos arquivos instalados. O Editor não foi aberto.

Compatibilidade documental dos candidatos, sem instalar nem resolver dependências:
- URP, com.unity.render-pipelines.universal: documentação Unity 6.3 / URP 17.3; candidato, não resolvido.
- UI Toolkit: integrado ao core do Unity 6; nenhuma dependência adicional instalada nesta fase. Compatibilidade funcional do projeto permanece para IM-01.
- Localization, com.unity.localization 1.5.14: versão documentada oficialmente; candidato, não instalado/resolvido.
- Unity Test Framework, com.unity.test-framework 1.4.6: versão documentada oficialmente; candidato, não instalado/resolvido.
- Packages/manifest.json, packages-lock.json, resolução do Package Manager, build e testes Unity: NOT RUN/deferidos a IM-01.

Fontes oficiais consultadas em 2026-11-25: release 6000.3.29f1 (https://unity.com/releases/editor/whats-new/6000.3.29f1); suporte/requisitos Windows 6000.3 (https://docs.unity.com/en-us/engine/6000.3/manual/platform-specific/windows/requirements-and-compatibility); CLI e opção de instalação autocontida (https://docs.unity.com/en-us/unity-cli/reference/editor-project-commands; https://docs.unity.com/en-us/unity-cli/release-notes/2026/october); URP 17.3/Unity 6.3 (https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.3/manual/index.html); Localization 1.5.14 (https://docs.unity3d.com/Packages/com.unity.localization@1.5/manual/index.html); Test Framework 1.4.6 (https://docs.unity3d.com/Packages/com.unity.test-framework@1.4/manual/index.html); UI Toolkit no Unity 6 (https://docs.unity3d.com/jp/current/Manual/best-practice-guides/ui-toolkit-for-advanced-unity-developers/install-ui-toolkit-and-sample-projects.html).

## AC04 — Licença Personal e elegibilidade: BLOCKED

Não houve login Unity, aceite de termos, ativação, criação de conta, leitura de licença/token ou consulta de dados financeiros. A licença ativa e a elegibilidade do proprietário continuam não comprovadas. Ação necessária sob controle do proprietário: abrir a interface oficial, analisar os termos vigentes, confirmar elegibilidade como yes/no/unknown sem divulgar valores e ativar Personal se decidir fazê-lo. Nenhum valor ou credencial deve ser compartilhado.

## AC05 — CI Unity gratuito e licenciado: BLOCKED

A tabela pública da Unity lista 200 minutos Windows Micro, 100 Linux Micro e 100 Mac Standard mensais; o limite público não prova entitlement, disponibilidade regional ou habilitação para esta conta. A documentação do DevOps alerta que exceder a quota bloqueia o projeto e pode exigir upgrade para continuar. Nenhum projeto DevOps foi criado ou ativado.

Os termos vigentes do Unity Editor (atualizados em 2026-11-16) incluem builds, testes e tarefas de agentes entre os processos automatizados do Unity Build Server; exigem licença/tier Build Server correspondente, proíbem Unity Personal com Build Server e condicionam o uso aos encargos aplicáveis. Portanto, os minutos públicos do DevOps não demonstram por si só uma rota de CI Unity que seja simultaneamente gratuita e licenciada para esta conta. GitHub Actions docs-validation não concede licença Unity nem valida build/test Unity.

Fontes oficiais: preço/quota pública (https://unity.com/products; https://docs.unity.com/en-us/devops/pricing/free-plan); termos Unity Editor, §§1.1 e 2.5 (https://unity.com/legal/editor-terms-of-service/software); entrada no DevOps (https://docs.unity.com/en-us/devops/get-started-end-users).

Ação necessária: o proprietário deve verificar em leitura na conta já existente seu tier/eligibilidade, entitlement de Build Server e Build Automation, quota Windows suportada e se há cobertura licenciada sem billing ou overage pago. Não iniciar sessão, aceitar termos, ativar serviço, adicionar cobrança, runner, segredo ou integração nesta execução. Se a conta depender de Personal/Build Server ou de qualquer cobrança, AC05 permanece BLOCKED.

## AC06 — Validação documental e checks

Validação local: `python .github/scripts/validate_docs.py` — exit 0; saída `D00 historical lock; subsequent WOs require their own fresh execution lock` e `DOCS_VALIDATION_OK: 420 original briefs; 356 sequential prerequisites; release matrix; 20-node canonical DAG; 30 apps; 3 static SVGs; proposed governance; lock checks`. `git diff --cached --check` — exit 0, sem erros no diff staged. Checks remotos do SHA que contém esta evidência serão registrados na descrição da PR #23 após o push. Unity EditMode, PlayMode, build Windows e locale smoke: NOT RUN; nenhum projeto existe.

## AC07 — ADR-001 e Checkpoint Delta: propostas não ratificadas/não promovidas

ADR-001 — PROPOSTA: manter Unity 6.3 LTS / 6000.3.29f1 x86_64 em instalação autocontida D: com Windows Mono local verificado; não adicionar IL2CPP nem outros módulos sem necessidade aprovada. URP 17.3, UI Toolkit core, Localization 1.5.14 e Test Framework 1.4.6 continuam candidatos documentais, sem resolução. Não aprovar a rota CI enquanto tier, entitlement e ausência de custo não forem comprovados pelo proprietário. Esta proposta não ratifica arquitetura nem licença.

Checkpoint Delta — PROPOSTA NÃO PROMOVIDA: reconhecer AC03 localmente resolvido; manter PF-01 BLOCKED por AC04/AC05; preservar docs/Checkpoint.md, Context Lock, PR draft e issue aberta. Nenhum avanço para IM-01 antes de evidência do proprietário e auditoria independente.

## AC08 — Escopo, privacidade e riscos: PASS com gates explícitos

Única alteração no repositório prevista: este documento de evidência; nenhum arquivo de projeto, código, Assets, Packages, workflow, regra, fonte canônica, lock ou checkpoint foi modificado. Sem despesas, assinatura, licença ativada por executor, runner, serviço Cloud, segredo, bypass, merge, implementação Unity, promoção de checkpoint ou IM-01.

Os comandos desta execução usaram variáveis transitórias de supressão de telemetria/crash/update. A primeira chamada Unity CLI anterior a este complemento já está registrada em CD-02-execution.md como feita antes da supressão; transmissão não foi verificada independentemente. Dados locais UnityHub preexistentes permanecem; não houve exclusão. Nenhuma credencial, arquivo de licença, valor financeiro, nome de usuário ou caminho absoluto foi incluído.

## Recomendação

**BLOCKED, aguardando ações do proprietário em AC04/AC05.** O Editor oficial e suporte Windows x64 Mono foram instalados e verificados após aprovação UAC. Publique este complemento na PR #23 e pare: a conta/licença e a rota CI gratuita ainda não estão comprovadas; não avançar para IM-01.
