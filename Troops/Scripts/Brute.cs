using Godot;
using System;

public partial class Brute : BaseTroop
{
	[Export] public override float speed { get; set; } = 20.0f;
	[Export] public  override int health { get; set; } = 20;
	[Export] public override int maxHealth { get; set; } = 20;
	[Export] public override int damage { get; set; } = 2;
	
	[Export] public override int upgradeLevel {get; set;} = 0;
	[Export] public override int speedLevel {get; set;} = 0;
	[Export] public override int healthLevel { get; set; } = 0;
	[Export] public override int damageLevel { get; set; } = 0;
	public override TargetBase target { get; set; }
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	public override Timer cooldown {get; set;}
	public override bool healer { get; set; } = false;
	public override GameManager.Towers troopType { get; set; }

	

	public async override void _Ready()
	{
		troopType=GameManager.Towers.Brute;
		health = maxHealth;
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		cooldown = GetNode<Timer>("Cooldown"); updateHitboxes();
		TargetSet();
		navAgent.TargetDesiredDistance = 64;
		foreach (AnimatedSprite2D child in GetNode<Node2D>("Sprites").GetChildren()){
			child.Visible=false;
		}
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		Vector4 upgradeSpread=fetchUpgrades();
		GD.Print(upgradeSpread);
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
	}
}
