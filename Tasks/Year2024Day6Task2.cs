namespace AdventOfCode.Tasks;

public class Year2024Day6Task2 : IDailyTask
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

    private static (int X, int Y) GetNewPos((int X, int Y) currentPos, Direction dir) =>
        dir switch
        {
            Direction.Up => (currentPos.X, currentPos.Y - 1),
            Direction.Right => (currentPos.X + 1, currentPos.Y),
            Direction.Left => (currentPos.X - 1, currentPos.Y),
            Direction.Down => (currentPos.X, currentPos.Y + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(dir)),
        };

    private static Direction GetNewDir(Direction dir) =>
        dir switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(dir)),
        };

    private record struct ProblemState((int X, int Y) Pos, Direction Dir);

    private static bool IsPathALoop(char[,] map, (int X, int Y) pos, Direction dir)
    {
        var prevStates = new HashSet<ProblemState>();
        while (true)
        {
            var currentState = new ProblemState(pos, dir);
            if (prevStates.Contains(currentState))
            {
                return true;
            }
            prevStates.Add(currentState);
            (int X, int Y) newPos = GetNewPos(pos, dir);
            if (
                newPos.X >= map.GetLength(0)
                || newPos.X < 0
                || newPos.Y >= map.GetLength(1)
                || newPos.Y < 0
            )
            {
                return false;
            }
            while (map[newPos.X, newPos.Y] == '#')
            {
                dir = GetNewDir(dir);
                newPos = GetNewPos(pos, dir);
            }
            pos = newPos;
        }
    }

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

        var startPos = guardPos;
        var startDir = guardDirection;
        var checkedPoss = new HashSet<(int X, int Y)> { startPos };
        var loopCount = 0;
        while (true)
        {
            (int X, int Y) newPos = GetNewPos(guardPos, guardDirection);
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
                guardDirection = GetNewDir(guardDirection);
                newPos = GetNewPos(guardPos, guardDirection);
            }
            guardPos = newPos;
            if (checkedPoss.Contains(guardPos))
            {
                continue;
            }
            checkedPoss.Add(guardPos);
            inputMatrix[guardPos.X, guardPos.Y] = '#';
            if (IsPathALoop(inputMatrix, startPos, startDir))
            {
                loopCount++;
            }
            inputMatrix[guardPos.X, guardPos.Y] = '.';
        }

        return loopCount.ToString();
    }
}
