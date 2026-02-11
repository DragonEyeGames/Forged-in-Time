using Godot;
using System;

public partial class Base : Sprite2D
{
	private NavigationAgent2D navAgent;
	[Export] public Node2D target;
	[Export]
	public PackedScene troop;

	public void spawnTroop(){
		Troop newTroop = troop.Instantiate() as Troop;
		GetParent().AddChild(newTroop);
		newTroop.GlobalPosition=GlobalPosition;
		newTroop.target=target;
	}
}
