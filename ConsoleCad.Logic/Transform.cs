namespace ConsoleCad.Logic;

public readonly struct Transform {
    public Marker Offset { get; }

    public Transform(double x, double y, double z) =>
        Offset = new Marker(x, y, z);

    public Transform(Marker offset) => Offset = offset;

    public Marker Apply(Marker local) => local + Offset;

    public static Transform operator +(Transform a, Transform b) =>
        new(a.Offset + b.Offset);
}
