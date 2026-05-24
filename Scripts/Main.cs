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
		GetNode<SignalBus>("/root/SignalBus").Connect(
			SignalBus.SignalName.PlayerKilled,
			new Callable(this, nameof(OnPlayerKilled))
		);
		GD.Print("Player 1 Default IS" + GameManager.player1DefaultTarget);
		GameManager.player1Target = GameManager.player1DefaultTarget;
		GameManager.player2Target = GameManager.player2DefaultTarget;
		GD.Print(GameManager.player1Target);
	}

public void OnPlayerKilled(bool player1)
{
	int player=1;
	if(player1==false){
		player=2;
	}
	GD.Print("Player " + player + " died!");
	GameManager.winner=player;
	GetTree().ChangeSceneToFile("res://Scenes/end_screen.tscn");
}
	
	public void addMoney(){
		Player1Manager.money+=50;
		Player2Manager.money+=50;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Player1")){
			//GameManager.player1=true;
			//OnPlayerKilled(true);
		}
		if(Input.IsActionJustPressed("Player2")){
			//GameManager.player1=false;
			//OnPlayerKilled(false);
		}
	}
}
