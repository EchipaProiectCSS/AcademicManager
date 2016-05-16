﻿using System.Diagnostics;

namespace Database.Implementations.Internal.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Internal;
    using Utility;

    public class QueryParser : IQueryParser
    {
        public ICollection<IQueryInstruction> Parse(string scriptBody)
        {
            var oneLineStringIntructions = CleanScript(scriptBody);

            //todo: assertion - post
            Debug.Assert(columnNamesFromSelect.Count > 0, $"The columns used in select operation are {columns}");

            return ConvertToInstructions(oneLineStringIntructions);
        }

        private static ICollection<IQueryInstruction> ConvertToInstructions(
            IEnumerable<string> oneLineStringIntructions)
        {
            var instructions = new List<IQueryInstruction>();

            foreach (var instruction in oneLineStringIntructions)
            {
                foreach (var factory in QueryFactories.Factories)
                {
                    if (!factory.IsMatch(instruction))
                    {
                        continue;
                    }

                    instructions.Add(factory.Create(instruction));
                    break;
                }
            }

            return instructions;
        }

        private static IEnumerable<string> CleanScript(string scriptBody)
        {
            var instructions = RemoveEmptyLinesAndCommentLines(scriptBody);

            var oneLineInstructions = EnsureOneAtomicInstructionPerLine(instructions);

            return MergeMultilineInstructionsIntoOneLine(oneLineInstructions);
        }

        private static IEnumerable<string> MergeMultilineInstructionsIntoOneLine(IReadOnlyList<string> lines)
        {
            var changes = new Dictionary<int, List<string>>();
            for (var i = 0; i < lines.Count; i++)
            {
                if (!lines[i].EndsWith(";", StringComparison.Ordinal))
                {
                    changes[i] = new List<string>();

                    do
                    {
                        changes[i].Add(lines[i]);
                        i++;
                    }
                    while (lines.Count < i && !lines[i].EndsWith(";", StringComparison.Ordinal));
                }
            }

            var result = new List<string>();
            for (var i = 0; i < lines.Count; i++)
            {
                if (changes.ContainsKey(i))
                {
                    result.Add(string.Join(" ", changes[i]));
                }
                else
                {
                    result.Add(lines[i]);
                }
            }

            return result;
        }

        private static List<string> EnsureOneAtomicInstructionPerLine(IReadOnlyList<string> lines)
        {
            var changes = new Dictionary<int, string[]>();

            for (var i = 0; i < lines.Count; i++)
            {
                var splits = lines[i].Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                if (splits.Length >= 2)
                {
                    for (var j = 0; j < splits.Length; j++)
                    {
                        splits[j] = splits[j] + ';';
                    }
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
            List<string> lines = scriptBody.Trim().Split('\n').ToList();

            List<string> trimmedLines = lines.Select(line => line.Trim()).ToList();

            trimmedLines.RemoveAll(l => l.StartsWith("--", StringComparison.Ordinal) || string.IsNullOrWhiteSpace(l));

            return trimmedLines;
        }
    }
}