using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZeroTrace.Domain
{
    /// <summary>One fictional file exposed by the local prototype.</summary>
    public sealed class SyntheticFile
    {
        internal SyntheticFile(string id, string fileName, string descriptionEnUs, string descriptionPtBr,
            string contentEnUs, string contentPtBr)
        {
            Id = id;
            FileName = fileName;
            DescriptionEnUs = descriptionEnUs;
            DescriptionPtBr = descriptionPtBr;
            ContentEnUs = contentEnUs;
            ContentPtBr = contentPtBr;
        }

        public string Id { get; }
        public string FileName { get; }
        public string DescriptionEnUs { get; }
        public string DescriptionPtBr { get; }
        public string ContentEnUs { get; }
        public string ContentPtBr { get; }

        public string GetDisplayLabel(string localeCode) =>
            FileName + " · " + GetDescription(localeCode);

        public string GetDescription(string localeCode) =>
            string.Equals(localeCode, "pt-BR", StringComparison.Ordinal) ? DescriptionPtBr : DescriptionEnUs;

        public string GetContent(string localeCode) =>
            string.Equals(localeCode, "pt-BR", StringComparison.Ordinal) ? ContentPtBr : ContentEnUs;
    }

    /// <summary>Immutable shared catalog for the existing synthetic exercise.</summary>
    public static class SyntheticFileCatalog
    {
        public const string BriefingId = "briefing";
        public const string RecoveryManifestId = "recovery_manifest";
        public const string ShiftNoteId = "shift_note";

        private static readonly ReadOnlyCollection<SyntheticFile> Files = Array.AsReadOnly(new[]
        {
            new SyntheticFile(BriefingId, "briefing.txt", "Training note", "Nota de treinamento",
                "Training note: this fictional workstation session uses a synthetic exercise.",
                "Nota de treinamento: esta sessão fictícia usa um exercício sintético."),
            new SyntheticFile(RecoveryManifestId, "recovery_manifest.txt", "Recovery index", "Índice de recuperação",
                "Recovery index: locate this synthetic file to complete the existing local exercise.",
                "Índice de recuperação: localize este arquivo sintético para concluir o exercício local existente."),
            new SyntheticFile(ShiftNoteId, "shift_note.txt", "Shift note", "Nota de turno",
                "Shift note: all workstation events in this prototype are fictional and local.",
                "Nota de turno: todos os eventos desta estação são fictícios e locais.")
        });

        public static IReadOnlyList<SyntheticFile> All => Files;

        public static bool TryGetById(string id, out SyntheticFile file)
        {
            foreach (var candidate in Files)
            {
                if (string.Equals(candidate.Id, id, StringComparison.Ordinal))
                {
                    file = candidate;
                    return true;
                }
            }

            file = null;
            return false;
        }

        public static bool TryGetByIdOrFileName(string value, out SyntheticFile file)
        {
            foreach (var candidate in Files)
            {
                if (string.Equals(candidate.Id, value, StringComparison.Ordinal) ||
                    string.Equals(candidate.FileName, value, StringComparison.Ordinal))
                {
                    file = candidate;
                    return true;
                }
            }

            file = null;
            return false;
        }
    }

    public readonly struct TerminalStateSnapshot
    {
        public TerminalStateSnapshot(LocalComputerState computerState, LocalPrototypeApp activeApp,
            bool exerciseComplete, int attemptCount)
        {
            ComputerState = computerState;
            ActiveApp = activeApp;
            ExerciseComplete = exerciseComplete;
            AttemptCount = Math.Max(0, attemptCount);
        }

        public LocalComputerState ComputerState { get; }
        public LocalPrototypeApp ActiveApp { get; }
        public bool ExerciseComplete { get; }
        public int AttemptCount { get; }
    }

    public enum TerminalResultCode
    {
        Help,
        Clear,
        WorldStatus,
        FilesList,
        FileRead,
        CaseList,
        AcademyStatus,
        NotReady,
        TerminalInactive,
        EmptyInput,
        InputTooLong,
        TooManyArguments,
        InvalidCharacters,
        UnknownCommand,
        WrongArity,
        SyntheticFileNotFound,
        OutputTooLong
    }

    /// <summary>Typed, immutable result. Presentation maps the code to localized copy.</summary>
    public sealed class TerminalCommandResult
    {
        public TerminalCommandResult(TerminalResultCode code, TerminalStateSnapshot snapshot,
            SyntheticFile file = null)
        {
            Code = code;
            Snapshot = snapshot;
            File = file;
        }

        public TerminalResultCode Code { get; }
        public TerminalStateSnapshot Snapshot { get; }
        public SyntheticFile File { get; }

        public bool IsError => Code != TerminalResultCode.Help && Code != TerminalResultCode.Clear &&
                               Code != TerminalResultCode.WorldStatus && Code != TerminalResultCode.FilesList &&
                               Code != TerminalResultCode.FileRead && Code != TerminalResultCode.CaseList &&
                               Code != TerminalResultCode.AcademyStatus;
    }

    /// <summary>Deterministic allowlisted parser. It has no host, filesystem, or network adapter.</summary>
    public sealed class TerminalCommandDispatcher
    {
        public const int MaximumInputCharacters = 256;
        public const int MaximumArguments = 4;

        public TerminalCommandResult Execute(string input, TerminalStateSnapshot snapshot)
        {
            if (snapshot.ComputerState != LocalComputerState.Ready)
            {
                return Result(TerminalResultCode.NotReady, snapshot);
            }

            if (snapshot.ActiveApp != LocalPrototypeApp.Terminal)
            {
                return Result(TerminalResultCode.TerminalInactive, snapshot);
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                return Result(TerminalResultCode.EmptyInput, snapshot);
            }

            if (input.Length > MaximumInputCharacters)
            {
                return Result(TerminalResultCode.InputTooLong, snapshot);
            }

            if (!IsSafeInput(input))
            {
                return Result(TerminalResultCode.InvalidCharacters, snapshot);
            }

            var tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0)
            {
                return Result(TerminalResultCode.EmptyInput, snapshot);
            }

            if (tokens.Length - 1 > MaximumArguments)
            {
                return Result(TerminalResultCode.TooManyArguments, snapshot);
            }

            var command = tokens[0];
            var argumentCount = tokens.Length - 1;

            if (string.Equals(command, "help", StringComparison.Ordinal))
            {
                return Arity(argumentCount, 0, TerminalResultCode.Help, snapshot);
            }

            if (string.Equals(command, "clear", StringComparison.Ordinal))
            {
                return Arity(argumentCount, 0, TerminalResultCode.Clear, snapshot);
            }

            if (string.Equals(command, "world.status", StringComparison.Ordinal))
            {
                return Arity(argumentCount, 0, TerminalResultCode.WorldStatus, snapshot);
            }

            if (string.Equals(command, "files.list", StringComparison.Ordinal))
            {
                return Arity(argumentCount, 0, TerminalResultCode.FilesList, snapshot);
            }

            if (string.Equals(command, "files.read", StringComparison.Ordinal))
            {
                if (argumentCount != 1)
                {
                    return Result(TerminalResultCode.WrongArity, snapshot);
                }

                return SyntheticFileCatalog.TryGetByIdOrFileName(tokens[1], out var file)
                    ? new TerminalCommandResult(TerminalResultCode.FileRead, snapshot, file)
                    : Result(TerminalResultCode.SyntheticFileNotFound, snapshot);
            }

            if (string.Equals(command, "case.list", StringComparison.Ordinal))
            {
                return Arity(argumentCount, 0, TerminalResultCode.CaseList, snapshot);
            }

            if (string.Equals(command, "academy.status", StringComparison.Ordinal))
            {
                return Arity(argumentCount, 0, TerminalResultCode.AcademyStatus, snapshot);
            }

            return Result(TerminalResultCode.UnknownCommand, snapshot);
        }

        public static bool IsSafeInput(string input)
        {
            if (input == null || input.Length > MaximumInputCharacters)
            {
                return false;
            }

            foreach (var character in input)
            {
                var asciiLetter = character >= 'A' && character <= 'Z' || character >= 'a' && character <= 'z';
                var asciiDigit = character >= '0' && character <= '9';
                if (!asciiLetter && !asciiDigit && character != ' ' && character != '.' &&
                    character != '_' && character != '-')
                {
                    return false;
                }
            }

            return true;
        }

        private static TerminalCommandResult Arity(int actual, int expected, TerminalResultCode successCode,
            TerminalStateSnapshot snapshot) =>
            actual == expected ? Result(successCode, snapshot) : Result(TerminalResultCode.WrongArity, snapshot);

        private static TerminalCommandResult Result(TerminalResultCode code, TerminalStateSnapshot snapshot) =>
            new TerminalCommandResult(code, snapshot);
    }

    public sealed class TerminalTranscriptEntry
    {
        public TerminalTranscriptEntry(string input, bool echoInput, TerminalCommandResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            EchoInput = echoInput && TerminalCommandDispatcher.IsSafeInput(input) && !string.IsNullOrWhiteSpace(input);
            Input = EchoInput ? input : string.Empty;
            Result = result;
        }

        public string Input { get; }
        public bool EchoInput { get; }
        public TerminalCommandResult Result { get; }
    }

    /// <summary>Bounded, session-local command history and rendered-output accounting.</summary>
    public sealed class LocalPrototypeTerminalSession
    {
        public const int MaximumHistoryEntries = 50;
        public const int MaximumCommandOutputCharacters = 4096;
        public const int MaximumVisibleOutputCharacters = 32768;

        private readonly List<string> history = new List<string>();
        private readonly List<TerminalTranscriptEntry> output = new List<TerminalTranscriptEntry>();
        private int visibleOutputCharacters;

        public IReadOnlyList<string> History => new ReadOnlyCollection<string>(history);
        public IReadOnlyList<TerminalTranscriptEntry> Output => new ReadOnlyCollection<TerminalTranscriptEntry>(output);
        public int VisibleOutputCharacters => visibleOutputCharacters;

        public void RecordHistory(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !TerminalCommandDispatcher.IsSafeInput(input))
            {
                return;
            }

            if (history.Count == MaximumHistoryEntries)
            {
                history.RemoveAt(0);
            }

            history.Add(input);
        }

        public bool AppendOutput(TerminalTranscriptEntry entry, int renderedCharacterCount)
        {
            if (entry == null || renderedCharacterCount < 0 ||
                renderedCharacterCount > MaximumCommandOutputCharacters)
            {
                return false;
            }

            while (output.Count > 0 && visibleOutputCharacters + renderedCharacterCount > MaximumVisibleOutputCharacters)
            {
                visibleOutputCharacters -= outputLengths[0];
                output.RemoveAt(0);
                outputLengths.RemoveAt(0);
            }

            if (renderedCharacterCount > MaximumVisibleOutputCharacters)
            {
                return false;
            }

            output.Add(entry);
            outputLengths.Add(renderedCharacterCount);
            visibleOutputCharacters += renderedCharacterCount;
            return visibleOutputCharacters <= MaximumVisibleOutputCharacters;
        }

        public bool RecalculateOutputLengths(IReadOnlyList<int> renderedCharacterCounts)
        {
            if (renderedCharacterCounts == null || renderedCharacterCounts.Count != output.Count)
            {
                return false;
            }

            var recalculatedLengths = new List<int>(renderedCharacterCounts.Count);
            var total = 0;
            for (var index = 0; index < renderedCharacterCounts.Count; index++)
            {
                var length = renderedCharacterCounts[index];
                if (length < 0 || length > MaximumCommandOutputCharacters)
                {
                    return false;
                }

                total += length;
                recalculatedLengths.Add(length);
            }

            while (output.Count > 0 && total > MaximumVisibleOutputCharacters)
            {
                total -= recalculatedLengths[0];
                output.RemoveAt(0);
                recalculatedLengths.RemoveAt(0);
            }

            outputLengths.Clear();
            outputLengths.AddRange(recalculatedLengths);
            visibleOutputCharacters = total;
            return true;
        }

        public void ClearOutput()
        {
            output.Clear();
            visibleOutputCharacters = 0;
            outputLengths.Clear();
        }

        public void Reset()
        {
            history.Clear();
            ClearOutput();
        }

        private readonly List<int> outputLengths = new List<int>();

    }
}
