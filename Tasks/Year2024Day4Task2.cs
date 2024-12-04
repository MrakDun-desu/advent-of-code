using Vector2 = (int X, int Y);

namespace AdventOfCode.Tasks;

public class Year2024Day4Task2 : IDailyTask
{
    private const string TARGET = "MAS";
    readonly int maxDist = TARGET.Length - 1;

    private int InnerFits(string[] inputLines, Vector2 start, Vector2 dir)
    {
        for (int i = 0; i < TARGET.Length; i++)
        {
            if (inputLines[start.Y + dir.Y * i][start.X + dir.X * i] != TARGET[i])
            {
                return 0;
            }
        }
        return 1;
    }

    private int Fits(
        string[] inputLines,
        Vector2 start1,
        Vector2 start2,
        Vector2 dir1,
        Vector2 dir2
    )
    {
        var end = new Vector2(start1.X + dir1.X * maxDist, start1.Y + dir1.Y * maxDist);
        return end.X >= inputLines[0].Length || end.X < 0 || end.Y >= inputLines.Length || end.Y < 0
            ? 0
            : InnerFits(inputLines, start1, dir1) * InnerFits(inputLines, start2, dir2);
    }

    public string Execute(string input)
    {
        var inputLines = input.Split('\n')[..^1];
        var x_masCount = 0;
        for (int y = 0; y < inputLines.Length; y++)
        {
            for (int x = 0; x < inputLines[0].Length; x++)
            {
                x_masCount +=
                    Fits(inputLines, (x, y), (x, y + maxDist), (1, 1), (1, -1))
                    + Fits(inputLines, (x, y), (x, y + maxDist), (-1, 1), (-1, -1))
                    + Fits(inputLines, (x, y), (x + maxDist, y), (1, 1), (-1, 1))
                    + Fits(inputLines, (x, y), (x + maxDist, y), (1, -1), (-1, -1));
            }
        }

        return x_masCount.ToString();
    }
}
