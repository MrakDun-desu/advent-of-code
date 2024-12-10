using System.Numerics;

namespace AdventOfCode.Utils;

public record Vector2<T>(T X, T Y) where T : INumber<T> {
    public static Vector2<T> operator -(Vector2<T> a, Vector2<T> b) {
        return new Vector2<T>(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2<T> operator +(Vector2<T> a, Vector2<T> b) {
        return new Vector2<T>(a.X + b.X, a.Y + b.Y);
    }

    public static implicit operator Vector2<T>((T, T) input) {
        return new Vector2<T>(input.Item1, input.Item2);
    }

    public bool IsBetween(Vector2<T> a, Vector2<T> b) => X > a.X && X < b.X && Y > a.Y && Y < b.Y;
}

