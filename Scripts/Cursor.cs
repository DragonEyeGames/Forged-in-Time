using Godot;
using System;

public partial class Cursor : Sprite2D
{
	[Export] public bool player1=true;
	[Export] public int ID = 0;
	[Export] public ScreenCursor screenCounterpart;
	private Controller selected=null;
	public int player = 0;
	private float speed=1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(player1){
			Player1Manager.cursor=this;
			player=1;
		}
		else if(!player1){
			Player2Manager.cursor=this;
			player=2;
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
		rightX*=speed;
		rightY*=speed;
		if(rightX!=0 || rightY!=0){
			//speed*=1.03f;
			if(speed>5){
				speed=5;
			}
		} else {
			speed=5;
		}
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
		
		Vector2 screenPos = GetViewport().GetCanvasTransform() * GlobalPosition;
		screenCounterpart.GlobalPosition=screenPos;
		
		if(player1){
			if(selected!=null && Input.IsActionJustPressed("Click-1") && !selected.Disabled){
				selected.clickedBy = player;
				selected.EmitSignal(Button.SignalName.Pressed);
			}
		}
		else if(!player1){
			if(selected!=null && Input.IsActionJustPressed("Click-2") && !selected.Disabled){
				selected.clickedBy = player;
				selected.EmitSignal(Button.SignalName.Pressed);
			}
		}
	}
	
	public void towerInteract(Node2D towerArea){
		//if(selected!=null){
		//	return;
		//}
		if(towerArea.GetParent() is Controller){
			Controller tower = towerArea.GetParent() as Controller;
			tower.select();
			if(selected!=null){
				selected.deselect();
			}
			selected=tower;
		}
	}
	
	public void towerLeave(Node2D towerArea){
		if(towerArea.GetParent() is Controller){
			Controller tower = towerArea.GetParent() as Controller;
			if(tower==selected){
				tower.deselect();
				selected=null;
			}
			
		}
	}
}
