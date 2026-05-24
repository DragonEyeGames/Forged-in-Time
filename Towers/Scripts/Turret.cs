using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Turret : Tower
{
	private bool canShoot = true;
	private List<CharacterBody2D> player1Colliding = new List<CharacterBody2D> {};
	private List<CharacterBody2D> player2Colliding = new List<CharacterBody2D> {};
	private AnimatedSprite2D turret;
	[Export] public int damage=3;
	[Export] public Timer cooldown;
	[Export] private PackedScene arrow;
	// Called when the node enters the scene tree for the first time.

	public override void OnTimeAdvance(bool upgradePlayer, int level){
		if(Player1==upgradePlayer){
			upgrade(level);
		}
	}

	public override void upgrade(int level){
		if(turret==null){
			turret= GetNode<AnimatedSprite2D>("Sprites/1");
		}
		if(level==1){
			turret.Visible=false;
			turret=GetNode<AnimatedSprite2D>("Sprites/2");
			turret.Visible=true;
			damage=4;
			cooldown.WaitTime=.6;
		}
		if(level==2){
			turret.Visible=false;
			turret=GetNode<AnimatedSprite2D>("Sprites/3");
			turret.Visible=true;
			damage=4;
			cooldown.WaitTime=.2;
		}
		if(level==3){
			turret.Visible=false;
			turret=GetNode<AnimatedSprite2D>("Sprites/4");
			turret.Visible=true;
			damage=3;
			cooldown.WaitTime=.1;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(turret==null){
			turret= GetNode<AnimatedSprite2D>("Sprites/1");
		}
		TowerGenerics();
		if(!hovering && canShoot){
			if(Player1 && player2Colliding.Count>0){
				canShoot=false;
				cooldown.Start();
				if(turret!=GetNode<AnimatedSprite2D>("Sprites/1")){
					turret.LookAt(player2Colliding[0].GlobalPosition);
					turret.GlobalRotation+=(float)Math.PI/2;
				}
				if(turret!=GetNode<AnimatedSprite2D>("Sprites/1") && turret!=GetNode<AnimatedSprite2D>("Sprites/2")){
					BaseTroop troop = player2Colliding[0] as BaseTroop;
					troop.health-=damage;
					troop.Hit();
				}
				turret.Play("default");
			}
			if(!Player1 && player1Colliding.Count>0){
				canShoot=false;
				cooldown.Start();
				if(turret!=GetNode<AnimatedSprite2D>("Sprites/1")){
					turret.LookAt(player1Colliding[0].GlobalPosition);
					turret.GlobalRotation+=(float)Math.PI/2;
				}
				if(turret!=GetNode<AnimatedSprite2D>("Sprites/1") && turret!=GetNode<AnimatedSprite2D>("Sprites/2")){
					BaseTroop troop = player1Colliding[0] as BaseTroop;
					troop.health-=damage;
					troop.Hit();
				}
				turret.Play("default");
			}
		}
		
	}
	
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
	
	public void cooled(){
		canShoot=true;
	}
	
	public void fireArrow(){
		if(GetNode<AnimatedSprite2D>("Sprites/2").Animation=="default"){
			GetNode<AnimatedSprite2D>("Sprites/2").Play("shoot_end");
			Arrow newArrow = arrow.Instantiate<Arrow>();
			GetParent().AddChild(newArrow);
			newArrow.GlobalPosition=GetNode<AnimatedSprite2D>("Sprites/2").GlobalPosition;
			newArrow.GlobalRotation=GetNode<AnimatedSprite2D>("Sprites/2").GlobalRotation;
			newArrow.player1=Player1;
		}
			
	}
}
