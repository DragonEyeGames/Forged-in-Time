using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Turret : Node2D
{
	[Export] bool Player1 = false;
	private List<CharacterBody2D> player1Colliding = new List<CharacterBody2D> {};
	private List<CharacterBody2D> player2Colliding = new List<CharacterBody2D> {};
	private Sprite2D turret;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turret=GetNode<Sprite2D>("Turret");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Player1 && player2Colliding.Count>0){
			turret.LookAt(player2Colliding[0].GlobalPosition);
			turret.GlobalRotation-=(float)Math.PI/2;
			player2Colliding[0].QueueFree();
			player2Colliding.RemoveAt(0);
		}
	}
	
	public void Player1Entered(Node2D body){
		player1Colliding.Add(body as CharacterBody2D);
	}
	
	public void Player1Exited(Node2D body){
		player1Colliding.Remove(body as CharacterBody2D);
	}
	
	public void Player2Entered(Node2D body){
		player2Colliding.Add(body as CharacterBody2D);
	}
	
	public void Player2Exited(Node2D body){
		player2Colliding.Remove(body as CharacterBody2D);
	}
}
