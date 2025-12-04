using AdventOfCode.Utils;

namespace AdventOfCode.Tasks;

public class Year2025Day4Task1 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var positions = lines
            .Select(l => l.Select(c => c switch {
                '@' => 1,
                '.' => 0,
                _ => throw new FormatException("Only the characters '@' and '.' are allowed in input")
            }).ToArray()).ToArray();

        var movableCount = 0;
        for (var y = 0; y < positions.Length; y++) {
            for (var x = 0; x < positions[y].Length; x++) {
                var adjacent = Enumerable.Range(y - 1, 3)
                    .SelectMany(val => {
                        return Enumerable.Range(x - 1, 3)
                            .Select(xVal => new Vector2<int>(xVal, val));
                    })
                    .Where(pos =>
                            pos.IsBetween((-1, -1), (positions[y].Length, positions.Length))
                            && pos != (x, y)
                            )
                    .ToArray();

                var adjacentFilledCount = adjacent.Aggregate(0, (acc, pos) => acc + positions[pos.Y][pos.X]);
                if (positions[y][x] == 1 && adjacentFilledCount < 4) {
                    movableCount++;
                }
            }
        }

        return movableCount.ToString();
    }
}
