using Godot;
using System;

public partial class Troop : CharacterBody2D
{
	public bool player1=false;
	public const float Speed = 400.0f;
	public const float JumpVelocity = -400.0f;
	public NavigationAgent2D navAgent;
	[Export] public Node2D target;
	[Export] public int health=5;
	private AnimatedSprite2D sprite;
	
	public override void _Ready(){
		navAgent=GetNode<NavigationAgent2D>("NavAgent");
		sprite=GetNode<AnimatedSprite2D>("Sprite");
		navAgent.PathMaxDistance=10.0f;
		updateHitboxes();
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
		if(Velocity.X>0){
			sprite.FlipH=false;
		}
		if(Velocity.X<0){
			sprite.FlipH=true;
		}
	}
	
	public void recalculate(){
		navAgent.TargetPosition=target.GlobalPosition;
	}
	
	public async void updateHitboxes(){
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		if(player1){
			GetNode("Player2").QueueFree();
		} else if(!player1){
			GetNode("Player1").QueueFree();
		}
	}
		
}
