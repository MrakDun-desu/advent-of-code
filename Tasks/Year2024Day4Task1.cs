using Vector2 = (int X, int Y);

namespace AdventOfCode.Tasks;

public class Year2024Day4Task1 : IDailyTask
{
    private const string TARGET = "XMAS";

    private static int Fits(string[] inputLines, Vector2 start, Vector2 dir)
    {
        var maxDist = TARGET.Length - 1;
        var end = new Vector2(start.X + dir.X * maxDist, start.Y + dir.Y * maxDist);
        if (end.X >= inputLines[0].Length || end.X < 0 || end.Y >= inputLines.Length || end.Y < 0)
        {
            return 0;
        }
        for (int i = 0; i < TARGET.Length; i++)
        {
            if (inputLines[start.Y + dir.Y * i][start.X + dir.X * i] != TARGET[i])
            {
                return 0;
            }
        }

        return 1;
    }

    public string Execute(string input)
    {
        var inputLines = input.Split('\n')[..^1];
        var xmasCount = 0;
        for (int y = 0; y < inputLines.Length; y++)
        {
            for (int x = 0; x < inputLines[0].Length; x++)
            {
                xmasCount +=
                    Fits(inputLines, (x, y), (0, 1))
                    + Fits(inputLines, (x, y), (0, -1))
                    + Fits(inputLines, (x, y), (1, 0))
                    + Fits(inputLines, (x, y), (1, 1))
                    + Fits(inputLines, (x, y), (1, -1))
                    + Fits(inputLines, (x, y), (-1, 0))
                    + Fits(inputLines, (x, y), (-1, 1))
                    + Fits(inputLines, (x, y), (-1, -1));
            }
        }

        return xmasCount.ToString();
    }
}
