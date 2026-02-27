using Godot;
using System;

public partial class Troop : BaseTroop
{

	public override float Speed { get; set; } = 400.0f;
	public override int health { get; set; } = 5;
	public override NavigationAgent2D navAgent { get; set; }
	public override AnimatedSprite2D sprite  {get; set;}
	public override Base target { get; set; }
	public override int damage { get; set; } = 1;

	public override void _Ready()
	{
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		sprite = GetNode<AnimatedSprite2D>("Sprite");
		navAgent.PathMaxDistance = 10.0f;
		updateHitboxes();
	}

	public override void attack(int damage)
	{
		if (!attacking)
		{
			target.health  -= damage;
			GD.Print(target.health);
			if (target.health <= 0)
			{
				target.Die();
			}
			attacking = false;
			GetNode<Timer>("Cooldown").Start();
		}
	}

	public void on_path_finished()
	{
		attack(damage);
		pathfinding =  false;
	}

	public void on_cooldown()
	{
		attacking = false;
		attack(damage);
	}
}	
