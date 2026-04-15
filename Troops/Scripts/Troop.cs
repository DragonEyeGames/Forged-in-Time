using Godot;
using System;

public partial class Troop : BaseTroop
{
	[Export] public override float Speed { get; set; } = 40.0f;
	[Export] public override int health { get; set; } = 5;
	[Export] public override int maxHealth { get; set; } = 5;
	[Export] public override int damage { get; set; } = 1;
	
	[Export] public override int upgradeLevel { get; set; } = 0;
	
	[Export] public override int healthLevel { get; set; } = 0;
	[Export] public override int damageLevel { get; set; } = 0;
	[Export] public override int speedLevel {get; set;} = 0;
	
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
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity=Vector2.Zero;
		var dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
		velocity = dir * 40;
		Velocity=velocity;
		if(!navAgent.IsNavigationFinished()){
			MoveAndSlide();
		}
		if(health<=0){
			QueueFree();
			
		}
		if(Velocity.X>0){
			sprite.FlipH=false;
		}
		if(Velocity.X<0){
			sprite.FlipH=true;
		}
	}
	
	public void recalculate(){
		navAgent.TargetPosition=target.GlobalPosition;
	}
	
	public async void updateHitboxes(){
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		if(player1){
			GetNode("Player2").QueueFree();
		} else if(!player1){
			GetNode("Player1").QueueFree();
		}
	}
		
}
