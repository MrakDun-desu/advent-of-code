using AdventOfCode.Utils;

namespace AdventOfCode.Tasks;

public class Year2025Day3Task2 : IDailyTask {
    const int Iterations = 12;
    const int NumCharOffset = 48;

    public string Execute(string input) {
        var lines = input.Split('\n')[..^1];

        var output = 0UL;
        foreach (var line in lines) {
            ReadOnlySpan<char> chars = line;
            var maxes = new char[Iterations];
            var indices = new int[Iterations];

            for (var i = 0; i < Iterations; i++) {
                var pos = i switch {
                    0 => 0,
                    _ => indices[i - 1] + 1
                };
                for (; pos < (chars.Length - (Iterations - 1) + i); pos++) {
                    if (maxes[i] < chars[pos]) {
                        maxes[i] = chars[pos];
                        indices[i] = pos;
                        if (maxes[i] == '9') {
                            break;
                        }
                    }
                }
            }

            var combined = 0UL;
            for (ulong i = 0; i < Iterations; i++) {
                combined += (ulong)(maxes[i] - NumCharOffset) * MyMath.Pow(10UL, Iterations - 1 - i);
            }

            output += combined;
        }

        return output.ToString();
    }
}
