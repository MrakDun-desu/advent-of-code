using System.Globalization;
namespace AdventOfCode.Tasks;

public class Year2024Day11Task1 : IDailyTask {
    private readonly NumberFormatInfo Format = CultureInfo.InvariantCulture.NumberFormat;
    private List<ulong> Stones = default!;

    private bool HasEvenDigits(ulong num, out (ulong, ulong) split) {
        var strNum = num.ToString(Format);
        if (strNum.Length % 2 != 0) {
            split = (0, 0);
            return false;
        }

        split = (ulong.Parse(strNum[..(strNum.Length / 2)], Format), ulong.Parse(strNum[(strNum.Length / 2)..], Format));
        return true;
    }

    private void ChangeStones() {
        for (var i = 0; i < Stones.Count; i++) {
            if (Stones[i] == 0) {
                Stones[i] = 1;
            } else if (HasEvenDigits(Stones[i], out (ulong, ulong) result)) {
                Stones[i] = result.Item1;
                Stones.Insert(i + 1, result.Item2);
                i++;
            } else {
                Stones[i] *= 2024;
            }
        }
    }

    public string Execute(string input) {
        input = input.Trim();
        Stones = input.Split(' ').Select(ulong.Parse).ToList();
        for (var i = 0; i < 25; i++) {
            ChangeStones();
        }

        return Stones.Count.ToString(Format);
    }
}
