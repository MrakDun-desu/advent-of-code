using AdventOfCode.Utils;

namespace AdventOfCode.Tasks;

public class Year2025Day6Task1 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var numbers = lines[..^1]
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray())
            .ToArray();
        var symbols = lines[^1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        var totalSum = 0UL;
        for (var i = 0; i < symbols.Length; i++) {
            var init = numbers[0][i];
            for (var j = 1; j < numbers.Length; j++) {
                init = symbols[i] switch {
                    "+" => init + numbers[j][i],
                    "*" => init * numbers[j][i],
                    _ => throw new FormatException("Only operands '+' and '*' are allowed")
                };
            }
            totalSum += init;
        }

        return totalSum.ToString();
    }
}
