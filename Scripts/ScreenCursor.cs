using Godot;
using System;

public partial class ScreenCursor : Sprite2D
{
	public bool player1=false;
	private Controller selected=null;
	
	[Export] public Cursor worldCounterpart;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		var bodies = GetChild<Area2D>(0).GetOverlappingAreas();
		if(bodies.Count>0){
			GD.Print(bodies[0]);
			if(selected!=null && selected!=bodies[0].GetParent<Controller>()){
				selected.deselect();
			}
			if(selected!=bodies[0].GetParent<Controller>()){
				bodies[0].GetParent<Controller>().select();
			}
			selected=bodies[0].GetParent<Controller>();
		} else {
			if(selected!=null){
				selected.deselect();
				selected=null;
			}
			
		}
		
	}
	
	public void hudInteract(Node2D hudArea){
		//if(selected!=null){
		//	return;
		//}
		if(hudArea.GetParent() is Controller){
			//Controller hud = hudArea.GetParent() as Controller;
			//hud.select();
			//if(selected!=null){
				//selected.deselect();
			//}
			//selected=hud;
		}
	}
	
	public void hudLeave(Node2D hudArea){
		//if(hudArea.GetParent() is Controller){
		//	Controller hud = hudArea.GetParent() as Controller;
		//	if(hud==selected){
		//		hud.deselect();
		//		//selected=null;
		//	}
			
		//}
	}
}
