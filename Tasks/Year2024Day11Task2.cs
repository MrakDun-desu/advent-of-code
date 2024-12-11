using System.Globalization;
using AdventOfCode.Utils;
namespace AdventOfCode.Tasks;

public class Year2024Day11Task2 : IDailyTask {
    private readonly NumberFormatInfo Format = CultureInfo.InvariantCulture.NumberFormat;
    private readonly Dictionary<(ulong, uint), ulong> Cache = [];

    private static ulong NumDigits(ulong num) {
        var numDigits = 1uL;
        while (true) {
            if (num / MyMath.Pow(10uL, numDigits++) == 0) {
                return numDigits - 1;
            }
        }
    }

    private ulong CountStones(ulong stone, uint depth = 75) {
        if (depth == 0) {
            return 1;
        }
        if (Cache.TryGetValue((stone, depth), out var output)) {
            return output;
        }
        if (stone == 0) {
            output = CountStones(1, depth - 1);
        } else if (NumDigits(stone) % 2 == 0) {
            var numDigits = NumDigits(stone);
            var upperHalf = stone / MyMath.Pow(10uL, numDigits / 2);
            var lowerHalf = stone - (upperHalf * MyMath.Pow(10uL, numDigits / 2));
            output = CountStones(upperHalf, depth - 1) + CountStones(lowerHalf, depth - 1);
        } else {
            output = CountStones(stone * 2024, depth - 1);
        }
        Cache[(stone, depth)] = output;
        return output;
    }

    public string Execute(string input) {
        input = input.Trim();
        var stones = input.Split(' ').Select(uint.Parse).ToArray();
        ulong sum = 0;
        for (var i = 0; i < stones.Length; i++) {
            sum += CountStones(stones[i]);
        }

        return sum.ToString(Format);
    }
}
