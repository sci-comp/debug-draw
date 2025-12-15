#if TOOLS

using Godot;

public partial class DebugDrawGizmo : EditorNode3DGizmo
{
    public override void _Redraw()
    {
        Clear();

        if (GetNode3D() is not World.IDebugDrawable drawable)
        {
            return;
        }
            
        DebugDraw.Clear();
        drawable.DrawDebug();
        
        var material = GetPlugin().GetMaterial("lines", this);
        
        foreach (var line in DebugDraw.Lines)
        {
            var points = new Vector3[] { line.From, line.To };
            AddLines(points, material, false, line.Color);
        }

        foreach (var box in DebugDraw.Boxes)
        {
            DrawBox(box.Center, box.Size, box.Color, material);
        }

        foreach (var sphere in DebugDraw.Spheres)
        {
            DrawSphere(sphere.Center, sphere.Radius, sphere.Color, material);
        }

        foreach (var point in DebugDraw.Points)
        {
            DrawPoint(point.Position, point.Size, point.Color, material);
        }
    }

    private void DrawBox(Vector3 center, Vector3 size, Color color, StandardMaterial3D material)
    {
        var hs = size / 2;
        var corners = new Vector3[8]
        {
            center + new Vector3(-hs.X, -hs.Y, -hs.Z),
            center + new Vector3( hs.X, -hs.Y, -hs.Z),
            center + new Vector3( hs.X, -hs.Y,  hs.Z),
            center + new Vector3(-hs.X, -hs.Y,  hs.Z),
            center + new Vector3(-hs.X,  hs.Y, -hs.Z),
            center + new Vector3( hs.X,  hs.Y, -hs.Z),
            center + new Vector3( hs.X,  hs.Y,  hs.Z),
            center + new Vector3(-hs.X,  hs.Y,  hs.Z)
        };

        var lines = new Vector3[24]
        {
            // Bottom face
            corners[0], corners[1],
            corners[1], corners[2],
            corners[2], corners[3],
            corners[3], corners[0],
            // Top face
            corners[4], corners[5],
            corners[5], corners[6],
            corners[6], corners[7],
            corners[7], corners[4],
            // Vertical edges
            corners[0], corners[4],
            corners[1], corners[5],
            corners[2], corners[6],
            corners[3], corners[7]
        };

        AddLines(lines, material, false, color);
    }

    private void DrawSphere(Vector3 center, float radius, Color color, StandardMaterial3D material)
    {
        const int segments = 16;
        var lines = new Vector3[segments * 3 * 2];
        int index = 0;

        for (int axis = 0; axis < 3; axis++)
        {
            for (int i = 0; i < segments; i++)
            {
                float angle1 = (float)i / segments * Mathf.Tau;
                float angle2 = (float)(i + 1) / segments * Mathf.Tau;

                lines[index++] = GetCirclePoint(center, radius, angle1, axis);
                lines[index++] = GetCirclePoint(center, radius, angle2, axis);
            }
        }

        AddLines(lines, material, false, color);
    }

    private Vector3 GetCirclePoint(Vector3 center, float radius, float angle, int axis)
    {
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return axis switch
        {
            0 => center + new Vector3(x, y, 0),
            1 => center + new Vector3(x, 0, y),
            2 => center + new Vector3(0, x, y),
            _ => center
        };
    }

    private void DrawPoint(Vector3 position, float size, Color color, StandardMaterial3D material)
    {
        var hs = size / 2;
        var lines = new Vector3[6]
        {
            position + new Vector3(-hs, 0, 0), position + new Vector3(hs, 0, 0),
            position + new Vector3(0, -hs, 0), position + new Vector3(0, hs, 0),
            position + new Vector3(0, 0, -hs), position + new Vector3(0, 0, hs)
        };

        AddLines(lines, material, false, color);
    }
}


#endif

