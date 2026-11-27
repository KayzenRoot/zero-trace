using System;
using System.Collections.Generic;
using System.Globalization;

namespace ZeroTrace.Presentation
{
    /// <summary>All user-facing copy for the technical slice in the two supported locales.</summary>
    public static class LocalPrototypeText
    {
        private static readonly Dictionary<string, string> English = new Dictionary<string, string>
        {
            ["launch"] = "Start local prototype",
            ["notice"] = "LOCAL TECHNICAL PROTOTYPE — NOT RELEASE CONTENT",
            ["roomHintOff"] = "Look at the computer and press E to start it.",
            ["roomHintShutdown"] = "Computer is shut down. Look at it and press E to restart.",
            ["roomHintBooting"] = "Computer is starting…",
            ["desktopTitle"] = "NULL OS / LOCAL SESSION",
            ["prototypeBadge"] = "TECHNICAL PROTOTYPE · OFFLINE",
            ["clock"] = "SIMULATED TIME 08:{0}",
            ["mail"] = "Mail",
            ["files"] = "File Manager",
            ["academy"] = "Academy",
            ["terminal"] = "Terminal",
            ["mailTitle"] = "Local inbox",
            ["mailFrom"] = "From: shift coordinator (fictional)",
            ["mailBody"] = "Welcome to the local training session. Open File Manager and locate recovery_manifest.txt. The exercise uses synthetic files only.",
            ["filesTitle"] = "Synthetic workspace",
            ["exercisePrompt"] = "Exercise: select the recovery index named in the inbox message, then submit it.",
            ["selectFile"] = "Select a synthetic file:",
            ["submit"] = "Submit selection",
            ["incorrect"] = "Not quite. Re-read the inbox hint and try another file.",
            ["correct"] = "Correct. The requested synthetic recovery index is selected.",
            ["alreadyComplete"] = "Exercise complete. Restart it to practice again.",
            ["invalidSelection"] = "That file is not part of this exercise. Choose one of the listed files.",
            ["attemptLimit"] = "The attempt limit was reached. Restart the exercise before submitting again.",
            ["saveFailed"] = "Progress was not saved. Your previous progress is unchanged. Try again to retry.",
            ["restart"] = "Restart exercise",
            ["academyTitle"] = "ZERO Academy · Local lesson",
            ["academyBody"] = "Training sandbox: inspect a fictional briefing, use only the synthetic File Manager, and compare your choice with the message. No real files, accounts, hosts, or networks are used.",
            ["close"] = "Close window",
            ["power"] = "Power off computer",
            ["booting"] = "Starting fictional NULL OS…",
            ["computerOff"] = "COMPUTER: OFF",
            ["computerBooting"] = "COMPUTER: BOOTING",
            ["computerReady"] = "COMPUTER: READY",
            ["computerShutdown"] = "COMPUTER: SHUTDOWN",
            ["roomBadge"] = "LOCAL PROTOTYPE · OFFLINE",
            ["enterRoom"] = "ENTER LOCAL ROOM",
            ["exitRoom"] = "Return to startup",
            ["focusHint"] = "WASD move · Mouse look · E interact · Esc return",
            ["saveStatus"] = "Progress saved on this device.",
            ["notSelected"] = "No file selected.",
            ["selected"] = "Selected: {0}",
            ["exerciseCompleted"] = "COMPLETED",
            ["exercisePending"] = "IN PROGRESS",
            ["terminalTitle"] = "NULL OS Terminal · Offline",
            ["terminalIntro"] = "Read-only local interface for the fictional technical prototype. No host commands or real files are available.",
            ["terminalInputLabel"] = "Command",
            ["terminalPlaceholder"] = "Enter an allowlisted command",
            ["terminalRun"] = "Run",
            ["terminalClearOutput"] = "Clear output",
            ["terminalRunHint"] = "Enter runs a command · ↑/↓ browses session history",
            ["terminalHelp"] = "Allowed commands:\nhelp\nclear\nworld.status\nfiles.list\nfiles.read <file-id|filename.txt>\ncase.list\nacademy.status",
            ["terminalErrorPrefix"] = "ERROR: ",
            ["terminalUnavailable"] = "Terminal is available only while the computer is ready.",
            ["terminalInactive"] = "Open Terminal from the desktop before entering a command.",
            ["terminalEmpty"] = "Enter a command.",
            ["terminalInputLimit"] = "Input exceeds the 256-character limit.",
            ["terminalArgumentLimit"] = "Too many arguments. The maximum is four.",
            ["terminalInvalidCharacters"] = "Input contains unsupported characters.",
            ["terminalUnknownCommand"] = "Command not found. Type help for the allowlist.",
            ["terminalWrongArity"] = "Invalid number of arguments. Type help for command syntax.",
            ["terminalFileNotFound"] = "Synthetic file not found.",
            ["terminalOutputLimit"] = "Output exceeded the per-command limit.",
            ["terminalWorldStatus"] = "Computer: {0}\nActive app: {1}",
            ["terminalFilesHeader"] = "Synthetic files:",
            ["terminalFileHeader"] = "Synthetic file: {0}\n{1}",
            ["terminalCaseSummary"] = "Current exercise: locate recovery_manifest.txt in File Manager. No case records are created.",
            ["terminalAcademyStatus"] = "Exercise: {0}\nAttempts: {1}",
            ["terminalNoApp"] = "None"
        };

        private static readonly Dictionary<string, string> Portuguese = new Dictionary<string, string>
        {
            ["launch"] = "Iniciar protótipo local",
            ["notice"] = "PROTÓTIPO TÉCNICO LOCAL — NÃO É CONTEÚDO DE LANÇAMENTO",
            ["roomHintOff"] = "Olhe para o computador e pressione E para ligá-lo.",
            ["roomHintShutdown"] = "O computador está desligado. Olhe para ele e pressione E para reiniciar.",
            ["roomHintBooting"] = "O computador está iniciando…",
            ["desktopTitle"] = "NULL OS / SESSÃO LOCAL",
            ["prototypeBadge"] = "PROTÓTIPO TÉCNICO · OFFLINE",
            ["clock"] = "HORA SIMULADA 08:{0}",
            ["mail"] = "E-mail",
            ["files"] = "Arquivos",
            ["academy"] = "Academia",
            ["terminal"] = "Terminal",
            ["mailTitle"] = "Caixa de entrada local",
            ["mailFrom"] = "De: coordenação do turno (fictícia)",
            ["mailBody"] = "Boas-vindas à sessão de treinamento local. Abra Arquivos e localize recovery_manifest.txt. O exercício usa somente arquivos sintéticos.",
            ["filesTitle"] = "Área de trabalho sintética",
            ["exercisePrompt"] = "Exercício: selecione o índice de recuperação citado no e-mail e envie a escolha.",
            ["selectFile"] = "Selecione um arquivo sintético:",
            ["submit"] = "Enviar escolha",
            ["incorrect"] = "Ainda não. Releia a dica do e-mail e tente outro arquivo.",
            ["correct"] = "Correto. O índice sintético de recuperação solicitado foi selecionado.",
            ["alreadyComplete"] = "Exercício concluído. Reinicie para praticar novamente.",
            ["invalidSelection"] = "Esse arquivo não faz parte deste exercício. Escolha um dos arquivos listados.",
            ["attemptLimit"] = "O limite de tentativas foi atingido. Reinicie o exercício antes de enviar outra escolha.",
            ["saveFailed"] = "O progresso não foi salvo. O progresso anterior foi mantido. Tente novamente para repetir.",
            ["restart"] = "Reiniciar exercício",
            ["academyTitle"] = "ZERO Academy · Aula local",
            ["academyBody"] = "Ambiente de treinamento: leia uma orientação fictícia, use somente o gerenciador de arquivos sintéticos e compare sua escolha com a mensagem. Nenhum arquivo real, conta, host ou rede é usado.",
            ["close"] = "Fechar janela",
            ["power"] = "Desligar computador",
            ["booting"] = "Iniciando o NULL OS fictício…",
            ["computerOff"] = "COMPUTADOR: DESLIGADO",
            ["computerBooting"] = "COMPUTADOR: INICIANDO",
            ["computerReady"] = "COMPUTADOR: PRONTO",
            ["computerShutdown"] = "COMPUTADOR: DESLIGADO",
            ["roomBadge"] = "PROTÓTIPO LOCAL · OFFLINE",
            ["enterRoom"] = "ENTRAR NA SALA LOCAL",
            ["exitRoom"] = "Voltar ao início",
            ["focusHint"] = "WASD mover · Mouse olhar · E interagir · Esc voltar",
            ["saveStatus"] = "Progresso salvo neste dispositivo.",
            ["notSelected"] = "Nenhum arquivo selecionado.",
            ["selected"] = "Selecionado: {0}",
            ["exerciseCompleted"] = "CONCLUÍDO",
            ["exercisePending"] = "EM ANDAMENTO",
            ["terminalTitle"] = "Terminal NULL OS · Offline",
            ["terminalIntro"] = "Interface local somente para leitura do protótipo técnico fictício. Não há comandos do computador nem arquivos reais.",
            ["terminalInputLabel"] = "Comando",
            ["terminalPlaceholder"] = "Digite um comando permitido",
            ["terminalRun"] = "Executar",
            ["terminalClearOutput"] = "Limpar saída",
            ["terminalRunHint"] = "Enter executa · ↑/↓ percorre o histórico da sessão",
            ["terminalHelp"] = "Comandos permitidos:\nhelp\nclear\nworld.status\nfiles.list\nfiles.read <file-id|filename.txt>\ncase.list\nacademy.status",
            ["terminalErrorPrefix"] = "ERRO: ",
            ["terminalUnavailable"] = "O Terminal só fica disponível quando o computador está pronto.",
            ["terminalInactive"] = "Abra o Terminal na área de trabalho antes de digitar um comando.",
            ["terminalEmpty"] = "Digite um comando.",
            ["terminalInputLimit"] = "A entrada ultrapassa o limite de 256 caracteres.",
            ["terminalArgumentLimit"] = "Há argumentos demais. O máximo é quatro.",
            ["terminalInvalidCharacters"] = "A entrada contém caracteres não permitidos.",
            ["terminalUnknownCommand"] = "Comando não encontrado. Digite help para ver a lista permitida.",
            ["terminalWrongArity"] = "Quantidade de argumentos inválida. Digite help para ver a sintaxe.",
            ["terminalFileNotFound"] = "Arquivo sintético não encontrado.",
            ["terminalOutputLimit"] = "A saída ultrapassou o limite por comando.",
            ["terminalWorldStatus"] = "Computador: {0}\nAplicativo ativo: {1}",
            ["terminalFilesHeader"] = "Arquivos sintéticos:",
            ["terminalFileHeader"] = "Arquivo sintético: {0}\n{1}",
            ["terminalCaseSummary"] = "Exercício atual: localize recovery_manifest.txt em Arquivos. Nenhum registro de caso é criado.",
            ["terminalAcademyStatus"] = "Exercício: {0}\nTentativas: {1}",
            ["terminalNoApp"] = "Nenhum"
        };

        public static string Get(string key, string localeCode)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            var language = string.Equals(localeCode, "pt-BR", StringComparison.Ordinal) ? Portuguese : English;
            return language.TryGetValue(key, out var value) ? value : key;
        }

        public static string Format(string key, string localeCode, params object[] arguments) =>
            string.Format(CultureInfo.InvariantCulture, Get(key, localeCode), arguments);
    }
}
