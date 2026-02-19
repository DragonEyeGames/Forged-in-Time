using Godot;
using System;

public partial class Base : Sprite2D
{
	private NavigationAgent2D navAgent;
	[Export] public Node2D target;
	[Export]
	[Export] public int health=100;
	public PackedScene troop;

	public void spawnTroop(){
		Troop newTroop = troop.Instantiate() as Troop;
		GetParent().AddChild(newTroop);
		newTroop.GlobalPosition=GlobalPosition;
		newTroop.target=target;
	}
	public void Die()
	{
		if (health <= 0)
		{
			queue_free()
		}
	}
}
