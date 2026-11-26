# ZT-D01-WO-001-QA-01 | BG01 — leitura do projeto Unity Cloud

- Data da observação: 2026-11-26 (BRT)
- Escopo: somente leitura do projeto existente e das páginas de plano, uso e custo disponíveis no Unity Dashboard oficial.
- Repositório: KayzenRoot/zero-trace
- Issue / PR: #28 / #28 (PR aberta, em draft)
- Branch: wo/zt-d01-wo-001-qa-01-exec
- Base main e Context Lock: 1b72c6773fc9edd39c44239ec127a75d7ca9d158
- HEAD lido antes desta evidência: cd0f4ea2f8e1259b222317c7ac30dd0621407c1e
- Decisões preservadas: ZT-ADR-009 e ZT-ADR-010; bootstrap restrito conforme ZT-ADR-011.
- Evidência: valores e estados textuais lidos nas páginas oficiais indicadas abaixo. Este registro não inclui IDs de conta/projeto, identificadores pessoais, credenciais ou logs brutos. Nenhuma captura de tela é anexada.

## Veredito

**BG01 PASS para existência e leitura do projeto; QA-01 permanece BLOCKED.** O painel mostrou o projeto ZERO TRACE na organização existente e uma única entrada de projeto. A leitura não ativou serviços nem criou configurações. A lista filtrada por produtos ativos ficou vazia; a página de Build Automation não tinha conexão de repositório, target ou builds. Isso comprova o bootstrap/read-back delimitado, sem qualificar entitlement UBA, licença, custo futuro ou execução de CI.

O painel de Build Automation exibiu um selo “Free tier”, mas também uma mensagem genérica sobre limite de free trial. Como essa interface não esclareceu se a rota da conta é entitlement Free sem trial, e não houve revisão/aceite de termos nesta tarefa, AC02 continua bloqueado. Os valores de uso/custo são uma leitura pontual e o próprio painel alerta para atraso de até 24 horas; não são garantia de ausência de cobrança futura.

## Source Check e governança

| Verificação | Resultado |
|---|---|
| main remoto e base/planning snapshot do Context Lock | 1b72c6773fc9edd39c44239ec127a75d7ca9d158; iguais |
| HEAD remoto da PR e HEAD lido | cd0f4ea2f8e1259b222317c7ac30dd0621407c1e; iguais |
| Blobs do projeto no main | 122/122; sem divergências |
| Fingerprints críticos no main | 24/24; sem divergências |
| SHA-256 GDD MASTER e Mission Catalog | 2/2; iguais aos valores do Context Lock |
| Deltas documentais autorizados do Context Lock | 7/7; iguais no HEAD lido |
| Packages/manifest.json e Packages/packages-lock.json | blobs iguais aos de main |
| Repositório e PR #28 | público; PR aberta, draft, branch autorizada |
| Ruleset ZT-GOV-main | ID 28038825; ativo; bypass vazio; docs-validation obrigatório |
| Checks no HEAD lido, anteriores à publicação deste arquivo | docs-validation, Socket Security: Project Report e Socket Security: Pull Request Alerts — 3/3 SUCCESS |

O diff da PR antes desta adição continha somente documentação; não alterava Assets, Packages ou ProjectSettings. O arquivo desta evidência também é documental. O checkpoint canônico e o Context Lock não foram alterados nesta execução.

## Leitura do Unity Dashboard

### Projeto e produtos

- A Home/Projects da organização existente exibiu ZERO TRACE; a lista apresentou uma única entrada de projeto.
- Project Settings mostrou o nome ZERO TRACE, a declaração COPPA correspondente à resposta do proprietário (“não é principalmente direcionado a menores de 13 anos”), e a configuração padrão de coleta de dados do desenvolvedor como Essential + Recommended. Essa configuração foi apenas lida, não alterada; este registro não conclui que toda telemetria esteja desativada.
- Google Analytics não estava vinculado.
- Na página Products, o filtro Status: Active exibiu “No products available”.
- Em Build Automation Overview, a interface mostrou o selo Free tier, texto genérico sobre notificação ao aproximar-se do limite de free trial e zero builds concluídos/falhos, sem dados de build.
- Configurations indicava que o source control ainda não estava configurado; Target setup/Quick target setup estavam indisponíveis. Build History mostrou “No build available yet” e “no build target configurations”.
- Nenhum projeto foi criado nesta leitura. Não houve ativação de UBA ou outro produto, conexão com GitHub, criação de target, configuração de auto-build, execução/upload de build ou alteração de pacote. Nenhuma credencial ou webhook foi criada ou alterada nesta execução; não foi consultada uma página de inventário independente de credenciais/webhooks.

### Plano, uso, custo e cobrança

A página de Usage da organização identificou a seção Monthly free tier e mostrou os valores abaixo. O rótulo visível para Windows foi “Windows minutes”; a tela não identificou o builder Micro:

| Item exibido | Limite no painel | Uso exibido |
|---|---:|---:|
| Storage | 25 GB | 0 |
| Egress | 100 GB | 0 |
| Windows minutes | 200 min | 0 |
| Mac minutes | 100 min | 0 |
| Linux minutes | 100 min | 0 |

A leitura do uso é da organização e não prova elegibilidade UBA sob Unity Personal, nem que os minutos Windows sejam Micro. Não havia cap operacional efetivo demonstrado. Budget Alerts indicava “No budgets” e explicava que alertas de orçamento não desativam serviços e que serviços podem continuar sendo faturados às taxas aplicáveis.

Cost and Usage Reporting mostrou US$ 0,00 nos últimos sete dias e US$ 0,00 no ciclo de novembro de 2026, sem dados de detalhamento. A interface adverte que a atualização pode levar até 24 horas, que os custos são estimativas e que a fatura prevalece. O projeto havia sido criado recentemente em relação a essa leitura; portanto, o zero e a ausência de detalhes não provam custo final zero.

Payment Methods mostrou nenhum método de pagamento disponível. Subscriptions não apresentou resultados. São leituras atuais do painel, não garantias sobre cobranças futuras ou sobre a natureza da rota Free/trial.

## Critérios QA-01

| Critério | Estado nesta execução | Evidência / limite |
|---|---|---|
| BG01 — bootstrap vazio | PASS, limitado ao read-back visível | Projeto e organização exibidos; nenhum produto ativo, source control, target ou build observado; nenhuma ativação feita nesta execução. A tela não ofereceu inventário independente de credenciais/webhooks. |
| AC01 — Source Check e Context Lock | PASS | 122/122 blobs, 24/24 fingerprints, 2/2 SHA-256, 7/7 deltas, pacotes iguais a main e ruleset ativo sem bypass. |
| AC02 — plano, elegibilidade e termos | BLOCKED | Selo Free tier coexistiu com aviso genérico de free trial; UBA com Personal e termos aplicáveis não foram confirmados. Nenhum termo foi aceito. |
| AC03 — quota e segurança de custo | BLOCKED | Uso atual 0 e limites visíveis; cap efetivo não comprovado, não havia budgets e os alertas não interrompem serviços; relatório tem atraso declarado. |
| AC04 — configuração confiável | BLOCKED | Windows Micro e Editor 6000.3.29f1 não foram confirmados no seletor; source control e target não configurados. |
| AC05 — testes cloud | NOT RUN | Sem serviço/target autorizado e sem build. |
| AC06 — artefato Windows | NOT RUN | Nenhum build foi configurado ou executado. |
| AC07 — smoke do player e idiomas | NOT RUN | Não existe artefato QA desta execução. |
| AC08 — guarda de release | NOT RUN | Nenhum artefato foi produzido/inspecionado; Runtime local não foi revalidado nesta leitura. |
| AC09 — CI e governança | PARTIAL | Os checks do GitHub e ruleset no HEAD lido passaram; nenhum status UBA em commit confiável foi produzido. Ruleset não foi alterado. |
| AC10 — evidência e decisão | PARTIAL | Este Evidence Bundle e proposta de delta estão publicados; auditoria independente e aprovação permanecem pendentes. |

## Segurança, custo e reversão

- A sessão limitou-se à navegação e filtros de leitura no Unity Dashboard oficial.
- Não houve aceite de termos, trial, ativação de produto, mudança de billing, método de pagamento, credencial, webhook, configuração de build, build/teste cloud ou artefato.
- Não foram abertos arquivos de credenciais nem coletados, copiados ou registrados tokens, IDs de conta/projeto, dados pessoais ou logs brutos.
- Não foi aberto o Editor, não houve execução local Unity, instalação, despesa, serviço cloud, mudança em pacote/projeto, merge ou promoção do checkpoint.
- Validação local desta evidência antes do commit: python .github/scripts/validate_docs.py — DOCS_VALIDATION_OK (exit 0); git diff --check — PASS; varredura sanitizada de padrões de segredos — PASS.
- Os dados de custo têm o atraso informado pelo Unity; a interface de orçamento não impõe corte. Zero uso observado não elimina risco de cobrança se serviços forem ativados futuramente.
- A evidência pode ser revertida por commit de reversão normal. O projeto Unity Cloud existente não deve ser removido automaticamente.

## Próxima ação necessária

Antes de qualquer configuração/build, obter uma confirmação inequívoca, na interface oficial ou suporte Unity, de que a conta possui entitlement UBA compatível com Personal, plano Free sem trial/Pay-as-you-go, termos aplicáveis e builder Windows Micro com Editor 6000.3.29f1. Em seguida, a operação de custo precisa de limites efetivamente controláveis/read-back; avisos informativos não bastam. Qualquer ativação/configuração exige autorização separada compatível com o Work Order. Sem esses gates, manter QA-01 bloqueado e não executar builds.

## Checkpoint Delta proposto — NÃO PROMOVIDO

- Base main: 1b72c6773fc9edd39c44239ec127a75d7ca9d158.
- HEAD de leitura: cd0f4ea2f8e1259b222317c7ac30dd0621407c1e.
- BG01: registrar como PASS limitado ao projeto existente e ao read-back visível no Dashboard.
- AC01: Source Check fresco PASS.
- AC02–AC04: BLOCKED pelos motivos acima; AC05–AC08 não executados; AC09 e AC10 parciais.
- ADR-009 permanece NOT QUALIFIED; ADR-010 permanece decisão de desenvolvimento local. ZT-ADR-011 não foi expandida.
- D01 continua NOT COMPLETE; D02/release permanecem bloqueados.
- Não altera docs/Checkpoint.md e não promove estado canônico.

Estado: **EVIDÊNCIA PROPOSTA / DELTA NÃO PROMOVIDO / QA-01 BLOCKED**. Validação local documental e de integridade passou; checks exatos do HEAD de publicação serão lidos após o push e registrados na atualização da PR.
