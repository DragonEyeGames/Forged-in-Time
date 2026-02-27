using Godot;
using System;

public abstract partial class BaseTroop : CharacterBody2D
{
    public bool player1 = true;
    [Export] public abstract float Speed {get; set;}
    [Export] public abstract int health {get; set;}
    [Export] public abstract int damage {get; set;}
    public bool attacking = false;
    public bool pathfinding = true;

    
    public abstract NavigationAgent2D navAgent {get; set;}
    public abstract Base target {get; set;}
    public abstract AnimatedSprite2D sprite  {get; set;}

    
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
    }

    public abstract void attack(int damage);
}
