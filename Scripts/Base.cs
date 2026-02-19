using Godot;
using System;
using System.Collections.Generic;

public partial class Base : Sprite2D
{
	[Export] public bool player1=false;
	private NavigationAgent2D navAgent;
	[Export] public Node2D target;
	[Export]
	[Export] public int health=100;
	public PackedScene troop;
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

	public override void _Ready(){
		if(player1){
			GameManager.player1Base=this;
			GD.Print("1");
		} else if(!player1){
			GameManager.player2Base=this;
			GD.Print("2");
		}
	}

	public void spawnTroop(Troops troopType){
		if(troopType==Troops.Melee){
			Troop newTroop = troop.Instantiate() as Troop;
			GetParent().AddChild(newTroop);
			newTroop.GlobalPosition=GlobalPosition;
			newTroop.target=target;
			newTroop.player1=player1;
		}
		
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
	public void Die()
	{
		if (health <= 0)
		{
			queue_free()
		}
	}
}
