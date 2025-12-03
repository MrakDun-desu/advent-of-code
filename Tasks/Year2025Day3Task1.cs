namespace AdventOfCode.Tasks;

public class Year2025Day3Task1 : IDailyTask {
    const int NumCharOffset = 48;

    public string Execute(string input) {
        var lines = input.Split('\n')[..^1];

        var output = 0L;
        foreach (var line in lines) {
            ReadOnlySpan<char> chars = line;
            var max = (char)0;
            var maxIndex = 0;
            for (var i = 0; i < chars.Length - 1; i++) {
                if (max < chars[i]) {
                    max = chars[i];
                    maxIndex = i;
                }
            }

            var max2 = (char)0;
            for (var i = maxIndex + 1; i < chars.Length; i++) {
                if (max2 < chars[i]) {
                    max2 = chars[i];
                }
            }

            int combined = (max - NumCharOffset) * 10 + max2 - NumCharOffset;
            output += combined;
        }

        return output.ToString();
    }
}
