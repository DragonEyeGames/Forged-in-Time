using Godot;
using System;

public partial class Brute : Troop
{
	[Export] public override float speed { get; set; } = 20.0f;
	[Export] public  override int health { get; set; } = 20;
	[Export] public override int maxHealth { get; set; } = 20;
	[Export] public override int damage { get; set; } = 2;
	
	[Export] public override int upgradeLevel {get; set;} = 0;
	[Export] public override int speedLevel {get; set;} = 0;
	[Export] public override int healthLevel { get; set; } = 0;
	[Export] public override int damageLevel { get; set; } = 0;
	
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	[Export] public override Base target { get; set; }
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
		navAgent.TargetDesiredDistance = 64;
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		Vector4 upgradeSpread=fetchUpgrades();
		GD.Print(upgradeSpread);
	}
}
