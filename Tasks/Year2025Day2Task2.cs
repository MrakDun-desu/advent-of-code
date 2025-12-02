namespace AdventOfCode.Tasks;

public class Year2025Day2Task2 : IDailyTask {
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
                for (int subLen = iStr.Length / 2; subLen > 0; subLen--) {
                    if (iStr.Length % subLen != 0) {
                        continue;
                    }
                    var valuesSame = true;
                    var subStr = iStr[..subLen];
                    var offset = subLen;
                    while (offset < iStr.Length) {
                        var curStr = iStr[offset..(offset + subLen)];
                        if (curStr != subStr) {
                            valuesSame = false;
                            break;
                        }
                        offset += subLen;
                    }
                    if (valuesSame) {
                        output += i;
                        break;
                    }
                }
            }
        }

        return output.ToString();
    }
}
