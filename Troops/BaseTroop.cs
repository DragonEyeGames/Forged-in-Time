using Godot;
using System;

public abstract partial class BaseTroop : CharacterBody2D
{
    public bool player1 = true;
    public abstract float Speed {get; set;}
    public abstract int health {get; set;}
    public abstract int maxHealth {get; set;}
    public abstract int damage {get; set;}
    public abstract bool healer {get; set;}
    public abstract int healthLevel  {get; set;}
    public abstract int speedLevel  {get; set;}
    public abstract int damageLevel  {get; set;}
    public bool attacking = false;
    public bool pathfinding = true;

    
    public abstract NavigationAgent2D navAgent {get; set;}
    public abstract TargetBase target {get; set;}
    public abstract AnimatedSprite2D sprite  {get; set;}
    public abstract Timer cooldown {get; set;}

    
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
    }

    public override void _PhysicsProcess(double delta)
    {
        if (pathfinding)
        {
            Vector2 velocity = Vector2.Zero;
            var dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
            velocity = dir * 40;
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

        if (target.health <= 0)
        {
            QueueFree();
        }

    }

    public void attack(int damage)
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
            cooldown.Start();
        }
    }
    
    public void altAttack(int damage)
    
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
