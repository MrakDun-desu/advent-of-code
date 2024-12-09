namespace AdventOfCode.Tasks;

public class Year2024Day6Task1 : IDailyTask
{
    private enum Direction
    {
        Up = 0,
        Right = 1,
        Left = 2,
        Down = 3,
    }

    private readonly Dictionary<char, Direction> DirChars = new()
    {
        { '^', Direction.Up },
        { '>', Direction.Right },
        { '<', Direction.Left },
        { 'v', Direction.Down },
    };

    public string Execute(string input)
    {
        var inputLines = input.Split("\r\n")[..^1];
        var sizeX = inputLines[0].Length;
        var sizeY = inputLines.Length;

        char[,] inputMatrix = new char[sizeX, sizeY];
        Direction guardDirection = Direction.Up;
        (int X, int Y) guardPos = (0, 0);
        for (var y = 0; y < sizeY; y++)
        {
            for (var x = 0; x < sizeX; x++)
            {
                var current = inputLines[y][x];
                inputMatrix[x, y] = current;
                if (DirChars.TryGetValue(current, out Direction value))
                {
                    guardPos = (x, y);
                    guardDirection = value;
                }
            }
        }

        while (true)
        {
            inputMatrix[guardPos.X, guardPos.Y] = 'X';
            (int X, int Y) newPos = getNewPos(guardPos, guardDirection);
            if (
                newPos.X >= inputMatrix.GetLength(0)
                || newPos.X < 0
                || newPos.Y >= inputMatrix.GetLength(1)
                || newPos.Y < 0
            )
            {
                break;
            }
            while (inputMatrix[newPos.X, newPos.Y] == '#')
            {
                guardDirection = guardDirection switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    _ => throw new ArgumentOutOfRangeException(nameof(guardDirection)),
                };
                newPos = getNewPos(guardPos, guardDirection);
            }
            guardPos = newPos;
        }

        var output = 1;
        for (int x = 0; x < inputMatrix.GetLength(0); x++)
        {
            for (int y = 0; y < inputMatrix.GetLength(1); y++)
            {
                if (inputMatrix[x, y] == 'X')
                {
                    output++;
                }
            }
        }
        return output.ToString();
        (int X, int Y) getNewPos((int X, int Y) currentPos, Direction dir) =>
            dir switch
            {
                Direction.Up => (currentPos.X, currentPos.Y - 1),
                Direction.Right => (currentPos.X + 1, currentPos.Y),
                Direction.Left => (currentPos.X - 1, currentPos.Y),
                Direction.Down => (currentPos.X, currentPos.Y + 1),
                _ => throw new ArgumentOutOfRangeException(nameof(dir)),
            };
    }
}
