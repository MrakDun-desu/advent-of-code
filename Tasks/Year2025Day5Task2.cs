using AdventOfCode.Utils;

namespace AdventOfCode.Tasks;

public class Year2025Day5Task2 : IDailyTask {
    public string Execute(string input) {
        var parts = input.Split("\n\n");

        var ranges = parts[0].Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split('-').Select(ulong.Parse).ToArray())
            .Select(x => (Start: x[0], End: x[1]))
            .ToArray();

        List<(ulong Start, ulong End)> mergedRanges = [ranges[0]];

        for (var i = 1; i < ranges.Length; i++) {
            var newRange = ranges[i];
            var added = false;
            for (var j = 0; j < mergedRanges.Count; j++) {
                var range = mergedRanges[j];

                // Existing: |----|
                // New:               |-----|
                // Continue checking in the next iteration
                if (newRange.Start > range.End) {
                    continue;
                }

                // Existing:          |-----|
                // New:      |-----|
                // Insert a new item before the current one
                if (newRange.End < range.Start) {
                    mergedRanges.Insert(j, newRange);
                    added = true;
                    break;
                }

                // Existing:    |-----|
                // New:      |-----|
                // Or
                // Existing: |-----|
                // New:        |------|
                // Or
                // Existing: |-----|
                // New:    |---------|
                // Extend the current item and keep checking forward if it's necessary to merge any others
                if (newRange.Start < range.Start || newRange.End > range.End) {
                    mergedRanges[j] = mergedRanges[j] with {
                        Start = Math.Min(newRange.Start, range.Start),
                        End = Math.Max(newRange.End, range.End)
                    };
                    if (newRange.End > range.End) {
                        var nextIndex = j + 1;
                        while (nextIndex < mergedRanges.Count) {
                            var nextRange = mergedRanges[nextIndex];
                            // end is before the start of next one, can stop checking
                            if (newRange.End < nextRange.Start) {
                                break;
                            }
                            // end is inside of the next range - extend by the next one and remove the next
                            mergedRanges.RemoveAt(nextIndex);
                            if (newRange.End >= nextRange.Start && newRange.End <= nextRange.End) {
                                mergedRanges[j] = mergedRanges[j] with { End = nextRange.End };
                                break;
                            }
                        }
                    }
                }

                // Existing: |--------|
                // New:       |-----|
                // No need to change anything
                added = true;
                break;
            }
            if (!added) {
                mergedRanges.Add(newRange);
            }
        }

        var sum = 0UL;
        // Console.WriteLine("Merged ranges:");
        foreach (var range in mergedRanges) {
            // Console.WriteLine($"({range.Start}, {range.End})");
            sum += range.End + 1 - range.Start;
        }

        return sum.ToString();

    }
}
