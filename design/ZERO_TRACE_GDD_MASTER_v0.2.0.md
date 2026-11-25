# ZERO TRACE — Hacker Life Simulator

**GAME DESIGN DOCUMENT MASTER** | ZT-GDD-MASTER | v0.2.0 | 24/11/2026

**Status: REVISED PROPOSAL / OWNER RATIFICATION REQUIRED.** Documento de design para revisão, sem implementação. Visão internacional, carreira de longo prazo, simulação segura e multiplayer. Revisão ZT-GDD-REV-001: preserva 420 IDs, 36 módulos, 30 apps e a ambição completa. Novos parâmetros são hipóteses para qualificação; não representam decisões ratificadas. As regras revisadas substituem propostas conflitantes da v0.1.0.

# 01 | Visão, produto e pilares

**High concept.** ZERO TRACE é um simulador de carreira cibernética em primeira pessoa, para PC, no qual o jogador opera um sistema digital fictício, aprende competências técnicas, assume responsabilidades profissionais e constrói reputação em um mundo econômico vivo. A fantasia central não é apertar um botão de invasão: é compreender sistemas, tomar decisões sob incerteza e produzir resultados verificáveis.

**Público.** Adultos e jovens interessados em simulação, programação, investigação, gestão, RPGs complexos e segurança. O iniciante tem trilhas graduais; o especialista encontra casos inéditos, restrições de qualidade e sistemas interdependentes. Público internacional prioritário; English (en-US) é fonte de conteúdo, pt-BR é localização secundária integral. Classificação indicativa a avaliar antes da publicação.

**Pilares:** autenticidade de conceitos; liberdade com consequências; competência demonstrada, não grind; economia com escassez e confiança; mundo reativo; aprendizado transferível; jogo seguro por construção. Nenhum conceito que exija interação com alvo real é necessário para jogar. A estética premium será definida na etapa visual seguinte; este GDD fixa requisitos de legibilidade, não telas finais.

**Metas de produto:** 60 missões de campanha (80–120 horas-alvo combinando casos e preparação), 420 conteúdos autorais únicos no total, progressão aberta de 500–1.000+ horas como hipótese de design, 30 aplicativos, duas carreiras, economia persistente, modos solo e online. Horas não são trava de acesso nem promessa de duração. O catálogo é plano de produção, não conteúdo implementado. O jogador competente pode avançar mais rápido.

**Não objetivos:** internet real acessível pelo terminal; ferramentas ofensivas reais; mineração ou dinheiro real; jogos de azar; venda de poder competitivo; vigilância de jogadores fora do jogo; cidade aberta integral em 3D; dependência de API generativa paga; MMO de escala ilimitada no lançamento.

**Decisões de produto:** DIRETRIZES CONFIRMADAS pelo pedido: Unity + Codex, Steam, grande porte, ambas carreiras, interação online, marketplace, inglês primeiro e português secundário. As demais regras quantitativas e escolhas arquiteturais deste documento são propostas a ratificar. Nenhuma decisão de implementação foi promovida.

**Identidade competitiva revisada: Learn, Work, Build, Belong.** Quatro provas de diferenciação: (1) uma falha nasce em uma aplicação virtual e atravessa investigação, defesa, revisão e recuperação no mesmo estado; (2) conhecimento adquirido abre emprego e responsabilidades, não somente equipamentos; (3) ferramentas e hardware têm utilidade econômica sem poder comprado; (4) incidentes geram consequências e notícias rastreáveis. Não afirmar exclusividade de mercado nem sucesso garantido. O vertical slice deve provar os dois primeiros antes de ampliar conteúdo.

**North star:** proporção de jogadores que resolvem um caso novo e explicam sua decisão sem copiar a solução. Indicadores complementares: prazer, agência, diversidade de soluções, recuperação de erros, cooperação e acessibilidade. Horas jogadas não são meta isolada.

# 02 | Experiência, loops e onboarding

**Loop de 5 minutos:** consultar mensagens e alertas → selecionar uma tarefa → observar evidências → agir no ambiente virtual → conferir resultado → registrar progresso. O feedback mostra o que mudou, por que uma ação falhou e quais recursos foram consumidos. Nunca usar animação como substituto para estado verificável.

**Loop de sessão (30–120 min):** planejar trabalho, preparar ferramentas, executar caso em fases, validar, entregar relatório, receber pagamento/reputação, estudar ou atualizar hardware. Sessões podem ser interrompidas em checkpoints; missão longa salva progresso e decisões. No online, atividades em andamento têm política de desconexão explícita.

**Loop de carreira:** conquistar credenciais internas → obter contratos mais difíceis ou promoção → adquirir equipamento e contatos → assumir casos com maior impacto → especializar-se → montar equipe ou empresa → competir e ensinar. O ciclo de endgame acrescenta governança, pesquisa, competições e projetos coletivos.

**Onboarding:** primeiro boot de NULL OS, leitura de um e-mail, exercício de arquivos, diagnóstico de conectividade em laboratório, primeira entrega remunerada, compra de upgrade e seleção opcional de trilha (a partir de V0.2; V0.1 usa onboarding mínimo definido na seção 21). Assistência contextual em três níveis: Guided (passos conceituais), Assisted (pistas) e Expert (briefing e feedback). A assistência não impede a carreira casual; ranking de prova usa regras uniformes. Modo de leitura, pausa no solo, tutorial repetível, opção de pular após avaliação.

**Falha produtiva:** o jogador recebe pistas e possibilidade de corrigir; falhas provocam perda de bônus, confiança ou tempo simulado, não perda arbitrária de todo patrimônio. Missões de campanha essenciais mantêm rota de recuperação. Dificuldade adaptativa só muda conteúdo não ranqueado; não mascara regras nem altera prova competitiva em andamento.

**Aceite ZT-GD-02:** novato completa onboarding sem fonte externa; avaliadores conseguem explicar o loop após uma sessão; interrupção e retomada preservam objetivos; uma falha tem causa legível e caminho de recuperação.

**Loop de tensão e escolhas.** Cada missão profissional apresenta briefing com escopo, prioridade e orçamento, observação, hipótese, escolha entre abordagens, execução virtual, verificação e retrospectiva. Ao menos dois caminhos legítimos nos casos abertos; exames podem ter resposta única objetiva. A dificuldade cresce por dependências, incerteza, requisitos conflitantes e responsabilidade, nunca por espera passiva. Ação longa permite acelerar no solo ou executar outra tarefa, com cancelamento e custo mostrado antes.

**Recuperação:** treino ilimitado sem bônus repetitivo; missão narrativa falha para frente quando possível; falhas irrecuperáveis oferecem checkpoint explícito, preservando consequências e impedindo pagamento duplicado. Um jogador sem dinheiro sempre acessa laboratório, equipamento emprestado e contrato básico. Tutorial pode ser pulado, mas o primeiro contrato mostra requisitos não atendidos. Não exigir login diário, streak ou prazo de relógio real para aprender.

# 03 | Mundo, história e facções

**Cenário.** República digital fictícia de Veyra, com regiões Northline (indústria), Meridian (finanças), Halcyon (pesquisa), Ashport (logística) e Umbra (subcultura). Organizações, pessoas, endereços, marcas e documentos são inventados. O ano é deliberadamente próximo do presente. Não retratar pessoas reais nem reutilizar dados vazados. Redes são topologias internas do motor.

**Campanha: The Silence Protocol.** Seis arcos de dez episódios: First Signal (aprender e sobreviver); Broken Trust (confiança e carreira); Glass City (conflito corporativo); Blackout (resiliência urbana); Fracture (disputa de facções); Zero Trace (responsabilidade e legado). Cada arco culmina numa decisão com consequências verificáveis. O jogador não precisa adotar carreira criminosa para terminar. A trilha clandestina trata dilemas narrativos, não instruções de crimes reais.

**Facções:** Meridian Security Consortium (defesa), Atlas Infrastructure (serviços), Helix Research (ciência), Civic Integrity Bureau (investigação), The Relay (independentes), Null Assembly (clandestina), Free Circuit (comunidade e ensino). Cada uma tem objetivos, recursos, relações, confiança, tolerância a riscos e lista de serviços. A reputação não é compartilhada automaticamente entre elas.

**Narrativa sistêmica:** flags de confiança, provas preservadas, contratos honrados, danos virtuais, decisões de divulgação e alianças são persistidas. Finais: custodiante, pesquisador independente, empreendedor, operador clandestino ou solução mista. O desfecho altera contratos, não bloqueia o sandbox. A campanha online ocorre em instâncias pessoais ou grupos consensuais; decisões individuais não reescrevem o mundo global para terceiros.

**Mundo vivo:** calendário acelerado no solo (proposta: 1 min real = 10 min virtuais; pausa e avanço controlado). Tempo econômico é separado e definido na seção 12. Online, relógio do servidor com janelas de eventos anunciadas e trabalhos delegáveis; ausência não arruína uma carreira. Empresas geram empregos, atualizações, incidentes, compras, notícias e falências simuladas. NPCs seguem agendas e metas; processamento por eventos e níveis de detalhe substitui simulação contínua de cada agente.

**Aceite ZT-GD-03:** uma decisão de campanha altera pelo menos dois resultados previstos; nenhum ramo essencial fica sem conclusão; sementes iguais e mesmas ações produzem estado reproduzível; notícias citam eventos internos realmente ocorridos.

**Camadas de estado:** verdade global (calendário, preços de referência, eventos comunitários), estado de organização (orçamento, serviços e contratos), instância privada (casos, efeitos e decisões) e perfil (competências, reputação). Uma missão pessoal só publica efeitos globais por agregação autorizada e limitada; nenhum final de campanha invalida o de outro jogador. Eventos globais têm pré-condições, efeito máximo por região, prazo, encerramento e alternativa para baixa população. Conteúdo perdido em temporada volta em arquivo de treino sem prêmio competitivo.

# 04 | Personagem, carreiras e emprego

**Identidade:** alias, avatar, idioma, local de moradia fictício, portfólio, histórico profissional e árvore de competências. Sem coleta de currículo real, documentos pessoais ou localização física. A carreira Hacker representa operações independentes e pesquisa; Cybersecurity Professional representa trabalho organizacional. São caminhos complementares, não classes irreversíveis.

| Hacker / Independent Operations | Cybersecurity Professional |
|---|---|
| Ethical Hacker / Penetration Tester | SOC Analyst / Incident Responder |
| Bug Bounty Hunter / Security Researcher | Security Engineer / AppSec Engineer |
| Reverse Engineer (artefatos sintéticos) | Cloud Security / DevSecOps Engineer |
| OSINT Analyst (fontes fictícias) / Digital Investigator | Digital Forensics / Threat Intelligence Analyst |
| Software Developer / Cyber Mercenary (ficcional) | Security Architect / CISO |

**Competências compartilhadas:** sistemas, redes, programação, dados, segurança, investigação, comunicação, gestão e ética. Subcompetências têm evidências, não apenas XP. Um cargo exige nível nas competências essenciais, credencial interna quando aplicável e avaliações de qualidade. Reespecialização mantém conhecimento, mas exige experiência específica para cargos avançados.

**Job Center:** vagas geradas por orçamento, risco, região e reputação. Fluxo: vaga → candidatura com portfólio virtual → prova técnica inédita → entrevista (escolhas e justificativas) → oferta → período de experiência → tarefas, revisões, promoção ou desligamento. Salário depende de faixa, empresa e senioridade; não é ganho passivo ilimitado. Contratos fixos pagam por entregas em ciclos de trabalho, com proteção contra punição por ausência real. Freelancers recebem por marcos aceitos, com escopo e limite de responsabilidade. Demissão permite recurso narrativo, requalificação e nova candidatura.

**Exemplo SOC:** ler fila de alertas sintéticos, distinguir falso positivo de incidente, atribuir severidade, preservar registro, decidir resposta, validar recuperação e documentar. **Exemplo AppSec:** receber solicitação de mudança, revisar código seguro em linguagem do jogo, criar regra de acesso, adicionar testes, solicitar aprovação fictícia, liberar versão em staging e monitorar regressões. **Exemplo independente:** aceitar contrato de auditoria de laboratório com autorização e escopo virtual, levantar inventário, avaliar controles por ações abstratas, registrar evidências e propor correções.

**CISO e empresa:** construir estratégia, orçamento, inventário de riscos, indicadores, contratar NPCs/jogadores, delegar tarefas, negociar SLAs, revisar incidentes e responder a conselho. Nenhuma promoção é automática por patrimônio. A responsabilidade gerencial aumenta, mas o jogador pode continuar técnico.

**Aceite ZT-GD-04:** ambas carreiras concluem onboarding e têm ciclo remuneração/avaliação; mudança preserva competências; candidaturas insuficientes têm feedback; contratos não pagam duas vezes; tarefas AppSec têm testes antes/depois; cargos avançados exigem prova específica.

**Modelo de trabalho e autoridade.** Vaga define competências essenciais, nível mínimo, autorização, faixa de pagamento, responsabilidade e tarefas. Emprego: Applied → Assessment → Offered → Probation → Active → Review → Promoted / Transferred / Departed; rejeição ou desistência é recuperável. Período de experiência: duas entregas aceitas, não tempo conectado. Demissão exige regra publicada e feedback; erro honesto abre revisão, não punição indefinida. No máximo um emprego principal e dois contratos paralelos por perfil no piloto (hipótese). Trabalho parcial é pago apenas por marco previamente aceito; não cobrar presença fora do jogo.

| Família de trabalho | Ações e evidência que a distinguem | Dependência |
|---|---|---|
| Independent assessment / research | escopo virtual, comparação de controles, relatório reproduzível | M07/09/17 |
| SOC / incident response | triagem, priorização, recuperação e retrospectiva | M09/11/17 |
| AppSec / DevSecOps | requisitos, código defensivo, testes, aprovação, rollback | M08/10/12 |
| Forensics / intelligence | hipóteses, proveniência, incerteza, cadeia de custódia | M11 |
| Infrastructure / engineering | projeto, orçamento, disponibilidade, validação | M12/13 |
| Leadership / entrepreneurship | capacidade, risco, contratos, revisão de equipe | M16/24/25 |

**Dois caminhos de ponta a ponta no slice:** Hacker: PRO-031 (acordo de escopo) mais uma avaliação sintética curta dentro do mesmo caso, com relatório e pagamento; profissional: PRO-001 (fila de alertas) mais diagnóstico, decisão e validação. Não adicionar IDs nem contar subobjetivos como missões. Empregos completos entram em Early Access; slice simula vínculo de teste e remuneração por entrega. O sistema diferencia carreira independente de emprego, sem exigir uma escolha moral binária.

# 05 | Progressão, reputação e rankings

| Rank | Ritmo indicativo | Evidência mínima proposta |
|---|---|---|
| R1 Novice | 0–10h | onboarding e laboratório básico |
| R2 Apprentice | 10–30h | 2 competências no nível 1 e prova guiada |
| R3 Junior | 30–70h | 3 competências no nível 2, 5 entregas |
| R4 Operator | 70–120h | 2 provas independentes, 10 entregas |
| R5 Specialist | 120–200h | trilha nível 3, 3 casos novos, qualidade ≥75 |
| R6 Senior | 200–300h | 2 domínios nível 4, caso complexo, qualidade ≥80 |
| R7 Expert | 300–450h | 3 domínios nível 4, exame cego ≥85 |
| R8 Elite | 450–650h | 1 domínio nível 5, 2 capstones, ≥85 |
| R9 Phantom | 650–1.000h | 2 domínios nível 5, desafio multidisciplinar ≥90 |
| R10 Zero Trace | 1.000h+ | projeto de síntese inédito, banca simulada ≥90, integridade verificada |

**Escala de competência:** 0 desconhece; 1 compreende; 2 executa com apoio; 3 executa autonomamente; 4 explica trade-offs e valida; 5 resolve caso novo e ensina. Cada incremento exige artefatos e ao menos um caso de transferência, com oportunidades de refazer. A campanha não exige R10: seus ranks são recomendações de preparo, e os exames R8–R10 são capstones endgame de sementes inéditas, adicionais ao catálogo autoral e obrigatórios como sistema em V1.0. XP de atividades repetidas sofre redução (1,0; 0,5; 0,2; 0 para repetição idêntica); provas não podem ser substituídas por XP. Mérito de aprendizagem e fama social são separados.

**Pontuação de missão (proposta):** Q = 35% correção do objetivo + 25% qualidade da evidência + 20% segurança/impacto + 15% documentação + 5% eficiência. Cada dimensão vale 0–100. Quebra de regra crítica virtual (fora do escopo, adulteração de prova, dano proibido) invalida entrega competitiva independentemente da média. Aprovação comum Q≥70; profissional avançada Q≥80; provas usam limiar do rank. Avaliação é determinística e explicável; tempo só desempata após qualidade equivalente quando a prova declarar isso.

**Reputação:** confiança por facção (−100 a 100), ética profissional, confiabilidade e notoriedade clandestina são eixos distintos. Um contrato bem entregue melhora confiança; falha honesta tem penalidade menor que fraude. Jogador pode reconstruir confiança por trabalhos de recuperação. Sem perda de rank só por ficar offline.

**Leaderboards:** carreira técnica por especialização, CTF por temporada, equipes, investigação, produção de software, gestão e patrimônio (separado). Online usa resultados validados pelo servidor; offline tem conquistas locais, sem migração de saldo, item, score ou provas. Temporadas competitivas propostas de 12 semanas; Elo/Glicko a avaliar, com pareamento por habilidade, colocação provisória e regras publicadas. Empates, desconexões e revisões são tratados de forma explícita.

**Aceite ZT-GD-05:** repetir missão idêntica não promove; pagamento ou item não concede rank; prova adulterada é rejeitada; critérios e notas são visíveis; resultado online não é gravável pelo cliente; ranks R1–R10 têm caminhos verificáveis.

**Regra de evidência operacional:** skill 1 requer explicação e prática guiada; 2, exercício com apoio; 3, dois casos distintos sem solução guiada; 4, caso com trade-offs e testes; 5, capstone e justificativa em rubrica de ensino. Evidência deve identificar competência, versão de rubrica, família de caso, nível de assistência e resultado. Não aumentar nível apenas por repetição de seed. Exames de promoção são disponíveis quando competências anteriores estão demonstradas; o próprio rank-alvo nunca é pré-requisito. Rank recomendado de missão acadêmica NÃO bloqueia acesso ao aprendizado; prova verifica competências já ensinadas, não posse de certificação posterior. Ranks são duráveis, mas prova de temporada usa regras e versão correntes.

| Gate endgame | Exame adicional ao catálogo 420 | Critério proposto |
|---|---|---|
| CAP-R08 | projeto de AppSec ou defesa + investigação independente | 1 domínio nível 5, dois artefatos independentes, Q≥85 |
| CAP-R09 | crise multidisciplinar com orçamento e validação | 2 domínios nível 5, Q≥90, sem violação crítica |
| CAP-R10 | projeto de síntese e defesa das escolhas em banca simulada | Q≥90 em seed inédita, todos gates anteriores, integridade |

**R9/R10 não são conteúdo vazio:** após CAP-R09/R10, acesso a contratos enterprise gerados, mentoria, gestão de data center e pesquisa multiestágio com parâmetros de desafio próprios. O sistema entrega pelo menos três templates avançados distintos e um capstone por rank em V1.0. A campanha permanece concluível sem estes ranks. Não incluir capstones gerados no total autoral.

**Competição:** Q é inteiro de 0 a 100 calculado com rubricas versionadas; pesos são os originais. Exames usam seed de avaliação diferente da prática; GHOST/assistência de resposta indisponíveis, materiais de referência aprovados iguais. Score finalizado no servidor, com revisão, recurso, correção compensatória e histórico. Ranking global não substitui score por especialidade e não mistura patrimônio com mérito.

# 06 | ZERO Academy e design pedagógico

**Método:** explicar conceito → demonstrar em cenário sintético → praticar → receber feedback → resolver variante sem tutorial → justificar → revisar. A estrutura de tarefas, conhecimentos e habilidades do NICE Framework é referência de organização, não endosso ou certificação oficial [R1]. Cada aula possui objetivo, pré-requisito, glossário inglês/português, exercício, rubrica, erro comum, recuperação e avaliação de transferência.

| Trilha | Competências | Evidência |
|---|---|---|
| Foundations | hardware, OS, terminal, arquivos | inventário e configuração correta |
| Networks | endereçamento, DNS, TCP/IP, HTTP e latência | diagrama e diagnóstico virtual |
| Programming | lógica, funções, testes, estruturas | script isolado com testes |
| Web & Data | API, SQL conceitual, integridade | aplicativo fictício validado |
| Crypto & Identity | hash, chaves conceituais, autorização | matriz de acesso e validação |
| Defensive Security | logs, monitoramento, risco, hardening | plano de controle e evidências |
| Authorized Assessment | escopo, método, avaliação de controles | relatório de laboratório |
| Forensics & OSINT | cadeia de custódia, fontes sintéticas | linha do tempo com confiança |
| Cloud & DevSecOps | serviços, backups, implantação, revisão | pipeline simulado e recuperação |
| Leadership & Ethics | gestão, comunicação, privacidade | plano de resposta e capstone |

**Certificações internas:** Foundation, Network, Code, AppSec, Incident Response, Investigator, Cloud, Research, Architect e Leadership. Certificação é específica do jogo, não equivale a credencial externa. Provas de rank podem ser refeitas com nova semente; feedback revela critérios e lacunas, não gabarito imediato. Progresso acadêmico offline não ingressa automaticamente em ranking online.

**Fronteira educacional:** conceitos reais de segurança, práticas defensivas e programação geral são ensinados; artefatos e ações ofensivas são abstrações inventadas, não tutoriais operacionais transferíveis para alvos reais. Nenhum alvo real, credencial real, malware funcional ou mecanismo de evasão. A avaliação premia escopo, correção e responsabilidade.

**Aceite ZT-GD-06:** 100 desafios autorais catalogados; cada um mapeia competência e evidência; teste de transferência contém dados novos; avaliações não exigem internet; todos os materiais didáticos revisados por especialista antes de lançamento.

**Matriz pedagógica por catálogo:** cada grupo ACA tem um domínio principal, objetivos cognitivos, exercícios e avaliação de transferência. A certificação integral só é oferecida após a trilha completa; avaliações parciais procedurais de módulo permitem progressão antes disso. Fundação básica completa somente em V1.0 pelo catálogo original; logo, nenhuma credencial final dessa trilha pode ser exigida em V0.5. As provas parciais não contam como novas missões autorais. Antes de publicar cada aula: revisão de exatidão por profissional de cibersegurança e teste de instrução por usuário iniciante. Conteúdo ofensivo usa apenas ações fictícias restritas; fundamentos defensivos e lógica podem ser diretamente ensinados.

**Aceite educacional ampliado:** medir diagnóstico pré-aula e caso equivalente pós-aula com dados novos; registrar ganho e erro recorrente sem afirmar certificação externa. Piloto proposto de 12 novatos e 6 especialistas, voluntário e sem coletar dados pessoais desnecessários; tamanho é exploratório, não evidência estatística de eficácia. Corrigir fatos incorretos antes da distribuição.

# 07 | ZERO ENGINE: regras de simulação

**Arquitetura conceitual:** núcleo C# independente de Unity, interface de apresentação, conteúdo declarativo versionado e adaptadores de persistência/online. Unity cuida de gráficos, entrada, áudio, UI e apresentação; o núcleo resolve regras do universo. Não é um sistema operacional real nem uma máquina virtual de propósito geral. Mundo em grafo de entidades com IDs imutáveis; comandos validados produzem eventos; projeções alimentam aplicativos.

**Entidades:** World, Region, Organization, Person, Device, VirtualNetwork, Service, Account, File, Process, Application, Policy, Incident, Evidence, Mission, Contract, Skill, InventoryItem, LedgerEntry, Order, Guild, Match e Season. Cada entidade tem proprietário, escopo, versão, estado e regras de acesso. Eventos incluem criação, alteração, falha, recuperação, consumo de recursos, entrega e recompensa.

**Sistemas:** SO virtual com diretórios, usuários, grupos, permissões, processos, quotas e serviços; rede virtual com nós, enlaces, nomes, latência, capacidade e perdas sintéticas; aplicações com versões, políticas e testes; segurança com controles, alertas, severidade, evidências, incidentes e recuperação; economia e hardware; NPCs; missões; tempo e persistência. Operações são determinísticas quando mesma versão/semente e mesma ordem de comandos.

**Ciclo de comando:** autenticar ator → verificar escopo e recursos → validar regras → executar transição → emitir eventos → persistir → atualizar projeções → responder com resultado e feedback. No online, apenas servidor autoriza transições. Ação inválida não consome saldo nem altera estado salvo penalidade explícita de missão. Eventos agendados têm ordem estável e execução limitada por orçamento de simulação.

**Rede fictícia:** protocolo e endereços representados por dados internos; resolver nomes consulta apenas registro de entidades fictícias. Tráfego e capturas são registros sintéticos. Firewall é política avaliada no grafo, não alteração de firewall do host. No laboratório, avaliações de segurança são verbos de jogo com efeitos controlados, nunca ferramentas reais.

**Persistência:** snapshots versionados + journal dos eventos críticos; hashes para diagnóstico, migração transacional e rollback quando aplicável. Solo usa saves separados por perfil; online usa estado canônico do servidor. Conflito de versão rejeita comando e solicita atualização. Relógio do cliente não determina pagamentos. Excluir perfil online exige política clara e tratamento de obrigações de registro.

**Aceite ZT-GD-07:** 100% dos comandos críticos passam por validação; replay de mesma semente tem hash lógico igual; falta de recursos tem erro previsível; falha no salvamento preserva último estado íntegro; testes demonstram ausência de acesso externo pelo runtime virtual.

**Fidelidade e escala.** L0 agregado (organização distante), L1 serviço/dispositivo (inventário e controles), L2 laboratório ativo (arquivo, processo, registros, testes). Promoção/demissão de nível preserva identidade, orçamento, compromissos e efeitos; não recria saldo ou incidentes. Tempo: relógio lógico inteiro; ordem estável por instante, prioridade e ID; aleatoriedade com seed por domínio; valores monetários e recursos críticos inteiros; replay compara estado canônico, não FPS ou timestamps físicos. Jogador vê latência virtual separada da real. Operações de rede somente no grafo interno, sem socket real exposto ao simulador.

**Contrato de transição:** ator, escopo, objeto, versão esperada, comando, ID único, custo prévio → resultado aceito/rejeitado, nova versão, eventos autorizados. Cancelamentos, compensações e tarefas agendadas têm transições idempotentes. Estado de missão e recompensa crítica compartilham transação ou protocolo recuperável; notificações e projeções podem atrasar sem alterar verdade.

# 08 | NULL OS: 30 aplicativos

O desktop é o principal espaço de trabalho, acessível por monitor 3D ou modo direto em tela cheia. Janelas movem, redimensionam, ancoram, minimizam e suportam atalhos, foco, busca, histórico, notificações e acessibilidade. Aplicativos consultam projeções do mesmo mundo; nenhum inventa dados independentes.

| ID / aplicativo | Interações e resultado | Dependência principal |
|---|---|---|
| A01 Terminal | comandos virtuais, histórico, ajuda, saída tipada | M05–08 |
| A02 File Manager | árvore, permissões, busca, versões, lixeira virtual | M05 |
| A03 Browser | sites fictícios, links internos, abas, favoritos | M07/M20 |
| A04 Mail | briefing, propostas, anexos sintéticos, respostas | M17/M19 |
| A05 Code Studio | editor, lint, testes, execução isolada | M08/M10 |
| A06 Network Map | grafo, filtros, estado e capacidade | M07 |
| A07 Packet Analyzer | leitura de registros de tráfego sintético | M07/M09 |
| A08 Security Center | controles, risco, alertas e configuração | M09 |
| A09 Database Studio | esquema, consultas em dados sintéticos, integridade | M10 |
| A10 Hardware Monitor | uso de CPU/RAM, energia, temperatura virtual | M13 |
| A11 Server Manager | serviços, configuração, saúde, snapshots | M12 |
| A12 Cloud Console | recursos, políticas, orçamento e recuperação | M12 |
| A13 Banking | saldo, extratos, pagamentos e faturas virtuais | M22 |
| A14 Marketplace | filtros, ordens, histórico e negociação | M23 |
| A15 Messenger | contatos NPC e jogadores, bloqueio e denúncia | M19/M26 |
| A16 Forensic Lab | evidências, linha do tempo, custódia e relatório | M11 |
| A17 Bug Bounty | escopo fictício, submissão, revisão e disputa | M09/M17 |
| A18 ZERO Academy | aulas, glossário, laboratório, provas | M14 |
| A19 Job Center | vagas, candidaturas, entrevistas, carreira | M15/M16 |
| A20 News Network | notícias de eventos do mundo | M20 |
| A21 Stock Market | indicadores e carteira estritamente virtual | M21 |
| A22 Crypto Lab | integridade, assinatura conceitual e identidade | M09 |
| A23 GHOST AI | ajuda contextual e revisão de evidência | M19 |
| A24 Guild Manager | equipes, papéis, permissões e caixa | M24/M27 |
| A25 Code Repository | versões de scripts, revisão, testes e publicação | M08/M10 |
| A26 Contract Center | escopo, marcos, aceite, prazos e disputa | M17/M22 |
| A27 SOC Dashboard | fila de alertas, triagem, resposta e SLA | M09 |
| A28 Settings | idioma, vídeo, som, controle, acessibilidade | M32 |
| A29 App Store | instalar, remover, atualizar apps virtuais | M04/M21 |
| A30 Leaderboard | ranking, temporada, filtros e resultados | M25/M30 |

**Integração exemplar:** alerta em A27 gera evidência em A16; A26 abre tarefa; A25 registra correção; A05 testa; A08 valida controle; A13 recebe recompensa após aceite. Ações de contexto usam IDs, não texto colado para buscar entidades. Cada app tem estado vazio, carregamento, erro, retry e atalhos documentados.

**Aceite ZT-GD-08:** apps de uma versão estão funcionais, não placeholders; navegação por teclado; abrir até 8 janelas simultâneas (ou todas as disponíveis se menos) não perde estado; permissões consistentes em todos os apps; cadeia exemplar funciona de ponta a ponta.

**Matriz de estreia dos aplicativos (cumulativa, proposta):** V0.1 A01, A02, A04, A10, A16, A18, A26, A27, A28 (9); V0.2 acrescenta A06, A08, A13, A14 (13); V0.5 acrescenta A03, A05, A07, A09, A11, A12, A15, A17, A19, A20, A23, A25, A30 (26); V1.0 acrescenta A21, A22, A24, A29 (30). Em V0.2, A14 é loja NPC offline e A13 ledger solo; em V0.5, guildas básicas acessíveis por lobby/contratos (A24 completo em V1.0). A01 oferece comandos de rede mínimos em V0.1; A10/A16/A27 têm recortes jogáveis e não placeholders. Cada app só apresenta ações cujo contrato está habilitado. O menu de modo não promete recurso bloqueado. A28 inclui EN/PT-BR e acessibilidade desde o slice.

**Contrato comum de interface:** estados Loading, Empty, Ready, Error, Offline, Read-only, Pending e Conflict; erros exibem motivo, ação recuperável e, quando pertinente, referência de transação. Comando financeiro pendente não aparece concluído. Layout guarda tamanho/foco localmente, não replica janela de outro usuário. O visual deve representar estados, não substituir regras.

# 09 | Programação, AppSec e ferramentas criadas pelo jogador

**ZERO Script:** linguagem pequena com sintaxe familiar, tipos simples, variáveis, coleções, condicionais, loops limitados, funções e API explícita de objetos virtuais. A proposta é interpretador próprio ou runtime restrito após avaliação técnica. Não usar shell, eval de código host, reflexão, biblioteca nativa, acesso a rede/disco real, processos externos ou plugins executáveis. Todo script tem orçamento de instruções, memória, tempo, chamadas e saída; exceder retorna erro controlado.

**Fluxo:** criar projeto → escolher escopo → editar → rodar em fixture → ler testes → revisar diferenças → publicar versão → instalar ou distribuir pacote virtual. Ferramentas produzem relatórios, dashboards, inventários, verificadores defensivos e automatizações internas. Vendas só de pacotes declarativos/script restrito validados; sem executáveis arbitrários, links externos ou dados reais. Assinatura e versão identificam o pacote; revogação impede novas instalações de pacote bloqueado.

**AppSec realista:** requisitos de segurança, modelagem de ameaças conceitual, matriz de permissões, entradas válidas/inválidas, testes de autorização, tratamento de erros, revisão de dependências fictícias, migração, staging, revisão por pares e rollback. O jogador corrige estado e código de aplicações simuladas; testes ocultos avaliam regressões. Exemplo: recurso virtual deve ser visível apenas para dono ou papel autorizado, e a suíte valida ambos os casos.

**Aceite ZT-GD-09:** scripts válidos produzem saídas reproduzíveis; loops excessivos param; tentativas de acessar capacidades não expostas falham; teste defeituoso não satisfaz missão; pacote publicado tem versão e revisão; casos de correção incluem teste positivo e negativo.

**Ciclo de publicação de software:** Draft → Tested → Reviewed → Approved → Published → Deprecated / Revoked; declaração de capacidades, versão de runtime, hash, autor, limites e teste de regressão. No marketplace inicial só templates e pacotes aprovados pela curadoria; nenhum código distribuído em chat, HTML ativo ou upload de executável. Sandbox revalida na execução, não confia somente no verificador. Programas de jogador não alteram resultado de exame fora das capacidades explícitas; provas usam fixture limpa e orçamento uniforme. Ferramenta revogada para de executar, sem destruir dados legítimos, e compra disputada segue compensação.

# 10 | Segurança, investigação e operações

**Modelo de risco no jogo:** ativo × ameaça × impacto × probabilidade, com escalas e evidências visíveis. Incidente tem estado New → Triaged → Contained → Resolved → Reviewed; reabertura é permitida. Ações defensivas mudam políticas, disponibilidade, confidencialidade e integridade virtuais; efeito colateral é simulado. Avaliação ofensiva é representada por verificações abstratas de controle em ambiente autorizado, com pré-condições e resultado de evidência, não por payloads reais.

**Forense:** itens de evidência imutáveis com origem, momento, responsável, hash interno, cadeia de custódia e classificação. Hipóteses podem conflitar; prova exige cruzar fontes sintéticas e indicar confiança. Falsos positivos não são punidos se corretamente justificados. Logs do laboratório são gerados pelo motor, não coletados do computador real.

**CTF e clandestino:** desafios são cenários de regras fechadas, instanciados e consentidos. Nenhum ataque fora de uma instância; recursos pessoais e empresa de outro jogador não são alvos sem permissão explícita e modalidade apropriada. A carreira clandestina dramatiza dilemas, reputação e consequências em NPCs; nunca fornece instrução de intrusão, roubo de dados ou evasão real.

**Aceite ZT-GD-10:** incidente pode ser diagnosticado, tratado e revisado; evidências adulteradas são detectadas; avaliações fora do escopo são bloqueadas; modo arena não altera patrimônio pessoal; efeitos colaterais e falsos positivos são rastreáveis.

**Diferencial: Cyber Case Chain.** Falha sintética → alerta → triagem → investigação → correção → regressão → recuperação → relatório → impacto/reputação. Cada etapa consome ou produz artefato de mesma instância, com IDs rastreáveis. Cenários ofensivos oferecem inspeção e avaliação virtual de controles, raciocínio sobre permissões e evidências; sem ações reutilizáveis contra sistemas reais. Em emprego, prioridade operacional e autorização limitam o que é possível; na carreira independente, escopo e qualidade do relatório definem aceite. Casos graves têm janela virtual de resposta e consequências limitadas, com opções de recuperação. Evitar normalizar crimes reais como treinamento.

# 11 | Hardware, propriedades e vida física

**Ambiente 3D:** apartamento inicial, estação de trabalho, bancada, depósito, escritório e data center modular. Interação com objetos, iluminação, som, clima e decoração; deslocamento opcional pelo modo desktop. Nada de necessidade biológica punitiva. Aluguel e energia são despesas virtuais; período de carência impede softlock. Mobiliário e cosméticos não influenciam ranking técnico.

| Componente | Regra de simulação |
|---|---|
| CPU | orçamento de processamento por ciclo; compatibilidade socket/placa |
| GPU | capacidade de computação virtual para render e simulações permitidas |
| RAM | limite de processos simultâneos; pressão gera lentidão virtual |
| SSD | espaço, taxa de I/O e condição |
| Motherboard | slots, socket, memória e expansão |
| PSU | potência disponível e margem; falha se configuração inválida |
| Cooling | capacidade térmica; throttling virtual |
| NIC/Router | largura de banda, latência e políticas internas |
| Server/Rack | slots, custo energético, capacidade compartilhada |

**Montagem:** validar compatibilidade, potência e espaço → instalar → benchmark sintético → diagnosticar → entregar. Item tem ID único, família, nível, condição 0–100, procedência, proprietário, garantia e status (equipado, armazenado, listado, reservado, aposentado). Desgaste depende de uso virtual, não tempo offline; reparo custa ZC, condição mínima evita falhas. Hardware afeta paralelismo e escala, não resolve provas de competência nem gera moedas por uso de GPU real.

**Balanceamento exemplo:** workstation inicial 4 unidades CPU, 8 RAM, 256 storage; intermediária 12/32/1024; avançada 32/128/4096. Um relatório pequeno consome 1 CPU/2 RAM; tarefa enterprise requer recursos combinados e equipe. Valores são unidades fictícias e não benchmarks de hardware real.

**Aceite ZT-GD-11:** montagem incompatível é rejeitada com explicação; vender item equipado exige desequipar; desgaste e reparo são previsíveis; nenhuma compra desbloqueia prova por si só; perfil sem dinheiro tem equipamento básico de recuperação.

**Capacidade de hardware:** escalas virtuais independentes de hardware real; utilização CPU/RAM/IO limita concorrência de tarefas e duração virtual, não a habilidade. Perfil de referência 4/8/256; intermediário 12/32/1024; avançado 32/128/4096. Tarefas essenciais de rank sempre têm laboratório de referência fornecido. No modo profissional, emprestar máquina pode reduzir margem econômica, mas não tornar missão impossível. Reparos nunca apagam competência; desgaste só por utilização, com aviso e confirmação de substituição. Arena usa equipamento normalizado. Procedência e estado de item são gerados pelo servidor, não por dados enviados pelo cliente.

# 12 | Economia, bancos e empresas

**Moeda:** Zero Credits (ZC), inteira, exclusivamente virtual; sem conversão para dinheiro real, blockchain, saque, crédito real ou negociação externa. Offline tem economia própria; online tem ledger autoritativo. Proposta inicial: saldo 1.000 ZC, computador básico não negociável, aluguel 300 ZC/ciclo, energia 100 ZC/ciclo, reserva de recuperação via contratos básicos. Ciclo econômico solo é configurável; online usa janelas de trabalho sem cobranças punitivas por ausência.

| Classe de atividade | Recompensa indicativa |
|---|---|
| Aula / treinamento | 0–100 ZC; foco em competência |
| Contrato básico | 150–400 ZC |
| Contrato intermediário | 800–2.500 ZC |
| Especialista | 3.000–12.000 ZC |
| Operação de equipe | 10.000–60.000 ZC, dividido |
| Emprego | 800–20.000 ZC/ciclo por senioridade e entregas |

**Fontes:** contratos, salários virtuais por entrega, recompensas, vendas NPC e serviços. **Sumidouros:** upgrades, manutenção, energia, licenças fictícias, aluguel, taxas de mercado e infraestrutura. Transferências entre jogadores não criam moeda. Recompensa usa base por dificuldade × qualidade × complexidade, com teto por missão e divisão proporcional pactuada; primeira conclusão pode ter bônus. Repetição automatizada não gera fluxo ilimitado. Não utilizar monetização de urgência, juros predatórios ou dívidas reais.

**Exemplo:** contrato de 1.000 ZC × qualidade 0,85 × complexidade 1,2 = 1.020 ZC antes de taxas; se transferência de mercado for 1.020 ZC com taxa 5%, comprador paga 1.020, vendedor recebe 969, 51 são removidos da economia. Contrato NPC cria moeda por evento registrado; negociação não cria moeda.

**Ledger:** lançamentos balanceados entre contas de jogador, empresa, escrow, emissão e sumidouro. Saldo nunca negativo sem regra de crédito virtual explícita (crédito fica fora da primeira versão). Operação exige chave de idempotência, autorização, bloqueio de concorrência e trilha de auditoria. Ajustes administrativos são eventos compensatórios com justificativa e revisão, não alteração silenciosa. Monitorar inflação, concentração, volume, origem dos fundos, capacidade de compra e tempo até primeiro upgrade.

**Empresas:** registro, nome moderado, papéis, caixa separado, orçamento, contratação, folha por ciclos de entrega, contratos, despesas, capacidade e reputação. Falência virtual gera liquidação controlada, não compromete bens pessoais não vinculados. Papéis financeiros exigem permissões e limites; saídas acima de limite pedem dupla aprovação quando configurada.

**Stock Market:** simulação de indicadores e participações de NPCs, somente ZC. Não há derivativos, apostas, depósitos reais ou retorno prometido; extensão somente após balanceamento. Não é ferramenta de investimento real.

**Aceite ZT-GD-12:** conservação contábil em transferências; duplicação/replay não remunera; recuperação restaura contas e inventário consistentes; inflação monitorada; novo jogador consegue recuperar-se de insolvência; transações não usam dinheiro real.

**Modelo econômico de referência (não calibrado):** um ciclo representa 5 entregas profissionais remuneradas, não dias reais. Despesas fixas de 400 ZC por ciclo; treinamento e exames não avançam cobrança. Caso de referência: saldo inicial 1.000, cinco entregas básicas a 250 ZC com Q=80 e complexidade 1,0 → 1.000 de receita; 400 de despesa → 1.600 de saldo antes de upgrades. Dificuldade não pode impedir recuperação. Ciclo empresa separado e obrigações por contrato; ausência real não debita saldo. Proposta de primeiro upgrade 1.200–1.800 ZC, meta de 1–3 ciclos ativos, validar em testes. Hardware inicial não negociável, não gerando renda por novos perfis.

**Regra aritmética:** prêmio = piso(base × Q/100 × multiplicador de complexidade × fator de repetição), teto explícito por contrato; fator de repetição para mesma família e evidência equivalente 1 / 0,5 / 0,2 / 0 em janela definida pelo contrato; nova tarefa econômica legitimamente distinta não é cópia. Em coop, porcentagens somam 100%, arredondamento por resto determinístico, soma jamais supera prêmio. Taxa = piso(preço × taxa_em_basis_points / 10.000), comprador paga preço, vendedor recebe preço menos taxa; números inteiros. Refund/compensação não cria ganho. Emissão, queima e transferências reconciliadas por ledger. Salários, bônus e contratos derivados da mesma entrega usam um único grupo de recompensa para impedir dupla remuneração.

**Stress de balanceamento:** simular pelo menos quatro perfis (novato, especialista, trader, empresa) e três populações (50, 250, 1.000), com 30/90 ciclos virtuais, choques de escassez, bots de carga e nova oferta. Métricas: razão emissão/sumidouro, mediana e P90 do patrimônio, cesta de hardware de referência, tempo de upgrade, capacidade de recuperação. Alertas propostos: variação da cesta >20% em 10 ciclos, crescimento líquido de ZC >10% por 10 ciclos sustentado, ou novato sem rota de recuperação; investigar e ajustar prospectivamente com changelog. Não confiscar patrimônio nem prometer estabilidade. Antes de abrir mercado, documentar faixas e resultado observado; nenhuma economia foi simulada nesta revisão.

# 13 | ZERO Exchange e Steam Community Market

**Exchange interno:** hardware, peças usadas, computadores montados, capacidade virtual, ferramentas validadas e serviços com contrato. Fluxo de item: dono válido → cotação → reserva exclusiva → listagem → compra atômica (saldo, item e taxa) → entrega → histórico. Cancelamento libera reserva; expiração devolve item. Para serviços: proposta → escrow → marco → aceite ou disputa → pagamento ou devolução. Proposta de taxa inicial 5%, ajustável mediante simulação; sem taxa em cancelamento antes de execução. Preços definidos por jogadores com limites antiabuso e transparência de histórico; NPCs oferecem faixa de referência.

**Antiabuso:** bloquear venda de item vinculado/equipado/reservado, oferta duplicada, saldo insuficiente, auto-negociação artificial, duplicação por corrida e fraude de identidade. Monitorar wash trading e concentração; retenção temporária de operações suspeitas, revisão humana, recurso e registro. Sem bloqueio automático definitivo por mera anomalia.

**Steam:** Steam Inventory Schema suporta marcar itens como comercializáveis e negociáveis [R2]. A integração desejada é limitada a cosméticos e colecionáveis: carcaças, skins de CPU/GPU sem atributo funcional, temas e troféus visuais elegíveis. Não vender poder ou ZC via Steam. Um processador funcional é vendido em ZC no Exchange; uma versão visual especial poderá ser negociada no Community Market se elegível. Não prometer que qualquer item será aprovado, nem receita, saque ou liquidez. O saldo da Steam é separado da moeda do jogo.

**Integração técnica proposta:** identidade Steam verificada pelo serviço; mapeamento de item definition e instance ID; inventário cosmético reconciliado com leitura confiável; concessão idempotente, verificação de propriedade no equipar, revogação quando negociado; sem aceitar prova enviada pelo cliente. Nenhuma ponte de conversão de moeda. Serviço interno opera se a integração comercial estiver desabilitada; funcionalidade Steam Market fica condicionada à aprovação, regras e testes aplicáveis.

**Aceite ZT-GD-13:** duas compras concorrentes geram um único vencedor; falha intermediária não duplica nem perde item; troca não altera ranking; item cosmético sem propriedade não equipa; Steam indisponível não corrompe ledger; revisão de elegibilidade e compliance documentada antes de habilitar Market.

**Escopo de comércio por estágio:** V0.2 NPC fixo; V0.5 mercado online de hardware funcional com preço fixo e escrow de entrega, somente após gate de ledger; V1.0 negociação direta, ordens e leilão, serviços por marcos e ferramentas curadas. Leilão: lances reservam saldo, maior lance válido ao fechar ganha, empate por ordem do servidor, perdedores liberados; proposta de extensão de 2 min nos minutos finais, teto total de 10 min; preço mínimo e taxa visíveis, sem lance próprio. Contratos de serviço têm prazo virtual, evidência, revisão, devolução de saldo não liberado e apelação; não há execução compulsória fora do jogo.

**Comércio Steam:** schema diferencia marketable de tradable [R2]; suporte técnico não implica elegibilidade ou aprovação do título. Apenas IDs cosméticos separados de itens funcionais, sem conversão ZC, recompensa monetizável por grind, ou promessa de revenda. Em falha da Steam, cache apenas visual de propriedade já confirmada, nenhuma nova concessão/comércio até reconciliação; direito à aparência não altera atributos. Gates: política e permissões verificadas, catálogo elegível, reconciliação, revogação, suporte e comunicação de indisponibilidade. Community Market continua condicional.

# 14 | Missões, narrativa e geração procedural

**420 conteúdos autorais:** 60 campanha, 100 contratos profissionais, 50 investigações, 40 clandestinos fictícios, 100 Academy, 40 cooperativos e 30 eventos. O catálogo anexo contém ID, título inglês, entrega verificável, rank indicativo e primeiro marco de disponibilidade. As entradas são propostas de briefs, não roteiros finais, nem contabilizam variantes geradas ou traduções como missões adicionais.

**Contrato de missão:** ID e versão; modo; região/facção; briefing inglês e localização; competência; rank; pré-requisitos (missões e flags); autorização virtual; estado inicial/semente; grafo de objetivos; restrições; critérios de sucesso e falha; evidências; rubrica; recompensa; recuperação; efeitos persistentes; duração-alvo; acessibilidade; QA. Estado Offered → Accepted → Active → Submitted → Verified → Completed; Failed/Cancelled/Expired têm transições declaradas. Recompensa somente uma vez por instância. Marcos podem ser retomados, mas provas competitivas obedecem regra própria.

**Procedural:** selecionar template pela trilha, rank, reputação e estado do mundo; montar conjunto de entidades sintéticas, objetivo e restrições; usar semente, validar conectividade do grafo, solvabilidade, acesso permitido, dificuldade e orçamento; deduplicar por assinatura de objetivo; publicar somente após validação. Variar topologia, dados, personagens, restrições e resultado, não apenas nome. Se gerador falha, descartar semente e usar caso verificado. Conteúdo competitivo usa pool de sementes controlado e avaliação igualitária.

**Exemplo de missão autoral completa: CAM-031 / Blackout First Report.** Rank R4 recomendado, arco Blackout; pré-requisitos: final do arco Glass City e competência de investigação/defesa nível 2. Brief: serviço energético fictício perdeu disponibilidade e registros são incompletos. Objetivos: delimitar ativos afetados, ordenar eventos sintéticos, justificar hipóteses, escolher recuperação de serviço e produzir relatório. Restrições: manter evidência original, não alterar serviços fora do contrato. Aceite: mapa de impacto correto, linha do tempo coerente, recuperação confirmada em teste e relatório com incerteza. Falha recuperável: voltar ao snapshot de laboratório e corrigir plano; falha grave invalida bônus e altera confiança. Recompensa proposta 2.800 ZC × (Q/100); duração 60–120 min. Bifurcação: priorizar recuperação ou investigação aprofundada, com custo e consequência narrativos. Offline e online instanciado, sem alvo real.

**Aceite ZT-GD-14:** 420 IDs únicos e distribuição exata; 100% do catálogo final com briefs, localização e QA; amostra de 1.000 sementes por família sem cenário insolúvel conhecido; save/reload no meio de missão preserva flags; não duplicar recompensa; telemetria mostra repetição e abandono.

**Qualificação do conteúdo:** cada um dos 420 registros é BRIEF, não READY. Pipeline Brief → Designed → Implemented → Localized → Solvability verified → Playtested → Released. Nenhum ID pode ser computado no DoD de lançamento antes de Released. Definir versão do template, pré-requisitos de competência, capacidades, dependências de sistemas, objetivos obrigatórios/opcionais, oráculo de aceite, alternativas, consequências, acessibilidade, recuperação, recompensa, duração e strings. Testar três situações por missão: caminho válido, erro recuperável, ação proibida. Missões ramificadas testam todos os finais e retorno ao sandbox.

**Variedade:** classificar cada caso por verbo dominante (configurar, diagnosticar, implementar, investigar, proteger, restaurar, gerir, negociar ou competir), tipo de artefato e pressão (orçamento, disponibilidade, qualidade, tempo virtual). Proibir variante puramente cosmética como novo ID. Em geração procedural, pelo menos dois eixos substantivos variam; validação de solvabilidade por caminho testemunha no simulador e testes de objetivos/escopo; limitar tentativas (proposta 20) e fallback a seed aprovada. Seeds reprovadas não remuneram. Antes de lançar template, qualificar 100 seeds distintas por faixa relevante como meta de teste, com zero caso inválido aceito e nenhum loop sem saída. Meta de variedade: não repetir assinatura de objetivo nas últimas 5 ofertas, se houver pool suficiente; caso contrário informar escassez.

**Acesso:** pré-requisito sequencial da missão é distinto de rank recomendado e de competência. Catálogo revisado explicita grupo, política de acesso, estado BRIEF e chave de localização; matriz de dependências por família no catálogo JSON v0.2.0; os recortes de integração estão na seção 22. Não afirmar solvabilidade, classificação de dificuldade ou tradução completa apenas pela existência do registro.

# 15 | NPCs, GHOST e mundo procedural

NPCs têm identidade fictícia, especialidade, orçamento, confiança, agenda, objetivo e memória limitada de eventos. Behavior trees/utility AI propostos selecionam tarefas; ações passam pelas mesmas regras do motor. Organizações negociam conforme necessidades e capacidade; concorrência não depende de trapacear. Notícias derivam de eventos confirmados e templates; não inventam acontecimentos externos.

**GHOST:** assistente local contextual com busca em glossário, ajuda de comando, checklist, explicação de critérios, recomendações pedagógicas e resumo de evidências sintéticas. Não resolve provas ranqueadas, não envia dados ao exterior, não controla automaticamente ações financeiras, não acessa internet real. Gera respostas a partir de regras e conteúdo revisado no lançamento; LLM opcional futuro exige decisão, orçamento, segurança, privacidade, qualidade e política Steam de conteúdo aplicável.

**Escala proposta:** 100 organizações ativas e 2.000 NPCs abstratos no alvo inicial completo, com simulação em camadas e processamento por eventos. Apenas subset de entidades detalhadas está ativo em uma instância; os demais usam estado agregado. O número deve ser validado em benchmark, não tratado como capacidade já comprovada.

**Aceite ZT-GD-15:** NPCs respeitam orçamento e permissões; replay é reproduzível; assistente fornece pistas coerentes com estado; ajuda desabilitada em provas; nenhuma informação sensível sai do runtime; escala-alvo passa em benchmark definido.

**GHOST e NPCs:** ajuda por níveis: explicação conceitual → pista contextual → demonstração em treino separado. Registro do nível de ajuda distingue treino de prova, não diminui salário automaticamente. Ação de NPC usa orçamento, contrato e informação disponível, sem onisciência. Notícias citam IDs de eventos confirmados, dados sintéticos e consequências; um erro de geração não cria estado novo. Política de IA sem LLM pago obrigatório mantida; qualquer LLM futuro precisa de consentimento, controles, avaliação e disclosure Steam aplicável.

# 16 | Multiplayer, persistência e guildas

**Career Mode:** solo offline, save local, pause, campanha completa com equivalentes NPC para conteúdos cooperativos quando viável; nenhuma transferência de progressão econômica para online. **Persistent Online:** perfil separado, serviços autoritativos e regiões/shards com economia definida; estado persistido e recuperação. **Cooperative Operations:** 2–5 jogadores, objetivos de equipe, papéis opcionais, recompensa pactuada, reconexão; instância possui escopo explícito. **Cyber Arena:** 1v1 e 3v3/5v5 como metas condicionadas ao balanceamento, desafios simétricos, recursos normalizados, eventos e ranking por temporada.

**Sessão:** autenticação Steam verificada → carregar perfil → seleção de região/lobby → ingresso por convite ou matchmaking → servidor cria instância com semente e versão → valida comandos → persiste eventos/resultados → encerra e publica resumo. Steam fornece opções de networking/relay [R3], mas banco, lógica econômica e mundo persistente exigem serviço próprio. Nenhum servidor P2P autoritativo para economia ranqueada. Lobby não concede acesso a arquivos de outros jogadores.

**Guildas:** Founder, Officer, Treasury, Member e Recruit; convites, expulsões, transferência de liderança, tarefas, caixa compartilhado, logs e consentimento. Limite inicial 30 membros, 5 por operação; aumento condicionado a testes. Tesouraria tem quotas, aprovação configurável, trilha e recurso. Guilda pode operar empresa ou participar de torneios; não controla bens privados sem contrato.

**Desconexão:** janela de reconexão proposta 5 min; coop pausa o papel ausente ou oferece substituição consensual; arena usa regra pública de abandono e revisão para falha do servidor. Resultados pendentes não remuneram duas vezes. Reinício recupera estado confirmado; manutenção anunciada; fallback offline independente.

**Ranking:** score competitivo calculado pelo servidor, publicação Steam com modo Trusted quando aplicável [R4]. Sem vantagem por cosmético, assinatura, região ou compra. Relatório de partida inclui versão, seed, rubrica, score e auditoria. Leaderboards privados opcionais para amigos, respeitando privacidade.

**Aceite ZT-GD-16:** servidor rejeita comandos inválidos e spoofing de identidade; desconexão/reconexão não duplica recompensa; economia resiste a clientes modificados; jogadores não consentidores nunca são alvos de PvP; guilda sem permissão não movimenta caixa; teste de carga passa antes de ampliar população.

**Matriz de modos e autoridade.** Career: solo, pausa, saves locais, sem acesso a economia/ranking online. Practice: laboratório local ou instância de treino online, sem resultado competitivo. Persistent Online: perfil novo, servidor mantém ZC, ativos e entregas. Coop: 2–5 jogadores, papéis substituíveis, instância isolada e prêmio repartido; quando população baixa, casos compatíveis têm alternativa de treino solo/NPC, sem emitir score cooperativo. Arena: duelos 1v1 e desafio individual no piloto, 3v3/5v5 apenas após qualificação; loadout, tempo e seed normalizados. Guildas não podem atacar perfis ou propriedades de terceiros fora de consentimento.

**Economia de cooperação:** aceitar divisão antes do início; mudança exige consentimento de todos; participação válida por marcos verificáveis, não duração conectada. Desconexão conserva contribuição já aceita, reconexão de 5 min, substituição após regra publicada. Abandono deliberado tem revisão; falha do servidor cancela rating ou conclui somente se resultado confirmado; nunca recompensa duplicada. Season score não altera rank profissional. Matchmaking com região e faixa, fila com widening documentado e modo treino quando insuficiente.

**Guilda:** fundadores não podem se apropriar de escrow ou caixa compartilhado; transferência de liderança registrada, separação entre permissão de contratar e liberar pagamento, dupla aprovação acima do limiar definido, log de leitura para membros autorizados. Dissolução liquida obrigações antes de distribuir saldo; disputa congela somente ativos afetados.

**Operação online:** admission control, filas com estado visível, limite de instâncias, manutenção, read-only econômico em incidente, desligamento de negociação sem desligar modo solo. Definir política de retenção e exportação/encerramento do mundo antes de vender online como persistente; comunicar limites e dependência de serviço, sem prometer operação perpétua.

# 17 | Endgame, temporadas e comunidade

Endgame não é um rank artificialmente prolongado. Especialistas gerenciam portfólios de pesquisa, projetos enterprise, mentoring, torneios, empresas, data centers, crises globais simuladas e contratos multidisciplinares. Prestígio é cosmético/histórico; reinício voluntário não destrói progresso essencial. Temporadas redefinem classificação competitiva, não competências ou patrimônio comum. Recompensas de temporada podem voltar em formas equivalentes, sem pressão de compra ou login diário.

**Workshop:** editor de missão com grafo, assets permitidos, validação de objetivos, prévia e teste. Pacotes declarativos versionados; sem binários ou scripts arbitrários; scripts ZERO Script somente com verificador e capacidades restritas. Proposta: começar com Workshop curado, ampliar conteúdo não ranqueado após moderação. A documentação Steam distingue conteúdo aberto e curado [R5]. Mods desativam ranking e mercado canônico, salvo curadoria, revisão e homologação. Relatórios de conteúdo, denúncia, remoção, recurso, direitos autorais e compatibilidade de versões são obrigatórios.

**Live ops:** calendário de eventos, changelog inglês/pt-BR, feedback público, rollback, status de serviço, suporte e canal de denúncia. Nenhum evento exige participação para manter rank profissional. Separar conteúdos sazonais de provas permanentes.

**Aceite ZT-GD-17:** especialista tem ao menos três loops pós-campanha; fim de temporada preserva carreira; pacote inválido não carrega; conteúdo denunciado pode ser retirado e auditado; modo sem Workshop continua funcional.

**Endgame verificável:** pesquisa (variante inédita, evidência reproduzível, peer review), empresa (portfólio, capacidade e risco, contratos recorrentes sem pagamento passivo ilimitado), crise comunitária (objetivos agregados e contribuição verificável), mentoria (feedback sobre artefatos sintéticos, sem exposição pessoal) e arena (competência normalizada). Pelo menos três loops completos no lançamento, incluindo pesquisa, empresa e competição/cooperação, sem reinício compulsório. Workshop: conteúdo curado usa os mesmos contratos de missão, quotas, regras de moderação e licenças; conteúdo não revisado nunca entra em mercado/score oficial.

# 18 | Direção de UX, arte, áudio e localização

**Arte:** realismo estilizado premium, grafite/obsidiana, lime e violeta como acentos, vidro e microanimações sem comprometer contraste. Ambientes íntimos e técnicos, iluminação atmosférica, chuva e som de equipamento opcionais. Nada de telas excessivamente carregadas. Visual final, identidade, assets e motion design são entregáveis da Etapa 02.

**Interface:** menu Continue, New Career, Online World, Academy, Settings, Credits. HUD mínimo no 3D; NULL OS com foco em produtividade. Escalas de UI 80–150%, fontes monoespaçadas legíveis, navegação teclado/mouse, remapeamento, alto contraste, redução de movimento, legendas, volume por categoria e indicação que não depende só de cor. Evitar flashing e permitir desligar efeitos ambientais. Controle/gamepad e Steam Deck são candidatos a qualificação posterior, não promessa de lançamento.

**English-first:** IDs de texto imutáveis, strings em en-US com revisão editorial nativa, pt-BR traduzido e revisado por humano, glossário de termos técnicos, pluralização e formatação local. Código, schemas e documentação de produto em inglês; este GDD e auditorias em pt-BR para operação. Comandos de terminal e nomes de API não mudam com idioma; explicação muda. Textos de UGC têm marcação de idioma. UI testa expansão de 35% e caracteres acentuados. Voz é opcional; não bloquear missão por áudio.

**Aceite ZT-GD-18:** nenhuma chave ausente nos dois idiomas; navegação por teclado completa nos apps principais; contraste e legibilidade revisados; legendas cobrem conteúdo narrativo necessário; nenhuma missão depende exclusivamente de cor/som; desempenho visual medido.

**Brief obrigatório para a Etapa 02:** mapear 30 apps e 5 estados de interface críticos (empty, pending, failure, denied, recovery); telas prioritárias: boot/onboarding, desktop, terminal, SOC, Code Studio, Forensic Lab, Academy, Job Center, Contract Center, hardware, Exchange, ranking, guilda e lobby. Mostrar UI em 1080p, escala 150%, modo baixo contraste evitado, navegação por teclado e layout com texto inglês e pt-BR. Não exigir ambiente 3D para uso do desktop. English-first inclui títulos, briefing, respostas, rubricas, tutorial, mensagens de erro e marketing. Gate de publicação: nenhuma string de missão faltante ou placeholder nos dois idiomas.

# 19 | Segurança real, privacidade e integridade

**Modelo de ameaças:** cliente adulterado, replay, concorrência, abuso de marketplace, scripts maliciosos, UGC, injeção de conteúdo, vazamento de identidade, privilégio administrativo, DDoS, corrupção de dados e fraude de ranking. A fronteira principal separa simulador fictício do host e serviço online. O jogo real usa rede somente para funções autorizadas de multiplayer/Steam; o universo virtual nunca roteia tráfego ao mundo real.

**Controles obrigatórios:** allowlist de capacidades para scripts, cotas e isolamento; validação de entrada e autorização por objeto; servidor autoritativo; TLS e gestão de segredos; privilégios mínimos; logs auditáveis; limites de taxa; moderação de nome/chat/UGC; bloquear/mutar/denunciar; dados mínimos; retenção e exclusão definidas; backups testados; dependências e assets com licença registrada; revisão independente para dinheiro virtual, identidade e sandbox. Não solicitar credenciais externas nem dados pessoais em missões. Sem execução de arquivo host, shell ou sockets no ambiente virtual. Não permitir upload de capturas ou códigos reais para simulação competitiva.

**Privacidade:** SteamID pseudonimizado onde possível, perfil visível configurável, telemetria minimizada e consentimento onde exigido; sem captura de arquivos, digitação fora da janela ou monitoramento invasivo. Política de privacidade, classificação indicativa, termos, moderação e requisitos de jurisdição serão revisados antes da publicação; não assumir aprovação legal. Banimentos têm evidência, revisão e recurso.

**Recuperação online:** metas iniciais RPO ≤5 min para estado geral e zero perda de transação confirmada em cenário de falha individual; RTO ≤4h em desastre testado, com restauração, reconciliação de ledger e inventário e revisão de divergência. Essas metas dependem de arquitetura e orçamento comprovados. Se não atingidas, não habilitar economia persistente.

**Aceite ZT-GD-19:** zero acesso host no suite de sandbox; testes de autorização e corrida; restauração ensaiada; nenhuma vulnerabilidade HIGH/CRITICAL conhecida no escopo de lançamento; revisão independente dos componentes críticos; plano de incidente e canais de denúncia operacionais.

**Proof obligations HIGH_ASSURANCE:** (P1) scripts, logs, UGC e fixtures não acessam host, arquivos, processos, segredos ou rede externa; interpretação com capacidades mínimas, orçamento de CPU/memória/saída e nenhuma API dinâmica não permitida; testes negativos em build real e servidor. (P2) identidade Steam e sessão são verificadas no servidor, expiradas/revogáveis, com autorização por objeto em toda ação crítica; segredo fora do cliente. (P3) saldo, item e escrow conservados em todas as corridas, falhas e retries; testar concorrência, restauração, compensação e replay. (P4) score e evidência não são aceitos do cliente como autoridade; servidor usa seed, rubrica, versão e eventos. (P5) moderação, UGC e dados pessoais: conteúdo declarativo, limites, relatório e recurso, retenção mínima, sem observação do dispositivo pessoal. (P6) operação: restore em ambiente isolado, reconciliação, alertas e runbook de incidente; desabilitar mercado em falha.

**Recuperação e autoridade:** separar dados descartáveis de gameplay e estado econômico confirmado. RPO geral ≤5 min e RTO ≤4h são hipóteses; para transação confirmada exigir RPO=0 no modelo de falha individual definido, com durabilidade comprovada antes do ACK. Desastre regional só tem RPO=0 se arquitetura e orçamento comprovarem; caso contrário documentar cenário e bloquear lançamento se não satisfizer política aprovada. Revisão independente das provas é gate, não afirmação de auditor externo contratado. Nenhuma ferramenta real de hacking, exploit, malware, coleta de credenciais ou alvos externos será disponibilizada.

# 20 | Monetização, custos e viabilidade

**Modelo:** premium buy-to-play, demo gratuita, Early Access US$14.99, Standard US$29.99, Deluxe US$39.99 e expansões US$9.99–19.99 como hipóteses, não preços ratificados. Deluxe oferece trilha sonora, arte e cosméticos, nunca poder, rank ou atalhos profissionais. Sem loot boxes pagas, anúncios no jogo, venda de ZC, NFT ou assinatura obrigatória inicialmente. Regras de Steam e precificação regional precisam de verificação antes da loja. Não contabilizar Steam Market como receita garantida.

**Custos:** desenvolvimento local pode usar ferramentas gratuitas/elegibilidade de licenças; não prometer custo zero para operação persistente. Custos incluem Steam Direct, produção artística/áudio, revisão educacional, QA, localização, servidor, banco, armazenamento, backups, observabilidade, tráfego, moderação e suporte. Nenhuma conta, compra, assinatura ou publicação é autorizada por este GDD. Preflight deve confirmar licença Unity, termos SDK Steam, custo e limites de infraestrutura.

**Modelo de dimensionamento:** CCU médio e pico × carga por jogador; duração média de sessão; eventos/s; tamanho de estado e retenção; tráfego; backups; custo de plantão. Cenários de teste: 50 CCU (piloto), 250 CCU (beta), 1.000 CCU (meta inicial a qualificar). Não assumir que servidores gratuitos suportam esses números. Limitar convites, regiões e shard quando custo/capacidade não comprovados. Definir teto de gasto mensal e alertas antes de abrir online ao público. Estratégia de receita líquida deve descontar taxas, impostos, reembolsos, custos de operação e suporte, sem previsões inventadas.

**Aceite ZT-GD-20:** budget e preço aprovados; sem vantagem paga em teste comparativo; custos medidos em piloto; fallback offline preservado; regras de publicação e mercado verificadas; nenhum compromisso financeiro não autorizado.

**Viabilidade de produção:** 420 briefs × h_missão, mais 30 apps, mundo, testes, arte e localização. Cenários ilustrativos apenas para autoria/QA de missões: média 8h = 3.360h, 16h = 6.720h, 32h = 13.440h; excluem engine, multiplayer, assets, revisão, suporte e retrabalho não contido na hipótese. Medir tempo real por tipo nas 10 primeiras missões; estimar P50/P90 por categoria, capacidade semanal efetiva, gargalos e custo. Não converter em data de lançamento sem equipe/orçamento. Codex acelera execução, mas não substitui playtests, direção de arte e revisão pedagógica.

**Economia operacional:** modelo mensal = computação + banco + armazenamento/backups + tráfego + observabilidade + suporte/moderação + impostos e contingência. Piloto fechado limitado por orçamento autorizado, não por desejo de CCU. Para Early Access público, definir custo por jogador ativo, reserva, limites, SLA de suporte e plano de encerramento de serviço. A Steam Market não entra como receita projetada até validação. Premium sem vantagens, preços permanecem hipóteses.

# 21 | Escopo por versão e gates de lançamento

Visão grande não significa implementar tudo simultaneamente. Multiplayer e isolamento entram no desenho inicial, porém mundo persistente e mercado são ativados somente depois de provas. As contagens são cumulativas e incluem Academy e eventos autorais, sem duplicar conteúdo.

| Marco | Camp. | Prof. | Inv. | Clan. | Acad. | Coop. | Eventos | Total |
|---|---|---|---|---|---|---|---|---|
| V0.1 Vertical Slice | 2 | 2 | 1 | 0 | 5 | 0 | 0 | 10 |
| V0.2 Demo | 5 | 5 | 3 | 2 | 10 | 0 | 0 | 25 |
| V0.5 Early Access | 25 | 45 | 20 | 15 | 45 | 12 | 8 | 170 |
| V1.0 Full Release | 60 | 100 | 50 | 40 | 100 | 40 | 30 | 420 |

**V0.1 (interno):** apartamento único, NULL OS básico (9 apps enumerados abaixo), simulação de arquivos/rede, primeiro caso de cada carreira, save e 10 missões. Gate: loop integral e testes headless; protótipo de isolamento e contrato de comandos.

**V0.2 Demo (pública condicionada):** 25 conteúdos, onboarding, 13 apps, casos de investigação, economia simples offline, upgrade, localização e feedback. Gate: build distribuível, playtest e acessibilidade básica; sem promessa de online público.

**V0.5 Early Access:** 170 conteúdos, 26 apps, ambas carreiras, procedural validado, coop e piloto persistente com capacidade limitada, guildas básicas e mercado interno após gates de integridade. Ranks R1–R7; conteúdo de R8+ bloqueado até qualificação. Steam identity/achievements quando verificados; sem Steam Market funcional obrigatório nesta fase. Gate: suporte, observabilidade, orçamento, rollback e teste de carga; se online não estiver pronto, não vender como disponível.

**V1.0:** 420 conteúdos, 30 apps, seis arcos, R1–R10, ambas carreiras, endgame, mundo persistente, Exchange, coop, arena, guildas, ranking, localização completa e integração Steam aprovada. Workshop curado previsto; Community Market de cosméticos é condicional a elegibilidade e decisão comercial, não bloqueia a versão se desabilitado e comunicado. Não declarar V1.0 completa com defeito crítico ou sem funcionalidades essenciais aprovadas.

**V1.x/FUTURE:** novas regiões, carreiras, expansão, suporte adicional de plataformas, Workshop mais amplo, IA generativa opcional. Não entra automaticamente em V1.0. Classificação de escopo: NECESSARY = requisitos do marco; IMPORTANT = aprimoramento mensurável sem bloquear DoD; FUTURE = após 1.0 ou gate externo; OUT OF SCOPE = sistemas reais, dinheiro real, execução arbitrária e cidade integral.

**Dependência de produção:** conteúdo inicial depende de templates, rubricas, localization keys e ferramentas de autoria. Arte final depende da Etapa 02. Planejamento de código depende da aprovação deste GDD e do Source Pack. Calendário não estimado sem capacidade real da equipe, throughput e custo.

**Subgates cumulativos:** V0.1a core/contratos e sandbox; V0.1b slice completo com hardware mínimo, 9 apps, ledger offline e 10 conteúdos; V0.2 demo offline, 13 apps, 25 conteúdos, testes de primeira sessão; V0.5a carreiras e 170 conteúdos em build de teste com dependências online prontas; V0.5b piloto com autenticação e recuperação (50 CCU alvo); V0.5c Early Access com coop/arena inicial e mercado interno somente após seus gates. Os 12 casos COO e 8 EVT do marco exigem ao menos piloto online funcional e validação de modo; se gate falhar, adiar lançamento de escopo ou obter revisão formal do marco, não contar conteúdo indisponível nem publicar como pronto. V1.0 só após 420 conteúdos Released, 30 apps, R1–R10, mundo persistente e endgame qualificados; Community Market continua condicional. A ordem é incremental, não autorização de código.

**Onboarding por release:** V0.1 exige apenas CAM-001 e CAM-002 como experiência inicial; Academy, contrato e investigação disponíveis por checkpoint de onboarding (concluir CAM-001), sem exigir CAM-003/005. V0.2 completa First Signal inicial até CAM-005 e loja/upgrade. Não exigir certificado integral posterior para entrar em módulo inicial. No slice, PRO-001 e PRO-031 são acessíveis após CAM-001 e tutorial contextual. PRO-011 exige AppSec mínimo (matriz de acesso) em V0.2, não Code Studio completo.

# 22 | Arquitetura macro e backlog de módulos

IDs abaixo são estáveis como referência de planejamento. Cada linha indica pré-requisitos e saída auditável; versão exata das bibliotecas, estrutura de projetos e Work Orders ficam na Etapa 03. Dependências são incrementais: um protótipo pode usar contratos de módulo ainda não totalmente produzido.

| ID / módulo | Depende de | Entrega / aceite de módulo |
|---|---|---|
| M00 Source Pack & GDD | aprovação de design | docs canônicos, ADRs, checkpoint, DoD |
| M01 Unity Foundation | M00 | build reproduzível, CI, cenas mínimas |
| M02 Simulation Core | M01 | comandos, eventos, relógio, replay |
| M03 3D Environment | M01, visual | cena interativa, modo desktop |
| M04 NULL OS | M01–02 | desktop, janela, foco, app lifecycle |
| M05 Virtual File System | M02 | árvore, permissão, persistência |
| M06 Terminal Engine | M04–05 | parser restrito, ajuda, erros |
| M07 Virtual Networking | M02, M05 | grafo, tráfego e DNS internos |
| M08 Programming Engine | M02, M05–06 | scripts isolados, cotas, testes |
| M09 Security Simulation | M07–08 | controles, incidentes, evidência |
| M10 Application Security | M08–09 | apps fictícios, testes, revisão |
| M11 Digital Forensics | M05, M09 | custódia, hipótese, relatório |
| M12 Cloud & Infrastructure | M07, M09 | serviços, backup, recovery |
| M13 Hardware Simulation | M02, M03 | recursos, montagem, condições |
| M14 ZERO Academy | M05–11 por trilha; M17 mínimo para exercícios | currículo e rubricas verificadas |
| M15 Career Engine | M02, M14 | competências, progressão |
| M16 Employment System | M15, M17 mínimo | vagas, prova, trabalho, salário |
| M17 Mission Engine | M02, M05; M09 por tipo de caso, M22 para pagamento final | grafo, avaliador, recompensa |
| M18 Campaign & Narrative | M17, M19 mínimo | seis arcos, escolhas, finais |
| M19 NPC Intelligence | M02 | agentes e decisões auditáveis |
| M20 Procedural World | M17, M19 | sementes, validação, eventos |
| M21 Economy Engine | M02; M13 e M17 por integração | fontes/sumidouros e orçamento |
| M22 Banking System | M21 | ledger, idempotência, extrato |
| M23 ZERO Exchange | M13, M22, M27 online | reserva, negociação, disputa |
| M24 Business Management | M16, M22 | empresa, papéis, orçamento |
| M25 Reputation & Rankings | M15, M17 | rubricas, score, temporadas |
| M26 Multiplayer Foundation | M02, M30 identidade mínima | sessão, autorização, protocolo |
| M27 Persistent Online World | M20, M22, M26 | autoridade, persistência, restore |
| M28 Cooperative Operations | M17, M26–27 | equipe, marcos, reconexão |
| M29 Competitive Cyber Arena | M25–28 | partidas justas e score seguro |
| M30 Steam Integration | M01 para identidade; M25–29 para recursos consumidores | identidade, distribuição, ranking, inventário |
| M31 Workshop & Modding | M08, M17, M30 | autoria, validação, moderação |
| M32 Localization & Accessibility | M01, M04 | i18n desde início, acessibilidade |
| M33 Audio & Visual Polish | M03–04, M32 | arte final e desempenho |
| M34 Security & Performance | todos aplicáveis | evidências de segurança, carga, recuperação |
| M35 Release Engineering | M34, M30 | pacote, deploy, validação Steam |

**Sequência de risco:** M00→M01→M02, com M32 inicial; protótipos de sandbox, saves e autorização cedo. M17 mínimo antecede emprego; M19 mínimo antecede campanha; M30 identity mínimo antecede M26, e demais Steam features após os sistemas consumidores. A tabela é macro; a matriz de entregas parciais da seção 22 é normativa para evitar ciclos entre integrações. M21–22 recebem HIGH_ASSURANCE para economia autoritativa, M08 para sandbox, M26–30 para identidade e mercado. Não implementar o próximo incremento com bloqueio no atual.

**Grafo normativo de incrementos (design, não Work Orders).** A tabela macro representa integrações finais e não deve ser ordenada como 36 entregas monolíticas. Estes recortes quebram ciclos; demais entregas seguem os módulos originais.

| Nó | Pré-requisitos | Saída / gate |
|---|---|---|
| D00 | aprovação GDD e visual | M00 Source Pack |
| D01 | D00 | M01 + M32 base + M03 cena mínima |
| D02 | D01 | M02 núcleo, relógio, comandos, persistência base |
| D03 | D02 | M04/05/06 desktop, arquivos e terminal mínimo |
| D04 | D02, D03 | M07 rede mínima, M08 sandbox protótipo |
| D05 | D04 | M09/11 segurança e forense mínimas |
| D06 | D02, D03 | M17a orquestração e avaliação sem pagamento |
| D07 | D02 | M21a contas e M22a ledger solo sem dependência de missão |
| D08 | D06, D07, D05 | M17b recompensa atômica, M19a NPC mínimo, M13a hardware |
| D09 | D03, D04, D05, D08 | M14a, M15a, M18a; slice V0.1 |
| D10 | D09 | M10a, M23a NPC, M14b, M18b; demo V0.2 |
| D11 | D10 | M08/09/10/11/12/13/14/15/16/17/18/19/20/21/22/25 aprofundados por trilha |
| D12 | D01 | M30a identidade Steam (sem ranking/inventário) |
| D13 | D02, D12 | M26 sessão/autorização |
| D14 | D11, D13 | M27 estado autoritativo, restore |
| D15 | D14 | M23b mercado fixo, M24a guildas, M28 coop, M29 arena piloto, M30b rankings |
| D16 | D15 | M34 piloto e M35 build; Early Access condicionado |
| D17 | D16 | demais apps, M23c serviços/leilão, M24/M25/M28/M29 completos, M31 Workshop curado, M33 polish |
| D18 | D17 | M34 qualificação final, M35 release; V1.0 |
| D19 | D15, gate externo | M30c Steam Market cosmético condicional; não dependência de D18 |

**M34/M35 são transversais:** executar testes, builds, auditoria e recuperação desde D02, não esperar D16/D18. Integração final de D11 não impõe ordem interna circular: M17a precede M14/M15/M16, ledger solo antecede recompensa, M19a precede narrativa e M13 precede balanceamento. Etapa 03 decompõe D11 em WOs específicos e valida DAG antes de implementar.

# 23 | Qualidade, métricas e critérios de aceite globais

**Metas iniciais de desempenho (a qualificar):** Windows 11 64-bit, 1080p/60 FPS alvo em PC de referência a fixar (CPU 6c, 16 GB RAM, GPU classe RTX 3060); 30 FPS mínimo em perfil low definido no preflight. P95 de resposta a comando local ≤100 ms sem tarefa virtual longa; online P95 de ACK ≤300 ms em região sob carga-alvo, excluindo latência extrema; save solo ≤2 s P95; 1.000 CCU é meta de capacidade, não garantia. Headless benchmark: 2.000 NPCs abstratos, 100 organizações, 10.000 dispositivos virtuais em fixture; medição por percentis, memória, throughput e duração. Proteger frame time com processamento por orçamento e filas.

**Plano de testes:** unitários de regras, invariantes e parser; property-based de ledger e geração; integração de saves, apps e missões; E2E de ambas carreiras; testes negativos de sandbox, autorização, fraude e isolamento; carga e soak 24h de online; chaos/recovery; migração e restauração; localização, acessibilidade, hardware compatível, QA de conteúdo, playtests com novatos e especialistas. Testes de adversário são restritos ao produto e ambientes próprios, não alvo externo. Definir critérios e fixture antes da implementação; nenhuma exclusão artificial para inflar cobertura.

**Métricas de produto propostas:** ≥80% dos playtesters completam onboarding sem ajuda externa; ≥70% identificam causa de falha de uma missão; ≥60% resolvem variante inédita após aula; ≥75% avaliam clareza de progressão; monitorar abandono, repetição, tempo até primeiro contrato/upgrade, dispersão de ranks, inflação, fraude, crash-free e latência. Métricas são hipóteses com amostra definida no plano de teste, não métricas obtidas. Balancear pela evidência, sem otimizar retenção compulsiva.

**DoD por versão:** escopo implementado, testado, documentado, build reproduzível, distribuído em ambiente autorizado e validado; inventário de assets/licenças; conteúdo en-US e pt-BR; segurança sem HIGH/CRITICAL conhecido; regressão aprovada; migração/backup/recovery quando aplicável; logs/telemetria mínimos; changelog, suporte e rollback. Evidence Bundle com base/head SHA, arquivos, testes, resultados, riscos e Checkpoint Delta; auditoria independente objetiva antes de promoção canônica. Publicação comercial só com autorização e gates externos.

**Gate deste GDD:** distribuição de 420 entradas, IDs estáveis, mecânicas e dependências presentes, riscos e decisões não aprovadas explícitos. Este gate é de documentação, não de produto funcional. A próxima etapa é conceito visual após ratificação; nenhum código, PR, conta, custo, deploy ou merge foi executado.

**Gate de qualidade do design e playtest:** separar teste estrutural (IDs, DAG, contagem), teste de conteúdo (solvabilidade, rubrica), teste de experiência (observação de sessão, compreensão), segurança (provas P1–P6) e operação (carga/restore). Não promover DoD por percentual de cobertura ou meta declarada. Vertical slice: ambos os loops completos em duas sessões independentes, um caso defensivo atravessando pelo menos 3 apps, erro recuperável e pagamento único. Demo: testar 12 novatos/6 experientes, ≥80% completam onboarding, ≥60% resolvem variante, nenhuma falha bloqueante; metas indicativas para ajustar, reportar denominador e incerteza. Online: 24h soak no nível de CCU do marco, falhas injetadas no ambiente próprio, reconciliação zero divergência, teste de reconexão e retry. V1.0: todas as 420 missões homologadas e localização, capstones R8–R10 e três endgame loops; nenhum HIGH/CRITICAL conhecido.

# 24 | Decisões, riscos e referências

| ID | Decisão / status |
|---|---|
| GD-D01 | CONFIRMADA: grande porte, Unity + Codex, Steam, English-first / pt-BR |
| GD-D02 | CONFIRMADA: duas trajetórias, mundo online e marketplace desejados |
| GD-D03 | PROPOSTA: ZERO TRACE, NULL OS, ZERO ENGINE, GHOST; verificar marcas e nome |
| GD-D04 | PROPOSTA: solo separado de online; servidor autoritativo; sem P2W |
| GD-D05 | PROPOSTA: 420 conteúdos e distribuição por marco; validar capacidade |
| GD-D06 | PROPOSTA: ZC sem conversão; Steam Market só cosméticos elegíveis |
| GD-D07 | PROPOSTA: Unity URP/UI Toolkit, C# core, SQLite local, PostgreSQL online |
| GD-D08 | PROPOSTA: linguagem restrita e UGC declarativo; escolha técnica após threat model |
| GD-D09 | PROPOSTA: preço premium, quotas, metas de desempenho e rank |
| GD-D10 | CONDICIONAL: Steam Market, Workshop, escala e regiões após gates |

| Risco | Nível | Mitigação / gate |
|---|---|---|
| Escopo excessivo e produção de conteúdo | Alto | vertical slice, ferramentas de autoria, throughput medido |
| Simulação sem profundidade ou tediosa | Alto | playtest novatos/especialistas; casos de transferência |
| Fuga do sandbox / scripts ou UGC | Crítico | capacidades restritas, testes negativos e revisão independente |
| Duplicação de item/moeda | Crítico | transações, idempotência, reconciliação e restore |
| Custos online e baixa população | Alto | pilotos limitados, orçamento, modo solo independente |
| Integração Steam não elegível | Alto | consulta oficial e feature flag; Exchange permanece |
| Conhecimento técnico incorreto | Alto | revisão educacional, fontes e feedback de especialistas |
| Marca/asset/licença e privacidade | Alto | clearance de nome, inventário de licenças, revisão legal |
| Abuso social e fraude de ranking | Alto | moderação, autoridade, logs, recurso, regras de temporada |
| Localização fraca | Médio | revisão nativa, glossário e QA de interface |

**Pendências antes da Etapa 03:** ratificar GDD e visual; verificar nome e propriedade intelectual; decidir versão Unity e licenças; definir orçamento, equipe e capacidade de produção; aprovar critérios pedagógicos e avaliador especialista; escolher arquitetura de sandbox, backend e Steam SDK; validar elegibilidade Market/Workshop; estabelecer região inicial, orçamento online, classificação indicativa e privacidade. Não são bloqueios à criação do conceito visual, mas impedem declarar produção ou lançamento aprovado.

**Referências oficiais consultadas em 24/11/2026 (apoio conceitual; revalidar na implementação):**

- [R1] NIST NICE Framework Resource Center: https://www.nist.gov/itl/applied-cybersecurity/nice/nice-framework-resource-center
- [R2] Steam Inventory Schema: https://partner.steamgames.com/doc/features/inventory/schema
- [R3] Steam Networking: https://partner.steamgames.com/doc/features/multiplayer/networking
- [R4] Steam Leaderboards: https://partner.steamgames.com/doc/features/leaderboards
- [R5] Steam Workshop: https://partner.steamgames.com/doc/features/workshop

**Checkpoint de design:** ZT-GDD-MASTER v0.2.0 | REVISED PROPOSAL / OWNER RATIFICATION REQUIRED. Próxima ação: Etapa 02, direção artística, somente após ratificar ou ajustar este documento. Etapa 03: Source Pack, ADRs, arquitetura, Work Orders e Context Lock. Nenhum incremento de implementação iniciado.

# 25 | Anexo A — Catálogo autoral de 420 missões

Cada entrada tem ID estável, título em inglês, resultado/artefato que define o aceite do brief, rank recomendado, marco de estreia e pré-requisito. O conteúdo textual final (briefing, diálogo, seed, objetivos, restrições, rubrica completa e tradução) será escrito em incrementos de produção, conforme o contrato da seção 14. Os ranks são recomendações; o modo profissional e ranqueado aplica gates de competência. O rank indicado não é tempo obrigatório de jogo. Em cada trilha sequencial, o ID anterior é pré-requisito salvo a primeira entrada; eventos independem de ordem. A primeira missão de cada trilha requer autorização da modalidade e competências de entrada quando aplicáveis. Aulas não exigem rank recomendado; onboarding e provas parciais seguem as seções 5, 6 e 21. A campanha exige o arco anterior. Em Academy, provas de fim de trilha exigem os exercícios anteriores. V0.1 = vertical slice; V0.2 = demo; V0.5 = Early Access; V1.0 = lançamento completo. A tabela usa estreia, não contagens cumulativas.

**Rubrica comum:** resultado listado verificável no estado do mundo, evidência preservada e autorização respeitada; Q≥70 em casos comuns, ≥80 em profissionais avançados, provas por rank (seção 05). Recompensas por categoria e dificuldade (seção 12); Academy prioriza conhecimento; eventos competitivos só pontuam após verificação autoritativa. Cenários autorais podem ser rejogados sem nova recompensa integral. Nenhum brief requer sistemas reais.

## CAM-001 a CAM-010 | Campanha · First Signal

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CAM-001 · First Boot | configurar perfil e desktop | R1 | V0.1 |
| CAM-002 · Lost Directory | recuperar arquivo virtual | R1 | V0.1 |
| CAM-003 · Signal Check | diagnosticar conexão do laboratório | R1 | V0.2 |
| CAM-004 · First Contract | entregar inventário ao cliente | R1 | V0.2 |
| CAM-005 · Rent Due | organizar orçamento inicial | R1 | V0.2 |
| CAM-006 · A Better Machine | montar upgrade compatível | R2 | V0.5 |
| CAM-007 · Inbox Evidence | classificar mensagens fictícias | R2 | V0.5 |
| CAM-008 · Two Paths | concluir avaliação de carreira | R2 | V0.5 |
| CAM-009 · Relay Contact | validar identidade de contato virtual | R2 | V0.5 |
| CAM-010 · First Reputation | apresentar portfólio e escolher vínculo | R2 | V0.5 |

## CAM-011 a CAM-020 | Campanha · Broken Trust

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CAM-011 · New Employer | concluir entrevista e contrato | R2 | V0.5 |
| CAM-012 · The Missing Ticket | reconstituir tarefa desaparecida | R2 | V0.5 |
| CAM-013 · Broken Permission | validar correção de acesso | R2 | V0.5 |
| CAM-014 · Silent Alert | classificar evento de segurança | R2 | V0.5 |
| CAM-015 · False Accusation | comparar evidências conflitantes | R2 | V0.5 |
| CAM-016 · The Audit Letter | entregar relatório auditável | R3 | V0.5 |
| CAM-017 · Conflicting Orders | justificar escolha de prioridade | R3 | V0.5 |
| CAM-018 · A Costly Fix | balancear disponibilidade e proteção | R3 | V0.5 |
| CAM-019 · Whistle in the Dark | avaliar canal de divulgação fictício | R3 | V0.5 |
| CAM-020 · Trust Ledger | resolver conflito e registrar consequência | R3 | V0.5 |

## CAM-021 a CAM-030 | Campanha · Glass City

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CAM-021 · Meridian Offer | avaliar proposta enterprise | R3 | V0.5 |
| CAM-022 · Glass Inventory | mapear ativos de corporação fictícia | R3 | V0.5 |
| CAM-023 · Policy Drift | comparar configuração com política | R3 | V0.5 |
| CAM-024 · Release Window | validar atualização em staging | R3 | V0.5 |
| CAM-025 · The Quiet Vendor | avaliar risco de fornecedor | R3 | V0.5 |
| CAM-026 · Customer Boundary | demonstrar isolamento entre clientes | R4 | V1.0 |
| CAM-027 · Broken Promise | reconciliar SLA e fatos | R4 | V1.0 |
| CAM-028 · The Board Meeting | apresentar plano de mitigação | R4 | V1.0 |
| CAM-029 · Divided Interests | escolher divulgação responsável | R4 | V1.0 |
| CAM-030 · Glass Verdict | entregar decisão ao conselho | R4 | V1.0 |

## CAM-031 a CAM-040 | Campanha · Blackout

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CAM-031 · Blackout First Report | delimitar impacto e restaurar serviço de teste | R4 | V1.0 |
| CAM-032 · Dark District | priorizar serviços virtuais afetados | R4 | V1.0 |
| CAM-033 · Control Room | verificar estado de dependências | R4 | V1.0 |
| CAM-034 · Missing Backup | comparar snapshots sintéticos | R4 | V1.0 |
| CAM-035 · Emergency Budget | alocar recursos limitados | R4 | V1.0 |
| CAM-036 · The Long Night | coordenar fila de incidentes | R5 | V1.0 |
| CAM-037 · Broken Timeline | conciliar relógios de registros | R5 | V1.0 |
| CAM-038 · Service Return | validar recuperação de serviços | R5 | V1.0 |
| CAM-039 · Public Statement | explicar impacto e incerteza | R5 | V1.0 |
| CAM-040 · After the Lights | concluir revisão pós-incidente | R5 | V1.0 |

## CAM-041 a CAM-050 | Campanha · Fracture

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CAM-041 · Fractured Alliance | escolher mandato entre facções | R5 | V1.0 |
| CAM-042 · The Double Contract | resolver conflito de interesses | R5 | V1.0 |
| CAM-043 · Research Dispute | reproduzir resultado de laboratório | R5 | V1.0 |
| CAM-044 · Lost Witness | correlacionar depoimentos fictícios | R5 | V1.0 |
| CAM-045 · Signal Council | negociar regras de cooperação | R5 | V1.0 |
| CAM-046 · Trust Under Pressure | rever acessos de equipe | R6 | V1.0 |
| CAM-047 · The Hidden Cost | avaliar consequência econômica | R6 | V1.0 |
| CAM-048 · Boundary Line | recusar ou renegociar pedido indevido | R6 | V1.0 |
| CAM-049 · The Accord | construir acordo de resposta | R6 | V1.0 |
| CAM-050 · Fracture Resolution | validar efeitos das escolhas | R6 | V1.0 |

## CAM-051 a CAM-060 | Campanha · Zero Trace

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CAM-051 · Legacy Systems | classificar riscos herdados | R6 | V1.0 |
| CAM-052 · The Last Portfolio | provar competência multidisciplinar | R6 | V1.0 |
| CAM-053 · A City in Balance | priorizar investimentos virtuais | R6 | V1.0 |
| CAM-054 · Mentor's Test | avaliar aprendiz fictício | R6 | V1.0 |
| CAM-055 · Board of Trust | defender estratégia de segurança | R6 | V1.0 |
| CAM-056 · The Final Simulation | resolver crise de laboratório inédita | R6 | V1.0 |
| CAM-057 · Open Evidence | publicar relatório fictício revisado | R6 | V1.0 |
| CAM-058 · A Choice of Futures | selecionar legado e alianças | R6 | V1.0 |
| CAM-059 · World Reconciled | validar estado final do mundo | R6 | V1.0 |
| CAM-060 · Zero Trace | apresentar síntese e liberar endgame | R6 | V1.0 |

## PRO-001 a PRO-010 | Contratos profissionais · SOC Operations

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-001 · Alert Queue | triagem de alertas sintéticos | R1 | V0.1 |
| PRO-002 · Log Baseline | documentar atividade esperada | R1 | V0.2 |
| PRO-003 · Noise Budget | reduzir falsos positivos de regra | R1 | V0.2 |
| PRO-004 · Priority Shift | ordenar incidentes por impacto | R2 | V0.5 |
| PRO-005 · Escalation Note | entregar encaminhamento com evidências | R2 | V0.5 |
| PRO-006 · Coverage Gap | mapear lacuna de monitoramento | R2 | V0.5 |
| PRO-007 · Shift Handover | produzir passagem de turno | R3 | V0.5 |
| PRO-008 · SLA Recovery | resolver fila dentro de restrições | R3 | V0.5 |
| PRO-009 · Detection Review | validar regra em conjunto de testes | R3 | V0.5 |
| PRO-010 · SOC Improvement | comparar indicadores antes e depois | R4 | V1.0 |

## PRO-011 a PRO-020 | Contratos profissionais · Application Security

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-011 · Access Review | validar matriz de acesso de aplicação | R1 | V0.2 |
| PRO-012 · Input Contract | testar entradas válidas e inválidas | R1 | V0.5 |
| PRO-013 · Safe Errors | remover exposição indevida em erros sintéticos | R1 | V0.5 |
| PRO-014 · Session Policy | validar regras de sessão virtual | R2 | V0.5 |
| PRO-015 · Dependency Review | classificar dependências fictícias | R2 | V0.5 |
| PRO-016 · Data Boundary | testar isolamento de registros | R2 | V0.5 |
| PRO-017 · Staging Gate | executar suite de regressão | R3 | V0.5 |
| PRO-018 · Patch Review | justificar mudança e revisar testes | R3 | V0.5 |
| PRO-019 · Rollback Drill | restaurar versão anterior segura | R3 | V0.5 |
| PRO-020 · Secure Release | entregar release com evidências | R4 | V1.0 |

## PRO-021 a PRO-030 | Contratos profissionais · Network & Security Engineering

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-021 · Network Diagram | entregar mapa de rede autorizada | R2 | V0.5 |
| PRO-022 · Segment Plan | definir segmentos por função | R2 | V0.5 |
| PRO-023 · Service Inventory | validar catálogo de serviços | R2 | V0.5 |
| PRO-024 · Policy Cleanup | identificar regras redundantes | R3 | V0.5 |
| PRO-025 · Capacity Test | verificar capacidade de enlace virtual | R3 | V0.5 |
| PRO-026 · Access Boundary | testar comunicação permitida e negada | R3 | V0.5 |
| PRO-027 · Change Ticket | documentar mudança e aprovação | R4 | V1.0 |
| PRO-028 · Resilience Path | validar rota de contingência sintética | R4 | V1.0 |
| PRO-029 · Configuration Drift | reconciliar políticas divergentes | R4 | V1.0 |
| PRO-030 · Network Assurance | entregar relatório de controles | R5 | V1.0 |

## PRO-031 a PRO-040 | Contratos profissionais · Authorized Assessment

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-031 · Scope Agreement | entregar escopo e inventário de um laboratório autorizado | R1 | V0.1 |
| PRO-032 · Asset Review | validar inventário do laboratório | R2 | V0.5 |
| PRO-033 · Control Checklist | avaliar controles abstratos | R2 | V0.5 |
| PRO-034 · Evidence Quality | classificar evidências confiáveis | R3 | V0.5 |
| PRO-035 · Risk Ranking | priorizar achados simulados | R3 | V0.5 |
| PRO-036 · Remediation Brief | formular correções defensivas | R3 | V0.5 |
| PRO-037 · Retest Window | validar correção em fixture | R4 | V1.0 |
| PRO-038 · Duplicate Finding | consolidar relatórios redundantes | R4 | V1.0 |
| PRO-039 · Client Debrief | explicar risco e limitações | R4 | V1.0 |
| PRO-040 · Assessment Package | entregar relatório e reteste | R5 | V1.0 |

## PRO-041 a PRO-050 | Contratos profissionais · Bug Bounty Research

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-041 · Program Rules | interpretar escopo de bounty fictício | R3 | V0.5 |
| PRO-042 · Reproducible Finding | registrar evidência sintética reproduzível | R3 | V0.5 |
| PRO-043 · Impact Statement | justificar impacto de achado abstrato | R3 | V0.5 |
| PRO-044 · Duplicate Review | comparar submissões virtuais | R4 | V1.0 |
| PRO-045 · Triage Dialogue | responder perguntas do avaliador | R4 | V1.0 |
| PRO-046 · Safe Disclosure | planejar comunicação responsável | R4 | V1.0 |
| PRO-047 · Fix Verification | validar teste de correção | R5 | V1.0 |
| PRO-048 · Research Notebook | organizar hipóteses e resultados | R5 | V1.0 |
| PRO-049 · Bounty Appeal | resolver disputa com evidência | R5 | V1.0 |
| PRO-050 · Research Portfolio | entregar conjunto revisado | R6 | V1.0 |

## PRO-051 a PRO-060 | Contratos profissionais · Cloud & DevSecOps

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-051 · Cloud Inventory | mapear serviços virtuais | R3 | V0.5 |
| PRO-052 · Budget Alert | configurar limite simulado | R3 | V0.5 |
| PRO-053 · Role Review | reduzir privilégios de laboratório | R3 | V0.5 |
| PRO-054 · Build Gate | validar pipeline fictício | R4 | V1.0 |
| PRO-055 · Artifact Trust | verificar integridade de pacote | R4 | V1.0 |
| PRO-056 · Backup Schedule | definir política de recuperação | R4 | V1.0 |
| PRO-057 · Restore Proof | restaurar snapshot de teste | R5 | V1.0 |
| PRO-058 · Deployment Review | avaliar mudança em staging | R5 | V1.0 |
| PRO-059 · Resilience Budget | dimensionar capacidade e custo | R5 | V1.0 |
| PRO-060 · Cloud Assurance | entregar plano operacional | R6 | V1.0 |

## PRO-061 a PRO-070 | Contratos profissionais · Incident Response

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-061 · Incident Intake | classificar incidente sintético | R3 | V0.5 |
| PRO-062 · Affected Assets | delimitar impacto | R3 | V0.5 |
| PRO-063 · Containment Choice | comparar alternativas defensivas | R3 | V0.5 |
| PRO-064 · Evidence Hold | preservar artefatos do caso | R4 | V1.0 |
| PRO-065 · Recovery Plan | ordenar restauração de dependências | R4 | V1.0 |
| PRO-066 · Communication Tree | definir comunicação interna fictícia | R4 | V1.0 |
| PRO-067 · Business Continuity | priorizar serviços essenciais | R5 | V1.0 |
| PRO-068 · Recovery Test | validar serviço restaurado | R5 | V1.0 |
| PRO-069 · Lessons Learned | produzir revisão sem culpa | R5 | V1.0 |
| PRO-070 · Response Exercise | concluir exercício de mesa | R6 | V1.0 |

## PRO-071 a PRO-080 | Contratos profissionais · Threat Intelligence & Forensics

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-071 · Source Confidence | classificar fontes fictícias | R4 | V0.5 |
| PRO-072 · Timeline Merge | conciliar registros sintéticos | R4 | V0.5 |
| PRO-073 · Artifact Registry | documentar cadeia de custódia | R4 | V0.5 |
| PRO-074 · Hypothesis Test | refutar hipótese com evidências | R5 | V1.0 |
| PRO-075 · Trend Brief | comparar eventos do mundo | R5 | V1.0 |
| PRO-076 · Attribution Limits | explicar incerteza de associação | R5 | V1.0 |
| PRO-077 · Evidence Transfer | validar entrega de artefatos | R6 | V1.0 |
| PRO-078 · Executive Summary | traduzir achados para diretoria | R6 | V1.0 |
| PRO-079 · Case Peer Review | revisar conclusões de colega | R6 | V1.0 |
| PRO-080 · Intelligence Package | entregar relatório fundamentado | R7 | V1.0 |

## PRO-081 a PRO-090 | Contratos profissionais · Software & Research Engineering

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-081 · Tool Specification | especificar utilitário virtual | R4 | V0.5 |
| PRO-082 · Fixture Design | criar casos sintéticos de teste | R4 | V0.5 |
| PRO-083 · Report Generator | automatizar relatório interno | R4 | V0.5 |
| PRO-084 · Resource Meter | medir orçamento do script | R5 | V1.0 |
| PRO-085 · Version Conflict | conciliar versões de pacote | R5 | V1.0 |
| PRO-086 · Peer Review | revisar mudanças e testes | R5 | V1.0 |
| PRO-087 · Compatibility Matrix | validar versões de API virtual | R6 | V1.0 |
| PRO-088 · Release Notes | documentar versão do utilitário | R6 | V1.0 |
| PRO-089 · Maintenance Ticket | corrigir regressão de pacote | R6 | V1.0 |
| PRO-090 · Research Reproduction | reproduzir resultado independente | R7 | V1.0 |

## PRO-091 a PRO-100 | Contratos profissionais · Architecture & Leadership

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| PRO-091 · Risk Register | criar inventário de riscos | R5 | V1.0 |
| PRO-092 · Control Ownership | atribuir responsabilidade | R5 | V1.0 |
| PRO-093 · Hiring Panel | avaliar portfólio virtual | R5 | V1.0 |
| PRO-094 · Budget Defense | defender orçamento de proteção | R6 | V1.0 |
| PRO-095 · Vendor Decision | comparar fornecedores fictícios | R6 | V1.0 |
| PRO-096 · Policy Council | aprovar política com justificativa | R6 | V1.0 |
| PRO-097 · Crisis Exercise | coordenar exercício de equipe | R7 | V1.0 |
| PRO-098 · Audit Readiness | reunir evidências de controle | R7 | V1.0 |
| PRO-099 · Strategy Review | revisar indicadores de segurança | R7 | V1.0 |
| PRO-100 · CISO Portfolio | apresentar estratégia anual simulada | R8 | V1.0 |

## INV-001 a INV-010 | Investigações · Digital Traces

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| INV-001 · Missing Message | localizar mensagem em corpus fictício | R1 | V0.1 |
| INV-002 · Timestamp Puzzle | ordenar eventos por horário | R1 | V0.2 |
| INV-003 · Duplicate File | comparar integridade de arquivos | R1 | V0.2 |
| INV-004 · Unknown Alias | correlacionar identidades fictícias | R2 | V0.5 |
| INV-005 · Mailbox Conflict | identificar contradição documentada | R2 | V0.5 |
| INV-006 · Broken Record | recuperar registro por snapshot | R2 | V0.5 |
| INV-007 · Custody Card | registrar origem de evidência | R3 | V0.5 |
| INV-008 · False Lead | rejeitar pista sem suporte | R3 | V0.5 |
| INV-009 · Case Timeline | consolidar linha do tempo | R3 | V0.5 |
| INV-010 · Trace Report | entregar conclusão com confiança | R4 | V1.0 |

## INV-011 a INV-020 | Investigações · Corporate Incidents

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| INV-011 · Vanished Invoice | reconciliar documento e ledger virtual | R2 | V0.5 |
| INV-012 · Access Dispute | comparar registros de autorização | R2 | V0.5 |
| INV-013 · Service Failure | separar causa e coincidência | R2 | V0.5 |
| INV-014 · Release Mystery | correlacionar falha e versão | R3 | V0.5 |
| INV-015 · Vendor Trail | mapear relações fictícias | R3 | V0.5 |
| INV-016 · Deleted Ticket | reconstruir histórico de tarefa | R3 | V0.5 |
| INV-017 · Inventory Loss | reconciliar estoque e transações | R4 | V1.0 |
| INV-018 · Insider Question | avaliar hipóteses sem acusação indevida | R4 | V1.0 |
| INV-019 · Control Evidence | validar evidências de auditoria | R4 | V1.0 |
| INV-020 · Corporate Case | apresentar relatório ao cliente | R5 | V1.0 |

## INV-021 a INV-030 | Investigações · Synthetic OSINT

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| INV-021 · Public Registry | comparar registros públicos inventados | R3 | V0.5 |
| INV-022 · News Contradiction | verificar versões de notícia interna | R3 | V0.5 |
| INV-023 · Organization Map | mapear vínculos declarados fictícios | R3 | V0.5 |
| INV-024 · Source Reliability | atribuir confiança justificada | R4 | V1.0 |
| INV-025 · Image Metadata | interpretar metadados sintéticos | R4 | V1.0 |
| INV-026 · Chronology Gap | identificar lacuna de timeline | R4 | V1.0 |
| INV-027 · Rumor Check | refutar rumor do mundo | R5 | V1.0 |
| INV-028 · Name Collision | separar aliases semelhantes | R5 | V1.0 |
| INV-029 · Research Ethics | delimitar coleta permitida | R5 | V1.0 |
| INV-030 · Open Source Brief | entregar síntese de fontes internas | R6 | V1.0 |

## INV-031 a INV-040 | Investigações · Financial & Market Cases

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| INV-031 · Price Spike | explicar variação no Exchange | R4 | V0.5 |
| INV-032 · Missing Asset | reconciliar propriedade virtual | R4 | V0.5 |
| INV-033 · Ledger Difference | identificar diferença contábil | R4 | V1.0 |
| INV-034 · Trade Sequence | ordenar eventos de negociação | R5 | V1.0 |
| INV-035 · Suspicious Pattern | classificar anomalia sem condenação | R5 | V1.0 |
| INV-036 · Escrow Dispute | avaliar evidências de entrega | R5 | V1.0 |
| INV-037 · Supplier Chain | reconstruir fluxo de peças | R6 | V1.0 |
| INV-038 · Market Review | propor controle contra abuso | R6 | V1.0 |
| INV-039 · Compensation Case | validar ajuste compensatório | R6 | V1.0 |
| INV-040 · Economic Evidence | entregar conclusão auditável | R7 | V1.0 |

## INV-041 a INV-050 | Investigações · Complex Investigations

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| INV-041 · Multiple Clocks | alinhar registros de sistemas virtuais | R5 | V1.0 |
| INV-042 · Conflicting Witnesses | avaliar depoimentos inventados | R5 | V1.0 |
| INV-043 · Cross Region Case | correlacionar eventos entre regiões | R5 | V1.0 |
| INV-044 · Evidence Integrity | detectar adulteração sintética | R6 | V1.0 |
| INV-045 · Partial Recovery | explicar limites do material recuperado | R6 | V1.0 |
| INV-046 · Parallel Incidents | separar casos independentes | R6 | V1.0 |
| INV-047 · Uncertain Origin | classificar hipóteses por confiança | R7 | V1.0 |
| INV-048 · Peer Challenge | revisar tese adversária | R7 | V1.0 |
| INV-049 · Case Presentation | defender evidências perante banca | R7 | V1.0 |
| INV-050 · Master Investigator | resolver caso inédito multidomínio | R8 | V1.0 |

## CLN-001 a CLN-010 | Operações clandestinas fictícias · Relay Dilemmas

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CLN-001 · Anonymous Offer | avaliar proposta ficcional e limites | R1 | V0.2 |
| CLN-002 · Questionable Brief | identificar conflito ético | R1 | V0.2 |
| CLN-003 · The Sealed Archive | escolher destinação de arquivo sintético | R1 | V0.5 |
| CLN-004 · A Favor Owed | negociar obrigação narrativa | R2 | V0.5 |
| CLN-005 · Missing Consent | recusar tarefa sem autorização virtual | R2 | V0.5 |
| CLN-006 · The Broker | comparar credibilidade de NPCs | R2 | V0.5 |
| CLN-007 · Two Messages | decidir qual contato confiar | R3 | V1.0 |
| CLN-008 · Safe Exit | encerrar contrato fictício com consequência | R3 | V1.0 |
| CLN-009 · Reputation Split | avaliar efeito entre facções | R3 | V1.0 |
| CLN-010 · Relay Decision | documentar escolha e aceitar desfecho | R4 | V1.0 |

## CLN-011 a CLN-020 | Operações clandestinas fictícias · Corporate Shadows

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CLN-011 · Rival Proposal | avaliar contrato de facção fictícia | R2 | V0.5 |
| CLN-012 · Confidential Claim | verificar alegação em dados sintéticos | R2 | V0.5 |
| CLN-013 · The Decoy File | classificar artefato narrativo | R2 | V0.5 |
| CLN-014 · Broken Alliance | negociar encerramento de vínculo | R3 | V0.5 |
| CLN-015 · Competing Truths | comparar relatos contraditórios | R3 | V0.5 |
| CLN-016 · The Protected Witness | preservar privacidade fictícia | R3 | V0.5 |
| CLN-017 · Disclosure Clock | escolher canal de comunicação | R4 | V1.0 |
| CLN-018 · Ethical Divide | justificar decisão sob pressão | R4 | V1.0 |
| CLN-019 · Lost Opportunity | aceitar custo de recusa | R4 | V1.0 |
| CLN-020 · Shadow Verdict | resolver dilema corporativo | R5 | V1.0 |

## CLN-021 a CLN-030 | Operações clandestinas fictícias · Underground Networks

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CLN-021 · Faction Invitation | avaliar condições de ingresso | R3 | V0.5 |
| CLN-022 · The False Broker | reconhecer inconsistência de narrativa | R3 | V0.5 |
| CLN-023 · Trust Token | validar credencial de jogo | R3 | V0.5 |
| CLN-024 · The Neutral Ground | negociar encontro virtual | R4 | V1.0 |
| CLN-025 · Resource Conflict | dividir recursos simulados | R4 | V1.0 |
| CLN-026 · Broken Contract | resolver disputa por evidência | R4 | V1.0 |
| CLN-027 · The Informant | avaliar confiança sem revelar dado real | R5 | V1.0 |
| CLN-028 · A Line Crossed | decidir resposta a violação fictícia | R5 | V1.0 |
| CLN-029 · Exit Agreement | negociar saída de facção | R5 | V1.0 |
| CLN-030 · Underground Ledger | reconciliar consequências narrativas | R6 | V1.0 |

## CLN-031 a CLN-040 | Operações clandestinas fictícias · Final Allegiances

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| CLN-031 · The Price of Loyalty | avaliar custo de aliança | R5 | V1.0 |
| CLN-032 · Parallel Promises | resolver compromissos incompatíveis | R5 | V1.0 |
| CLN-033 · Hidden Agenda | identificar contradições em documentos fictícios | R5 | V1.0 |
| CLN-034 · The Public Interest | comparar impacto de escolhas | R6 | V1.0 |
| CLN-035 · The Last Broker | negociar acordo final | R6 | V1.0 |
| CLN-036 · No Collateral | priorizar proteção de NPCs | R6 | V1.0 |
| CLN-037 · The Refusal | manter limites sob pressão | R7 | V1.0 |
| CLN-038 · Second Chance | reconstruir confiança | R7 | V1.0 |
| CLN-039 · The Reckoning | responder por decisões narrativas | R7 | V1.0 |
| CLN-040 · Independent Legacy | concluir arco sem instruções reais de crime | R8 | V1.0 |

## ACA-001 a ACA-010 | ZERO Academy · Foundations

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-001 · Identify Components | classificar componentes de PC | R1 | V0.1 |
| ACA-002 · Boot Sequence | ordenar eventos de inicialização virtual | R1 | V0.1 |
| ACA-003 · File Types | organizar arquivos por função | R1 | V0.1 |
| ACA-004 · Directory Tree | construir estrutura de pastas | R2 | V0.1 |
| ACA-005 · User Roles | atribuir permissões básicas | R2 | V0.1 |
| ACA-006 · Process Budget | interpretar consumo de recursos | R2 | V0.2 |
| ACA-007 · Storage Health | diagnosticar condição virtual | R3 | V0.2 |
| ACA-008 · System Settings | configurar preferências | R3 | V0.5 |
| ACA-009 · Backup Basics | validar cópia de segurança | R3 | V0.5 |
| ACA-010 · Foundation Exam | resolver caso básico inédito | R4 | V1.0 |

## ACA-011 a ACA-020 | ZERO Academy · Networks

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-011 · Network Nodes | identificar nós e enlaces | R1 | V0.2 |
| ACA-012 · Address Concepts | classificar endereços fictícios | R1 | V0.2 |
| ACA-013 · Name Resolution | explicar resolução interna | R1 | V0.2 |
| ACA-014 · Protocol Roles | associar protocolos a funções | R2 | V0.5 |
| ACA-015 · Latency Lab | comparar latência sintética | R2 | V0.5 |
| ACA-016 · Bandwidth Lab | diagnosticar capacidade | R2 | V0.5 |
| ACA-017 · Network Map | produzir diagrama virtual | R3 | V0.5 |
| ACA-018 · Segment Rules | validar regras permitidas | R3 | V0.5 |
| ACA-019 · Connection Diagnosis | explicar falha de laboratório | R3 | V0.5 |
| ACA-020 · Network Exam | resolver topologia nova | R4 | V1.0 |

## ACA-021 a ACA-030 | ZERO Academy · Programming

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-021 · Variables | produzir relatório com variáveis | R2 | V0.5 |
| ACA-022 · Conditions | classificar estados fictícios | R2 | V0.5 |
| ACA-023 · Loops | enumerar objetos virtuais com limite | R2 | V0.5 |
| ACA-024 · Functions | reutilizar lógica de relatório | R3 | V0.5 |
| ACA-025 · Collections | organizar inventário em lista | R3 | V0.5 |
| ACA-026 · Error Handling | tratar falha de entrada | R3 | V0.5 |
| ACA-027 · Unit Tests | escrever testes positivos e negativos | R4 | V1.0 |
| ACA-028 · Resource Limits | respeitar cota do interpretador | R4 | V1.0 |
| ACA-029 · Refactoring | preservar comportamento ao simplificar | R4 | V1.0 |
| ACA-030 · Code Exam | entregar utilitário testado | R5 | V1.0 |

## ACA-031 a ACA-040 | ZERO Academy · Web & Data

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-031 · Request Model | explicar fluxo de API virtual | R2 | V0.5 |
| ACA-032 · Data Types | validar esquema sintético | R2 | V0.5 |
| ACA-033 · Record Query | consultar tabela fictícia | R2 | V0.5 |
| ACA-034 · Data Integrity | testar restrições de registros | R3 | V0.5 |
| ACA-035 · API Contract | documentar entradas e saídas | R3 | V0.5 |
| ACA-036 · Access Matrix | validar visibilidade de objetos | R3 | V0.5 |
| ACA-037 · Input Validation | testar casos limite | R4 | V1.0 |
| ACA-038 · Versioned Schema | reconciliar versões de dados | R4 | V1.0 |
| ACA-039 · Application Test | validar regressão de app | R4 | V1.0 |
| ACA-040 · Web Data Exam | entregar aplicação fictícia correta | R5 | V1.0 |

## ACA-041 a ACA-050 | ZERO Academy · Crypto & Identity

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-041 · Integrity Check | comparar hashes de artefatos sintéticos | R2 | V0.5 |
| ACA-042 · Identity Roles | distinguir identidade e autorização | R2 | V0.5 |
| ACA-043 · Permission Matrix | construir matriz de papéis | R2 | V0.5 |
| ACA-044 · Key Concepts | explicar função de chaves conceituais | R3 | V0.5 |
| ACA-045 · Signature Lab | validar assinatura de jogo | R3 | V0.5 |
| ACA-046 · Certificate Model | interpretar validade virtual | R3 | V0.5 |
| ACA-047 · Secret Hygiene | classificar armazenamento seguro em cenário | R4 | V1.0 |
| ACA-048 · Session Lifecycle | ordenar estados de sessão | R4 | V1.0 |
| ACA-049 · Identity Review | testar acesso permitido e negado | R4 | V1.0 |
| ACA-050 · Identity Exam | resolver caso de identidade virtual | R5 | V1.0 |

## ACA-051 a ACA-060 | ZERO Academy · Defensive Security

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-051 · Risk Basics | classificar ativos e riscos | R3 | V0.5 |
| ACA-052 · Log Reading | interpretar registros sintéticos | R3 | V0.5 |
| ACA-053 · Alert Triage | priorizar alertas de laboratório | R3 | V0.5 |
| ACA-054 · Policy Review | identificar desvio de política | R4 | V1.0 |
| ACA-055 · False Positive | justificar classificação | R4 | V1.0 |
| ACA-056 · Control Testing | validar efeito de proteção | R4 | V1.0 |
| ACA-057 · Incident Severity | atribuir severidade | R5 | V1.0 |
| ACA-058 · Recovery Checklist | ordenar restauração | R5 | V1.0 |
| ACA-059 · Postmortem | documentar causa e melhoria | R5 | V1.0 |
| ACA-060 · Defense Exam | resolver incidente inédito | R6 | V1.0 |

## ACA-061 a ACA-070 | ZERO Academy · Authorized Assessment

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-061 · Rules of Engagement | definir escopo e autorização fictícia | R3 | V0.5 |
| ACA-062 · Asset Classification | inventariar laboratório | R3 | V0.5 |
| ACA-063 · Control Evaluation | registrar resultado de verificação abstrata | R3 | V0.5 |
| ACA-064 · Evidence Standard | selecionar prova suficiente | R4 | V1.0 |
| ACA-065 · Risk Severity | justificar classificação de achado | R4 | V1.0 |
| ACA-066 · Remediation Advice | propor controle defensivo | R4 | V1.0 |
| ACA-067 · Retest | validar mudança de estado virtual | R5 | V1.0 |
| ACA-068 · Disclosure Ethics | escolher processo responsável | R5 | V1.0 |
| ACA-069 · Report Structure | organizar relatório de auditoria | R5 | V1.0 |
| ACA-070 · Assessment Exam | avaliar cenário fechado novo | R6 | V1.0 |

## ACA-071 a ACA-080 | ZERO Academy · Forensics & OSINT

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-071 · Evidence Origin | registrar procedência | R3 | V0.5 |
| ACA-072 · Custody Chain | validar cadeia de custódia | R3 | V0.5 |
| ACA-073 · Timeline Basics | ordenar eventos sintéticos | R3 | V0.5 |
| ACA-074 · Source Comparison | comparar fontes internas | R4 | V1.0 |
| ACA-075 · Confidence Scale | atribuir incerteza | R4 | V1.0 |
| ACA-076 · Metadata Review | interpretar metadados fictícios | R4 | V1.0 |
| ACA-077 · Hypothesis Review | testar explicação alternativa | R5 | V1.0 |
| ACA-078 · Privacy Boundary | delimitar coleta fictícia | R5 | V1.0 |
| ACA-079 · Case Writing | produzir relatório fundamentado | R5 | V1.0 |
| ACA-080 · Investigator Exam | concluir investigação inédita | R6 | V1.0 |

## ACA-081 a ACA-090 | ZERO Academy · Cloud & DevSecOps

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-081 · Service Model | classificar serviços virtuais | R4 | V1.0 |
| ACA-082 · Infrastructure Map | mapear dependências | R4 | V1.0 |
| ACA-083 · Least Privilege | revisar papéis de laboratório | R4 | V1.0 |
| ACA-084 · Build Lifecycle | ordenar etapas de pipeline | R5 | V1.0 |
| ACA-085 · Artifact Verification | validar pacote sintético | R5 | V1.0 |
| ACA-086 · Staging Test | executar validação de release | R5 | V1.0 |
| ACA-087 · Budget Control | alocar recursos virtuais | R6 | V1.0 |
| ACA-088 · Backup Recovery | restaurar snapshot de teste | R6 | V1.0 |
| ACA-089 · Rollback | comprovar retorno de versão | R6 | V1.0 |
| ACA-090 · Cloud Exam | resolver caso de infraestrutura | R7 | V1.0 |

## ACA-091 a ACA-100 | ZERO Academy · Leadership & Ethics

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| ACA-091 · Stakeholder Map | identificar responsáveis fictícios | R4 | V1.0 |
| ACA-092 · Risk Communication | explicar risco a público leigo | R4 | V1.0 |
| ACA-093 · Prioritization | ordenar trabalho por impacto | R4 | V1.0 |
| ACA-094 · Conflict of Interest | justificar decisão ética | R5 | V1.0 |
| ACA-095 · Budget Planning | alocar orçamento virtual | R5 | V1.0 |
| ACA-096 · Team Review | avaliar contribuição com evidências | R5 | V1.0 |
| ACA-097 · Incident Tabletop | coordenar exercício de mesa | R6 | V1.0 |
| ACA-098 · Governance Policy | definir responsabilidade | R6 | V1.0 |
| ACA-099 · Mentoring | explicar conceito a aprendiz NPC | R6 | V1.0 |
| ACA-100 · Leadership Exam | defender decisão multidisciplinar | R7 | V1.0 |

## COO-001 a COO-010 | Cooperativas · Team Operations

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| COO-001 · Shared Inventory | consolidar mapa de ativos em equipe | R2 | V0.5 |
| COO-002 · Split Roles | distribuir responsabilidades | R2 | V0.5 |
| COO-003 · Joint Report | integrar evidências de dois papéis | R2 | V0.5 |
| COO-004 · Shift Relay | transferir tarefa com contexto | R3 | V0.5 |
| COO-005 · Parallel Review | revisar controles em paralelo | R3 | V0.5 |
| COO-006 · Dependency Handoff | coordenar entrega entre papéis | R3 | V0.5 |
| COO-007 · Team Budget | negociar recursos limitados | R4 | V1.0 |
| COO-008 · Peer Validation | validar entrega de colega | R4 | V1.0 |
| COO-009 · Reconnect Drill | retomar operação interrompida | R4 | V1.0 |
| COO-010 · Team Completion | entregar resultado coletivo auditado | R5 | V1.0 |

## COO-011 a COO-020 | Cooperativas · Defense Response

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| COO-011 · Alert Storm | coordenar triagem em equipe | R3 | V0.5 |
| COO-012 · Shared Timeline | consolidar eventos sintéticos | R3 | V0.5 |
| COO-013 · Service Priority | decidir prioridade de recuperação | R3 | V0.5 |
| COO-014 · Containment Council | comparar planos defensivos | R4 | V1.0 |
| COO-015 · Evidence Team | preservar registros em conjunto | R4 | V1.0 |
| COO-016 · Recovery Shift | restaurar serviços de teste | R4 | V1.0 |
| COO-017 · Client Update | entregar comunicação conjunta | R5 | V1.0 |
| COO-018 · Business Handoff | validar dependências restauradas | R5 | V1.0 |
| COO-019 · After Action | realizar revisão de equipe | R5 | V1.0 |
| COO-020 · Defense Marathon | concluir incidente cooperativo complexo | R6 | V1.0 |

## COO-021 a COO-030 | Cooperativas · Engineering Projects

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| COO-021 · Team Specification | negociar requisitos de app virtual | R4 | V0.5 |
| COO-022 · Module Split | atribuir módulos de software | R4 | V0.5 |
| COO-023 · Integration Fixture | construir conjunto de testes | R4 | V0.5 |
| COO-024 · Review Board | revisar mudanças cruzadas | R5 | V1.0 |
| COO-025 · Resource Planning | dimensionar hardware virtual | R5 | V1.0 |
| COO-026 · Release Candidate | validar versão em equipe | R5 | V1.0 |
| COO-027 · Deployment Window | coordenar release simulado | R6 | V1.0 |
| COO-028 · Rollback Team | comprovar recuperação | R6 | V1.0 |
| COO-029 · Maintenance Sprint | resolver regressões de pacote | R6 | V1.0 |
| COO-030 · Enterprise Delivery | entregar projeto com aceite coletivo | R7 | V1.0 |

## COO-031 a COO-040 | Cooperativas · Research & Crisis

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| COO-031 · Research Division | dividir hipóteses entre pesquisadores | R5 | V1.0 |
| COO-032 · Independent Proof | reproduzir resultado de colega | R5 | V1.0 |
| COO-033 · Evidence Debate | avaliar conclusões divergentes | R5 | V1.0 |
| COO-034 · City Tabletop | coordenar crise de mesa | R6 | V1.0 |
| COO-035 · Multi Organization | alinhar organizações fictícias | R6 | V1.0 |
| COO-036 · Economic Pressure | priorizar resposta sob orçamento | R6 | V1.0 |
| COO-037 · Public Briefing | entregar comunicado com incerteza | R7 | V1.0 |
| COO-038 · Peer Challenge | validar caso de equipe adversária | R7 | V1.0 |
| COO-039 · Recovery Coalition | coordenar restauração regional | R7 | V1.0 |
| COO-040 · Grand Operation | concluir capstone cooperativo | R8 | V1.0 |

## EVT-001 a EVT-010 | Eventos e competições · Cyber Arena & CTF

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| EVT-001 · Logic Sprint | resolver puzzle isolado validado | R2 | V0.5 |
| EVT-002 · Network Puzzle | interpretar topologia sintética | R2 | V0.5 |
| EVT-003 · Evidence Race | ordenar evidências com precisão | R2 | V0.5 |
| EVT-004 · Policy Duel | comparar controles em cenário simétrico | R3 | V0.5 |
| EVT-005 · Code Relay | entregar solução virtual testada | R3 | V0.5 |
| EVT-006 · Triage Cup | classificar alertas com rubrica comum | R3 | V1.0 |
| EVT-007 · Forensic Cup | reconstruir caso inédito | R4 | V1.0 |
| EVT-008 · Team Defense | proteger serviço de arena virtual | R4 | V1.0 |
| EVT-009 · Research Challenge | validar hipótese em laboratório fechado | R4 | V1.0 |
| EVT-010 · Arena Finals | concluir rodada competitiva autoritativa | R5 | V1.0 |

## EVT-011 a EVT-020 | Eventos e competições · World Events

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| EVT-011 · Supply Shortage | adaptar orçamento a escassez fictícia | R3 | V0.5 |
| EVT-012 · Regional Outage | coordenar recuperação de serviços simulados | R3 | V0.5 |
| EVT-013 · Research Week | entregar experimento virtual | R3 | V0.5 |
| EVT-014 · Career Fair | participar de avaliação profissional | R4 | V1.0 |
| EVT-015 · Market Audit | reconciliar amostra de transações | R4 | V1.0 |
| EVT-016 · Backup Day | comprovar recuperação de snapshot | R4 | V1.0 |
| EVT-017 · Community Review | avaliar proposta de melhoria | R5 | V1.0 |
| EVT-018 · Public Trust | responder a crise reputacional fictícia | R5 | V1.0 |
| EVT-019 · Infrastructure Week | entregar plano de capacidade | R5 | V1.0 |
| EVT-020 · World Recovery | validar estabilidade regional | R6 | V1.0 |

## EVT-021 a EVT-030 | Eventos e competições · Seasonal Endgame

| ID / título (EN) | Entrega verificável (PT-BR) | Rank | Estreia |
|---|---|---|---|
| EVT-021 · Mentor Summit | avaliar trabalho de aprendiz virtual | R5 | V1.0 |
| EVT-022 · Architect League | defender arquitetura em banca | R5 | V1.0 |
| EVT-023 · Enterprise Cup | entregar contrato complexo | R5 | V1.0 |
| EVT-024 · Research Symposium | reproduzir resultado de pesquisa | R6 | V1.0 |
| EVT-025 · Guild Showcase | apresentar projeto coletivo | R6 | V1.0 |
| EVT-026 · Hardware Expo | validar montagem e benchmark virtual | R6 | V1.0 |
| EVT-027 · Ethics Forum | justificar escolha em dilema fictício | R7 | V1.0 |
| EVT-028 · Grand Investigation | resolver caso sintético multietapa | R7 | V1.0 |
| EVT-029 · Season Championship | concluir desafio ranqueado | R7 | V1.0 |
| EVT-030 · Legacy Festival | apresentar portfólio e legado de temporada | R8 | V1.0 |

# 26 | Anexo B — Auditoria do catálogo

| Categoria | V0.1 | V0.2 | V0.5 | V1.0 | Total |
|---|---|---|---|---|---|
| Campanha | 2 | 3 | 20 | 35 | 60 |
| Contratos profissionais | 2 | 3 | 40 | 55 | 100 |
| Investigações | 1 | 2 | 17 | 30 | 50 |
| Operações clandestinas fictícias | 0 | 2 | 13 | 25 | 40 |
| ZERO Academy | 5 | 5 | 35 | 55 | 100 |
| Cooperativas | 0 | 0 | 12 | 28 | 40 |
| Eventos e competições | 0 | 0 | 8 | 22 | 30 |
| Total incremental | 10 | 15 | 145 | 250 | 420 |

**Validação de documentação:** 420 IDs únicos; 42 grupos de 10; distribuição por categoria e release verificada por programa; pré-requisitos sequenciais não são publicados depois do dependente. Não equivale a teste de jogabilidade, tradução completa ou solvabilidade de conteúdo implementado. A revisão preserva todos os IDs, títulos, objetivos, ranks indicativos e estreias; inclui metadados explícitos de BRIEF, dependências e política de acesso no JSON v0.2.0.

# 27 | Anexo C — Auditoria ZT-GDD-REV-001

**Veredito original: CORRECTION REQUIRED.** A correção é documental; não constitui exploração real de falhas, auditoria de código ou prova de segurança. Os níveis CRITICAL/HIGH indicam risco de design se omitido, não vulnerabilidade confirmada de software existente.

| ID / risco | Evidência no original | Correção proposta v0.2.0 | Gate residual |
|---|---|---|---|
| F01 / HIGH | §5, §6, catálogo: Ranks recomendados podem bloquear aprendizado; exame Foundation ACA-010 só V1.0. | Acesso por competência, avaliação parcial e capstones sem circularidade. | Validação de alcance em fixtures de progressão. |
| F02 / HIGH | §21, catálogo: Slice exige rede/onboarding/upgrade posterior; apps não cobrem PRO-001, INV-001 e ACA hardware. | Onboarding mínimo e 9 apps, recortes de carreira. | E2E das 10 missões. |
| F03 / HIGH | §22: Dependências macro agregam ledger, missão, Steam e multiplayer sem nós auditáveis. | DAG D00–D19 com recortes e gates. | Decomposição em WOs e DAG fino na Etapa 03. |
| F04 / HIGH | §5, anexo: Nenhum brief R9/R10; capstones citados sem contrato de aceite. | CAP-R08/09/10 e loops de elite separados dos 420. | Playtest e testes de promoção. |
| F05 / HIGH | §21: 170 incluem 12 COO e 8 EVT; online piloto pode falhar e ainda parecer pronto. | Gate D16 exige modo e escopo demonstrados; adiar ou ratificar alteração. | Piloto e evidência de modos. |
| F06 / HIGH | §12: Ciclo econômico ambíguo; preço, renda, tempo e recuperação sem modelo testável. | Ciclo por entregas, exemplo, hipóteses e stress econômico. | Simulação de balanceamento. |
| F07 / HIGH | §12, §14: Q de 0–100 e multiplicador de pagamento ambíguo em CAM-031; arredondamento e dupla recompensa indefinidos. | Q/100, inteiros, teto, grupo de recompensa e divisão. | Testes de conservação e limites. |
| F08 / CRITICAL | §9, §19: Isolamento e revisão genéricos, sem conjunto de obrigações verificáveis. | P1–P6 e capacidades, fronteiras e revisão. | Threat model, testes de sandbox; não validado por código. |
| F09 / CRITICAL | §12–13, §19: Ledger e inventário exigem prova de atomicidade/recuperação; RPO geral não basta para saldo confirmado. | Gate de durabilidade/ACK, reconciliação e modo read-only. | Testes de corrida, falha e restore. |
| F10 / HIGH | §16–17: Baixa população, desconexão, divisão de prêmio e dissolução de guilda subespecificadas. | Matriz de modos, contrato de contribuição, governança e fallback. | Testar co-op, matchmaking, disputas. |
| F11 / HIGH | §14, anexo: Brief de uma linha não demonstra solvabilidade nem variedade. | Pipeline de conteúdo, oráculo, assinatura, 100 seeds/template. | QA de cada missão e procedural. |
| F12 / HIGH | §6: Aprendizado declarado sem medição de transferência e revisão por etapa. | Avaliação pré/pós e revisão humana. | Recrutar especialistas e playtest. |
| F13 / HIGH | §13: Market Steam elegível apenas condicionalmente; faltam estados de indisponibilidade. | Separar inventários, reconciliação e gate externo. | Verificar permissões do AppID. |
| F14 / HIGH | §20: Grande escopo sem estimativa de esforço por missão e custo de operação. | Cenários paramétricos, medição no slice, teto online. | Capacidade real e orçamento. |
| F15 / MEDIUM | §4: Emprego e promoção pouco modelados como atividades jogáveis. | Máquina de estados e trabalho por entrega. | Playtest de ambas carreiras. |
| F16 / MEDIUM | §3, §7: Camadas de mundo e promoção de detalhe sem contrato de preservação. | L0/L1/L2, verdade global vs instância e relógio lógico. | Replay e benchmark. |
| F17 / MEDIUM | §8, §18: Disponibilidade de apps por release e estados de UI insuficientes. | Matriz 9/13/26/30, estados e brief visual. | Design System e QA de acessibilidade. |
| F18 / MEDIUM | §10, §15: Autenticidade pode virar coleção de minigames; GHOST pode trivializar prova. | Cyber Case Chain e ajuda escalonada. | Provar integração e diversão no slice. |
| F19 / MEDIUM | §11: Hardware pode se tornar trava de competência. | Laboratório de referência, empréstimo, normalização arena. | Playtest sem upgrades. |
| F20 / MEDIUM | §13, §17: Leilões, serviços, UGC e disputas sem política concreta. | Estados, curadoria, escrow, regras e recurso. | Teste de mercado, moderação e compliance. |
| F21 / MEDIUM | §17, §20: Endgame e temporadas dependem de retenção sem critérios de longevidade. | Loops completos, arquivo de treino, não FOMO. | Playtest longitudinal e dados reais. |
| F22 / MEDIUM | §18, §24: Inglês primeiro precisa de aceite de strings; decisões comerciais e nome não ratificados. | Gate de localização e registro de pendências. | Revisão nativa e clearance. |

**Veredito revisado: PROPOSED / READY FOR OWNER REVIEW.** As 22 observações recebem correção de design ou gate de validação; não há aprovação de implementação. A decisão de aceitar o GDD revisado pertence ao proprietário. Não reduzir a meta de conteúdo, remover carreira, sacrificar English-first ou abolir multiplayer. Se requisitos condicionais forem rejeitados, registrar decisão e reavaliar escopo, sem alteração silenciosa.

**Decisões novas propostas (GD-D11–D18):** D11 avaliações sem rank circular, capstones fora dos 420; D12 divisão de relógio econômico e real; D13 catálogo com estado BRIEF; D14 matriz de apps e subgates; D15 economia inteira e limites de comércio; D16 endgame e Cyber Case Chain; D17 UGC curado e provas HIGH_ASSURANCE; D18 piloto limitado por orçamento e conteúdo homologado. Todas PROPOSED, não ADRs de implementação.

**Referências reconsultadas para esta revisão:** documentação Steam Inventory Schema (marketable/tradable) e Steam Leaderboards (Trusted impede escrita direta pelo cliente; serviço autorizado publica), URLs R2/R4 da seção 24. NIST NICE Resource Center (R1) como referência pedagógica, não certificação. Não houve validação de conta, AppID, regras específicas de elegibilidade, parecer jurídico ou consulta a especialistas.

**Source check:** Markdown e JSON v0.1.0 lidos integralmente por seções e análise automatizada. PDF anterior não foi tratado como fonte normativa; revisão usa Markdown editável e catálogo estruturado. SHA-256 Markdown original: c9893262182e473bace089cf0f8268c981d721341bb1ca1bb8a4bbb840a9843f. SHA-256 catálogo original: 3f010ea109d621accb8c9ec8fb2ece87d1e1c042b4c89c3d294db4b89ee0a015.

**Validação estática:** 420 IDs originais preservados, 42 grupos, distribuição 10/25/170/420, pré-requisitos sequenciais existentes e disponíveis no marco, 36 módulos preservados; grafo D00–D19 sem ciclos, matriz de apps 9/13/26/30 sem duplicação. Nenhuma missão é considerada implementada ou homologada.
