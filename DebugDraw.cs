#if TOOLS
using Godot;
using System.Collections.Generic;

public static class DebugDraw
{
    public struct LineData
    {
        public Vector3 From;
        public Vector3 To;
        public Color Color;
    }

    public struct BoxData
    {
        public Vector3 Center;
        public Vector3 Size;
        public Color Color;
    }

    public struct SphereData
    {
        public Vector3 Center;
        public float Radius;
        public Color Color;
    }

    public struct PointData
    {
        public Vector3 Position;
        public float Size;
        public Color Color;
    }

    public static List<LineData> Lines = [];
    public static List<BoxData> Boxes = [];
    public static List<SphereData> Spheres = [];
    public static List<PointData> Points = [];

    public static void Line(Vector3 from, Vector3 to, Color color)
    {
        Lines.Add(new LineData { From = from, To = to, Color = color });
    }

    public static void Box(Vector3 center, Vector3 size, Color color)
    {
        Boxes.Add(new BoxData { Center = center, Size = size, Color = color });
    }

    public static void Sphere(Vector3 center, float radius, Color color)
    {
        Spheres.Add(new SphereData { Center = center, Radius = radius, Color = color });
    }

    public static void Point(Vector3 position, float size, Color color)
    {
        Points.Add(new PointData { Position = position, Size = size, Color = color });
    }


    public static void Path(Vector3[] points, Color color)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Line(points[i], points[i + 1], color);
        }
    }

    public static void Arc(Vector3 startPos, Vector3 initialVelocity, Vector3 gravity, float duration = 1f, int segments = 30, Color? color = null)
    {
        Vector3[] points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            float t = (i / (float)(segments - 1)) * duration;
            points[i] = startPos + initialVelocity * t + 0.5f * gravity * t * t;
        }

        Path(points, color ?? Colors.White);
    }

    public static void Clear()
    {
        Lines.Clear();
        Boxes.Clear();
        Spheres.Clear();
        Points.Clear();
    }

}

#endif