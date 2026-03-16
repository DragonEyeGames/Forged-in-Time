using Godot;
using System;

public partial class Cursor : Sprite2D
{
	[Export] public bool player1=true;
	[Export] public int ID = 0;
	[Export] public Sprite2D screenCounterpart;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(player1){
			Player1Manager.cursor=this;
		}
		if(!player1){
			Player2Manager.cursor=this;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 screenPos = GetViewport().GetCanvasTransform() * GlobalPosition;
		screenCounterpart.GlobalPosition=screenPos;
		if(player1){
			Visible=!Player1Manager.hudOpen;
		}
		if(!player1){
			Visible=!Player2Manager.hudOpen;
		}
		if(!Visible){
			return;
		}
		float rightX = 0.0f;
		float rightY = 0.0f;
		if(!GameManager.keyboard){
			rightX = Input.GetJoyAxis(ID, JoyAxis.LeftX);
			rightY = Input.GetJoyAxis(ID, JoyAxis.LeftY);
		}
		else if(GameManager.keyboard){
			if(player1){
				rightX = Input.GetAxis("Left-1", "Right-1");
				rightY = Input.GetAxis("Up-1", "Down-1");
			}
			else if(!player1){
				rightX = Input.GetAxis("Left-2", "Right-2");
				rightY = Input.GetAxis("Up-2", "Down-2");
			}
		}
		
		if(Math.Abs(rightX)<.1f){
			rightX=0;
		}
		if(Math.Abs(rightY)<.1f){
			rightY=0;
		}
		rightX*=4;
		rightY*=4;
		Vector2 position = Position;
		position.X+=rightX;
		position.Y+=rightY;
		if(position.X<7){
			position.X=7;
		}
		if(position.Y<7){
			position.Y=7;
		}
		if(position.X>1144){
			position.X=1144;
		}
		if(position.Y>640){
			position.Y=640;
		}
		Position=position;
		
	}
}
