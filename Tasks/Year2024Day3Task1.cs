using System.Text.RegularExpressions;

namespace AdventOfCode.Tasks;

public partial class Year2024Day3Task1 : IDailyTask
{
    public string Execute(string input)
    {
        return MulRegex()
            .Matches(input)
            .Aggregate(
                0,
                (acc, curr) =>
                    acc + (int.Parse(curr.Groups[1].Value) * int.Parse(curr.Groups[2].Value))
            )
            .ToString();
    }

    [GeneratedRegex("mul\\((\\d+),(\\d+)\\)")]
    private static partial Regex MulRegex();
}
