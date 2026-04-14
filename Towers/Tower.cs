using Godot;
using System;

public abstract partial class Tower : Node2D
{
	[Export]public bool hovering=true;
	[Export]
	public bool Player1 = false;
<<<<<<< HEAD:Towers/Scripts/Tower.cs
	[Export] public GameManager.Towers towerType;
	public Polygon2D polygon;
	public Polygon2D polygon2;
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower):Towers/Tower.cs
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
		if(!Player1){
			color.G=.75f;
			color.B=.75f;
		}
		if(hovering){
			color.A=.5f;
		} else if (color.A!=1.0f) {
			color.A=1.0f;
			if(Player1 && GetNode<CollisionShape2D>("Player1Territory/CollisionShape2D").Disabled){
				GetNode<CollisionShape2D>("Player1Territory/CollisionShape2D").SetDeferred("disabled", false);
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<Hud>("../../HUD").input="";
				GetNode<Territory>("../../Territory").recalculate();
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<Hud>("../../HUD").input="";
				//GetNode<CollisionShape2D>("Player1Territory/CollisionShape2D").SetDeferred("disabled", true);
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<Hud>("../../HUD").toggle();
				GetNode<Hud>("../../HUD").input="";
			}
			if(!Player1 && GetNode<CollisionShape2D>("Player2Territory/CollisionShape2D").Disabled){
				GetNode<CollisionShape2D>("Player2Territory/CollisionShape2D").SetDeferred("disabled", false);
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<Hud>("../../HUD2").input="";
				GetNode<Territory>("../../Territory").recalculate();
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<Hud>("../../HUD2").input="";
				//GetNode<CollisionShape2D>("Player2Territory/CollisionShape2D").SetDeferred("disabled", true);
				await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
				GetNode<Hud>("../../HUD2").toggle();
				GetNode<Hud>("../../HUD2").input="";
			}
		}
		Modulate=color;
	}
	
	public async void sell(){
		GD.Print("Sell");
		if(Player1){
			GD.Print("sedf");
			GetNode<Area2D>("Player1Territory").SetCollisionLayerValue(4, false);
			GetNode<Area2D>("Player1Territory").SetCollisionLayerValue(6, true);
			GD.Print(GetNode<Area2D>("Player1Territory").CollisionLayer);
		} else {
			GD.Print("sssaedf");
			GetNode<Area2D>("Player2Territory").SetCollisionLayerValue(5, false);
			GetNode<Area2D>("Player2Territory").SetCollisionLayerValue(6, true);
		}
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		GameManager.territory.recalculate();
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		if(towerType!=GameManager.Towers.Spikes){
			if(Player1){
				GameManager.player1Placement.replaceBox(GlobalPosition);
			} else {
				GameManager.player2Placement.replaceBox(GlobalPosition);
			}
		} else {
			if(Player1){
				GameManager.player1Placement.replaceBig(GlobalPosition);
			} else {
				GameManager.player2Placement.replaceBig(GlobalPosition);
			}
			GD.Print("spikes");
		}
		polygon.QueueFree();
		polygon2.QueueFree();
		GameManager.baker.CallDeferred("BakePoly");
		
		QueueFree();
	}
}
