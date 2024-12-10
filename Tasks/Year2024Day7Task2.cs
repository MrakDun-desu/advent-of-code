namespace AdventOfCode.Tasks;

public class Year2024Day7Task2 : IDailyTask
{
    private static ulong Pow(ulong a, ulong b)
    {
        ulong output = 1;
        for (ulong i = 0; i < b; i++)
        {
            output *= a;
        }
        return output;
    }

    private static bool IsResultPossible(ulong result, ulong[] numbers)
    {
        var operatorsCount = (ulong)numbers.Length - 1;
        var variationsCount = Pow(3, operatorsCount);

        var computeOutput = Parallel.For(
            0,
            (int)variationsCount,
            (i, state) =>
            {
                var operationResult = numbers[0];
                for (ulong j = 0; j < operatorsCount; j++)
                {
                    if (state.ShouldExitCurrentIteration)
                    {
                        return;
                    }
                    var operatorType = (ulong)i / Pow(3, j) % 3;

                    operationResult = operatorType switch
                    {
                        0 => operationResult + numbers[j + 1],
                        1 => operationResult * numbers[j + 1],
                        2 => ulong.Parse($"{operationResult}{numbers[j + 1]}"),
                        _ => throw new ArgumentOutOfRangeException(nameof(numbers)),
                    };

                    if (operationResult > result)
                    {
                        return;
                    }
                }

                if (operationResult == result)
                {
                    state.Break();
                }
            }
        );
        return !computeOutput.IsCompleted;
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

            if (IsResultPossible(result, numbers))
            {
                sum += result;
            }
        }
        return sum.ToString();
    }
}
