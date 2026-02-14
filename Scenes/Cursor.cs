using Godot;
using System;

public partial class Cursor : Sprite2D
{
	[Export] public bool player1=true;
	[Export] public int ID = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(player1){
			Player1Manager.cursor=this;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player1){
			Visible=!Player1Manager.hudOpen;
		}
		float rightX = Input.GetJoyAxis(ID, JoyAxis.RightX)*5;
		float rightY = Input.GetJoyAxis(ID, JoyAxis.RightY)*5;
		if(Math.Abs(rightX)<.5f){
			rightX=0;
		}
		if(Math.Abs(rightY)<.5f){
			rightY=0;
		}
		Vector2 position = Position;
		position.X+=rightX;
		position.Y+=rightY;
		Position=position;
	}
}
