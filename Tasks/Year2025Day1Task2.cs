namespace AdventOfCode.Tasks;

public class Year2025Day1Task2 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split('\n')[..^1];

        var zerosCount = 0;
        var dialPos = 50;
        foreach (var line in lines) {
            var dir = line[0];
            var count = int.Parse(line[1..]);
            var wasZero = dialPos == 0;
            dialPos = dir switch {
                'L' => dialPos - count,
                'R' => dialPos + count,
                _ => throw new NotSupportedException("Only movements left and right are supported")
            };

            zerosCount += Math.Abs(dialPos / 100);
            if (dialPos <= 0 && !wasZero) {
                zerosCount += 1;
            }
            dialPos %= 100;
            if (dialPos < 0) {
                dialPos += 100;
            }
        }

        return zerosCount.ToString();
    }
}
