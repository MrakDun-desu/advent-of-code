namespace AdventOfCode.Tasks;

public class Year2024Day10Task1 : IDailyTask {
    private record Vector2(int X, int Y) {
        public static Vector2 operator -(Vector2 a, Vector2 b) {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public bool IsBetween(Vector2 a, Vector2 b) => X > a.X && X < b.X && Y > a.Y && Y < b.Y;
    }

    private bool IsReachableFrom(Vector2 end, Vector2 start, byte[,] map) {
        if (start == end) {
            return true;
        }
        if (map[start.X, start.Y] == 9) {
            return false;
        }
        var current = map[start.X, start.Y];
        if (start.X > 0 && map[start.X - 1, start.Y] == current + 1 && IsReachableFrom(end, start + new Vector2(-1, 0), map)) {
            return true;
        }
        if (start.X < map.GetLength(0) - 1 && map[start.X + 1, start.Y] == current + 1 && IsReachableFrom(end, start + new Vector2(1, 0), map)) {
            return true;
        }
        if (start.Y > 0 && map[start.X, start.Y - 1] == current + 1 && IsReachableFrom(end, start + new Vector2(0, -1), map)) {
            return true;
        }
        if (start.Y < map.GetLength(1) - 1 && map[start.X, start.Y + 1] == current + 1 && IsReachableFrom(end, start + new Vector2(0, 1), map)) {
            return true;
        }
        return false;
    }

    public string Execute(string input) {
        var inputLines = input.Split('\n')[..^1];
        var inputMap = new byte[inputLines[0].Length, inputLines.Length];

        var startPositions = new List<Vector2>();
        var endPositions = new List<Vector2>();
        for (var y = 0; y < inputMap.GetLength(1); y++) {
            for (var x = 0; x < inputMap.GetLength(0); x++) {
                inputMap[x, y] = (byte)(inputLines[y][x] - '0');
                switch (inputMap[x, y]) {
                    case 9:
                        endPositions.Add(new Vector2(x, y));
                        break;

                    case 0:
                        startPositions.Add(new Vector2(x, y));
                        break;
                }
            }
        }

        var sum = 0;
        foreach (Vector2 startPosition in startPositions) {
            foreach (Vector2 endPosition in endPositions) {
                if (IsReachableFrom(endPosition, startPosition, inputMap)) {
                    sum++;
                }
            }
        }
        return sum.ToString();
    }
}
