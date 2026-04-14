using Godot;
using System;
using System.Collections.Generic;

public partial class Base : TargetBase
{
	[Export] public bool player1 = false;
	private NavigationAgent2D navAgent;
	[Export] public TargetBase target;
	[Export] public PackedScene troop;
	[Export] public PackedScene brute;
	[Export] public PackedScene ranged;
	[Export] public PackedScene healer;
	[Export] public int maxTroops = 15;
	private bool releaseTime = false;
	[Export] public override int health { get; set; } = 200;
	[Export] public override int maxHealth { get; set; } = 200;
	[Export] public override bool isBase { get; set; } = true;


	public enum Troops{
		Melee,
		Ranged,
		Brute,
		Healer
	}
	public bool releasing=false;
	public List<Troops> reserveTroops = new List<Troops>();

	public async override void _Ready()
	{
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
			SelfModulate=new Color(1, .2f, .2f, 1);

		}
		if (health > 0) 
		{
			GD.Print("Tower Alive");
		}
		await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);
		GameManager.territory.recalculate();
	}

	public void spawnTroop(Troops troopType){
		GD.Print(troopType);
		BaseTroop newTroop = troop.Instantiate() as BaseTroop;
		if(troopType==Troops.Melee){
			newTroop = troop.Instantiate() as BaseTroop;
		}
		if(troopType==Troops.Brute){
			newTroop = brute.Instantiate() as BaseTroop;
		}
		if(troopType==Troops.Ranged){
			newTroop = ranged.Instantiate() as BaseTroop;
		}
		if(troopType==Troops.Healer){
			newTroop = healer.Instantiate() as BaseTroop;
		}
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
		GetNode<Controller>("Detection").select();
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
	public override void Die() 
	{
		if (health <= 0) 
		{
			QueueFree();
		} 
	}

}
