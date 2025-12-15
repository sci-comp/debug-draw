#if TOOLS

using Godot;

public partial class DebugDrawGizmoPlugin : EditorNode3DGizmoPlugin
{
    public DebugDrawGizmoPlugin()
    {
        CreateMaterial("lines", Colors.White, false, false, true);
    }

    public override string _GetGizmoName()
    {
        return "DebugDraw";
    }

    public override bool _HasGizmo(Node3D forNode)
    {
        return forNode is World.IDebugDrawable;
    }

    public override EditorNode3DGizmo _CreateGizmo(Node3D forNode)
    {
        if (forNode is not World.IDebugDrawable)
        {
            return null;
        }
        return new DebugDrawGizmo();
    }

}

#endif