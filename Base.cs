using Godot;
using System;

public partial class Base : Sprite2D
{
	private RayCast2D ray;
	private NavigationAgent2D navAgent;
	private Node2D target;
	[Export]
	public PackedScene troop;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		target = GetNode<Node2D>("../Icon2");
		ray = GetNode<RayCast2D>("RayCast2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckValid();
	}
	
	public void CheckValid(){
		navAgent.TargetPosition=target.GlobalPosition;
		GD.Print(navAgent.IsTargetReachable());
	}
	
	public bool Reachable(){
		Vector2[] path = navAgent.GetCurrentNavigationPath();
		for (int i = 0; i < path.Length-1; i++)
		{
			Vector2 basePoint = path[i];
			ray.GlobalPosition=basePoint;
			Vector2 targetPoint = path[i+1];
			ray.TargetPosition=ray.ToLocal(targetPoint);
			ray.ForceRaycastUpdate();
			if(ray.IsColliding()){
				GD.Print("true");
			}
			
		}
		return true;
	}
	
	public void spawnTroop(){
		Troop newTroop = troop.Instantiate() as Troop;
		GetParent().AddChild(newTroop);
		newTroop.GlobalPosition=GlobalPosition;
		newTroop.target=target;
	}
}
