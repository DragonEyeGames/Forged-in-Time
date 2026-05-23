using Godot;
using System;

public partial class Ranged : BaseTroop
{
	[Export] public override float speed { get; set; } = 30.0f;
	
	[Export] public  override int health { get; set; } = 5;
	[Export] public override int maxHealth { get; set; } = 5;
	
	[Export] public override int upgradeLevel {get; set;} = 0;
	[Export] public override int speedLevel {get; set;} = 0;
	[Export] public override int damageLevel { get; set; } = 0;
	[Export] public override int healthLevel { get; set; } = 0;
	
	[Export] public override int damage { get; set; } = 2;
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	[Export] public override TargetBase target { get; set; }
	public override Timer cooldown {get; set;}
	public override bool healer { get; set; } = false;
	public override GameManager.Towers troopType { get; set; }
	

	public async override void _Ready()
	{
		troopType=GameManager.Towers.Ranged;
		health = maxHealth;
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		cooldown = GetNode<Timer>("Cooldown"); updateHitboxes();
		navAgent.TargetDesiredDistance = 300;
		sprite = GetNode<AnimatedSprite2D>("Sprites/1");
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		initialize();
		fetchUpgrades();
		TargetSet();
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
		if(!player1){
			if(Player2Manager.upgradeLevel==1){
				sprite = GetNode<AnimatedSprite2D>("Sprites/2");
			 	GetNode<AnimatedSprite2D>("Sprites/2").Visible=true;
			}
			if(Player2Manager.upgradeLevel==2){
				sprite = GetNode<AnimatedSprite2D>("Sprites/3");
			 	GetNode<AnimatedSprite2D>("Sprites/3").Visible=true;
			}
			if(Player2Manager.upgradeLevel==3){
				sprite = GetNode<AnimatedSprite2D>("Sprites/4");
			 	GetNode<AnimatedSprite2D>("Sprites/4").Visible=true;
			}
		}
		sprite.Visible=true;
		sprite.Scale = new Vector2(-1, 1);
	}
}
