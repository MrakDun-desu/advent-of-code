namespace AdventOfCode.Tasks;

public class Year2025Day1Task1 : IDailyTask {
    public string Execute(string input) {
        var lines = input.Split('\n')[..^1];

        var zerosCount = 0;
        var dialPos = 50;
        foreach (var line in lines) {
            var dir = line[0];
            var count = int.Parse(line[1..]);
            dialPos = dir switch {
                'L' => (dialPos - count) % 100,
                'R' => (dialPos + count) % 100,
                _ => throw new NotSupportedException("Only movements left and right are supported")
            };

            if (dialPos == 0) {
                zerosCount++;
            }
        }

        return zerosCount.ToString();
    }
}
