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
            ["mailTitle"] = "Local inbox",
            ["mailFrom"] = "From: shift coordinator (fictional)",
            ["mailBody"] = "Welcome to the local training session. Open File Manager and locate recovery_manifest.txt. The exercise uses synthetic files only.",
            ["filesTitle"] = "Synthetic workspace",
            ["fileBriefing"] = "briefing.txt · Training note",
            ["fileRecovery"] = "recovery_manifest.txt · Recovery index",
            ["fileShift"] = "shift_note.txt · Shift note",
            ["exercisePrompt"] = "Exercise: select the recovery index named in the inbox message, then submit it.",
            ["selectFile"] = "Select a synthetic file:",
            ["submit"] = "Submit selection",
            ["incorrect"] = "Not quite. Re-read the inbox hint and try another file.",
            ["correct"] = "Correct. The requested synthetic recovery index is selected.",
            ["alreadyComplete"] = "Exercise complete. Restart it to practice again.",
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
            ["exercisePending"] = "IN PROGRESS"
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
            ["mailTitle"] = "Caixa de entrada local",
            ["mailFrom"] = "De: coordenação do turno (fictícia)",
            ["mailBody"] = "Boas-vindas à sessão de treinamento local. Abra Arquivos e localize recovery_manifest.txt. O exercício usa somente arquivos sintéticos.",
            ["filesTitle"] = "Área de trabalho sintética",
            ["fileBriefing"] = "briefing.txt · Nota de treinamento",
            ["fileRecovery"] = "recovery_manifest.txt · Índice de recuperação",
            ["fileShift"] = "shift_note.txt · Nota de turno",
            ["exercisePrompt"] = "Exercício: selecione o índice de recuperação citado no e-mail e envie a escolha.",
            ["selectFile"] = "Selecione um arquivo sintético:",
            ["submit"] = "Enviar escolha",
            ["incorrect"] = "Ainda não. Releia a dica do e-mail e tente outro arquivo.",
            ["correct"] = "Correto. O índice sintético de recuperação solicitado foi selecionado.",
            ["alreadyComplete"] = "Exercício concluído. Reinicie para praticar novamente.",
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
            ["exercisePending"] = "EM ANDAMENTO"
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
