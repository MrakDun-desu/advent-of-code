using System.Globalization;
using AdventOfCode.Utils;
namespace AdventOfCode.Tasks;

public class Year2024Day11Task2 : IDailyTask {
    private readonly NumberFormatInfo Format = CultureInfo.InvariantCulture.NumberFormat;

    private static ulong NumDigits(ulong num) {
        var numDigits = 1uL;
        while (true) {
            if (num / MyMath.Pow(10uL, numDigits++) == 0) {
                return numDigits - 1;
            }
        }
    }

    private static UInt128 CountStones(ulong stone, uint depth = 75) {
        if (depth == 0) {
            return 1;
        }
        depth--;
        // while the number doesn't have even digits, no need to recurse
        var numDigits = NumDigits(stone);
        while (numDigits % 2 != 0) {
            if (depth-- == 0) {
                return 1;
            }
            stone = stone == 0 ? 1uL : stone * 2024uL;
            numDigits = NumDigits(stone);
        }
        var upperHalf = stone / MyMath.Pow(10uL, numDigits / 2);
        var lowerHalf = stone - (upperHalf * MyMath.Pow(10uL, numDigits / 2));
        return CountStones(upperHalf, depth) + CountStones(lowerHalf, depth);
    }

    public string Execute(string input) {
        input = input.Trim();
        var stones = input.Split(' ').Select(uint.Parse).ToArray();
        if (stones.Length < 5) {
            return "";
        }
        UInt128 sum = 0;
        for (var i = 0; i < stones.Length; i++) {
            sum += CountStones(stones[i], 75);
        }

        return sum.ToString(Format);
    }
}
