namespace AdventOfCode.Tasks;

public class Year2025Day6Task2 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var lineLength = lines.Aggregate(0, (acc, curr) => Math.Max(acc, curr.Length));
        // pad the stuff right so we don't need to check for index out of bounds
        // (lines aren't equal length because all editors trim trailing whitespace)
        lines = lines.Select(l => l.PadRight(lineLength, ' ')).ToArray();

        var symbols = lines[^1];
        var currentSymbol = symbols[0];
        var totalSum = 0UL;
        var currentSum = 0UL;
        for (var x = 0; x < lineLength; x++) {
            List<char> currentNumber = [];
            for (var y = 0; y < lines.Length - 1; y++) {
                if (lines[y][x] == ' ') {
                    continue;
                }
                currentNumber.Add(lines[y][x]);
            }
            if (currentNumber.Count == 0) {
                continue;
            }
            var newNumber = ulong.Parse(currentNumber.ToArray());

            switch (symbols[x]) {
                case ' ' when currentSymbol == '+':
                    currentSum += newNumber;
                    break;
                case ' ' when currentSymbol == '*':
                    currentSum *= newNumber;
                    break;
                case '+' or '*':
                    currentSymbol = symbols[x];
                    totalSum += currentSum;
                    currentSum = newNumber;
                    break;
            }
        }

        totalSum += currentSum;

        return totalSum.ToString();
    }
}
