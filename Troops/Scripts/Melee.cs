using Godot;
using System;

public partial class Melee : BaseTroop
{
	[Export] public override int speedLevel {get; set;}
	[Export] public override float speed { get; set; } = 40.0f;
	[Export] public override int healthLevel { get; set; } = 0;
	[Export] public override int health { get; set; } = 5;
	[Export] public override int maxHealth { get; set; } = 5;
	[Export] public override int damageLevel { get; set; } = 0;
	[Export] public override int damage { get; set; } = 1;
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	public override TargetBase target { get; set; }
	public override Timer cooldown {get; set;}
	public override bool healer { get; set; } = false;
	public override GameManager.Towers troopType { get; set; }
	[Export] public override int upgradeLevel {get; set;} = 0;


	public async override void _Ready()
	{
		troopType=GameManager.Towers.Melee;
		health = maxHealth;
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		
		cooldown = GetNode<Timer>("Cooldown");
		navAgent.TargetDesiredDistance = 64;
		foreach (AnimatedSprite2D child in GetNode<Node2D>("Sprites").GetChildren()){
			child.Visible=false;
		}
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		fetchUpgrades();
		TargetSet();
		if(upgradeLevel>6){
			sprite = GetNode<AnimatedSprite2D>("Sprites/3");
			 GetNode<AnimatedSprite2D>("Sprites/3").Visible=true;
		} else if(upgradeLevel>30){
			sprite = GetNode<AnimatedSprite2D>("Sprites/2");
			 GetNode<AnimatedSprite2D>("Sprites/2").Visible=true;
		} else {
		}
		sprite = GetNode<AnimatedSprite2D>("Sprites/1");
		//GetNode<AnimatedSprite2D>("Sprites/1").Visible=true;
		if(player1){
			if(Player1Manager.upgradeLevel==1){
				sprite = GetNode<AnimatedSprite2D>("Sprites/2");
			 	GetNode<AnimatedSprite2D>("Sprites/2").Visible=true;
			}
			if(Player1Manager.upgradeLevel==2){
				sprite = GetNode<AnimatedSprite2D>("Sprites/3");
			 	GetNode<AnimatedSprite2D>("Sprites/3").Visible=true;
			}
			if(Player1Manager.upgradeLevel==3){
				sprite = GetNode<AnimatedSprite2D>("Sprites/4");
			 	GetNode<AnimatedSprite2D>("Sprites/4").Visible=true;
			}
		}
		sprite.Visible=true;
		sprite.Scale = new Vector2(-1, 1);
	}
}
