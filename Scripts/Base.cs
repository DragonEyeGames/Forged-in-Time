using Godot;
using System;
using System.Collections.Generic;

public partial class Base : Sprite2D
{
	[Export] public bool player1=false;
	private NavigationAgent2D navAgent;
	[Export] public Base target;
	[Export] public int health=100;
	[Export] public PackedScene troop;
	[Export] public PackedScene brute;
	[Export] public PackedScene ranged;
	[Export] public PackedScene healer;
	[Export] public int maxTroops=15;
	private bool releaseTime=false;
	public enum Troops{
		Melee,
		Ranged,
		Brute,
		Healer
	}
	public bool releasing=false;
	public List<Troops> reserveTroops = new List<Troops>();

	public async override void _Ready(){
		if(!player1){
			GetNode<CpuParticles2D>("Smoke").Visible=true;
			Texture=(Texture2D)GD.Load("res://Assets/TowerArt/player2Base.png");
			randomEmission();
		} else {
			GetNode<CpuParticles2D>("Smoke").Visible=false;
			Texture=(Texture2D)GD.Load("res://Assets/TowerArt/player1Base.png");
		}
		if(player1){
			GD.Print("Player1");
			GameManager.player1Base=this;
			GetNode<Area2D>("Player-2").QueueFree();
			GetNode<Area2D>("HUD2/Storage/Release/Player-2").QueueFree();
			GetNode<Area2D>("Detection/Player-2").QueueFree();
		} else if(!player1){
			GD.Print("Player2");
			GameManager.player2Base=this;
			GetNode<Area2D>("Player-1").QueueFree();
			GetNode<Area2D>("HUD2/Storage/Release/Player-1").QueueFree();
			GetNode<Area2D>("Detection/Player-1").QueueFree();
		}
		if (health > 0) 
		{
			GD.Print("Tower Alive");
		}
		await ToSignal(GetTree().CreateTimer(0.15f), SceneTreeTimer.SignalName.Timeout);
		GameManager.territory.recalculate();
	}

	public void spawnTroop(Troops troopType){
		PackedScene sceneToSpawn = troop;
		if (troopType == Troops.Brute)
			sceneToSpawn = brute;
		else if (troopType == Troops.Ranged)
			sceneToSpawn = ranged;
		else if (troopType == Troops.Healer)
			sceneToSpawn = healer;
		Troop newTroop = sceneToSpawn.Instantiate<Troop>();;
		GetParent().AddChild(newTroop);
		newTroop.GlobalPosition=GlobalPosition;
		newTroop.target=target;
		newTroop.player1=player1;
	}
	
	public override void _Process(double delta){
		
		if(releasing && reserveTroops.Count>=1 && !releaseTime){
			releaseTroop();
		} else if (reserveTroops.Count==0){
			releasing=false;
		}
		if(player1 && Input.IsActionJustPressed("Click-1")){
			if(GetNode<ColorRect>("HUD2").Visible){
				toggle();
			}
		} else if(!player1 && Input.IsActionJustPressed("Click-2")){
			if(GetNode<ColorRect>("HUD2").Visible){
				toggle();
			}
		}
		GetNode<RichTextLabel>("HUD2/Storage/Troops").Text=reserveTroops.Count.ToString() + "/" + maxTroops.ToString() + " Troops";
		if(player1 && GameManager.player1HUDOpen!=GetNode<CollisionPolygon2D>("Detection/Player-1/CollisionPolygon2D").Disabled){
			GetNode<CollisionPolygon2D>("Detection/Player-1/CollisionPolygon2D").SetDeferred("disabled", GameManager.player1HUDOpen);
			if(GameManager.player1HUDOpen){
				GetNode<ColorRect>("HUD2").Visible=false;
				GetNode<CollisionPolygon2D>("HUD2/Storage/Release/Player-1/CollisionPolygon2D").SetDeferred("disabled", true);
			}
			
		}
		if(!player1 && GameManager.player2HUDOpen!=GetNode<CollisionPolygon2D>("Detection/Player-2/CollisionPolygon2D").Disabled){
			GetNode<CollisionPolygon2D>("Detection/Player-2/CollisionPolygon2D").SetDeferred("disabled", GameManager.player2HUDOpen);
			if(GameManager.player2HUDOpen){
				GetNode<ColorRect>("HUD2").Visible=false;
				GetNode<CollisionPolygon2D>("HUD2/Storage/Release/Player-2/CollisionPolygon2D").SetDeferred("disabled", true);
			}
		}
	}
	
	public void toggle(){
		GetNode<ColorRect>("HUD2").Visible=!GetNode<ColorRect>("HUD2").Visible;
		if(player1){
			GetNode<CollisionPolygon2D>("HUD2/Storage/Release/Player-1/CollisionPolygon2D").SetDeferred("disabled", !GetNode<ColorRect>("HUD2").Visible);
		} else if(!player1){
			GetNode<CollisionPolygon2D>("HUD2/Storage/Release/Player-2/CollisionPolygon2D").SetDeferred("disabled", !GetNode<ColorRect>("HUD2").Visible);
		}
	}
	
	public void release(){
		GetNode<ColorRect>("HUD2").Visible=false;
		GetNode<Controller>("HUD2/Storage/Release").deselect();
		//GetNode<Controller>("Detection").select();
		releasing=true;
		if(releasing && reserveTroops.Count>=1){
			spawnTroop(reserveTroops[0]);
			reserveTroops.RemoveAt(0);
		}
	}
	
	private async void releaseTroop(){
		releaseTime=true;
		await ToSignal(GetTree().CreateTimer(0.15f), Timer.SignalName.Timeout);
		spawnTroop(reserveTroops[0]);
		reserveTroops.RemoveAt(0);
		releaseTime=false;
	}
	
	public void Die() 
	{
		if (health <= 0) 
		{
			GetNode<SignalBus>("/root/SignalBus").EmitSignal(SignalBus.SignalName.PlayerKilled, player1);
			QueueFree();
		} 
	}

	public async void randomEmission(){
		float randomWait = (float)GD.RandRange(10.0f, 25.0f);
   		await ToSignal(GetTree().CreateTimer(randomWait), SceneTreeTimer.SignalName.Timeout);
		GetNode<CpuParticles2D>("Smoke").Emitting=true;
		randomWait = (float)GD.RandRange(5.0f, 8.0f);
   		await ToSignal(GetTree().CreateTimer(randomWait), SceneTreeTimer.SignalName.Timeout);
		GetNode<CpuParticles2D>("Smoke").Emitting=false;
		randomEmission();
	}
}
