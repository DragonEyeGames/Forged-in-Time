using Godot;
using System;

public abstract partial class Tower : Node2D
{
	public bool hovering=true;
	public bool Player1 = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public async void TowerGenerics(){
		Color color = Modulate;
		if(hovering){
			color.A=.5f;
		} else {
			color.A=1.0f;
			if(Player1 && GetNode<CollisionShape2D>("Player1Territory/CollisionShape2D").Disabled){
				GetNode<CollisionShape2D>("Player1Territory/CollisionShape2D").SetDeferred("disabled", false);
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<TerritoryChecker>("../../Territory").recalculate();
				GetNode<Hud>("../../HUD").toggle();
			}
			if(!Player1 && GetNode<CollisionShape2D>("Player2Territory/CollisionShape2D").Disabled){
				GetNode<CollisionShape2D>("Player2Territory/CollisionShape2D").SetDeferred("disabled", false);
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<TerritoryChecker>("../../Territory").recalculate();
				GetNode<Hud>("../../HUD").toggle();
			}
		}
		Modulate=color;
	}
}
