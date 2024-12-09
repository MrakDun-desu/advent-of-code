namespace AdventOfCode.Tasks;

public class Year2024Day7Task2 : IDailyTask
{
    private static HashSet<ulong> GetPossibleResults(ulong state, ulong[] next)
    {
        if (next.Length == 0)
        {
            return [state];
        }

        var rest = next[1..];
        var output = GetPossibleResults(state + next[0], rest);
        output.UnionWith(GetPossibleResults(state * next[0], rest));
        output.UnionWith(GetPossibleResults(ulong.Parse($"{state}{next[0]}"), rest));
        return output;
    }

    public string Execute(string input)
    {
        var inputLines = input.Split("\n")[..^1];
        ulong sum = 0;
        foreach (var line in inputLines)
        {
            var splitLine = line.Split(": ");

            var result = ulong.Parse(splitLine[0]);
            var numbers = splitLine[1].Split(' ').Select(ulong.Parse).ToArray();

            if (GetPossibleResults(numbers[0], numbers[1..]).Contains(result))
            {
                sum += result;
            }
        }
        return sum.ToString();
    }
}
