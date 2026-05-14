using Godot;
using System;

public abstract partial class BaseTroop : CharacterBody2D
{
	public bool player1 = true;
	public abstract float speed { get; set; }
	public abstract int health { get; set; }
	public abstract int maxHealth { get; set; }
	public abstract int damage { get; set; }
	public abstract bool healer { get; set; }

	public abstract int upgradeLevel { get; set; }
	public abstract int healthLevel { get; set; }
	public abstract int speedLevel { get; set; }
	public abstract int damageLevel { get; set; }

	public bool attacking = false;
	public bool pathfinding = true;


	public abstract NavigationAgent2D navAgent { get; set; }
	public abstract TargetBase target { get; set; }
	public abstract AnimatedSprite2D sprite { get; set; }
	public abstract Timer cooldown { get; set; }

	public abstract GameManager.Towers troopType { get; set; }
	

	public Vector4 fetchUpgrades()
	{
		int id = 2;
		if (player1)
		{
			id = 1;
		}

		Vector4 upgrades = GameManager.fetchUpgrades(id, troopType);
		upgradeLevel = (int)upgrades.X;
		speedLevel = (int)upgrades.Y;
		healthLevel = (int)upgrades.Z;
		damageLevel = (int)upgrades.W;
		speed += TroopUpgrades.Speed[speedLevel];
		damage += TroopUpgrades.Damage[damageLevel];
		health += TroopUpgrades.Health[healthLevel];
		return upgrades;
	}

	public void TargetSet()
	{
		if (player1)
		{
			target = GameManager.player1Target;
		}
		else if (!player1)
		{
			target = GameManager.player2Target;
		}

		if (!target.isBase)
		{
			Miner miner = target as Miner;
			miner.OwnerChanged1 -= minerDied1;
			miner.OwnerChanged2 -= minerDied2;
			miner.OwnerChanged1 += minerDied1;
			miner.OwnerChanged2 += minerDied2;
		}
	}

	public async void updateHitboxes()
	{
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		if (player1)
		{
			GetNode("Player2").QueueFree();
		}
		else if (!player1)
		{
			GetNode("Player1").QueueFree();
		}
	}


	public void recalculate()
	{
		navAgent.TargetPosition = target.GlobalPosition;
		if (player1)
		{
			target = GameManager.player1Target;
		}
		else if (!player1)
		{
			target = GameManager.player2Target;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (target == null)
		{
			TargetSet();
			return;
		}
		
		if (pathfinding)
		{
			Vector2 velocity = Vector2.Zero;
			var dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
			velocity = dir * speed;
			Velocity = velocity;
			if (!navAgent.IsNavigationFinished())
			{
				MoveAndSlide();
			}

			if (health <= 0)
			{
				QueueFree();
			}

			if (Velocity.X > 0)
			{
				sprite.FlipH = false;
			}

			if (Velocity.X < 0)
			{
				sprite.FlipH = true;
			}
		}

	}

	public void attack(int damage)
	{
		if (!attacking)
		{
			attacking = true;
			if (!target.isBase)
			{
				Miner miner = target as Miner;
				if (player1 && miner.playerOwned != 1)
				{
					target.health -= damage;
				}
				else if (!player1 && miner.playerOwned != 2)
				{
					target.health -= damage;
				}
				else if (player1 && miner.playerOwned == 1)
				{
					target = GameManager.player1DefaultTarget;
					pathfinding = true;
					recalculate();
					return;
				}
				else if (!player1 && miner.playerOwned == 2)
				{
					target = GameManager.player2DefaultTarget;
					pathfinding = true;
					recalculate();
					return;
				}

				if (target.health <= 0)
				{
					{
						miner.playerKilled = player1;
						if (player1)
						{
							target.Die();
							target = GameManager.player1DefaultTarget;
						}
						else if (!player1)
						{
							target.Die();
							target = GameManager.player2DefaultTarget;
						}

						attacking = false;
						pathfinding = true;
						recalculate();
						return;
					}
				}
				else
				{
					cooldown.Start();
				}

			if (target.isBase)
			{
				target.health -= damage;
				if (target.health <= 0)
				{
					GD.Print("YOI ONE YIPEEEEEEEE");
					QueueFree();
					return;
				}
				else
				{
					cooldown.Start();
				}
			}

				attacking = false;
			}
		}
	}

	public void on_path_finished()
	{
		pathfinding = false;
		attack(damage);
	}

	public void on_cooldown()
	{
		attacking = false;
		if (!pathfinding)
		{
			attack(damage);
		}
	}

	public void targetSwitch()
	{
		if (player1)
		{
			target = GameManager.player1Target;
			recalculate();
		}
		else
		{
			target = GameManager.player2Target;
			recalculate();
		}
	}

	public async void minerDied1()
	{	
		await ToSignal(GetTree().CreateTimer(GD.Randf() * 1f), Timer.SignalName.Timeout);
		target = GameManager.player1Target;
		pathfinding = true;
		recalculate();
	}

	public async void minerDied2()
	{
		await ToSignal(GetTree().CreateTimer(GD.Randf() * 1f), Timer.SignalName.Timeout);
		target = GameManager.player2Target;
		pathfinding = true;
		recalculate();
	}

}