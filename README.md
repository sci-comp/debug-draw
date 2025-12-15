# DebugDraw

Editor-only debug visualization system for Godot level design.

## Usage

1. Implement `World.IDebugDrawable` on your Node3D
2. Call `DebugDraw` methods in your `DrawDebug()` implementation
3. Toggle the preview on to see gizmos in the editor

## Example

	public partial class ProjectileLauncher : Node3D, World.IDebugDrawable
	{
		[Export] public bool ShowPreview = false;
		[Export] public float Force = 10f;

		public override void _Ready()
		{
			// IsEditorHint() returns true when this node is running in the editor viewport
			// (not when you press Play). We only want runtime initialization when actually
			// playing the game, not while editing the scene.
			if (!Engine.IsEditorHint())
			{
				// Runtime initialization here
			}
		}

	#if TOOLS
		// #if TOOLS is a compile-time directive - this code is completely removed from
		// exported builds. This ensures zero performance cost in shipped games.

		public override void _Process(double delta)
		{
			// We need IsEditorHint() here because _Process runs both in the editor viewport
			// AND when you press Play. We only want to update gizmos while editing scenes,
			// not during gameplay testing.
			if (Engine.IsEditorHint() && ShowPreview)
			{
				UpdateGizmos();  // Tells the gizmo system to call DrawDebug() again
			}
		}

		public void DrawDebug()
		{
			// This method is only called by the DebugDrawGizmo system, which only exists
			// in the editor, so it's safe to wrap in #if TOOLS. It will never be called
            // in exported builds.

            Vector3 launchDir = -GlobalTransform.Basis.Z;
            Vector3 startPos = GlobalPosition + 0.25f * launchDir;
            Vector3 initialVelocity = launchDir * Force;

            // Draw projectile arc with physics
            DebugDraw.Arc(startPos, initialVelocity, new Vector3(0, -9.8f, 0), color: Colors.Red);
        }
    #endif
    }

## API

### Primitives
- `DebugDraw.Line(Vector3 from, Vector3 to, Color color)`
- `DebugDraw.Box(Vector3 center, Vector3 size, Color color)`
- `DebugDraw.Sphere(Vector3 center, float radius, Color color)`
- `DebugDraw.Point(Vector3 position, float size, Color color)`

### Composite Shapes
- `DebugDraw.Path(Vector3[] points, Color color)` - Connects array of points with lines
- `DebugDraw.Arc(Vector3 startPos, Vector3 initialVelocity, Vector3 gravity, float duration = 1f, int segments = 30, Color? color = null)` - Draws physics-based projectile trajectory

## Notes

- Only works in editor (guarded with `#if TOOLS`)
- Call `UpdateGizmos()` to refresh the visualization
- Zero runtime cost in exported builds
