using Godot;
using System;

public partial class Player2Manager : Node
{
	public static GameManager.Towers toPlace;
	public static bool placing=false;
	public static bool validPlacement=true;
	public static Cursor cursor;
	public static bool hudOpen=false;
	public static int upgradeLevel=0;
	public static int money=1000;
	
	public static int score=0;
	
	public override void _Ready()
	{
		GetNode<SignalBus>("/root/SignalBus").Connect(
			SignalBus.SignalName.TimeAdvance,
			new Callable(this, nameof(OnTimeAdvance))
		);
	}
	
	public void OnTimeAdvance(bool player1, int level){
		if(!player1){
			upgradeLevel=level;
			GD.Print(upgradeLevel);
		}
	}
}
