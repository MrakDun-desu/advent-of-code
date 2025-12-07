namespace AdventOfCode.Tasks;

public class Year2025Day7Task2 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var beams = new Dictionary<int, ulong> {
            { lines[0].IndexOf('S'), 1UL}
        };

        for (var y = 1; y < lines.Length; y++) {
            for (var x = 0; x < lines[y].Length; x++) {
                if (lines[y][x] == '^' && beams.TryGetValue(x, out var count)) {
                    if (beams.TryGetValue(x - 1, out var leftCount)) {
                        beams[x - 1] = leftCount + count;
                    } else {
                        beams[x - 1] = count;
                    }

                    if (beams.TryGetValue(x + 1, out var rightCount)) {
                        beams[x + 1] = rightCount + count;
                    } else {
                        beams[x + 1] = count;
                    }
                    beams.Remove(x);
                }
            }
        }

        return beams.Aggregate(0UL, (acc, curr) => acc += curr.Value).ToString();
    }
}
