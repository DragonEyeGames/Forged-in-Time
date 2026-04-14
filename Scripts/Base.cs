using Godot;
using System;
using System.Collections.Generic;

public partial class Base : TargetBase
{
	[Export] public bool player1=false;
	private NavigationAgent2D navAgent;
	[Export] public TargetBase target;
	[Export] public int health=100;
	[Export] public PackedScene troop;
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

<<<<<<< HEAD
	public async override void _Ready(){
		if(player1){
			SelfModulate=new Color(1, .2f, .2f, 1);
		}
=======
	public override void _Ready(){
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
		if(player1){
			GameManager.player1Base=this;
<<<<<<< HEAD
			GetNode<Area2D>("Player-2").QueueFree();
			GetNode<Area2D>("HUD2/Storage/Release/Player-2").QueueFree();
			GetNode<Area2D>("Detection/Player-2").QueueFree();
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
		} else if(!player1){
			GameManager.player2Base=this;
<<<<<<< HEAD
			GetNode<Area2D>("Player-1").QueueFree();
			GetNode<Area2D>("HUD2/Storage/Release/Player-1").QueueFree();
			GetNode<Area2D>("Detection/Player-1").QueueFree();
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
		}
		if (health > 0) 
		{
			GD.Print("Tower Alive");
		}
<<<<<<< HEAD
		await ToSignal(GetTree().CreateTimer(0.15f), SceneTreeTimer.SignalName.Timeout);
		GameManager.territory.recalculate();
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
	}

	public void spawnTroop(Troops troopType){
		if(troopType==Troops.Melee){
			Troop newTroop = troop.Instantiate() as Troop;
			GetParent().AddChild(newTroop);
			newTroop.GlobalPosition=GlobalPosition;
			newTroop.target=target;
			newTroop.player1=player1;
		}
<<<<<<< HEAD
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
=======
		
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
	}
	
	public override void _Process(double delta){
		if(GetNode<Node2D>("Territory").Visible){
			GetNode<Node2D>("Territory").Visible=false;
			GetNode<CollisionShape2D>("Territory/CollisionShape2D").SetDeferred("disabled", true);
		}
		if(releasing && reserveTroops.Count>=1 && !releaseTime){
			releaseTroop();
		} else if (reserveTroops.Count==0){
			releasing=false;
		}
		GetNode<RichTextLabel>("HUD2/Storage/Troops").Text=reserveTroops.Count.ToString() + "/" + maxTroops.ToString() + " Troops";
	}
	
	public void toggle(){
		GetNode<ColorRect>("HUD2").Visible=!GetNode<ColorRect>("HUD2").Visible;
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
