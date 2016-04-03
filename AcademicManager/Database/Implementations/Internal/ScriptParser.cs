namespace Database.Implementations.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Internal;

    public class ScriptParser : IScriptParser
    {
        public ICollection<IScriptInstruction> Parse(string scriptBody)
        {
            var oneLineStringIntructions = CleanScript(scriptBody);
            return ConvertToInstructions(oneLineStringIntructions);
        }

        private static ICollection<IScriptInstruction> ConvertToInstructions(List<string> oneLineStringIntructions)
        {
            var result = new List<IScriptInstruction>();

            foreach (var intruction in oneLineStringIntructions)
            {

            }

            return result;
        }

        private static List<string> CleanScript(string scriptBody)
        {
            var instructions = RemoveEmptyLinesAndCommentLines(scriptBody);

            var oneLineInstructions = EnsureOneAtomicInstructionPerLine(instructions);

            return MergeMultilineInstructionsIntoOneLine(oneLineInstructions);
        }

        private static List<string> MergeMultilineInstructionsIntoOneLine(IReadOnlyList<string> lines)
        {
            var changes = new Dictionary<int, List<string>>();
            for (var i = 0; i < lines.Count; i++)
            {
                if (!lines[i].EndsWith(";", StringComparison.Ordinal))
                {
                    changes[i] = new List<string> {lines[i]};

                    do
                    {
                        i++;
                        changes[i].Add(lines[i]);
                    } while (!lines[i].EndsWith(";", StringComparison.Ordinal));
                }
            }

            var result = new List<string>();
            for (var i = 0; i < lines.Count; i++)
            {
                if (changes.ContainsKey(i))
                {
                    result[i] = string.Join(" ", changes[i]);
                }
            }
            return result;
        }

        private static List<string> EnsureOneAtomicInstructionPerLine(List<string> lines)
        {
            var changes = new Dictionary<int, string[]>();

            for (var i = 0; i < lines.Count; i++)
            {
                var splits = lines[i].Split(';');
                if (splits.Length >= 2)
                {
                    changes[i] = splits;
                }
            }

            var result = new List<string>();

            for (var i = 0; i < lines.Count; i++)
            {
                if (changes.ContainsKey(i))
                {
                    result.AddRange(changes[i]);
                }
                else
                {
                    result.Add(lines[i]);
                }
            }
            return result;
        }

        private static List<string> RemoveEmptyLinesAndCommentLines(string scriptBody)
        {
            var lines = scriptBody.Split('\n').ToList();

            lines.RemoveAll(l => l.StartsWith("--", StringComparison.Ordinal) || string.IsNullOrWhiteSpace(l));

            return lines;
        }
    }
}