using Godot;
using System;
using System.Collections.Generic;

public partial class AoeTower :  Tower
{
	private List<CharacterBody2D> player1Colliding = new List<CharacterBody2D> {};
	private List<CharacterBody2D> player2Colliding = new List<CharacterBody2D> {};
	[Export] public int damage=3;
	[Export] public Timer cooldown;
	private bool canShoot = true;
	private bool backupHover = true;
	
	public override void _Process(double delta)
	{
		if(backupHover!=hovering){
			shoot();
			backupHover=hovering;
		}
		TowerGenerics();
		if (!hovering && canShoot)
		{
			if (Player1 && player2Colliding.Count > 0)
			{
				GD.Print(player2Colliding.Count);
				canShoot = false;
				cooldown.Start();
				shoot();
				for (int i = 0; i <= player2Colliding.Count - 1; i++)
				{
					BaseTroop troop = player2Colliding[i] as BaseTroop;
					troop.health -= damage;
					troop.Hit();
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Attack");
				}
			}

			if (!Player1 && player1Colliding.Count > 0)
			{
				GD.Print(player1Colliding.Count);
				canShoot = false;
				cooldown.Start();
				shoot();
				for (int i = 0; i <= player1Colliding.Count - 1; i++)
				{
					BaseTroop troop = player1Colliding[i] as BaseTroop;
					GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Attack");
					troop.health -= damage;
					troop.Hit();
					GD.Print("Damagin");
				}
			}
		}
	}
	
	public void Player1Entered(Node2D body){
		player1Colliding.Add(body.GetParent() as CharacterBody2D);
		GD.Print(player1Colliding.Count);

	}
	
	public void Player1Exited(Node2D body){
		player1Colliding.Remove(body.GetParent() as CharacterBody2D);
	}
	
	public void Player2Entered(Node2D body){
		player2Colliding.Add(body.GetParent() as CharacterBody2D);
	}
	
	public void Player2Exited(Node2D body){
		player2Colliding.Remove(body.GetParent() as CharacterBody2D);
	}

	public void on_cooldown_timeout()
	{
		canShoot = true;
	}
	
	public void shoot(){
		foreach(AnimatedSprite2D child in GetNode<Node2D>("Spikes").GetChildren()){
			if(child is AnimatedSprite2D){
				child.Play("spike");
			}
		}
	}
	
}
