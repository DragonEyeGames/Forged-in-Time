using Godot;
using System;

public partial class Troop : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -400.0f;
	public NavigationAgent2D navAgent;
	[Export] public Node2D target;
	[Export] public int health=5;
	
	public override void _Ready(){
		navAgent=GetNode<NavigationAgent2D>("NavAgent");
		navAgent.PathMaxDistance=10.0f;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity=Vector2.Zero;
		var dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
		velocity = dir * 40;
		Velocity=velocity;
		if(!navAgent.IsNavigationFinished()){
			MoveAndSlide();
		}
		if(health<=0){
			QueueFree();
		}
	}
	
	public void recalculate(){
		navAgent.TargetPosition=target.GlobalPosition;
	}
}
