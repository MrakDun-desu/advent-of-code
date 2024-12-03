using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks;

public partial class Year2024Day3Task2 : IDailyTask
{
    public string Execute(string input)
    {
        var dos = DoRegex().Matches(input);
        var donts = DontRegex().Matches(input);
        var muls = MulRegex().Matches(input);

        var instructions = new List<Instruction>();
        _ = dos.Aggregate(
            instructions,
            static (acc, curr) =>
            {
                acc.Add(new(InstructionType.Do, 0, 0, curr.Index));
                return acc;
            }
        );
        _ = donts.Aggregate(
            instructions,
            static (acc, curr) =>
            {
                acc.Add(new(InstructionType.Dont, 0, 0, curr.Index));
                return acc;
            }
        );
        _ = muls.Aggregate(
            instructions,
            static (acc, curr) =>
            {
                acc.Add(
                    new(
                        InstructionType.Mul,
                        int.Parse(curr.Groups[1].Value),
                        int.Parse(curr.Groups[2].Value),
                        curr.Index
                    )
                );
                return acc;
            }
        );

        instructions.Sort((a, b) => a.Index.CompareTo(b.Index));

        var output = 0;
        var active = true;
        foreach (var instruction in instructions)
        {
            switch (instruction.Type)
            {
                case InstructionType.Do:
                    active = true;
                    break;
                case InstructionType.Dont:
                    active = false;
                    break;
                case InstructionType.Mul:
                    if (!active)
                    {
                        continue;
                    }

                    output += instruction.Arg1 * instruction.Arg2;
                    break;
            }
        }

        return output.ToString();
    }

    private enum InstructionType
    {
        Do = 1,
        Dont = 2,
        Mul = 3,
    }

    private record Instruction(InstructionType Type, int Arg1, int Arg2, int Index);

    [GeneratedRegex("mul\\((\\d+),(\\d+)\\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex("do\\(\\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex("don't\\(\\)")]
    private static partial Regex DontRegex();
}
