#if TOOLS

using Godot;

[Tool]
public partial class DebugDrawPlugin : EditorPlugin
{
    private DebugDrawGizmoPlugin _gizmoPlugin;

    public override void _EnterTree()
    {
        _gizmoPlugin = new DebugDrawGizmoPlugin();
        AddNode3DGizmoPlugin(_gizmoPlugin);
    }

    public override void _ExitTree()
    {
        RemoveNode3DGizmoPlugin(_gizmoPlugin);
    }
}

#endif

