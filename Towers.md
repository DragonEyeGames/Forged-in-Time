This is a breakdown of the tower system

Here is the scene tree and explanation. The scene tree in question is turret

Base Node; The base node
	Base;
	Turret;
		Fire; This and the two above are purely for decoration
	Player1; An area 2D with no collision layer and a collision mask of 2
		CollisionShape2D; A collision shape (typically a circle)
	Player2; An area 2D with no collision layer and a collision mask of 3
		CollisionShape2D; A collision shape (should be identical to the other one)
	Animator; An animation player purely for visual gain
	Cooldown; The cooldown timer. Makes it so it can't attack too fast.
	Player1Territory; An area 2D that takes area. Should be on collision layer 4 with no collision mask
		CollisionShape2D; The collision shape for claiming territory
	Player2Territory; Like the other one, but on collision layer 5 instead
		CollisionShape2D; This should be identical to the other territory collision shape
		
	If you are making a tower that doesn't attack, you will not need the Player1 or Player2 nodes.

And this is the script with explanation

using Godot;
using System;
using System.Collections;
using System.Collections.Generic; This is needed for lists so import it

public partial class Turret : Tower
{
	private bool canShoot = true; Used for cooldowns
	private List<CharacterBody2D> player1Colliding = new List<CharacterBody2D> {}; Used for attacks
	private List<CharacterBody2D> player2Colliding = new List<CharacterBody2D> {}; Player 1's troops and Player 2's troops respectably
	private Sprite2D turret; A sprite
	[Export] public int damage=1; The damage
	[Export] public Timer cooldown;  Cooldown timer
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turret=GetNode<Sprite2D>("Turret"); Sets the sprite reference
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TowerGenerics(); Needed for every tower. Call this no matter what. (abstraction in action)
		if(!hovering && canShoot){ This section is for shooting. Not needed if the tower doesn't attack
			if(Player1 && player2Colliding.Count>0){ If we are player one and can shoot player 2
				canShoot=false;
				cooldown.Start(); Both cooldown related
				turret.LookAt(player2Colliding[0].GlobalPosition); Purely visual
				turret.GlobalRotation-=(float)Math.PI/2; Same here
				Troop troop = player2Colliding[0] as Troop; Gets the oldest enemy we are still colliding with
				troop.health-=damage; Deals damage
				GetNode<AnimationPlayer>("Animator").Play("pew"); Plays a shooting animation
			}
			if(!Player1 && player1Colliding.Count>0){ Same stuff but for player 2
				canShoot=false;
				cooldown.Start();
				turret.LookAt(player1Colliding[0].GlobalPosition);
				turret.GlobalRotation-=(float)Math.PI/2;
				Troop troop = player1Colliding[0] as Troop;
				troop.health-=damage;
				GetNode<AnimationPlayer>("Animator").Play("pew");
			}
		}
		
	}
	
	These are all hooked up to areas to detect the troops
	Hook area entered and exited up to these functions
	
	public void Player1Entered(Node2D body){
		player1Colliding.Add(body.GetParent() as CharacterBody2D);
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
	
	This is used for a cooldown
	
	public void cooled(){
		canShoot=true;
	}
}
