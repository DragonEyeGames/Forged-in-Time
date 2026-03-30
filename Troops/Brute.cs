using Godot;
using System;

public partial class Brute : BaseTroop
{
    [Export] public override float Speed { get; set; } = 20.0f;
    [Export] public  override int health { get; set; } = 20;
    [Export] public override int maxHealth { get; set; } = 20;
    [Export] public override int damage { get; set; } = 2;
    public override NavigationAgent2D navAgent { get; set; }
    public override AnimatedSprite2D sprite  {get; set;}
    [Export] public override Base target { get; set; }
    public override Timer cooldown {get; set;}
    public override bool healer { get; set; } = false;

	

    public override void _Ready()
    {
 
        navAgent = GetNode<NavigationAgent2D>("NavAgent");
        sprite = GetNode<AnimatedSprite2D>("Sprite");
        cooldown = GetNode<Timer>("Cooldown"); updateHitboxes();
        navAgent.TargetDesiredDistance = 25;
    }
}
