using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Turret : Tower
{
	[Export] bool Player1 = false;
	private bool canShoot = true;
	private List<CharacterBody2D> player1Colliding = new List<CharacterBody2D> {};
	private List<CharacterBody2D> player2Colliding = new List<CharacterBody2D> {};
	private Sprite2D turret;
	[Export] public int damage=1;
	[Export] public Timer cooldown;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turret=GetNode<Sprite2D>("Turret");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TowerGenerics();
		if(!hovering && canShoot){
			if(Player1 && player2Colliding.Count>0){
				canShoot=false;
				cooldown.Start();
				turret.LookAt(player2Colliding[0].GlobalPosition);
				turret.GlobalRotation-=(float)Math.PI/2;
				Troop troop = player2Colliding[0] as Troop;
				troop.health-=damage;
				GetNode<AnimationPlayer>("Animator").Play("pew");
			}
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
	
	public void cooled(){
		canShoot=true;
	}
}
