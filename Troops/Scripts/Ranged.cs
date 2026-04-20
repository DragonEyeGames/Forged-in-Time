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
	[Export] public override Base target { get; set; }
	public override Timer cooldown {get; set;}
	public override bool healer { get; set; } = false;
	public override GameManager.Towers troopType { get; set; }
	

	public override void _Ready()
	{
		troopType=GameManager.Towers.Ranged;
		health = maxHealth;
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		cooldown = GetNode<Timer>("Cooldown"); updateHitboxes();
		navAgent.TargetDesiredDistance = 50;
		fetchUpgrades();
	}
}
