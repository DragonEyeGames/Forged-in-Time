using Godot;
using System;

public partial class Healer : BaseTroop
{
    
    [Export] public override float Speed { get; set; } = 25.0f;
    [Export] public  override int health { get; set; } = 3;
    [Export] public override int damage { get; set; } = 1;
    public override NavigationAgent2D navAgent { get; set; }
    public override AnimatedSprite2D sprite  {get; set;}
    [Export] public override Base target { get; set; }
    public override Timer cooldown {get; set;}
    public override bool healer { get; set; } = true;

	

    public override void _Ready()
    {
 
        navAgent = GetNode<NavigationAgent2D>("NavAgent");
        sprite = GetNode<AnimatedSprite2D>("Sprite");
        cooldown = GetNode<Timer>("Cooldown"); updateHitboxes();
        navAgent.TargetDesiredDistance = 25;
    }


}
