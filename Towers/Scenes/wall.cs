using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Wall : Tower
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TowerGenerics();
	}
}
