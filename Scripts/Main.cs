using Godot;
using System;

public partial class Main : Node2D
{
	[Export]
	public bool keyboard=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.keyboard=keyboard;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Player1")){
			GameManager.player1=true;
		}
		if(Input.IsActionJustPressed("Player2")){
			GameManager.player1=false;
		}
	}
}
