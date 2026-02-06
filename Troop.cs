using Godot;
using System;

public partial class Troop : CharacterBody2D
{
	public const float Speed = 600.0f;
	public const float JumpVelocity = -400.0f;
	public NavigationAgent2D navAgent;
	[Export] public Node2D target;
	
	public override void _Ready(){
		navAgent=GetNode<NavigationAgent2D>("NavAgent");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		navAgent.TargetPosition=target.GlobalPosition;
		Vector2 velocity=Vector2.Zero;
		var dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
		velocity = dir * 100;
		Velocity=velocity;
		if(!navAgent.IsNavigationFinished()){
			MoveAndSlide();
		}
	}
}
