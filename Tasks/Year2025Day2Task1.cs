namespace AdventOfCode.Tasks;

public class Year2025Day2Task1 : IDailyTask {
    public string Execute(string input) {
        var ranges = input
            .Split(',')
            .Select(rangeStr => rangeStr
                    .Split('-')
                    .Select(long.Parse)
                    .ToArray())
            .ToArray();

        var output = 0L;
        foreach (var range in ranges) {
            for (long i = range[0]; i <= range[1]; i++) {
                var iStr = i.ToString();
                var halfLen = iStr.Length / 2;
                if (iStr[..halfLen] == iStr[halfLen..]) {
                    output += i;
                }
            }
        }

        return output.ToString();
    }
}
