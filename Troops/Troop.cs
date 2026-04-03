using Godot;
using System;

public partial class Troop : BaseTroop
{
	[Export] public override int speedLevel {get; set;}
	[Export] public override float Speed { get; set; } = 40.0f;
	[Export] public override int healthLevel { get; set; } = 0;
	[Export] public override int health { get; set; } = 5;
	[Export] public override int maxHealth { get; set; } = 5;
	[Export] public override int damageLevel { get; set; } = 0;
	[Export] public override int damage { get; set; } = 1;
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	public override Base target { get; set; }
	public override Timer cooldown {get; set;}
	public override bool healer { get; set; } = false;


	public override void _Ready()
	{
		health = maxHealth;
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		cooldown = GetNode<Timer>("Cooldown");
		navAgent.PathMaxDistance = 10.0f;
		updateHitboxes();
	}
	
}	
