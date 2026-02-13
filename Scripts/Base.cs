using Godot;
using System;
using System.Collections.Generic;

public partial class Base : Sprite2D
{
	private NavigationAgent2D navAgent;
	[Export] public Node2D target;
	[Export]
	public PackedScene troop;
	[Export] public int maxTroops=15;
	public enum Troops{
		Melee,
		Ranged,
		Brute,
		Healer
	}
	public List<Troops> reserveTroops = new List<Troops>();

	public override void _Ready(){
		GameManager.player1Base=this;
	}

	public void spawnTroop(){
		Troop newTroop = troop.Instantiate() as Troop;
		GetParent().AddChild(newTroop);
		newTroop.GlobalPosition=GlobalPosition;
		newTroop.target=target;
	}
	
	public override void _Process(double delta){
		GetNode<RichTextLabel>("HUD2/Storage/Troops").Text=reserveTroops.Count.ToString() + "/" + maxTroops.ToString() + " Troops";
	}
}
