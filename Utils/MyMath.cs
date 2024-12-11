using System.Numerics;

namespace AdventOfCode.Utils;

public static class MyMath {
    public static T Pow<T>(T num, T exp) where T : INumber<T> {
        T output = T.One;
        for (T i = T.Zero; i < exp; i++) {
            output *= num;
        }
        return output;
    }
}
