using Range = (ulong Start, ulong End);

namespace AdventOfCode.Tasks;

public class Year2025Day5Task2 : IDailyTask {
    private List<Range> AddAndMerge(List<Range> source, Range newRange) {
        for (var i = 0; i < source.Count; i++) {
            var range = source[i];
            if (range.Start <= newRange.End && newRange.Start <= range.End) {
                newRange = (Math.Min(newRange.Start, range.Start), Math.Max(newRange.End, range.End));
                source.RemoveAt(i);
                i--;
            }
        }

        source.Add(newRange);
        return source;
    }

    public string Execute(string input) {
        return input.Split("\n\n")[0]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split('-').Select(ulong.Parse).ToArray())
            .Select(x => (Start: x[0], End: x[1]))
            .Aggregate(new List<Range>(), AddAndMerge)
            .Aggregate(0UL, (acc, curr) => acc += curr.End - curr.Start + 1)
            .ToString();
    }
}
