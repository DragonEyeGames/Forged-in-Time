using Godot;
using System;

public partial class BG : ParallaxBackground
{
	[Export] public float ScrollSpeed = 100.0f;

	public override void _Process(double delta)
	{
		Vector2 newOffset = ScrollOffset;
		newOffset.X -= ScrollSpeed * (float)delta;
		ScrollOffset = newOffset;
	}
}
