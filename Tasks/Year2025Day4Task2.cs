using AdventOfCode.Utils;

namespace AdventOfCode.Tasks;

public class Year2025Day4Task2 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var positions = lines
            .Select(l => l.Select(c => c switch {
                '@' => 1,
                '.' => 0,
                _ => throw new FormatException("Only the characters '@' and '.' are allowed in input")
            }).ToArray()).ToArray();

        var movedCount = 0;
        var adjacent = Enumerable.Range(-1, 3)
            .SelectMany(y => Enumerable.Range(-1, 3).Select(x => new Vector2<int>(x, y)))
            .Where(val => val != (0, 0))
            .ToArray();
        while (true) {
            var removed = false;
            for (var y = 0; y < positions.Length; y++) {
                for (var x = 0; x < positions[y].Length; x++) {
                    if (positions[y][x] != 1) {
                        continue;
                    }
                    var adjacentFilledCount = adjacent
                        .Aggregate(0, (acc, adj) => {
                            var pos = adj + (x, y);
                            if (!pos.IsBetween((-1, -1), (positions[y].Length, positions.Length))) {
                                return acc;
                            }
                            return acc + positions[pos.Y][pos.X];
                        });
                    if (adjacentFilledCount < 4) {
                        positions[y][x] = 0;
                        movedCount++;
                        removed = true;
                    }
                }
            }

            if (!removed) {
                break;
            }
        }

        return movedCount.ToString();
    }
}
