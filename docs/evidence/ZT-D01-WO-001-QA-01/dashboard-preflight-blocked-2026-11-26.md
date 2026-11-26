# QA-01 — preflight da Unity Cloud e bloqueio controlado

- Data: 2026-11-26
- Work Order: ZT-D01-WO-001-QA-01
- Issue/PR: #28
- Branch: wo/zt-d01-wo-001-qa-01-exec
- Base main: 1b72c6773fc9edd39c44239ec127a75d7ca9d158
- Context Lock e snapshot de planejamento: 1b72c6773fc9edd39c44239ec127a75d7ca9d158
- HEAD de entrada: 510ef68c5f43fcea05904063c676e7944b840ed4

## Veredito

**SOURCE CHECK PASS; QA-01 BLOCKED antes de criar projeto Unity Cloud ou ativar Unity Build Automation.** O painel autenticado não expõe uma franquia DevOps Free, entitlement UBA, quotas mensais efetivas, uso da franquia, limite configurável, builder Windows Micro ou seletor de versão do Editor enquanto não houver projeto Unity Cloud. O painel mostra que não há projeto. A documentação pública da Unity não substitui o read-back exigido para a conta.

Nenhum projeto/serviço cloud foi criado ou ativado. Nenhum termo foi aceito, método de pagamento adicionado, credencial/webhook criado, build executado ou cobrança iniciada. Auto-build permaneceu desabilitado. Nenhum arquivo de projeto Unity foi alterado.

## Source Check fresco e governança

| Verificação | Resultado |
|---|---|
| main atual e base da PR | 1b72c6773fc9edd39c44239ec127a75d7ca9d158 |
| HEAD remoto da branch | 510ef68c5f43fcea05904063c676e7944b840ed4 |
| 122 blobs Unity em main | 122/122; sem divergências |
| Arquivos Unity rastreados em main | 122 |
| Fingerprints críticos do Context Lock | 24/24; sem divergências |
| Snapshot de planejamento do projeto | 122/122 blobs e 24/24 fingerprints |
| SHA-256 GDD MASTER v0.2.0 | 4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1; confere |
| SHA-256 Mission Catalog v0.2.0 | 836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365; confere |
| Manifesto e lock de pacotes | JSON válido; hashes iguais ao Context Lock |
| Pacotes canônicos | URP 17.3.0, Localization 1.5.14, Pipeline 1.0.1; Test Framework builtin 1.6.0 |
| Editor no projeto | 6000.3.29f1; revision 96d2fe17529c |
| Repositório | Público; default branch main |
| PR #28 / issue #28 | Abertas; PR em draft; HEAD remoto corresponde à branch autorizada |
| Checks observados no HEAD de entrada | docs-validation, Socket Project Report e Socket PR Alerts: 3/3 pass |
| Ruleset efetivo | ZT-GOV-main, ID 28038825, active, escopo main, zero bypass |
| Proteções lidas no ruleset | bloqueio de exclusão e non-fast-forward; histórico linear; PR com resolução de threads; squash-only; docs-validation estrito obrigatório |

O checkout original de IM-02 foi preservado, incluindo o arquivo local não rastreado ProjectSettings/SceneTemplateSettings.json. A evidência foi preparada em worktree isolado, inicialmente no SHA remoto autorizado.

## Read-back da Unity

Foi usada somente a sessão existente do Unity Dashboard, sem ler cookies, tokens ou arquivos de credenciais. A organização existente kayzenweb3dev estava selecionada e a interface identificou o usuário conectado como Primary Owner; identificadores pessoais e IDs internos foram omitidos desta evidência.

O Dashboard mostrou “Create your first project”; portanto, não havia projeto Unity Cloud para abrir. A tela de criação foi fechada sem enviar o formulário. O proprietário respondeu que ZERO TRACE não é principalmente direcionado a menores de 13 anos. Essa resposta não foi usada para declarar elegibilidade de serviço nem para criar o projeto.

No painel Cost and Usage Reporting:

- no intervalo dos últimos sete dias, total spend exibido: US$ 0.00;
- no ciclo de faturamento de novembro de 2026, a tabela não apresentou dados;
- o próprio painel informa que dados podem demorar até 24 horas.

Em Payment Methods, a interface exibiu “No payment method available” e informou que não há serviços Unity Gaming Services ativos que exijam método de pagamento. Isso é um read-back de cobrança atual, mas não prova franquia gratuita, quotas disponíveis, nem o custo futuro de qualquer configuração.

No Unity Hub 4.0.2, a lista local apresentou Unity 6.3 / Editor 6000.3.29f1 instalado; o inventário de aplicativos não apresentou processo/janela do Editor em execução. A ativação/licença local não foi reaberta nem reativada nesta sessão. O status Personal foi anteriormente informado pelo proprietário e registrado nos documentos canônicos, mas o painel de DevOps não confirmou elegibilidade UBA sob essa licença.

## Fontes públicas oficiais e limite da evidência

A página pública de preços anuncia, por organização/mês, 200 minutos Windows Micro, 100 Linux Micro, 100 Mac Standard, 25 GB de storage compartilhado e 100 GB de egress. Esses números são somente referência pública; a conta não mostrou os limites ou uso efetivos. A documentação alerta que ultrapassar limites pode bloquear o projeto, exigir upgrade para continuar e expor storage à exclusão após 30 dias; os avisos são enviados em 50%, 75% e 90%. Nenhuma margem publicada foi tratada como quota disponível da organização.

A Unity documenta que UBA pode executar testes EditMode e PlayMode e falhar o build quando algum teste falhar. A documentação pública lista 6000.3 LTS como Supported e diz que versões oficiais costumam ser adicionadas em até cinco dias úteis. O patch 6000.3.29f1 e o builder Windows Micro não foram lidos no seletor da conta; sua disponibilidade efetiva continua não confirmada.

A documentação de conexão Git diz que repositórios públicos podem conectar sem credenciais. A documentação de configuração permite selecionar branch, versão do Unity, OS/builder, cache e agendamento. Nenhuma configuração foi criada; não se presume status/check nativo de GitHub.

Fontes consultadas:

- https://unity.com/products/pricing-updates
- https://docs.unity.com/en-us/devops/pricing/free-plan
- https://docs.unity.com/en-us/devops/pricing/cost-and-usage-reporting
- https://docs.unity.com/en-us/build-automation/reference/unit-tests
- https://docs.unity.com/en-us/build-automation/reference/supported-unity-versions
- https://docs.unity.com/en-us/build-automation/reference/supported-platforms-on-each-builder-os
- https://docs.unity.com/en-us/build-automation/optimize-build-speed/choose-machine-specification
- https://docs.unity.com/en-us/build-automation/basic-build-configuration/overview
- https://docs.unity.com/en-us/build-automation/get-started-with-build-automation/connect-your-version-control-system
- https://unity.com/legal/editor-terms-of-service/software
- https://unity.com/legal/terms-of-service

Os termos foram consultados como fonte pública; nenhum novo termo ou consentimento foi aceito.

## AC01–AC10

| Critério | Estado | Evidência / bloqueio |
|---|---|---|
| AC01 — Source Check e Context Lock | PASS | Base, branch, 122/122 blobs, 24/24 fingerprints, dois SHA-256, manifesto/lock, issue/PR e ruleset conferidos. |
| AC02 — conta, entitlement e termos | BLOCKED | Organização existente e proprietário observados; pagamento não disponível; Personal consta como declaração anterior do proprietário. Plano DevOps explicitamente Free e entitlement UBA sob Personal não são mostrados. Nenhum termo foi aceito. |
| AC03 — quota e custo seguro | BLOCKED | Apenas US$ 0.00 nos últimos sete dias; ciclo atual sem dados e com aviso de atraso até 24 h. Quotas/uso efetivos e cap operacional não foram apresentados; não é seguro iniciar build. |
| AC04 — alvo confiável UBA | BLOCKED | Versão 6000.3 LTS consta suportada em fonte pública, mas 6000.3.29f1, Windows Micro, Windows x64/Mono, branch main, cache limpo e opções do alvo não foram lidos na conta. |
| AC05 — testes cloud | NOT RUN / BLOCKED | Não existe configuração UBA; nenhum teste em nuvem executado. |
| AC06 — artefato Windows | NOT RUN / BLOCKED | Nenhum build iniciado; sem artifact ou hash. |
| AC07 — smoke de idioma/player | NOT RUN / BLOCKED | Sem artefato para download privado ou validação do player. |
| AC08 — guardas de release e segurança | PARTIAL | Defines rastreados estão vazios; flags locais de Runtime não foram verificadas nesta sessão e não há artifact para inspecionar. |
| AC09 — status CI e governança | BLOCKED | Ruleset e checks GitHub existentes conferidos. Não há build/status UBA no commit; nenhuma integração ou bridge foi criada. |
| AC10 — Evidence Bundle e decisão | PARTIAL | Este registro e o Checkpoint Delta proposto foram publicados; auditoria independente, aprovação e promoção permanecem pendentes. |

## Ação necessária para desbloqueio

O bloqueio é específico: a interface da organização sem projeto não mostra o plano DevOps/UBA, quotas e consumo do ciclo, cap efetivo, builder Windows Micro ou a versão exata 6000.3.29f1. Para continuar sem risco de custo, a Unity ou a conta deve expor esses valores para a organização sem trial, pagamento, cartão, upgrade ou aceite novo. Se a leitura depender de criar um projeto ou ativar serviço, será necessário um ajuste governado do gate antes dessa operação; não foi inferida autorização para contornar o preflight. Qualquer necessidade de token, webhook ou permissão de escrita também exige uma autorização específica antes de criar credencial.

Até esse read-back, não criar projeto/serviço, configuração, credencial, webhook, build ou checkout de artifact. Manter auto-build desligado e QA-01/D01 bloqueados. ADR-009, ADR-010 e o checkpoint canônico permanecem intactos.
