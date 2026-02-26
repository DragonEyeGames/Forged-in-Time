using Godot;
using System;

public partial class Cursor : Sprite2D
{
	[Export] public bool player1=true;
	[Export] public int ID = 0;
	private Node2D tower=null;
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
		if(player1){
			Visible=!Player1Manager.hudOpen;
		}
		if(!Visible){
			return;
		}
		float rightX = Input.GetJoyAxis(ID, JoyAxis.LeftX);
		float rightY = Input.GetJoyAxis(ID, JoyAxis.LeftY);
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
		
		if(tower!=null && Input.IsActionJustPressed("Click2")){
			tower.GetNode<Node2D>("NonDamageToweInfo").Visible = true;
		}
		
	}
		
		private void OnP1Entered(Node2D P1Ent){
			tower=P1Ent.GetParent() as Node2D;
		}
		
		private void OnP1Exited(Node2D P1Exit){
			tower=null;
		}
		
		private void OnP2Entered(Node2D P2Ent){
		
		}
	
	}
