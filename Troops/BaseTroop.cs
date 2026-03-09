using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseTroop : CharacterBody2D
{
    public bool player1 = true;
    public abstract float Speed {get; set;}
    public abstract int health {get; set;}
    public abstract int damage {get; set;}
    public abstract bool healer {get; set;}
    public bool attacking = false;
    public bool pathfinding = true;

    
    public abstract NavigationAgent2D navAgent {get; set;}
    public abstract Base target {get; set;}
    public abstract AnimatedSprite2D sprite  {get; set;}
    public abstract Timer cooldown {get; set;}
    public List<BaseTroop> Freinds = new List<BaseTroop>();
    
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
            Vector2 velocity;
            velocity = Vector2.Zero;
            var dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
            velocity = dir;
            Velocity = velocity * Speed;
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
                target.health -= damage;
                GD.Print(target.health);
                if (target.health <= 0)
                {
                    target.Die();
                }

                attacking = false;
                cooldown.Start();
            }
    }

    public void heal(int damage)
    {
        GD.Print("heal");
        if (Freinds != null)
        {
            for (int i = 0; i < Freinds.Count; i++)
            {
                BaseTroop BestFreind =  Freinds[i] as BaseTroop;
                BestFreind.health += damage;
                GD.Print(BestFreind.health);
            
            }
        }
    }
    
    public void on_path_finished()
    {
        attack(damage);
        pathfinding =  false;
    }
    
    public void on_cooldown()
    {
        if (!healer)
        {
            attacking = false;
            attack(damage);
        }
        else if (healer)
        {
            attacking = false;
            heal(damage);
        }
    }

    public void healStart(Node2D body)
    {
        GD.Print("healStart");
        if (body is BaseTroop)
        {
            BaseTroop FreindlyTroop = body as BaseTroop;
            if (FreindlyTroop.target == this.target)
            {
                Freinds.Add(FreindlyTroop);
                cooldown.Start();
            }

        }
    }
    
    public void healEnd(Node2D body)
    {
        if (body is BaseTroop)
        {
            BaseTroop FreindlyTroop = body as BaseTroop;
            if (FreindlyTroop.target == this.target)
            {
                Freinds.Remove(FreindlyTroop);
            }

        }
    }
}
