namespace ConsoleCad.Logic;

public readonly struct Marker {
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public Marker(double x, double y, double z) =>
        (X, Y, Z) = (x, y, z);

    public static Marker operator +(Marker a, Marker b) =>
        new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
}
