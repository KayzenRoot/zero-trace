# ZT-D01-WO-001-QA-01/CD-01 — Free entitlement, licenciamento e custo

- Data da execução: 2026-11-26 (BRT)
- Repositório: KayzenRoot/zero-trace
- Issue/PR: #28 / #28; PR permanece aberta em draft
- Branch: wo/zt-d01-wo-001-qa-01-exec
- Base main: 1b72c6773fc9edd39c44239ec127a75d7ca9d158
- HEAD de entrada auditado: 511606e22890072e2153d7e130a2613493a10460
- HEAD final da publicação: SHA exato registrado na descrição da PR #28 e nos checks consultados para o HEAD publicado. O arquivo não embute o hash do próprio commit.
- Context Lock original: docs/locks/ZT-D01-WO-001-QA-01.json, preservado sem alteração; escopo desta correção está em docs/locks/ZT-D01-WO-001-QA-01-CD-01.json.
- ADR-009, ADR-010 e ADR-011 preservadas.

## Resumo e veredito

**CD-01: investigação documental concluída com bloqueios identificados. ADR-012: PROPOSED, não aprovada. QA-01: BLOCKED. D01: incompleto.**

As fontes oficiais explicam que a tela usa “Start free trial” para iniciar a rota que descrevem como Free plan, e também descrevem uma franquia recorrente sem checkout inicial. Isso esclarece a terminologia pública, mas não classifica a rota desta conta, não identifica se existe período de avaliação/conversão, nem prova elegibilidade UBA sob Unity Personal. A documentação do cap mensal de minutos se aplica a uma assinatura ativa; timeout individual, concorrência e retenção de artefatos não impõem o limite mensal cumulativo interno.

O canal oficial de suporte foi localizado, mas o formulário UBA exige endereço de e-mail, URL/IDs do projeto e organização, e caixas obrigatórias de concordância com os Termos e reconhecimento da Política de Privacidade. Como o Work Order não autoriza nova aceitação de termos ou o envio desses identificadores, nada foi preenchido ou enviado. Não havia ação de salvar rascunho visível; uma minuta genérica e sem identificadores está registrada abaixo.

## Source Check e Context Lock

| Verificação | Resultado |
|---|---|
| Main, base do lock, HEAD local e remoto da branch | 1b72c677…; 511606e…; coincidiam no início da execução |
| Unity project blobs no Context Lock | 122/122 iguais a main |
| Fingerprints críticos | 24/24 iguais a main |
| GDD MASTER e Mission Catalog | 2/2 SHA-256 iguais aos hashes do Context Lock |
| Deltas documentais pré-existentes autorizados | 7/7 blobs iguais ao Context Lock |
| Packages/manifest.json e packages-lock.json | blobs iguais a main |
| PR e issue | PR #28 aberta/draft; issue #28 aberta |
| Ruleset ZT-GOV-main | ID 28038825, ativo, bypass vazio, docs-validation obrigatório, squash-only |
| CI no HEAD de entrada | docs-validation e os dois checks Socket: 3/3 SUCCESS |
| Context Lock original | blob e SHA-256 conferidos; preservado sem escrita |

O estado inicial da worktree estava limpo. O delta CD-01 altera apenas quatro arquivos documentais listados abaixo. Não altera Assets/, Packages/, ProjectSettings/, a cena Unity, código, dependências, ADR-009/010/011, o Context Lock original ou docs/Checkpoint.md. Os demais documentos já presentes no diff histórico da PR foram preservados.

## Read-back do Unity Dashboard

A leitura atual foi limitada à página Products do projeto existente: o filtro de produtos ativos mostrou “No products available”. Não houve alteração de conta. A leitura anterior registrada em BG01 continua sendo o único read-back de plano/uso/custo: ela contém rótulo Free tier, texto genérico de free trial e limites organizacionais, mas não identifica entitlement recorrente para UBA, qualifica o builder Micro ou comprova limites efetivos. Os valores privados de uso e custo não são reproduzidos aqui, e a própria leitura anterior indicava possível atraso dos relatórios.

Não houve ativação, aceite de termos, inscrição em Free/trial, mudança de faturamento, método de pagamento, integração GitHub, target, auto-build, credencial, webhook, build, teste, artefato ou implantação. Nenhuma nova página de credenciais foi aberta.

## Matriz de fontes oficiais

Fontes consultadas em 2026-11-26; datas de atualização abaixo são as exibidas pelas páginas quando disponíveis.

| Fonte oficial | Evidência documental | Aplicabilidade / limite |
|---|---|---|
| [Get started • DevOps](https://docs.unity.com/en-us/devops/get-started-end-users), atualização indicada: 6 meses | “Start free trial to use the free plan” e, em separado, “Subscribe” para pay-as-you-go. | Confirma ambiguidade de nomenclatura; não revela a rota desta conta nem seus termos. |
| [Free plan • DevOps](https://docs.unity.com/en-us/devops/pricing/free-plan), atualização indicada: 5 meses | Uso Free; ao exceder limites, bloqueio do projeto; sem upgrade, acesso somente leitura e possível exclusão de dados após 30 dias; avisos em 50/75/90%. | É uma regra pública, não um corte configurado para nossos limites menores. |
| [Unity pricing changes](https://unity.com/products/pricing-updates), alterações efetivas desde 2026-03-01 | Publica 200 min Windows Micro, 100 min Mac Standard, 100 min Linux Micro, 25 GB storage e 100 GB egress por organização/mês; diz que continuar após limites requer adicionar cartão no checkout PAYG. | Referência pública; não prova entitlement, saldo, ausência de conversão ou controles desta conta. |
| [Unity Terms of Service](https://unity.com/legal/terms-of-service), atualização 2026-11-16 | Termos gerais incluem uso gratuito/de avaliação e admitem conversão automática quando o Offering Identification aplicável prevê isso. | Não identifica o Offering Identification ou rota apresentados a esta conta. |
| [Unity Cloud Additional Terms](https://unity.com/legal/cloud-terms-of-service), atualização 2026-06-30 | DevOps/UBA são Unity Cloud Offerings; o uso exige aceitar termos aplicáveis. | A aceitação aplicável à conta não foi verificada. O formulário de suporte exigia nova concordância. |
| [Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software), atualização 2026-11-16 | §2.5 regula Unity Build Server e sua assinatura; o limite Personal para Build Server é separado. | Não é prova positiva nem negativa de elegibilidade da UBA gerenciada com Personal. |
| [Free-plan build failures](https://docs.unity.com/en-us/build-automation/check-build-results/troubleshoot-build-failures/builds-failing-immediately), atualização indicada: 3 meses | Limite mensal de build é verificado como cap de assinatura ativa; serviço Free excedido pode cancelar/bloquear builds. | Não prova que o cap esteja disponível no Free. |
| [Usage and cost reporting](https://docs.unity.com/en-us/devops/pricing/cost-and-usage-reporting) | Relatório e painel mostram uso/custo do período, com observações de atualização/estimativa conforme BG01. | Monitoramento atrasado é informativo, não guarda preventiva. |
| [Choose a machine specification](https://docs.unity.com/en-us/build-automation/optimize-build-speed/choose-machine-specification), atualização indicada: 9 meses | Windows Micro público: 8 vCPU, 16 GB RAM, sem GPU e disco de 512 GB. | Existência pública não prova que apareça no seletor da conta. |
| [Supported platforms per builder OS](https://docs.unity.com/en-us/build-automation/reference/supported-platforms-on-each-builder-os), atualização indicada: 24 dias | Windows 11 24H2 lista Windows Mono como plataforma suportada. | Compatibilidade pública não demonstra target/account availability ou x64 exato neste projeto. |
| [Supported Unity versions](https://docs.unity.com/en-us/build-automation/reference/supported-unity-versions), atualização indicada: 23 dias | A família 6000.3 LTS aparece como Supported. | Não lista especificamente 6000.3.29f1; patch exato precisa aparecer no seletor do serviço. |
| [Configure a build](https://docs.unity.com/en-us/build-automation/basic-build-configuration/overview), atualização indicada: 10 dias | Target permite escolher branch, versão do Editor, SO, cache incluindo No cache, automação e agendamento. | São configurações futuras que exigiriam ativação/target, não feitas nesta execução. |
| [Unit tests • Build Automation](https://docs.unity.com/en-us/build-automation/reference/unit-tests), atualização indicada: 1 ano | Suporta EditMode e PlayMode, opção de falhar o build em teste falho e relatórios. | Prova capacidade documentada, não execução ou resultado neste projeto. |
| [Connect version control](https://docs.unity.com/en-us/build-automation/get-started-with-build-automation/connect-your-version-control-system) | Repositório GitHub público pode ser conectado sem token; auto-build requer permissão ampliada de webhook. | Nenhuma conexão foi criada; status/check de commit é questão distinta. |
| [UBA project integrations](https://docs.unity.com/en-us/build-automation/build-automation-integrations/project-integrations), atualização indicada: 5 meses | Integrações enviam notificações a ferramentas de colaboração e webhooks; a página não documenta publicação de check nativo do GitHub. | Ausência na documentação revisada significa “não comprovado”, não impossibilidade técnica. |
| [UBA API v2](https://docs.unity.com/en-us/oas-build-automation-client/2.0.0) | API documenta disparo/consulta/cancelamento de builds, commit/branch e timeout por build; timeout 0 usa padrão de 12 horas. | Não documenta status de volta ao GitHub nem limite mensal cumulativo; nenhuma API/credencial foi usada. |
| [Manage concurrent builds](https://docs.unity.com/en-us/build-automation/manage-consumption-costs), atualização indicada: 3 meses | Limite de concorrência enfileira execuções adicionais e torna consumo mais previsível. | Não impede consumo acumulado acima dos limites mensais internos. |
| [Build results](https://docs.unity.com/en-us/build-automation/check-build-results/overview) | “Billable Build Time” conta tempo a partir do início do build. | Cancelar não é demonstrado como desfazendo minutos já contabilizados. |
| [Artifact retention](https://docs.unity.com/en-us/build-automation/manage-retention-rules), atualização indicada: 3 meses | Regras de retenção são avaliadas de hora em hora; por padrão o projeto não tem regras próprias. | Retenção não estabelece teto rígido de armazenamento/egress nem foi configurada. |

### Free versus trial

As páginas públicas descrevem Free plan e apontam o botão “Start free trial” para essa rota; a página de pricing diz que Free não exige checkout inicial e que PAYG requer ação de checkout/cartão para continuar após esgotar limites. O texto geral dos Termos, porém, permite conversão automática se as condições da oferta aplicável assim disserem. Portanto, o rótulo do botão não comprova por si só um trial pago, mas também não resolve as condições específicas da conta. A conclusão segura é **terminologia pública parcialmente esclarecida; classificação account-specific bloqueada**.

### Licenciamento

UBA gerenciada e Unity Build Server não são tratados como a mesma oferta nas fontes. A restrição Personal a Build Server não deve ser generalizada para UBA; tampouco a existência de um plano Free prova a elegibilidade UBA desta conta Unity Personal. A rota e os termos específicos continuam não comprovados.

## Matriz de custo e limites operacionais

| Controle | Resultado | Por que não fecha o gate |
|---|---|---|
| Franquia pública Free | 200 Windows Micro min, 25 GB storage, 100 GB egress/mês; demais quotas na matriz acima | Maior que o envelope interno e não determina saldo/account entitlement. |
| Envelope interno do Work Order | 100 Windows Micro min, 12.5 GB compartilhado, 25 GB egress, custo adicional US$0 | Mantido sem alteração; não encontrei mecanismo efetivo que imponha os limites cumulativos menores. |
| Alertas de orçamento | Informativos; alertas não desligam serviço | Não são corte rígido. |
| Cap mensal de build | Documentação consultada vincula o cap a assinatura ativa | Disponibilidade/eficácia no Free não comprovada; não ativada. |
| Concorrência | Limita trabalhos paralelos e enfileira o excedente | Reduz concorrência, não soma mensal. |
| Timeout | Corta uma execução individual; padrão público de 12 horas se não configurado | Por-build, começa no checkout; não limita total mensal nem foi configurado. |
| Cancelamento | API permite cancelar build em progresso | Tempo já iniciado é billable; cancelamento não é mecanismo garantido de zero consumo. |
| Retenção | Apaga artefatos por idade/quantidade com avaliação horária | Não limita ingestão de artefato/armazenamento antes da avaliação nem egress de download. |
| Relatório de custo | Pode ter atraso/estimativa, conforme painel e BG01 | Não é prevenção e pode não refletir atividade recente. |
| Sem cartão/checkout | O documento público diz que exceder Free sem meio de pagamento bloqueia o projeto; continuar requer checkout PAYG | Bloqueio não substitui o envelope interno nem protege disponibilidade/dados; não é autorização para adicionar pagamento. |

**Conclusão AC03:** BLOCKED. Não iniciar build se não existir cap/controle agregado eficaz e read-back utilizável no Free. Alertas, baixa concorrência, monitoramento manual e cancelamento não suprem esse requisito.

## Windows Micro, Editor e integração GitHub

- **Builder público:** Windows Micro existe tecnicamente. O limite público é referido como Windows Micro. Não foi possível verificar disponibilidade na conta porque UBA permaneceu sem ativação.
- **Target/backend:** Windows 11 24H2 documenta Windows Mono. Não foi possível qualificar o seletor real de Windows Standalone x64 com Mono.
- **Editor:** documentação lista 6000.3 LTS, mas não confirma o patch 6000.3.29f1 neste serviço/conta. Não substituí versão.
- **No-cache:** há opção documentada no target, mas não foi configurada.
- **Testes:** suporte documental a EditMode/PlayMode e falha por teste; não houve teste cloud.
- **GitHub confiável:** repositório é público, então documentação prevê leitura sem token. Configuração de source control, branch main restrita, gatilho manual, auto-build desligado e exclusão de forks/PRs são gates para uma execução futura.
- **Status GitHub:** fontes consultadas mostram notificações/webhooks e API para acionar/acompanhar UBA. Não encontrei prova oficial de um check/commit status nativo do GitHub associado ao SHA construído. Uma ponte exigiria credencial/permissão adicional e revisão independente; não foi projetada ou implementada.

## Suporte Unity oficial

- Canal oficial localizado: Unity Help Center → Contact Support → Products and services → DevOps → Unity Build Automation.
- O formulário exige email de contato, URL UBA do projeto, Unity Project ID, Unity Organization ID, aceite obrigatório dos Unity Terms of Service e reconhecimento obrigatório da Privacy Policy.
- O Work Order proíbe novo aceite de termos e exige parar se o canal precisar de identificadores ou informação além do escopo necessário. Parei antes de preencher qualquer campo, marcar caixas ou enviar.
- Não havia botão de salvar rascunho visível. Nenhum ticket foi submetido; não existe número de caso, resposta ou data de envio. Esta execução não confirma se há ticket anterior.
- Minuta local, genérica e sem IDs/email:

> Subject: Unity Build Automation Free entitlement and zero-cost qualification under Unity Personal
> Hello Unity Support,
> We are qualifying a no-cost Windows QA/CI workflow for an existing Unity Cloud project. Public onboarding documentation uses both “Start free trial to use the free plan” and “Free plan” terminology. Please clarify whether Free is a recurring entitlement or time-limited trial, whether automatic paid conversion is possible, whether managed UBA is permitted for eligible Unity Personal users, whether Windows Micro includes the published monthly allocation, whether exact Editor 6000.3.29f1 is available for Windows Mono, what enforceable cumulative usage controls exist on Free, whether both EditMode and PlayMode tests can fail a build, and whether a native GitHub status can be associated with the exact commit. We will not activate a service, add payment, or create a build target before these gates are resolved.
> Thank you.

Esta minuta não foi transmitida. Para qualquer envio futuro, o proprietário precisa revisar os termos exigidos e autorizar separadamente o compartilhamento dos campos de contato e identificadores mínimos solicitados pelo formulário.

## Critérios do CD-01

| Critério | Estado | Evidência / limite |
|---|---|---|
| CD01-AC01 — Source Check e Context Lock | PASS | 122/122 blobs, 24/24 fingerprints, 2/2 SHA-256, 7/7 deltas; lock original intocado. |
| CD01-AC02 — Metadados da PR reconciliados | PASS após publicação | Corpo da PR substituído para refletir BG01 limitado, HEAD final, blockers AC02–AC04, ADR-012 proposta e ausência de ativação/build/promoção. |
| CD01-AC03 — Terminologia Free/trial | PASS documental limitado | Fontes explicam a frase de onboarding e Free plan; conversão e oferta desta conta ainda incertas. |
| CD01-AC04 — Entitlement account-specific | BLOCKED | Rótulo Free e read-back anterior não provam inscrição UBA recorrente nem Personal eligibility. |
| CD01-AC05 — Licença/termos | BLOCKED | Build Server é distinto; UBA/Personal e termos aplicáveis a esta conta não estão comprovados; suporte exigia aceite obrigatório. |
| CD01-AC06 — Quota/controles de custo | BLOCKED | Sem corte agregado efetivo para o envelope interno no Free; controles públicos não bastam. |
| CD01-AC07 — Windows Micro/Editor | PARTIAL | Micro, Windows Mono e família 6000.3 LTS constam em fontes; seletor account-specific e patch 6000.3.29f1 não comprovados. |
| CD01-AC08 — Integração GitHub | PARTIAL | A leitura pública pode dispensar token; publicação de check GitHub exato não comprovada. |
| CD01-AC09 — Suporte gratuito | BLOCKED / minuta preparada | Portal oficial consultado; envio exigiria acordo e dados não autorizados. Sem ticket/resposta. |
| CD01-AC10 — ADR-012 | PASS como proposta | Publicada com status PROPOSED, não aprovada e sem autorização de ativação. |
| CD01-AC11 — Sem mutação/custo não autorizado | PASS | Sem enrollment, aceite, ativação, alteração de billing, credencial, build ou serviço. |
| CD01-AC12 — Evidência/commit/push/CI exato | PASS após publicação | Bundle publicado na mesma PR; descrição e leitura final registram o HEAD publicado e seus checks. |

## Critérios originais QA-01

| Critério | Estado após CD-01 |
|---|---|
| BG01 — Projeto vazio e read-back limitado | PASS conforme evidência histórica BG01; não ampliado nesta execução |
| AC01 — Source Check/Context Lock | PASS |
| AC02 — Plano/entitlement/licença/termos | BLOCKED |
| AC03 — Limites reais e guarda de custo | BLOCKED |
| AC04 — Windows Micro/Editor/executor confiável | BLOCKED |
| AC05 — Testes EditMode/PlayMode em cloud | NOT RUN |
| AC06 — Artefato Windows x64 | NOT RUN |
| AC07 — Smoke en-US/pt-BR no player | NOT RUN |
| AC08 — Release/security guard no artefato | NOT RUN |
| AC09 — Status UBA no GitHub e governança | BLOCKED; checks documentais da PR não são check UBA |
| AC10 — Evidência/decisão/aprovação | PARTIAL; proposta e bundle publicados; auditoria independente ainda necessária |

## Arquivos do delta, validação e checks

Arquivos adicionados/alterados por CD-01:

- docs/adr/ZT-ADR-012.md
- docs/Decisions.md
- docs/locks/ZT-D01-WO-001-QA-01-CD-01.json
- docs/evidence/ZT-D01-WO-001-QA-01/CD-01-free-entitlement-qualification.md

Validações locais executadas (2026-11-26): `python .github/scripts/validate_docs.py` — `DOCS_VALIDATION_OK` (exit 0); parse JSON dos locks CD-01, manifest e package lock — PASS; Source Check fresco após atualização das referências remotas — base `1b72c677…`, branch/HEAD de entrada `511606e…`, 122/122 blobs Unity, 24/24 fingerprints críticos, 2/2 SHA-256 canônicos e 7/7 deltas prévios; `Packages/manifest.json` e `packages-lock.json` iguais ao HEAD/base; Git blob e SHA-256 do Context Lock original iguais aos valores registrados; governança lida de volta — ruleset ativo, sem bypass e com `docs-validation` obrigatório; diff validado, restrito aos quatro caminhos autorizados, `git diff --cached --check` e `git diff --check` — PASS; varredura sanitizada de tokens, email e UUID nos quatro arquivos — PASS; seis blobs preservados iguais ao HEAD de entrada. Não houve mudança rastreada em `Assets/`, `Packages/` ou `ProjectSettings/`. Não foram executados Unity builds, testes Unity, instalação, automação headless ou testes cloud. O SHA final e os checks exatos serão registrados nos metadados da PR após o push.

Os checks obrigatórios e de segurança do GitHub devem ser lidos no HEAD final exato, depois do push; a PR #28 continua em draft e não é elegível para merge por causa dos gates QA-01 bloqueados.

## Segurança, riscos, rollback e próxima ação

- **Risco de custo:** sem limite agregado efetivo para o envelope interno; uso excedente pode bloquear o projeto e afetar a disponibilidade/dados.
- **Risco jurídico/entitlement:** termos gerais permitem cenários de conversão quando a oferta aplicável o prevê, mas a oferta/account-specific continua desconhecida.
- **Risco de disponibilidade:** janela de atualização do relatório e cap/timeout não cobrem uso cumulativo.
- **Risco de CI:** status nativo UBA→GitHub por SHA não comprovado; uma ponte exigiria nova credencial e acesso.
- **Rollback:** somente documental, por commit normal que reverta estes quatro arquivos; nenhuma mudança Unity Cloud precisa ser revertida.

Próxima ação necessária: obter confirmação oficial da oferta Free específica, elegibilidade UBA sob Personal e limites/caps efetivos, sem ativar o serviço. O suporte pode ser consultado pelo proprietário somente após revisão/aceite dos termos exigidos e autorização explícita para enviar email e IDs mínimos; alternativamente, registrar resposta oficial já existente sem esses dados no repositório. Depois, submeter ADR-012 à auditoria independente. Uma eventual ativação continuará exigindo autorização separada.

## Checkpoint Delta proposto — NÃO PROMOVIDO

- Reconhecer BG01 apenas no limite do read-back já publicado.
- Manter QA-01 BLOCKED por AC02–AC04; AC05–AC08 sem execução; AC09 não qualificado; AC10 parcial até auditoria independente.
- ADR-012 é somente proposta. ADR-009 continua preservada; ADR-010 e ADR-011 não foram alteradas.
- D01 segue NOT COMPLETE; D02 e release continuam bloqueados.
- Não editar docs/Checkpoint.md, não promover checkpoint, não mesclar PR #28.

Estado final proposto: **CD-01 documentado; ADR-012 PROPOSED; QA-01 BLOCKED; D01 NOT COMPLETE; zero ativação e zero custo adicional.**
