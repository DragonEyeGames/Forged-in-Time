using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class TimeAdvancement : Control
{
	private bool open=false;
	int upgrade=0;
	int level=0;
	[Export] public bool player1 = true;
	public bool upgradeOpen=false;
	private int maxLevel=2;
	private List<string> newList = new List<string>{"Sends your forces to medieval times and out of the stone age.", "Puts your troops right in modern day America. Strong and powerful towers."};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<RichTextLabel>("Description").Text=newList[0];
		if(player1){
			GetNode<Area2D>("Button/Player2").QueueFree();
		} else {
			GetNode<Area2D>("Button/Player1").QueueFree();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player1){
			GetNode<Button>("Button").Disabled = (Player1Manager.money<500);
		} else {
			GetNode<Button>("Button").Disabled = (Player2Manager.money<500);
		}
		
	}
	
	public void timeAdvance(){
		level+=1;
		GetNode<SignalBus>("/root/SignalBus").EmitSignal(SignalBus.SignalName.TimeAdvance, player1, level);
		if(level==maxLevel){
			QueueFree();
			return;
		}
		GetNode<RichTextLabel>("Description").Text=newList[level];
	}
}
