using Godot;
using System;

public partial class Base : Sprite2D
{
	private NavigationAgent2D navAgent;
	private Node2D target;
	[Export]
	public PackedScene troop;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		target = GetNode<Node2D>("../Icon2");
	}
	public void spawnTroop(){
		Troop newTroop = troop.Instantiate() as Troop;
		GetParent().AddChild(newTroop);
		newTroop.GlobalPosition=GlobalPosition;
		newTroop.target=target;
	}
}
