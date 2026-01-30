#if TOOLS
using Godot;

namespace World
{
    [Tool]
    public partial class GizmoTest : Node3D, IDebugDrawable
    {
        private bool _showDebug;

        [Export]
        public bool ShowDebug
        {
            get => _showDebug;
            set
            {
                if (_showDebug != value)
                {
                    _showDebug = value;
                    if (Engine.IsEditorHint())
                    {
                        UpdateGizmos();
                    }
                }
            }
        }

        public override void _Process(double delta)
        {
            if (ShowDebug && Engine.IsEditorHint())
            {
                UpdateGizmos();
            }
        }

        public void DrawDebug()
        {
            if (ShowDebug)
            {
                DebugDraw.Line(Vector3.Zero, Vector3.Forward * 5, Colors.Yellow);
            }
        }

    }

}
#endif 

