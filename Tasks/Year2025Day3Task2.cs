namespace AdventOfCode.Tasks;

public class Year2025Day3Task2 : IDailyTask {
    const int Iterations = 12;
    const int NumCharOffset = 48;

    public string Execute(string input) {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        ulong total = 0UL;
        Parallel.ForEach(
                lines,
                () => 0UL,
                (lineSrc, _, subTotal) => {
                    ReadOnlySpan<char> line = lineSrc;
                    Span<char> maxes = new char[Iterations];
                    Span<int> indices = new int[Iterations];

                    for (var i = 0; i < Iterations; i++) {
                        var pos = i switch {
                            0 => 0,
                            _ => indices[i - 1] + 1
                        };
                        for (; pos < (line.Length - (Iterations - 1) + i); pos++) {
                            if (maxes[i] < line[pos]) {
                                maxes[i] = line[pos];
                                indices[i] = pos;
                                if (maxes[i] == '9') {
                                    break;
                                }
                            }
                        }
                    }

                    var combined = (ulong)(maxes[0] - NumCharOffset);
                    for (var i = 1; i < Iterations; i++) {
                        combined = combined * 10 + (ulong)(maxes[i] - NumCharOffset);
                    }

                    return subTotal + combined;
                },
                partitionTotal => Interlocked.Add(ref total, partitionTotal)
            );

        return total.ToString();
    }
}
