using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks;

public partial class Year2024Day3Task1 : IDailyTask
{
    public string Execute(string input)
    {
        var output = 0;
        foreach (Match instruction in MulRegex().Matches(input))
        {
            output +=
                int.Parse(instruction.Groups[1].Value) * int.Parse(instruction.Groups[2].Value);
        }
        return output.ToString();
    }

    [GeneratedRegex("mul\\((\\d+),(\\d+)\\)")]
    private static partial Regex MulRegex();
}
