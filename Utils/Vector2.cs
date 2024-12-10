namespace AdventOfCode.Utils;

public record Vector2(int X, int Y) {
    public static Vector2 operator -(Vector2 a, Vector2 b) {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2 operator +(Vector2 a, Vector2 b) {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }

    public static implicit operator Vector2((int, int) input) {
        return new Vector2(input.Item1, input.Item2);
    }

    public bool IsBetween(Vector2 a, Vector2 b) => X > a.X && X < b.X && Y > a.Y && Y < b.Y;
}

