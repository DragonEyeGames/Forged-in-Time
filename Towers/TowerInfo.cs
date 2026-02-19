using Godot;
using System;

public partial class TowerInfo : CanvasLayer
{
	public void on_sell_pressed()
		{
			GD.Print("werk");
			QueueFree();
		}
}
