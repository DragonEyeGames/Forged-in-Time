using Godot;
using System;

public partial class ScreenCursor : Sprite2D
{
	public bool player1=false;
	private Controller selected=null;
	private int player;
	
	[Export] public Cursor worldCounterpart;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (player == 0)
		{
			player = 1;
		}
		else
		{
			player = 2;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player1){
			if(selected!=null && Input.IsActionJustPressed("Click-1")){
				selected.EmitSignal(Button.SignalName.Pressed);
			}
		}
		else if(!player1){
			if(selected!=null && Input.IsActionJustPressed("Click-2")){
				selected.EmitSignal(Button.SignalName.Pressed);
			}
		}
		
	}
	
	public void hudInteract(Node2D hudArea){
		//if(selected!=null){
		//	return;
		//}
		if(hudArea.GetParent() is Controller){
			Controller hud = hudArea.GetParent() as Controller;
			hud.select();
			hud.clickedBy = player;
			if(selected!=null){
				selected.deselect();
			}
			selected=hud;
		}
	}
	
	public void hudLeave(Node2D hudArea){
		if(hudArea.GetParent() is Controller){
			Controller hud = hudArea.GetParent() as Controller;
			if(hud==selected){
				hud.deselect();
				selected=null;
			}
			
		}
	}
}
