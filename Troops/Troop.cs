using Godot;
using System;

public partial class Troop : BaseTroop
{

	public override float Speed { get; set; } = 400.0f;
	public override int health { get; set; } = 5;
	public override int damage { get; set; } = 1;
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	public override Base target { get; set; }
	public override Timer cooldown {get; set;}
	

	public override void _Ready()
	{
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		cooldown = GetNode<Timer>("Cooldown");
		navAgent.PathMaxDistance = 10.0f;
		updateHitboxes();
	}
	
}	
