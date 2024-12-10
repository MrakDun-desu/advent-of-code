namespace AdventOfCode.Tasks;

public class Year2024Day8Task2 : IDailyTask
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

        public bool IsBetween(Vector2 a, Vector2 b) => X < a.X && X > b.X && Y < a.Y && Y > b.Y;
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
        var border = new Vector2(-1, -1);
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
                    for (var pos = positions[i]; pos.IsBetween(inputDims, border); pos += diff)
                    {
                        antinodes.Add(pos);
                    }
                    for (var pos = positions[j]; pos.IsBetween(inputDims, border); pos -= diff)
                    {
                        antinodes.Add(pos);
                    }
                }
            }
        }
        return antinodes.Count.ToString();
    }
}
