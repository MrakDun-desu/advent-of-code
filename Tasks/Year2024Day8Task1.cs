namespace AdventOfCode.Tasks;

public class Year2024Day8Task1 : IDailyTask
{
    private record Vector2(int X, int Y)
    {
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
    }

    public string Execute(string input)
    {
        var inputLines = input.Split('\n')[..^1];
        var inputDims = new Vector2(inputLines[0].Length, inputLines.Length);
        var antennas = new Dictionary<char, List<Vector2>>();
        for (var y = 0; y < inputDims.Y; y++)
        {
            for (var x = 0; x < inputDims.X; x++)
            {
                var cell = inputLines[y][x];
                if (cell == '.')
                {
                    continue;
                }
                if (antennas.TryGetValue(cell, out var positions))
                {
                    positions.Add(new Vector2(x, y));
                }
                else
                {
                    antennas[cell] = [new Vector2(x, y)];
                }
            }
        }

        var antinodes = new HashSet<Vector2>();
        foreach (var (_, positions) in antennas)
        {
            for (var i = 0; i < positions.Count; i++)
            {
                for (var j = 0; j < positions.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var diff = positions[i] - positions[j];
                    var pos1 = positions[i] + diff;
                    var pos2 = positions[j] - diff;
                    if (pos1.X < inputDims.X && pos1.X >= 0 && pos1.Y < inputDims.Y && pos1.Y >= 0)
                    {
                        antinodes.Add(pos1);
                    }
                    if (pos2.X < inputDims.X && pos2.X >= 0 && pos2.Y < inputDims.Y && pos2.Y >= 0)
                    {
                        antinodes.Add(pos2);
                    }
                }
            }
        }
        return antinodes.Count.ToString();
    }
}
