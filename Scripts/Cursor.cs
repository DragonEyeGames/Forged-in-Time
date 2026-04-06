using Godot;
using System;

public partial class Cursor : Sprite2D
{
	[Export] public bool player1=true;
	[Export] public int ID = 0;
	[Export] public ScreenCursor screenCounterpart;
	Node2D tower;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(player1){
			Player1Manager.cursor=this;
		}
		if(!player1){
			Player2Manager.cursor=this;
		}
 		screenCounterpart.player1=player1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player1){
		//	Visible=!Player1Manager.hudOpen;
		}
		if(!player1){
		//	Visible=!Player2Manager.hudOpen;
		}
		if(!Visible){
		//	return;
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
		rightX*=5;
		rightY*=5;
		Vector2 position = Position;
		position.X+=rightX;
		position.Y+=rightY;
		if(position.X<7){
			position.X=7;
		}
		if(position.Y<25){
			position.Y=25;
		}
		if(position.X>1059){
			position.X=1059;
		}
		if(position.Y>611){
			position.Y=611;
		}
		Position=position;
		
		if(tower!=null && Input.IsActionJustPressed("Click2")){
			tower.GetNode<ColorRect>("NonDamageToweInfo").Visible = true;
		}
	}
		
		private void OnP1Entered(Node2D P1Ent){
			tower=P1Ent.GetParent() as Node2D;
		}
		
		private void OnP1Exited(Node2D P1Exit){
			tower=null;
		}
		
		private void OnP2Entered(Node2D P2Ent){
			tower=P2Ent.GetParent() as Node2D;
		}
		
		private void OnP2Exited(Node2D P2Exit){
			tower=null;
		}
	
//		Vector2 screenPos = GetViewport().GetCanvasTransform() * GlobalPosition;
		//screenCounterpart.GlobalPosition=screenPos;
		
	}
