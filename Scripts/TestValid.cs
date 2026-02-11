using Godot;
using System;

public partial class TestValid : Sprite2D
{
	private NavigationAgent2D navAgent;
	private Node2D target;
	private bool valid = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		navAgent=GetNode<NavigationAgent2D>("NavAgent");
		target=GetNode<Node2D>("../Enemy");
	}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//CheckValid();
	}
	
	public void CheckValid(){
		navAgent.TargetPosition=target.GlobalPosition;
		valid=navAgent.IsTargetReachable();
	}
	
	public bool isValid(){
		CheckValid();
		return valid;
	}
	
}
