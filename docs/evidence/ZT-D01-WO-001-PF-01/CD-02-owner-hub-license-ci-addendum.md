# ZT-D01-WO-001-PF-01/CD-02 — adendo do Hub, licença e rota CI

- Data: 2026-11-25 (America/Sao_Paulo)
- Base main: `152d9c12ce65fb19c7daba8442e3c0e77f2822db`
- HEAD de entrada auditado: `7178172df1056f4c20834bebd14008a55c899e82`
- Branch: `docs/zt-d01-wo-001-pf-01`
- PR #23: aberta, draft, sem merge; issue #22: aberta
- Context Lock: blob `7538a0c234d5ba8e8fca4acd84399d173bb9924c`, preservado.

Este adendo responde à auditoria publicada em [PR #23, comentário 7161428595](https://github.com/KayzenRoot/zero-trace/pull/23#issuecomment-7161428595). Ele complementa os Evidence Bundles anteriores sem reescrever seus registros históricos. O estado final dos checks do commit que publica este arquivo será registrado na descrição da mesma PR.

**Recomendação do executor: BLOCKED em AC04 e AC05.** O Hub já estava instalado; o Editor existente foi localizado e consta no Hub; a tela de licenças mostrou uma licença Personal ativa localmente. A elegibilidade atual continua sob confirmação do proprietário. A rota de CI gratuita em nuvem permanece candidata, sem entitlement ou configuração de conta verificados.

## AC01 — Source Check e governança: PASS

Fetch e comparação feitos antes da edição. A main e o HEAD remoto da branch permaneceram, respectivamente, `152d9c12ce65fb19c7daba8442e3c0e77f2822db` e `7178172df1056f4c20834bebd14008a55c899e82`; o checkout isolado estava limpo. PR #23 está aberta em draft na branch autorizada; issue #22 está aberta.

- 25/25 blobs de base conferem com o Context Lock.
- 27/27 blobs do snapshot de planejamento conferem com `17357330cef527192952d73ef53a67b974b3d947`.
- 27/27 hashes brutos do checkout LF conferem; `git check-attr eol` retorna LF para GDD e catálogo.
- 2/2 SHA-256 das fontes conferem com o lock: GDD `4352e4a2bdf4f7d3925caaa6314ba1b550efe1c3f917ba05acd50aa940ea5ed1`; catálogo `836e305e1fb9777ba1298e76d93b8bfa97d77ebc10b7ed846fae3477e1b0b365`.
- Blob do Context Lock permanece `7538a0c234d5ba8e8fca4acd84399d173bb9924c`.
- Read-back da API do GitHub: ruleset `ZT-GOV-main`, ID `28038825`, enforcement `active`, branch alvo `main`, zero bypass actors. As cinco regras efetivas continuam: deletion, non-fast-forward, histórico linear, pull request e status obrigatório. O requisito de status permanece estrito para `docs-validation`; método de merge permitido: squash.
- O checkout original, separado deste clone LF, permaneceu limpo em `aa25f417e4b1ddb03c67a90544deea8e3a59fa45`; nenhuma configuração permanente do Git foi alterada.
- O D00 no SHA base permanece `45531364841 SUCCESS`. No HEAD de entrada exato `7178172df1056f4c20834bebd14008a55c899e82`, os check-runs foram 3/3 SUCCESS: `docs-validation`, `Socket Security: Project Report` e `Socket Security: Pull Request Alerts`.

## AC02 — Ambiente, hardware e armazenamento: PASS com VRAM não confirmada

Inventário local sanitizado, sem nome de usuário, identificador da máquina ou caminho privado:

| Item | Observação | Limite |
|---|---|---|
| Sistema | Windows 11 Pro, build 26300, 64 bits | O Editor não foi aberto; sem teste gráfico. |
| CPU | AMD Ryzen 3 4300GE, x64 | Modelo reportado pelo Windows. |
| RAM | 15,1 GiB | Valor reportado pelo Windows. |
| GPU | NVIDIA GeForce RTX 5050; driver 32.0.16.1714 | VRAM exata não confirmada; a propriedade WMI observada não é confiável para afirmar capacidade. Um adaptador de vídeo virtual também estava presente. |
| C: livre | 11.450.781.696 bytes | Medido após a instalação existente. |
| D: livre | 570.761.302.016 bytes | Medido após a instalação existente. |

O requisito oficial de Windows 10 21H1 ou posterior é atendido pelo SO observado. Isso não comprova desempenho de gameplay, driver gráfico, VRAM ou funcionamento de projeto.

## AC03 — Hub, Editor, módulos e pacotes: PASS local; versão efetiva do Test Framework não resolvida

### Hub e Editor existentes

- Unity Hub já estava instalado como pacote oficial Unity Technologies SF, versão `4.0.2.65535`; não foi reinstalado.
- A tela **Instalações** lista `Unity 6.3 LTS (6000.3.29f1)`. Em **Instalações > Localizar**, a seleção do executável existente retornou “6000.3.29f1 já está instalado”. O Editor permaneceu registrado; nenhuma segunda instalação ou reinstalação ocorreu.
- Editor em `D:`: metadado `6000.3.29f1_96d2fe17529c`; assinatura Authenticode `Valid`, Unity Technologies SF.
- `WindowsStandaloneSupport` existe, com `win64_player_development_mono` e `win64_player_nondevelopment_mono` presentes.
- Unity CLI 1.0.1 permanece instalada e assinada. Metadado de produto `1.0.1`, SHA-256 `c06c2e86941162e8d46957d39a6089162ad52a00023433881b5e9a18ea220d17`, Authenticode `Valid`, Unity Technologies SF, idêntico ao registro anterior. A execução anterior bem-sucedida `unity --version` está em `CD-02-installation-followup.md`. Neste shell, o comando não está no `PATH`; a tentativa de iniciar diretamente o binário dentro do pacote AppX retornou acesso negado. Não houve reparo ou reinstalação: a versão, assinatura e hash do binário foram confirmados por leitura.
- Hub e Editor foram abertos apenas para leitura; a janela do Editor não foi iniciada.

### Candidatos de pacote, sem instalação ou resolução

| Pacote/área | Estado atual | Evidência e limite |
|---|---|---|
| URP | Candidato documentado para Unity 6.3 / URP 17.3 | Sem dependência instalada ou resolvida. |
| UI Toolkit | Integrado ao Unity 6 | Sem teste funcional de projeto. |
| Localization | `1.5.14` documentado para 6000.3 | Candidato, não instalado/resolvido. |
| Unity Test Framework (`com.unity.test-framework`) | Pacote Core cuja versão é fixada em conjunto com a versão do Editor | A documentação 6000.3 não informa aqui uma versão independente; a versão efetiva não foi lida localmente. |

### Correção documental do Unity Test Framework

Os registros históricos CD-01 e `CD-02-installation-followup.md` mencionam, respectivamente, `1.6` e `1.4.6`. Nenhuma dessas referências demonstra a versão efetivamente incluída com este Editor. Este adendo corrige a interpretação: a fonte oficial da linha 6000.3 declara que Core packages são fixados a uma versão única correspondente à versão do Editor. Portanto, `1.4.6` é retirado como candidato independente, e `1.6` não é tratado como versão instalada. Não se afirma uma versão efetiva sem leitura de metadado local. A descoberta/resolução da versão real fica para IM-01, quando houver projeto autorizado.

Nenhum `Packages/manifest.json`, `packages-lock.json`, projeto Unity ou resolução do Package Manager foi criado. Nenhum pacote foi instalado.

## AC04 — Licença local e elegibilidade: BLOCKED parcialmente

Em **Configurações > Licenças**, o Hub exibiu uma licença `Personal` com data de ativação `17 ago. 2024`. A documentação oficial do Hub descreve essa tela como a lista de licenças ativas e a data de ativação. Isso comprova a licença Personal listada localmente no momento da observação.

A atualização manual da lista permaneceu em “Atualizando licenças...” e foi encerrada fechando a janela; portanto a atualização não foi confirmada como concluída. O estado observado não confirma, sozinho, a elegibilidade atual do proprietário nem a aceitação dos termos vigentes. Nenhum login, aceite de termos, declaração de elegibilidade ou ativação foi executado pelo agente; nenhum dado financeiro, credencial ou identificador de conta foi registrado.

**Ação do proprietário:** conferir em leitura que a licença Personal continua ativa após atualizar a lista; revisar os termos atuais e confirmar elegibilidade apenas como `yes`, `no` ou `unknown`, sem compartilhar valores. Se a elegibilidade for `no` ou `unknown`, manter AC04 bloqueado e não usar Personal no trabalho do projeto.

## AC05 — Rota CI Unity legal e gratuita: BLOCKED; candidato concreto identificado

O candidato a investigar é **Unity Build Automation (UBA) Free, serviço de build em nuvem**. A documentação oficial lista Windows Desktop como plataforma e execução integrada de testes EditMode e PlayMode. A tabela pública consultada lista 200 minutos Windows Micro por mês e concorrência incluída; a documentação Free Plan diz que ultrapassar limites bloqueia o projeto DevOps e que é necessário upgrade para continuar. Estes são limites públicos, não provam entitlement ou configurações desta conta.

A Unity distingue UBA, que executa builds em nuvem, de Unity Build Server, que atende builds on-premises. Os termos atuais do Editor, seção 2.5, descrevem o Build Server para processos headless automatizados e vedam seu uso com Unity Personal. **Inferência delimitada:** essa seção, por si só, não esclarece o licenciamento de UBA cloud; não se aplica automaticamente o bloqueio do Build Server ao serviço em nuvem, nem se presume que UBA Free esteja autorizado para esta conta.

**Resultado:** AC05 não qualificado. Não foi acessado o Unity Dashboard da conta nem lidos entitlement, tier DevOps, limite disponível, modo de billing/overage ou configuração de cobrança. Nenhum projeto DevOps, job, runner, token, integração ou configuração foi criado. Não houve build/teste. GitHub Actions continua cobrindo somente validação documental; não concede licença Unity.

**Ação necessária:** o proprietário deve conferir, em modo somente leitura na conta Unity já existente, se UBA Free está disponível e habilitado sem plano pay-as-you-go, qual a franquia Windows aplicável, o que ocorre no limite e que a configuração não gera cobrança. Confirmar também os termos que cobrem o uso do Editor no serviço cloud e a elegibilidade do tier Personal. Se os termos/entitlement da rota cloud permanecerem ambíguos, solicitar confirmação oficial da Unity; não ativar serviço, alterar billing ou fornecer credenciais nesta etapa. Manter AC05 BLOCKED até a prova específica da conta e do uso pretendido.

## AC06 — Validação local e checks remotos

### Source Check pré-edição

`git fetch origin main docs/zt-d01-wo-001-pf-01` — exit 0. Comparação automatizada de blobs e bytes raw com o lock — exit 0: 25/25 base, 27/27 planejamento, 27/27 raw, 2/2 SHA-256; zero divergências. Ruleset — read-back via API GitHub, exit 0. PR #23 e issue #22 — estado open; PR em draft.

### Validação deste adendo

- `python .github/scripts/validate_docs.py` — exit 0; `DOCS_VALIDATION_OK: 420 original briefs; 356 sequential prerequisites; release matrix; 20-node canonical DAG; 30 apps; 3 static SVGs; proposed governance; lock checks`.
- Busca de padrões de segredo/credencial neste arquivo — exit 1 e nenhuma correspondência.
- Revisão do diff: somente este arquivo de evidência; sem dados pessoais ou credenciais. `git diff --cached --check` — exit 0.
- No HEAD de entrada exato `7178172df1056f4c20834bebd14008a55c899e82`, os check-runs foram 3/3 SUCCESS. Os checks do HEAD que publica este arquivo serão conferidos por SHA exato e registrados na descrição da PR #23 após o push. Unity EditMode, PlayMode e builds Windows permanecem **NOT RUN**, pois não há projeto e esse trabalho pertence a incremento posterior autorizado.

## AC07 — ADR-001 proposta e Checkpoint Delta não promovido

### ADR-001 — proposta, não ratificada

**Contexto:** editor `6000.3.29f1` e Windows Mono localmente verificados; Hub registra o Editor existente. A licença Personal aparece ativa localmente, mas elegibilidade e termos atuais seguem com o proprietário. A UBA cloud Free é candidata, sem entitlement/condições da conta confirmados.

**Alternativas:** (A) aprovar agora os candidatos de editor, pacotes e CI; (B) substituir a rota por build self-hosted/Build Server; (C) manter apenas o ambiente local qualificado, deixar UBA Free como candidata documental e adiar decisão até haver prova do proprietário e auditoria independente.

**Recomendação PROPOSTA:** C. Manter Unity 6.3 LTS / `6000.3.29f1` x86_64 e Windows Mono apenas como candidato de desenvolvimento local. URP 17.3 e Localization 1.5.14 continuam candidatos; UI Toolkit permanece integrado ao Unity 6; Test Framework é Core vinculado ao Editor e sem versão efetiva confirmada. Não aprovar UBA, Build Server, tier/licença ou automação até AC04/AC05 passarem. A versão exata de pacotes e sua resolução só pode ser definida no incremento de projeto autorizado.

**Riscos e verificação futura:** limites públicos do UBA não demonstram franquia ou billing desta conta; termos do Build Server não resolvem, por si sós, a licença de UBA cloud. Confirmar tier, elegibilidade e custo zero antes de qualquer ativação. Não foram criados arquivos de projeto ou executados testes Unity.

### Checkpoint Delta — proposta não promovida

Reconhecer o Hub, Editor `6000.3.29f1`, CLI 1.0.1 e suporte Windows Mono como observados nesta estação. Manter PF-01 **BLOCKED por elegibilidade/termos em AC04 e entitlement/custo da rota CI em AC05**. Preservar `docs/Checkpoint.md`, Context Lock, issue #22 e PR #23 draft. Não avançar para IM-01 nem promover checkpoint.

## AC08 — Escopo, privacidade e riscos: PASS no escopo documental

Somente este arquivo de evidência é acrescentado nesta execução. Não foram alterados código, projeto Unity, Assets, Packages, ProjectSettings, workflows, governança, fontes canônicas, Context Lock, ADR ratificada ou checkpoint. Nenhuma ferramenta foi instalada/atualizada; o Hub/Editor existente não foi reinstalado; não houve aceite de termos, ativação de licença, cobrança, runner, serviço, segredo, bypass, merge ou implementação. O Editor não foi aberto e testes/build Unity não foram executados. O diff será restrito a este arquivo; nenhum dado pessoal, credencial, token, serial, informação financeira ou caminho privado será publicado.

## Fontes oficiais consultadas em 2026-11-25

- [Unity Hub — Managing licenses](https://docs.unity.com/en-us/hub/manage-license)
- [Unity Editor Software Terms](https://unity.com/legal/editor-terms-of-service/software)
- [Unity DevOps — Free plan](https://docs.unity.com/en-us/devops/pricing/free-plan)
- [Unity public pricing](https://unity.com/products)
- [Unity Build Automation — unit tests](https://docs.unity.com/en-us/build-automation/reference/unit-tests)
- [Unity CI/CD — Build Automation cloud e Build Server on-premises](https://unity.com/solutions/ci-cd)
- [Unity 6000.3 — Test Framework Core package](https://docs.unity.com/en-us/engine/6000.3/manual/packages-list/packages-all/pack-core/com-unity-test-framework)
- [Unity 6000.3 — Windows requirements and compatibility](https://docs.unity.com/en-us/engine/6000.3/manual/platform-specific/windows/requirements-and-compatibility)
- [Unity Editor 6000.3.29f1 release](https://unity.com/releases/editor/whats-new/6000.3.29f1)

**Conclusão do executor: BLOCKED — AC04 depende da confirmação do proprietário sobre elegibilidade/termos; AC05 depende da confirmação da conta e dos termos de UBA cloud sem billing.** Publique este adendo na mesma PR #23, mantenha-a em draft e pare. Auditoria independente continua necessária.
