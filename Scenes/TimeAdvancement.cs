using Godot;
using System;
using Godot.Collections;

public partial class TimeAdvancement : Control
{
	private bool open=false;
	int upgrade=0;
	[Export] public bool player1 = true;
	public bool upgradeOpen=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
}
