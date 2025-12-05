using AdventOfCode.Utils;

namespace AdventOfCode.Tasks;

public class Year2025Day5Task1 : IDailyTask {
    public string Execute(string input) {
        var parts = input.Split("\n\n");

        var ranges = parts[0].Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split('-').Select(ulong.Parse).ToArray())
            .Select(x => (x[0], x[1]))
            .ToArray();

        var ids = parts[1].Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(ulong.Parse)
            .ToArray();

        var sum = 0;
        foreach (var id in ids) {
            foreach (var range in ranges) {
                if (id >= range.Item1 && id <= range.Item2) {
                    sum++;
                    break;
                }
            }
        }

        return sum.ToString();
    }
}
