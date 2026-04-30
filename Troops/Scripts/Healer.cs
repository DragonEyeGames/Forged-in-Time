using Godot;
using System;

public partial class Healer : BaseTroop
{
	[Export] public override int speedLevel {get; set;}
	[Export] public override float speed { get; set; } = 25.0f;
	[Export] public override int maxHealth { get; set; } = 3;
	[Export] public override int health { get; set; }
	[Export] public override int healthLevel { get; set; } = 0;
	[Export] public override int damageLevel { get; set; } = 0;
	[Export] public override int damage { get; set; } = 1;
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	[Export] public override TargetBase target { get; set; }
	public override Timer cooldown {get; set;}
	public override bool healer { get; set; } = true;
	public override GameManager.Towers troopType { get; set; }
	[Export] public override int upgradeLevel {get; set;} = 0;

	

	public async override void _Ready()
	{
		troopType=GameManager.Towers.Healer;
		health = maxHealth;
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		cooldown = GetNode<Timer>("Cooldown"); updateHitboxes();
		navAgent.TargetDesiredDistance = 25;
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		fetchUpgrades();
		TargetSet();
	}


}
