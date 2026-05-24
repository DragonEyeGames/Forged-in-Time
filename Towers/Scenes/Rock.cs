using Godot;
using System;

public partial class Rock : CharacterBody2D
{
	public bool player1=false;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Right.Rotated(Rotation);// - (float)Math.PI/2
		Velocity = direction * 400;
		MoveAndSlide();
	}
	
	public void Player1Hit(Node2D body){
		if(player1){
			return;
		}
		BaseTroop troop = body.GetParent() as BaseTroop;
		troop.health-=1;
		troop.Hit();
		QueueFree();
	}
	
	public void Player2Hit(Node2D body){
		if(!player1){
			return;
		}
		BaseTroop troop = body.GetParent() as BaseTroop;
		troop.health-=1;
		troop.Hit();
		QueueFree();
	}
}
