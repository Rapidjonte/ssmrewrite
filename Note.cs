using Raylib_cs;

namespace ssm;

public struct Note {
    public float X;
    public float Y;
    public float Time;
    public Color Color;
}
public class Beatmap
{
    public string Name = "";
    public Note[] Notes = [];
}